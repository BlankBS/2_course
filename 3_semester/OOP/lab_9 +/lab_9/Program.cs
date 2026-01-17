using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Security.Policy;

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

public class Task2
{
    public static void Run()
    {
        Console.WriteLine("\n===== Задание 2 =====");

        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.WriteLine("Исходная коллекция List<int>: ");
        Console.WriteLine(string.Join(", ", numbers));

        short CountToRemove = 3;
        numbers.RemoveRange(2, CountToRemove);
        Console.WriteLine("\nПосле удаления " + CountToRemove + " элементов: ");
        Console.WriteLine(string.Join(", ", numbers));

        numbers.Add(99);
        numbers.Insert(0, 100);
        numbers.AddRange(new int[] { 200, 300 });
        Console.WriteLine("\nПосле добавления:");
        Console.WriteLine(string.Join(", ", numbers));

        Dictionary<int, int> dictionary = new Dictionary<int, int>();
        int keyCounter = 1;
        foreach (var num in numbers)
        {
            dictionary[keyCounter++] = num;
        }

        Console.WriteLine("\nВторая коллекция Dictionary<int, int>:");
        foreach (var kvp in dictionary)
        {
            Console.WriteLine("[" + kvp.Key + "] = " + kvp.Value);

        }
        int searchValue = 99;
        bool found = false;
        foreach(var kvp in dictionary)
        {
            if(kvp.Value == searchValue)
            {
                Console.WriteLine("\nЗначение " + searchValue + " найдено по ключу " + kvp.Key);
                found = true;
                break;
            }
        }
        if(!found)
        {
            Console.WriteLine("\nЗначение " + searchValue + " не найдено");
        }
    }
}

public class Task3
{
    public static void Run()
    {
        Console.WriteLine("\n===== Задание 3 =====");

        ObservableCollection<Concert> concerts = new ObservableCollection<Concert>();
        ConcurrentBag<Concert> _concerts = new ConcurrentBag<Concert>()

        concerts.CollectionChanged += Concerts_CollectionChanged;

        concerts.Add(new Concert("Ван Гог", "Insight", new DateTime(2025, 11, 12), 13));
        concerts.Add(new Concert("Stand Up Comedy Hall", "Астро разборы", new DateTime(2025, 11, 12), 35));
        concerts.RemoveAt(0);
    }

    private static void Concerts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Concert c in e.NewItems)
            {
                Console.WriteLine("> Добавлен концерт: " + c);
            }
        }
        if (e.OldItems != null)
        {
            foreach (Concert c in e.OldItems)
            {
                Console.WriteLine("< Удалён концерт: " + c);
            }
        }
    }
}

namespace lab_9
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("===== 1 Задание =====");

            ConcertManager manager = new ConcertManager();
            manager.AddConcert(1, new Concert("Prime Hall", "Чиж&Со", new DateTime(2025, 11, 11), 27));
            manager.AddConcert(2, new Concert("Prime Hall", "Белорусская государственная филармония", new DateTime(2025, 12, 12), 34));

            manager.PrintConcerts();

            Concert found = manager.FindConcert(1);
            Console.WriteLine("\nНайден концерт по ID 1:");
            if(found != null)
            {
                found.ShowInfo();
            }

            manager.RemoveConcert(2);
            manager.PrintConcerts();

            Task2.Run();
            Task3.Run();
        }
    }
}
