using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace TestConfigProject
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult);
            Application.Run(loginForm);
            if (Connected)
            {
                Application.Run(new MainForm());
            }
            
        }

        private static bool Connected = false;

        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }
    }
}
