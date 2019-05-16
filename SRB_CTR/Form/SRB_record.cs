using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using SRB.Frame;

namespace SRB_CTR
{
    class SRB_Record
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

        public string FileName { get => fileName;  }
        public bool Is_running { get => is_running; }
        public string Path { get => path;  }
        public string Suffix { get => suffix;  }
        private SRB_record_uc form;

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
            flush_thread = new Thread(new ThreadStart(thAutoFlush));
            flush_thread.Priority = ThreadPriority.Lowest;
            flush_thread.Start();
        }
        private void thAutoFlush()
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


        public void add(Access ac)
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
                form = new SRB_record_uc(this);
            }
            return form;
        }


    }
}
