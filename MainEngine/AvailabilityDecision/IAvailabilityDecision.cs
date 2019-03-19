using LibraryModule;

namespace AvailabilityModule
{
    public interface IAvailabilityDecision
    {
        AvailabilityResult GetAvailabilityDecision(string fileLocation, AnalysisMode mode);
    }
}