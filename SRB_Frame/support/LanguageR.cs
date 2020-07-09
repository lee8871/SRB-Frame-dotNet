using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    public static class LanguageR
    {
        public static void run(string cmdStr)
        {
            using (Process myPro = new Process())
            {
                string cmdExe = @"C:\Program Files\R\R-4.0.1\bin\rscript.exe";
                //指定启动进程是调用的应用程序和命令行参数
                ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);
                myPro.StartInfo = psi;
                myPro.Start();
                myPro.WaitForExit();
            }
        }
    }
}
