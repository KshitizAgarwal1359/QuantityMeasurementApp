namespace BusinessLogic.API.Models
{
    // UC14: TemperatureUnit sealed class implementing IMeasurable — supports Celsius and Fahrenheit.
    // Unlike LengthUnit, WeightUnit, and VolumeUnit which use LINEAR conversion (value * factor),
    // TemperatureUnit uses NON-LINEAR conversion via Func<double, double> lambda expressions
    // Temperature conversion formulas:
    //   Celsius to Fahrenheit: °F = (°C × 9/5) + 32
    //   Fahrenheit to Celsius: °C = (°F - 32) × 5/9
    //
    // Celsius is the BASE UNIT — all conversions go through Celsius internally.
    //
    // Temperature does NOT support arithmetic operations (add, subtract, divide):
    //   - Adding two absolute temperatures is meaningless (100°C + 50°C ≠ 150°C)
    //- Dividing temperatures produces no meaningful physical quantity
    //- This is enforced via SupportsArithmetic() returning false and
    //     ValidateOperationSupport() throwing InvalidOperationException
    // Lambda Expression Pattern:
    //   - Func<double, double> toBaseUnit: converts FROM this unit TO Celsius (base)
    //   - Func<double, double> fromBaseUnit: converts FROM Celsius (base) TO this unit
    //   - Func<bool> supportsArithmetic: lambda () => false indicating no arithmetic support
    //
    // Sealed class with private constructor mirrors the LengthUnit/WeightUnit/VolumeUnit pattern,
    // ensuring only predefined instances exist.
    public sealed class TemperatureUnit : IMeasurable
    {
        // CELSIUS: base unit — identity conversion lambda (celsius) => celsius
        public static readonly TemperatureUnit CELSIUS = new TemperatureUnit(
            "CELSIUS", "°C",
            (celsius) => celsius,
            (celsius) => celsius
        );
        // FAHRENHEIT: converts to/from Celsius using standard formulas
        // To base (Celsius): °C = (°F - 32) × 5/9
        // From base (Celsius): °F = (°C × 9/5) + 32
        public static readonly TemperatureUnit FAHRENHEIT = new TemperatureUnit(
            "FAHRENHEIT", "°F",
            (fahrenheit) => (fahrenheit - 32.0) * 5.0 / 9.0,
            (celsius) => celsius * 9.0 / 5.0 + 32.0
        );
        private readonly string name;
        private readonly string unitLabel;

        // Func<double, double> lambda expressions for non-linear conversion
        // This is the C# equivalent of Java's Function<Double, Double> functional interface
        private readonly Func<double, double> toBaseUnit;
        private readonly Func<double, double> fromBaseUnit;
        // Lambda expression to indicate that TemperatureUnit does NOT support arithmetic operations
        // C# equivalent of Java's SupportsArithmetic supportsArithmetic = () -> false;
        private readonly Func<bool> supportsArithmetic = () => false;
        // Private constructor prevents external instantiation — only predefined instances exist
        private TemperatureUnit(string name, string unitLabel,
            Func<double, double> toBaseUnit, Func<double, double> fromBaseUnit)
        {
            this.name = name;
            this.unitLabel = unitLabel;
            this.toBaseUnit = toBaseUnit;
            this.fromBaseUnit = fromBaseUnit;
        }
        // GetConversionFactor returns 1.0 as a nominal value — not used for temperature conversion
        // Temperature conversion is NON-LINEAR and uses lambda expressions instead
        public double GetConversionFactor()
        {
            return 1.0;
        }
        // Converts a value in this temperature unit to the base unit (Celsius)
        // Uses the stored lambda expression for non-linear conversion
        // Example: FAHRENHEIT.ConvertToBaseUnit(212.0) returns 100.0
        public double ConvertToBaseUnit(double value)
        {
            return this.toBaseUnit(value);
        }
        // Converts a value from the base unit (Celsius) to this temperature unit
        // Uses the stored lambda expression for non-linear conversion
        // Example: FAHRENHEIT.ConvertFromBaseUnit(100.0) returns 212.0
        public double ConvertFromBaseUnit(double baseValue)
        {
            return this.fromBaseUnit(baseValue);
        }
        // Returns the human-readable display label (e.g., "°C", "°F")
        public string GetUnitName()
        {
            return this.unitLabel;
        }
        // UC15: Returns measurement type category
        public string GetMeasurementType()
        {
            return "TEMPERATURE";
        }

        // UC14: Override — TemperatureUnit does NOT support arithmetic operations
        // Returns false via the stored lambda expression: () => false
        // This is checked by Quantity<U>.PerformBaseArithmetic before executing operations
        public bool SupportsArithmetic()
        {
            return this.supportsArithmetic();
        }
        // UC14: Override — throws InvalidOperationException for any arithmetic operation
        // Provides clear, informative error messages explaining WHY temperature arithmetic is unsupported
        // Called by Quantity<U>.PerformBaseArithmetic via this.unit.ValidateOperationSupport(operation)
        public void ValidateOperationSupport(string operation)
        {
            throw new InvalidOperationException(
                $"Temperature does not support {operation} operation. " +
                "Temperature measurements use non-linear conversions and arithmetic operations are not meaningful."
            );
        }
        // Returns the enum-style name (e.g., "CELSIUS", "FAHRENHEIT")
        public override string ToString()
        {
            return this.name;
        }
    }
}
