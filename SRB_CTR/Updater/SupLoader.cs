using SRB.Frame.updater;
using System;
using System.Collections.Generic;
using System.IO;

namespace SRB_CTR
{
    class SupLoader
    {
        SupFile[] sup_files;
        public SupLoader(string path)
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

    }
}
