using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ProgrammingElement
{
    public string Name { get; set; }

    public ProgrammingElement(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}

public class Operator : ProgrammingElement
{
    public Operator(string name) : base(name) { }
}

public class Technology : ProgrammingElement
{
    public Technology(string name) : base(name) { }
}

public class Concept : ProgrammingElement
{
    public Concept(string name) : base(name) { }
}

public class Programmer
{
    public delegate void RenameHandler(object sender, string newName);
    public delegate void NewPropertyHandler(object sender, ProgrammingElement element);

    public event RenameHandler OnRename;
    public event NewPropertyHandler OnNewProperty;

    public void Rename(string name)
    {
        Console.WriteLine($"Программист пытается переименовать на: {name}");
        OnRename?.Invoke(this, name);
    }

    public void AddNewProperty(ProgrammingElement element)
    {
        Console.WriteLine($"Программист добавляет новый элемент: {element}");
        OnNewProperty?.Invoke(this, element);
    }
}

public class ProgrammingLanguage
{
    public string Name { get; set; }
    public string Version { get; set; }
    public List<ProgrammingElement> Elements { get; set; }

    public ProgrammingLanguage(string name, string version)
    {
        Name = name;
        Version = version;
        Elements = new List<ProgrammingElement>();

        Elements.AddRange(new ProgrammingElement[]
        {
            new Operator("if"),
            new Operator("for"),
            new Concept("ООП")
        });
    }

    public void HandleRename(object sender, string newName)
    {
        if (sender is Programmer)
        {
            string oldName = Name;
            Name = newName;
            Console.WriteLine($"Язык {oldName} переименован на {Name}");
        }
    }

    public void HandleVersionChange(object sender, string newVersion)
    {
        if (sender is Programmer)
        {
            string oldVersion = Version;
            Version = newVersion;
            Console.WriteLine($"Версия {oldVersion} изменена на {Version}");
        }
    }

    public void HandleNewProperty(object sender, ProgrammingElement element)
    {
        if (sender is Programmer && !Elements.Exists(e => e.Name == element.Name))
        {
            Elements.Add(element);
            Console.WriteLine($"{Name} получил новое свойство: {element}");
        }
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"\n--- {Name} v{Version} ---");
        Console.WriteLine("Элементы: " + string.Join(", ", Elements));
    }
}

public static class StringProcessor
{
    public static string RemovePunctuation(string input) =>
        new string(input.Where(c => !char.IsPunctuation(c)).ToArray());

    public static string ToUpperCase(string input) =>
        input.ToUpper();

    public static string RemoveExtraSpaces(string input) =>
        string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

    public static string ReplaceRussianLetters(string input)
    {
        var result = new StringBuilder();
        char replacedChar;

        foreach (char c in input)
        {
            replacedChar = c;

            if (c >= 'а' && c <= 'я')
            {
                replacedChar = (char)('a' + (c - 'a'));
            }
            else if (c >= 'А' && c <= 'Я')
            {
                replacedChar = (char)('A' + (c - 'A'));
            }
            else if (c == 'ё')
            {
                replacedChar = 'е';
            }
            else if (c == 'Ё')
            {
                replacedChar = 'Е';
            }

            result.Append(replacedChar);
        }
        return result.ToString();
    }

    public static string ReverseString(string input) =>
        new string(input.Reverse().ToArray());

    public static string ProcessString(string input, List<Func<string, string>> processors)
    {
        return processors.Aggregate(input, (current, processor) => processor(current));
    }
}

namespace lab_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЧАСТЬ 1: ОБРАБОТКА СТРОК С ДЕЛЕГАТАМИ ===\n");

            string testString = "  Привет,   мир! Это  тестовая  строка...  ";

            Console.WriteLine($"Исходная строка: '{testString}'");

            Action<string> printAction = (str) => Console.WriteLine($"Результат: '{str}'");

            Predicate<string> isLongString = (str) => str.Length > 10;
            Console.WriteLine($"Строка длиннее 10 символов: {isLongString(testString)}");

            Func<string, string> simpleProcessor = (str) => str.Trim().ToUpper();
            printAction(simpleProcessor(testString));

            Console.WriteLine("\n=== ПОСЛЕДОВАТЕЛЬНАЯ ОБРАБОТКА СТРОКИ ===");

            var processors = new List<Func<string, string>>
            {
                StringProcessor.RemoveExtraSpaces,
                StringProcessor.RemovePunctuation,
                StringProcessor.ToUpperCase,
                str => str.Replace(" ", "_")
            };

            string processed = StringProcessor.ProcessString(testString, processors);
            Console.WriteLine($"Финальный результат: {processed}");

            Console.WriteLine("\n=== АЛЬТЕРНАТИВНАЯ ОБРАБОТКА ===");
            var alternativeProcessors = new List<Func<string, string>>
            {
                StringProcessor.RemovePunctuation,
                StringProcessor.ReplaceRussianLetters,
                StringProcessor.ReverseString,
                str => $"{str}"
            };

            string alternativeResult = StringProcessor.ProcessString("Пример текст!", alternativeProcessors);
            Console.WriteLine($"Альтернативный результат: {alternativeResult}");

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("=== ЧАСТЬ 2: ЯЗЫКИ ПРОГРАММИРОВАНИЯ И СОБЫТИЯ ===\n");

            Programmer programmer = new Programmer();

            var languages = new List<ProgrammingLanguage>
            {
                new ProgrammingLanguage("C#", "8.0"),
                new ProgrammingLanguage("Java", "15"),
                new ProgrammingLanguage("Python", "3.9"),
                new ProgrammingLanguage("JavaScript", "ES2020"),
                new ProgrammingLanguage("Go", "1.16")
            };

            programmer.OnRename += languages[0].HandleRename;
            programmer.OnNewProperty += languages[0].HandleNewProperty;

            programmer.OnNewProperty += languages[1].HandleNewProperty;

            programmer.OnRename += (sender, newName) =>
            {
                if (sender is Programmer)
                {
                    Console.WriteLine($"Python отказался менять имя, но изменил версию на: {newName}");
                    languages[2].HandleVersionChange(sender, newName + ".python");
                }
            };
            programmer.OnNewProperty += languages[2].HandleNewProperty;

            programmer.OnRename += languages[3].HandleRename;

            programmer.OnRename += (sender, newName) =>
            {
                if (newName.Contains("Go") && sender is Programmer)
                {
                    string oldName = languages[4].Name;
                    languages[4].Name = newName;
                    Console.WriteLine($"Go был переименован из {oldName} в {newName}");
                }
            };

            programmer.OnNewProperty += (sender, element) =>
            {
                if (element is Technology tech && sender is Programmer)
                {
                    languages[4].Elements.Add(tech);
                    Console.WriteLine($"Go получил технологию: {tech.Name}");
                }
            };

            Console.WriteLine("=== НАЧАЛЬНОЕ СОСТОЯНИЕ ===");
            languages.ForEach(lang => lang.DisplayInfo());

            Console.WriteLine("\n=== ДЕЙСТВИЯ ПРОГРАММИСТА ===");

            programmer.Rename("CSharp 9.0");
            programmer.Rename("GoLang");
            programmer.Rename("Python 4.0");

            programmer.AddNewProperty(new Technology("LINQ"));
            programmer.AddNewProperty(new Operator("null-coalescing"));
            programmer.AddNewProperty(new Concept("Pattern Matching"));
            programmer.AddNewProperty(new Technology("Goroutines"));
            programmer.AddNewProperty(new Concept("Async/Await"));

            Console.WriteLine("\n=== КОНЕЧНОЕ СОСТОЯНИЕ ===");
            languages.ForEach(lang => lang.DisplayInfo());

            Console.WriteLine("\n=== ДОПОЛНИТЕЛЬНЫЕ ДЕЙСТВИЯ ===");
            programmer.Rename("TypeScript");
            programmer.AddNewProperty(new Technology("WebAssembly"));

            Console.WriteLine("\n=== ФИНАЛЬНОЕ СОСТОЯНИЕ ===");
            languages.ForEach(lang => lang.DisplayInfo());

            Console.WriteLine("\n=== МНОЖЕСТВЕННЫЕ ДЕЛЕГАТЫ ===");

            Action<string> multiDelegate = null;
            multiDelegate += (str) => Console.WriteLine($"Обработчик 1: {str}");
            multiDelegate += (str) => Console.WriteLine($"Обработчик 2: {str.ToUpper()}");
            multiDelegate += (str) => Console.WriteLine($"Обработчик 3: Длина = {str.Length}");

            Console.WriteLine("Вызов multicast делегата:");
            multiDelegate?.Invoke("Тестовая строка");

            Console.WriteLine("\nПрограмма завершена!");
        }
    }
}
