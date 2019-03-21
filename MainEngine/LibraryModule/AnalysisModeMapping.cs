using System;

namespace LibraryModule
{
    /// <summary>
    /// Class to provide mappings and general settings for the properties being tested
    /// by the tool
    /// </summary>
    public static class AnalysisModeMapping
    {
        /// <summary>
        /// Map between a mode and number of total availability executions/runs
        /// </summary>
        /// <param name="mode">Basic, Medium or Advanced</param>
        /// <returns></returns>
        public static int GetAvailabilityMaxRuns(AnalysisMode mode)
        {
            switch (mode)
            {
                case AnalysisMode.Basic:
                    return 3;
                case AnalysisMode.Medium:
                    return 5;
                case AnalysisMode.Advanced:
                    return 10;
                default:
                    throw new ArgumentException("Unknown analysis mode!");
            }
        }
    }
}