using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class DropBall
    {
        private Graphics bufferGraphics;
        private Point position;
        private Brush brush;
        private int Y;
        private int X;

        public DropBall(Graphics bufferGraphics, Point position)
        {
            this.position = position;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(Color.Orange);
            Y = 100;
            X = 200;
        }


        public void Draw()
        {
            bufferGraphics.FillRectangle(brush, position.X, position.Y, 70, 20);
        }

        public void Move()
        {
            position.Y += 4;
        }


    }
}
