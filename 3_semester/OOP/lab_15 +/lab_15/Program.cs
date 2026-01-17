using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TplLab
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== 1. Длительная задача + Stopwatch ===");
            RunLongTaskWithStopwatch();

            Console.WriteLine("\n=== 2. Тот же алгоритм + CancellationToken ===");
            RunLongTaskWithCancellation();

            Console.WriteLine("\n=== 3. Три задачи с результатами для четвертой ===");
            RunFormulaTasks().Wait();

            Console.WriteLine("\n=== 4. Continuation: ContinueWith и GetAwaiter/GetResult ===");
            ContinuationWithContinueWith();
            ContinuationWithAwaiter();

            Console.WriteLine("\n=== 5. Parallel.For/ForEach против обычных циклов ===");
            CompareParallelFor();

            Console.WriteLine("\n=== 6. Parallel.Invoke ===");
            ParallelInvokeDemo();

            Console.WriteLine("\n=== 7. BlockingCollection: склад техники ===");
            WarehouseDemo().Wait();

            Console.WriteLine("\n=== 8. async/await для любого метода ===");
            SimpleAsyncDemo().Wait();

            Console.WriteLine("\nГотово. Нажмите любую клавишу.");
            Console.ReadKey();
        }


        static void RunLongTaskWithStopwatch()
        {
            const int max = 5_000_000;
            for (int run = 1; run <= 3; run++)
            {
                var sw = Stopwatch.StartNew();

                var task = Task.Run(() =>
                {
                    Console.WriteLine("Run {0}: Task Id = {1}", run, Task.CurrentId);
                    var primes = Sieve(max);
                    return primes.Count;
                });

                Console.WriteLine("Run {0}: IsCompleted = {1}, Status = {2}",
                    run, task.IsCompleted, task.Status);

                int result = task.Result;
                sw.Stop();

                Console.WriteLine(
                    "Run {0}: IsCompleted = {1}, Status = {2}, Primes = {3}, Time = {4} ms",
                    run, task.IsCompleted, task.Status, result, sw.ElapsedMilliseconds);
            }
        }

        static List<int> Sieve(int n)
        {
            var isPrime = new bool[n + 1];
            for (int i = 2; i <= n; i++)
                isPrime[i] = true;

            for (int p = 2; p * p <= n; p++)
            {
                if (!isPrime[p]) continue;
                for (int k = p * p; k <= n; k += p)
                    isPrime[k] = false;
            }

            var list = new List<int>();
            for (int i = 2; i <= n; i++)
                if (isPrime[i]) list.Add(i);
            return list;
        }


        static void RunLongTaskWithCancellation()
        {
            const int max = 20_000_000;
            var cts = new CancellationTokenSource();
            cts.CancelAfter(2000);
            var token = cts.Token;

            var task = Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();
                Console.WriteLine("Task with cancellation. Id = {0}", Task.CurrentId);

                var isPrime = new bool[max + 1];
                for (int i = 2; i <= max; i++)
                    isPrime[i] = true;

                for (int p = 2; p * p <= max; p++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested inside algorithm.");
                        token.ThrowIfCancellationRequested();
                    }

                    if (!isPrime[p]) continue;
                    for (int k = p * p; k <= max; k += p)
                    {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();
                        isPrime[k] = false;
                    }
                }

                int count = 0;
                for (int i = 2; i <= max; i++)
                    if (isPrime[i]) count++;

                sw.Stop();
                Console.WriteLine("Finished, primes = {0}, time = {1} ms",
                    count, sw.ElapsedMilliseconds);
                return count;
            }, token);

            try
            {
                int result = task.Result;
                Console.WriteLine("Task completed successfully, result = {0}", result);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is OperationCanceledException)
                    Console.WriteLine("Task was canceled.");
                else
                    Console.WriteLine("Task error: {0}", ex.InnerException);
            }
            finally
            {
                cts.Dispose();
            }
        }


        static Task RunFormulaTasks()
        {
            // f = (a + b) * c
            var tA = Task.Run<int>(() =>
            {
                Thread.Sleep(300);
                return 10;
            });
            var tB = Task.Run<int>(() =>
            {
                Thread.Sleep(400);
                return 20;
            });
            var tC = Task.Run<int>(() =>
            {
                Thread.Sleep(500);
                return 3;
            });

            var formulaTask = Task.Run(async () =>
            {
                int a = await tA;
                int b = await tB;
                int c = await tC;
                int f = (a + b) * c;
                Console.WriteLine("Formula (a + b) * c = {0}", f);
            });

            return formulaTask;
        }


        static void ContinuationWithContinueWith()
        {
            Task<int> t1 = Task.Run<int>(() =>
            {
                Thread.Sleep(500);
                return 5;
            });

            Task<int> t2 = Task.Run<int>(() =>
            {
                Thread.Sleep(700);
                return 7;
            });

            Task<int> t3 = Task.Run<int>(() =>
            {
                Thread.Sleep(900);
                return 9;
            });

            Task continuation = Task.Factory.ContinueWhenAll(
                new[] { t1, t2, t3 },
                tasks =>
                {
                    int sum = tasks.Sum(t => t.Result);
                    Console.WriteLine("ContinueWith: sum = {0}", sum);
                });

            continuation.Wait();
        }

        static void ContinuationWithAwaiter()
        {
            Task<int> t = Task.Run<int>(() =>
            {
                Thread.Sleep(500);
                return 42;
            });

            var awaiter = t.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                try
                {
                    int result = awaiter.GetResult();
                    Console.WriteLine("GetAwaiter/GetResult: result = {0}", result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Continuation error: {0}", ex.Message);
                }
            });

            t.Wait();
        }


        static void CompareParallelFor()
        {
            const int size = 5_000_000;
            int[] data = Enumerable.Range(0, size).ToArray();
            int[] copy1 = new int[size];
            int[] copy2 = new int[size];

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
                copy1[i] = data[i] * 2;
            sw.Stop();
            Console.WriteLine("Normal for: {0} ms", sw.ElapsedMilliseconds);

            sw.Restart();
            Parallel.For(0, size, i =>
            {
                copy2[i] = data[i] * 2;
            });
            sw.Stop();
            Console.WriteLine("Parallel.For: {0} ms", sw.ElapsedMilliseconds);

            sw.Restart();
            Parallel.ForEach(
                Partitioner.Create(0, size),
                range =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                        copy2[i] = data[i] * 3;
                });
            sw.Stop();
            Console.WriteLine("Parallel.ForEach (partitioned): {0} ms", sw.ElapsedMilliseconds);
        }


        static void ParallelInvokeDemo()
        {
            var sw = Stopwatch.StartNew();
            Parallel.Invoke(
                () =>
                {
                    Thread.Sleep(400);
                    Console.WriteLine("Action 1 done");
                },
                () =>
                {
                    Thread.Sleep(600);
                    Console.WriteLine("Action 2 done");
                },
                () =>
                {
                    Thread.Sleep(800);
                    Console.WriteLine("Action 3 done");
                });
            sw.Stop();
            Console.WriteLine("Parallel.Invoke total time: {0} ms", sw.ElapsedMilliseconds);
        }


        static Task WarehouseDemo()
        {
            var rnd = new Random();
            var storage = new BlockingCollection<string>(5);

            string[] supplierNames = { "LG", "Samsung", "Bosch", "Sony", "Philips" };
            string[] productTypes = { "TV", "Fridge", "Washer", "Microwave", "Vacuum" };

            var suppliers = supplierNames
                .Select((name, idx) =>
                    Task.Run(async () =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            await Task.Delay(rnd.Next(200, 1000));
                            string item = string.Format(
                                "{0}-{1}-{2}",
                                name,
                                productTypes[rnd.Next(productTypes.Length)],
                                Guid.NewGuid().ToString().Substring(0, 4));
                            storage.Add(item);
                            PrintStorage("Supplier adds", storage);
                        }
                    }))
                .ToArray();

            var consumers = Enumerable.Range(1, 10)
                .Select(id =>
                    Task.Run(async () =>
                    {
                        while (!storage.IsCompleted)
                        {
                            string item;
                            if (storage.TryTake(out item, 500))
                            {
                                Console.WriteLine("Customer {0} bought: {1}", id, item);
                                PrintStorage(
                                    string.Format("Customer {0} takes", id),
                                    storage);
                                await Task.Delay(rnd.Next(100, 400));
                            }
                            else
                            {
                                Console.WriteLine("Customer {0} found no items and left.", id);
                                break;
                            }
                        }
                    }))
                .ToArray();

            return Task.Run(async () =>
            {
                await Task.WhenAll(suppliers);
                storage.CompleteAdding();
                await Task.WhenAll(consumers);
                storage.Dispose();
            });
        }

        static void PrintStorage(string reason, BlockingCollection<string> storage)
        {
            string text = storage.Count == 0
                ? "<empty>"
                : string.Join(", ", storage.ToArray());
            Console.WriteLine("[{0}] Stock now: {1}", reason, text);
        }


        static async Task SimpleAsyncDemo()
        {
            Console.WriteLine("Async method started...");
            int result = await LongOperationAsync(1000);
            Console.WriteLine("Async method finished, result = {0}", result);
        }

        static async Task<int> LongOperationAsync(int delayMs)
        {
            var sw = Stopwatch.StartNew();
            await Task.Delay(delayMs);
            sw.Stop();
            return (int)sw.ElapsedMilliseconds;
        }
    }
}
