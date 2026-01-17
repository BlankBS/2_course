using System;

class Program
{
    static void Print(string t) => Console.WriteLine($"\n===== {t} =====");

    static void TestType(string typeName)
    {
        Print(typeName);

        Console.WriteLine("Сборка: " + Reflector.GetAssemblyName(typeName));
        Console.WriteLine("Есть публичные конструкторы: " + Reflector.HasPublicConstructors(typeName));

        Console.WriteLine("\nМетоды:");
        foreach (var m in Reflector.GetPublicMethods(typeName)) Console.WriteLine("  " + m);

        Console.WriteLine("\nПоля / Свойства:");
        foreach (var m in Reflector.GetFieldsAndProps(typeName)) Console.WriteLine("  " + m);

        Console.WriteLine("\nИнтерфейсы:");
        foreach (var i in Reflector.GetInterfaces(typeName)) Console.WriteLine("  " + i);

        Console.WriteLine("\nМетоды с параметром double:");
        foreach (var m in Reflector.MethodsWithParameterType(typeName, "System.Double")) Console.WriteLine("  " + m);
    }

    static void Main()
    {
        TestType("StackDouble");
        TestType("Concert");
        TestType("ConcertManager");

        Print("Invoke");
        var concert = new Concert("Show", "Artist", DateTime.Now, 100);
        Reflector.Invoke(concert, "ShowInfo");

        Print("Invoke с параметрами");
        var manager = new ConcertManager();
        Reflector.Invoke(manager, "AddConcert", new object[]
        {
            1, new Concert("RockFest", "Metallica", DateTime.Now, 250)
        });

        manager.PrintConcerts();

        Print("Create<T>");
        var created = Reflector.Create<Concert>();
        Console.WriteLine("Создан объект: " + created);

        Reflector.WriteToFile("reflection_output.txt", "Работа завершена");
        Console.WriteLine("\nФайл reflection_output.txt создан.");
    }
}
