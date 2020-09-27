using System.Drawing;
using System.Xml;

namespace SRB_Chart
{
    public class SvgDrawer : IDrawer
    {
        public Rectangle ClipRectangle { get => clip_rec; set => clip_rec = value; }
        Rectangle clip_rec;
        XmlWriter writer;
        int element_num;


        public void init (string file,int width, int height)
        {
            var setting = new XmlWriterSettings();
            setting.Indent = true;
            writer = XmlWriter.Create(file, setting);
            element_num = 0;
            writer.WriteStartDocument();
            writer.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            writer.WriteAttributeString("version", "1.1");
            writer.WriteAttributeString("id", "svg_head");
            writer.WriteAttributeString("width", $"{width}");
            writer.WriteAttributeString("height", $"{height}");
            clip_rec = new Rectangle(0, 0, width, height);
        }

        void IDrawer.DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            writer.WriteStartElement("path");
            writer.WriteAttributeString("id", $"Line{element_num++}");
            writer.WriteAttributeString("d", $"M {x1} {y1} L {x2} {y2}");
            writer.WriteAttributeString("stroke", ColorTranslator.ToHtml(pen.Color));
            writer.WriteAttributeString("stroke-width", pen.Width.ToString());
            writer.WriteAttributeString("fill", "none");
            writer.WriteEndElement();
        }

        void IDrawer.DrawString(string s, Font font, SolidBrush brush, float x, float y, StringFormat format)
        {
            writer.WriteStartElement("text");
            writer.WriteAttributeString("x", $"{x}");
            writer.WriteAttributeString("y", $"{y}");
            writer.WriteAttributeString("fill", brush.Color.ToString());
            writer.WriteAttributeString("font-family", font.Name);
            writer.WriteAttributeString("font-size", $"{font.SizeInPoints}px");
            writer.WriteAttributeString("text-anchor", format.text_anchor());
            writer.WriteString(s);
            writer.WriteEndElement();
        }


        public void finish()
        {
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
    }
    static class StringFormatToSVG
    {
        public static string text_anchor(this StringFormat format)
        {
            switch( format.Alignment)
            {
                case StringAlignment.Center:
                    return "middle";
                case StringAlignment.Near:
                    return "start";
                case StringAlignment.Far:
                    return "end";
                default: throw new System.Exception("StringFormat error");
            }
        }

    }

}
