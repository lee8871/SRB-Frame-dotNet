using System.Drawing;

namespace SRB_Chart
{
    public interface IDrawer
    {
        Rectangle ClipRectangle { get; set; }

        void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
        void DrawString(string s, Font font, SolidBrush brush, float x, float y, StringFormat format);
    }
}
