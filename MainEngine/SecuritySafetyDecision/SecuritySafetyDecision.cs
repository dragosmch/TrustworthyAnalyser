
using System;
using LibraryModule;

namespace SecuritySafetyModule
{
    public class SecuritySafetyDecision : ISecuritySafetyDecision
    {
        private readonly ISecuritySafetyRunner _securitySafetyRunner;

        public SecuritySafetyDecision(ISecuritySafetyRunner securitySafetyRunner)
        {
            _securitySafetyRunner = securitySafetyRunner;
        }

        public SecuritySafetyResult GetSecuritySafetyDecision(string fileLocation, AnalysisMode mode)
        {
            var resultObject = _securitySafetyRunner.GetWinCheckSecResultObject(fileLocation);
            var percentageResult = GetSecuritySafetyPercentage(resultObject, mode);
            // -1, 0 or 1
            int ternaryResult = GetSecuritySafetyResultFromPercentage(percentageResult, mode);
            return new SecuritySafetyResult
            {
                SafetyScore = ternaryResult,
                SafetyAndSecurityPercentage = percentageResult,
                SafetyAndSecurityPercentageBase = GetSecuritySafetyMaxPercentage(mode),
                SecurityScore = ternaryResult,
                WinCheckSecResultObject = resultObject
            };
        }

        private int GetSecuritySafetyResultFromPercentage(int percentage, AnalysisMode mode)
        {
            switch (mode)
            {
                case AnalysisMode.Basic:
                    if (percentage > 50) return 1;
                    if (percentage >= 40) return 0;
                    return -1;
                case AnalysisMode.Medium:
                    if (percentage > 70) return 1;
                    if (percentage >= 60) return 0;
                    return -1;
                case AnalysisMode.Advanced:
                    if (percentage > 90) return 1;
                    if (percentage >= 70) return 0;
                    return -1;
                default:
                    throw new Exception("Unknown analysis mode!");
            }
        }

        private int GetSecuritySafetyMaxPercentage(AnalysisMode mode)
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
                    throw new Exception("Unknown analysis mode!");
            }
        }
        

        private int GetSecuritySafetyPercentage(WinCheckSecResultObject resultObject, AnalysisMode mode)
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

            // if the mode is Advanced, compute final percentage with all properties
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
