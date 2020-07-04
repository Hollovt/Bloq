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

    public class Start : Figure
    {
        public Node_out node_out;
        public Rectangle rectangle;
        public Start()
        {
            node_out = new Node_out(new Rectangle());
            text = "Start";
        }
        public void DrawNode_out(PaintEventArgs e)
        {
            Rectangle node_rect = new Rectangle(x - 5, y + heigh / 2 - 10, 10, 10);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);
            g.DrawEllipse(pen, node_rect);
            g.FillEllipse(new SolidBrush(Color.Black), node_rect);
        }
        public override void DrawRect(PaintEventArgs e)
        {
            rectangle = new Rectangle(x - width / 2, y - heigh / 2 - 5, width, heigh);
            node_out.rectangle = new Rectangle(x - 5, y + heigh / 2 - 10, 10, 10);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Green, 3);
            if (is_checked)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            }
            g.DrawEllipse(pen, rectangle);
            if (!node_out.is_clicked)
                DrawNode_out(e);
            StringFormat flags = new StringFormat();
            flags.LineAlignment = System.Drawing.StringAlignment.Center;
            flags.Alignment = System.Drawing.StringAlignment.Center;
            g.DrawString(this.text, Form1.fnt, new SolidBrush(Color.Black), rectangle, flags);
            if (node_out.is_clicked)
            {
                node_out.DrawNode(e);
            }

        }
    }
}
