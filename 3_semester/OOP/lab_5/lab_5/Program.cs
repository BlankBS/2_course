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

    protected Mammal(string name, int age, double weight, int gestationPeriod) : base(name, age, weight)
    {
        GestationPeriod = gestationPeriod;
    }

    public override string Move()
    {
        return $"{Name} runs on ground";
    }

    public virtual string Hunt()
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

    protected Bird(string name, int age, double weight, double wingspan) : base(name, age, weight)
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

    protected Fish(string name, int age, double weight, double swimSpeed) : base(name, age, weight)
    {
        SwimSpeed = swimSpeed;
    }

    public override string Move()
    {
        return $"{Name} swims in water";
    }

    public virtual string Hunt()
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

    public Lion(string name, int age, double weight, int gestationPeriod, string maneColor) : base(name, age, weight, gestationPeriod)
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

    public Tiger(string name, int age, double weight, int gestationPeriod, string stripesPatter) : base(name, age, weight, gestationPeriod)
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

    public Owl(string name, int age, double weight, double wingspan, bool canRotateHead) : base(name, age, weight, wingspan)
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

    public Parrot(string name, int age, double weight, double wingspan, bool canTalk) : base(name, age, weight, wingspan)
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

    public Shark(string name, int age, double weight, double swimSpeed, int teethCount) : base(name, age, weight, swimSpeed)
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

    public Crocodile(string name, int age, double weight, double biteForce) : base(name, age, weight)
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
            Lion lion = new Lion("Simba", 5, 190, 4, "Golden");
            Tiger tiger = new Tiger("Rajah", 4, 220, 3, "Vertical");
            Owl owl = new Owl("Hedwig", 2, 1.5, 120, true);
            Parrot parrot = new Parrot("Rio", 3, 0.4, 80, true);
            Shark shark = new Shark("Jaws", 8, 450, 30, 3000);
            Crocodile crocodile = new Crocodile("Snappy", 10, 150, 2200);

            Animal[] animals = { lion, tiger, owl, parrot, shark, crocodile };
            IMovable[] movables = { lion, tiger, owl, parrot, shark, crocodile };
            IHuntable[] huntables = { lion, tiger, owl, parrot, shark, crocodile };

            Console.WriteLine("=== Movement ===");
            foreach (var movable in movables)
            {
                Console.WriteLine(movable.Move());
            }

            Console.WriteLine("\n=== Hunting (Animal method) ===");
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.Hunt());
            }

            Console.WriteLine("\n=== Hunting (IHuntable method) ===");
            foreach (var huntable in huntables)
            {
                if (huntable is Crocodile croc)
                {
                    Console.WriteLine(((IHuntable)croc).Hunt());
                }
                else
                {
                    Console.WriteLine(huntable.Hunt());
                }
            }

            Console.WriteLine("\n=== Type checking with is/as ===");
            foreach (var obj in animals)
            {
                if (obj is Mammal mammal)
                {
                    Console.WriteLine($"{obj.Name} is a mammal with {mammal.GestationPeriod} months gestation");
                }
                else if (obj is Bird bird)
                {
                    Console.WriteLine($"{obj.Name} is a bird with {bird.Wingspan}cm wingspan");
                }
                else if (obj is Fish fish)
                {
                    Console.WriteLine($"{obj.Name} is a fish swimming at {fish.SwimSpeed} km/h");
                }
                else
                {
                    Console.WriteLine($"{obj.Name} is another type of animal");
                }
            }

            Console.WriteLine("\n=== Printer demonstration ===");
            Printer printer = new Printer();
            foreach (var movable in movables)
            {
                printer.IAmPrinting(movable);
            }

            Console.WriteLine("\n=== Equality check ===");
            Crocodile croc1 = new Crocodile("Snappy", 10, 150, 2200);
            Crocodile croc2 = new Crocodile("Snappy", 10, 150, 2200);
            Console.WriteLine($"croc1.Equals(croc2): {croc1.Equals(croc2)}");
            Console.WriteLine($"Hash codes: {croc1.GetHashCode()} vs {croc2.GetHashCode()}");

            Console.WriteLine("\n=== All animals ToString() ===");
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }
}
