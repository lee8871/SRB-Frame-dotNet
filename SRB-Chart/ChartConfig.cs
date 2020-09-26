using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB_Chart
{
    public delegate string dDoubleToString(double x);
    delegate double dImageToValue(float x, ChartDimension cfg);
    delegate float dValueToImage(double x, ChartDimension cfg);
    class ChartConfig
    {
        public ChartDimension x;
        public ChartDimension y;
    }
    struct ChartDimension
    {
        public double zoom;
        public double grid_size;
        public double min;
        public double max;
        public dDoubleToString ToStr;
        
        public dImageToValue imageToValue;
        public dValueToImage valueToImage;
    }
}
