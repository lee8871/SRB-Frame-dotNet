using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SRB_Chart
{
    public partial class Chart : Control
    {
        double y_zoom = 0.5;
        double x_zoom = 0.5;
        double x_location = 520;
        double y_min= -120;
        double y_max= 330;
        double x_grid_size = 100;
        double y_grid_size = 100;
        List<IPlot> plots;

        IPlot forcu_on_plot = null;
        PointF origin_in_pixel;

        public delegate string dDoubleToString(double x);
        public dDoubleToString y_ToStr;
        public dDoubleToString x_ToStr;
        public string varToStr(double val)
        {
            return val.ToString("F0");
        }

        public Chart()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            plots = new List<IPlot>();

            y_ToStr = varToStr;
            x_ToStr = varToStr;
        }
        void checkOrinin()
        {
            double x, y;
            y = Y_max * Y_zoom;
            x = -X_location * X_zoom;
            origin_in_pixel = new PointF((float)x, (float)y);
        }
        internal void PlotVisibleChange(IPlot plot)
        {
            throw new NotImplementedException();
        }

        void yResize()
        {
            this.Size = new Size(this.Size.Width, (int)((Y_max - Y_min) * Y_zoom));
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Size = new Size(this.Size.Width, (int)((Y_max - Y_min) * Y_zoom));
            base.OnSizeChanged(e);
        }

        internal void PlotNameChange(IPlot plot)
        {
            throw new NotImplementedException();
        }

        internal void PlotColorChange(IPlot plot)
        {
            //throw new NotImplementedException();
        }

        [Category("图表")]
        public double Y_zoom { 
            get => y_zoom; 
            set
            {
                y_zoom = value;
                yResize();
                checkOrinin();
                this.Refresh();
            }
        }
        [Category("图表")]
        public double X_zoom { get => x_zoom;
            set
            {
                x_zoom = value;
                checkOrinin();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_min
        {
            get => y_min; 
            set
            {
                if (value > y_max)
                {
                    y_min = y_max - 1;
                }
                else
                {
                    y_min = value;
                }
                yResize();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_max
        {
            get => y_max;
            set
            {
                if (value < y_min)
                {
                    y_max = y_min + 1;
                }
                else
                {
                    y_max = value;
                }
                yResize();
                checkOrinin();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double X_location
        {
            get => x_location;
            set
            {
                x_location = value;
                checkOrinin();
                this.Refresh();
            }
        }


        [Category("图表")]
        public double X_grid_size { 
            get => x_grid_size; 
            set
            {
                x_grid_size = value;
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_grid_size { 
            get => y_grid_size;
            set
            {
                y_grid_size = value;
                this.Refresh();
            }
        }

        public IPlot Forcu_on_plot { get => forcu_on_plot; set => forcu_on_plot = value; }

        public void add(IPlot p)
        {
            plots.Add(p);
            p.Chart = this;

        }
        public void remove(IPlot p)
        {
            plots.Remove(p);
            p.Chart = null;

        }

        PointF chartToPixel(double x, double y)
        {
            x *= x_zoom;
            y *= y_zoom;
            return new PointF((float)x+ origin_in_pixel.X, (float)y + origin_in_pixel.Y);
        }
        double ChartX(PointF pix)
        {
            return (double)((pix.X - origin_in_pixel.X)/ x_zoom);
        }
        double ChartY(PointF pix)
        {
            return (double)((pix.Y - origin_in_pixel.Y) / y_zoom);
        }
        double ChartX(float pix)
        {
            return (double)((pix - origin_in_pixel.X) / x_zoom);
        }
        double ChartY(float pix)
        {
            return (double)((pix - origin_in_pixel.Y) / y_zoom);
        }
        Pen pen_grid = new Pen(Color.LightGray, 1);
        Pen pen_form = new Pen(Color.Gray, 1);
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            OnPaintGril(pe);
            foreach (var p in plots) {
                OnPaintLine(pe, p);
            }
        }

        protected virtual void OnPaintGril(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            double grid_pixel;
            int n;
            double line;
            double max;
            float text_location;

            grid_pixel = x_grid_size * x_zoom;
            n = -(int)(origin_in_pixel.X / grid_pixel);
            line = origin_in_pixel.X + n * grid_pixel;
            max = pe.ClipRectangle.X + pe.ClipRectangle.Width;
            if (origin_in_pixel.Y < 0)
            {
                text_location = 0;
            }
            else if (origin_in_pixel.Y > this.Size.Height)
            {
                text_location = this.Size.Height - g.MeasureString("100000", this.Font).Height;
            }
            else
            {
                text_location = origin_in_pixel.Y - g.MeasureString("100000", this.Font).Height;
            }
            while (line < max)
            {
                g.DrawLine(pen_grid, (float)line, pe.ClipRectangle.Y, (float)line, pe.ClipRectangle.Y + pe.ClipRectangle.Height);
                g.DrawString($"{x_ToStr(n * x_grid_size)}", this.Font, SystemBrushes.ControlText, (float)line, text_location);
                line += grid_pixel;
                n++;
            }
            grid_pixel = y_grid_size * y_zoom;
            n = -(int)(origin_in_pixel.Y / grid_pixel);
            line = origin_in_pixel.Y + n * grid_pixel;
            max = pe.ClipRectangle.Y + pe.ClipRectangle.Height;
            if (origin_in_pixel.X < 0)
            {
                text_location = 0;
            }
            else if (origin_in_pixel.X > this.Size.Width)
            {
                text_location = this.Size.Width - g.MeasureString("100000", this.Font).Width;
            }
            else
            {
                text_location = origin_in_pixel.X;
            }
            while (line < max)
            {
                g.DrawLine(pen_grid, pe.ClipRectangle.X, (float)line, pe.ClipRectangle.X + pe.ClipRectangle.Width, (float)line);
                g.DrawString($"{y_ToStr(-n * y_grid_size)}", this.Font, SystemBrushes.ControlText, text_location, (float)line);
                line += grid_pixel;
                n++;
            }
            g.DrawLine(pen_form, origin_in_pixel.X, pe.ClipRectangle.Y, origin_in_pixel.X, pe.ClipRectangle.Y + pe.ClipRectangle.Height);
            g.DrawLine(pen_form, pe.ClipRectangle.X, origin_in_pixel.Y, pe.ClipRectangle.X + pe.ClipRectangle.Width, origin_in_pixel.Y);
        }


        public void gotoForemost()
        {
            if (forcu_on_plot != null)
            {
                if (forcu_on_plot.Length > 0)
                {
                    double temp = forcu_on_plot.X_max- forcu_on_plot.X_min - (Size.Width - 200 )/ x_zoom ;
                    if (temp < forcu_on_plot.X_min)
                    {
                        temp = forcu_on_plot.X_min;
                    }
                    this.X_location = temp;
                }
            }
        }
        protected virtual void OnPaintLine(PaintEventArgs pe, IPlot plot)
        {
            Graphics g = pe.Graphics;
            double bgn = ChartX(pe.ClipRectangle.X);
            double end = ChartX(pe.ClipRectangle.X+pe.ClipRectangle.Width);
            Dot from;
            int len = plot.Length;
            int i = plot.findBefore(bgn,len);
            from = plot[i];
            i++;
            while(i<len)
            {
                Dot d = plot[i];
                i++;
                g.DrawLine(plot.Line, chartToPixel(d.X, d.Y), chartToPixel(from.X, from.Y));
                from = d;
                if (d.X > end) { break; }
            }
        }





        bool is_moving = false;
        Point mouse_down_location;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                mouse_down_location = e.Location;
                is_moving = true;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (is_moving)
            {
                Point n = e.Location;
                Point o = mouse_down_location;
                this.forcu_on_plot = null;
                this.X_location -= (n.X - o.X)/x_zoom;
                mouse_down_location = n;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                is_moving = false;
            }
        }
        double[] size_a = { 0.05, 0.1, 0.15, 0.25, 0.5, 0.75, 1, 1.5, 2.5,5 };
        int size_num = 4;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0)
                {
                    if (size_num > 0)
                    {
                        size_num--;
                        X_zoom = size_a[size_num];
                    }
                }
                else if (e.Delta < 0)
                {
                    if (size_num < size_a.Length - 1)
                    {
                        size_num++;
                        X_zoom = size_a[size_num];
                    }
                }
            }
            else
            {
            }
        }


    }
}
