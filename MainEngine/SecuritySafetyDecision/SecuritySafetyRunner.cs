using System;
using System.Diagnostics;

namespace SecuritySafetyModule
{
    class SecuritySafetyRunner
    {

        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";
        private const string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
        private static string outputResults = "";

        public static WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation)
        {
            CallWinCheckSec(fileLocation);
            if (outputResults != "")
            {
                var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WinCheckSecResultObject>(outputResults);
                outputResults = "";
                return resultObject;

            }
            return null;
        }

        static void CallWinCheckSec(string fileLocation)
        {
            string fileToAnalyse = fileLocation != null ? "\"" + fileLocation + "\"" : TestFileLocation;
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = ScriptLocation,
                Arguments = "-j " + fileToAnalyse,
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
                    exeProcess.ErrorDataReceived += ErrorDataReceived;
                    exeProcess.BeginOutputReadLine();
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Log error.
            }
        }

        private static void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e);
        }

        private static void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (outputResults == "") outputResults += e.Data;
        }
    }
}
