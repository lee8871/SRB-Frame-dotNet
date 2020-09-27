using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB_Chart
{
    public class ChartConfig
    {
        public ChartDimension x;
        public ChartDimension y;
        public double y_min;
        public double y_max;
        public double x_min;
        public double x_max;


        public ChartConfig(ChartDimension.dDoubleToString varToStr)
        {
            x.zoom = 0.5;
            y.zoom = 0.5;
            x.grid_size = 100;
            y.grid_size = 100;
            x.mirror = false;
            y.mirror = true;
            y.ToStr = varToStr;
            x.ToStr = varToStr;
            y_min = -500;
            y_max = 1000;
            x_min = 0;
            x_max = 2000;
        }

        public ChartConfig(ChartConfig that)
        {
            x = that.x;
            y = that.y;

            y_min = that.y_min;
            y_max = that.y_max;
            x_min = that.x_min;
            x_max = that.x_max;
        }


        public (double x,double y) toMathPoint(float x, float y)
        {
            return (this.x.toValue(x), this.y.toValue(y));
        }
        public (double x, double y) toMathPoint(PointF p)
        {
            return (this.x.toValue(p.X), this.y.toValue(p.Y));
        }
        public (float x, float y) toImagePoint(double x, double y)
        {
            return (this.x.toImage(x), this.y.toImage(y));
        }
        public void checkOrigin()
        {
            if (y.mirror)
            {
                y.shift = -y.toImageDst(y_max);
            }
            else
            {
                y.shift = -y.toImageDst(y_min);
            }

            if (x.mirror)
            {
                x.shift = -x.toImageDst(x_max);
            }
            else
            {
                x.shift = -x.toImageDst(x_min);
            }
        }

    }
    public struct ChartDimension
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

        public delegate string dDoubleToString(double x);
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
