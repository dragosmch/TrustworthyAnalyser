using System;
using System.Threading;
using AvailabilityModule;
using SecuritySafetyModule;

namespace MainEngine
{
    public static class TrustworthyAnalyzer
    {
        
        private static readonly TrustworthinessResult TrustworthinessResult = new TrustworthinessResult();

        public static TrustworthinessResult ReturnResults(string givenFile, int mode)
        {
            TrustworthinessResult.Clear();
            string fileToAnalyse = givenFile;
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
