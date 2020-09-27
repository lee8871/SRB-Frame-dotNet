using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SRB_Chart
{
    [Serializable]
    public partial class Chart : Control
    {
        ChartConfig cfg_display;
        List<IPlot> plots;
        IPlot forcu_on_plot = null;
        //PointF origin_in_pixel;


        double[] size_a = { 0.05, 0.1, 0.15, 0.25, 0.5, 0.75, 1, 1.5, 2.5, 5 };
        int size_num = 4;

        public string varToStr(double val)
        {
            return val.ToString("F0");
        }

        public Chart()
        {
            cfg_display = new ChartConfig(varToStr);
            InitializeComponent();
            plots = new List<IPlot>();
            pensInit();

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
            get => cfg_display.y.zoom; 
            set
            {
                cfg_display.y.zoom = value;
                yResize();
                cfg_display.checkOrigin();
                this.Refresh();
            }
        }
        [Category("图表")]
        public double X_zoom { get => cfg_display.x.zoom;
            set
            {
                cfg_display.x.zoom = value;
                cfg_display.checkOrigin();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_min
        {
            get => cfg_display.y_min; 
            set
            {
                if (value > cfg_display.y_max)
                {
                    cfg_display.y_min = cfg_display.y_max - 1;
                }
                else
                {
                    cfg_display.y_min = value;
                }
                yResize();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_max
        {
            get => cfg_display.y_max;
            set
            {
                if (value < cfg_display.y_min)
                {
                    cfg_display.y_max = cfg_display.y_min + 1;
                }
                else
                {
                    cfg_display.y_max = value;
                }
                yResize();
                cfg_display.checkOrigin();
                this.Refresh();
            }
        }

        [Category("图表")]
        public double X_location
        {
            get => cfg_display.x_min;
            set
            {
                cfg_display.x_min = value;
                cfg_display.checkOrigin();
                this.Refresh();
            }
        }


        [Category("图表")]
        public double X_grid_size { 
            get => cfg_display.x.grid_size; 
            set
            {
                cfg_display.x.grid_size = value;
                this.Refresh();
            }
        }

        [Category("图表")]
        public double Y_grid_size { 
            get => cfg_display.y.grid_size;
            set
            {
                cfg_display.y.grid_size = value;
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
        /// 
        public void setYToStr(ChartDimension.dDoubleToString value)
        {
                cfg_display.y.ToStr = value;
                this.Refresh();
        }
        /// <summary>
        /// 设置X坐标刻度显示文本的转换函数
        /// </summary>
        /// 
        public void setXToStr(ChartDimension.dDoubleToString value)
        {
            cfg_display.x.ToStr = value;
            this.Refresh();
        }

        /*
        /// <summary>
        /// 设置Y坐标刻度显示文本的转换函数
        /// </summary>
        /// 

        [   DisplayName("Y字符串转换"),
            Description("将Y坐标刻度转换为对应的字符串"),
            NotifyParentProperty(false),
            Browsable(false)]
        public ChartDimension.dDoubleToString Y_ToStr { 
            get => cfg_display.y.ToStr;
            set{
                cfg_display.y.ToStr = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 设置X坐标刻度显示文本的转换函数
        /// </summary>
        /// 

        [DisplayName("X字符串转换"),
            Description("将X坐标刻度转换为对应的字符串"),
            NotifyParentProperty(false),
            Browsable(false)]
        public ChartDimension.dDoubleToString X_ToStr { 
            get => cfg_display.x.ToStr;
            set {
                cfg_display.x.ToStr = value;
                this.Refresh();
            }
        }
        */
        public void gotoForcuPlot()
        {
            if (forcu_on_plot != null)
            {
                if (forcu_on_plot.Length > 0)
                {
                    double temp = forcu_on_plot.X_max - forcu_on_plot.X_min - cfg_display.x.toValueDst(Size.Width - 200);
                    if (temp < forcu_on_plot.X_min)
                    {
                        temp = forcu_on_plot.X_min;
                    }
                    this.X_location = temp;
                }
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

        Pen pen_grid = new Pen(Color.LightGray, 1);
        Pen pen_form = new Pen(Color.Gray, 1);
        SolidBrush brush_text;
        StringFormat format;

        void pensInit()
        {
            this.DoubleBuffered = true;
            brush_text = new SolidBrush(ForeColor);
            format = new StringFormat();

        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            brush_text = new SolidBrush(ForeColor);
        }
        GrapherDrawer gd = new GrapherDrawer();
        SvgDrawer svgd = new SvgDrawer();


        public void paintSVG(string file)
        {
            ChartConfig cfg_svg;
            cfg_svg = new ChartConfig(cfg_display);
            cfg_svg.x_min = 0;
            cfg_svg.x_max = plots[0].X_max;
            int w = (int)(cfg_svg.x.toImageDst(cfg_svg.x_max - cfg_svg.x_min));
            int h = (int)(cfg_svg.y.toImageDst(cfg_svg.y_max - cfg_svg.y_min));
            cfg_svg.checkOrigin();
            svgd.init(file, w, h);
            OnPaintGril(svgd, cfg_svg);
            foreach (var p in plots)
            {
                OnPaintLine(svgd, cfg_svg, p);
            }
            svgd.finish();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            gd.Graphics = pe.Graphics;
            gd.ClipRectangle = pe.ClipRectangle;
            OnPaintGril(gd,cfg_display);
            foreach (var p in plots) {
                OnPaintLine(gd, cfg_display, p);
            }
        }

        protected virtual void OnPaintGril(IDrawer g, ChartConfig c)
        {

            float grid_pixel;
            int n;
            double line;
            double max;
            float text_location;
            grid_pixel = c.x.toImageDst(c.x.grid_size);
            n = -(int)(c.x.toImage(0) / grid_pixel);
            line = c.x.toImage(0) + n * grid_pixel;
            max = g.ClipRectangle.X + g.ClipRectangle.Width;
            if (c.y.toImage(0) < 0+100)
            {
                text_location = 0;
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Far;
            }
            else if (c.y.toImage(0) > this.Size.Height-100)
            {
                text_location = this.Size.Height;
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
            }
            else
            {
                text_location = c.y.toImage(0);
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Far;
            }
            while (line < max)
            {
                g.DrawLine(pen_grid, (float)line, g.ClipRectangle.Y, (float)line, g.ClipRectangle.Y + g.ClipRectangle.Height);
                g.DrawString($"{c.x.ToStr(n * c.x.grid_size)}", this.Font, brush_text, (float)line, text_location,format) ;
                line += grid_pixel;
                n++;
            }
            grid_pixel = c.y.toImageDst(c.y.grid_size);
            n = -(int)(c.y.toImage(0) / grid_pixel);
            line = c.y.toImage(0) + n * grid_pixel;
            max = g.ClipRectangle.Y + g.ClipRectangle.Height;
            if (c.x.toImage(0) < 100)
            {
                text_location = 0;
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
            }
            else if (c.x.toImage(0) > this.Size.Width-100)
            {
                text_location = this.Size.Width;
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Near;
            }
            else
            {
                text_location = c.x.toImage(0);
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
            }
            while (line < max)
            {
                g.DrawLine(pen_grid, g.ClipRectangle.X, (float)line, g.ClipRectangle.X + g.ClipRectangle.Width, (float)line);
                g.DrawString($"{c.y.ToStr(-n * c.y.grid_size)}", this.Font, brush_text, text_location, (float)line,format);
                line += grid_pixel;
                n++;
            }
            g.DrawLine(pen_form, c.x.toImage(0), g.ClipRectangle.Y, c.x.toImage(0), g.ClipRectangle.Y + g.ClipRectangle.Height);
            g.DrawLine(pen_form, g.ClipRectangle.X, c.y.toImage(0), g.ClipRectangle.X + g.ClipRectangle.Width, c.y.toImage(0));
        }
        protected virtual void OnPaintLine(IDrawer g, ChartConfig c,IPlot plot)
        {
            double bgn = c.x.toValue(g.ClipRectangle.X);
            float end_pix = g.ClipRectangle.X+g.ClipRectangle.Width;
            int len = plot.Length;
            int i = plot.findBefore(bgn,len);
            var from = c.toImagePoint(plot[i].X, plot[i].Y);
            i++;
            while(i<len)
            {
                var to = c.toImagePoint(plot[i].X, plot[i].Y);
                i++;
                g.DrawLine(plot.Line, from.x, from.y, to.x, to.y); 
                from = to;
                if (to.x > end_pix) { 
                    break;
                }
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
                this.X_location -= cfg_display.x.toValueDst(n.X - o.X);
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
