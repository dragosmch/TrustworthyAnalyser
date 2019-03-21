using System;
using System.IO;
using AvailabilityModule;
using LibraryModule;
using SecuritySafetyModule;

namespace MainEngine
{
    /// <summary>
    /// Main analyser engine for determining trustworthiness
    /// </summary>
    public class TrustworthyAnalyser
    {        
        private readonly TrustworthinessResult _trustworthinessResult = new TrustworthinessResult();
        private readonly IAvailabilityDecision _availabilityDecision = new AvailabilityDecision(new AvailabilityRunner());
        private readonly ISecuritySafetyDecision _securitySafetyDecision = new SecuritySafetyDecision(new SecuritySafetyRunner());

        public TrustworthinessResult ReturnResults(IProgress<int> progress, string pathToFile, AnalysisMode mode)
        {
            // Validate user input
            if (!File.Exists(pathToFile) || !pathToFile.Contains(".exe")) return null;

            GetAvailabilityDecision(progress, pathToFile, mode);
            GetSecuritySafetyDecision(progress, pathToFile, mode);
            int totalResult = 
                _trustworthinessResult.AvailabilityResult.AvailabilityScore 
                    + _trustworthinessResult.SecuritySafetyResult.SafetyScore 
                    + _trustworthinessResult.SecuritySafetyResult.SecurityScore;
            _trustworthinessResult.TrustworthinessLevel = GetApplicationLevelFromScore(totalResult);
            return _trustworthinessResult;
        }

        /// <summary>
        /// Determine application trustworthiness from the ternary score
        /// </summary>
        /// <param name="score">An int representing the sum of ternary scores</param>
        /// <returns>A final application level</returns>
        private static TrustworthyApplicationLevel GetApplicationLevelFromScore(int score)
        {
            if (score >= 2)
                return TrustworthyApplicationLevel.Trustworthy;
            if (score <= 0)
                return TrustworthyApplicationLevel.NotTrustworthy;
            return TrustworthyApplicationLevel.Inconclusive;
        }

        /// <summary>
        /// Gets the result of the availability testing.
        /// </summary>
        /// <param name="progress">Object to indicate to the UI the overall progress of the execution</param>
        /// <param name="fileToAnalyse">Path to the executable.</param>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>An AvailabilityResult object containing detailed information.</returns>
        private void GetAvailabilityDecision(IProgress<int> progress, string fileToAnalyse, AnalysisMode mode)
        {
            _trustworthinessResult.AvailabilityResult = _availabilityDecision.GetAvailabilityDecision(progress, fileToAnalyse, mode);
        }

        /// <summary>
        /// Gets the result of the Security and Safety testing.
        /// </summary>
        /// <param name="progress">Object to indicate to the UI the overall progress of the execution</param>
        /// <param name="fileToAnalyse">Path to the executable.</param>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>An SecuritySafetyResult object containing detailed information.</returns>
        private void GetSecuritySafetyDecision(IProgress<int> progress, string fileToAnalyse, AnalysisMode mode)
        {
            _trustworthinessResult.SecuritySafetyResult = _securitySafetyDecision.GetSecuritySafetyDecision(progress, fileToAnalyse, mode);
        }
    }
}
