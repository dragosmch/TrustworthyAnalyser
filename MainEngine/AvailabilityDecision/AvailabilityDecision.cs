namespace AvailabilityModule
{
    public class AvailabilityDecision
    {
        private static readonly int TimeoutInMilliseconds = 1500;
        private static int NoOfTimesToRun;


        public static int GetAvailabilityNoOfSuccessfulRunsResult(string fileLocation, int mode)
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
