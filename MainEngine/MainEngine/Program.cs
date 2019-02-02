using System;
using System.Diagnostics;
using System.Threading;

namespace MainEngine
{
    class Program
    {
        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";
        private const string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";

        static void Main(string[] args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = ScriptLocation,
                Arguments = TestFileLocation,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            //string output = "";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                    exeProcess.BeginOutputReadLine();
                    exeProcess.WaitForExit();
                    Thread.Sleep(5000);
                }
                //Console.WriteLine(output);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                // Log error.
            }
        }
    }
}
