using System;
using System.Linq;
using System.Collections.Generic;

public class ZooController
{
    private Zoo _zoo;

    public ZooController(Zoo zoo)
    {
        _zoo = zoo;
    }

    public double GetAverageWeightByType(AnimalType type)
    {
        var animalsOfType = _zoo.Where(a => a.Type == type).ToList();
        if (animalsOfType.Count == 0) return 0;

        return animalsOfType.Average(a => a.Weight);
    }

    public int CountPredatoryBirds()
    {
        return _zoo.Count(a => a is Bird && a is IHuntable);
    }

    public List<Animal> GetAnimalsSortedByBirthYear()
    {
        return _zoo.OrderBy(a => a.BirthYear).ToList();
    }
    
    public void DisplayAnimalsSortedByBirthYear()
    {
        List<Animal> sortedAnimals = GetAnimalsSortedByBirthYear();
        Console.WriteLine("=== Животные отсортированные по году рождения ===");
        foreach(Animal animal in sortedAnimals)
        {
            Console.WriteLine($"{animal.Name} - {animal.BirthYear} год");
            
        }
    }

}

public static class ZooExtensions
{
    public static IEnumerable<Animal> Where(this Zoo zoo, Func<Animal, bool> predicate)
    {
        for(int i =0;i<zoo.Count;i++)
        {
            if (predicate(zoo[i]))
                yield return zoo[i];
        }
    }
}