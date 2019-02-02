using SecuritySafetyDecision;
using System;
using System.Diagnostics;
using System.Threading;

namespace MainEngine
{
    class Program
    {
        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";
        private const string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
        private static string outputResults = "";

        static void Main(string[] args)
        {
            CallWinCheckSec();
            if (outputResults != "")
            {
                var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WinCheckSecResultObject>(outputResults);
            }
        }

        static void CallWinCheckSec()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = ScriptLocation,
                Arguments = "-j " + TestFileLocation,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.OutputDataReceived += OnOutputDataReceived;
                    exeProcess.BeginOutputReadLine();
                    exeProcess.WaitForExit();
                    Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Log error.
            }
        }

        static void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputResults += e.Data;
            Console.WriteLine(e.Data);
        }
    }
}
