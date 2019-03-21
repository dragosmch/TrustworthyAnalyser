using System;
using LibraryModule;

namespace AvailabilityModule
{
    /// <summary>
    /// Class that decides the outcome of the availability tests.
    /// </summary>
    public class AvailabilityDecision : IAvailabilityDecision
    {
        // Time for how long to let an application run before killing the process.
        private const int TimeoutInMilliseconds = 1500;
        private readonly IAvailabilityRunner _availabilityRunner;
        private int _noOfTimesToRun;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="availabilityRunner"></param>
        public AvailabilityDecision(IAvailabilityRunner availabilityRunner)
        {
            _availabilityRunner = availabilityRunner;
        }

        /// <inheritdoc />
        public AvailabilityResult GetAvailabilityDecision(IProgress<int> progress, string fileLocation, AnalysisMode mode)
        {
            var noOfSuccessfulRuns = GetAvailabilityNoOfSuccessfulRunsResult(progress, fileLocation, mode);
            return new AvailabilityResult
            {
                AvailabilityScore = GetAvailabilityResultFromRuns(noOfSuccessfulRuns),
                AvailabilityNoOfRuns = GetAvailabilityTotalNoOfRuns(),
                AvailabilityNoOfSuccessfulRuns = noOfSuccessfulRuns
            };
        }

        /// <summary>
        ///  Method to test the availability of an application
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="fileLocation">Path to the executable.</param>
        /// <param name="mode">Analysis mode.</param>
        /// <returns>An int representing number of executions without errors/exceptions</returns>
        private int GetAvailabilityNoOfSuccessfulRunsResult(IProgress<int> progress, string fileLocation, AnalysisMode mode)
        {
            _noOfTimesToRun = AnalysisModeMapping.GetAvailabilityMaxRuns(mode);
            return _availabilityRunner.RunExecutableMultipleTimes(progress, fileLocation, _noOfTimesToRun, TimeoutInMilliseconds, true);
        }

        /// <summary>
        /// Get total number of times the application is tested
        /// </summary>
        /// <returns>Total number of runs</returns>
        private int GetAvailabilityTotalNoOfRuns()
        {
            return _noOfTimesToRun;
        }

        /// <summary>
        /// Get number of times the application run successfully 
        /// </summary>
        /// <param name="noOfSuccessfulRuns">Number of successful tests</param>
        /// <returns>1 for Trustworthy, 0 for Inconclusive, -1 for Not Trustworthy</returns>
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
