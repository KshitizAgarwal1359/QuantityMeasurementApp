using QuantityMeasurement.Controllers;
using QuantityMeasurement.Models;
using QuantityMeasurement.Service;

namespace QuantityMeasurement
{
    // UC15: Menu class — interactive console menu for the Quantity Measurement Application.
    // Implements IMenu to provide a user-friendly interface for all measurement operations.
    //
    // Flow: Main Menu → Select Operation → Select Measurement Type → Select Units → Enter Values → Display Result
    //
    // Supported Operations:
    //   1. Compare — Compare two quantities for equality
    //   2. Convert — Convert a quantity from one unit to another
    //   3. Add — Add two quantities (not supported for Temperature)
    //   4. Subtract — Subtract two quantities (not supported for Temperature)
    //   5. Divide — Divide two quantities (not supported for Temperature)
    //   6. Exit — Exit the application
    //
    // Temperature Restriction: Arithmetic operations (Add, Subtract, Divide) are not meaningful
    // for temperature measurements and are blocked at the menu level with a clear message.
    //
    // The Menu delegates all business logic to the Controller layer,
    // which in turn delegates to the Service layer (N-Tier Architecture).
    public class Menu : IMenu
    {
        // Controller dependency — handles business logic delegation
        private readonly QuantityMeasurementController controller;

        // Unit arrays for each measurement type — displayed to the user for selection
        private static readonly string[] LengthUnits = { "FEET", "INCH", "YARDS", "CENTIMETERS" };
        private static readonly string[] WeightUnits = { "KILOGRAM", "GRAM", "POUND" };
        private static readonly string[] VolumeUnits = { "LITRE", "MILLILITRE", "GALLON" };
        private static readonly string[] TemperatureUnits = { "CELSIUS", "FAHRENHEIT" };

        // Measurement type names for display
        private static readonly string[] MeasurementTypes = { "LENGTH", "WEIGHT", "VOLUME", "TEMPERATURE" };

        // Constructor — receives controller dependency
        public Menu(QuantityMeasurementController controller)
        {
            this.controller = controller ?? throw new ArgumentNullException(
                nameof(controller), "Controller cannot be null.");
        }

        // Starts the interactive menu loop — runs until user selects Exit
        public void Run()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   Interactive Menu (UC15)");
            Console.WriteLine("========================================");
            Console.WriteLine();

            bool running = true;
            while (running)
            {
                ShowMainMenu();
                int choice = GetIntInput("Enter your choice: ", 1, 6);

                switch (choice)
                {
                    case 1:
                        HandleComparison();
                        break;
                    case 2:
                        HandleConversion();
                        break;
                    case 3:
                        HandleArithmetic("ADD");
                        break;
                    case 4:
                        HandleArithmetic("SUBTRACT");
                        break;
                    case 5:
                        HandleArithmetic("DIVIDE");
                        break;
                    case 6:
                        Console.WriteLine();
                        Console.WriteLine("Thank you for using Quantity Measurement Application!");
                        Console.WriteLine("========================================");
                        running = false;
                        break;
                }
            }
        }

        // Displays the main menu options
        private void ShowMainMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        MAIN MENU");
            Console.WriteLine("========================================");
            Console.WriteLine("  1. Compare two quantities");
            Console.WriteLine("  2. Convert a quantity");
            Console.WriteLine("  3. Add two quantities");
            Console.WriteLine("  4. Subtract two quantities");
            Console.WriteLine("  5. Divide two quantities");
            Console.WriteLine("  6. Exit");
            Console.WriteLine("========================================");
        }

        // Displays available measurement types and returns selected type
        private string SelectMeasurementType()
        {
            Console.WriteLine();
            Console.WriteLine("--- Select Measurement Type ---");
            for (int i = 0; i < MeasurementTypes.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {MeasurementTypes[i]}");
            }
            int choice = GetIntInput("Enter measurement type: ", 1, MeasurementTypes.Length);
            return MeasurementTypes[choice - 1];
        }

        // Returns the available units for a given measurement type
        private string[] GetUnitsForType(string measurementType)
        {
            return measurementType switch
            {
                "LENGTH" => LengthUnits,
                "WEIGHT" => WeightUnits,
                "VOLUME" => VolumeUnits,
                "TEMPERATURE" => TemperatureUnits,
                _ => Array.Empty<string>()
            };
        }

        // Displays available units for a measurement type and returns the selected unit
        private string SelectUnit(string measurementType, string prompt)
        {
            string[] units = GetUnitsForType(measurementType);
            Console.WriteLine();
            Console.WriteLine($"--- {prompt} ---");
            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {units[i]}");
            }
            int choice = GetIntInput("Enter unit: ", 1, units.Length);
            return units[choice - 1];
        }

        // Gets a double value from user with validation
        private double GetDoubleInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (double.TryParse(input, out double value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        // Gets an integer within a range from user with validation
        private int GetIntInput(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }

        // ---- Operation Handlers ----

        // Handles the Comparison operation
        // Flow: Select type → Select unit1 → Enter value1 → Select unit2 → Enter value2 → Display result
        private void HandleComparison()
        {
            Console.WriteLine();
            Console.WriteLine("===== COMPARE TWO QUANTITIES =====");

            string measurementType = SelectMeasurementType();

            // First quantity
            Console.WriteLine();
            Console.WriteLine("--- First Quantity ---");
            string unit1 = SelectUnit(measurementType, "Select Unit for First Quantity");
            double value1 = GetDoubleInput($"Enter value in {unit1}: ");

            // Second quantity
            Console.WriteLine();
            Console.WriteLine("--- Second Quantity ---");
            string unit2 = SelectUnit(measurementType, "Select Unit for Second Quantity");
            double value2 = GetDoubleInput($"Enter value in {unit2}: ");

            // Perform comparison via controller
            Console.WriteLine();
            Console.WriteLine("--- Result ---");
            controller.PerformComparison(value1, unit1, value2, unit2, measurementType);
        }

        // Handles the Conversion operation
        // Flow: Select type → Select source unit → Enter value → Select target unit → Display result
        private void HandleConversion()
        {
            Console.WriteLine();
            Console.WriteLine("===== CONVERT A QUANTITY =====");

            string measurementType = SelectMeasurementType();

            // Source quantity
            Console.WriteLine();
            Console.WriteLine("--- Source Quantity ---");
            string sourceUnit = SelectUnit(measurementType, "Select Source Unit");
            double value = GetDoubleInput($"Enter value in {sourceUnit}: ");

            // Target unit
            string targetUnit = SelectUnit(measurementType, "Select Target Unit");

            // Perform conversion via controller
            Console.WriteLine();
            Console.WriteLine("--- Result ---");
            controller.PerformConversion(value, sourceUnit, targetUnit, measurementType);
        }

        // Handles Add, Subtract, and Divide operations
        // Flow: Select type → Check temperature restriction → Select units → Enter values
        //       → (For Add/Subtract: select target unit) → Display result
        private void HandleArithmetic(string operation)
        {
            string operationName = operation switch
            {
                "ADD" => "ADD TWO QUANTITIES",
                "SUBTRACT" => "SUBTRACT TWO QUANTITIES",
                "DIVIDE" => "DIVIDE TWO QUANTITIES",
                _ => operation
            };

            Console.WriteLine();
            Console.WriteLine($"===== {operationName} =====");

            string measurementType = SelectMeasurementType();

            // Temperature restriction: arithmetic operations are not supported
            if (measurementType == "TEMPERATURE")
            {
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("  ERROR: Temperature does not support");
                Console.WriteLine($"  {operation} operation.");
                Console.WriteLine("  Temperature measurements use non-linear");
                Console.WriteLine("  conversions and arithmetic operations");
                Console.WriteLine("  are not meaningful.");
                Console.WriteLine("========================================");
                Console.WriteLine();
                return;
            }

            // First quantity
            Console.WriteLine();
            Console.WriteLine("--- First Quantity ---");
            string unit1 = SelectUnit(measurementType, "Select Unit for First Quantity");
            double value1 = GetDoubleInput($"Enter value in {unit1}: ");

            // Second quantity
            Console.WriteLine();
            Console.WriteLine("--- Second Quantity ---");
            string unit2 = SelectUnit(measurementType, "Select Unit for Second Quantity");
            double value2 = GetDoubleInput($"Enter value in {unit2}: ");

            Console.WriteLine();
            Console.WriteLine("--- Result ---");

            // Perform operation via controller
            switch (operation)
            {
                case "ADD":
                    string addTargetUnit = SelectUnit(measurementType, "Select Target Unit for Result");
                    Console.WriteLine();
                    controller.PerformAddition(value1, unit1, value2, unit2, addTargetUnit, measurementType);
                    break;
                case "SUBTRACT":
                    string subTargetUnit = SelectUnit(measurementType, "Select Target Unit for Result");
                    Console.WriteLine();
                    controller.PerformSubtraction(value1, unit1, value2, unit2, subTargetUnit, measurementType);
                    break;
                case "DIVIDE":
                    controller.PerformDivision(value1, unit1, value2, unit2, measurementType);
                    break;
            }
        }
    }
}
