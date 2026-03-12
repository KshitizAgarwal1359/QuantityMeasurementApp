namespace QuantityMeasurement
{
    // UC15: IMenu interface — defines the contract for the interactive console menu.
    // Follows Interface Segregation Principle (ISP): clients depend only on what they need.
    // The Menu implementation provides the interactive user experience,
    // while Program.cs only needs to call Run() to start the application.
    public interface IMenu
    {
        // Starts the interactive menu loop.
        // Displays the main menu, accepts user input, and delegates operations
        // to the Controller layer via QuantityMeasurementController.
        // The loop continues until the user selects the Exit option.
        void Run();
    }
}
