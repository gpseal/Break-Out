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
        }

        public void Bounce()
        {

            if (position.X >= playArea.Width - size) //checks to see if ball is outside the boundary
            {
                velocity.X *= NEG;  //changes velocity to negative so ball changes direction and bounces off walls
            }

            if (position.Y >= playArea.Height - size)
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

        public void BrickBounceVert()
        {
            velocity.Y *= -1;
            score += 10;  //adds to total score, sent back world
        }

        public void BrickBounceSide()
        {
            velocity.X *= -1;
            score += 10;  //adds to total score, sent back world
        }

        public void PaddleBounce()
        {
            
            velocity.Y *= -1;

            if (ballTopMiddle >= PaddleX && ballTopMiddle <= PaddleX + size) //if ball hits paddle near edge, velocity x will be changed
            {
                velocity.X +=2;
            }

            else if (ballTopMiddle <= PaddleX + paddleWidth && ballTopMiddle >= PaddleX + paddleWidth - size) //if ball hits paddle near edge velocity x will be changed
            {
                velocity.X -= 2;
            }

            else //if ball does not hit near edge of paddle, velocity X reurns to normal
            {
                if (velocity.X < 0)
                {
                    velocity.X = -5;
                }

                else
                {
                    velocity.X = 5;
                }

            }

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
    }
}
