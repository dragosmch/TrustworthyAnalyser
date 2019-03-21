using System;

namespace AvailabilityModule
{
    /// <summary>
    /// Interface for class that runs the checks for Availability
    /// </summary>
    public interface IAvailabilityRunner
    {
        /// <summary>
        ///  Run an executable file multiple times.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="fileLocation">Path to file.</param>
        /// <param name="noOfRuns">How many times to run the executable.</param>
        /// <param name="timeout">How long to let the executable run, in milliseconds.</param>
        /// <param name="runExecutablesInParallel">Run the executable multiple times in parallel if true, sequentially otherwise.</param>
        /// <returns>Number of runs that were successful(no errors occured).</returns>
        int RunExecutableMultipleTimes(IProgress<int> progress, string fileLocation, int noOfRuns, int timeout, bool runExecutablesInParallel);
    }
}