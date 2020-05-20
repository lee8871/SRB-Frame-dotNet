using SRB.Frame;
using System;

namespace SRB.NodeType.PhotoElecX6
{
    public class Interpreter : BaseNode.INodeInterpreter
    {
        internal MappingCluster Mapping0_clu;
        public override string Help_net_work =>
            "https://github.com/lee8871/SRB-Introduction/blob/master/SRB%E5%8F%8C%E7%94%B5%E6%9C%BA%E8%8A%82%E7%82%B9.md";
        
        public int value(int num) => (short)bank.getBankUshort(num*2);



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
                new byte[] {13,0,0,1,2,3,4,5,6,7,8,9,10,11,12}              ,
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
