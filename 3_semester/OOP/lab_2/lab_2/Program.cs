using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class StackDouble
{
    private const int DEFAULT_CAPACITY = 10;
    private readonly int id;
    private double[] elements;
    private int top;
    private static int objectCount = 0;

    private string name;
    private int capacity;

    static StackDouble()
    {
        Console.WriteLine("Статический конструктор StackDouble вызван");
    }

    private StackDouble(string name)
    {
        this.name = name;
        this.id = GetHashCode();
        objectCount++;
    }

    public StackDouble() : this("Unnamed Stack")
    {
        elements = new double[DEFAULT_CAPACITY];
        capacity = DEFAULT_CAPACITY;
        top = -1;
    }

    public StackDouble(string name, int capacity) : this(name)
    {
        this.capacity = capacity;
        elements = new double[capacity];
        top = -1;
    }

    public StackDouble(string name = "Default Stack", params double[] initialElements) : this(name)
    {
        capacity = Math.Max(initialElements.Length * 2, DEFAULT_CAPACITY);
        elements = new double[capacity];
        top = -1;

        foreach (var element in initialElements)
        {
            Push(element);
        }
    }

    public int ID => id;
    public string Name
    {
        get => name;
        private set => name = value;
    }

    public int Capacity
    {
        get => capacity;
        set
        {
            if (value > capacity)
            {
                Array.Resize(ref elements, value);
                capacity = value;
            }
        }
    }

    public int Count => top + 1;
    public bool IsEmpty => top == -1;
    public bool IsFull => top == capacity - 1;

    public static int ObjectCount => objectCount;

    public void Push(double element)
    {
        if (IsFull)
        {
            Capacity *= 2;
        }

        elements[++top] = element;
    }

    public double Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Стек пуст");

        return elements[top--];
    }

    public double Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Стек пуст");

        return elements[top];
    }

    public void Clear()
    {
        top = -1;
        Array.Clear(elements, 0, elements.Length);
    }

    public bool TryPop(out double result, ref int operationsCount)
    {
        operationsCount++;
        if (IsEmpty)
        {
            result = default;
            return false;
        }

        result = Pop();
        return true;
    }

    public double this[int index]
    {
        get
        {
            if (index < 0 || index > top)
                throw new IndexOutOfRangeException();

            return elements[top - index];
        }
    }

    public static void PrintClassInfo()
    {
        Console.WriteLine($"Класс StackDouble. Создано объектов: {objectCount}");
    }

    public override bool Equals(object obj)
    {
        if (obj is StackDouble other)
        {
            if (Count != other.Count) return false;

            for (int i = 0; i <= top; i++)
            {
                if (elements[i] != other.elements[i])
                    return false;
            }

            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + capacity.GetHashCode();
            hash = hash * 23 + top.GetHashCode();

            for (int i = 0; i <= Math.Min(top, 2); i++)
            {
                hash = hash * 23 + elements[i].GetHashCode();
            }

            return hash;
        }
    }

    public override string ToString()
    {
        return $"StackDouble [ID: {id}, Name: {name}, Capacity: {capacity}, Count: {Count}]";
    }
}

public partial class StackDouble
{
    public bool ContainsNegative()
    {
        for (int i = 0; i <= top; i++)
        {
            if (elements[i] < 0)
                return true;
        }
        return false;
    }

    public StackDouble Clone()
    {
        var clone = new StackDouble(name + " - Clone", capacity);
        Array.Copy(elements, clone.elements, top + 1);
        clone.top = top;
        return clone;
    }
}

namespace lab_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== СОЗДАНИЕ ОБЪЕКТОВ ===");

            var stack1 = new StackDouble();
            var stack2 = new StackDouble("Мой стек", 5);
            var stack3 = new StackDouble("С числами", 1.5, -2.7, 3.14, 0);

            Console.WriteLine(stack1);
            Console.WriteLine(stack2);
            Console.WriteLine(stack3);

            stack1.Push(10.5);
            stack2.Push(20.3);
            stack1.Name = "Измененное имя";

        }
    }
}
