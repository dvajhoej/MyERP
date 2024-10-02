using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

// List of border characters to assist with drawing windows
//const char H_BORDER_CHARACTER = '─'; // Horizontal border character
//const char V_BORDER_CHARACTER = '│'; // Vertical border character

//const char NW_CORNER          = '┌'; // North-west corner character
//const char NE_CORNER          = '┐'; // North-east corner character

//const char SW_CORNER          = '└'; // South-west corner character
//const char SE_CORNER          = '┘'; // South-east corner character

//const char WEST_T             = '├'; // West T-junction character
//const char EAST_T             = '┤'; // East T-junction character

//const char NORTH_T            = '┬'; // North T-junction character
//const char SOUTH_T            = '┴'; // South T-junction character
//const char CROSS              = '┼'; // Cross character