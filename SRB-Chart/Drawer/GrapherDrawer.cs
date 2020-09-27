using System.Drawing;

namespace SRB_Chart.Drawer
{
    class GrapherDrawer:IDrawer
    {
        Graphics Graphics;
        Rectangle ClipRec;

        void IDrawer.DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            throw new System.NotImplementedException();
        }

        void IDrawer.DrawString(string s, Font font, Brush brush, float x, float y)
        {
            throw new System.NotImplementedException();
        }

        void IDrawer.Finish()
        {
            throw new System.NotImplementedException();
        }

        (float dX, float dY) IDrawer.MeasureString(string text, Font font)
        {
            throw new System.NotImplementedException();
        }
    }


}
