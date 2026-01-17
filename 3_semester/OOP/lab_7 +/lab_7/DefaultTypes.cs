using System;

public class IntAnimal : Animal
{
    public int Value
    {
        get; set;
    }

    public IntAnimal(string name, int value) : base(name, 0, value)
    {
        Value = value;
    }

    public override string Move()
    {
        return $"Integer {Name} doesn't move, it has value {Value}";
    }
    public override string Hunt()
    {
        return $"Integer {Name} is hunting for mathematical operations";
    }

    public override string ToString()
    {
        return $"IntAnimal: {Name}, Value: {Value}";
    }
}

public class DoubleAnimal : Animal
{
    public double Value { get; set; }

    public DoubleAnimal(string name, double value) : base(name, 0, value)
    {
        Value = value;
    }

    public override string Move()
    {
        return $"Double {Name} moves precisely with value {Value:F2}";
    }

    public override string Hunt()
    {
        return $"Double {Name} is hunting for floating point operations";
    }

    public override string ToString()
    {
        return $"DoubleAnimal: {Name}, Value: {Value:F2}";
    }
}

public class StringAnimal : Animal
{
    public string TextValue { get; set; }

    public StringAnimal(string name, string textValue) : base(name, 0, textValue.Length)
    {
        TextValue = textValue;
    }

    public override string Move()
    {
        return $"String '{Name}' moves through characters: '{TextValue}'";
    }

    public override string Hunt()
    {
        return $"String '{Name}' is hunting for concatenation operations";
    }

    public override string ToString()
    {
        return $"StringAnimal: {Name}, Text: '{TextValue}'";
    }
}