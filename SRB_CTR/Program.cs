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
        private static void Main(string[] args)
        {
           // SRB.Frame.ByteArrayExpand.test_byte_array_handle();
            Process cp;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cp = Process.GetCurrentProcess();
            cp.PriorityClass = ProcessPriorityClass.High;
            cp.PriorityClass = ProcessPriorityClass.RealTime;

            SRB.Frame.Node.specializer = new Specializer();
            SrbOnelineMaster main_srb = new SrbOnelineMaster();
            Application.Run(main_srb.Nodes_form);

        }
    }
}
