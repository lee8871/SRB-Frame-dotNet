using SRB.Frame;
using System;

namespace SRB.NodeType.Joystick
{
    public class Interpreter : Node.INodeInterpreter
    {
        public int joy_rx => toJoy(5);
        public int joy_ry => toJoy(6);
        public int joy_lx => toJoy(7);
        public int joy_ly => toJoy(8);

        public int pressure_u => bank.getBankByte(9);
        public int pressure_r => bank.getBankByte(10);
        public int pressure_d => bank.getBankByte(11);
        public int pressure_l => bank.getBankByte(12);

        public int pressure_trag => bank.getBankByte(13);
        public int pressure_cir => bank.getBankByte(14);
        public int pressure_cros => bank.getBankByte(15);
        public int pressure_squ => bank.getBankByte(16);

        public int pressure_l1 => bank.getBankByte(17);
        public int pressure_r1 => bank.getBankByte(18);
        public int pressure_l2 => bank.getBankByte(19);
        public int pressure_r2 => bank.getBankByte(20);

        public bool select => !bank.getBankBool(3, 0);
        public bool L3 => !bank.getBankBool(3, 1);
        public bool R3 => !bank.getBankBool(3, 2);
        public bool start => !bank.getBankBool(3, 3);

        public bool up => !bank.getBankBool(3, 4);
        public bool right => !bank.getBankBool(3, 5);
        public bool down => !bank.getBankBool(3, 6);
        public bool left => !bank.getBankBool(3, 7);

        public bool L2 => !bank.getBankBool(4, 0);
        public bool R2 => !bank.getBankBool(4, 1);
        public bool L1 => !bank.getBankBool(4, 2);
        public bool R1 => !bank.getBankBool(4, 3);

        public bool trag => !bank.getBankBool(4, 4);
        public bool circle => !bank.getBankBool(4, 5);
        public bool cross => !bank.getBankBool(4, 6);
        public bool square => !bank.getBankBool(4, 7);



        public int rumble_l_strength { set => bank.setBankByte((byte)(value.enterRound(0, 255)), 2); }
        public int rumble_l { set => bank.setBankByte(((byte)value.enterRound(0, 255)), 0); }
        public int rumble_r { set => bank.setBankByte(((byte)value.enterRound(0, 255)), 1); }

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
            cfg_clu = new ConfigCluster(Node);
            Mapping0_clu = new MappingCluster(3, Node, "Mapping0");
            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();
        }

        private void updataMapping(object sender, EventArgs e)
        {
            Node.bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[]{6,3, 3,4,5,6,7,8,        0,1,2},
                new byte[]{4,3, 5,6,7,8,            0,1,2},
                new byte[]{18,3,    3,4,5, 6,7,8,   9,10,11,   12,13,14,   15,16,17,  18,19,20 ,    0,1,2},
            });
        }


        public Interpreter(Node n)
            : base(n)
        {
            init();
        }

        protected override System.Windows.Forms.Control createControl()
        {
            return new PS2HandleControl(Node);
        }
        public override string Describe => @"";
    }
}
