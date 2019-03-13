using System;
using System.Diagnostics;

namespace SecuritySafetyModule
{
    class SecuritySafetyRunner
    {

        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";
        private const string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
        private static string _outputResults = "";

        public static WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation)
        {
            CallWinCheckSec(fileLocation);
            if (_outputResults != "")
            {
                var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WinCheckSecResultObject>(_outputResults);
                _outputResults = "";
                return resultObject;

            }
            return null;
        }

        private static void CallWinCheckSec(string fileLocation)
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
            if (_outputResults.Length == 0) _outputResults += e.Data;
        }
    }
}
