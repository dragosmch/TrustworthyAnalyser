using System;
using System.Threading;
using AvailabilityModule;
using SecuritySafetyModule;

namespace MainEngine
{
    public static class TrustworthyAnalyzer
    {
        private static readonly string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\AppThatFailsEveryOtherTime.exe";
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

        public static TrustworthinessResult ReturnResults(string givenFile, int mode)
        {
            TrustworthinessResult.clear();
            string fileToAnalyse = givenFile ?? TestFileLocation;
            GetAvailabilityDecision(fileToAnalyse, mode);
            GetSecuritySafetyDecision(fileToAnalyse, mode);
            int totalResult = 
                TrustworthinessResult.AvailabilityResult.Availability 
                    + TrustworthinessResult.SecuritySafetyResult.Safety 
                    + TrustworthinessResult.SecuritySafetyResult.Security;
            if (totalResult >= 2)
                TrustworthinessResult.TrustworthinessLevel = TrustworthyApplicationLevel.Trustworthy;
            else if (totalResult <= 0)
                TrustworthinessResult.TrustworthinessLevel = TrustworthyApplicationLevel.NotTrustworthy;
            else
                TrustworthinessResult.TrustworthinessLevel = TrustworthyApplicationLevel.Inconclusive;
            return TrustworthinessResult;
        }

        private static void GetAvailabilityDecision(string fileToAnalyse, int mode)
        {
            TrustworthinessResult.AvailabilityResult = AvailabilityDecision.GetAvailabilityDecision(fileToAnalyse, mode);
        }
        private static void GetSecuritySafetyDecision(string fileToAnalyse, int mode)
        {
            TrustworthinessResult.SecuritySafetyResult = SecuritySafetyDecision.GetSecuritySafetyDecision(fileToAnalyse, mode);
        }

        private static void ConsoleWriteLineWithColour(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
