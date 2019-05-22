namespace SRB.port
{
    internal class LoopQueuePointer
    {
        private int size;
        private int point;
        public int Point { get => point;  }

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
        public int pointMove()
        {
            int rev = point;
            point++;
            if (point >= size)
            {
                point -= size;
            }
            return rev;
        }
        public static int operator - (LoopQueuePointer a, LoopQueuePointer b){        
            int rev = a.point - b.point;
            if (rev < 0) rev += a.size;
            return rev;
        }
        public static bool operator== (LoopQueuePointer a, LoopQueuePointer b)
        {
            return (a.point == b.point);
        }
        public static bool operator!=(LoopQueuePointer a, LoopQueuePointer b)
        {
            return (a.point != b.point);
        }
        public override string ToString()
        {
            return point + " in " + size;
        }

    }
}