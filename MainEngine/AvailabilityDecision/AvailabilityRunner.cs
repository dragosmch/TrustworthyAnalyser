using System.Diagnostics;
using System.Management;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AvailabilityModule
{
    public static class AvailabilityRunner
    {

        // Returns number of runs that were successful(no errors occured)
        public static int RunExecutableMultipleTimes(string fileLocation, int noOfRuns, int timeout)
        {
            int resultOfRuns = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //for (int i = 0; i < noOfRuns; i++)
            //{
            //    resultOfRuns += RunExecutable(fileLocation, timeout);
            //}
            Parallel.For(0, noOfRuns, actionBody =>
                {
                    int resultOfThisRun = RunExecutable(fileLocation, timeout);
                    Interlocked.Add(ref resultOfRuns, resultOfThisRun);
                });
            Console.WriteLine(sw.ElapsedMilliseconds);
            return resultOfRuns;
        }



        // Returns 1 if execution was normal, 0 if exception was caught or problem stopped running
        private static int RunExecutable(string fileLocation, int timeout)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileLocation,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas",
                UseShellExecute = true,
                CreateNoWindow = true
            };

            try
            {
                using (var exeProcess = Process.Start(processStartInfo))
                {
                    Thread.Sleep(1000);
                    // Kill the process if is running more than a threshold
                    if (exeProcess == null || exeProcess.WaitForExit(timeout)) return 0;

                    KillProcessAndChildren(exeProcess.Id);
                    // if we had to kill it, it means it ran fine for a while
                    return 1;
                }
            }
            // If exception, means run was failure
            catch
            {
                return 0;
            }
        }

        private static void KillProcessAndChildren(int pid)
        {
            var culture = CultureInfo.InvariantCulture;
            var processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            var processCollection = processSearcher.Get();

            // We must kill child processes first!
            foreach (var processObject in processCollection)
            {
                var managementObject = (ManagementObject) processObject;
                // kill child processes(also kills children of children etc.)
                KillProcessAndChildren(Convert.ToInt32(managementObject["ProcessID"], culture));
            }
            var process = Process.GetProcessById(pid);
            if (!process.HasExited) process.Kill();

        }
    }
}
