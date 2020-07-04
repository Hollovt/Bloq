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

    public abstract class Node
    {
        public Rectangle rectangle;


        public int x;
        public int y;
        public bool is_clicked;
        public abstract void DrawNode(PaintEventArgs e);


    }
}
