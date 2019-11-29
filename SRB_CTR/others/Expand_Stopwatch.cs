using System.Diagnostics;


namespace SRB_CTR
{
    internal static class Expand_Stopwatch
    {
        static public double getElapsedMs(this Stopwatch sw)
        {
            return (1000.0 * sw.ElapsedTicks) / Stopwatch.Frequency; ;
        }
    }
}
