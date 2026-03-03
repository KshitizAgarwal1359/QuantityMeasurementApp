namespace QuantityMeasurement.Models
{
    // UC13: ArithmeticOperation sealed class — enum-based operation dispatch using lambda expressions.
    //Lambda Expression Pattern:
    //- The Func<double, double, double> acts as the functional interface
    //- Each constant receives a lambda (a, b) => ... as its operation
    //- The Compute method invokes the stored lambda to execute the operation
    //This design enables:
    //- Type-safe operation switching without if-else chains or switch statements
    //- Clean extensibility for future operations (MULTIPLY, MODULO, etc.)
    //- Single source of truth for operation logic (including division-by-zero protection)
    //- Consistent error handling at the operation level
    // Sealed class with private constructor mirrors the LengthUnit/WeightUnit/VolumeUnit pattern, ensuring only predefined instances exist and preventing external instantiation.
    public sealed class ArithmeticOperation
    {
        // ADD: lambda (a, b) => a + b — addition of two base-unit values
        public static readonly ArithmeticOperation ADD = new ArithmeticOperation("ADD", (a, b) => a + b);
        // SUBTRACT: lambda (a, b) => a - b — subtraction of two base-unit values
        public static readonly ArithmeticOperation SUBTRACT = new ArithmeticOperation("SUBTRACT", (a, b) => a - b);
        // DIVIDE: lambda with division-by-zero guard — fail-fast principle
        // Throws ArithmeticException if divisor is zero, preventing silent Infinity/NaN results
        public static readonly ArithmeticOperation DIVIDE = new ArithmeticOperation("DIVIDE", (a, b) =>
        {
            if (b == 0.0)
            {
                throw new ArithmeticException("Cannot divide by zero. The divisor quantity must have a non-zero value.");
            }
            return a / b;
        });
        private readonly string name;
        // Func<double, double, double> is the C# equivalent of Java's DoubleBinaryOperator
        // It takes two double values (base-unit values of the quantities) and returns a double result
        private readonly Func<double, double, double> operation;
        // Private constructor prevents external instantiation — only predefined instances exist
        private ArithmeticOperation(string name, Func<double, double, double> operation)
        {
            this.name = name;
            this.operation = operation;
        }
        // Compute method invokes the stored lambda to execute the arithmetic operation
        // Called by Quantity<U>.PerformBaseArithmetic to apply the operation on base-unit values
        public double Compute(double left, double right)
        {
            return this.operation(left, right);
        }
        // Returns the operation name for diagnostic/logging purposes
        public override string ToString()
        {
            return this.name;
        }
    }
}
