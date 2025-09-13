using System;
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
        Console.WriteLine("\n123");
    }

    public StackDouble() : this("Unnamed Stack")
    {
        elements = new double[DEFAULT_CAPACITY];
        capacity = DEFAULT_CAPACITY;
        top = -1;
        Console.WriteLine("\n321");
    }

    public StackDouble(string name, int capacity) : this(name)
    {
        this.capacity = capacity;
        elements = new double[capacity];
        top = -1;
        Console.WriteLine("\n456");
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
        Console.WriteLine("\n777");
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

            if(elements != null && top >= 0)
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


            Console.WriteLine($"\nВерхний элемент stack1: {stack1.Peek()}");
            Console.WriteLine($"Размер stack1: {stack1.Count}");

            int opsCount = 0;
            if (stack1.TryPop(out double result, ref opsCount))
            {
                Console.WriteLine($"Извлечен элемент: {result}, операций: {opsCount}");
            }

            var stack4 = stack3.Clone();
            Console.WriteLine($"\nstack3 equal stack4: {stack3.Equals(stack4)}");

            Console.WriteLine($"Тип stack1: {stack1.GetType()}");

            StackDouble.PrintClassInfo();

            Console.WriteLine("\n=== РАБОТА С МАССИВОМ ОБЪЕКТОВ ===");

            StackDouble[] stacks =
            {
                new StackDouble("Стек A", 5.1, -3.2, 7.4),
                new StackDouble("Стек B", 1.0, 2.0, 3.0),
                new StackDouble("Стек C", -1.5, 4.8),
                new StackDouble("Стек D", 9.9, 8.7, 7.6),
                new StackDouble("Стек E", -0.5, 2.2, -3.3)
            };

            var minTopStack = stacks.OrderBy(s => s.IsEmpty ? double.MaxValue : s.Peek()).First();
            var maxTopStack = stacks.OrderByDescending(s => s.IsEmpty ? double.MinValue : s.Peek()).First();

            Console.WriteLine($"Стек с наименьшим верхним элементом: {minTopStack.Name}, элемент: {minTopStack.Peek()}");
            Console.WriteLine($"Стек с наибольшим верхним элементом: {maxTopStack.Name}, элемент: {maxTopStack.Peek()}");

            var stacksWithNegatives = stacks.Where(s => s.ContainsNegative()).ToArray();

            Console.WriteLine("\nСтеки, содержащие отрицательные элементы: ");
            foreach(var stack in stacksWithNegatives)
            {
                Console.WriteLine($"- {stack.Name}");
            }

            Console.WriteLine("\n=== АНОНИМНЫЙ ТИП ===");

            var anonymousStack = new
            {
                Name = "Анонимный стек",
                Elements = new double[] { 1.1, 2.2, 3.3 },
                CreatedAt = DateTime.Now
            };

            Console.WriteLine($"Анонимный тип: {anonymousStack.Name}");
            Console.WriteLine($"Элементы: {string.Join("; ", anonymousStack.Elements)}");
            Console.WriteLine($"Создан: {anonymousStack.CreatedAt}");

            Console.WriteLine($"\nТип анонимного объекта: {anonymousStack.GetType()}");
        }
    }
}
