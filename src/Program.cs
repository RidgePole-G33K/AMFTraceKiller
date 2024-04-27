using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AMFTraceKiller
{
    class Program
    {
        static void Main()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "livehime.exe";

            if (!File.Exists(path))
                return;

            var waiter = new AutoResetEvent(false);

            var p = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(path)
            };
            p.Exited += (o, e) =>
            {
                if (File.Exists(path = @"C:\AMFTrace.log"))
                    File.Delete(path);
                waiter.Set();
            };
            p.Start();

            waiter.WaitOne();
        }
    }
}