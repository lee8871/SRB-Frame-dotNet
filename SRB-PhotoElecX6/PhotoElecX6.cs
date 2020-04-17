using SRB.Frame;
using System;

namespace SRB.NodeType.PhotoElecX6
{
    public class Interpreter : BaseNode.INodeInterpreter
    {
        internal ConfigCluster motor_clu;
        internal AdjustCluster adj_clu;
        internal MappingCluster Mapping0_clu;
        public override string Help_net_work =>
            "https://github.com/lee8871/SRB-Introduction/blob/master/SRB%E5%8F%8C%E7%94%B5%E6%9C%BA%E8%8A%82%E7%82%B9.md";

        public int Speed_a { set => setSpeedA(value); }
        public int Speed_b { set => setSpeedB(value); }

        public int Brake_a { set => setBrakeA(value); }
        public int Brake_b { set => setBrakeB(value); }

        private void setBrakeA(int value)
        {
            int temp;
            temp = value;
            temp |= (1 << 15);
            bank[0] = (byte)temp;
            bank[1] = (byte)(temp >> 8);
        }

        private void setBrakeB(int value)
        {
            int temp;
            temp = value;
            temp |= (1 << 15);
            bank[2] = (byte)temp;
            bank[3] = (byte)(temp >> 8);
        }

        public void setSpeedA(int speed)
        {
            int temp;
            temp = speed;
            temp &= ~(1 << 15);
            bank[0] = (byte)temp;
            bank[1] = (byte)(temp >> 8);
        }
        public void setSpeedB(int speed)
        {
            int temp;
            temp = speed;
            temp &= ~(1 << 15);
            bank[2] = (byte)temp;
            bank[3] = (byte)(temp >> 8);
        }
        public void init()
        {
            motor_clu = new ConfigCluster(Node);
            clusters[motor_clu.CID] = motor_clu;
            adj_clu = new AdjustCluster(Node);
            clusters[adj_clu.CID] = adj_clu;
            Mapping0_clu = new MappingCluster(3, Node, "Mapping0");
            clusters[Mapping0_clu.CID] = Mapping0_clu;

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();

        }

        private void updataMapping(object sender, EventArgs e)
        {
            Node.bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[] {0,4,0,1,2,3}                  ,
                new byte[] {0,2,0,2}             ,
                new byte[] {0,4,2,3,0,1}
            });
        }
        public Interpreter(BaseNode n)
            : base(n)
        {
            init();
        }


        protected override System.Windows.Forms.Control createControl()
        {
            return new Ctrl(Node);
        }
        public override string Describe => @"This node drivers two motors. Without speed or force sensor";
    }
}
