using SAAFileTool.FileManagement;
using SAAFileTool.Info;
using SAAFileTool.Logging;
using System;
using System.IO;

namespace SAAFileTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SAA File Tool Demo ===");

            var log = new SAALog();

            var diskInfo = new SAADiskInfo(log);
            diskInfo.PrintDrivesInfo();

            var fileInfo = new SAAFileInfo(log);
            Console.Write("\nВведите путь к файлу (ENTER — пример): ");
            var filePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var tmp = Path.Combine(Path.GetTempPath(), "saasample.txt");
                File.WriteAllText(tmp, "sample");
                fileInfo.PrintFileInfo(tmp);
            }
            else fileInfo.PrintFileInfo(filePath);

            var dirInfo = new SAADirInfo(log);
            Console.Write("\nВведите путь к директории (ENTER — профиль пользователя): ");
            var dirPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dirPath))
                dirPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            dirInfo.PrintDirInfo(dirPath);

            var fm = new SAAFileManager(log);

            Console.Write("\nВведите корневую директорию для операций (ENTER — текущая): ");
            var root = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(root)) root = Directory.GetCurrentDirectory();

            fm.PerformInspectAndFileOps(root);

            Console.Write("\nВведите директорию-источник для файлов (ENTER — текущая): ");
            var src = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(src)) src = Directory.GetCurrentDirectory();

            Console.Write("Введите расширение (ENTER = .txt): ");
            var ext = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ext)) ext = ".txt";

            fm.CopyFilesByExtension(src, root, ext);

            var inspectDir = Path.Combine(root, "SAAInspect");
            Console.Write("\nВведите папку для распаковки архива (ENTER — temp): ");
            var unzipTarget = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(unzipTarget))
                unzipTarget = Path.Combine(Path.GetTempPath(), "SAA_unzip_demo");

            fm.ZipAndUnzipSAAFiles(inspectDir, unzipTarget);

            Console.WriteLine("\n--- Работа с логом ---");
            Console.WriteLine($"Лог расположен: {log.LogFilePath}");
            Console.WriteLine($"Всего записей: {log.CountEntries()}");

            Console.Write("Ключевое слово (ENTER — пропустить): ");
            var key = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(key))
                log.SearchByKeyword(key).ForEach(Console.WriteLine);

            Console.Write("Дата (yyyy-MM-dd) или Enter: ");
            var dStr = Console.ReadLine();
            if (DateTime.TryParse(dStr, out var d))
                log.SearchByDate(d).ForEach(Console.WriteLine);

            Console.Write("Оставить в логе только текущий час? y/N: ");
            var ch = Console.ReadLine();
            if (ch.ToLower() == "y") log.KeepOnlyCurrentHour();

            Console.WriteLine($"Конечное количество записей: {log.CountEntries()}");

            Console.WriteLine("\n=== Завершено ===");
        }
    }
}
