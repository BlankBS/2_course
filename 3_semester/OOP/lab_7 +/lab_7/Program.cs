using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Testing CollectionType<T> with Animal Hierarchy ===\n");

        TestWithAnimalTypes();

        TestWithStandardTypes();

        TestFileOperations();

        TestOperatorOverloading();

        TestExceptionHandling();
    }

    static void TestWithAnimalTypes()
    {
        Console.WriteLine("--- Testing with Animal Types ---");

        var animalCollection = new CollectionType<Animal>();

        animalCollection.Add(new Lion("Simba", 5, 190, 4, "Golden"));
        animalCollection.Add(new Tiger("Rajah", 4, 220, 3, "Vertical"));
        animalCollection.Add(new Owl("Hedwig", 2, 1.5, 120, true));
        animalCollection.Add(new Parrot("Rio", 3, 0.4, 80, true));
        animalCollection.Add(new Shark("Jaws", 8, 450, 30, 3000));
        animalCollection.Add(new Crocodile("Snappy", 10, 150, 2200));

        animalCollection.DisplayAll();

        Console.WriteLine("\n--- Searching for heavy animals (Weight > 100) ---");
        var heavyAnimals = animalCollection.Find(a => a.Weight > 100);
        foreach (var animal in heavyAnimals)
        {
            Console.WriteLine($"  {animal}");
        }

        Console.WriteLine("\n--- Searching for young animals (Age < 5) ---");
        var youngAnimals = animalCollection.Find(a => a.Age < 5);
        foreach (var animal in youngAnimals)
        {
            Console.WriteLine($"  {animal}");
        }

        Console.WriteLine("\n--- Removing an animal ---");
        var animalToRemove = animalCollection.Find(a => a.Name == "Rio").FirstOrDefault();
        if (animalToRemove != null)
        {
            animalCollection.Remove(animalToRemove);
        }

        animalCollection.DisplayAll();
    }

    static void TestWithStandardTypes()
    {
        Console.WriteLine("\n--- Testing with Standard Types (via wrapper classes) ---");

        var intCollection = new CollectionType<IntAnimal>();
        intCollection.Add(new IntAnimal("Prime", 17));
        intCollection.Add(new IntAnimal("Even", 42));
        intCollection.Add(new IntAnimal("Odd", 13));
        intCollection.DisplayAll();

        var doubleCollection = new CollectionType<DoubleAnimal>();
        doubleCollection.Add(new DoubleAnimal("Pi", 3.14159));
        doubleCollection.Add(new DoubleAnimal("Euler", 2.71828));
        doubleCollection.Add(new DoubleAnimal("GoldenRatio", 1.61803));
        doubleCollection.DisplayAll();

        var stringCollection = new CollectionType<StringAnimal>();
        stringCollection.Add(new StringAnimal("Hello", "World"));
        stringCollection.Add(new StringAnimal("Programming", "C#"));
        stringCollection.Add(new StringAnimal("Animal", "Collection"));
        stringCollection.DisplayAll();

        var largeInts = intCollection.Find(x => x.Value > 20);
        Console.WriteLine("Large integers (>20):");
        foreach (var num in largeInts)
        {
            Console.WriteLine($"  {num}");
        }
    }

    static void TestFileOperations()
    {
        Console.WriteLine("\n--- Testing File Operations ---");

        var lionCollection = new CollectionType<Lion>();
        lionCollection.Add(new Lion("Nala", 4, 160, 4, "Light Brown"));
        lionCollection.Add(new Lion("Simba", 5, 190, 4, "Golden"));

        Console.WriteLine("Original lion collection:");
        lionCollection.DisplayAll();

        lionCollection.SaveToJson("lions.json");

        var loadedLions = new CollectionType<Lion>();
        loadedLions.LoadFromJson("lions.json");
        Console.WriteLine("Loaded lion collection from JSON:");
        loadedLions.DisplayAll();

        var birdCollection = new CollectionType<Owl>();
        birdCollection.Add(new Owl("Hedwig", 2, 1.5, 120, true));
        birdCollection.Add(new Owl("Nightwing", 3, 2.1, 130, true));
        birdCollection.SaveToJson("owls.json");

        var loadedBirds = new CollectionType<Owl>();
        loadedBirds.LoadFromJson("owls.json");
        Console.WriteLine("Loaded owl collection from JSON:");
        loadedBirds.DisplayAll();

        var animalCollection = new CollectionType<Animal>();
        animalCollection.Add(new Lion("Alex", 6, 200, 4, "Brown"));
        animalCollection.Add(new Tiger("Richard", 5, 220, 3, "Striped"));

        animalCollection.SaveToXml("animals.xml");
        Console.WriteLine("Animals saved to XML");

        var loadedAnimals = new CollectionType<Animal>();
        loadedAnimals.LoadFromXml("animals.xml");
        Console.WriteLine("Loaded animals from XML:");
        loadedAnimals.DisplayAll();
    }

    static void TestOperatorOverloading()
    {
        Console.WriteLine("\n--- Testing Operator Overloading ---");

        var collection1 = new CollectionType<Bird>();

        collection1 += new Owl("Nightwing", 3, 2.1, 130, true);
        collection1 += new Parrot("Polly", 2, 0.5, 75, false);

        Console.WriteLine("After using + operator:");
        collection1.DisplayAll();

        var birdToRemove = collection1.Find(b => b.Name == "Polly").FirstOrDefault();
        if (birdToRemove != null)
        {
            collection1 -= birdToRemove;
        }

        Console.WriteLine("After using - operator:");
        collection1.DisplayAll();

        var collection2 = new CollectionType<Bird>();
        collection2.Add(new Owl("Nightwing", 3, 2.1, 130, true));

        Console.WriteLine($"collection1 == collection2: {collection1 == collection2}");
        Console.WriteLine($"collection1 != collection2: {collection1 != collection2}");
    }

    static void TestExceptionHandling()
    {
        Console.WriteLine("\n--- Testing Exception Handling ---");

        var testCollection = new CollectionType<Fish>();

        try
        {
            testCollection.Add(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception when adding null: {ex.GetType().Name}");
        }

        try
        {
            testCollection.Find(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception when searching with null predicate: {ex.GetType().Name}");
        }

        try
        {
            var fish = testCollection[10];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception when accessing invalid index: {ex.GetType().Name}");
        }

        try
        {
            var collection = new CollectionType<Animal>();
            collection.LoadFromJson("nonexistent.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception when loading from nonexistent file: {ex.GetType().Name}");
        }
    }
}