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
        private const int DROPMOVE = 4;
        private const int DROPELIMINATE = 100;
        private const int DROPWIDTH = 20;
        private const int DROPINNERWIDTH = 10;

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
            bufferGraphics.FillEllipse(blue, X + 30, Y, DROPWIDTH, DROPWIDTH);
            bufferGraphics.FillEllipse(white, X + 35, Y + 5, DROPINNERWIDTH, DROPINNERWIDTH);
        }

        public void Move()
        {
            Y += DROPMOVE;
        }

        public void Hit(Rectangle paddle)
        {
            if (paddle.Contains(X, Y + DROPWIDTH) || paddle.Contains(X + DROPWIDTH, Y + DROPWIDTH) || paddle.Contains(X, Y) || paddle.Contains(X + DROPWIDTH, Y)) /*https://docs.microsoft.com/en-us/dotnet/api/system.windows.rect.contains?view=net-5.0*/
            {
                drop = true;
                Y += DROPELIMINATE;
            }
        }
        public bool Drop { get => drop; set => drop = value; }
    }
}
