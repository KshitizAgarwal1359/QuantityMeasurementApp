// -----------------------------------------------------------------------
// <copyright file="QuantityMeasurementService.cs" company="QuantityMeasurement">
//     QuantityMeasurementApp - Service layer for quantity operations
// </copyright>
// <summary>
//     This service class provides methods for comparing and validating
//     quantity measurements. It follows the Single Responsibility Principle
//     by handling only measurement comparison logic.
// </summary>
// -----------------------------------------------------------------------

using QuantityMeasurement.Models;

namespace QuantityMeasurement.Services
{
    /// <summary>
    /// Service class responsible for performing quantity measurement operations
    /// such as comparing feet values for equality.
    /// Follows the Single Responsibility Principle (SRP) from SOLID.
    /// </summary>
    public class QuantityMeasurementService
    {
        /// <summary>
        /// Compares two Feet measurement objects for equality.
        /// Delegates the equality logic to the Feet class's Equals method.
        /// </summary>
        /// <param name="firstMeasurement">The first Feet measurement to compare.</param>
        /// <param name="secondMeasurement">The second Feet measurement to compare.</param>
        /// <returns>
        /// True if both measurements are equal; false otherwise.
        /// Returns false if either measurement is null.
        /// </returns>
        public bool CompareFeetMeasurements(Feet? firstMeasurement, Feet? secondMeasurement)
        {
            // Handle null cases: if firstMeasurement is null, they cannot be equal
            if (firstMeasurement is null)
            {
                return false;
            }

            // Delegate to the Feet class's Equals method for value-based comparison
            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}
