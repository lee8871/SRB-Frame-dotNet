using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB.Frame;
using SRB.Frame.Cluster;

namespace SRB.NodeType.PS2_Handle
{
    public class Node : BaseNode
    {
        public int joy_rx { get => toJoy(6); }
        public int joy_ry { get => toJoy(7); }
        public int joy_lx { get => toJoy(8); }
        public int joy_ly { get => toJoy(9); }
        

        public bool handle_exist { get => getBankBool(3,0); }

        public bool select { get => !getBankBool(4, 0); }
        public bool L3 { get =>! getBankBool(4, 1); }
        public bool R3 { get =>! getBankBool(4, 2); }
        public bool start { get => !getBankBool(4, 3); }

        public bool up { get =>! getBankBool(4, 4); }
        public bool right { get => !getBankBool(4, 5); }
        public bool down { get =>!getBankBool(4, 6); }
        public bool left { get => !getBankBool(4, 7); }

        public bool L2 { get =>! getBankBool(5, 0); }
        public bool R2 { get =>! getBankBool(5, 1); }
        public bool L1 { get =>! getBankBool(5, 2); }
        public bool R1 { get =>! getBankBool(5, 3); }

        public bool trag { get =>! getBankBool(5, 4); }
        public bool circle { get => !getBankBool(5, 5); }
        public bool cross { get =>! getBankBool(5, 6); }
        public bool square { get =>! getBankBool(5, 7); }

        public int rumble_l_strength { set => setBankByte((byte)(value.enterRound(0, 255)), 2); }
        public int rumble_l { set => setBankByte(((byte)value.enterRound(0, 255)), 0); }
        public int rumble_r { set => setBankByte(((byte)value.enterRound(0, 255)), 1); }

        public override string Help_net_work => 
            "https://github.com/lee8871/SRB-Introduction/blob/master/SRB%E6%89%8B%E6%9F%84%E8%8A%82%E7%82%B9.md";
        public void setRumble(int rumble)
        {
            rumble = rumble.enterRound(0, 255);
        }
        public int toJoy(int byte_location)
        {
            int data = bank[byte_location];
            data -= 128;
            if (data < 0)
            { 
                data++;
            }
            return data;
        }

        internal ConfigCluster cfg_clu;
        internal MappingCluster Mapping0_clu;

        public void init()
        {
            cfg_clu = new ConfigCluster(this);
            clusters[cfg_clu.Clustr_ID] = cfg_clu;

            Mapping0_clu = new MappingCluster(3, this,"Mapping0");
            clusters[Mapping0_clu.Clustr_ID] = Mapping0_clu;

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();
        }

        private void updataMapping(object sender, EventArgs e)
        {
            bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[] {7,3,3,4,5,6,7,8,9,0,1,2}                  ,
                new byte[] {4,3,6,7,8,9,0,1,2}             ,
                new byte[] {6,3,4,5,6,7,8,9,0,1,2}
            });
        }

        public Node(byte addr, ISRB_Master f = null)
            : base(addr, f)
        {
            init();
        }
        
        public Node(BaseNode n)
            : base(n)
        {
            init();
        }
        public void bulidUpD0()
        {
            this.addAccess(0, 0);
        }
        public void bulidUpD0(ushort ms)
        {
            rumble_l = ms;
            this.addAccess(0);
        }
        public override System.Windows.Forms.Control getClusterControl()
        {
            return new PS2HandleControl(this);
        }
        public override string Describe()
        {
            return @"";
        }
    }
}
