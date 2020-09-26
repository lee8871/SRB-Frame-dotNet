using System.Drawing;

namespace SRB_Chart
{
    public interface IDrawer
    {



        void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
        void DrawString(string s, Font font, Brush brush, float x, float y);
        void Finish();
        (float dX,float dY) MeasureString(string text, Font font);


    }
}
