using System;
using System.Collections.Generic;
using System.Drawing;
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
        public double y_min;
        public double y_max;
        public double x_location;

        public (double x,double y) toMathPoint(float x, float y)
        {
            return (this.x.toValue(x), this.y.toValue(y));
        }
        public (double x, double y) toMathPoint(PointF p)
        {
            return (this.x.toValue(p.X), this.y.toValue(p.Y));
        }
        public PointF toImagePoint(double x, double y)
        {
            return new PointF(this.x.toImage(x), this.y.toImage(y));
        }

    }
    struct ChartDimension
    {
        public double zoom;
        /// <summary>
        /// shift 是数学坐标系下，两条坐标线之间的数值距离
        /// </summary>
        public double grid_size;
        /// <summary>
        /// shift 是数学坐标系下，显示框原点到数值坐标原点的像素距离
        /// </summary>
        public float shift;
        public bool mirror;
        public dDoubleToString ToStr;
        
        public double toValue(float v)
        {
            if (mirror == false)
            {
                return (v - shift)/zoom;
            }
            else
            {

                return (-v - shift)/zoom ;
            }
        }
        public float toImage(double v)
        {
            if (mirror == false) {
                return (float) (v * zoom + shift);
            }
            else
            {

                return (float)(-(v * zoom + shift));
            }
        }
        public double toValueDst(float v)
        {
            return (v / zoom);
        }
        public float toImageDst(double v)
        {
            return (float)(v * zoom);
        }

    }
}
