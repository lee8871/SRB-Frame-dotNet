using SRB.Frame;
using SRB.Frame.Cluster;
using System;

namespace SRB.NodeType.PS2_Handle
{
    public class Node : BaseNode
    {
        public int joy_rx { get => toJoy(5); }
        public int joy_ry { get => toJoy(6); }
        public int joy_lx { get => toJoy(7); }
        public int joy_ly { get => toJoy(8); }

        public int pressure_u { get => getBankByte(9); }
        public int pressure_r { get => getBankByte(10); }
        public int pressure_d { get => getBankByte(11); }
        public int pressure_l { get => getBankByte(12); }

        public int pressure_trag { get => getBankByte(13); }
        public int pressure_cir { get => getBankByte(14); }
        public int pressure_cros { get => getBankByte(15); }
        public int pressure_squ { get => getBankByte(16); }

        public int pressure_l1 { get => getBankByte(17); }
        public int pressure_r1 { get => getBankByte(18); }
        public int pressure_l2 { get => getBankByte(19); }
        public int pressure_r2 { get => getBankByte(20); }

        public bool select { get => !getBankBool(3, 0); }
        public bool L3 { get => !getBankBool(3, 1); }
        public bool R3 { get => !getBankBool(3, 2); }
        public bool start { get => !getBankBool(3, 3); }

        public bool up { get => !getBankBool(3, 4); }
        public bool right { get => !getBankBool(3, 5); }
        public bool down { get => !getBankBool(3, 6); }
        public bool left { get => !getBankBool(3, 7); }

        public bool L2 { get => !getBankBool(4, 0); }
        public bool R2 { get => !getBankBool(4, 1); }
        public bool L1 { get => !getBankBool(4, 2); }
        public bool R1 { get => !getBankBool(4, 3); }

        public bool trag { get => !getBankBool(4, 4); }
        public bool circle { get => !getBankBool(4, 5); }
        public bool cross { get => !getBankBool(4, 6); }
        public bool square { get => !getBankBool(4, 7); }



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
            clusters[cfg_clu.CID] = cfg_clu;

            Mapping0_clu = new MappingCluster(3, this, "Mapping0");
            clusters[Mapping0_clu.CID] = Mapping0_clu;

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();
        }

        private void updataMapping(object sender, EventArgs e)
        {
            bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[]{6,3, 3,4,5,6,7,8,        0,1,2},
                new byte[]{4,3, 5,6,7,8,            0,1,2},
                new byte[]{18,3,    3,4,5, 6,7,8,   9,10,11,   12,13,14,   15,16,17,  18,19,20 ,    0,1,2},
            });
        }

        public Node(byte addr, IMaster f = null)
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
