using System;
using System.Diagnostics;
using System.Threading;

namespace SecuritySafetyDecision
{
    public static class SecuritySafetyDecision
    {

        private const string ScriptLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\WinCheckSec\build\Release\winchecksec.exe";
        private const string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
        private static string outputResults = "";

        public static int GetSecuritySafetyPercentage(string fileLocation)
        {
            var resultObject = GetWinCheckSecResultObject(fileLocation);
            int percentage = 0;
            if (resultObject.Aslr) percentage += 20;
            if (resultObject.DynamicBase) percentage += 20;
            if (resultObject.Nx) percentage += 20;
            if (resultObject.Seh) percentage += 20;
            if (resultObject.Isolation) percentage += 5;
            if (resultObject.Gs) percentage += 4;
            if (resultObject.Cfg) percentage += 3;
            if (resultObject.HighEntropyVa) percentage += 2;
            if (resultObject.Authenticode) percentage += 2;
            if (resultObject.SafeSeh) percentage += 1;
            if (resultObject.Rfg) percentage += 1;
            if (resultObject.ForceIntegrity) percentage += 1;

            return percentage;
        }

        public static WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation)
        {
            CallWinCheckSec(fileLocation);
            if (outputResults != "")
            {
                var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WinCheckSecResultObject>(outputResults);
                return resultObject;
            }
            return null;
        }

        static void CallWinCheckSec(string fileLocation)
        {
            string fileToAnalyse = fileLocation != null ? fileLocation : TestFileLocation;
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
        }
    }
}
