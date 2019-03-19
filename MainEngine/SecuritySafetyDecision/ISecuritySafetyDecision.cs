using LibraryModule;

namespace SecuritySafetyModule
{
    public interface ISecuritySafetyDecision
    {
        SecuritySafetyResult GetSecuritySafetyDecision(string fileLocation, AnalysisMode mode);
    }
}