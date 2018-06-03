using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;




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
            //mainForm main_form = new mainForm();
            //lemonReceiver.ToNode.ComPortControl comport0;
            uartDara f2 = new uartDara();
            //f2.Show(main_form);
            //main_form.Show();
            Application.Run(f2);
		}
	}
}
