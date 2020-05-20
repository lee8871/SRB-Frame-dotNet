using SRB.Frame.updater;
using System;
using System.Collections.Generic;
using System.IO;


namespace SRB.Frame.updater
{
    public class SupLoader
    {
        SupFile[] sup_files = new SupFile[0];
        public int File_counter => sup_files.Length;
        public bool Is_file_loaded => (sup_files.Length != 0);
        public SupLoader()
        {

        }
        public void LoadFiles(string path)
        {
            Queue<SupFile> sf_queue = new Queue<SupFile>();
            DirectoryInfo d = new DirectoryInfo(path);

            foreach (var file in d.GetFiles("*.sup"))
            {
                SupFile sf = new SupFile(file.FullName);
                sf_queue.Enqueue(sf);
            }
            sup_files = sf_queue.ToArray();
        }
        public SupFile findByHardwareCode(string hc)
        {
            SupFile rev_sf=null;
            foreach(var sf in sup_files)
            {
                foreach(string file_hc in sf.Hardware_codes_array)
                {
                    if(hc == file_hc)
                    {
                        if (rev_sf != null)
                        {
                            throw new Exception("TODO add select file");
                        }
                        rev_sf = sf;
                    }
                }
            }
            return rev_sf;
        }
        public override string ToString()
        {
            string rev = string.Format("{0} file(s) loaded:\n", File_counter );
            foreach (var file in sup_files)
            {
                rev += string.Format("{0}, SRBv.{1}, NODEv.{2}\n\tFor hardware: ", 
                      file.Node_type, file.srbVER, file.nodeVER);
                foreach(var hc in file.Hardware_codes_array)
                {
                    rev += hc + ", ";
                }
                rev += "\n";
            }
            return rev;

        }

    }
}
