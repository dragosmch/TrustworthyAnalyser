using System;
using LibraryModule;

namespace AvailabilityModule
{
    /// <summary>
    /// Class that decides the outcome of the availability tests
    /// </summary>
    public class AvailabilityDecision : IAvailabilityDecision
    {
        // Time for how long to let an application run before killing the process
        private const int TimeoutInMilliseconds = 1500;
        private readonly IAvailabilityRunner _availabilityRunner;
        private int _noOfTimesToRun;

        public AvailabilityDecision(IAvailabilityRunner availabilityRunner)
        {
            _availabilityRunner = availabilityRunner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="mode"></param>
        /// <returns>An AvailabilityResult object containing detailed information.</returns>
        public AvailabilityResult GetAvailabilityDecision(string fileLocation, AnalysisMode mode)
        {
            var noOfSuccessfulRuns = GetAvailabilityNoOfSuccessfulRunsResult(fileLocation, mode);
            return new AvailabilityResult
            {
                AvailabilityScore = GetAvailabilityResultFromRuns(noOfSuccessfulRuns),
                AvailabilityNoOfRuns = GetAvailabilityNoOfRuns(),
                AvailabilityNoOfSuccessfulRuns = noOfSuccessfulRuns
            };
        }

        /// <summary>
        ///  Method to test the availability of an application
        /// </summary>
        /// <param name="fileLocation">Path to file.</param>
        /// <param name="mode">Analysis mode.</param>
        /// <returns>An int representing number of executions without errors/exceptions</returns>
        private int GetAvailabilityNoOfSuccessfulRunsResult(string fileLocation, AnalysisMode mode)
        {
            switch (mode)
            {
                case AnalysisMode.Basic:
                    _noOfTimesToRun = 3;
                    break;
                case AnalysisMode.Medium:
                    _noOfTimesToRun = 5;
                    break;
                case AnalysisMode.Advanced:
                    _noOfTimesToRun = 10;
                    break;                
                default:
                    throw new Exception("Unknown analysis mode!");
            }

            return _availabilityRunner.RunExecutableMultipleTimes(fileLocation, _noOfTimesToRun, TimeoutInMilliseconds, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetAvailabilityNoOfRuns()
        {
            return _noOfTimesToRun;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noOfSuccessfulRuns"></param>
        /// <returns></returns>
        private int GetAvailabilityResultFromRuns(int noOfSuccessfulRuns)
        {
            if (noOfSuccessfulRuns < _noOfTimesToRun / 2 + 1)
                return -1;
            if (noOfSuccessfulRuns < _noOfTimesToRun)
                return 0;
            return 1;
        }
    }
}
