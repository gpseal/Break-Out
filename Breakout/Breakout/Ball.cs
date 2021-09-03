using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Ball
    {
        private const int NEG = -1;
        private const int BALLSIZE = 20;

        private Point position;
        private Point velocity;
        private Brush brush;
        private Brush DarkGray;
        private Brush LightGray;
        private Brush highlight;
        private Size playArea;
        private Graphics bufferGraphics;
        private int size;
        private int ballTop;
        private int ballLeft;
        private int ballBottom;
        private int ballRight;
        private int ballMiddle;
        //private Color highlight;


        public Ball(Point position, Point velocity, Color colour, Graphics bufferGraphics, Size playArea, int size)
        {
            //highlight = Color.White;
            this.size = size;
            this.position = position;
            this.velocity = velocity;
            highlight = new SolidBrush(Color.White);
            LightGray = new SolidBrush(Color.LightGray);
            DarkGray = new SolidBrush(Color.DarkGray);
            brush = new SolidBrush(colour);
            this.bufferGraphics = bufferGraphics;
            this.playArea = playArea;
            
        }

        public void Draw()
        {
            bufferGraphics.FillEllipse(brush, position.X, position.Y, size, size);
            bufferGraphics.FillEllipse(DarkGray, position.X, position.Y, size - 3, size - 3);
            bufferGraphics.FillEllipse(LightGray, position.X + 1, position.Y + 1, size - 8, size - 8);
            bufferGraphics.FillEllipse(highlight, position.X+3, position.Y+3, size-15, size-15);
        }

        public void Move()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            ballTop = position.Y;
            ballLeft = position.X;
            ballRight = ballBottom + BALLSIZE;
            ballBottom = ballTop + BALLSIZE;
            ballMiddle = position.X + (BALLSIZE/2);
        }

        public void Bounce()
        {
            
            if (position.X >= playArea.Width - BALLSIZE) //checks to see if ball is outside the boundary
            {
                velocity.X *= NEG;  //changes velocity to negative so ball changes direction and bounces off walls
            }

            if (position.Y >= playArea.Height - BALLSIZE)
            {
                velocity.Y *= -1;
            }

            if (position.X <= 0)
            {
                velocity.X *= -1;
            }

            if (position.Y <= 0)
            {
                velocity.Y *= -1;
            }
        }

        public void BrickBounce()
        { 
            velocity.Y *= -1;
        }

        public int BallTop { get => ballTop; set => ballTop = value; }
        public int BallLeft { get => ballLeft; set => ballLeft = value; }
        public int BallRight { get => ballRight; set => ballRight = value; }
        public int BallBottom { get => ballBottom; set => ballBottom = value; }
        public int BallMiddle { get => ballMiddle; set => ballMiddle = value; }
    }
}
