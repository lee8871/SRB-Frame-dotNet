using System;
using System.Drawing;

namespace SRB_Chart
{
    public abstract class IPlot
    {
        Chart chart = null;
        bool visiable = false;
        string name;
        Pen line = new Pen(Color.DarkGreen, 2f);
        public abstract int Length { get; }
        object tag;
        public abstract Dot this[int i] { get; }
        public virtual double X_min{
            get{
                if (this.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return this[0].X;
                }
            }
        }
        public virtual double X_max
        {
            get
            {
                if (this.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return this[this.Length-1].X;
                }
            }
        }

        public virtual int findBefore(double x,int len)
        {
            int i = 0;
            for (; i < len; i++)
            {
                if(this[i].X >= x)
                {
                    if (i != 0)
                    {
                        return i-1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return len - 1;
        }
        public IPlot()
        {
            name = this.ToString();
        }
        public Chart Chart
        {
            get => chart;
            set
            {
                chart = value;
            }
        }
        public bool Visiable { 
            get => visiable;
            set
            {
                visiable = value;
                chart?.PlotVisibleChange(this);
            }
        }
        public string Name { 
            get => name; 
            set
            {
                name = value;
                chart?.PlotNameChange(this);
            }
        }

        public Color Color {
            get => line.Color; 
            set
            {
                line = new Pen(value, line.Width) ;
                chart?.PlotColorChange(this);
            }
        }

        public object Tag { get => tag; set => tag = value; }
        public Pen Line => line;
    }
    public struct Dot
    {
        double y;
        double x;
        public double Y { get => y; set => y = value; }
        public double X { get => x; set => x = value; }
        public Dot(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
    }


}
