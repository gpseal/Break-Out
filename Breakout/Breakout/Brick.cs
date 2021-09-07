using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private Rectangle rectangle;
        private int score;
        private TextBox texBox1;

        public Brick(Point position, Color colour, Graphics bufferGraphics, int width, int height, Ball ball, int brickNum, TextBox texBox1)
        {
            this.texBox1 = texBox1;
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
            rectangle = new Rectangle(position.X, position.Y, width, height);
        }

        public void IntroAnim()
        {
            position.Y += 25; //bricks fall from above top of screen to begin game
        }

        public void Draw()
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
            bufferGraphics.FillRectangle(brush, position.X, position.Y, width, height);
            bufferGraphics.FillRectangle(highlight, position.X + 2, position.Y + 2, width - 4, height - 4);
            bufferGraphics.DrawLine(highlightPen, new Point(position.X, position.Y), new Point(position.X, position.Y + height));
            bufferGraphics.DrawLine(highlightPen, new Point(position.X, position.Y), new Point(position.X + width, position.Y));
            bufferGraphics.DrawLine(shadowPen, new Point(position.X, position.Y + height), new Point(position.X + width, position.Y + height));
            bufferGraphics.DrawLine(shadowPen, new Point(position.X + width, position.Y + height), new Point(position.X + width, position.Y));
            
        }

        public void Hit()
        {
            if (rectangle.Contains(ball.BallTopMiddle, ball.BallBottom) || rectangle.Contains(ball.BallTopMiddle, ball.BallTop))
            {
                ball.BrickBounceVert();
                //hit = true;
                position.Y = -100;
            }

            if (rectangle.Contains(ball.BallLeft, ball.BallSideMiddle) || rectangle.Contains(ball.BallRight, ball.BallSideMiddle))
            {
                ball.BrickBounceSide();
                //hit = true;
                position.Y = -100;
            }




            //int brickTop = position.Y;
            //int brickBottom = position.Y + height;
            //int brickLeft = position.X;
            //int brickRight = position.X + width;


            //if (ball.BallLeft <= brickRight && ball.BallSideMiddle >= brickTop && ball.BallSideMiddle <= brickBottom && ball.BallRight > brickLeft)  //detects left hit
            //{
            //    ball.BrickBounceSide();
            //    //hit = true;
            //    position.Y = -100;
            //}

            //else if (ball.BallRight >= brickLeft && ball.BallSideMiddle >= brickTop && ball.BallSideMiddle <= brickBottom && ball.BallLeft < brickRight && hit == false)  //detects right hit
            //{
            //    ball.BrickBounceSide();
            //    //hit = true;
            //    position.Y = -100;
            //}

            //else if (ball.BallTop <= brickBottom && ball.BallTopMiddle <= brickRight && ball.BallTopMiddle >= brickLeft && ball.BallBottom >= brickBottom && hit == false) // detect if ball hits bottom
            //{
            //    ball.BrickBounceVert();
            //    //hit = true;
            //    position.Y = -100;
            //}

            //else if (ball.BallBottom >= brickTop && ball.BallTopMiddle <= brickRight && ball.BallTopMiddle >= brickLeft && ball.BallTop <= brickTop && hit == false) // detect if ball hits top
            //{
            //    ball.BrickBounceVert();
            //    //hit = true;
            //    position.Y = -100;
            //}

            //hit = false;
        }

        public bool Hit1 { get => hit; set => hit = value; }
        public int BrickNum { get => brickNum; set => brickNum = value; }
        public int Score { get => score; set => score = value; }
    }



}
