﻿using System;
using LibraryModule;

namespace AvailabilityModule
{
    /// <summary>
    /// Interface for class that determines the outcome of the Availability testing
    /// </summary>
    public interface IAvailabilityDecision
    {
        /// <summary>
        /// Gets the result of the availability testing.
        /// </summary>
        /// <param name="progress">Object to indicate to the UI the overall progress of the execution</param>
        /// <param name="fileLocation">Path to the executable.</param>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>An AvailabilityResult object containing detailed information.</returns>
        AvailabilityResult GetAvailabilityDecision(IProgress<int> progress, string fileLocation, AnalysisMode mode);
    }
}