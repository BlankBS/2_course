using System;

interface IMovable
{
    string Move();
}

interface IHuntable
{
    string Hunt();
}

[Serializable]
public abstract class Animal : IMovable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public double Weight { get; set; }

    protected Animal() { }

    protected Animal(string name, int age, double weight)
    {
        Name = name;
        Age = age;
        Weight = weight;
    }

    public abstract string Move();

    public virtual string Hunt()
    {
        return $"{Name} is looking for food";
    }

    public override bool Equals(object obj)
    {
        if (obj is Animal other)
        {
            return Name == other.Name && Age == other.Age && Weight == other.Weight;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
        hash = hash * 23 + Age.GetHashCode();
        hash = hash * 23 + Weight.GetHashCode();
        return hash;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {Name}, Age: {Age}, Weight: {Weight}";
    }
}

public abstract class Mammal : Animal, IHuntable
{
    public int GestationPeriod { get; set; }

    protected Mammal() { }

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

public abstract class Bird : Animal, IHuntable
{
    public double Wingspan { get; set; }

    protected Bird() { }

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

public abstract class Fish : Animal, IHuntable
{
    public double SwimSpeed { get; set; }

    protected Fish() { }

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

public sealed class Lion : Mammal
{
    public string ManeColor { get; set; }

    protected Lion() { }

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

public sealed class Tiger : Mammal
{
    public string StripesPatter { get; set; }

    protected Tiger() { }

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

public sealed class Owl : Bird
{
    public bool CanRotateHead { get; set; }

    protected Owl () { }

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

public sealed class Parrot : Bird
{
    public bool CanTalk { get; set; }

    protected Parrot() { }

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

public sealed class Shark : Fish
{
    public int TeethCount { get; set; }

    protected Shark() { }

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

public sealed class Crocodile : Animal, IHuntable
{
    public double BiteForce { get; set; }

    protected Crocodile() { }

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