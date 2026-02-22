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
        // ==================== Yards and Centimeters Tests (UC4 - Extended Unit Support) ====================
        // Verifies that Quantity(1.0, YARDS) equals Quantity(1.0, YARDS)
        [Fact]
        public void TestEquality_YardToYard_SameValue()
        {
            QuantityLength firstYard = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength secondYard = new QuantityLength(1.0, LengthUnit.YARDS);
            bool isEqual = firstYard.Equals(secondYard);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, YARDS) does not equal Quantity(2.0, YARDS)
        [Fact]
        public void TestEquality_YardToYard_DifferentValue()
        {
            QuantityLength firstYard = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength secondYard = new QuantityLength(2.0, LengthUnit.YARDS);
            bool isEqual = firstYard.Equals(secondYard);
            Assert.False(isEqual);
        }
        // Verifies that Quantity(1.0, YARDS) equals Quantity(3.0, FEET)
        [Fact]
        public void TestEquality_YardToFeet_EquivalentValue()
        {
            QuantityLength oneYard = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength threeFeet = new QuantityLength(3.0, LengthUnit.FEET);
            bool isEqual = oneYard.Equals(threeFeet);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(3.0, FEET) equals Quantity(1.0, YARDS) - symmetry
        [Fact]
        public void TestEquality_FeetToYard_EquivalentValue()
        {
            QuantityLength threeFeet = new QuantityLength(3.0, LengthUnit.FEET);
            QuantityLength oneYard = new QuantityLength(1.0, LengthUnit.YARDS);
            bool isEqual = threeFeet.Equals(oneYard);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, YARDS) equals Quantity(36.0, INCHES)
        [Fact]
        public void TestEquality_YardToInches_EquivalentValue()
        {
            QuantityLength oneYard = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength thirtySixInches = new QuantityLength(36.0, LengthUnit.INCH);
            bool isEqual = oneYard.Equals(thirtySixInches);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(36.0, INCHES) equals Quantity(1.0, YARDS) - symmetry
        [Fact]
        public void TestEquality_InchesToYard_EquivalentValue()
        {
            QuantityLength thirtySixInches = new QuantityLength(36.0, LengthUnit.INCH);
            QuantityLength oneYard = new QuantityLength(1.0, LengthUnit.YARDS);
            bool isEqual = thirtySixInches.Equals(oneYard);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, YARDS) does not equal Quantity(2.0, FEET)
        [Fact]
        public void TestEquality_YardToFeet_NonEquivalentValue()
        {
            QuantityLength oneYard = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength twoFeet = new QuantityLength(2.0, LengthUnit.FEET);
            bool isEqual = oneYard.Equals(twoFeet);
            Assert.False(isEqual);
        }
        // Verifies that Quantity(1.0, CENTIMETERS) equals Quantity(0.393701, INCHES)
        [Fact]
        public void TestEquality_CentimetersToInches_EquivalentValue()
        {
            QuantityLength oneCm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            QuantityLength equivalentInches = new QuantityLength(0.393701, LengthUnit.INCH);
            bool isEqual = oneCm.Equals(equivalentInches);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, CENTIMETERS) does not equal Quantity(1.0, FEET)
        [Fact]
        public void TestEquality_CentimetersToFeet_NonEquivalentValue()
        {
            QuantityLength oneCm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            QuantityLength oneFoot = new QuantityLength(1.0, LengthUnit.FEET);
            bool isEqual = oneCm.Equals(oneFoot);
            Assert.False(isEqual);
        }
        // Verifies that Quantity(2.0, CENTIMETERS) equals Quantity(2.0, CENTIMETERS)
        [Fact]
        public void TestEquality_CmToCm_SameValue()
        {
            QuantityLength firstCm = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            QuantityLength secondCm = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            bool isEqual = firstCm.Equals(secondCm);
            Assert.True(isEqual);
        }
        // Verifies that Quantity(1.0, CENTIMETERS) does not equal Quantity(2.0, CENTIMETERS)
        [Fact]
        public void TestEquality_CmToCm_DifferentValue()
        {
            QuantityLength firstCm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            QuantityLength secondCm = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            bool isEqual = firstCm.Equals(secondCm);
            Assert.False(isEqual);
        }
        // Verifies transitive property: 1 Yard = 3 Feet, 3 Feet = 36 Inches, so 1 Yard = 36 Inches
        [Fact]
        public void TestEquality_MultiUnit_TransitiveProperty()
        {
            QuantityLength quantityA = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength quantityB = new QuantityLength(3.0, LengthUnit.FEET);
            QuantityLength quantityC = new QuantityLength(36.0, LengthUnit.INCH);
            // A equals B
            Assert.True(quantityA.Equals(quantityB));
            // B equals C
            Assert.True(quantityB.Equals(quantityC));
            // Therefore A equals C (transitive)
            Assert.True(quantityA.Equals(quantityC));
        }
        // Verifies that a Quantity yard object equals itself (reflexive property)
        [Fact]
        public void TestEquality_YardSameReference()
        {
            QuantityLength yardQuantity = new QuantityLength(1.0, LengthUnit.YARDS);
            bool isEqual = yardQuantity.Equals(yardQuantity);
            Assert.True(isEqual);
        }
        // Verifies that a Quantity yard object is not equal to null
        [Fact]
        public void TestEquality_YardNullComparison()
        {
            QuantityLength yardQuantity = new QuantityLength(1.0, LengthUnit.YARDS);
            bool isEqual = yardQuantity.Equals(null);
            Assert.False(isEqual);
        }
        // Verifies that a Quantity centimeters object equals itself (reflexive property)
        [Fact]
        public void TestEquality_CentimetersSameReference()
        {
            QuantityLength cmQuantity = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            bool isEqual = cmQuantity.Equals(cmQuantity);
            Assert.True(isEqual);
        }
        // Verifies that a Quantity centimeters object is not equal to null
        [Fact]
        public void TestEquality_CentimetersNullComparison()
        {
            QuantityLength cmQuantity = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            bool isEqual = cmQuantity.Equals(null);
            Assert.False(isEqual);
        }
        // Verifies complex scenario: 2 Yards = 6 Feet = 72 Inches
        [Fact]
        public void TestEquality_AllUnits_ComplexScenario()
        {
            QuantityLength twoYards = new QuantityLength(2.0, LengthUnit.YARDS);
            QuantityLength sixFeet = new QuantityLength(6.0, LengthUnit.FEET);
            QuantityLength seventyTwoInches = new QuantityLength(72.0, LengthUnit.INCH);
            // 2 Yards equals 6 Feet
            Assert.True(twoYards.Equals(sixFeet));
            // 6 Feet equals 72 Inches
            Assert.True(sixFeet.Equals(seventyTwoInches));
            // 2 Yards equals 72 Inches
            Assert.True(twoYards.Equals(seventyTwoInches));
        }
        // Verifies zero values across all units are equal
        [Fact]
        public void TestEquality_ZeroValues_AllUnits()
        {
            QuantityLength zeroFeet = new QuantityLength(0.0, LengthUnit.FEET);
            QuantityLength zeroInches = new QuantityLength(0.0, LengthUnit.INCH);
            QuantityLength zeroYards = new QuantityLength(0.0, LengthUnit.YARDS);
            QuantityLength zeroCm = new QuantityLength(0.0, LengthUnit.CENTIMETERS);
            Assert.True(zeroFeet.Equals(zeroInches));
            Assert.True(zeroFeet.Equals(zeroYards));
            Assert.True(zeroFeet.Equals(zeroCm));
            Assert.True(zeroYards.Equals(zeroCm));
        }
    }
}