using MyERP.StartScreen;
using TECHCOOL.UI;
namespace MyERP
{
    internal class Program
    {
        static void Main()
        {
            DataStarter.DataStart();
            Screen.Display(new MyMenuScreen());
        }


    }
}

