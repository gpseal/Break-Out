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
        private Brush blue;
        private Brush white;
        private int Y;
        private int X;

        public DropBall(Graphics bufferGraphics, Point position)
        {
            this.position = position;
            this.bufferGraphics = bufferGraphics;
            blue = new SolidBrush(Color.BlueViolet);
            white = new SolidBrush(Color.White);
            Y = position.Y;
            X = position.X;
        }

        public void Draw()
        {
            bufferGraphics.FillEllipse(blue, X+30, Y, 20, 20);
            bufferGraphics.FillEllipse(white, X + 35, Y+5, 10, 10);
        }

        public void Move()
        {
            Y += 4;
        }

        public Point Position { get => position; set => position = value; }
        public int Y1 { get => Y; set => Y = value; }
        public int X1 { get => X; set => X = value; }
    }
}
