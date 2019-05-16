using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace SRB_CTR
{
    static class Expand_Stopwatch
    {
        static public double getElapsedMs(this Stopwatch sw)
        {
            return (1000.0 * sw.ElapsedTicks) / Stopwatch.Frequency; ;
        }
    }
}
