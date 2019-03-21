using LibraryModule;

namespace SecuritySafetyModule
{
    /// <summary>
    /// Interface for class that checks for Security and Safety properties.
    /// </summary>
    public interface ISecuritySafetyRunner
    {
        /// <summary>
        /// Get object containing properties obtained from WinCheckSec
        /// </summary>
        /// <param name="fileLocation">Path to executable file.</param>
        /// <returns></returns>
        WinCheckSecResultObject GetWinCheckSecResultObject(string fileLocation);
    }
}