using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

public class StackDouble
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
        set => name = value;
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

            if (elements != null && top >= 0)
            {
                for (int i = 0; i <= Math.Min(top, 2); i++)
                {
                    hash = hash * 23 + elements[i].GetHashCode();
                }
            }

            return hash;
        }
    }

    public override string ToString()
    {
        return $"StackDouble [ID: {id}, Name: {name}, Capacity: {capacity}, Count: {Count}]";
    }
}
public interface IEventInfo
{
    void ShowInfo();
}

public class Concert : IEventInfo
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public DateTime Date { get; set; }
    public double TicketPrice { get; set; }

    public Concert(string title, string artist, DateTime date, double ticketPrice)
    {
        Title = title;
        Artist = artist;
        Date = date;
        TicketPrice = ticketPrice;
    }

    public void ShowInfo()
    {
        Console.WriteLine(Title + " — " + Artist + ", " + Date.ToShortDateString() + ", цена: " + TicketPrice + "$");
    }

    public override string ToString()
    {
        return Title + " (" + Artist + ") " + Date.ToShortDateString();
    }
}

public class ConcertManager
{
    private Dictionary<int, Concert> concerts = new Dictionary<int, Concert>();

    public void AddConcert(int concertId, Concert concert)
    {
        concerts[concertId] = concert;
    }

    public void RemoveConcert(int concertId)
    {
        if (concerts.ContainsKey(concertId))
        {
            concerts.Remove(concertId);
        }
    }

    public Concert FindConcert(int concertId)
    {
        if (concerts.ContainsKey(concertId))
        {
            return concerts[concertId];
        }
        return null;
    }

    public void PrintConcerts()
    {
        Console.WriteLine("\nСписок концертов:");
        foreach (var concert in concerts)
        {
            Console.WriteLine("[" + concert.Key + "] " + concert.Value);
        }
    }

    public Dictionary<int, Concert> GetConcerts()
    {
        return concerts;
    }
}