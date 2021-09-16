/*
 * Draws drop item, moves drop item, determines if drop item has been collected.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class DropBall
    {
        private Graphics bufferGraphics;
        private Point position;
        private Brush blue;
        private Brush white;
        private int Y;
        private int X;
        private bool drop;

        public DropBall(Graphics bufferGraphics, Point position)
        {
            this.position = position;
            this.bufferGraphics = bufferGraphics;
            blue = new SolidBrush(Color.BlueViolet);
            white = new SolidBrush(Color.White);
            Y = position.Y;
            X = position.X;
            drop = false;
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

        public void Hit(Rectangle paddle)
        {
            if (paddle.Contains(X, Y + 10) || paddle.Contains(X + 10, Y + 10) || paddle.Contains(X, Y) || paddle.Contains(X+10, Y)) /*https://docs.microsoft.com/en-us/dotnet/api/system.windows.rect.contains?view=net-5.0*/
            {
                drop = true;
                Y += 100;
            }
        }
        public bool Drop { get => drop; set => drop = value; }
    }
}
