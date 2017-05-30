using System;
using System.Linq;
using System.Windows.Forms;
using ClassLib;
namespace StockControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ClassLib.Classlib.User =System.Environment.UserName;
            ClassLib.Classlib.DomainUser = System.Environment.UserDomainName;
            ClassLib.Classlib.ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            ClassLib.Classlib.ScreenHight = Screen.PrimaryScreen.Bounds.Height;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mainfrom());
        }
    }
}