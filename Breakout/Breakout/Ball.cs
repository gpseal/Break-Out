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
        
        private int paddleX;
        private int paddleWidth;
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
        private int score;
        private int ballTopMiddle;
        private int ballSideMiddle;
        private int ballBottomRight;
        private int ballBottomLeft;
        private bool ballOut;
        private bool dead;
        private bool brickDead;

        private int test;
        //private Color highlight;


        public Ball(Point position, Point velocity, Color colour, Graphics bufferGraphics, Size playArea, int size, int paddleWidth)
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
            this.paddleWidth = paddleWidth;
            score = 0; //score for game kept, will tick up when a brick bounce method is called
            ballOut = false;
            brickDead = false;
        }

        public void Draw()
        {
            bufferGraphics.FillEllipse(brush, position.X, position.Y, size, size);
            bufferGraphics.FillEllipse(DarkGray, position.X, position.Y, size - 3, size - 3);
            bufferGraphics.FillEllipse(LightGray, position.X + 1, position.Y + 1, size - 8, size - 8);
            bufferGraphics.FillEllipse(highlight, position.X + 3, position.Y + 3, size - 15, size - 15);
        }

        public void Move()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            ballTop = position.Y;
            ballLeft = position.X;
            ballTopMiddle = position.X + (size / 2);
            ballSideMiddle = position.Y + (size / 2);
            ballRight = ballLeft + size;
            ballBottom = ballTop + size;
            BallOut();
        }

        public void BrickBounce(Rectangle brick)
        {
            brickDead = false;

            if (brick.Contains(BallTopMiddle, BallBottom) || brick.Contains(BallTopMiddle, BallTop)) //Checks to see if the mid bottom point, or midtop point of the ball have entered a brick or the paddle
            {
                brickDead = true; //brick will no longer be detected
                BounceUpDown();
            }

            if (brick.Contains(BallLeft, BallSideMiddle) || brick.Contains(BallRight, BallSideMiddle)) //Checks to see if the mid side points, have entered a brick or the paddle
            {
                brickDead = true;
                BounceLeftRight();
            }
        }

        public void Bounce()

        {
            if (position.X >= playArea.Width - size) //checks to see if ball is outside the boundary
            {
                BounceLeftRight();  //changes velocity to negative so ball changes direction and bounces off walls
            }

            if (position.X <= 0)
            {
                BounceLeftRight();
            }

            if (position.Y <= 0)
            {
                BounceUpDown();
            }

        }

        public void BallOut()
        {
            if (position.Y >= playArea.Height + 100 && dead == false)
            {
                ballOut = true;
            }
        }

        public void BounceUpDown()
        {
            velocity.Y *= NEG;
        }

        public void BounceLeftRight()
        {
            velocity.X *= NEG;
        }

        //public void PaddleBounce()
        //{
        //    bufferGraphics.FillEllipse(highlight, position.X, position.Y + size -5, size, size-15);

        //    velocity.Y *= -1;

        //    if (ballTopMiddle >= PaddleX && ballTopMiddle <= PaddleX + size) //if ball hits paddle near edge, velocity x will be changed
        //    {
        //        velocity.X +=2;
        //    }

        //    else if (ballTopMiddle <= PaddleX + paddleWidth && ballTopMiddle >= PaddleX + paddleWidth - size) //if ball hits paddle near edge velocity x will be changed
        //    {
        //        velocity.X -= 2;
        //    }

        //    else //if ball does not hit near edge of paddle, velocity X reurns to normal
        //    {
        //        if (velocity.X < 0)
        //        {
        //            velocity.X = -5;
        //        }

        //        else
        //        {
        //            velocity.X = 5;
        //        }

        //    }

        //}

        public void Reset()
        {
            position.X = 300;
            position.Y = 200;
            dead = false;
            ballOut = false;
        }


        public int BallTop { get => ballTop; set => ballTop = value; }
        public int BallLeft { get => ballLeft; set => ballLeft = value; }
        public int BallRight { get => ballRight; set => ballRight = value; }
        public int BallBottom { get => ballBottom; set => ballBottom = value; }
        public int BallTopMiddle { get => ballTopMiddle; set => ballTopMiddle = value; }
        public int BallSideMiddle { get => ballSideMiddle; set => ballSideMiddle = value; }
        public int PaddleX { get => paddleX; set => paddleX = value; }
        public int Score { get => score; set => score = value; }
        public int Size { get => size; set => size = value; }
        public bool BallOut1 { get => ballOut; set => ballOut = value; }
        public Point Position { get => position; set => position = value; }
        public bool Dead { get => dead; set => dead = value; }
        public bool BrickDead { get => brickDead; set => brickDead = value; }
    }
}
