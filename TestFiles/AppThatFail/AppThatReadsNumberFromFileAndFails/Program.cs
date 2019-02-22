using System;
using System.IO;
using System.Threading;

namespace AppThatReadsNumberFromFileAndFails
{
    class Program
    {
        private static string locationOfFile = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\failInputFile.txt";

        static void Main(string[] args)
        {
            string result;
            using (var streamReader = new StreamReader(locationOfFile))
            {
                result = streamReader.ReadToEnd();
            }
            if (result == "fail")
            {
                OverWriteMessage("pass");
                throw new Exception("Program failed!");
            }
            if (result == "pass")
            {
                OverWriteMessage("fail");
                Thread.Sleep(60000);
            }


        }

        private static void OverWriteMessage(string newMessage)
        {
            using (var streamWriter = new StreamWriter(locationOfFile, append:false))
            {
                streamWriter.Write(newMessage);
                streamWriter.Flush();
            }
        }
            
    }
}
