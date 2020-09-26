using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SRB_Chart
{
    public partial class Chart : Control
    {
        ChartConfig cfg;

        List<IPlot> plots;
        IPlot forcu_on_plot = null;
        PointF origin_in_pixel;

        double x_location = 0;

        double[] size_a = { 0.05, 0.1, 0.15, 0.25, 0.5, 0.75, 1, 1.5, 2.5, 5 };
        int size_num = 4;

        public string varToStr(double val)
        {
            return val.ToString("F0");
        }

        public Chart()
        {
            cfg = new ChartConfig();
            cfg.x.zoom = 0.5;
            cfg.y.zoom = 0.5;
            cfg.x.grid_size = 100;
            cfg.y.grid_size = 100;
            cfg.y.ToStr = varToStr;
            cfg.x.ToStr = varToStr;
            cfg.y.min = -500;
            cfg.y.max = 1000;
            InitializeComponent();
            this.DoubleBuffered = true;
            plots = new List<IPlot>();

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
            get => cfg.y.zoom; 
            set
            {
                cfg.y.zoom = value;
                yResize();
                checkOrinin();
                this.Refresh();
            }
        }
        [Category("图表")]
        public double X_zoom { get => cfg.x.zoom;
            set
            {
                cfg.x.zoom = value;
                checkOrinin();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_min
        {
            get => cfg.y.min; 
            set
            {
                if (value > cfg.y.max)
                {
                    cfg.y.min = cfg.y.max - 1;
                }
                else
                {
                    cfg.y.min = value;
                }
                yResize();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_max
        {
            get => cfg.y.max;
            set
            {
                if (value < cfg.y.min)
                {
                    cfg.y.max = cfg.y.min + 1;
                }
                else
                {
                    cfg.y.max = value;
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
            get => cfg.x.grid_size; 
            set
            {
                cfg.x.grid_size = value;
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_grid_size { 
            get => cfg.y.grid_size;
            set
            {
                cfg.y.grid_size = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 设置当前控件追踪显示的曲线图
        /// </summary>
        public IPlot Forcu_on_plot { get => forcu_on_plot; set => forcu_on_plot = value; }
        /// <summary>
        /// 设置Y坐标刻度显示文本的转换函数
        /// </summary>
        public dDoubleToString Y_ToStr { 
            get => cfg.y.ToStr;
            set{
                cfg.y.ToStr = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 设置X坐标刻度显示文本的转换函数
        /// </summary>
        public dDoubleToString X_ToStr { 
            get => cfg.x.ToStr;
            set {
                cfg.x.ToStr = value;
                this.Refresh();
            }
        }

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
            x *= cfg.x.zoom;
            y *= cfg.y.zoom;
            return new PointF((float)x+ origin_in_pixel.X, (float)y + origin_in_pixel.Y);
        }
        double ChartX(PointF pix)
        {
            return (double)((pix.X - origin_in_pixel.X)/ cfg.x.zoom);
        }
        double ChartY(PointF pix)
        {
            return (double)((pix.Y - origin_in_pixel.Y) / cfg.y.zoom);
        }
        double ChartX(float pix)
        {
            return (double)((pix - origin_in_pixel.X) / cfg.x.zoom);
        }
        double ChartY(float pix)
        {
            return (double)((pix - origin_in_pixel.Y) / cfg.y.zoom);
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

            grid_pixel = cfg.x.grid_size * cfg.x.zoom;
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
                g.DrawString($"{cfg.x.ToStr(n * cfg.x.grid_size)}", this.Font, SystemBrushes.ControlText, (float)line, text_location);
                line += grid_pixel;
                n++;
            }
            grid_pixel = cfg.y.grid_size * cfg.y.zoom;
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
                g.DrawString($"{cfg.y.ToStr(-n * cfg.y.grid_size)}", this.Font, SystemBrushes.ControlText, text_location, (float)line);
                line += grid_pixel;
                n++;
            }
            g.DrawLine(pen_form, origin_in_pixel.X, pe.ClipRectangle.Y, origin_in_pixel.X, pe.ClipRectangle.Y + pe.ClipRectangle.Height);
            g.DrawLine(pen_form, pe.ClipRectangle.X, origin_in_pixel.Y, pe.ClipRectangle.X + pe.ClipRectangle.Width, origin_in_pixel.Y);
        }


        public void gotoForcuPlot()
        {
            if (forcu_on_plot != null)
            {
                if (forcu_on_plot.Length > 0)
                {
                    double temp = forcu_on_plot.X_max- forcu_on_plot.X_min - (Size.Width - 200 )/ cfg.x.zoom ;
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
            int len = plot.Length;
            int i = plot.findBefore(bgn,len);
            var from = plot[i];
            i++;
            while(i<len)
            {
                var d = plot[i];
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
                this.X_location -= (n.X - o.X)/ cfg.x.zoom;
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
