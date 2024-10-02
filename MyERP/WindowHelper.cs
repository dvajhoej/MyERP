using System.Runtime.InteropServices;

namespace MyERP
{
    // Define a class WindowHelper with static methods for drawing windows
    public class WindowHelper
    {
        // Method to draw a spacer line with a specified start piece, spacer, and end piece
        public static void Spacer(char startpiece, char spacer, int spaces, char endpiece)
        {
            // Use string interpolation to create the spacer line
            Console.WriteLine($"{startpiece}{new string(spacer, spaces)}{endpiece}");
        }

        // Method to draw the top border of a window
        public static void Top(int spaces)
        {
            // Use the NW_CORNER and H_BORDER_CHARACTER characters to draw the top border
            Console.WriteLine('┌' + (new string('─', spaces)) + '┐');
        }

        // Method to draw the bottom border of a window
        public static void Bot(int spaces)
        {
            // Use the SW_CORNER and H_BORDER_CHARACTER characters to draw the bottom border
            Console.WriteLine('└' + (new string('─', spaces)) + '┘');
        }

        // Method to truncate a string to a specified maximum length
        public static string Truncate(string value, int maxLength)
        {
            // Check if the string is null or empty
            if (string.IsNullOrEmpty(value)) return value;

            // If the string is shorter than or equal to the maximum length, return it as is
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }




        public static void Loader()
        {
            Console.WriteLine(@" /$$       /$$   /$$ /$$$$$$$$        /$$$$$$  /$$$$$$$$  /$$$$$$  /$$   /$$ /$$$$$$$  /$$$$$$ /$$$$$$$$ /$$     /$$/    ");
            Console.WriteLine(@"| $$      | $$$ | $$| $$_____/       /$$__  $$| $$_____/ /$$__  $$| $$  | $$| $$__  $$|_  $$_/|__  $$__/|  $$   /$$/     ");
            Console.WriteLine(@"| $$      | $$$$| $$| $$            | $$  \__/| $$      | $$  \__/| $$  | $$| $$  \ $$  | $$     | $$    \  $$ /$$/      ");
            Console.WriteLine(@"| $$      | $$ $$ $$| $$$$$         |  $$$$$$ | $$$$$   | $$      | $$  | $$| $$$$$$$/  | $$     | $$     \  $$$$/       ");
            Console.WriteLine(@"| $$      | $$  $$$$| $$__/          \____  $$| $$__/   | $$      | $$  | $$| $$__  $$  | $$     | $$      \  $$/        ");
            Console.WriteLine(@"| $$      | $$\  $$$| $$             /$$  \ $$| $$      | $$    $$| $$  | $$| $$  \ $$  | $$     | $$       | $$         ");
            Console.WriteLine(@"| $$$$$$$$| $$ \  $$| $$$$$$$$      |  $$$$$$/| $$$$$$$$|  $$$$$$/|  $$$$$$/| $$  | $$ /$$$$$$   | $$       | $$         ");
            Console.WriteLine(@"|________/|__/  \__/|________/       \______/ |________/ \______/  \______/ |__/  |__/|______/   |__/       |__/         ");
            Top(116);
            Console.WriteLine("│{0,-48}{1,-68}│", "", "Tryk på en tast for at hente data");
            Bot(116);      
        }
        public static void getdata()
        {

            Console.Clear();
            int spacer = 70;
            WindowHelper.Top(spacer);
            Console.WriteLine("│{0,-70}│", "Der oprettes forbindelse til databasen.");
            Console.WriteLine("│{0,-70}│", "Vent venligst");
            WindowHelper.Bot(spacer);
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MAXIMIZE = 3;

        private void MaximizeConsoleWindow()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_MAXIMIZE);
        }

    }

}

// List of border Chars to assist with techcooling the page
//const char H_BORDER_CHARACTER = '─';
//const char V_BORDER_CHARACTER = '│';

//const char NW_CORNER          = '┌'; // North-west corner character
//const char NE_CORNER          = '┐'; // North-east corner character

//const char SW_CORNER          = '└'; // South-west corner character
//const char SE_CORNER          = '┘'; // South-east corner character

//const char WEST_T             = '├'; // West T-junction character
//const char EAST_T             = '┤'; // East T-junction character

//const char NORTH_T            = '┬'; // North T-junction character
//const char SOUTH_T            = '┴'; // South T-junction character
//const char CROSS              = '┼'; // Cross character