using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SRB_CTR.nsBrain;




namespace SRB_CTR
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
        [STAThread]
		static void Main()
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);          
            
            // showfortest();
            brain mainB = new brain();
            Application.Run(mainB.Nodes_form);

        
        }
        static void showfortest()
        {
            mainForm main_form = new mainForm();
            uartDara f2 = new uartDara();
            main_form.Show();
            Application.Run(f2);
		}
	}
}
