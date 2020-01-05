using SRB.Frame;
using System.Threading;

namespace SRB_CTR
{
    public class UpdateAll
    {
        private SrbOnelineMaster frame;
        public UpdateAll(SrbOnelineMaster master)
        {
            sup_loader = new SupLoader();
            this.frame = master;
        }
        public void gotoUpdateModeAll()
        {
            foreach (BaseNode n in frame.Bus)
            {
                if (n.Is_in_update == false)
                {
                    n.gotoUpdateMode();
                }
            }
        }
        public void gotoUpdateModeAllFromPowerOn()
        {
            BaseNode.SrbUpdater.holdAll(frame.Bus);
        }
        SupLoader sup_loader;
        public SupLoader Sup_loader => sup_loader;
        public void loadFiles(string sup_files_path)
        {
            sup_loader.LoadFiles(sup_files_path);
        }
        public delegate void appendInfo(string st);
        private Thread burning_thread;
        private appendInfo dAppendInfo;
        public void burnAll(appendInfo delegateInfo)
        {
            dAppendInfo = delegateInfo;
            if (sup_loader.Is_file_loaded)
            {
                burning_thread = new Thread(new ThreadStart(burnAllTh));
                burning_thread.Start();
            }
            else
            {
                dAppendInfo(null);
                dAppendInfo("Update all fail. No sup file.\n");

            }
        }
        private void burnAllTh()
        {
            dAppendInfo(null);
            int totle_node = 0;
            foreach (BaseNode n in frame.Bus)
            {
                if (n.Is_in_update == true)
                {
                    totle_node++;
                }
            }
            if(totle_node == 0)
            {
                dAppendInfo("No Node in update mode. " +
                    "You may connect new nodes or set some nodes to update mode, than try burn all.\n");
            }
            else
            {
                dAppendInfo(string.Format(
                    "{0} node(s) waiting to be burn.\n\n", totle_node));
                int node_counter = 0;
                foreach (BaseNode n in frame.Bus)
                {
                    node_counter++;
                    if (n.Is_in_update == true)
                    {
                        string hc = n.Updater.Hardware_code;
                        dAppendInfo(string.Format(
                            "node {0}/{1} burning. hardware code is {2}\n", 
                            node_counter,totle_node, hc));

                        var sup_file = sup_loader.findByHardwareCode(hc);
                        if (sup_file != null)
                        {
                            n.Updater.loadFile(sup_file);
                            n.Updater.update();
                            n.gotoNormalMode();
                            dAppendInfo(string.Format(
                                "\tburning done\n", hc));
                        }
                        else
                        {
                            dAppendInfo(string.Format(
                                "\t.sup file not found for {0}, burning cancled", hc));
                        }

                    }
                }
            }
        }
    }
}
