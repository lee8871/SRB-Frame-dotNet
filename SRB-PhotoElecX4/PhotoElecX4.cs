using SRB.Frame;
using System;

namespace SRB.NodeType.PhotoElecX4
{
    public class sGroupPheValue
    {
        protected virtual int size => 8;
        protected byte[] ba;
        protected int diff;

        public void init(byte[] ba, int diff)
        {
            this.ba = ba;
            this.diff = diff;
        }
        public void fork()
        {
            byte[] my_ba = new byte[size];
            for(int i=0;i< size; i++)
            {
                my_ba[i] =  ba[diff + i];
            }
            ba = my_ba;
            diff = 0;
        }



    } 


    public class Interpreter : Node.INodeInterpreter
    {
        public int Sensor_num { get => 4; }
        internal MappingCluster Mapping0_clu;
        public override string Help_net_work =>
            "https://github.com/lee8871/SRB-Introduction/blob/master/SRB%E5%8F%8C%E7%94%B5%E6%9C%BA%E8%8A%82%E7%82%B9.md";
        public int value(int num) => (short)bank[1,2];
        
        public int value(int num) => (short)bank.getBankUshort(num * 2);

        public byte gpv_in_bank => bank.getBankByte(3);
        public void init()
        {
            Mapping0_clu = new MappingCluster(3, Node, "Mapping0");

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();
        }
        
        private void updataMapping(object sender, EventArgs e)
        {
            Node.bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[] {9,1,	3,4,5,6,7,8,9,10,11,2,},
                new byte[] {9,3,	3,4,5,6,7,8,9,10,11,0,1,2,},
                new byte[] {9,1,    3,4,5,6,7,8,9,10,11,2,},
            });
        }
        public Interpreter(Node n)
            : base(n)
        {
            n.initSyncClu();
            init();
            n.eBankChangeByAccess += N_eBankChangeByAccess;
        }

        private void N_eBankChangeByAccess(object sender, EventArgs e)
        {

        }

        protected override System.Windows.Forms.Control createControl()
        {
            return new Ctrl(Node);
        }
        public override string Describe => @"This node drivers two motors. Without speed or force sensor";


    }
}
