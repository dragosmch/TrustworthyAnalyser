using System;

namespace AvailabilityModule
{
    public class AvailabilityResult
    {
        public int Availability;
        public int AvailabilityNoOfRuns;
        public int AvailabilityNoOfSuccessfulRuns;

        public override string ToString()
        {
            return $"Successful availability runs: {AvailabilityNoOfSuccessfulRuns}/{AvailabilityNoOfRuns}{Environment.NewLine}";
        }
    }
}
