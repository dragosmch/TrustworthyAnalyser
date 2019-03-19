using System;

namespace LibraryModule
{
    public class AvailabilityResult
    {
        public int AvailabilityScore { get; set; }
        public int AvailabilityNoOfRuns { get; set; }
        public int AvailabilityNoOfSuccessfulRuns { get; set; }

        public override string ToString()
        {
            return $"Successful availability runs: {AvailabilityNoOfSuccessfulRuns}/{AvailabilityNoOfRuns}{Environment.NewLine}";
        }
    }
}
