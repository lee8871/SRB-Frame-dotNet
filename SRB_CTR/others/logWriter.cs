﻿using System;
using System.IO;
using System.Text;
using System.Threading;

namespace SRB_CTR
{
    internal class Log_Writer
    {
        public static bool No_exit_flag = true;
        private string FileName;
        private string Suffix;
        private StreamWriter sw;
        private int file_size = 0;
        private int file_num = 1;
        public Log_Writer(string logName, string suf = ".log")
        {
            Suffix = suf;
            FileName = string.Format("./log/{0}-{1}"
                , logName, DateTime.Now.ToString("yyyyMMdd-HH-mm-ss"));
            newFile();
        }

        private object sw_lock = new object();
        public void add(string st)
        {
            lock (sw_lock)
            {
                sw.WriteLine(st);
            }
            file_size += st.Length;
        }
        public void flush()
        {
            lock (sw_lock)
            {
                sw.Flush();
            }
            if (file_size >= 500 * 1000)
            {
                newFile();
            }

        }


        private Thread flush_thread;
        private bool running_flag = false;
        public bool Running_flag => running_flag;
        public void autoFlushRun()
        {
            if (running_flag == false)
            {
                flush_thread = new Thread(new ThreadStart(autoFlushTH));
                flush_thread.Priority = ThreadPriority.Lowest;
                running_flag = true;
                flush_thread.Start();
            }
        }

        public void autoFlushStop()
        {
            if (running_flag == true)
            {
                running_flag = false;
            }
        }

        private void autoFlushTH()
        {
            while (running_flag)
            {
                Thread.Sleep(1000);
                flush();
                if (No_exit_flag == false)
                {
                    return;
                }
            }
        }

        public void newFile()
        {
            lock (sw_lock)
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                file_size = 0;
                string fn = string.Format("{0}({1}){2}", FileName, file_num, Suffix);
                sw = new StreamWriter(fn, true, Encoding.ASCII, 1024 * 1024);
                sw.AutoFlush = false;
                file_num++;
                sw.Flush();
            }
        }
    }
}
