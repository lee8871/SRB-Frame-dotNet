using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SRB_CTR.nsFrame;




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
            frame mainB = new frame();
            Application.Run(mainB.Nodes_form);

        
        }
	}
}
