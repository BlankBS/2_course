using System;
using System.IO;
using SAAFileTool.Logging;

namespace SAAFileTool.Info
{
    public class SAADiskInfo
    {
        private readonly SAALog _log;
        public SAADiskInfo(SAALog log) => _log = log;

        private void Log(string a, string d = "", string p = "") => _log?.Write(a, d, p);

        public void PrintDrivesInfo()
        {
            foreach (var d in DriveInfo.GetDrives())
            {
                if (!d.IsReady) continue;

                Console.WriteLine($"Drive: {d.Name}");
                Console.WriteLine($"  Format: {d.DriveFormat}");
                Console.WriteLine($"  Type: {d.DriveType}");
                Console.WriteLine($"  Total: {d.TotalSize}");
                Console.WriteLine($"  Free: {d.AvailableFreeSpace}");
                Console.WriteLine($"  Label: {d.VolumeLabel}\n");

                Log("DiskInfoShown", d.Name);
            }
        }

        public (long free, string fs) GetFreeSpaceAndFs(string driveName)
        {
            try
            {
                var d = new DriveInfo(driveName);
                if (!d.IsReady) throw new Exception("Drive not ready");
                return (d.AvailableFreeSpace, d.DriveFormat);
            }
            catch (Exception ex)
            {
                Log("DiskQueryError", ex.Message);
                return (0, "");
            }
        }
    }
}
