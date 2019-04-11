using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvailabilityModule;
using LibraryModule;

namespace EvaluationModule
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var analysisMode = AnalysisMode.Advanced;
            var numberOfExecutions = 100;
            string fileLocation =
                @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles\AppThatFailsEveryThreeOutOfFour.exe";
            int sum = 0;
            IAvailabilityRunner availabilityRunner = new AvailabilityRunner();
            AvailabilityDecision availabilityDecision = new AvailabilityDecision(availabilityRunner);
            for (int i = 0; i < numberOfExecutions; i++)
            {
                sum += availabilityDecision
                    .GetAvailabilityDecision(new Progress<int>(), fileLocation, analysisMode)
                    .AvailabilityNoOfSuccessfulRuns;
            }
            Console.WriteLine($"Total sum {sum}");
            var dividedByEx = sum * 1.0 / numberOfExecutions;
            Console.WriteLine($"Divided by executions {dividedByEx}");
            Console.WriteLine($"Success percentage {dividedByEx / LibraryModule.AnalysisModeMapping.GetAvailabilityMaxRuns(analysisMode) * 100}");


        }
    }
}
