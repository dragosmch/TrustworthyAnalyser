using System.Diagnostics;
using System.Threading.Tasks;
using System.Management;
using System;

namespace AvailabilityModule
{
    public class AvailabilityRunner
    {

        // Returns number of runs that were successfull(no errors occured)
        public static int RunExecutableMultipleTimes(string fileLocation, int noOfRuns, int timeout)
        {
            int resultOfRuns = 0;
            for (int i = 0; i < noOfRuns; i++)
            {
                resultOfRuns +=  RunExecutable(fileLocation, timeout);
            }
            return resultOfRuns;
        }



        // Returns 1 if execution was normal, 0 if exception was caught or problem stopped running
        private static int RunExecutable(string fileLocation, int timeout)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = fileLocation,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas",
                UseShellExecute = true,
                CreateNoWindow = true
            };

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    // Kill the process if is running more than a threshold
                    if (!exeProcess.WaitForExit(timeout))
                    {
                        KillProcessAndChildrens(exeProcess.Id);
                        // if we had to kill it, it means it ran fine for a while
                        return 1;
                    }
                }
                return 0;
            }
            // If exception, means run was failure
            catch
            {
                return 0;
            }
        }

        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            // We must kill child processes first!
            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    // kill child processes(also kills childrens of childrens etc.)
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"]));
                }
            }

            // Then kill parents.
            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
    }
}
