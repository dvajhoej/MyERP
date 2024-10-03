using MyERP.StartScreen;
using TECHCOOL.UI;
using System.Runtime.InteropServices;
using System;

namespace MyERP
{
    // Define a class Program to contain the main entry point of the application
    internal class Program
    {
        // P/Invoke declarations
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MAXIMIZE = 3;

        private static void MaximizeConsoleWindow()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_MAXIMIZE);
        }

        // Main method to start the application
        static void Main()
        {
            // Maximize the console window
            MaximizeConsoleWindow();

            // Start the data loading process
            DataStarter.DataStart();

            // Display the main menu screen
            Screen.Display(new MyMenuScreen());
        }
    }
}