using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

public partial class StackDouble
{
    public double Sum
    {
        get
        {
            double sum = 0;
            for (int i = 0; i <= top; i++)
            {
                sum += elements[i];
            }
            return sum;
        }
    }

    public bool HasElementAtIndex(int index, double value)
    {
        if (index < 0 || index > top) return false;
        return elements[top - index] == value;
    }

    public bool ContaintsNegativeElement()
    {
        for (int i = 0; i <= top; i++)
        {
            if (elements[i] < 0)
            {
                return true;
            }
        }
        return false;
    }

    public double GetStackSum()
    {
        double sum = 0;
        for (int i = 0; i <= top; i++)
        {
            sum += elements[i];
        }
        return sum;
    }
}

namespace lab_10
{
    internal class Program
    {
        static void ProcessMonths()
        {
            string[] months = {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            int n = 5;

            // query
            var monthsLengthN = from month in months
                                where month.Length == n
                                select month;
            Console.WriteLine($"Месяцы с длиной {n}: {string.Join(", ", monthsLengthN)}");

            var summerWinterMonths = from month in months
                                     where month == "June" || month == "July" || month == "August" ||
                                           month == "December" || month == "Janary" || month == "February"
                                     select month;
            Console.WriteLine($"Летние и зимние месяцы: {string.Join(", ", summerWinterMonths)}");

            // method
            var sortedMonths = months.OrderBy(month => month);
            Console.WriteLine($"Месяцы в алфавитном порядке: {string.Join(", ", sortedMonths)}");

            var monthsWithUAndLengthMore4 = months.Where(month => month.Contains('u') && month.Length >= 4);
            Console.WriteLine($"Месяцы с 'u' длиной >= 4: {string.Join(", ", monthsWithUAndLengthMore4)}");
            Console.WriteLine($"Количество: {monthsWithUAndLengthMore4.Count()}");
        }

        static void ProcessStackCollection()
        {
            List<StackDouble> stacks = new List<StackDouble>
            {
                new StackDouble("Стек 1", 1.5, -3.2, 7.8),
                new StackDouble("Стек 2", 4.1, 5.6),
                new StackDouble("Стек 3", -2.1, -4.3, 6.7, -8.9),
                new StackDouble("Стек 4", 9.9),
                new StackDouble("Стек 5", 1.0, 2.0, 3.0, 4.0, 5.0),
                new StackDouble("Стек 6", -1.5),
                new StackDouble("Стек 7", 0, 0, 0),
                new StackDouble("Стек 8", 2.2, -3.3, 4.4),
                new StackDouble("Стек 9", -7.7, 8.8),
                new StackDouble("Стек 10", 1.1)
            };

            foreach (var stack in stacks)
            {
                Console.WriteLine($"- {stack}");
            }

            var minTopStack = stacks.Where(stack => !stack.IsEmpty)
                                    .OrderBy(s => s.Peek())
                                    .FirstOrDefault();
            Console.WriteLine($"\n1. Стек с наименьшим верхним элементом: {minTopStack?.Name}");

            var maxTopStack = stacks.Where(stack => !stack.IsEmpty)
                                    .OrderByDescending(s => s.Peek())
                                    .FirstOrDefault();
            Console.WriteLine($"\n2. Стек с наибольшим верхним элементом: {maxTopStack?.Name}");

            var stacksWithNegatives = stacks.Where(stack => stack.ContaintsNegativeElement());
            Console.WriteLine($"\n3. Стеки с отрицательными элементами ({stacksWithNegatives.Count()} шт.):");
            foreach (var stack in stacksWithNegatives)
            {
                Console.WriteLine($"   - {stack.Name}");
            }

            var minStack = stacks.OrderBy(stack => stack.Count).First();
            Console.WriteLine($"\n4. Минимальный стек (по количеству элементов): {minStack.Name}, элементов: {minStack.Count}");

            var stacksLength1And3 = stacks.Where(s => s.Count == 1 || s.Count == 3).ToArray();
            Console.WriteLine($"\n5. Стеки длины 1 и 3 ({stacksLength1And3.Length} шт.):");
            foreach (var stack in stacksLength1And3)
            {
                Console.WriteLine($"   - {stack.Name}: элементов = {stack.Count}");
            }

            var firstStackWithZero = stacks.FirstOrDefault(s => !s.IsEmpty && s.Peek() == 0);
            Console.WriteLine($"\n6. Первый стек с нулевым верхним элементом: {firstStackWithZero?.Name}");

            var orderedBySum = from stack in stacks
                               orderby stack.GetStackSum()
                               select stack;

            Console.WriteLine("\n7. Стеки, упорядоченные по сумме элементов:");
            foreach (var stack in orderedBySum)
            {
                Console.WriteLine($"   - {stack.Name}: сумма = {stack.GetStackSum():F2}");
            }

            var complexLINQ = from stack in stacks
                              where stack.Count > 1
                              orderby stack.Count descending
                              group stack by stack.ContaintsNegativeElement() into g
                              where g.Count() >= 2
                              orderby g.Key
                              select new
                              {
                                  Category = g.Key ? "С отрицательными" : "Без отрицательных",
                                  Stacks = from s in g
                                           orderby s.GetStackSum() descending
                                           select s,
                                  StackCount = g.Count(),
                                  AvgCount = g.Average(s => s.Count),
                                  TotalSum = g.Sum(s => s.GetStackSum())
                              };

            foreach (var group in complexLINQ)
            {
                Console.WriteLine($"\n{group.Category}:");
                Console.WriteLine($"  Всего стеков: {group.StackCount}, средняя длина: {group.AvgCount:F1}, общая сумма: {group.TotalSum:F2}");
                foreach (var stack in group.Stacks)
                {
                    Console.WriteLine($"  - {stack.Name}: элементов={stack.Count}, сумма={stack.GetStackSum():F2}");
                }
            }

            var stackCategories = new[]
            {
                new { Name = "Стек 1", Category = "Маленькие" },
                new { Name = "Стек 2", Category = "Средние" },
                new { Name = "Стек 3", Category = "Большие" },
                new { Name = "Стек 4", Category = "Маленькие" },
                new { Name = "Стек 5", Category = "Большие" }
            };

            var joinQuery = from stack in stacks
                            join category in stackCategories
                            on stack.Name equals category.Name
                            select new
                            {
                                StackName = stack.Name,
                                Category = category.Category,
                                ElementCount = stack.Count,
                                Sum = stack.GetStackSum()
                            };

            foreach (var item in joinQuery)
            {
                Console.WriteLine($"  - {item.StackName} ({item.Category}): элементов={item.ElementCount}, сумма={item.Sum:F2}");
            }
        }

        static void Main(string[] args)
        {
            ProcessMonths();
            Console.WriteLine('\n');
            ProcessStackCollection();
        }
    }
}
