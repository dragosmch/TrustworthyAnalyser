namespace MainEngine
{
    public class TrustworthinessResult
    {
        public int Safety;
        public int SafetyAndSecurityPercentage;
        public int SafetyAndSecurityPercentageBase = 99;
        public int Security;
        public int Availability;
        public int AvailabilityNoOfRuns;
        public int AvailabilityNoOfSuccessfulRuns;
        public TrustworthyApplicationLevel TrustworthinessLevel;
        //public int Resilience;
        //public int Reliability;

        public void clear()
        {
            Safety = 0;
            SafetyAndSecurityPercentage = 0;
            Security = 0;
            Availability = 0;
            AvailabilityNoOfRuns = 0;
            AvailabilityNoOfSuccessfulRuns = 0;
        }
    }

    public enum TrustworthyApplicationLevel
    {
        Trustworthy,
        NotTrustworthy,
        Inconclusive
    }
}
