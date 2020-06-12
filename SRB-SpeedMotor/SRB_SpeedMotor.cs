using SRB.Frame;
using System;

namespace SRB.NodeType.SpeedMotor
{
    public class Interpreter : Node.INodeInterpreter
    {
        internal ConfigCluster motor_clu;
        internal AdjustCluster adj_clu;
        internal MappingCluster Mapping0_clu;
        public new Node Node => base.Node;
        public new IBus Bus => base.Bus;
        public override string Help_net_work =>
            "https://github.com/lee8871/SRB-Introduction/blob/master/SRB%E5%8F%8C%E7%94%B5%E6%9C%BA%E8%8A%82%E7%82%B9.md";

        public int target_speed { set => setSpeed((short)value); }
        public int set_displacement { set => bank.setBankUshort((ushort)value, 2); }// setSpeedB(value); }
        public int set_acceleration { set => bank.setBankUshort((ushort)value, 4);}
        public int sensor_speed { get => (short)bank.getBankUshort(6); }
        public int odometer { get => (int)bank.getBankUint(8); }

        private void setSpeed(short value)
        {
            bank.setBankUshort((ushort)value, 0); ;
        }

        public void init()
        {
            //motor_clu = new ConfigCluster(Node);
           // adj_clu = new AdjustCluster(Node);
            Mapping0_clu = new MappingCluster(3, Node, "Mapping0");

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();

        }

        private void updataMapping(object sender, EventArgs e)
        {
            Node.bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[] {8,6,6,7,8,9,10,11,12,13,14,0,1,2,3,4,5}                 ,
                new byte[] {0,2,0,2}             ,
                new byte[] {0,4,2,3,0,1}
            });
        }
        public Interpreter(Node n)
            : base(n)
        {
            init();
        }

        protected override System.Windows.Forms.Control createControl()
        {
            return new Ctrl(Node);
        }
        public override string Describe => @"单电机控制节点";
    }
}
