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

        public TrustworthinessResult ReturnResults(string pathToFile, AnalysisMode mode)
        {
            if (!File.Exists(pathToFile) || !pathToFile.Contains(".exe")) return null;

            string fileToAnalyse = pathToFile;
            GetAvailabilityDecision(fileToAnalyse, mode);
            GetSecuritySafetyDecision(fileToAnalyse, mode);
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

        private void GetAvailabilityDecision(string fileToAnalyse, AnalysisMode mode)
        {
            _trustworthinessResult.AvailabilityResult = _availabilityDecision.GetAvailabilityDecision(fileToAnalyse, mode);
        }
        private void GetSecuritySafetyDecision(string fileToAnalyse, AnalysisMode mode)
        {
            _trustworthinessResult.SecuritySafetyResult = _securitySafetyDecision.GetSecuritySafetyDecision(fileToAnalyse, mode);
        }
    }
}
