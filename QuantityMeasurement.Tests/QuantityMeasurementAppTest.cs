using QuantityMeasurement.Models;
namespace QuantityMeasurement.Tests
{
    public class QuantityMeasurementAppTest
    {
        
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
