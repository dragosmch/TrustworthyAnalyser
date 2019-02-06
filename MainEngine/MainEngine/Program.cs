using System;
using System.Diagnostics;
using System.Threading;
using AvailabilityModule;
using SecuritySafetyModule;

namespace MainEngine
{
    class Program
    {
        //private static string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
        private static string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\AppThatFailsEveryOtherTime.exe";
        public static TrustworthinessResult TrustworthinessResult = new TrustworthinessResult();
   
        static void Main(string[] args)
        {
            Stopwatch clock = new Stopwatch();
            clock.Start();
            string fileToAnalyse = args.Length == 1 ? args[0] : TestFileLocation;
            GetAvailabilityDecision(fileToAnalyse);
            GetSecuritySafetyDecision(fileToAnalyse);
            OutputResults();
            long timePassed = clock.ElapsedMilliseconds / 1000;
        }

        private static void OutputResults()
        {
            int totalResult = TrustworthinessResult.Availability + TrustworthinessResult.Safety + TrustworthinessResult.Security;
            if (totalResult >= 2)
                ConsoleWriteLineWithColour("Trustworthy", ConsoleColor.Green);
            else if (totalResult == 0)
                ConsoleWriteLineWithColour("Not Trustworthy", ConsoleColor.Red);
            else
                ConsoleWriteLineWithColour("Inconclusive result", ConsoleColor.Yellow);
            Thread.Sleep(5000);
        }

        private static void GetAvailabilityDecision(string fileToAnalyse)
        {
            TrustworthinessResult.Availability = AvailabilityDecision.GetAvailabilityResult(fileToAnalyse);
        }
        private static void GetSecuritySafetyDecision(string fileToAnalyse)
        {
            int decision = SecuritySafetyDecision.GetSecuritySafetyResult(fileToAnalyse);
            TrustworthinessResult.Security = decision;
            TrustworthinessResult.Safety = decision;
        }

        private static void ConsoleWriteLineWithColour(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
