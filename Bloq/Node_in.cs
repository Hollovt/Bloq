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

    public class Node_in : Node
    {

        //[NonSerialized()]
        public Node_out connected_with;


        public Node_in()
        {

        }

        public Node_in(Rectangle rec)
        {
            is_clicked = false;
            rectangle = rec;
            x = rectangle.X;
            y = rectangle.Y;
            connected_with = null;
        }

        public override void DrawNode(PaintEventArgs e)
        {

        }
    }
}
