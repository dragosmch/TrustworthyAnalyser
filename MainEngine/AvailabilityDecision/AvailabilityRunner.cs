using System.Diagnostics;
using System.Management;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace AvailabilityModule
{
    /// <summary>
    /// Class that contains methods to run a given executable for a specified amount of times and also duration
    /// </summary>
    public class AvailabilityRunner : IAvailabilityRunner
    {

        /// <summary>
        ///  Run an executable file multiple times.
        /// </summary>
        /// <param name="fileLocation">Path to file.</param>
        /// <param name="noOfRuns">How many times to run the executable.</param>
        /// <param name="timeout">How long to let the executable run, in milliseconds.</param>
        /// <param name="runExecutablesInParallel">Run the executable multiple times in parallel if true, sequentially otherwise.</param>
        /// <returns>Number of runs that were successful(no errors occured).</returns>
        public int RunExecutableMultipleTimes(IProgress<int> progress, string fileLocation, int noOfRuns, int timeout, bool runExecutablesInParallel)
        {
            int resultOfRuns = 0; 
            if (runExecutablesInParallel)
            {
                Parallel.For(0, noOfRuns, actionBody =>
                    {
                        progress.Report(1);
                        int resultOfThisRun = RunExecutable(fileLocation, timeout);
                        Interlocked.Add(ref resultOfRuns, resultOfThisRun);
                        progress.Report(1);
                    });
            }
            else
            {
                for (int i = 0; i < noOfRuns; i++)
                {
                    resultOfRuns += RunExecutable(fileLocation, timeout);
                    progress.Report(1);
                }
            }
            return resultOfRuns;
        }


        /// <summary>
        /// Function to run a given executable for a specified period of time.
        /// </summary>
        /// <param name="fileLocation">Path to file.</param>
        /// <param name="timeout">How long to let the executable run, in milliseconds.</param>
        /// <returns>1 if execution was normal, 0 if exception was caught or stopped running.</returns>
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
                using (var executableProcess = Process.Start(processStartInfo))
                {
                    Thread.Sleep(1000);
                    // Kill the process if is running more than a threshold
                    if (executableProcess == null || executableProcess.WaitForExit(timeout)) return 0;

                    KillProcessAndChildren(executableProcess.Id);
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

        /// <summary>
        /// Kill the processes created for running the executable, recursively.
        /// </summary>
        /// <param name="processId">Id of the process to kill</param>
        private static void KillProcessAndChildren(int processId)
        {
            var cultureInfo = CultureInfo.InvariantCulture;
            var processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + processId);

            // Get all processes created by running the executable
            var processCollection = processSearcher.Get();

            // We must kill the child processes first
            foreach (var processObject in processCollection)
            {
                var managementObject = (ManagementObject) processObject;
                // Recursive call for every child process
                KillProcessAndChildren(Convert.ToInt32(managementObject["ProcessID"], cultureInfo));
            }
            var process = Process.GetProcessById(processId);
            if (!process.HasExited) process.Kill();
        }
    }
}
