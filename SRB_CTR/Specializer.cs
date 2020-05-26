using SRB.Frame;

namespace SRB_CTR
{
    class Specializer : Node.ISpecializer
    {
        public void specializeNode(Node n)
        {
            switch (n.NodeType)
            {
                case "MotorX2":
                    n.Datas = new SRB.NodeType.Du_motor.Interpreter(n);
                    break;
                case "Joystick":
                    n.Datas = new SRB.NodeType.Joystick.Interpreter(n);
                    break;
                case "LiBatT2":
                    n.Datas = new SRB.NodeType.Charger.Interpreter(n);
                    break;
                case "PhotoElecX6":
                    n.Datas = new SRB.NodeType.PhotoElecX6.Interpreter(n);
                    break;
                default:
                    n.Datas = new SRB.Frame.untyped.Interpreter(n);
                    break;
            }
        }
    }
}
