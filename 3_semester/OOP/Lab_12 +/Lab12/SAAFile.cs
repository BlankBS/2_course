using System;
using System.IO;
using SAAFileTool.Logging;

namespace SAAFileTool.Info
{
    public class SAAFileInfo
    {
        private readonly SAALog _log;
        public SAAFileInfo(SAALog log) => _log = log;

        private void Log(string a, string d = "", string p = "") => _log?.Write(a, d, p);

        public void PrintFileInfo(string filePath)
        {
            try
            {
                var fi = new FileInfo(filePath);
                if (!fi.Exists)
                {
                    Console.WriteLine("Файл не найден.");
                    return;
                }

                Console.WriteLine($"Full: {fi.FullName}");
                Console.WriteLine($"Name: {fi.Name}");
                Console.WriteLine($"Ext: {fi.Extension}");
                Console.WriteLine($"Size: {fi.Length}");
                Console.WriteLine($"Created: {fi.CreationTime}");
                Console.WriteLine($"Modified: {fi.LastWriteTime}");

                Log("FileInfoShown", fi.Name, fi.FullName);
            }
            catch (Exception ex)
            {
                Log("FileInfoError", ex.Message);
            }
        }
    }
}
