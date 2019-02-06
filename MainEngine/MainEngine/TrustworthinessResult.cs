namespace MainEngine
{
    class TrustworthinessResult
    {
        public int Safety;
        public int Security;
        public int Availability;
        //public int Resilience;
        //public int Reliability;
    }

    public enum TrustworthyApplicationLevel
    {
        Trustworthy,
        NotTrustworthy,
        Inconclusive
    }
}
