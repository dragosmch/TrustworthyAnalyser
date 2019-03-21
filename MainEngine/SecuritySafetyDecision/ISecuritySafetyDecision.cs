using System;
using LibraryModule;

namespace SecuritySafetyModule
{
    public interface ISecuritySafetyDecision
    {
        SecuritySafetyResult GetSecuritySafetyDecision(IProgress<int> progress, string fileLocation, AnalysisMode mode);
    }
}