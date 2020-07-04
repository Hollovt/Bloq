using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloq
{
    [Serializable]

    public class Node_out : Node
    {
        // [NonSerialized()]
        public Node_in connected_with;
        public Node_out(Rectangle rec)
        {
            is_clicked = false;
            rectangle = rec;
            x = rectangle.X;
            y = rectangle.Y;
            connected_with = null;
        }
        public Node_out()
        {

        }
        public override void DrawNode(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            if (connected_with != null)
            {
                System.Drawing.Drawing2D.GraphicsPath capPath = new System.Drawing.Drawing2D.GraphicsPath();
                /*capPath.AddLine(-5, -10, 5, -10);
                capPath.AddLine(-5, -10, 0, 0);
                capPath.AddLine(0, 0, 5, -10);
                */
                /* Point p1(-5, -10, 5, -10);
                 Point p2(-5, -10, 0, 0);
                 Point p3(0, 0, 5, -10);*/
                pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
                g.DrawLine(pen, rectangle.X + 5, rectangle.Y + 5, connected_with.rectangle.X + 5, connected_with.rectangle.Y + 5);
                g.FillPath(new SolidBrush(Color.Black), capPath);
            }
            else
                g.DrawLine(pen, rectangle.X + 5, rectangle.Y + 5, x, y);
        }
    }
}
