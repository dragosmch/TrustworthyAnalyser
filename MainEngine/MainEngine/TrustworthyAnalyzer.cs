using System;
using System.IO;
using AvailabilityModule;
using LibraryModule;
using SecuritySafetyModule;

namespace MainEngine
{
    public class TrustworthyAnalyzer
    {
        
        private readonly TrustworthinessResult _trustworthinessResult = new TrustworthinessResult();
        private readonly IAvailabilityDecision _availabilityDecision = new AvailabilityDecision(new AvailabilityRunner());
        private readonly ISecuritySafetyDecision _securitySafetyDecision = new SecuritySafetyDecision(new SecuritySafetyRunner());

        public TrustworthinessResult ReturnResults(IProgress<int> progress, string pathToFile, AnalysisMode mode)
        {
            if (!File.Exists(pathToFile) || !pathToFile.Contains(".exe")) return null;

            string fileToAnalyse = pathToFile;
            GetAvailabilityDecision(progress, fileToAnalyse, mode);
            GetSecuritySafetyDecision(progress, fileToAnalyse, mode);
            int totalResult = 
                _trustworthinessResult.AvailabilityResult.AvailabilityScore 
                    + _trustworthinessResult.SecuritySafetyResult.SafetyScore 
                    + _trustworthinessResult.SecuritySafetyResult.SecurityScore;
            _trustworthinessResult.TrustworthinessLevel = GetApplicationLevelFromScore(totalResult);
            return _trustworthinessResult;
        }

        private static TrustworthyApplicationLevel GetApplicationLevelFromScore(int score)
        {
            if (score >= 2)
                return TrustworthyApplicationLevel.Trustworthy;
            if (score <= 0)
                return TrustworthyApplicationLevel.NotTrustworthy;
            return TrustworthyApplicationLevel.Inconclusive;
        }

        private void GetAvailabilityDecision(IProgress<int> progress, string fileToAnalyse, AnalysisMode mode)
        {
            _trustworthinessResult.AvailabilityResult = _availabilityDecision.GetAvailabilityDecision(progress, fileToAnalyse, mode);
        }
        private void GetSecuritySafetyDecision(IProgress<int> progress, string fileToAnalyse, AnalysisMode mode)
        {
            _trustworthinessResult.SecuritySafetyResult = _securitySafetyDecision.GetSecuritySafetyDecision(progress, fileToAnalyse, mode);
        }
    }
}
