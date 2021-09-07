using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class ItemDrop
    {
        private Graphics bufferGraphics;
        private Point position;
        private Brush brush;

        public ItemDrop(Graphics bufferGraphics)
        {
            //this.position = position;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(Color.Orange);
        }

        public void draw()
        {
            bufferGraphics.FillRectangle(brush, 400, 400, 100, 100);
        }
    }
}
