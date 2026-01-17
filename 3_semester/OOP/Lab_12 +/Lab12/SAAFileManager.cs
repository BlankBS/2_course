using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SAAFileTool.Logging;

namespace SAAFileTool.FileManagement
{
    public class SAAFileManager
    {
        private readonly SAALog _log;
        public SAAFileManager(SAALog log) => _log = log;

        private void Log(string a, string d = "", string p = "") => _log?.Write(a, d, p);

        public void PerformInspectAndFileOps(string rootPath)
        {
            try
            {
                var entries = Directory.EnumerateFileSystemEntries(rootPath).ToList();
                Log("ListedEntries", entries.Count.ToString(), rootPath);

                var inspect = Path.Combine(rootPath, "SAAInspect");
                Directory.CreateDirectory(inspect);

                var infoFile = Path.Combine(inspect, "saadirinfo.txt");
                using (var sw = new StreamWriter(infoFile))
                {
                    sw.WriteLine($"Listing {rootPath}:");
                    foreach (var e in entries) sw.WriteLine(e);
                }

                var copy = Path.Combine(inspect, "saadirinfo_copy.txt");
                File.Copy(infoFile, copy, true);

                var renamed = Path.Combine(inspect, "saadirinfo_renamed.txt");
                if (File.Exists(renamed)) File.Delete(renamed);
                File.Move(copy, renamed);

                File.Delete(infoFile);
            }
            catch (Exception ex)
            {
                Log("PerformInspectError", ex.Message);
            }
        }

        public void CopyFilesByExtension(string sourceDir, string rootTargetDir, string ext)
        {
            try
            {
                var dest = Path.Combine(rootTargetDir, "SAAFiles");
                Directory.CreateDirectory(dest);

                foreach (var f in Directory.GetFiles(sourceDir, "*" + ext))
                {
                    File.Copy(f, Path.Combine(dest, Path.GetFileName(f)), true);
                }

                var inspect = Path.Combine(rootTargetDir, "SAAInspect");
                Directory.CreateDirectory(inspect);

                var final = Path.Combine(inspect, "SAAFiles");
                if (Directory.Exists(final))
                    Directory.Delete(final, true);

                Directory.Move(dest, final);
            }
            catch (Exception ex)
            {
                Log("CopyFilesByExtError", ex.Message);
            }
        }

        public void ZipAndUnzipSAAFiles(string inspectDir, string unzipTarget)
        {
            try
            {
                var src = Path.Combine(inspectDir, "SAAFiles");
                if (!Directory.Exists(src)) return;

                var zip = Path.Combine(inspectDir, "SAAFiles.zip");
                if (File.Exists(zip)) File.Delete(zip);

                ZipFile.CreateFromDirectory(src, zip);

                Directory.CreateDirectory(unzipTarget);
                var extract = Path.Combine(unzipTarget, "SAAFiles_unzipped");
                if (Directory.Exists(extract)) Directory.Delete(extract, true);

                ZipFile.ExtractToDirectory(zip, extract);
            }
            catch (Exception ex)
            {
                Log("ZipUnzipError", ex.Message);
            }
        }
    }
}
