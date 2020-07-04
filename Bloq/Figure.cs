using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloq
{
    [Serializable]
    public abstract class Figure
    {
        public int x = 0;
        public int y = 0;
        public int heigh = 0;
        public int width = 0;
        public string text;
        public abstract void DrawRect(PaintEventArgs e);
        // public Font fnt = new Font("Arial", 10);
        public bool is_checked = false;
    }
}
