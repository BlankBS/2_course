using System;
using System.Linq;
using System.Text;

namespace lab_1
{
    internal class Program
    {
        static void FormatedOutput(string str)
        {
            Console.WriteLine("\n-------------------------------- \n");
            Console.WriteLine($"{str}\n");
        }

        static void Main(string[] args)
        {
            // -----------------------------------------------------------------------------------
            Console.WriteLine("Переменные всех возможных примитивных типов и их инициализация\n");

            long longValue = 9223372036854775807;
            int intValue = 2147483647;
            short shortValue = 32767;
            byte byteValue = 255;

            float floatValue = 3.14f;
            decimal decimalValue = 348564392.89m;
            double doubleValue = 3.1415956535;

            bool boolValue = true;
            
            char charValue = 'x';
            string stringValue = "BlankBS";

            Console.WriteLine($"long: {longValue}");
            Console.WriteLine($"int: {intValue}");
            Console.WriteLine($"short: {shortValue}");
            Console.WriteLine($"byte: {byteValue}");

            Console.WriteLine($"float: {floatValue}");
            Console.WriteLine($"decimal: {decimalValue}");
            Console.WriteLine($"double: {doubleValue}");

            Console.WriteLine($"bool: {boolValue}");
            Console.WriteLine($"char: {charValue}");
            Console.WriteLine($"string: {stringValue}");

            Console.WriteLine("\nВведите значения переменных: ");
            Console.Write("Введите int: ");
            int iWrite = int.Parse(Console.ReadLine());

            Console.Write("Введите float: ");
            float fWrite = float.Parse(Console.ReadLine());

            Console.Write("Введите bool (true/false): ");
            bool bWrite = bool.Parse(Console.ReadLine());

            Console.Write("Введите char: ");
            char cWrite = char.Parse(Console.ReadLine());

            Console.Write("Введите string: ");
            string sWrite = Console.ReadLine();

            Console.WriteLine("\nВведенные значения: ");
            Console.WriteLine($"int: {iWrite}");
            Console.WriteLine($"float: {fWrite}");
            Console.WriteLine($"bool: {bWrite}");
            Console.WriteLine($"char: {cWrite}");
            Console.WriteLine($"string: {sWrite}");

            // -----------------------------------------------------------------------------------
            FormatedOutput("Операции явного приведение (implicit conversion)");
            // -----------------------------------------------------------------------------------

            double dValue = 6.9;
            int idValue = (int)dValue;
            Console.WriteLine("double -> int = " + idValue);

            float fValue = 9.99f;
            int ifValue = (int)fValue;
            Console.WriteLine("float -> int = " + ifValue);

            long lValue = 1000;
            int ilValue = (int)lValue;
            Console.WriteLine("long -> int = " + ilValue);

            int iValue = 258;
            byte biValue = (byte)iValue;
            Console.WriteLine("int -> byte = " + biValue);

            char cValue = 'x';
            int icValue = (int)cValue;
            Console.WriteLine("char -> int = " + icValue);

            Console.WriteLine("\nОперации неявного приведение (explicit conversion)\n");

            int iExp = 100;
            long liExp = iExp;
            Console.WriteLine("int -> long = " + liExp);

            float fExp = 1.1f;
            double dfExp = fExp;
            Console.WriteLine("float -> double = " + dfExp);

            double dExp = 2.2;
            decimal decExp = (decimal)dExp;
            Console.WriteLine("double -> decimal = " + decExp);

            short sExp = 3;
            int isExp = sExp;
            Console.WriteLine("short -> int = " + isExp);

            byte bExp = 255;
            int ibExp = bExp;
            Console.WriteLine("byte -> int = " + ibExp);

            Console.WriteLine("\nПриведение с помощью Convert\n");

            float fCon = 3.3f;
            int ifCon = Convert.ToInt32(fCon);
            Console.WriteLine("float -> int = " + ifCon);

            bool bCon = true;
            int ibCon = Convert.ToInt32(bCon);
            Console.WriteLine("bool -> int = " + ibCon);

            string sCon = "true";
            bool bsCon = Convert.ToBoolean(sCon);
            Console.WriteLine("string -> bool = " + bsCon);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Упаковака и распоковка значимых типов");
            // -----------------------------------------------------------------------------------

            int a = 5;
            object obj = a;
            Console.WriteLine("Упаковка: " + obj);
            int b = (int)obj;
            Console.WriteLine("Распоковка: " + b);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Работа с неявно типизированной переменной");
            // -----------------------------------------------------------------------------------

            var date = DateTime.Now;
            Console.WriteLine("date = " + date);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Работа с Nullable переменной");
            // -----------------------------------------------------------------------------------

            int? nullableint = null;
            Console.WriteLine("Изначальное значение = " + nullableint);
            nullableint = 17;
            Console.WriteLine("После присвоения значения = " + nullableint);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Присвоение var");
            // -----------------------------------------------------------------------------------

            var myvar = 111;
            myvar = "Hello";

            // -----------------------------------------------------------------------------------
            FormatedOutput("Сравнивание строк");
            // -----------------------------------------------------------------------------------

            string s1 = "BlankBS";
            string s2 = "BlankBS";
            string s3 = "Hello, World!";

            bool areEqual1 = s1 == s2;
            bool areEqual2 = s2 == s3;

            Console.WriteLine("s1 == s2: " + areEqual1);
            Console.WriteLine("s2 == s3: " + areEqual2);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Три строки");
            // -----------------------------------------------------------------------------------

            string concatenated = string.Concat(s1, ' ', s2);
            string copy = s3;
            string substring = s3.Substring(0, s3.Length / 2);
            string[] words = s3.Split(' ');
            string inserted = s2.Insert(0, words[0] + ' ');
            string removed = inserted.Remove(0, words[0].Length + 1);
            string interpolate = $"Интерполяция: s2({s2}) + s3({s3}) = {s2 + s3}";

            Console.WriteLine($"Сцепление: {concatenated}");
            Console.WriteLine($"Копирование: {copy}");
            Console.WriteLine($"Выделение подстроки из s3({s3}): {substring}");
            Console.WriteLine($"Разделение s3({s3}): ");
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine($"Вставка: {inserted}");
            Console.WriteLine($"Удаление: {removed}");
            Console.WriteLine(interpolate);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Пустая и null строки");
            // -----------------------------------------------------------------------------------

            string? strNull = null;
            string strEmpty = "";

            if (string.IsNullOrEmpty(strNull))
            {
                Console.WriteLine("strNull пустая или null");
            }
            if (string.IsNullOrEmpty(strEmpty))
            {
                Console.WriteLine("strEmpty пустая или null");
            }
            Console.WriteLine("Длина strEmpty равна: " + strEmpty.Length);

            // -----------------------------------------------------------------------------------
            FormatedOutput("StringBuilder");
            // -----------------------------------------------------------------------------------

            StringBuilder strBuild = new StringBuilder("fHello");
            strBuild.Remove(1, 5);
            strBuild.Insert(0, "What about your ");
            strBuild.Append("ame");
            Console.WriteLine(strBuild);

            // -----------------------------------------------------------------------------------
            FormatedOutput("Массивы");
            // -----------------------------------------------------------------------------------

            int[,] arr =
            {
                {1, 0, 1},
                {0, 1, 0},
                {1, 0, 1}
            };

            Console.WriteLine("Массив: ");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j],4}");
                }
                Console.WriteLine();
            }

            // -----------------------------------------------------------------------------------
            FormatedOutput("Массив строк");
            // -----------------------------------------------------------------------------------

            string[] strArr = { "aaa", "bbb", "ccc" };
            Console.WriteLine("Содержимое массива строк: ");
            foreach (string str in strArr)
            {
                Console.Write(str + ' ');
            }

            int strArrLength = strArr.Length;
            Console.WriteLine($"\nДлина массива: {strArrLength}");

            Console.Write($"Введите индекс элемента, который требуется изменить (0 - {strArrLength - 1}): ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < strArrLength)
            {
                Console.Write("Введите новое значение: ");
                string newElem = Console.ReadLine();
                strArr[index] = newElem;

                Console.WriteLine("Новый массив: ");
                foreach (string str in strArr)
                {
                    Console.Write(str + ' ');
                }
            }
            else
            {
                Console.WriteLine("Неверный индекс");
            }

            // -----------------------------------------------------------------------------------
            FormatedOutput("Ступенчатый массив");
            // -----------------------------------------------------------------------------------

            double[][] newArr = new double[3][];

            newArr[0] = new double[2];
            newArr[1] = new double[3];
            newArr[2] = new double[4];

            Console.WriteLine("Заполните ступенчатый массив");

            for (int i = 0; i < newArr.Length; i++)
            {
                Console.WriteLine($"Введите значения для строки {i + 1} (длина {newArr[i].Length}): ");
                for (int j = 0; j < newArr[i].Length; j++)
                {
                    Console.Write($"arr[{i}][{j}] = ");
                    newArr[i][j] = double.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("Содержимое ступенчатого массива");

            for (int i = 0; i < newArr.Length; i++)
            {
                for (int j =0;j<newArr[i].Length; j++)
                {
                    Console.Write($"{newArr[i][j]}");
                }
                Console.WriteLine();
            }

            // -----------------------------------------------------------------------------------
            FormatedOutput("Неявно типизированные переменные для массива");
            // -----------------------------------------------------------------------------------

            var arrVar = new[] { "first", "second", "third" };
            Console.WriteLine("Содержимое массива: ");
            foreach (var item in arrVar)
            {
                Console.Write(item + ' ');
            }
            Console.WriteLine();

            var strVar = "Fourth";
            Console.WriteLine($"strVar = {strVar}");

            // -----------------------------------------------------------------------------------
            FormatedOutput("Кортежи");
            // -----------------------------------------------------------------------------------

            var myTuple = (1, "first", "s", "third", 2);
            Console.WriteLine($"Весь кортеж: {myTuple}");
            Console.WriteLine($"1-ый элемент: {myTuple.Item1}");
            Console.WriteLine($"3-ый элемент: {myTuple.Item3}");
            Console.WriteLine($"4-ый элемент: {myTuple.Item4}");

            var(intTuple, string1Tuple, charTuple, string2Tuple, ulongTuple) = myTuple;
            Console.WriteLine($"int: {intTuple}, str1: {string1Tuple}, char: {charTuple}, str2: {string2Tuple}, ulong: {ulongTuple}");

            var (_, _, _, _, ulong2Tuple) = myTuple;
            Console.WriteLine($"ulong с использование _: {ulong2Tuple}");
            
            var secondTuple = (3, "_first", "c", "_third", 4);
            Console.WriteLine($"Второй кортеж: {secondTuple}");
            Console.WriteLine($"Кортеж равен другому кортежу: {myTuple.Equals(secondTuple)}");

            // -----------------------------------------------------------------------------------
            FormatedOutput("Функция");
            // -----------------------------------------------------------------------------------

            (int max, int min, int sum, char ch) AnalyzeTuple(int[] numbers, string text)
            {
                if (numbers.Length == 0)
                    throw new ArgumentException("Массив не может быть пустым");

                int max = numbers[0];
                int min = numbers[1];
                int sum = 0;

                foreach (int number in numbers)
                {
                    if (number > max) max = number;
                    if (min > number) min = number;
                    sum += number;
                }

                char ch =  text.Length > 0 ? text[0] : '\0';
                return (max, min, sum, ch);
            }

            int[] numbers = { 6, 5, 8, 2, 7 };
            string text = "BlankBS";

            var result = AnalyzeTuple(numbers, text);

            Console.WriteLine($"Максимальная цифра: {result.max}");
            Console.WriteLine($"Минимальная цифра: {result.min}");
            Console.WriteLine($"Сумма: {result.sum}");
            Console.WriteLine($"Первый символ строки: {result.ch}");

            // -----------------------------------------------------------------------------------
            FormatedOutput("Checked/Unchecked");
            // -----------------------------------------------------------------------------------
            
            void CheckedOperation()
            {
                checked
                {
                    try
                    {
                        int maxValue = int.MaxValue;
                        int result = maxValue + 1;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Checked: переполнение");
                    }
                }
            }

            void UncheckedOperation()
            {
                unchecked
                {
                    int maxValue = int.MaxValue;
                    int result = maxValue + 1;
                    Console.WriteLine($"Unchecked: результат переполнения: {result}");
                }
            }

            CheckedOperation();
            UncheckedOperation();
        }
    }
}