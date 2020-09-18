using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRB.NodeType.PhotoElecX4
{
    public partial class Chart : Control
    {
        double y_zoom = 0.5;
        double x_zoom = 0.5;
        bool is_foremost = true;
        double x_location = 520;
        double y_min= -120;
        double y_max= 330;

        double x_grid_size = 100;
        double y_grid_size = 100;

        public Chart()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }



        PointF origin_in_pixel;
        void checkOrinin()
        {
            double x, y;
            y = Y_max * Y_zoom;
            x = -X_location * X_zoom;
            origin_in_pixel = new PointF((float)x, (float)y);
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

        PointF chartToPixel(double x, double y)
        {
            x *= x_zoom;
            y *= y_zoom;
            return new PointF((float)x+ origin_in_pixel.X, (float)y + origin_in_pixel.Y);
        }
        Pen pen_grid = new Pen(Color.LightGray, 1);
        Pen pen_form = new Pen(Color.Gray, 1);
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            checkOrinin();
            base.OnPaint(pe);
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
            else if(origin_in_pixel.Y > this.Size.Height)
            {
                text_location = this.Size.Height - g.MeasureString("100000", this.Font).Height ;
            }
            else
            {
                text_location = origin_in_pixel.Y - g.MeasureString("100000", this.Font).Height;
            }
            while (line < max)
            {
                g.DrawLine(pen_grid, (float)line, pe.ClipRectangle.Y, (float)line, pe.ClipRectangle.Y + pe.ClipRectangle.Height);
                g.DrawString($"{n * x_grid_size}", this.Font, SystemBrushes.ControlText, (float)line, text_location);
                line += grid_pixel;
                n++ ;
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
                text_location = origin_in_pixel.X ;
            }
            while (line < max)
            {
                g.DrawLine(pen_grid, pe.ClipRectangle.X, (float)line, pe.ClipRectangle.X + pe.ClipRectangle.Width,(float)line );
                g.DrawString($"{-n * y_grid_size}", this.Font, SystemBrushes.ControlText,  text_location,(float)line);
                line += grid_pixel;
                n++ ;
            }
            g.DrawLine(pen_form, origin_in_pixel.X, pe.ClipRectangle.Y, origin_in_pixel.X, pe.ClipRectangle.Y + pe.ClipRectangle.Height);
            g.DrawLine(pen_form, pe.ClipRectangle.X, origin_in_pixel.Y, pe.ClipRectangle.X + pe.ClipRectangle.Width, origin_in_pixel.Y);
        }
    }
}
