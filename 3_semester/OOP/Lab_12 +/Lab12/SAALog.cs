using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAAFileTool.Logging
{
    public class SAALog
    {
        private readonly string _logFilePath;
        private readonly object _lock = new object();

        public SAALog(string directoryForLog = null)
        {
            var dir = string.IsNullOrWhiteSpace(directoryForLog)
                ? AppDomain.CurrentDomain.BaseDirectory
                : directoryForLog;

            Directory.CreateDirectory(dir);
            _logFilePath = Path.Combine(dir, "saalogfile.txt");
        }

        public string LogFilePath
        {
            get { return _logFilePath; }
        }

        public void Write(string action, string detail = "", string path = "")
        {
            try
            {
                var line = string.Format("[{0}] {1} | {2} | {3}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    action ?? "",
                    detail ?? "",
                    path ?? "");

                lock (_lock)
                {
                    // using var заменён на классический using для совместимости с C# 7.3
                    using (var sw = new StreamWriter(_logFilePath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                // не выбрасываем дальше, но логируем в консоль (или можно записать в отдельный лог ошибок)
                Console.Error.WriteLine("Ошибка при записи лога: " + ex.Message);
            }
        }

        public List<string> ReadAll()
        {
            try
            {
                if (!File.Exists(_logFilePath))
                {
                    // target-typed new() заменён на явное создание
                    return new List<string>();
                }

                lock (_lock)
                {
                    return File.ReadAllLines(_logFilePath, Encoding.UTF8).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Ошибка чтения лога: " + ex.Message);
                return new List<string>();
            }
        }

        // Парсит дату из начала строки; ожидает формат [yyyy-MM-dd HH:mm:ss]
        private bool TryParseLineDate(string line, out DateTime dt)
        {
            dt = default(DateTime);
            if (string.IsNullOrWhiteSpace(line)) return false;
            try
            {
                int start = line.IndexOf('[');
                int end = line.IndexOf(']');
                if (start >= 0 && end > start)
                {
                    var between = line.Substring(start + 1, end - start - 1);
                    return DateTime.TryParseExact(between, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out dt);
                }
            }
            catch { }
            return false;
        }

        // Поиск по ключевому слову (независимо от регистра)
        public List<string> SearchByKeyword(string keyword)
        {
            var all = ReadAll();
            if (string.IsNullOrEmpty(keyword)) return all;

            var result = new List<string>();
            foreach (var l in all)
            {
                // String.Contains с перегрузкой StringComparison отсутствует в C#7.3/.NET Framework до определённых версий,
                // поэтому используем IndexOf с StringComparison
                if (l != null && l.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    result.Add(l);
            }
            return result;
        }

        public List<string> SearchByDate(DateTime date)
        {
            var all = ReadAll();
            var result = new List<string>();
            foreach (var l in all)
            {
                DateTime dt;
                if (TryParseLineDate(l, out dt) && dt.Date == date.Date)
                    result.Add(l);
            }
            return result;
        }

        public List<string> SearchByRange(DateTime from, DateTime to)
        {
            var all = ReadAll();
            var result = new List<string>();
            foreach (var l in all)
            {
                DateTime dt;
                if (TryParseLineDate(l, out dt) && dt >= from && dt <= to)
                    result.Add(l);
            }
            return result;
        }

        public int CountEntries()
        {
            return ReadAll().Count;
        }

        /// <summary>
        /// Оставляет в лог-файле только записи за текущий час (по системному времени)
        /// </summary>
        public void KeepOnlyCurrentHour()
        {
            try
            {
                var all = ReadAll();
                var now = DateTime.Now;
                var keep = new List<string>();
                foreach (var l in all)
                {
                    DateTime dt;
                    if (TryParseLineDate(l, out dt))
                    {
                        if (dt.Year == now.Year && dt.Month == now.Month && dt.Day == now.Day && dt.Hour == now.Hour)
                            keep.Add(l);
                    }
                }

                lock (_lock)
                {
                    File.WriteAllLines(_logFilePath, keep, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Ошибка при обрезке лога: " + ex.Message);
            }
        }

        /// <summary>
        /// Удаляет записи старше указанной даты (не включая эту дату)
        /// </summary>
        public void DeleteOlderThan(DateTime dateExclusive)
        {
            try
            {
                var all = ReadAll();
                var keep = new List<string>();
                foreach (var l in all)
                {
                    DateTime dt;
                    if (TryParseLineDate(l, out dt))
                    {
                        if (dt >= dateExclusive) keep.Add(l);
                    }
                    else
                    {
                        // если не удалось распарсить — оставим (по желанию можно не оставлять)
                        keep.Add(l);
                    }
                }

                lock (_lock)
                {
                    File.WriteAllLines(_logFilePath, keep, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Ошибка при удалении старых записей: " + ex.Message);
            }
        }
    }
}
