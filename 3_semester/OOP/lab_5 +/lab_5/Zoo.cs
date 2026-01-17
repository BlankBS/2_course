using System;
using System.Collections.Generic;

public class Zoo : IEnumerable<Animal>
{
    private List<Animal> _animals;

    public Zoo()
    {
        _animals = new List<Animal>();
    }

    public IEnumerator<Animal> GetEnumerator()
    {
        return _animals.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Animal GetAnimal(int index) => _animals[index];
    public void SetAnimal(int index, Animal animal) => _animals[index] = animal;

    public void AddAnimal(Animal animal) => _animals.Add(animal);
    public bool RemoveAnimal(Animal animal) => _animals.Remove(animal);

    public void RemoveAt(int index) => _animals.RemoveAt(index);

    public void PrintAllAnimals()
    {
        Console.WriteLine("=== Все животные в зоопарке ===");
        foreach (var animal in _animals)
        {
            Console.WriteLine(animal.ToString());
            Console.WriteLine($"   {animal.Info}");
            Console.WriteLine();
        }
    }

    public Animal this[int index]
    {
        get => _animals[index];
        set => _animals[index] = value;
    }

    public int Count => _animals.Count;
}