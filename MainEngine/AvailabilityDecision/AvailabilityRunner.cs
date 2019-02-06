using System;
using System.Diagnostics;
using System.Threading;

namespace AvailabilityDecision
{
    public class AvailabilityRunner
    {
        private static int timeoutInMilliseconds = 5000;

        // Returns 0 if execution was normal, -1 if exception was caught
        public static int RunExecutable(string fileLocation)
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
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    if (!exeProcess.WaitForExit(timeoutInMilliseconds))
                    {
                        exeProcess.Kill();
                    }
                    Thread.Sleep(5000);
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}
