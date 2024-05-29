using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    internal static class Program
    {
        public static bool IsLoggedIn = false; // 로그인 상태를 저장하는 전역 변수
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

         
            if (!IsLoggedIn)
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new Homes());
            }
        }
    }
}