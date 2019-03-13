using AvailabilityModule;
using SecuritySafetyModule;
using System;

namespace MainEngine
{
    public class TrustworthinessResult
    {
        public TrustworthyApplicationLevel TrustworthinessLevel { get; set; }
        public SecuritySafetyResult SecuritySafetyResult { get; set; }
        public AvailabilityResult AvailabilityResult { get; set; }
        //public int Resilience;
        //public int Reliability;

        public string ToString(int mode)
        {
            return $"{AvailabilityResult}{SecuritySafetyResult.ToLongString(mode)}{Environment.NewLine}Final result: {TrustworthinessLevel}";
        }
    }

    public enum TrustworthyApplicationLevel
    {
        Trustworthy,
        NotTrustworthy,
        Inconclusive
    }
}
