using System;

namespace LibraryModule
{
    /// <summary>
    /// Result of the Safety and Security checks.
    /// </summary>
    public class SecuritySafetyResult
    {
        public int SafetyScore { get; set; }
        public int SafetyAndSecurityPercentage { get; set; }
        public int SafetyAndSecurityPercentageBase { get; set; }
        public int SecurityScore { get; set; }
        public WinCheckSecResultObject WinCheckSecResultObject { get; set; }

        /// <summary>
        /// Representation used in creating a report file.
        /// </summary>
        /// <param name="mode">Basic, Medium or Advanced</param>
        /// <returns></returns>
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
