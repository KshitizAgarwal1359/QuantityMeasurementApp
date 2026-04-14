namespace BusinessLogic.API.Models
{
    // UC13: ArithmeticOperation — type-safe operation dispatch for arithmetic.
    public sealed class ArithmeticOperation
    {
        public static readonly ArithmeticOperation ADD = new ArithmeticOperation("ADD", AddMethod);
        public static readonly ArithmeticOperation SUBTRACT = new ArithmeticOperation("SUBTRACT", SubtractMethod);
        public static readonly ArithmeticOperation DIVIDE = new ArithmeticOperation("DIVIDE", DivideMethod);
        private static double AddMethod(double a, double b)
        {
            return a + b;
        }
        private static double SubtractMethod(double a, double b)
        {
            return a - b;
        }
        private static double DivideMethod(double a, double b)
        {
            if (b == 0.0)
            {
                throw new ArithmeticException("Cannot divide by zero. The divisor quantity must have a non-zero value.");
            }
            return a / b;
        }
        private readonly string name;
        // Stored operation function for this arithmetic type
        private readonly Func<double, double, double> operation;
        // Private constructor — only predefined instances exist
        private ArithmeticOperation(string name, Func<double, double, double> operation)
        {
            this.name = name;
            this.operation = operation;
        }
        // Executes the arithmetic operation on two values
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
