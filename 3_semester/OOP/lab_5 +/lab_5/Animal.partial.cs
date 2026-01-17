using System;
public abstract partial class Animal
{
    private AnimalInfo _info;

    public AnimalType Type { get; set; }

    public AnimalInfo Info
    {
        get => _info;
        set => _info = value;
    }

    public int BirthYear => Info.BirthDate.Year;

    protected Animal(string name, int age, double weight, AnimalType type, DateTime birthDate)
    {
        Name = name;
        Age = age;
        Weight = weight;
        Type = type;
        _info = new AnimalInfo(birthDate, "Unknown", false);
    }
}