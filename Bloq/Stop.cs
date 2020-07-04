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

    public class Stop : Figure
    {
        public Node_in node_in;
        public Rectangle rectangle;
        public Stop()
        {
            node_in = new Node_in(new Rectangle());
            text = "Stop";
        }
        public void DrawNode_in(PaintEventArgs e)
        {
            Rectangle node_rect = new Rectangle(x - 5, y - heigh / 2 - 10, 10, 10);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);

            g.DrawEllipse(pen, node_rect);
            g.FillEllipse(new SolidBrush(Color.White), node_rect);
        }
        public override void DrawRect(PaintEventArgs e)
        {
            node_in.rectangle = new Rectangle(x - 5, y - heigh / 2 - 10, 10, 10);
            rectangle = new Rectangle(x - width / 2, y - heigh / 2 - 5, width, heigh);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red, 3);
            if (is_checked)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            }
            g.DrawEllipse(pen, rectangle);
            if (node_in.connected_with == null)
                DrawNode_in(e);
            StringFormat flags = new StringFormat();
            flags.LineAlignment = System.Drawing.StringAlignment.Center;
            flags.Alignment = System.Drawing.StringAlignment.Center;
            g.DrawString(this.text, Form1.fnt, new SolidBrush(Color.Black), rectangle, flags);

        }
    }
}
