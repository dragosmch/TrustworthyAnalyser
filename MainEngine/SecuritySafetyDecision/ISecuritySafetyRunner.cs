using LibraryModule;

namespace SecuritySafetyModule
{
    public interface ISecuritySafetyRunner
    {
        WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation);
    }
}