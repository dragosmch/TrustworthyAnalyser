namespace AvailabilityModule
{
    public class AvailabilityDecision
    {
        private static readonly int TimeoutInMilliseconds = 500;
        private static readonly int NoOfTimesToRun = 5;

        public static int GetAvailabilityResult(string fileLocation)
        {
            int resultOfRuns = AvailabilityRunner.RunExecutableMultipleTimes(fileLocation, NoOfTimesToRun, TimeoutInMilliseconds);
            if (resultOfRuns < NoOfTimesToRun / 2 + 1)
                return -1;
            else if (resultOfRuns < NoOfTimesToRun)
                return 0;
            return 1;
        }
    }
}
