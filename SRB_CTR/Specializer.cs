using SRB.Frame;

namespace SRB_CTR
{
    class Specializer : BaseNode.ISpecializer
    {
        public void specializeNode(BaseNode n)
        {
            switch (n.NodeType)
            {
                case "Du_Motor":
                    n.Datas = new SRB.NodeType.Du_motor.Interpreter(n);
                    break;
                case "Ps2_Handle":
                    n.Datas = new SRB.NodeType.PS2_Handle.Interpreter(n);
                    break;
                case "Charger_2LiB":
                    n.Datas = new SRB.NodeType.Charger.Interpreter(n);
                    break;
                default:
                    n.Datas = new SRB.Frame.untyped.Interpreter(n);
                    break;
            }
        }
    }
}
