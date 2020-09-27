using System.Drawing;

namespace SRB_Chart
{
    public class GrapherDrawer:IDrawer
    {
        public Graphics Graphics { get => g; set => g = value; }
        public Rectangle ClipRectangle { get => clip_rec; set => clip_rec = value; }

        Graphics g;
        Rectangle clip_rec;
        StringFormat format = new StringFormat();
        public GrapherDrawer()
        {
            format.Alignment = StringAlignment.Far;
        }
        void IDrawer.DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            g.DrawLine(pen,  x1,  y1,  x2,  y2);
        }
        void IDrawer.DrawString(string s, Font font, SolidBrush brush, float x, float y, StringFormat format)
        {
            g.DrawString(s, font, brush, x, y, format);
        }

    }

}
