using System;

namespace AvailabilityModule
{
    public interface IAvailabilityRunner
    {
        int RunExecutableMultipleTimes(IProgress<int> progress, string fileLocation, int noOfRuns, int timeout, bool runExecutablesInParallel);
    }
}