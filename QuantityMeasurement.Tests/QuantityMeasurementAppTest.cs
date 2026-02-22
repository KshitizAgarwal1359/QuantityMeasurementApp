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
        // ==================== Unit Conversion Tests (UC5) ====================
        // convert(1.0, FEET, INCHES) should return 12.0
        [Fact]
        public void TestConversion_FeetToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.Equal(12.0, result, 6);
        }
        // convert(24.0, INCHES, FEET) should return 2.0
        [Fact]
        public void TestConversion_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.INCH, LengthUnit.FEET);
            Assert.Equal(2.0, result, 6);
        }
        // convert(1.0, YARDS, INCHES) should return 36.0
        [Fact]
        public void TestConversion_YardsToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.YARDS, LengthUnit.INCH);
            Assert.Equal(36.0, result, 6);
        }
        // convert(72.0, INCHES, YARDS) should return 2.0
        [Fact]
        public void TestConversion_InchesToYards()
        {
            double result = QuantityLength.Convert(72.0, LengthUnit.INCH, LengthUnit.YARDS);
            Assert.Equal(2.0, result, 6);
        }
        // convert(2.54, CENTIMETERS, INCHES) should return ~1.0 (within epsilon)
        [Fact]
        public void TestConversion_CentimetersToInches()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.CENTIMETERS, LengthUnit.INCH);
            Assert.Equal(1.0, result, 4);
        }
        // convert(6.0, FEET, YARDS) should return 2.0
        [Fact]
        public void TestConversion_FeetToYards()
        {
            double result = QuantityLength.Convert(6.0, LengthUnit.FEET, LengthUnit.YARDS);
            Assert.Equal(2.0, result, 6);
        }
        // Round-trip: convert(convert(v, A, B), B, A) should approximately equal v
        [Fact]
        public void TestConversion_RoundTrip_PreservesValue()
        {
            double originalValue = 5.5;
            // Feet -> Inches -> Feet
            double convertedToInches = QuantityLength.Convert(originalValue, LengthUnit.FEET, LengthUnit.INCH);
            double convertedBack = QuantityLength.Convert(convertedToInches, LengthUnit.INCH, LengthUnit.FEET);
            Assert.Equal(originalValue, convertedBack, 6);
        }
        // convert(0.0, FEET, INCHES) should return 0.0
        [Fact]
        public void TestConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.Equal(0.0, result, 6);
        }
        // convert(-1.0, FEET, INCHES) should return -12.0
        [Fact]
        public void TestConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.Equal(-12.0, result, 6);
        }
        // Passing NaN should throw ArgumentException
        [Fact]
        public void TestConversion_NaN_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                QuantityLength.Convert(double.NaN, LengthUnit.FEET, LengthUnit.INCH));
        }
        // Passing PositiveInfinity should throw ArgumentException
        [Fact]
        public void TestConversion_PositiveInfinity_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                QuantityLength.Convert(double.PositiveInfinity, LengthUnit.FEET, LengthUnit.INCH));
        }
        // Passing NegativeInfinity should throw ArgumentException
        [Fact]
        public void TestConversion_NegativeInfinity_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                QuantityLength.Convert(double.NegativeInfinity, LengthUnit.FEET, LengthUnit.INCH));
        }
        // Conversion results are within acceptable floating-point epsilon tolerance
        [Fact]
        public void TestConversion_PrecisionTolerance()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.CENTIMETERS, LengthUnit.INCH);
            double expected = 0.393701;
            Assert.True(Math.Abs(result - expected) < 1e-6, $"Expected {expected} but got {result}");
        }
        // Converting a unit to itself returns the original value unchanged
        [Fact]
        public void TestConversion_SameUnit()
        {
            double result = QuantityLength.Convert(5.0, LengthUnit.FEET, LengthUnit.FEET);
            Assert.Equal(5.0, result, 6);
        }
        // Large value conversion maintains precision
        [Fact]
        public void TestConversion_LargeValue()
        {
            double result = QuantityLength.Convert(1000000.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.Equal(12000000.0, result, 6);
        }
        // Small value conversion maintains precision
        [Fact]
        public void TestConversion_SmallValue()
        {
            double result = QuantityLength.Convert(0.001, LengthUnit.FEET, LengthUnit.INCH);
            Assert.Equal(0.012, result, 6);
        }
        // Instance method ConvertTo returns a new QuantityLength with converted value
        [Fact]
        public void TestConversion_InstanceMethod_ConvertTo()
        {
            QuantityLength oneFoot = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength converted = oneFoot.ConvertTo(LengthUnit.INCH);
            Assert.Equal(12.0, converted.MeasurementValue, 6);
            Assert.Equal(LengthUnit.INCH, converted.Unit);
        }
        // ConvertTo returns a NEW instance (immutability check)
        [Fact]
        public void TestConversion_ConvertTo_ReturnsNewInstance()
        {
            QuantityLength original = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength converted = original.ConvertTo(LengthUnit.INCH);
            // Original should remain unchanged
            Assert.Equal(1.0, original.MeasurementValue, 6);
            Assert.Equal(LengthUnit.FEET, original.Unit);
            // Converted should be different instance with new values
            Assert.Equal(12.0, converted.MeasurementValue, 6);
            Assert.Equal(LengthUnit.INCH, converted.Unit);
        }
        // Multi-step round-trip: convert(convert(convert(v, A, B), B, C), C, A) ≈ v
        [Fact]
        public void TestConversion_MultiStepRoundTrip()
        {
            double originalValue = 3.0;
            // Feet -> Yards -> Inches -> Feet
            double toYards = QuantityLength.Convert(originalValue, LengthUnit.FEET, LengthUnit.YARDS);
            double toInches = QuantityLength.Convert(toYards, LengthUnit.YARDS, LengthUnit.INCH);
            double backToFeet = QuantityLength.Convert(toInches, LengthUnit.INCH, LengthUnit.FEET);
            Assert.Equal(originalValue, backToFeet, 4);
        }
        // Constructor should throw ArgumentException for NaN
        [Fact]
        public void TestConstructor_NaN_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new QuantityLength(double.NaN, LengthUnit.FEET));
        }
        // Constructor should throw ArgumentException for Infinity
        [Fact]
        public void TestConstructor_Infinity_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new QuantityLength(double.PositiveInfinity, LengthUnit.FEET));
        }
        // Yards to Feet conversion
        [Fact]
        public void TestConversion_YardsToFeet()
        {
            double result = QuantityLength.Convert(3.0, LengthUnit.YARDS, LengthUnit.FEET);
            Assert.Equal(9.0, result, 6);
        }
    }
}