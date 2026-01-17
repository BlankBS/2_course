using Microsoft.Win32.SafeHandles;
using System;
public enum AnimalType
{
    Mammal,
    Bird,
    Fish,
    Reptile
}

public struct AnimalInfo
{
    public DateTime BirthDate;
    public string Habitat;
    public bool IsEndangered;

    public AnimalInfo(DateTime birthDate, string habitat, bool isEndangered)
    {
        BirthDate = birthDate;
        Habitat = habitat;
        IsEndangered = isEndangered;
    }

    public override string ToString()
    {
        return $"Born: {BirthDate:yyyy-MM-dd}, Habitat: {Habitat}, Endangered: {IsEndangered}";
    }
}

interface IMovable
{
    string Move();
}

interface IHuntable
{
    string Hunt();
}
abstract class Mammal : Animal, IHuntable
{
    public int GestationPeriod { get; set; }

    protected Mammal(string name, int age, double weight, int gestationPeriod, DateTime birthDate) 
    : base(name, age, weight, AnimalType.Mammal, birthDate)
    {
        GestationPeriod = gestationPeriod;
    }

    public override string Move()
    {
        return $"{Name} runs on ground";
    }

    public override string Hunt()
    {
        return $"{Name} hunts other animals";
    }

    public override string ToString()
    {
        return base.ToString() + $", Gestation: {GestationPeriod} months";
    }
}

abstract class Bird : Animal, IHuntable
{
    public double Wingspan { get; set; }

    protected Bird(string name, int age, double weight, double wingspan, DateTime birthDate)
    : base(name, age, weight, AnimalType.Bird, birthDate)
    {
        Wingspan = wingspan;
    }

    public override string Move()
    {
        return $"{Name} flies in the sky";
    }

    public override string Hunt()
    {
        return $"{Name} searches for prey from the air";
    }

    public override string ToString()
    {
        return base.ToString() + $", Wignspan: {Wingspan}cm";
    }
}

abstract class Fish : Animal, IHuntable
{
    public double SwimSpeed { get; set; }

    protected Fish(string name, int age, double weight, double swimSpeed, DateTime birthDate)
    : base(name, age, weight, AnimalType.Fish, birthDate)
    {
        SwimSpeed = swimSpeed;
    }

    public override string Move()
    {
        return $"{Name} swims in water";
    }

    public override string Hunt()
    {
        return $"{Name} hunts in water";
    }

    public override string ToString()
    {
        return base.ToString() + $", Swim speed: {SwimSpeed} km/h";
    }
}

sealed class Lion : Mammal
{
    public string ManeColor { get; set; }

    public Lion(string name, int age, double weight, int gestationPeriod, string maneColor, DateTime birthDate) 
    : base(name, age, weight, gestationPeriod, birthDate)
    {
        ManeColor = maneColor;
    }

    public override string Hunt()
    {
        return $"{Name} the lion hunts in pride with {ManeColor} mane";
    }

    public override string ToString()
    {
        return base.ToString() + $", Mane Color: {ManeColor}";
    }
}

sealed class Tiger : Mammal
{
    public string StripesPatter { get; set; }

    public Tiger(string name, int age, double weight, int gestationPeriod, string stripesPatter, DateTime birthDate) 
    : base(name, age, weight, gestationPeriod, birthDate)
    {
        StripesPatter = stripesPatter;
    }

    public override string Hunt()
    {
        return $"{Name} the tiger hunts solo with {StripesPatter} stripes";
    }

    public override string ToString()
    {
        return base.ToString() + $", Stripes: {StripesPatter}";
    }
}

sealed class Owl : Bird
{
    public bool CanRotateHead { get; set; }

    public Owl(string name, int age, double weight, double wingspan, bool canRotateHead, DateTime birthDate) 
    : base(name, age, weight, wingspan, birthDate)
    {
        CanRotateHead = canRotateHead;
    }

    public override string Hunt()
    {
        string rotation = CanRotateHead ? "with head rotation" : "silently";
        return $"{Name} hunts at night {rotation}";
    }

    public override string ToString()
    {
        return base.ToString() + $", Can rotate head: {CanRotateHead}";
    }
}

sealed class Parrot : Bird
{
    public bool CanTalk { get; set; }

    public Parrot(string name, int age, double weight, double wingspan, bool canTalk, DateTime birthDate) 
    : base(name, age, weight, wingspan, birthDate)
    {
        CanTalk = canTalk;
    }

    public override string Hunt()
    {
        string talk = CanTalk ? "while mimicking sounds" : "quietly";
        return $"{Name} hunts for fruits {talk}";
    }

    public override string ToString()
    {
        return base.ToString() + $", Can talk: {CanTalk}";
    }
}

sealed class Shark : Fish
{
    public int TeethCount { get; set; }

    public Shark(string name, int age, double weight, double swimSpeed, int teethCount, DateTime birthDate) 
    : base(name, age, weight, swimSpeed, birthDate)
    {
        TeethCount = teethCount;
    }

    public override string Hunt()
    {
        return $"{Name} hunts with {TeethCount} sharp teeth";
    }

    public override string ToString()
    {
        return base.ToString() + $", Teeth: {TeethCount}";
    }
}

sealed class Crocodile : Animal, IHuntable
{
    public double BiteForce { get; set; }

    public Crocodile(string name, int age, double weight, double biteForce, DateTime birthDate) : base(name, age, weight)
    {
        BiteForce = biteForce;
    }

    public override string Move()
    {
        return $"{Name} swims and crawls on land";
    }

    string IHuntable.Hunt()
    {
        return $"{Name} ambushes prey with {BiteForce} kg bite force";
    }

    public override string Hunt()
    {
        return $", Bite force: {BiteForce} kg";
    }

    public override bool Equals(object obj)
    {
        if (obj is Crocodile other)
        {
            return base.Equals(obj) && BiteForce == other.BiteForce;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 23 + BiteForce.GetHashCode();
    }
}

class Printer
{
    public void IAmPrinting(IMovable someObj)
    {
        if (someObj is Animal animal)
        {
            Console.WriteLine(animal.ToString());
        }
        else
        {
            Console.WriteLine("Unknown object type");
        }
    }
}

namespace lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            Lion lion = new Lion("Simba", 5, 190, 4, "Golden", new DateTime(2018, 5, 15));
            Tiger tiger = new Tiger("Rajah", 4, 220, 3, "Vertical", new DateTime(2019, 3, 10));
            Owl owl = new Owl("Hedwig", 2, 1.5, 120, true, new DateTime(2021, 8, 20));
            Parrot parrot = new Parrot("Rio", 3, 0.4, 80, true, new DateTime(2020, 1, 5));
            Shark shark = new Shark("Jaws", 8, 450, 30, 3000, new DateTime(2015, 12, 1));
            Crocodile crocodile = new Crocodile("Snappy", 10, 150, 2200, new DateTime(2013, 7, 7));

            zoo.AddAnimal(lion);
            zoo.AddAnimal(tiger);
            zoo.AddAnimal(owl);
            zoo.AddAnimal(parrot);
            zoo.AddAnimal(shark);
            zoo.AddAnimal(crocodile);

            // Создаем контроллер
            ZooController controller = new ZooController(zoo);

            // Выполняем запросы
            Console.WriteLine("1. Средний вес млекопитающих: " +
                controller.GetAverageWeightByType(AnimalType.Bird));

            Console.WriteLine("2. Количество хищных птиц: " +
                controller.CountPredatoryBirds());

            Console.WriteLine("3. Животные по году рождения:");
            controller.DisplayAnimalsSortedByBirthYear();

            Console.WriteLine("\n4. Полная информация о зоопарке:");
            zoo.PrintAllAnimals();
        }
    }
}
