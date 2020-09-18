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
        double location = 520;
        double y_min= -120;
        double y_max= 330;

        int grid_pixel_size = 100;
        double x_grid_size = 100;

        public Chart()
        {
            InitializeComponent();
        }
        [Category("图表")]
        public double Y_zoom { 
            get => y_zoom; 
            set
            {
                y_zoom = value;
                this.Refresh();
            }
        }
        [Category("图表")]
        public double X_zoom { get => x_zoom;
            set
            {
                x_zoom = value;
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
                this.Refresh();
            }
        }

        [Category("图表")]
        public double X_location
        {
            get => location;
            set
            {
                location = value;
                this.Refresh();
            }
        }

        PointF origin_in_pixel;
        void checkOrinin()
        {
            double x,y;
            y = y_max * y_zoom;
            x = -location * x_zoom;
            origin_in_pixel = new PointF((float)x, (float)y);
        }
        void checkGridSize()
        {
            x_grid_size = grid_pixel_size / x_zoom;
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


            double x_grid_pixel = x_grid_size * x_zoom;
            int n = (int)(origin_in_pixel.X / x_grid_pixel);
            double diff = origin_in_pixel.X - n * x_grid_pixel;




            double x_max = pe.ClipRectangle.X + pe.ClipRectangle.Width;
            double line = diff;
            while (line< x_max) 
            {
                g.DrawLine(pen_grid, (float)line, pe.ClipRectangle.Y, (float)line , pe.ClipRectangle.Y + pe.ClipRectangle.Height);
                line += x_grid_pixel;
            }



            g.DrawLine(pen_form, origin_in_pixel.X, pe.ClipRectangle.Y, origin_in_pixel.X, pe.ClipRectangle.Y + pe.ClipRectangle.Height);
            g.DrawLine(pen_form, pe.ClipRectangle.X, origin_in_pixel.Y, pe.ClipRectangle.X + pe.ClipRectangle.Width, origin_in_pixel.Y);

        }
    }
}
