using System;

namespace LibraryModule
{
    public class SecuritySafetyResult
    {
        public int Safety { get; set; }
        public int SafetyAndSecurityPercentage { get; set; }
        public int SafetyAndSecurityPercentageBase { get; set; }
        public int Security { get; set; }
        public WinCheckSecResultObject WinCheckSecResultObject { get; set; }

        public string ToLongString(AnalysisMode mode)
        {
            return ToString() + WinCheckSecResultObject.ToString(mode);
        }
        public override string ToString()
        {
            return $@"Security and Safety protection score: {SafetyAndSecurityPercentage}/{SafetyAndSecurityPercentageBase}{Environment.NewLine}";
        }
    }
}
