using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
   public class WindowHelper
    {
        public static void Spacer(char startpiece, char spacer, int spaces, char endpiece)
        {

            Console.WriteLine($"{startpiece}{new string(spacer, spaces)}{endpiece}");



        }
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }
    }
}

 // List of border Chars to assist with techcooling the page
//const char H_BORDER_CHARACTER = '─';
//const char V_BORDER_CHARACTER = '│';
//const char NW_CORNER          = '┌';
//const char NE_CORNER          = '┐';
//const char SW_CORNER          = '└';
//const char SE_CORNER          = '┘';
//const char WEST_T             = '├';
//const char EAST_T             = '┤';
//const char NORTH_T            = '┬';
//const char SOUTH_T            = '┴';
//const char CROSS              = '┼';