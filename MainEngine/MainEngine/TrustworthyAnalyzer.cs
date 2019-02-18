using System;
using System.Threading;
using AvailabilityModule;
using SecuritySafetyModule;

namespace MainEngine
{
    public static class TrustworthyAnalyzer
    {
        //private static string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";
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

        public static TrustworthinessResult ReturnResults(string givenFile)
        {
            TrustworthinessResult.clear();
            string fileToAnalyse = givenFile ?? TestFileLocation;
            GetAvailabilityDecision(fileToAnalyse);
            GetSecuritySafetyDecision(fileToAnalyse);
            int totalResult = TrustworthinessResult.Availability + TrustworthinessResult.Safety + TrustworthinessResult.Security;
            if (totalResult >= 2)
                TrustworthinessResult.TrustworthinessLevel = TrustworthyApplicationLevel.Trustworthy;
            else if (totalResult <= 0)
                TrustworthinessResult.TrustworthinessLevel = TrustworthyApplicationLevel.NotTrustworthy;
            else
                TrustworthinessResult.TrustworthinessLevel = TrustworthyApplicationLevel.Inconclusive;
            return TrustworthinessResult;
        }

        private static void GetAvailabilityDecision(string fileToAnalyse)
        {
            int noOfSuccessfulRuns = AvailabilityDecision.GetAvailabilityNoOfSuccessfulRunsResult(fileToAnalyse);
            TrustworthinessResult.Availability = AvailabilityDecision.GetAvailabilityResultFromRuns(noOfSuccessfulRuns);
            TrustworthinessResult.AvailabilityNoOfRuns = AvailabilityDecision.getAvailabilityNoOfRuns();
            TrustworthinessResult.AvailabilityNoOfSuccessfulRuns = noOfSuccessfulRuns;
        }
        private static void GetSecuritySafetyDecision(string fileToAnalyse)
        {
            int percentageResult = SecuritySafetyDecision.GetSecuritySafetyPercentage(fileToAnalyse);
            TrustworthinessResult.SafetyAndSecurityPercentage = percentageResult;
            TrustworthinessResult.Security = SecuritySafetyDecision.GetSecuritySafetyResultFromPercentage(percentageResult);
            TrustworthinessResult.Safety = SecuritySafetyDecision.GetSecuritySafetyResultFromPercentage(percentageResult);
        }

        private static void ConsoleWriteLineWithColour(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
