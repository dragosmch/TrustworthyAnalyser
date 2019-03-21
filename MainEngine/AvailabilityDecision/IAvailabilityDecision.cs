using System;
using LibraryModule;

namespace AvailabilityModule
{
    public interface IAvailabilityDecision
    {
        AvailabilityResult GetAvailabilityDecision(IProgress<int> progress, string fileLocation, AnalysisMode mode);
    }
}