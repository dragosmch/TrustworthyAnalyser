using System;
using LibraryModule;

namespace SecuritySafetyModule
{
    /// <summary>
    /// Interface for class that determines the outcome of the Security and Safety testing.
    /// </summary>
    public interface ISecuritySafetyDecision
    {
        /// <summary>
        /// Gets the result of the Security and Safety testing.
        /// </summary>
        /// <param name="progress">Object to indicate to the UI the overall progress of the execution</param>
        /// <param name="fileLocation">Path to the executable.</param>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>An SecuritySafetyResult object containing detailed information.</returns>
        SecuritySafetyResult GetSecuritySafetyDecision(IProgress<int> progress, string fileLocation, AnalysisMode mode);
    }
}