using System;
using AvailabilityDecision;

namespace MainEngine
{
    class Program
    {
        private static string TestFileLocation = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\PhotoScapeSetup.exe";

   
        static void Main(string[] args)
        {
            string fileToAnalyse = args.Length == 1 ? args[0] : TestFileLocation;
            int result = AvailabilityRunner.RunExecutable(fileToAnalyse);
            //GetSecuritySafetyDecision(fileToAnalyse);

        }

        private static void GetSecuritySafetyDecision(string fileToAnalyse)
        {
            int securitySafetyPercentage = SecuritySafetyDecision.SecuritySafetyDecision.GetSecuritySafetyPercentage(fileToAnalyse);
            Console.WriteLine(securitySafetyPercentage);
            if (securitySafetyPercentage >= 80)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Trustworthy!");
                Console.ResetColor();
            }
            else if (securitySafetyPercentage <= 40)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("NOT Trustworthy!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Inconclusive!");
            }
        }
    }
}
