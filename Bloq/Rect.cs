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
    public class Rect : Figure
    {

        public Rectangle rectangle;
        //[NonSerialized()]
        public Node_in node_in = new Node_in(new Rectangle());
        //[NonSerialized()]
        public Node_out node_out = new Node_out(new Rectangle());
        public Rect()
        {
            //node_out = new Node_out(new Rectangle());
            //node_in = new Node_in(new Rectangle());
            text = stringi.BLOK_OPERACYJNY;
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
            Rectangle node_rect = new Rectangle(x - 5, y + heigh / 2 - 5, 10, 10);

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);
            g.DrawEllipse(pen, node_rect);
            g.FillEllipse(new SolidBrush(Color.Black), node_rect);
        }
        public override void DrawRect(PaintEventArgs e)
        {
            
            node_out.rectangle = new Rectangle(x - 5, y + heigh / 2 - 5, 10, 10);
            node_in.rectangle = new Rectangle(x - 5, y - heigh / 2 - 5, 10, 10);

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);
            if (is_checked)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            rectangle = new Rectangle(x - width / 2, y - heigh / 2, width, heigh);
            g.DrawRectangle(pen, rectangle);
            //g.DrawString(this.text, fnt, new SolidBrush(Color.Black), x-width/2, y-5);
            StringFormat flags = new StringFormat();
            flags.LineAlignment = System.Drawing.StringAlignment.Center;
            flags.Alignment = System.Drawing.StringAlignment.Center;
            g.DrawString(this.text, Form1.fnt, new SolidBrush(Color.Black), rectangle, flags);
            if (!node_out.is_clicked)
                DrawNode_out(e);
            if (!node_in.is_clicked)
                DrawNode_in(e);
            if (node_out.is_clicked)
            {
                node_out.DrawNode(e);
            }
            //textBox1.Text = "e.X = " + e.X.ToString() + " e.Y = " + e.Y.ToString() + "is_cliced = " + node_out.is_clicked + " " + node_out.x + " " +node_out.y;



        }
    }
}
