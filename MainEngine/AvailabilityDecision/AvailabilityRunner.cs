using System.Diagnostics;
using System.Threading.Tasks;

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
                        exeProcess.Kill();
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
    }
}
