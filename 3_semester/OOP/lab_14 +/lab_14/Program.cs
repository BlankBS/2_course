using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Timers;

class PrimeNumberFinder
{
    private readonly int _limit;
    private volatile bool _shouldStop;
    private int _primeCount;

    public int PrimeCount => _primeCount;

    public PrimeNumberFinder(int limit)
    {
        _limit = limit;
        _shouldStop = false;
        _primeCount = 0;
    }

    public void FindPrimes()
    {
        string filePath = "prime_numbers.txt";
        File.WriteAllText(filePath, $"Простые числа до {_limit} - {DateTime.Now}");

        for (int i = 2; i <= _limit; i++)
        {
            if (_shouldStop)
            {
                break;
            }

            if (IsPrime(i))
            {
                Interlocked.Increment(ref _primeCount);
                string message = $"Найдено простое число: {i}";

                Console.WriteLine($"[{Thread.CurrentThread.Name}] {message}");

                lock (typeof(PrimeNumberFinder))
                {
                    File.AppendAllText(filePath, $"{i}\n");

                    Thread.Sleep(100);
                }
            }

        }
        Console.WriteLine($"[{Thread.CurrentThread.Name}] Поиск завершен");
    }

    private bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (_shouldStop) return false;
            if (number % i == 0) return false;
        }

        return true;
    }

    public void Stop()
    {
        _shouldStop = true;
    }
}

namespace lab_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите задание:");
                Console.WriteLine("1. Информация о процессах");
                Console.WriteLine("2. Работа с доменами приложений");
                Console.WriteLine("3. Поиск простых чисел в отдельном потоке");
                Console.WriteLine("4. Четные и нечетные числа в двух потоках");
                Console.WriteLine("5. Задача на основе класса Timer");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Task1_ProcessInfo();
                        break;
                    case "2":
                        Task2_AppDomains();
                        break;
                    case "3":
                        Task3_PrimeNumbers();
                        break;
                    case "4":
                        Task4_EvenOddNumbers();
                        break;
                    case "5":
                        Task5_TimerTask();
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void Task1_ProcessInfo()
        {
            try
            {
                Process[] processes = Process.GetProcesses();

                Console.WriteLine($"Всего процессов: {processes.Length}");

                string filePath = "processes_info.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Информация о процессах = {DateTime.Now}");

                    foreach (var process in processes)
                    {
                        try
                        {
                            string info = $"ID: {process.Id,-8} | " +
                                         $"Имя: {process.ProcessName,-25} | " +
                                         $"Приоритет: {GetPriorityString(process),-15} | " +
                                         $"Запущен: {process.StartTime:HH:mm:ss} | " +
                                         $"Состояние: {GetStateString(process),-10} | " +
                                         $"CPU время: {process.TotalProcessorTime:hh\\:mm\\:ss} | " +
                                         $"Память: {process.WorkingSet64 / 1024 / 1024} MB";

                            Console.WriteLine(info);
                            writer.WriteLine(info);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при получении информации о процессе {process.ProcessName}: {ex.Message}");
                        }
                    }

                    Console.WriteLine($"Всего процессов: {processes.Length}");
                }
                Console.WriteLine($"\nИнформация сохранена в файл: {Path.GetFullPath(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void Task2_AppDomains()
        {
            try
            {
                AppDomain currentDomain = AppDomain.CurrentDomain;
                Console.WriteLine($" Имя: {currentDomain.FriendlyName}");
                Console.WriteLine($" Базовая директория: {currentDomain.BaseDirectory}");
                Console.WriteLine($" Конфигурационный файл: {currentDomain.SetupInformation.ConfigurationFile}");

                Console.WriteLine("\nЗагруженные сборки в текущем домене: ");
                Assembly[] assemblies = currentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    Console.WriteLine($" - {assembly.GetName().Name} (v{assembly.GetName().Version})");
                }

                AppDomainSetup setup = new AppDomainSetup();
                setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

                AppDomain newDomain = AppDomain.CreateDomain("NewTestDomain", null, setup);
                Console.WriteLine($"Создан новый домен: {newDomain.FriendlyName}");

                try
                {
                    string assemblyPath = typeof(Program).Assembly.Location;
                    newDomain.Load(AssemblyName.GetAssemblyName(assemblyPath));
                    Console.WriteLine("Сборка успешно загружена");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки сборки: {ex.Message}");
                }

                Console.WriteLine("Выгрузка домена...");
                AppDomain.Unload(newDomain);
                Console.WriteLine(" Домен успешно выгружен");

                try
                {
                    var test = newDomain.FriendlyName;
                }
                catch (AppDomainUnloadedException)
                {
                    Console.WriteLine("Проверка: домен действительно выгружен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void Task3_PrimeNumbers()
        {
            Console.Write("Введите n (до какого числа искать простые числа): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 2)
            {
                Console.WriteLine("Некорректный ввод. Используется n = 100");
                n = 100;
            }

            PrimeNumberFinder finder = new PrimeNumberFinder(n);
            Thread primeThread = new Thread(finder.FindPrimes);
            primeThread.Name = "PrimeNUmberThread";
            primeThread.Priority = ThreadPriority.Normal;

            Console.WriteLine("\nИнформация о потоке перед запуском:");
            PrintThreadInfo(primeThread);

            Console.WriteLine("\nЗапуск потока...");
            primeThread.Start();

            Thread.Sleep(500);

            bool running = true;
            while (running && primeThread.IsAlive)
            {
                Console.WriteLine("\nУправление потоком:");
                Console.WriteLine("1. Пауза (Suspend)");
                Console.WriteLine("2. Возобновить (Resume)");
                Console.WriteLine("3. Вывести информацию о потоке");
                Console.WriteLine("4. Завершить выполнение");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (primeThread.ThreadState != System.Threading.ThreadState.Suspended)
                        {
                            primeThread.Suspend();
                            Console.WriteLine("Поток приостановлен");
                        }
                        else
                        {
                            Console.WriteLine("Поток уже приостановлен");
                        }
                        break;
                    case "2":
                        if (primeThread.ThreadState == System.Threading.ThreadState.Suspended)
                        {
                            primeThread.Resume();
                            Console.WriteLine("Поток возобновлен");
                        }
                        else
                        {
                            Console.WriteLine("Поток не был приостановлен");
                        }
                        break;
                    case "3":
                        PrintThreadInfo(primeThread);
                        Console.WriteLine($"Найдено чисел: {finder.PrimeCount}");
                        break;
                    case "4":
                        finder.Stop();
                        running = false;
                        Console.WriteLine("Запрос на остановку потока отправлен");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }

                Thread.Sleep(100);
            }

            primeThread.Join();
            Console.WriteLine("\nПоток завершил работу");
            Console.WriteLine($"Всего найдено простых чисел: {finder.PrimeCount}");
        }

        static void PrintThreadInfo(Thread thread)
        {
            Console.WriteLine($"   ID: {thread.ManagedThreadId}");
            Console.WriteLine($"   Имя: {thread.Name ?? "Без имени"}");
            Console.WriteLine($"   Приоритет: {thread.Priority}");
            Console.WriteLine($"   Состояние: {thread.ThreadState}");
            Console.WriteLine($"   Фоновая: {thread.IsBackground}");
            Console.WriteLine($"   Активна: {thread.IsAlive}");
        }

        static void PrintNumber(int number, string typer, string filePath)
        {
            string message = $"[{Thread.CurrentThread.Name}] {number}";
            Console.WriteLine(message);

            lock(typeof(Program))
            {
                File.AppendAllText(filePath, message + "\n");
            }
        }

        static void Task4_EvenOddNumbers()
        {
            Console.WriteLine("Введите n(до какого числа выводить): ");
            if(!int.TryParse(Console.ReadLine(), out int number) || number < 1)
            {
                Console.WriteLine("Некорректный ввод. Используется n = 25");
                number = 25;
            }

            Console.WriteLine("\nВыберите режим работы:");
            Console.WriteLine("1. Без синхронизации (обычная работа)");
            Console.WriteLine("2. Сначала четные, потом нечетные");
            Console.WriteLine("3. Чередование: четное, нечетное");
            Console.Write("Ваш выбор: ");

            string modeChoice = Console.ReadLine();

            string filePath = "even_odd_numbers.txt";
            File.WriteAllText(filePath, $"Вывод чисел до {number} - {DateTime.Now}");

            switch (modeChoice)
            {
                case "1":
                    RunWithoutSync(number, filePath);
                    break;
                case "2":
                    RunEvenFirst(number, filePath);
                    break;
                case "3":
                    RunAlternating(number, filePath);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Используется режим 1.");
                    RunWithoutSync(number, filePath);
                    break;
            }

            Console.WriteLine($"\nРезультаты сохранены в файл: {Path.GetFullPath(filePath)}");
        }

        static void RunWithoutSync(int n, string filePath)
        {
            Console.WriteLine("\nРежим 1: без синхронизации");

            Thread evenThread = new Thread(() => PrintEvenNumbers(n, filePath, false));
            Thread oddThread = new Thread(() => PrintOddNumbers(n, filePath, false));

            evenThread.Name = "EvenThread";
            oddThread.Name = "OddThread";

            oddThread.Priority = ThreadPriority.BelowNormal;
            Console.WriteLine("Установлен приоритет для потока нечетных чисел: BelowNormal");

            Console.WriteLine("\nИнформация о потоках:");
            Console.WriteLine("Поток четных чисел:");
            PrintThreadInfo(evenThread);
            Console.WriteLine("\nПоток нечетных чисел:");
            PrintThreadInfo(oddThread);

            evenThread.Start();
            oddThread.Start();

            evenThread.Join();
            oddThread.Join();
        }

        static void RunEvenFirst(int n, string filePath)
        {
            Console.WriteLine("\nРежим 2: сначала четные, потом нечетные");

            object lockObject = new object();
            bool evenDone = false;

            Thread evenThread = new Thread(() =>
            {
                lock(lockObject)
                {
                    PrintEvenNumbers(n, filePath, false);
                    evenDone = true;
                    Monitor.Pulse(lockObject);
                }
            });

            Thread oddThread = new Thread(() =>
            {
                lock (lockObject)
                {
                    while (!evenDone)
                    {
                        Monitor.Wait(lockObject);
                    }
                    PrintOddNumbers(n, filePath, true);
                }
            });

            evenThread.Name = "EvenThread-Sync";
            oddThread.Name = "OddThread-Sync";

            evenThread.Start();
            oddThread.Start();

            evenThread.Join();
            oddThread.Join();
        }

        static void RunAlternating(int n, string filePath)
        {
            object lockObject = new object();
            bool isEvenTurn = true;
            int currentNumber = 1;

            Thread evenThread = new Thread(() =>
            {
                while (currentNumber <= n)
                {
                    lock (lockObject)
                    {
                        if (isEvenTurn && currentNumber % 2 == 0)
                        {
                            PrintNumber(currentNumber, "Четное", filePath);
                            currentNumber++;
                            isEvenTurn = false;
                            Monitor.Pulse(lockObject);
                        }
                        else
                        {
                            Monitor.Wait(lockObject);
                        }
                    }
                }
            });

            Thread oddThread = new Thread(() =>
            {
                while (currentNumber <= n)
                {
                    lock (lockObject)
                    {
                        if (!isEvenTurn && currentNumber % 2 != 0)
                        {
                            PrintNumber(currentNumber, "Нечетное", filePath);
                            currentNumber++;
                            isEvenTurn = true;
                            Monitor.Pulse(lockObject);
                        }
                        else
                        {
                            Monitor.Wait(lockObject);
                        }
                    }
                }
            });

            evenThread.Name = "EvenThread-Alt";
            oddThread.Name = "OddThread-Alt";

            evenThread.Start();
            oddThread.Start();

            evenThread.Join();
            oddThread.Join();
        }

        static void PrintEvenNumbers(int n, string filePath, bool syncMode)
        {
            for(int i = 2; i<= n;i+=2)
            {
                PrintNumber(i, "Четное", filePath);
                if (!syncMode) Thread.Sleep(100);
            }
        }

        static void PrintOddNumbers(int n, string filePath, bool syncMode)
        {
            for (int i = 1; i <= n; i+=2)
            {
                PrintNumber(i, "Нечетное", filePath);
                if (!syncMode) Thread.Sleep(50);
            }
        }

        static void Task5_TimerTask()
        {
            Console.WriteLine("Задача: мониторинг использования памяти процессом");
            Console.WriteLine("Таймер будет выводить информацию каждые 2 секунды");

            System.Timers.Timer monitorTimer = new System.Timers.Timer(2000);

            Process currentProcess = Process.GetCurrentProcess();
            int iteration = 0;

            monitorTimer.Elapsed += (sender, e) =>
            {
                iteration++;
                currentProcess.Refresh();

                string info = $"[Итерация {iteration}] " +
                             $"Память: {currentProcess.WorkingSet64 / 1024 / 1024} MB, " +
                             $"CPU время: {currentProcess.TotalProcessorTime:mm\\:ss}, " +
                             $"Потоков: {currentProcess.Threads.Count}";

                Console.WriteLine(info);

                File.AppendAllText("memory_monitor.log", $"{DateTime.Now::HH::mm::ss} - {info}\n");

                if(iteration >= 10)
                {
                    Console.WriteLine("Мониторинг завершен после 10 итераций");
                    monitorTimer.Stop();
                }
            };

            monitorTimer.AutoReset = true;
            monitorTimer.Enabled = true;

            Console.WriteLine("\nМониторинг запущен. Нажмите Enter для остановки...");
            Console.ReadLine();

            monitorTimer.Stop();
            monitorTimer.Dispose();
            Console.WriteLine("Мониторинг остановлен");
            Console.WriteLine($"Данные сохранены в файл: {Path.GetFullPath("memory_monitor.log")}");
        }
        static string GetPriorityString(Process process)
        {
            try
            {
                return process.PriorityClass.ToString();
            }
            catch
            {
                return "N/A";
            }
        }

        static string GetStateString(Process process)
        {
            try
            {
                return process.Responding ? "Running" : "Not Responding";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
