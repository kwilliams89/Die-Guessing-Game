using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KWilliams_CS3280_Assignment2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DieGuess());
        }
    }
}
