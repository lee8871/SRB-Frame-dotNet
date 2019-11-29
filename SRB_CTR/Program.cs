using System;
using System.Diagnostics;
using System.Windows.Forms;




namespace SRB_CTR
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Process cp;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cp = Process.GetCurrentProcess();
            cp.PriorityClass = ProcessPriorityClass.RealTime;
            // showfortest();
            SRB_oneline_master main_srb = new SRB_oneline_master();
            Application.Run(main_srb.Nodes_form);

        }
    }
}
