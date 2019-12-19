using SRB.Frame;

namespace SRB_CTR
{
    public class UpdateAll
    {
        private SrbOnelineMaster frame;
        public UpdateAll(SrbOnelineMaster master)
        {
            sup_loader = new SupLoader("D:/SRB-Update");
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
        public void burnAll()
        {
            foreach (BaseNode n in frame.Bus)
            {
                if (n.Is_in_update == true)
                {
                    string hc = n.Updater.Hardware_code;
                    var sup_file = sup_loader.findByHardwareCode(hc);
                    if (sup_file != null)
                    {
                        n.Updater.loadFile(sup_file);
                        n.Updater.update();
                        n.gotoNormalMode();
                    }


                }
            }

        }

    }
}
