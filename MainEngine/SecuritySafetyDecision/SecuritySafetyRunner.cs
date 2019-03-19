using System;
using System.Diagnostics;
using LibraryModule;

namespace SecuritySafetyModule
{
    public class SecuritySafetyRunner : ISecuritySafetyRunner
    {

        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";
        private string _outputResults = "";

        public WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation)
        {
            if (!CallWinCheckSec(fileLocation) || _outputResults.Length == 0) return null;

            var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WinCheckSecResultObject>(_outputResults);
            _outputResults = "";
            return resultObject;
        }

        private bool CallWinCheckSec(string fileLocation)
        {
            if (string.IsNullOrEmpty(fileLocation)) return false;
            string fileToAnalyse = "\"" + fileLocation + "\"";
            var startInfo = new ProcessStartInfo
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
                using (var exeProcess = Process.Start(startInfo))
                {
                    if (exeProcess == null) return false;
                    exeProcess.OutputDataReceived += OnOutputDataReceived;
                    exeProcess.ErrorDataReceived += ErrorDataReceived;
                    exeProcess.BeginOutputReadLine();
                    exeProcess.WaitForExit();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e);
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (_outputResults.Length == 0) _outputResults += e.Data;
        }
    }
}
