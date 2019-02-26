using System;

namespace SecuritySafetyModule
{
    public class SecuritySafetyResult
    {
        public int Safety;
        public int SafetyAndSecurityPercentage;
        public int SafetyAndSecurityPercentageBase;
        public int Security;
        public WinCheckSecResultObject winCheckSecResultObject;

        public string ToLongString(int mode)
        {
            return ToString() + winCheckSecResultObject.ToString(mode);
        }
        public override string ToString()
        {
            return $@"Security and Safety protection score: {SafetyAndSecurityPercentage}/{SafetyAndSecurityPercentageBase}{Environment.NewLine}";
        }
    }
}
