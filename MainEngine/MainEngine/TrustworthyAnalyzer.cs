using System;
using System.Threading;
using AvailabilityModule;
using SecuritySafetyModule;

namespace MainEngine
{
    public static class TrustworthyAnalyzer
    {
        //private static string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
        private static string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\AppThatFailsEveryOtherTime.exe";
        private static TrustworthinessResult TrustworthinessResult = new TrustworthinessResult();
   
        //static void Main(string[] args)
        //{
        //    Stopwatch clock = new Stopwatch();
        //    clock.Start();
        //    string fileToAnalyse = args.Length == 1 ? args[0] : TestFileLocation;
        //    GetAvailabilityDecision(fileToAnalyse);
        //    GetSecuritySafetyDecision(fileToAnalyse);
        //    //OutputResults();
        //    long timePassed = clock.ElapsedMilliseconds / 1000;
        //}

        private static void OutputResults(int totalResult)
        {
            if (totalResult >= 2)
                ConsoleWriteLineWithColour("Trustworthy", ConsoleColor.Green);
            else if (totalResult == 0)
                ConsoleWriteLineWithColour("Not Trustworthy", ConsoleColor.Red);
            else
                ConsoleWriteLineWithColour("Inconclusive result", ConsoleColor.Yellow);
            Thread.Sleep(5000);
        }

        public static TrustworthyApplicationLevel ReturnResults(string givenFile)
        {
            string fileToAnalyse = givenFile ?? TestFileLocation;
            GetAvailabilityDecision(fileToAnalyse);
            GetSecuritySafetyDecision(fileToAnalyse);
            int totalResult = TrustworthinessResult.Availability + TrustworthinessResult.Safety + TrustworthinessResult.Security;
            if (totalResult >= 2)
                return TrustworthyApplicationLevel.Trustworthy;
            else if (totalResult == 0)
                return TrustworthyApplicationLevel.NotTrustworthy;
            else
                return TrustworthyApplicationLevel.Inconclusive;
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
