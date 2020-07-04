using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Globalization;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Net.Mime.MediaTypeNames;
using System.Resources;

namespace Bloq
{

    public partial class Form1 : Form
    {
        public static bool clear = false;
        public static Font fnt = new Font("Arial", 8);
        public int which_checked = -1;
        public (int, int) which_is_linked;
              
        int with_figure = 2;
        bool is_dragged = false;
        public List<Figure> figurs = new List<Figure>();
        public List<Node> noes = new List<Node>();
        public int PenWidth = 5;
        public Rectangle RcDraw;
        
        int rectWidth = 100;
        int rectHeigh = 60;
        int RhWidth = 150;
        int RhHeigh = 90;
        int StartWidth = 100;
        int StartHeigh = 60;
        int xPosition;
        int yPosition;
        bool is_linking = false;
        bool is_start = false;
        public Form1()
        {
            InitializeComponent();
            buttons = new Button[6];
            buttons[0] = button4;
            buttons[1] = button5;
            buttons[2] = button7;
            buttons[3] = button6;
            buttons[4] = button10;
            buttons[5] = button11;
            with_figure = 2;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;

        }
        Button[] buttons;
       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Black;
        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e) //Rectangle
        {
            
            with_figure = 0;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            with_figure = 1;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;
        } //Rhombus

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new Form2();






            a.Visible = true;



        } //new

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            /* OpenFileDialog f = new OpenFileDialog();

             f.Filter = "JPG (*.JPG)|*.jpg";
             if (f.ShowDialog() == DialogResult.OK)
             {
                 file = Image.FromFile(f.FileName);
                 pictureBox1.Image = file;
             }*/
        } 

        private void button3_Click(object sender, EventArgs e)
        {
            
             OpenFileDialog f = new OpenFileDialog();
             System.IO.Stream myStream;

             f.Filter = "diag files (*.diag)|*.diag";


            if (f.ShowDialog() == DialogResult.OK)
            {

                if ((myStream = f.OpenFile()) != null)
                {
                    Save save = null;
                    
                        BinaryFormatter formatter = new BinaryFormatter();

                        // Deserialize the hashtable from the file and 
                        // assign the reference to the local variable.
                        save = (Save)formatter.Deserialize(myStream);
                    pictureBox1.Width = save.width;
                    pictureBox1.Height = save.heigh;
                    figurs = save.fig;
                    pictureBox1.Refresh();
                        
                        myStream.Close();
                    
                }
            }    
        } //load

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            xPosition = me.X;
            yPosition = me.Y;
            if (me.Button is MouseButtons.Left && !is_dragged)
            {
                switch (with_figure)
                {
                    case 0:

                        Rect temp = new Rect();
                        temp.x = e.X;
                        temp.y = e.Y;
                        temp.width = rectWidth;
                        temp.heigh = rectHeigh;
                        figurs.Add(temp);
                        break;
                    case 1:
                        Rhombus temp2 = new Rhombus();

                        temp2.x = e.X;
                        temp2.y = e.Y;
                        temp2.width = RhWidth;
                        temp2.heigh = RhHeigh;
                        figurs.Add(temp2);
                        break;
                    case 2:
                        if(is_start==true)
                        {
                            Form f = new Form();

                           
                            f.MaximizeBox = false;
                            f.MinimizeBox = false;
                            f.StartPosition = FormStartPosition.CenterScreen;
                            f.Width = 260;
                            f.Height = 135;
                            Label label = new Label();
                            label.Text = stringi.JestBlokStartowy;
                            label.Top = 20;

                            label.Size = new Size(250, 20);
                            label.Left = 10;
                            Button but = new Button();
                            but.Text = "OK";
                            but.Click += (s, k) => { f.Visible = false; };
                            but.Size = new Size(80, 20);
                            but.Top = 60;
                            but.Left = 150;

                            f.Controls.Add(label);
                            f.Controls.Add(but);
                            //f.ControlBox = false;
                            f.Visible = true;
                            break;
                        }
                        Start temp_start = new Start();
                        temp_start.x = e.X;
                        temp_start.y = e.Y;
                        temp_start.width = StartWidth;
                        temp_start.heigh = StartHeigh;
                        figurs.Add(temp_start);
                        is_start = true;
                        break;
                    case 3:
                        Stop temp_stop = new Stop();
                        temp_stop.x = e.X;
                        temp_stop.y = e.Y;
                        temp_stop.width = StartWidth;
                        temp_stop.heigh = StartHeigh;
                        figurs.Add(temp_stop);
                        break;
                    case 5:
                        System.Drawing.Drawing2D.GraphicsPath path;
                        for(int i=0; i<figurs.Count; i++)
                        {
                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            if(figurs[i] is Rect)
                            {
                                Rect tempr= figurs[i] as Rect;
                                path.AddRectangle(tempr.rectangle);
                                if (path.IsVisible(e.X, e.Y))
                                {
                                    if (tempr.node_in.connected_with != null)
                                    {
                                        tempr.node_in.connected_with.connected_with = null;
                                        tempr.node_in.connected_with.connected_with = null;
                                        tempr.node_in.connected_with.is_clicked = false;
                                       
                                    }
                                    if (tempr.node_out.connected_with != null)
                                    {
                                        tempr.node_out.connected_with.connected_with = null;
                                        tempr.node_out.connected_with.connected_with = null;
                                        tempr.node_out.connected_with.is_clicked = false;

                                    }
                                    if (figurs[i].is_checked)

                                    {
                                        which_checked = -1;
                                        textBox1.Text = "";
                                        textBox1.Enabled = false;
                                    }
                                    figurs.Remove(figurs[i]);
                                }
                            }
                            else if(figurs[i] is Rhombus)
                            {
                                Rhombus tempr = figurs[i] as Rhombus;
                                path.AddPolygon(tempr.points);
                                if (path.IsVisible(e.X, e.Y))
                                {
                                    if (tempr.node_in.connected_with != null)
                                    {
                                        tempr.node_in.connected_with.connected_with = null;
                                        tempr.node_in.connected_with.connected_with = null;
                                        tempr.node_in.connected_with.is_clicked = false;

                                    }
                                    if (tempr.node_out.connected_with != null)
                                    {
                                        tempr.node_out.connected_with.connected_with = null;
                                        tempr.node_out.connected_with.connected_with = null;
                                        tempr.node_out.connected_with.is_clicked = false;

                                    }
                                    if (tempr.node_out2.connected_with != null)
                                    {
                                        tempr.node_out2.connected_with.connected_with = null;
                                        tempr.node_out2.connected_with.connected_with = null;
                                        tempr.node_out2.connected_with.is_clicked = false;

                                    }

                                    if (figurs[i].is_checked)

                                    {
                                        which_checked = -1;
                                        textBox1.Text = "";
                                        textBox1.Enabled = false;
                                    }
                                    figurs.Remove(figurs[i]);
                                }
                            }
                            else if (figurs[i] is Start)
                            {
                                Start tempr = figurs[i] as Start;
                                path.AddRectangle(tempr.rectangle);
                                if (path.IsVisible(e.X, e.Y))
                                {
                                    
                                    if (tempr.node_out.connected_with != null)
                                    {
                                        tempr.node_out.connected_with.connected_with = null;
                                        tempr.node_out.connected_with.connected_with = null;
                                        tempr.node_out.connected_with.is_clicked = false;

                                    }
                                    if (figurs[i].is_checked)

                                    {
                                        which_checked = -1;
                                        textBox1.Text = "";
                                        textBox1.Enabled = false;
                                    }
                                    figurs.Remove(figurs[i]);
                                    is_start = false;
                                }
                            }
                            else if(figurs[i] is Stop)
                            {
                                Stop tempr = figurs[i] as Stop;
                                path.AddRectangle(tempr.rectangle);
                                if (path.IsVisible(e.X, e.Y))
                                {
                                    if (tempr.node_in.connected_with != null)
                                    {
                                        tempr.node_in.connected_with.connected_with = null;
                                        tempr.node_in.connected_with.connected_with = null;
                                        tempr.node_in.connected_with.is_clicked = false;

                                    }
                                   
                                    if (figurs[i].is_checked)

                                    {
                                        which_checked = -1;
                                        textBox1.Text = "";
                                        textBox1.Enabled = false;
                                    }
                                    figurs.Remove(figurs[i]);
                                }
                            }

                        }
                        break;

                }
            }
            else if (me.Button is MouseButtons.Right && !is_dragged)
            {
                System.Drawing.Drawing2D.GraphicsPath path;

                for (int i = 0; i < figurs.Count; i++)
                {
                    path = new System.Drawing.Drawing2D.GraphicsPath();
                    if (figurs[i] is Rect)
                    {
                        Rect temp = figurs[i] as Rect;
                        path.AddRectangle(temp.rectangle);
                        if (path.IsVisible(e.X, e.Y))
                        {
                            figurs[i].is_checked = true;
                            
                            textBox1.Enabled = true;

                            which_checked = i;
                            for (int j = 0; j < figurs.Count; j++)
                                if (i != j)
                                    figurs[j].is_checked = false;
                            textBox1.Text = figurs[i].text;
                            break;
                        }
                        figurs[i].is_checked = false;
                        which_checked = -1;
                        textBox1.Text = "";
                        textBox1.Enabled = false;

                    }
                    else if (figurs[i] is Rhombus)
                    {
                        Rhombus temp = figurs[i] as Rhombus;
                        path.AddPolygon(temp.points);
                        if (path.IsVisible(e.X, e.Y))
                        {
                            figurs[i].is_checked = true;
                           
                            textBox1.Enabled = true;
                            which_checked = i;
                            for (int j = 0; j < figurs.Count; j++)
                                if (i != j)
                                    figurs[j].is_checked = false;
                            textBox1.Text = figurs[i].text;


                            break;
                        }
                        figurs[i].is_checked = false;
                        which_checked = -1;
                        textBox1.Text = "";
                        textBox1.Enabled = false;
                    }
                    else if (figurs[i] is Start)
                    {
                        Start temp = figurs[i] as Start;
                        path.AddEllipse(temp.rectangle);
                        if (path.IsVisible(e.X, e.Y))
                        {
                            figurs[i].is_checked = true;
                           

                            which_checked = i;
                            for (int j = 0; j < figurs.Count; j++)
                                if (i != j)
                                    figurs[j].is_checked = false;
                            textBox1.Text = figurs[i].text;
                            break;
                        }
                        figurs[i].is_checked = false;
                        which_checked = -1;
                        textBox1.Text = "";
                        textBox1.Enabled = false;
                    }
                    else if (figurs[i] is Stop)
                    {
                        Stop temp = figurs[i] as Stop;
                        path.AddEllipse(temp.rectangle);
                        if (path.IsVisible(e.X, e.Y))
                        {
                            figurs[i].is_checked = true;
                            

                            which_checked = i;
                            for (int j = 0; j < figurs.Count; j++)
                                if (i != j)
                                    figurs[j].is_checked = false;
                            textBox1.Text = figurs[i].text;
                            break;
                        }
                        figurs[i].is_checked = false;
                        which_checked = -1;
                        textBox1.Text = "";
                        textBox1.Enabled = false;
                    }
                }
            }
            else if (me.Button is MouseButtons.Middle)
            {
                xPosition = e.X;
                yPosition = e.Y;
                is_dragged = true;
            }

            pictureBox1.Refresh();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (clear)
            {
                //e.Graphics.Clear(SystemColors.ControlLight);
                clear = false;
                this.which_checked = -1;
                is_start = false;
                figurs = new List<Figure>();
            }

            for (int i = 0; i < figurs.Count; i++)
                figurs[i].DrawRect(e);


        }

      

        private void button2_Click(object sender, EventArgs e) //save
        {
            System.IO.Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "diag files (*.diag)|*.diag";
            //saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    Save save = new Save();
                    save.fig = figurs;
                    save.heigh = pictureBox1.Height;
                    save.width = pictureBox1.Width;
                    Type[] types = { typeof(Rect), typeof(Rhombus), typeof(Start), typeof(Stop), typeof(Node_in), typeof(Node_out), typeof(Node) };
                    XmlSerializer serializer = new XmlSerializer(typeof(Save), types);
                    

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(myStream, save);

                    myStream.Close();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            if (with_figure == 4)
            {
                if (me.Button is MouseButtons.Left)
                {
                    Point point = new Point(xPosition, yPosition);
                    for (int i = 0; i < figurs.Count; i++)
                    {
                        if (figurs[i] is Rect)
                        {
                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            Rect temp = figurs[i] as Rect;
                            
                                
                            path.AddEllipse(temp.node_out.rectangle);
                            if (path.IsVisible(point) && temp.node_out.connected_with == null)
                            {
                                which_is_linked = (i, 0);
                                temp.node_out.connected_with = null;
                                is_linking = true;
                                temp.node_out.x = e.X;
                                temp.node_out.y = e.Y;
                                temp.node_out.is_clicked = true;
                                //textBox1.Text ="e.X = " +  e.X.ToString() + " e.Y = " + e.Y.ToString() + "is_cliced = " + temp.node_out.is_clicked + " "+ temp.node_out.x + " " + temp.node_out.y;
                                //textBox1.Text += "fifure[0].node_out.is_clicked = " + (figurs[0] as Rect).node_out.is_clicked;
                                pictureBox1.Refresh();
                                break;

                            }
                        }
                        if (figurs[i] is Rhombus)
                        {

                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            Rhombus temp = figurs[i] as Rhombus;
                            
                            path.AddEllipse(temp.node_out.rectangle);
                            if (path.IsVisible(point) && temp.node_out.connected_with == null)
                            {
                                which_is_linked = (i, 0);
                                temp.node_out.connected_with = null;
                                is_linking = true;

                                temp.node_out.x = e.X;
                                temp.node_out.y = e.Y;
                                temp.node_out.is_clicked = true;
                                //textBox1.Text = "e.X = " + e.X.ToString() + " e.Y = " + e.Y.ToString() + "is_cliced = " + temp.node_out.is_clicked + " " + temp.node_out.x + " " + temp.node_out.y;
                                //textBox1.Text += "fifure[0].node_out.is_clicked = " + (figurs[0] as Rect).node_out.is_clicked;
                                pictureBox1.Refresh();
                                break;

                            }
                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            path.AddEllipse(temp.node_out2.rectangle);
                            if (path.IsVisible(point) && temp.node_out2.connected_with == null)
                            {
                                which_is_linked = (i, 1);
                                temp.node_out2.connected_with = null;
                                is_linking = true;

                                temp.node_out2.x = e.X;
                                temp.node_out2.y = e.Y;
                                temp.node_out2.is_clicked = true;
                                //textBox1.Text = "e.X = " + e.X.ToString() + " e.Y = " + e.Y.ToString() + "is_cliced = " + temp.node_out.is_clicked + " " + temp.node_out.x + " " + temp.node_out.y;
                                //textBox1.Text += "fifure[0].node_out.is_clicked = " + (figurs[0] as Rect).node_out.is_clicked;
                                pictureBox1.Refresh();
                                break;

                            }


                        }
                        if (figurs[i] is Start)
                        {
                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            Start temp = figurs[i] as Start;
                            path.AddEllipse(temp.node_out.rectangle);
                            if (path.IsVisible(point) && temp.node_out.connected_with == null)
                            {
                                which_is_linked = (i, 0);
                                is_linking = true;

                                temp.node_out.x = e.X;
                                temp.node_out.y = e.Y;
                                temp.node_out.is_clicked = true;
                                //textBox1.Text = "e.X = " + e.X.ToString() + " e.Y = " + e.Y.ToString() + "is_cliced = " + temp.node_out.is_clicked + " " + temp.node_out.x + " " + temp.node_out.y;
                                //textBox1.Text += "fifure[0].node_out.is_clicked = " + (figurs[0] as Rect).node_out.is_clicked;
                                pictureBox1.Refresh();
                                break;

                            }
                        }
                    }
                }
            }
            if (which_checked >= 0)
            {
                if (/*me.Button == MouseButtons.Middle ||*/ is_dragged)
                {

                    figurs[which_checked].x += e.X - xPosition;
                    figurs[which_checked].y += e.Y - yPosition;
                    xPosition = e.X;
                    yPosition = e.Y;



                    pictureBox1.Refresh();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (which_checked >= 0)
                {
                    if (figurs[which_checked].x < 0)
                        figurs[which_checked].x = 0;
                    if (figurs[which_checked].y < 0)
                        figurs[which_checked].y = 0;
                    if (figurs[which_checked].x > pictureBox1.Width)
                        figurs[which_checked].x = pictureBox1.Width;
                    if (figurs[which_checked].y > pictureBox1.Height)
                        figurs[which_checked].y = pictureBox1.Height;
                }
                is_dragged = false;
                pictureBox1.Refresh();
            }
            if (is_linking)

            {
                bool czy_usuwamy = true;
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                //for (int i=0; i<figurs.Count; i++)
                {

                    if (figurs[which_is_linked.Item1] is Rect)
                    {
                        czy_usuwamy = true;
                        Rect temp = figurs[which_is_linked.Item1] as Rect;


                        for (int j = 0; j < figurs.Count; j++)
                        {

                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            if (figurs[j] is Rect && which_is_linked.Item1 != j)
                            {

                                Rect temp2 = figurs[j] as Rect;
                                
                                path.AddEllipse(temp2.node_in.rectangle);
                                if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with==null)
                                {
                                    temp2.node_in.is_clicked = true;
                                    temp2.node_in.connected_with = temp.node_out;
                                    temp.node_out.connected_with = temp2.node_in;
                                    czy_usuwamy = false;
                                    break;
                                }
                            }
                            else if (figurs[j] is Rhombus && which_is_linked.Item1 != j)
                            {
                                Rhombus temp2 = figurs[j] as Rhombus;
                                path.AddEllipse(temp2.node_in.rectangle );
                                if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with==null)
                                {
                                    temp2.node_in.connected_with = temp.node_out;
                                    temp2.node_in.is_clicked = true;
                                    temp.node_out.connected_with = temp2.node_in;
                                    czy_usuwamy = false;
                                    break;
                                }
                                
                            }
                            else if (figurs[j] is Stop && which_is_linked.Item1 != j)
                            {
                                Stop temp2 = figurs[j] as Stop;
                                path.AddEllipse(temp2.node_in.rectangle);
                                if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                {
                                    temp2.node_in.connected_with = temp.node_out;
                                    temp2.node_in.is_clicked = true;
                                    temp.node_out.connected_with = temp2.node_in;
                                    czy_usuwamy = false;
                                    break;
                                }
                            }

                        }
                        temp.node_out.is_clicked = !czy_usuwamy;


                    }
                    else if(figurs[which_is_linked.Item1] is Rhombus)
                    {
                        czy_usuwamy = true;
                        Rhombus temp = figurs[which_is_linked.Item1] as Rhombus;
                        if (which_is_linked.Item2 == 0)
                        {
                            for (int j = 0; j < figurs.Count; j++)
                            {

                                path = new System.Drawing.Drawing2D.GraphicsPath();
                                if (figurs[j] is Rect && which_is_linked.Item1 != j)
                                {

                                    Rect temp2 = figurs[j] as Rect;

                                    path.AddEllipse(temp2.node_in.rectangle);
                                    if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                    {
                                        temp2.node_in.is_clicked = true;
                                        temp2.node_in.connected_with = temp.node_out;
                                        temp.node_out.connected_with = temp2.node_in;
                                        czy_usuwamy = false;
                                        break;
                                    }
                                    

                                }
                                else if (figurs[j] is Rhombus && which_is_linked.Item1 != j)
                                {
                                    Rhombus temp2 = figurs[j] as Rhombus;
                                    path.AddEllipse(temp2.node_in.rectangle);
                                    if (path.IsVisible(e.X, e.Y) && temp2.node_in != null)
                                    {
                                        temp2.node_in.connected_with = temp.node_out;
                                        temp2.node_in.is_clicked = true;
                                        temp.node_out.connected_with = temp2.node_in;
                                        czy_usuwamy = false;
                                        break;
                                    }

                                }
                                else if (figurs[j] is Stop && which_is_linked.Item1 != j)
                                {
                                    Stop temp2 = figurs[j] as Stop;
                                    path.AddEllipse(temp2.node_in.rectangle);
                                    if (path.IsVisible(e.X, e.Y) && temp2.node_in != null)
                                    {
                                        temp2.node_in.connected_with = temp.node_out;
                                        temp2.node_in.is_clicked = true;
                                        temp.node_out.connected_with = temp2.node_in;
                                        czy_usuwamy = false;
                                        break;
                                    }
                                }

                            }
                            temp.node_out.is_clicked = !czy_usuwamy;
                        }
                        else
                        {
                            for (int j = 0; j < figurs.Count; j++)
                            {

                                path = new System.Drawing.Drawing2D.GraphicsPath();
                                if (figurs[j] is Rect && which_is_linked.Item1 != j)
                                {

                                    Rect temp2 = figurs[j] as Rect;

                                    path.AddEllipse(temp2.node_in.rectangle);
                                    if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                    {
                                        temp2.node_in.is_clicked = true;
                                        temp2.node_in.connected_with = temp.node_out2;
                                        temp.node_out2.connected_with = temp2.node_in;
                                        czy_usuwamy = false;
                                        break;
                                    }
                                    

                                }
                                else if (figurs[j] is Rhombus && which_is_linked.Item1 != j)
                                {
                                    Rhombus temp2 = figurs[j] as Rhombus;
                                    path.AddEllipse(temp2.node_in.rectangle);
                                    if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                    {
                                        temp2.node_in.connected_with = temp.node_out2;
                                        temp2.node_in.is_clicked = true;
                                        temp.node_out2.connected_with = temp2.node_in;
                                        czy_usuwamy = false;
                                        break;
                                    }

                                }
                                else if (figurs[j] is Stop && which_is_linked.Item1 != j)
                                {
                                    Stop temp2 = figurs[j] as Stop;
                                    path.AddEllipse(temp2.node_in.rectangle);
                                    if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                    {
                                        temp2.node_in.connected_with = temp.node_out2;
                                        temp2.node_in.is_clicked = true;
                                        temp.node_out2.connected_with = temp2.node_in;
                                        czy_usuwamy = false;
                                        break;
                                    }

                                }


                            }
                            temp.node_out2.is_clicked = !czy_usuwamy;
                        }

                    }
                    else if(figurs[which_is_linked.Item1] is Start)
                    {
                        czy_usuwamy = true;
                        Start temp = figurs[which_is_linked.Item1] as Start;


                        for (int j = 0; j < figurs.Count; j++)
                        {

                            path = new System.Drawing.Drawing2D.GraphicsPath();
                            if (figurs[j] is Rect && which_is_linked.Item1 != j)
                            {

                                Rect temp2 = figurs[j] as Rect;

                                path.AddEllipse(temp2.node_in.rectangle);
                                if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                {
                                    temp2.node_in.is_clicked = true;
                                    temp2.node_in.connected_with = temp.node_out;
                                    temp.node_out.connected_with = temp2.node_in;
                                    czy_usuwamy = false;
                                    break;
                                }
                            }
                            else if (figurs[j] is Rhombus && which_is_linked.Item1 != j)
                            {
                                Rhombus temp2 = figurs[j] as Rhombus;
                                path.AddEllipse(temp2.node_in.rectangle);
                                if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                {
                                    temp2.node_in.connected_with = temp.node_out;
                                    temp2.node_in.is_clicked = true;
                                    temp.node_out.connected_with = temp2.node_in;
                                    czy_usuwamy = false;
                                    break;
                                }

                            }
                            else if (figurs[j] is Stop && which_is_linked.Item1 != j)
                            {
                                Stop temp2 = figurs[j] as Stop;
                                path.AddEllipse(temp2.node_in.rectangle);
                                if (path.IsVisible(e.X, e.Y) && temp2.node_in.connected_with == null)
                                {
                                    temp2.node_in.connected_with = temp.node_out;
                                    temp2.node_in.is_clicked = true;
                                    temp.node_out.connected_with = temp2.node_in;
                                    czy_usuwamy = false;
                                    break;
                                }
                            }

                        }
                        temp.node_out.is_clicked = !czy_usuwamy;
                    }
                }
                is_linking = false;
                pictureBox1.Refresh();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (which_checked >= 0)
            {
                figurs[which_checked].text = textBox1.Text;

                pictureBox1.Refresh();
            }
        }

        private void button11_Click(object sender, EventArgs e) //delete
        {
            with_figure = 5;
            for(int i=0; i<6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;
        }

        private void button7_Click(object sender, EventArgs e) //start
        {
            with_figure = 2;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;
        }

        private void button6_Click(object sender, EventArgs e) //stop
        {
            with_figure = 3;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;
        }

        private void button10_Click(object sender, EventArgs e) //Link
        {
            with_figure = 4;
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            buttons[with_figure].BackColor = Color.LightBlue;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
        }//Polish

        private void button8_Click(object sender, EventArgs e) //English
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
        }
        private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyResources(resources, ctl.Controls);
                
            }
        }
    }
   

}

