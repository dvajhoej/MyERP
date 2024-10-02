namespace MyERP
{
    public class WindowHelper
    {
        public static void Spacer(char startpiece, char spacer, int spaces, char endpiece)
        {
            Console.WriteLine($"{startpiece}{new string(spacer, spaces)}{endpiece}");
        }
        public static void Top(int spaces)
        {
            Console.WriteLine('┌' + (new string('─', spaces)) + '┐');
        }
        public static void Bot(int spaces)
        {
            Console.WriteLine('└' + (new string('─', spaces)) + '┘');
        }
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
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