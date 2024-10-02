using MyERP.StartScreen;
using TECHCOOL.UI;

namespace MyERP
{
    // Define a class Program to contain the main entry point of the application
    internal class Program
    {
        // Main method to start the application
        static void Main()
        {
            // Start the data loading process
            DataStarter.DataStart();

            // Display the main menu screen
            Screen.Display(new MyMenuScreen());
        }
    }
}