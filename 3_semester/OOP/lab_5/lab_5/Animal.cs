public abstract partial class Animal : IMovable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public double Weight { get; set; }

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