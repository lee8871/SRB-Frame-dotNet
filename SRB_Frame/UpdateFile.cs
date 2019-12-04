using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    class UpdateFile
    {
        public Queue<byte[]> acccesses = new Queue<byte[]>();
        string path2 = @"D:\SRB-Update\LED-test-SLB11822.sup";
        string path = @"D:\SRB-Update\SRB-MotorX2-SLB11822.sup";
        public UpdateFile()
        {
            FileStream inFS = null;
            StreamReader inStream = null;/*
            try
            {
                inFS = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (FileNotFoundException)
            {
                throw new PathException(string.Format("没有找到Hex输入文件：{0}", in_path));
            }
            catch (DirectoryNotFoundException)
            {
                throw new PathException(string.Format("没有找到Hex输入文件路径：{0}", in_path));
            }
            catch (PathTooLongException)
            {
                throw new PathException(string.Format("Hex输入文件路径超过长度限制：{0}", in_path));
            }*/
            inFS = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            inStream = new StreamReader(inFS);

            while (inStream.EndOfStream==false)
            {
                string st = inStream.ReadLine();
                switch (st[0])
                {
                    case '[':
                        acccesses.Enqueue(SRB.Ahex.Ahex.ahexToByteArray(st));
                        break;
                    default:
                        break;
                }
            }
            inFS.Close();
            inFS = null;
            inStream.Close();
            inStream = null;
        }



    }
}
