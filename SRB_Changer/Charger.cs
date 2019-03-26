using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB.Frame;

namespace SRB.NodeType.Charger
{
    public class Node : BaseNode
    {
        public int joy_rx { get => toJoy(6); }
        public int joy_ry { get => toJoy(7); }
        public int joy_lx { get => toJoy(8); }
        public int joy_ly { get => toJoy(9); }

        public bool handle_exist { get => (bank[3] & (1 << 0)) != 0; }

        public bool select { get => (bank[4] & (1 << 0)) == 0;}
        public bool L3 { get => (bank[4] & (1 << 1)) == 0; }
        public bool R3 { get => (bank[4] & (1 << 2)) == 0; }
        public bool start { get => (bank[4] & (1 << 3)) == 0; }

        public bool up { get => (bank[4] & (1 << 4)) == 0; }
        public bool right { get => (bank[4] & (1 << 5)) == 0; }
        public bool down { get => (bank[4] & (1 << 6)) == 0; }
        public bool left { get => (bank[4] & (1 << 7)) == 0; }

        public bool L2 { get => (bank[5] & (1 << 0)) == 0; }
        public bool R2 { get => (bank[5] & (1 << 1)) == 0; }
        public bool L1 { get => (bank[5] & (1 << 2)) == 0; }
        public bool R1 { get => (bank[5] & (1 << 3)) == 0; }

        public bool trag { get => (bank[5] & (1 << 4)) == 0; }
        public bool circle { get => (bank[5] & (1 << 5)) == 0; }
        public bool cross { get => (bank[5] & (1 << 6)) == 0; }
        public bool square { get =>( (bank[5] & (1 << 7)) == 0); }

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
            cfg_clu = new ConfigCluster(11, this);
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
                new byte[] {6,3,4,5,6,7,8,9,0,1,2}                    ,
                new byte[] {4,3,6,7,8,9,0,1,2}                   ,
                new byte[] {7,3,3,4,5,6,7,8,9,0,1,2}
            });
        }

        public Node(byte addr, ISRB_Master f = null)
            : base(addr, f)
        {
            init();
        }
        
        public Node(Node n)
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
            this.addAccess(0);
        }
        public override System.Windows.Forms.Control getClusterControl()
        {
            return new Ctrl(this);
        }
        public override string Describe()
        {
            return @"This node drivers two motors. Without speed or force sensor";
        }
    }
}
