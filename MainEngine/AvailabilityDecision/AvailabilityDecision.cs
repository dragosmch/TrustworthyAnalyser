namespace AvailabilityModule
{
    public class AvailabilityDecision
    {
        private static readonly int TimeoutInMilliseconds = 1500;
        private static readonly int NoOfTimesToRun = 10;


        public static int GetAvailabilityNoOfSuccessfulRunsResult(string fileLocation)
        {
            return AvailabilityRunner.RunExecutableMultipleTimes(fileLocation, NoOfTimesToRun, TimeoutInMilliseconds);
        }

        public static int getAvailabilityNoOfRuns()
        {
            return NoOfTimesToRun;
        }

        public static int GetAvailabilityResultFromRuns(int noOfSucessfulRuns)
        {
            if (noOfSucessfulRuns < NoOfTimesToRun / 2 + 1)
                return -1;
            if (noOfSucessfulRuns < NoOfTimesToRun)
                return 0;
            return 1;
        }
    }
}
