using System;
using System.Windows.Forms;

namespace _4Term
{
     static class Program
    {
        /*----------------------------------------------------------
         * Main
         * 
         * The application's entry point.
         * Initializes application visual styles and runs the main form.
         *---------------------------------------------------------*/
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Main_Form mainForm = new Main_Form();
            Application.Run(mainForm);
        }
    }
}