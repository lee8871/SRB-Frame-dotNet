﻿using SRB.Frame;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace SRB_CTR
{
    internal class SRB_Record:ISRB_Record
    {
        private bool is_running = false;
        private string fileName;
        private string path;
        private string suffix;
        private StreamWriter sw;
        private int file_size = 0;
        private int file_num = 1;
        private Thread flush_thread;
        private object sw_lock = new object();

        public string FileName => fileName;
        public bool Is_running => is_running;
        public string Path => path;
        public string Suffix => suffix;
        private RecordUC form;

        public SRB_Record(string suf = ".json")
        {
            suffix = suf;
            path = "./record/";
            fileName = string.Format("{0}-{1}"
                , "record", DateTime.Now.ToString("yyMMdd-HHmmss"));
        }

        public void beginRecord()
        {
            newFile();
            is_running = true;
            flush_thread = new Thread(new ThreadStart(autoFlushTH));
            flush_thread.Priority = ThreadPriority.Lowest;
            flush_thread.Start();
        }
        private void autoFlushTH()
        {
            while (is_running)
            {
                Thread.Sleep(1000);
                flush();
            }
            lock (sw_lock)
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        public void endRecord()
        {
            is_running = false;
        }


        public void addAccess(Access ac)
        {
            if (is_running)
            {
                string st = ac.toJson();
                lock (sw_lock)
                {
                    sw.WriteLine(st);
                }
                file_size += st.Length;
            }
        }
        private void flush()
        {
            lock (sw_lock)
            {
                sw.Flush();
                if (file_size >= 5000 * 1024)
                {
                    newFile();
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
                string fn = string.Format("{0}{1}({2}){3}", Path, FileName, file_num, Suffix);
                sw = new StreamWriter(fn, true, Encoding.ASCII, 1024 * 1024);
                sw.AutoFlush = false;
                file_num++;
                sw.Flush();
            }
        }
        public System.Windows.Forms.Control getConfigControl()
        {
            if (form == null)
            {
                form = new RecordUC(this);
            }
            return form;
        }
    }
}
