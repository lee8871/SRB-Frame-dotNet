using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Support
{
    static class FileRecordMaker
    {
        static string base_path = "./log";
        static public string getStringFile(string node_type, string name,string type)
        {
            string path = $"{base_path}/{node_type}/{System.DateTime.Now.ToString("yy-MM-dd")}/";
            System.IO.Directory.CreateDirectory(path);//如果文件夹不存在就创建它
            return $"{path}/{name}-{ System.DateTime.Now.ToString("HHmmss")}.{type}";
        }


    }
}
