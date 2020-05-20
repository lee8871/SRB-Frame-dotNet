using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    class ByteExplanation
    {
        public byte this[int i] { get => ba[i]; set => ba[i] = value; }
        private byte[] ba;
        public List<Trasport> trasportLIST = new List<Trasport>(); 
        ByteExplanation(byte[] ba)
        {
            this.ba = ba;
        }
        ByteExplanation(int len)
        {
            this.ba = new byte[len];
        }

        public class Trasport
        {
            Type T;
            int offset;
            object obj;
            ByteExplanation parent;

        }

    }
}
