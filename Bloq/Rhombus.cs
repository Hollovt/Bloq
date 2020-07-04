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

    public class Rhombus : Figure
    {
        public Node_out node_out2;
        public Node_out node_out;
        public Node_in node_in;

        public Point[] points = new Point[4];
        public Rhombus()
        {
            node_out = new Node_out(new Rectangle());
            node_out2 = new Node_out(new Rectangle());
            node_in = new Node_in(new Rectangle());
            // text = "blok decyzyjny";
            text = stringi.BLOK_DECYZYJNY;
        }
        public void DrawNode_in(PaintEventArgs e)
        {
            Rectangle node_rect = new Rectangle(x - 5, y - heigh / 2 - 5, 10, 10);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);
            g.DrawEllipse(pen, node_rect);
            g.FillEllipse(new SolidBrush(Color.White), node_rect);
        }
        public void DrawNode_out(PaintEventArgs e)
        {
            Rectangle node_rect = new Rectangle(x + width / 2 - 5, y - 5, 10, 10);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);
            if (!node_out.is_clicked)
            {
                g.DrawEllipse(pen, node_rect);
                g.FillEllipse(new SolidBrush(Color.Black), node_rect);
            }
            node_rect = new Rectangle(x - width / 2 - 5, y - 5, 10, 10);
            if (!node_out2.is_clicked)
            {
                g.DrawEllipse(pen, node_rect);
                g.FillEllipse(new SolidBrush(Color.Black), node_rect);
            }

        }
        public override void DrawRect(PaintEventArgs e)
        {
            node_out.rectangle = new Rectangle(x + width / 2 - 5, y - 5, 10, 10);
            node_out2.rectangle = new Rectangle(x - width / 2 - 5, y - 5, 10, 10);
            node_in.rectangle = new Rectangle(x - 5, y - heigh / 2 - 5, 10, 10);
            points[0] = new Point(x, y - heigh / 2);
            points[1] = new Point(x + width / 2, y);
            points[2] = new Point(x, y + heigh / 2);
            points[3] = new Point(x - width / 2, y);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);
            if (is_checked)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            }
            g.DrawPolygon(pen, points);
            Rectangle rectangle = new Rectangle(x - width / 4, y - heigh / 4, width / 2, heigh / 2);
            Rectangle Pom = new Rectangle(x - width / 2 - 10, y - 20, 12, 12);
            StringFormat flags = new StringFormat();
            flags.LineAlignment = System.Drawing.StringAlignment.Center;
            flags.Alignment = System.Drawing.StringAlignment.Center;
            g.DrawString(this.text, Form1.fnt, new SolidBrush(Color.Black), rectangle, flags);
            g.DrawString("T", Form1.fnt, new SolidBrush(Color.Black), Pom, flags);
            Pom = new Rectangle(x + width / 2, y - 20, 12, 12);
            g.DrawString("F", Form1.fnt, new SolidBrush(Color.Black), Pom, flags);

            if (!node_in.is_clicked)
                DrawNode_in(e);
            DrawNode_out(e);
            if (node_out.is_clicked)
            {
                node_out.DrawNode(e);
            }
            if (node_out2.is_clicked)
            {
                node_out2.DrawNode(e);
            }



        }
    }
}
