using System;
using System.IO;
using SAAFileTool.Logging;

namespace SAAFileTool.Info
{
    public class SAADirInfo
    {
        private readonly SAALog _log;
        public SAADirInfo(SAALog log) => _log = log;

        private void Log(string a, string d = "", string p = "") => _log?.Write(a, d, p);

        public void PrintDirInfo(string dirPath)
        {
            try
            {
                var di = new DirectoryInfo(dirPath);
                if (!di.Exists)
                {
                    Console.WriteLine("Каталог не найден.");
                    return;
                }

                Console.WriteLine($"Directory: {di.FullName}");
                Console.WriteLine($"Files: {di.GetFiles().Length}");
                Console.WriteLine($"Created: {di.CreationTime}");
                Console.WriteLine($"Subdirs: {di.GetDirectories().Length}");
                Console.WriteLine("Parents:");

                var p = di.Parent;
                while (p != null)
                {
                    Console.WriteLine("  " + p.FullName);
                    p = p.Parent;
                }

                Log("DirInfoShown", "", dirPath);
            }
            catch (Exception ex)
            {
                Log("DirInfoError", ex.Message);
            }
        }
    }
}
