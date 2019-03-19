namespace AvailabilityModule
{
    public interface IAvailabilityRunner
    {
        int RunExecutableMultipleTimes(string fileLocation, int noOfRuns, int timeout, bool runExecutablesInParallel);
    }
}