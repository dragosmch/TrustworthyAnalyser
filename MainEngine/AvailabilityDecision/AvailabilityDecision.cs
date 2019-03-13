namespace AvailabilityModule
{
    public static class AvailabilityDecision
    {
        private const int TimeoutInMilliseconds = 1500;
        private static int _noOfTimesToRun;

        public static AvailabilityResult GetAvailabilityDecision(string fileLocation, int mode)
        {
            var noOfSuccessfulRuns = GetAvailabilityNoOfSuccessfulRunsResult(fileLocation, mode);
            return new AvailabilityResult
            {
                Availability = GetAvailabilityResultFromRuns(noOfSuccessfulRuns),
                AvailabilityNoOfRuns = GetAvailabilityNoOfRuns(),
                AvailabilityNoOfSuccessfulRuns = noOfSuccessfulRuns
            };
        }

        private static int GetAvailabilityNoOfSuccessfulRunsResult(string fileLocation, int mode)
        {
            switch (mode)
            {
                case 1:
                    _noOfTimesToRun = 5;
                    break;
                case 2:
                    _noOfTimesToRun = 10;
                    break;
                
                default:
                    _noOfTimesToRun = 3;
                    break;
            }

            return AvailabilityRunner.RunExecutableMultipleTimes(fileLocation, _noOfTimesToRun, TimeoutInMilliseconds);
        }

        private static int GetAvailabilityNoOfRuns()
        {
            return _noOfTimesToRun;
        }

        private static int GetAvailabilityResultFromRuns(int noOfSuccessfulRuns)
        {
            if (noOfSuccessfulRuns < _noOfTimesToRun / 2 + 1)
                return -1;
            if (noOfSuccessfulRuns < _noOfTimesToRun)
                return 0;
            return 1;
        }
    }
}
