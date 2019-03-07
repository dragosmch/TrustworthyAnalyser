namespace AvailabilityModule
{
    public static class AvailabilityDecision
    {
        private const int TimeoutInMilliseconds = 1500;
        private static int NoOfTimesToRun;

        public static AvailabilityResult GetAvailabilityDecision(string fileLocation, int mode)
        {
            int noOfSuccessfulRuns = GetAvailabilityNoOfSuccessfulRunsResult(fileLocation, mode);
            return new AvailabilityResult
            {
                Availability = GetAvailabilityResultFromRuns(noOfSuccessfulRuns),
                AvailabilityNoOfRuns = getAvailabilityNoOfRuns(),
                AvailabilityNoOfSuccessfulRuns = noOfSuccessfulRuns
            };
        }

        private static int GetAvailabilityNoOfSuccessfulRunsResult(string fileLocation, int mode)
        {
            switch (mode)
            {
                case 1:
                    NoOfTimesToRun = 5;
                    break;
                case 2:
                    NoOfTimesToRun = 10;
                    break;
                default:
                    NoOfTimesToRun = 3;
                    break;
            }

            return AvailabilityRunner.RunExecutableMultipleTimes(fileLocation, NoOfTimesToRun, TimeoutInMilliseconds);
        }

        private static int getAvailabilityNoOfRuns()
        {
            return NoOfTimesToRun;
        }

        private static int GetAvailabilityResultFromRuns(int noOfSuccessfulRuns)
        {
            if (noOfSuccessfulRuns < NoOfTimesToRun / 2 + 1)
                return -1;
            if (noOfSuccessfulRuns < NoOfTimesToRun)
                return 0;
            return 1;
        }
    }
}
