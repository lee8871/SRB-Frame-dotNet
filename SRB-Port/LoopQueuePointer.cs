namespace SRB.port
{
    internal class LoopQueuePointer
    {
        private int size;
        private int point;

        public LoopQueuePointer(int s)
        {
            if (s <= 0)
            {
                throw new System.Exception("Size 需要是一个正数.");
            }
            size = s;
            point = 0;
        }
        public LoopQueuePointer(LoopQueuePointer p)
        {
            size = p.size;
            point = p.point;
        }
        public void jumpTo(LoopQueuePointer b)
        {
            point = b.point;
        }
        public static implicit operator int(LoopQueuePointer a)
        {
            return a.point;
        }
        public static LoopQueuePointer operator ++(LoopQueuePointer a)
        {
            a.point++;
            if (a.point >= a.size)
            {
                a.point -= a.size;
            }
            return a;
        }
        public static LoopQueuePointer operator --(LoopQueuePointer a)
        {
            a.point--;
            if (a.point < 0)
            {
                a.point += a.size;
            }
            return a;
        }
        public static int operator -(LoopQueuePointer a, LoopQueuePointer b)
        {
            int rev = a.point - b.point;
            if (rev < 0) rev += a.size;
            return rev;
        }

        public static bool operator ==(LoopQueuePointer a, LoopQueuePointer b)
        {
            return (a.point == b.point);
        }
        public static bool operator !=(LoopQueuePointer a, LoopQueuePointer b)
        {
            return (a.point != b.point);
        }
        public override string ToString()
        {
            return point + " in " + size;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}