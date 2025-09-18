using System;
using System.Collections.Generic;
using System.Linq;

public class Production
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }

    public Production(int id, string organizationName)
    {
        Id = id;
        OrganizationName = organizationName;
    }

    public override string ToString()
    {
        return $"Production [ID: {Id}, Organization: {OrganizationName}]";
    }
}

public class Queue<T> where T : IComparable<T>
{
    

    public class Developer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }

        public Developer(int id, string fullName, string department)
        {
            Id = id;
            FullName = fullName;
            Department = department;
        }

        public override string ToString()
        {
            return $"Developer [ID: {Id}, Name: {FullName}, Department: {Department}]";
        }
    }

    private List<T> items;

    public Production ProductionInfo { get; set; }
    public Developer DeveloperInfo { get; set; }

    public Queue()
    {
        items = new List<T>();

        ProductionInfo = new Production(1, "Queue Production Inc.");
        DeveloperInfo = new Developer(101, "John Smith", "Software Development");
    }

    public void Enqueue(T item)
    {
        items.Add(item);
    }

    public T Dequeue()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T item = items[0];
        items.RemoveAt(0);
        return item;
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public int Count
    {
        get { return items.Count; }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= items.Count)
                throw new IndexOutOfRangeException();
            return items[index];
        }
        set
        {
            if (index < 0 || index >= items.Count)
                throw new IndexOutOfRangeException();
            items[index] = value;
        }
    }

    public static Queue<T> operator +(Queue<T> queue, T item)
    {
        queue.Enqueue(item);
        return queue;
    }

    public static Queue<T> operator --(Queue<T> queue)
    {
        queue.Dequeue();
        return queue;
    }

    public static bool operator true(Queue<T> queue)
    {
        return queue.IsEmpty();
    }

    public static bool operator false(Queue<T> queue)
    {
        return !queue.IsEmpty();
    }

    public static Queue<T> operator <(Queue<T> queue1, Queue<T> queue2)
    {
        Queue<T> result = new Queue<T>();
        foreach (var item in queue1.items.OrderByDescending(x => x))
        {
            result.Enqueue(item);
        }
        return result;
    }

    public static Queue<T> operator >(Queue<T> queue1, Queue<T> queue2)
    {
        Queue<T> result = new Queue<T>();
        foreach (var item in queue1.items.OrderBy(x => x))
        {
            result.Enqueue(item);
        }
        return result;
    }

    public Queue<T> CopySortedDescending()
    {
        Queue<T> result = new Queue<T>();
        foreach (var item in items.OrderByDescending(x => x))
        {
            result.Enqueue(item);
        }
        return result;
    }

    public static implicit operator int(Queue<T> queue)
    {
        return queue.Count;
    }

    public override string ToString()
    {
        return $"Queue with {Count} elements: [{string.Join(", ", items)}]";
    }
}

public static class StatisticOperation
{
    public static T Sum<T>(Queue<T> queue) where T : IComparable<T>
    {
        if (queue.Count == 0)
            return default(T);

        try
        {
            dynamic sum = 0;
            for (int i = 0; i < queue.Count; i++)
            {
                sum += (dynamic)queue[i];
            }
            return (T)sum;
        }
        catch
        {
            throw new InvalidOperationException("Sum operation is only supported for numeric types");
        }
    }

    public static T Difference<T>(Queue<T> queue) where T : IComparable<T>
    {
        if (queue.Count == 0)
            return default(T);

        try
        {
            T min = queue[0];
            T max = queue[0];

            for (int i = 1; i < queue.Count; i++)
            {
                if (queue[i].CompareTo(min) < 0)
                    min = queue[i];
                if (queue[i].CompareTo(max) > 0)
                    max = queue[i];
            }

            return (dynamic)max - (dynamic)min;
        }
        catch
        {
            throw new InvalidOperationException("Difference operation is only supported for numeric types");
        }
    }

    public static int CountElements<T>(Queue<T> queue) where T : IComparable<T>
    {
        return queue.Count;
    }

    public static int IndexOfFirstDot(this string str)
    {
        return str.IndexOf('.');
    }
    public static T LastElement<T>(this Queue<T> queue) where T : IComparable<T>
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return queue[queue.Count - 1];
    }
}

//public static class ExtensionMethods
//{
//    public static int IndexOfFirstDot(this string str)
//    {
//        return str.IndexOf('.');
//    }

//    public static T LastElement<T>(this Queue<T> queue) where T : IComparable<T>
//    {
//        if (queue.Count == 0)
//            throw new InvalidOperationException("Queue is empty");

//        return queue[queue.Count - 1];
//    }
//}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ КЛАССА QUEUE ===");

        Queue<int> intQueue = new Queue<int>();

        Console.WriteLine("\n1. Тестирование перегруженных операторов:");

        intQueue = intQueue + 10;
        intQueue = intQueue + 30;
        intQueue = intQueue + 20;
        intQueue = intQueue + 40;

        Console.WriteLine($"После добавления элементов: {intQueue}");
        Console.WriteLine($"Мощность очереди (неявное преобразование в int): {(int)intQueue}");

        if (intQueue)
            Console.WriteLine("Очередь пуста");
        else
            Console.WriteLine("Очередь не пуста");

        intQueue = --intQueue;
        Console.WriteLine($"После извлечения элемента: {intQueue}");

        Queue<int> sortedQueue = intQueue.CopySortedDescending();
        Console.WriteLine($"Очередь после сортировки по убыванию: {sortedQueue}");

        Queue<int> tempQueue = new Queue<int>();
        Queue<int> sortedViaOperator = intQueue < tempQueue;
        Console.WriteLine($"Сортировка через оператор: {sortedViaOperator}");

        Console.WriteLine("\n2. Тестирование вложенных объектов:");
        Console.WriteLine($"Production: {intQueue.ProductionInfo}");
        Console.WriteLine($"Developer: {intQueue.DeveloperInfo}");

        Console.WriteLine("\n3. Тестирование статических методов:");
        Console.WriteLine($"Сумма элементов: {StatisticOperation.Sum(intQueue)}");
        Console.WriteLine($"Разница между max и min: {StatisticOperation.Difference(intQueue)}");
        Console.WriteLine($"Количество элементов: {StatisticOperation.CountElements(intQueue)}");

        Console.WriteLine("\n4. Тестирование методов расширения:");
        string testString = "Hello.World.Test";
        Console.WriteLine($"Индекс первой точки в '{testString}': {testString.IndexOfFirstDot()}");
        Console.WriteLine($"Последний элемент очереди: {intQueue.LastElement()}");

        Console.WriteLine("\n5. Тестирование с другими типами данных:");

        Queue<string> stringQueue = new Queue<string>();
        stringQueue = stringQueue + "apple";
        stringQueue = stringQueue + "cherry";
        stringQueue = stringQueue + "banana";

        Console.WriteLine($"Строковая очередь: {stringQueue}");
        Console.WriteLine($"Последний элемент: {stringQueue.LastElement()}");
        Console.WriteLine($"Количество элементов в строковой очереди: {StatisticOperation.CountElements(stringQueue)}");

        Queue<string> sortedStringQueue = stringQueue.CopySortedDescending();
        Console.WriteLine($"Отсортированная строковая очередь: {sortedStringQueue}");

        Console.WriteLine("\n6. Тестирование исключений:");
        try
        {
            var sum = StatisticOperation.Sum(stringQueue);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ожидаемая ошибка при суммировании строк: {ex.Message}");
        }

        try
        {
            var diff = StatisticOperation.Difference(stringQueue);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ожидаемая ошибка при вычислении разницы строк: {ex.Message}");
        }

        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
        Console.ReadKey();
    }
}