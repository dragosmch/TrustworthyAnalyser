using AvailabilityModule;
using SecuritySafetyModule;
using System;

namespace MainEngine
{
    public class TrustworthinessResult
    {
        public TrustworthyApplicationLevel TrustworthinessLevel;
        public SecuritySafetyResult SecuritySafetyResult;
        public AvailabilityResult AvailabilityResult;
        //public int Resilience;
        //public int Reliability;

        public string ToString(int mode)
        {
            return $"{AvailabilityResult}{SecuritySafetyResult.ToLongString(mode)}{Environment.NewLine}Final result: {TrustworthinessLevel}";
        }

        // this should be deleted, it is nasty
        public void Clear()
        {
            TrustworthinessLevel = TrustworthyApplicationLevel.NotSet;
        }
    }

    public enum TrustworthyApplicationLevel
    {
        Trustworthy,
        NotTrustworthy,
        Inconclusive,
        NotSet
    }
}
