using System;

namespace LibraryModule
{
    /// <summary>
    /// Overall result of analysis
    /// </summary>
    public class TrustworthinessResult
    {
        public TrustworthyApplicationLevel TrustworthinessLevel { get; set; }
        public SecuritySafetyResult SecuritySafetyResult { get; set; }
        public AvailabilityResult AvailabilityResult { get; set; }

        public string ToString(AnalysisMode mode)
        {
            return $"{AvailabilityResult}{SecuritySafetyResult.ToLongString(mode)}{Environment.NewLine}Final result: {TrustworthinessLevel}";
        }
    }
}
