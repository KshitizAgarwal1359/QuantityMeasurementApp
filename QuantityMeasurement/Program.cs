namespace QuantityMeasurement
{
    // UC17: Console entry point kept for backward compatibility.
    // Use QuantityMeasurement.WebApi for the REST API server.
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Quantity Measurement Application");
            Console.WriteLine("================================");
            Console.WriteLine("UC17: Use the WebApi project to run the REST API server.");
            Console.WriteLine("Run: dotnet run --project QuantityMeasurement.WebApi");
        }
    }
}
