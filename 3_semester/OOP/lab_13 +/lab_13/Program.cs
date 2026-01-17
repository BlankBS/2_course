using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static BinarySerializer;

public class BinarySerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        using (var fs = File.Open(path, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            bf.Serialize(fs, obj);
        }
    }

    public T Deserialize<T>(string path)
    {
        using (var fs = File.Open(path, FileMode.Open))
        {
            var bf = new BinaryFormatter();
            var obj = (T)bf.Deserialize(fs);
            return obj;
        }
    }
}
public class SoapSerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        var soapType = Type.GetType("System.Runtime.Serialization.Formatters.Soap.SoapFormatter, System.Runtime.Serialization.Formatters.Soap");
        if (soapType == null)
        {
            throw new NotSupportedException("SoapFormatter not available in this runtime.");
        }

        dynamic formatter = Activator.CreateInstance(soapType);
        using (var fs = File.Open(path, FileMode.Create))
        {
            formatter.Serialize(fs, obj);
        }
    }

    public T Deserialize<T>(string path)
    {
        var soapType = Type.GetType("System.Runtime.Serialization.Formatters.Soap.SoapFormatter, System.Runtime.Serialization.Formatters.Soap");
        if (soapType == null)
        {
            throw new NotSupportedException("SoapFormatter not available in this runtime.");
        }

        dynamic formatter = Activator.CreateInstance(soapType);
        using (var fs = File.Open(path, FileMode.Open))
        {
            object obj = formatter.Deserialize(fs);
            return (T)obj;
        }
    }
}

public class JsonNetSerializer : ISerializer
{
    
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        Formatting = Newtonsoft.Json.Formatting.Indented,
        PreserveReferencesHandling = PreserveReferencesHandling.None
    };

    public void Serialize<T>(T obj, string path)
    {
        var txt = JsonConvert.SerializeObject(obj, _settings);
        File.WriteAllText(path, txt);
    }

    public T Deserialize<T>(string path)
    {
        var txt = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(txt, _settings);
    }
}

public class XmlSerializerImpl : ISerializer
{
    private readonly Type[] _knownTypes;

    public XmlSerializerImpl(params Type[] knownTypes)
    {
        _knownTypes = knownTypes;
    }

    public void Serialize<T>(T obj, string path)
    {
        var type = typeof(T);
        var xs = new XmlSerializer(type, _knownTypes);
        using (var fs = File.Open(path, FileMode.Create))
        {
            xs.Serialize(fs, obj);
        }
    }
    public T Deserialize<T>(string path)
    {
        var type = typeof(T);
        var xs = new XmlSerializer(type, _knownTypes);
        using (var fs = File.Open(path, FileMode.Open))
        {
            return (T)xs.Deserialize(fs);
        }
    }
}


namespace lab_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal[] animals = new Animal[]
            {
                new Lion("Simba", 5, 190.5, 4, "golden") { SecretNote = "Lion secret" },
                new Tiger("ShereKhan", 7, 220.1, 3, "black-orange") { SecretNote = "Tiger secret" },
                new Owl("Hedwig", 3, 2.3, 75, true) { SecretNote = "Owl secret" },
                new Parrot("Polly", 2, 0.9, 25, true) { SecretNote = "Parrot secret" },
                new Shark("Jaws", 10, 800, 40, 300) { SecretNote = "Shark secret" },
                new Crocodile("Gena", 12, 500, 1500) { SecretNote = "Croco secret" }
            };

            var binPath = "animals.bin";
            var soapPath = "animals.soap";
            var jsonPath = "animals.json";
            var xmlPath = "animals.xml";

            Type[] knownTypes = new Type[]
            {
                typeof(Lion), typeof(Tiger), typeof(Owl), typeof(Parrot), typeof(Shark), typeof(Crocodile)
            };

            ISerializer binary = new BinarySerializer();
            ISerializer soap = new SoapSerializer();
            ISerializer json = new JsonNetSerializer();
            ISerializer xml = new XmlSerializerImpl(knownTypes);

            binary.Serialize(animals, binPath);
            Console.WriteLine("Saved binary -> " + binPath);

            try
            {
                soap.Serialize(animals, soapPath);
                Console.WriteLine("Saved SOAP -> " + soapPath);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("SOAP serializer not available: " + e.Message);
            }

            json.Serialize(animals, jsonPath);
            Console.WriteLine("Saved JSON -> " + jsonPath);

            xml.Serialize(animals, xmlPath);
            Console.WriteLine("Saved XML -> " + xmlPath);

            var animalsFromBinary = binary.Deserialize<Animal[]>(binPath);
            Console.WriteLine("\nFrom Binary deserialized:");
            foreach (var a in animalsFromBinary)
            {
                Console.WriteLine(a);
                Console.WriteLine(" SecretNote: " + (a.SecretNote ?? "<NULL>"));
            }

            try
            {
                var animalsFromSoap = soap.Deserialize<Animal[]>(soapPath);
                Console.WriteLine("\nFrom SOAP deserialized:");
                foreach (var a in animalsFromSoap)
                {
                    Console.WriteLine(a);
                    Console.WriteLine(" SecretNote: " + (a.SecretNote ?? "<NULL>"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nSOAP deserialize skipped (not available): " + ex.Message);
            }

            var animalsFromJson = json.Deserialize<Animal[]>(jsonPath);
            Console.WriteLine("\nFrom JSON deserialized:");
            foreach (var a in animalsFromJson)
            {
                Console.WriteLine(a);
                Console.WriteLine(" SecretNote: " + (a.SecretNote ?? "<NULL>"));
            }

            var animalsFromXml = xml.Deserialize<Animal[]>(xmlPath);
            Console.WriteLine("\nFrom XML deserialized:");
            foreach (var a in animalsFromXml)
            {
                Console.WriteLine(a);
                Console.WriteLine(" SecretNote: " + (a.SecretNote ?? "<NULL>"));
            }


            Console.WriteLine("\n--- Проверка файлов (открываем первые 400 символов) ---");
            Console.WriteLine("JSON snippet:");
            Console.WriteLine(File.ReadAllText(jsonPath).Substring(0, Math.Min(400, File.ReadAllText(jsonPath).Length)));
            Console.WriteLine("\nXML snippet:");
            Console.WriteLine(File.ReadAllText(xmlPath).Substring(0, Math.Min(400, File.ReadAllText(xmlPath).Length)));

            Console.WriteLine("\n--- XPath пример ---");
            var doc = new XmlDocument();
            doc.Load(xmlPath);

            var ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            var lionNodes = doc.SelectNodes("//Animal[@xsi:type='Lion']", ns);
            Console.WriteLine($"Львов найдено (XPath //Animal[@xsi:type='Lion']): {lionNodes?.Count ?? 0}");

            var olderThanFive = doc.SelectNodes("//Animal[Age>5]");
            Console.WriteLine($"Животных старше 5 лет (XPath //Animal[Age>5]): {olderThanFive?.Count ?? 0}");

            Console.WriteLine("\n--- LINQ to XML ---");
            var xdoc = XDocument.Load(xmlPath);
            var names = xdoc.Descendants("Animal")
                            .Where(x =>
                            {
                                var ageEl = x.Element("Age");
                                if (ageEl == null) return false;
                                int age;
                                return int.TryParse(ageEl.Value, out age) && age > 3;
                            })
                            .Select(x => x.Element("Name")?.Value)
                            .Where(n => n != null);

            Console.WriteLine("Animals with Age > 3 by LINQ to XML:");
            foreach (var n in names) Console.WriteLine(" " + n);

            Console.WriteLine("\n--- LINQ to JSON ---");
            var jsonText = File.ReadAllText(jsonPath);
            var jarr = JsonConvert.DeserializeObject<dynamic>(jsonText);
            var jArray = jarr as Newtonsoft.Json.Linq.JArray;
            var heavy = jArray?.Children<Newtonsoft.Json.Linq.JObject>()
                          .Where(o => (double)o["Weight"] > 200)
                          .Select(o => (string)o["Name"]);

            Console.WriteLine("Animals with Weight > 200 by LINQ to JSON:");
            if (heavy != null)
            {
                foreach (var n in heavy) Console.WriteLine(" " + n);
            }

        }
    }
}
