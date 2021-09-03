using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Breakout
{
    class Brick
    {
        private Graphics bufferGraphics;
        private Point position;
        private Brush brush;
        private Pen highlightPen;
        private Pen shadowPen;
        private Brush highlight;
        private Brush shadow;
        private int width;
        private int height;
        private Ball ball;
        private bool hit;
        private int brickNum;

        public Brick(Point position, Color colour, Graphics bufferGraphics, int width, int height, Ball ball, int brickNum)
        {
            this.height = height;
            this.width = width;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(colour);
            highlightPen = new Pen(Brushes.White);
            shadowPen = new Pen(Brushes.Black);
            shadow = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            highlight = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
            this.position = position;
            this.ball = ball;
            hit = false;
            this.brickNum = brickNum;
        }

        public void IntroAnim()
        {
            position.Y += 50;
        }

        public void Draw()
        {
            bufferGraphics.FillRectangle(brush, position.X, position.Y, width, height);
            bufferGraphics.FillRectangle(highlight, position.X + 2, position.Y + 2, width - 4, height - 4);
            bufferGraphics.DrawLine(highlightPen, new Point(position.X, position.Y), new Point(position.X, position.Y + height));
            bufferGraphics.DrawLine(highlightPen, new Point(position.X, position.Y), new Point(position.X + width, position.Y));
            bufferGraphics.DrawLine(shadowPen, new Point(position.X, position.Y + height), new Point(position.X + width, position.Y + height));
            bufferGraphics.DrawLine(shadowPen, new Point(position.X + width, position.Y + height), new Point(position.X + width, position.Y));
        }

        public void Hit()
        {
            hit = false;
            if (ball.BallTop >= position.Y && ball.BallTop <= position.Y + 20 && ball.BallLeft >= position.X && ball.BallLeft <= position.X + width)
            {
                ball.BrickBounce();
                //hit = true;
                position.Y = -100;
            }
        }

        public bool Hit1 { get => hit; set => hit = value; }
        public int BrickNum { get => brickNum; set => brickNum = value; }
    }



}
