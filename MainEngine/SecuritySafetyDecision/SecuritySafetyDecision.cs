
using System;
using LibraryModule;

namespace SecuritySafetyModule
{
    public class SecuritySafetyDecision : ISecuritySafetyDecision
    {
        private readonly ISecuritySafetyRunner _securitySafetyRunner;

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="securitySafetyRunner"></param>
        public SecuritySafetyDecision(ISecuritySafetyRunner securitySafetyRunner)
        {
            _securitySafetyRunner = securitySafetyRunner;
        }

        /// <inheritdoc />
        public SecuritySafetyResult GetSecuritySafetyDecision(IProgress<int> progress, string fileLocation, AnalysisMode mode)
        {
            //progress.Report(1);
            var resultObject = _securitySafetyRunner.GetWinCheckSecResultObject(fileLocation);
            var percentageResult = GetSecuritySafetyScore(resultObject, mode);

            // -1, 0 or 1
            int ternaryResult = GetSecuritySafetyResultFromScore(percentageResult, mode);
           // progress.Report(1);
            return new SecuritySafetyResult
            {
                SafetyScore = ternaryResult,
                SafetyAndSecurityPercentage = percentageResult,
                SafetyAndSecurityPercentageBase = GetSecuritySafetyMaximumScore(mode),
                SecurityScore = ternaryResult,
                WinCheckSecResultObject = resultObject
            };
        }

        /// <summary>
        /// Calculate a ternary result based on a score
        /// </summary>
        /// <param name="score">An int between 0 and 100</param>
        /// <param name="mode">Basic, Medium or Advanced</param>
        /// <returns>-1 for Not Trustworthy, 0 for Inconclusive or 1 for Trustworthy</returns>
        private static int GetSecuritySafetyResultFromScore(int score, AnalysisMode mode)
        {
            switch (mode)
            {
                case AnalysisMode.Basic:
                    if (score > 50) return 1;
                    if (score >= 40) return 0;
                    return -1;
                case AnalysisMode.Medium:
                    if (score > 70) return 1;
                    if (score >= 60) return 0;
                    return -1;
                case AnalysisMode.Advanced:
                    if (score > 90) return 1;
                    if (score >= 70) return 0;
                    return -1;
                default:
                    throw new ArgumentException("Unknown analysis mode!");
            }
        }

        /// <summary>
        /// Determine the maximum score based on analysis mode
        /// </summary>
        /// <param name="mode">Basic, Medium or Advanced</param>
        /// <returns></returns>
        private static int GetSecuritySafetyMaximumScore(AnalysisMode mode)
        {
            switch (mode)
            {
                case AnalysisMode.Basic:
                    return 80;
                case AnalysisMode.Medium:
                    return 92;
                case AnalysisMode.Advanced:
                    return 100;
                default:
                    throw new ArgumentException("Unknown analysis mode!");
            }
        }

        /// <summary>
        /// Compute a score based on analysis mode and WinCheckSec security properties.
        /// </summary>
        /// <param name="resultObject">WinCheckSec properties</param>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>A integer between 0 and 100</returns>
        private static int GetSecuritySafetyScore(WinCheckSecResultObject resultObject, AnalysisMode mode)
        {
            if (resultObject == null) return -1;
            int percentage = 0;
            if (resultObject.Aslr) percentage += 20;
            if (resultObject.DynamicBase) percentage += 20;
            if (resultObject.Nx) percentage += 20;
            if (resultObject.Seh || resultObject.DotNet)
            {
                percentage += 20;
                resultObject.Seh = true;
            }
            if (mode == AnalysisMode.Basic) return percentage;

            if (resultObject.Isolation) percentage += 5;
            if (resultObject.Gs || resultObject.DotNet)
            {
                percentage += 4;
                resultObject.Gs = true;
            }
            if (resultObject.Cfg || resultObject.DotNet)
            {
                percentage += 3;
                resultObject.Cfg = true;
            }
            if (mode == AnalysisMode.Medium) return percentage;

            // if the mode is Advanced, compute final score with all properties
            if (resultObject.HighEntropyVa) percentage += 2;
            if (resultObject.Authenticode) percentage += 2;
            if (resultObject.SafeSeh || resultObject.DotNet)
            {
                percentage += 2;
                resultObject.SafeSeh = true;
            }
            if (resultObject.Rfg || resultObject.DotNet)
            {
                percentage += 1;
                resultObject.Rfg = true;
            }
            if (resultObject.ForceIntegrity) percentage += 1;

            return percentage;
        }
    }
}
