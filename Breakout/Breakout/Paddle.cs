using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Paddle
    {
        private Graphics bufferGraphics;
        private Point position;
        private Brush brush;
        private int PaddleWidth;
        private int height;
        private int introSpeed;
        private int aniFrame;
        private Size playArea;
        private Ball ball;
        private int paddleSpeed;
        private Rectangle rectangle;


        public Paddle(Point position, Color colour, Graphics bufferGraphics, int width, int height, Size playArea, Ball ball)
        {
            this.ball = ball;
            this.playArea = playArea;
            this.height = height;
            PaddleWidth = width;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(colour);
            this.position = position;
            introSpeed = 15;
            aniFrame = 0;
            paddleSpeed = 12;
            rectangle = new Rectangle(position.X, position.Y, width, height);

        }

        public void Intro()
        {
            position.Y -= introSpeed;
            introSpeed--;
        }

        public void MoveLeft()
        {
            if (position.X > 0)
            {
                position.X -= paddleSpeed;
            }
        }

        public void MoveRight()
        {
            if (position.X + PaddleWidth < playArea.Width)
            {
                position.X += paddleSpeed;
            }
            
        }

        public void Hit()
        {
            if (rectangle.Contains(ball.BallTopMiddle, ball.BallBottom) || rectangle.Contains(ball.BallTopMiddle, ball.BallTop))
            {
                ball.BrickBounceVert();
                //hit = true;
            }

            if (rectangle.Contains(ball.BallLeft, ball.BallSideMiddle) || rectangle.Contains(ball.BallRight, ball.BallSideMiddle))
            {
                ball.BrickBounceSide();
                //hit = true;
            }

            //int brickTop = position.Y;
            //int brickBottom = position.Y + height;
            //int brickLeft = position.X;
            //int brickRight = position.X + PaddleWidth;


            //if (ball.BallLeft <= brickRight && ball.BallSideMiddle >= brickTop && ball.BallSideMiddle <= brickBottom && ball.BallRight > brickLeft)  //detects left hit
            //{
            //    ball.BrickBounceSide();
            //}

            //else if (ball.BallRight >= brickLeft && ball.BallSideMiddle >= brickTop && ball.BallSideMiddle <= brickBottom && ball.BallLeft < brickRight)  //detects right hit
            //{
            //    ball.BrickBounceSide();
            //}

            //else if (ball.BallTop <= brickBottom && ball.BallTopMiddle <= brickRight && ball.BallTopMiddle >= brickLeft && ball.BallBottom >= brickBottom) // detect if ball hits bottom
            //{
            //    ball.BrickBounceVert();
            //}

            //else if (ball.BallBottom >= brickTop && ball.BallTopMiddle <= brickRight && ball.BallTopMiddle >= brickLeft && ball.BallTop <= brickTop) // detect if ball hits top
            //{
            //    ball.BrickBounceVert();
            //}
        }

        public void Draw()
        {
            if (aniFrame == 20)
            {
                aniFrame = 0;
            }
            rectangle.X = position.X;
            rectangle.Y = position.Y;

            bufferGraphics.FillRectangle(brush, position.X, position.Y, PaddleWidth, height); //draws image using textureBrush
            aniFrame++;
        }

        public Point Position { get => position; set => position = value; }

    }
}
