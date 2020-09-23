using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    public class Version : IComparable<Version>
    {
        private int major;
        private int branch;
        private int miner;
        private string name;

        public int Major { get => major; set => major = value; }
        public int Branch { get => branch; set => branch = value; }
        public int Miner { get => miner; set => miner = value; }
        public string Name { get => name; set => name = value; }

        public Version(string name = null, int major = -1, int branch = -1, int miner = -1)
        {
            this.name = name;
            this.major = major;
            this.branch = branch;
            this.miner = miner;
        }
        public void read(byte[] ba, int offset)
        {
            major = ba[offset++];
            branch = ba[offset++];
            miner = ba[offset++];
            if (major == 255) major = -1;
            if (branch == 255) branch = -1;
            if (miner == 255) miner = -1;

        }
        public void read(IReadAsByteArray ba, int offset)
        {
            major = ba[offset++];
            branch = ba[offset++];
            miner = ba[offset++];
            if (major == 255) major = -1;
            if (branch == 255) branch = -1;
            if (miner == 255) miner = -1;

        }
        public void read(string st)
        {
            string[] sta = st.Split(new char[1] { '.' });
            int point = 0;
            while (point != sta.Length)
            {
                if (sta[point++] == "V")
                {
                    break;
                }
            }
            if (sta[point] == "none")
            {
                major = -1;
                branch = -1;
                miner = -1;
                return;
            }
            major = Convert.ToInt32(sta[point++]);
            branch = Convert.ToInt32(sta[point++]);
            miner = Convert.ToInt32(sta[point++]);
            return;

        }
        public override string ToString()
        {
            string st;
            if (name == null)
            {
                st = "V.";
            }
            else
            {
                st = name + ".V.";
            }
            if (major == -1)
            {
                st += "none";
                return st;
            }
            st += major;
            if (branch != -1)
            {
                if (miner != -1)
                {
                    st += "." + branch + "." + miner;
                    return st;
                }
                else
                {
                    st += "." + branch + ".x";
                    return st;
                }
            }
            else
            {
                if (miner != -1)
                {
                    st += "." + miner;
                    return st;
                }
                else
                {
                    return st;
                }
            }
        }
        public override int GetHashCode()
        {
            if (this.major == -1)
            {
                return -1;
            }
            else
            {
                int miner = this.miner;
                int branch = this.branch;
                if (miner == -1)
                {
                    miner = 999;
                }
                if (branch == -1)
                {
                    branch = 999;
                }
                return this.major * 1000 * 1000 + branch * 1000 + miner;

            }

        }

        public int CompareTo(Version that)
        {
            if (this.major != that.major)
            {
                return this.major - that.major;
            }
            if (this.major == -1)
            {
                return 0;
            }
            if (this.branch != that.branch)
            {
                return 0;
            }
            return this.miner - that.miner;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static bool operator ==(Version lhs, Version rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator !=(Version lhs, Version rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }
        public static bool operator <(Version lhs, Version rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator >(Version lhs, Version rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator <=(Version lhs, Version rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        public static bool operator >=(Version lhs, Version rhs)
        {
            return lhs.CompareTo(rhs) >= 0;

        }
    }
}
