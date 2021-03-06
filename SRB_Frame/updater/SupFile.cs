﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame.updater
{
    public class SupFile : IEnumerable<byte[]>
    {
        byte[][] access_ba_a;
        string description;

        public SRB.Frame.Version srbVER = new SRB.Frame.Version("SRB");
        public SRB.Frame.Version nodeVER = new SRB.Frame.Version("App");


        int time_stamp;
        string node_type;

        string[] hardware_code;
        string build_time;
        string file;

        public byte[] this[int i] => access_ba_a[i];
        public int Length => access_ba_a.Length;


        public string File => file;
        public string Dscription=>description;
        public int Time_stamp => time_stamp;
        public string Node_type => node_type;
        public string[] Hardware_codes_array => hardware_code;
        public string Dscripbuild_timetion => build_time;
        public SupFile(string file)
        {
            this.file = file;
            FileStream inFS = null;
            try
            {
                inFS = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (Exception e)
            {
                inFS.Close();
                inFS = null;
                throw e;
            }
            StreamReader inStream = new StreamReader(inFS);
            description = "";
            Queue<byte[]> acs_queue = new Queue<byte[]>();
            Queue<string> hc_queue = new Queue<string>();


            while (inStream.EndOfStream == false)
            {
                string st = inStream.ReadLine();
                switch (st[0])
                {
                    case '[':
                        acs_queue.Enqueue(SRB.Ahex.Ahex.ahexToByteArray(st));
                        break;
                    case '#':
                        description += st.Substring(2) + "\n";
                        break;
                    default:
                        lodeArgument(st,hc_queue);
                        break;
                }
            }

            access_ba_a = acs_queue.ToArray();
            hardware_code = hc_queue.ToArray();

            acs_queue.Clear();
            hc_queue.Clear();

            inFS.Close();
            inFS = null;
            inStream.Close();
            inStream = null;

        }



        void lodeArgument(string st, Queue<string> hc_queue)
        {
            int sep_location = st.IndexOf('=');
            
            if (sep_location == -1)
            {
                return;
            }
            string key = st.Substring(0, sep_location);
            string value = st.Substring(sep_location + 1);
            switch (key)
            {
                case "srb_version":
                    srbVER.read(value);
                    break;
                case "node_version":

                    nodeVER.read(value);
                    break;
                case "time_stamp":
                    time_stamp = Convert.ToInt32(value);
                    break;
                case "node_type":
                    node_type = value;
                    break;
                case "hardware_code":
                    hc_queue.Enqueue(value);
                    break;
                case "build_time":
                    build_time = value;
                    break;
                default:
                    break;
            }

        }

        public IEnumerator<byte[]> GetEnumerator()
        {
            return ((IEnumerable<byte[]>)access_ba_a).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<byte[]>)access_ba_a).GetEnumerator();
        }
    }


    public class SupException : Exception
    {
        protected string file;
        public SupException(string message, string file) : base(message)
        {
            this.file = file;
        }
        public override string ToString()
        {
            return string.Format("file [{1}] error:{0}", Message, file);
        }

    }
    public class SupBadLineException : SupException
    {
        protected string line;
        protected int line_num;
        public SupBadLineException(string message, string file, string line,int line_num) : base(message, file)
        {
            this.line = line;
            this.line_num = line_num;
        }
        public override string ToString()
        {
            return string.Format("file [{1}] error in line{2},\"{3}\":{0}", Message, file, line_num, line);
        }

    }

}
