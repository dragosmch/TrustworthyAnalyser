using System;
using System.Diagnostics;
using LibraryModule;

namespace SecuritySafetyModule
{
    public class SecuritySafetyRunner : ISecuritySafetyRunner
    {
        // Location of the WinCheckSec script
        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";

        // Result of the WinCheckSec call as characters of output
        private string _outputResults = "";

        /// <inheritdoc />
        public WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation)
        {
            if (!CallWinCheckSec(fileLocation) || _outputResults.Length == 0) return null;

            var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WinCheckSecResultObject>(_outputResults);
            _outputResults = "";
            return resultObject;
        }

        /// <summary>
        /// Call the WinCheckSec library on the given file.
        /// </summary>
        /// <param name="fileLocation">Path to executable file.</param>
        /// <returns>An object containing security properties read by WinCheckSec</returns>
        private bool CallWinCheckSec(string fileLocation)
        {
            if (string.IsNullOrEmpty(fileLocation)) return false;

            // Wrap location in apostrophes in case spaces are present in the path
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
            catch // exception means the process failed and the analysis cannot continue
            {
                return false;
            }
        }

        /// <summary>
        /// Read error data produced by the started process.
        /// </summary>
        /// <param name="sender">Sender of data.</param>
        /// <param name="e">Event arguments.</param>
        private static void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e);
        }

        /// <summary>
        /// Read data produced by the started process.
        /// </summary>
        /// <param name="sender">Sender of data.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (_outputResults.Length == 0) _outputResults += e.Data;
        }
    }
}
