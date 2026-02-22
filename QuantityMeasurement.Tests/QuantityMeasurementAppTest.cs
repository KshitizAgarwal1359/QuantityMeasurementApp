using QuantityMeasurement.Models;
namespace QuantityMeasurement.Tests
{
    public class QuantityMeasurementAppTest
    {
        // ==================== Feet Equality Tests (UC1) ====================
        [Fact]
        public void TestFeetEquality_SameValue()
        {
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(1.0);
            bool isEqual = firstFeetValue.Equals(secondFeetValue);
            Assert.True(isEqual);
        }
        [Fact]
        public void TestFeetEquality_DifferentValue()
        {
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(2.0);
            bool isEqual = firstFeetValue.Equals(secondFeetValue);
            Assert.False(isEqual);
        }
        [Fact]
        public void TestFeetEquality_NullComparison()
        {
            Feet firstFeetValue = new Feet(1.0);
            bool isEqual = firstFeetValue.Equals(null);
            Assert.False(isEqual);
        }
        [Fact]
        public void TestFeetEquality_DifferentClass()
        {
            Feet firstFeetValue = new Feet(1.0);
            object differentClassObject = "Not a Feet object";
            bool isEqual = firstFeetValue.Equals(differentClassObject);
            Assert.False(isEqual);
        }
        [Fact]
        public void TestFeetEquality_SameReference()
        {
            Feet firstFeetValue = new Feet(1.0);
            bool isEqual = firstFeetValue.Equals(firstFeetValue);
            Assert.True(isEqual);
        }
        [Fact]
        public void TestFeetEquality_ZeroValueComparison()
        {
            Feet firstFeetValue = new Feet(0.0);
            Feet secondFeetValue = new Feet(0.0);
            bool isEqual = firstFeetValue.Equals(secondFeetValue);
            Assert.True(isEqual);
        }
        [Fact]
        public void TestFeetEquality_NegativeValueComparison()
        {
            Feet firstFeetValue = new Feet(-5.5);
            Feet secondFeetValue = new Feet(-5.5);
            bool isEqual = firstFeetValue.Equals(secondFeetValue);
            Assert.True(isEqual);
        }
        // ==================== Inches Equality Tests (UC2) ====================
        [Fact]
        public void TestInchesEquality_SameValue()
        {
            Inches firstInchesValue = new Inches(1.0);
            Inches secondInchesValue = new Inches(1.0);
            bool isEqual = firstInchesValue.Equals(secondInchesValue);
            Assert.True(isEqual);
        }
        [Fact]
        public void TestInchesEquality_DifferentValue()
        {
            Inches firstInchesValue = new Inches(1.0);
            Inches secondInchesValue = new Inches(2.0);
            bool isEqual = firstInchesValue.Equals(secondInchesValue);
            Assert.False(isEqual);
        }
        [Fact]
        public void TestInchesEquality_NullComparison()
        {
            Inches firstInchesValue = new Inches(1.0);
            bool isEqual = firstInchesValue.Equals(null);
            Assert.False(isEqual);
        }
        [Fact]
        public void TestInchesEquality_DifferentClass()
        {
            Inches firstInchesValue = new Inches(1.0);
            object differentClassObject = "Not an Inches object";
            bool isEqual = firstInchesValue.Equals(differentClassObject);
            Assert.False(isEqual);
        }
        [Fact]
        public void TestInchesEquality_SameReference()
        {
            Inches firstInchesValue = new Inches(1.0);
            bool isEqual = firstInchesValue.Equals(firstInchesValue);
            Assert.True(isEqual);
        }
        // ==================== QuantityLength Tests (UC3 - DRY Principle) ====================
        // Verifies that Quantity(1.0, "feet") equals Quantity(1.0, "feet")
        [Fact]
        public void TestEquality_FeetToFeet_SameValue()
        {
            QuantityLength firstQuantity = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength secondQuantity = new QuantityLength(1.0, LengthUnit.FEET);
            bool isEqual = firstQuantity.Equals(secondQuantity);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, "inch") equals Quantity(1.0, "inch")
        [Fact]
        public void TestEquality_InchToInch_SameValue()
        {
            QuantityLength firstQuantity = new QuantityLength(1.0, LengthUnit.INCH);
            QuantityLength secondQuantity = new QuantityLength(1.0, LengthUnit.INCH);
            bool isEqual = firstQuantity.Equals(secondQuantity);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, "feet") equals Quantity(12.0, "inch") - cross-unit equality
        [Fact]
        public void TestEquality_FeetToInch_EquivalentValue()
        {
            QuantityLength feetQuantity = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength inchQuantity = new QuantityLength(12.0, LengthUnit.INCH);
            bool isEqual = feetQuantity.Equals(inchQuantity);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(12.0, "inch") equals Quantity(1.0, "feet") - symmetry of conversion
        [Fact]
        public void TestEquality_InchToFeet_EquivalentValue()
        {
            QuantityLength inchQuantity = new QuantityLength(12.0, LengthUnit.INCH);
            QuantityLength feetQuantity = new QuantityLength(1.0, LengthUnit.FEET);
            bool isEqual = inchQuantity.Equals(feetQuantity);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, "feet") does not equal Quantity(2.0, "feet")
        [Fact]
        public void TestEquality_FeetToFeet_DifferentValue()
        {
            QuantityLength firstQuantity = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength secondQuantity = new QuantityLength(2.0, LengthUnit.FEET);
            bool isEqual = firstQuantity.Equals(secondQuantity);
            Assert.False(isEqual);
        }
        // Verifies that Quantity(1.0, "inch") does not equal Quantity(2.0, "inch")
        [Fact]
        public void TestEquality_InchToInch_DifferentValue()
        {
            QuantityLength firstQuantity = new QuantityLength(1.0, LengthUnit.INCH);
            QuantityLength secondQuantity = new QuantityLength(2.0, LengthUnit.INCH);
            bool isEqual = firstQuantity.Equals(secondQuantity);
            Assert.False(isEqual);
        }
        // Verifies that a QuantityLength object equals itself (reflexive property)
        [Fact]
        public void TestEquality_SameReference()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.FEET);
            bool isEqual = quantity.Equals(quantity);
            Assert.True(isEqual);
        }
        // Verifies that a QuantityLength object is not equal to null
        [Fact]
        public void TestEquality_NullComparison()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.FEET);
            bool isEqual = quantity.Equals(null);
            Assert.False(isEqual);
        }
        // Verifies that a QuantityLength compared with a different type returns false
        [Fact]
        public void TestEquality_DifferentClass()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.FEET);
            object differentClassObject = "Not a QuantityLength object";
            bool isEqual = quantity.Equals(differentClassObject);
            Assert.False(isEqual);
        }
        // Verifies that zero feet equals zero inches (cross-unit zero value)
        [Fact]
        public void TestEquality_ZeroFeet_ZeroInches()
        {
            QuantityLength zeroFeet = new QuantityLength(0.0, LengthUnit.FEET);
            QuantityLength zeroInches = new QuantityLength(0.0, LengthUnit.INCH);
            bool isEqual = zeroFeet.Equals(zeroInches);
            Assert.True(isEqual);
        }
    }
}