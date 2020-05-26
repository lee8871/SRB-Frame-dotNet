using SRB.Frame;
using System;

namespace SRB.Frame.untyped
{
    public class Interpreter : Node.INodeInterpreter
    {
        public override string Help_net_work => "";
      
        public void init()
        {


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
        public override string Describe => @"
This node has an unkonw type, you may scan again.";
    }
}
