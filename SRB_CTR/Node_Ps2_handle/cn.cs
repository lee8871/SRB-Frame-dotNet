using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB_CTR.SRB_Frame;
namespace SRB_CTR.nsBrain.Node_PS2_handle
{
    class Cn : Node
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

        public int rumble { set => bankWrite((byte)(value.enterRound(0, 255)), 2); }
        public ushort rumble_ms { set => bankWrite(value, 0); }

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



        public Cluster_handle_cfg.Clu cfg_clu;
        public Cluster_mapping.Clu Mapping0_clu;

        public void init()
        {
            cfg_clu = new Cluster_handle_cfg.Clu(11, this);
            clusters[cfg_clu.Clustr_ID] = cfg_clu;

            Mapping0_clu = new Cluster_mapping.Clu(3, this);
            clusters[Mapping0_clu.Clustr_ID] = Mapping0_clu;



            Mapping0_clu.read();
            bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[] {6,3,4,5,6,7,8,9,0,1,2}                    ,
                new byte[] {4,3,6,7,8,9,0,1,2}                   ,
                new byte[] {7,3,3,4,5,6,7,8,9,0,1,2}
            });
            //led_phase_clu.read();
        }
        public Cn(byte addr, SrbFrame f = null)
            : base(addr, f)
        {
            init();
        }
        
        public Cn(Node n)
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
            rumble_ms = ms;
            this.addAccess(0);
        }
        //protected override void dataAccessDone(Access ac)
        //{
        //    if (ac.Port == Access.PortEnum.D0)
        //    {
        //        try
        //        {
        //            if (ac.Status == Access.StatusEnum.RecvedDone)
        //            {
        //                // color_now = Color.FromArgb(ac._recv_data[1], ac._recv_data[2], ac._recv_data[0]);
        //                this.joy_rx = ac.Recv_data[2];
        //                this.joy_ry = ac.Recv_data[3];
        //                this.joy_lx = ac.Recv_data[4];
        //                this.joy_ly = ac.Recv_data[5];
        //                this.select = (ac.Recv_data[0] & (1 << 0)) == 0;
        //                this.L3 = (ac.Recv_data[0] & (1 << 1)) == 0;
        //                this.R3 = (ac.Recv_data[0] & (1 << 2)) == 0;
        //                this.start = (ac.Recv_data[0] & (1 << 3)) == 0;

        //                this.up = (ac.Recv_data[0] & (1 << 4)) == 0;
        //                this.right = (ac.Recv_data[0] & (1 << 5)) == 0;
        //                this.down = (ac.Recv_data[0] & (1 << 6)) == 0;
        //                this.left = (ac.Recv_data[0] & (1 << 7)) == 0;

        //                this.L2 = (ac.Recv_data[1] & (1 << 0)) == 0;
        //                this.R2 = (ac.Recv_data[1] & (1 << 1)) == 0;
        //                this.L1 = (ac.Recv_data[1] & (1 << 2)) == 0;
        //                this.R1 = (ac.Recv_data[1] & (1 << 3)) == 0;

        //                this.trag = (ac.Recv_data[1] & (1 << 4)) == 0;
        //                this.circle = (ac.Recv_data[1] & (1 << 5)) == 0;
        //                this.cross = (ac.Recv_data[1] & (1 << 6)) == 0;
        //                this.square = (ac.Recv_data[1] & (1 << 7)) == 0;

        //            }
        //        }
        //        catch (System.IndexOutOfRangeException)
        //        { }
        //    }
        //}
        internal override System.Windows.Forms.Control getClusterControl()
        {
            return new Ctrl(this);
        }
        public override string Describe()
        {
            return @"This node drivers two motors. Without speed or force sensor";
        }
    }
}
