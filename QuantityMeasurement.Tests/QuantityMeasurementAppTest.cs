// -----------------------------------------------------------------------
// <copyright file="QuantityMeasurementAppTest.cs" company="QuantityMeasurement">
//     QuantityMeasurementApp - UC1: Feet equality test cases
// </copyright>
// <summary>
//     Unit tests for verifying the Feet measurement equality logic.
//     Tests cover the equality contract: reflexive, symmetric, null handling,
//     type safety, same value, different value, and same reference scenarios.
// </summary>
// -----------------------------------------------------------------------

using QuantityMeasurement.Models;

namespace QuantityMeasurement.Tests
{
    /// <summary>
    /// Test class for validating Feet measurement equality.
    /// Each test verifies a specific aspect of the Equals contract.
    /// </summary>
    public class QuantityMeasurementAppTest
    {
        /// <summary>
        /// Test to verify that two Feet objects with the same numerical value
        /// are considered equal.
        /// Given: Two Feet objects with value 1.0
        /// When: Equals is called
        /// Then: Returns true
        /// </summary>
        [Fact]
        public void TestFeetEquality_SameValue()
        {
            // Arrange: Create two Feet objects with the same value
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(1.0);

            // Act: Compare the two Feet objects
            bool isEqual = firstFeetValue.Equals(secondFeetValue);

            // Assert: Both should be considered equal
            Assert.True(isEqual);
        }

        /// <summary>
        /// Test to verify that two Feet objects with different numerical values
        /// are not considered equal.
        /// Given: Two Feet objects with values 1.0 and 2.0
        /// When: Equals is called
        /// Then: Returns false
        /// </summary>
        [Fact]
        public void TestFeetEquality_DifferentValue()
        {
            // Arrange: Create two Feet objects with different values
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(2.0);

            // Act: Compare the two Feet objects
            bool isEqual = firstFeetValue.Equals(secondFeetValue);

            // Assert: They should not be considered equal
            Assert.False(isEqual);
        }

        /// <summary>
        /// Test to verify that a Feet object compared with null returns false.
        /// Ensures proper null handling to prevent NullReferenceException.
        /// Given: A Feet object with value 1.0 and null
        /// When: Equals is called with null
        /// Then: Returns false
        /// </summary>
        [Fact]
        public void TestFeetEquality_NullComparison()
        {
            // Arrange: Create a Feet object
            Feet firstFeetValue = new Feet(1.0);

            // Act: Compare with null
            bool isEqual = firstFeetValue.Equals(null);

            // Assert: Comparison with null should return false
            Assert.False(isEqual);
        }

        /// <summary>
        /// Test to verify that a Feet object compared with an object of a different
        /// type returns false. Ensures type safety and prevents ClassCastException.
        /// Given: A Feet object and a non-Feet object (string)
        /// When: Equals is called
        /// Then: Returns false
        /// </summary>
        [Fact]
        public void TestFeetEquality_DifferentClass()
        {
            // Arrange: Create a Feet object and a non-Feet object
            Feet firstFeetValue = new Feet(1.0);
            object differentClassObject = "Not a Feet object";

            // Act: Compare Feet with a different type
            bool isEqual = firstFeetValue.Equals(differentClassObject);

            // Assert: Different types should not be equal
            Assert.False(isEqual);
        }

        /// <summary>
        /// Test to verify reflexive property: a Feet object is equal to itself.
        /// Validates that the same reference always returns true.
        /// Given: A single Feet object reference
        /// When: Equals is called with the same reference
        /// Then: Returns true
        /// </summary>
        [Fact]
        public void TestFeetEquality_SameReference()
        {
            // Arrange: Create a Feet object
            Feet firstFeetValue = new Feet(1.0);

            // Act: Compare the object with itself (same reference)
            bool isEqual = firstFeetValue.Equals(firstFeetValue);

            // Assert: Same reference should always be equal (reflexive property)
            Assert.True(isEqual);
        }

        /// <summary>
        /// Test to verify that two Feet objects with zero value are considered equal.
        /// Edge case for boundary value testing.
        /// Given: Two Feet objects with value 0.0
        /// When: Equals is called
        /// Then: Returns true
        /// </summary>
        [Fact]
        public void TestFeetEquality_ZeroValueComparison()
        {
            // Arrange: Create two Feet objects with zero value
            Feet firstFeetValue = new Feet(0.0);
            Feet secondFeetValue = new Feet(0.0);

            // Act: Compare the two Feet objects with zero value
            bool isEqual = firstFeetValue.Equals(secondFeetValue);

            // Assert: Both zero-value objects should be equal
            Assert.True(isEqual);
        }

        /// <summary>
        /// Test to verify that negative Feet values are compared correctly.
        /// Given: Two Feet objects with the same negative value
        /// When: Equals is called
        /// Then: Returns true
        /// </summary>
        [Fact]
        public void TestFeetEquality_NegativeValueComparison()
        {
            // Arrange: Create two Feet objects with the same negative value
            Feet firstFeetValue = new Feet(-5.5);
            Feet secondFeetValue = new Feet(-5.5);

            // Act: Compare the two Feet objects
            bool isEqual = firstFeetValue.Equals(secondFeetValue);

            // Assert: Same negative values should be equal
            Assert.True(isEqual);
        }
    }
}
