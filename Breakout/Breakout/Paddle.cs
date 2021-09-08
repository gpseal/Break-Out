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
        //private Ball ball;
        private int paddleSpeed;
        private Rectangle rectangle;
        private List<Ball> ballList;
        private List<DropBall> dropBallList;

        //paddle textures etc
        private TextureBrush tbrush;
        private Image paddleImage;
        private Brush tail1;
        private Brush tail2;
        private Brush tail3;
        private Brush tail4;
        private TextureBrush engineBrush;
        private Image engine;

        //for item drops
        private bool drop;

        public Paddle(Point position, Color colour, Graphics bufferGraphics, int width, int height, Size playArea, /*Ball ball,*/ List<Ball> ballList, List<DropBall> dropBallList)
        {
            
            this.dropBallList = dropBallList;
            this.ballList = ballList;
            //this.ball = ball;
            this.playArea = playArea;
            this.height = height;
            PaddleWidth = width;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(colour);
            this.position = position;
            introSpeed = 15;
            aniFrame = 0;
            paddleSpeed = 10;
            rectangle = new Rectangle(position.X, position.Y, width, height);

            //paddle animations
            paddleImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("p" + (aniFrame).ToString());
            tbrush = new TextureBrush(paddleImage);
            tail1 = new SolidBrush(Color.FromArgb(190, 204, 245, 255));
            tail2 = new SolidBrush(Color.FromArgb(150, 204, 245, 255));
            tail3 = new SolidBrush(Color.FromArgb(100, 204, 245, 255));
            tail4 = new SolidBrush(Color.FromArgb(50, 204, 245, 255));
            engine = (Bitmap)Properties.Resources.ResourceManager.GetObject("engine");
            engineBrush = new TextureBrush(engine);
            
        }

        public void Intro()
        {
            position.Y -= introSpeed;
            introSpeed--;
        }

        public void MoveLeft()
        {
            int tailHeight = 4;
            if (position.X > 0)
            {
                position.X -= paddleSpeed;

                bufferGraphics.FillRectangle(tail1, position.X + PaddleWidth + paddleSpeed, position.Y + 7, paddleSpeed, tailHeight);
                bufferGraphics.FillRectangle(tail1, position.X + PaddleWidth + paddleSpeed, position.Y + 14, paddleSpeed, tailHeight);

                bufferGraphics.FillRectangle(tail2, position.X + PaddleWidth + paddleSpeed, position.Y + 7, paddleSpeed * 2, tailHeight);
                bufferGraphics.FillRectangle(tail2, position.X + PaddleWidth + paddleSpeed, position.Y + 14, paddleSpeed * 2, tailHeight);


                bufferGraphics.FillRectangle(tail3, position.X + PaddleWidth + 3 * paddleSpeed, position.Y + 7, paddleSpeed * 2, tailHeight);
                bufferGraphics.FillRectangle(tail3, position.X + PaddleWidth + 3 * paddleSpeed, position.Y + 14, paddleSpeed * 2, tailHeight);

                bufferGraphics.FillRectangle(tail4, position.X + PaddleWidth + 5 * paddleSpeed, position.Y + 7, paddleSpeed * 2, tailHeight);
                bufferGraphics.FillRectangle(tail4, position.X + PaddleWidth + 5 * paddleSpeed, position.Y + 14, paddleSpeed * 2, tailHeight);

                engineBrush.Transform = new Matrix(100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X + 8, position.Y); //adjusts position of texture
                bufferGraphics.FillRectangle(engineBrush, position.X + PaddleWidth - 10, position.Y, height, height);

            }
        }

        public void MoveRight()
        {
            int tailHeight = 4;
            if (position.X + PaddleWidth < playArea.Width)
            {
                position.X += paddleSpeed;
                bufferGraphics.FillRectangle(tail1, position.X - paddleSpeed*2, position.Y + 7, paddleSpeed, tailHeight);
                bufferGraphics.FillRectangle(tail1, position.X - paddleSpeed*2, position.Y + 14, paddleSpeed, tailHeight);

                bufferGraphics.FillRectangle(tail2, position.X - paddleSpeed * 3, position.Y + 7, paddleSpeed * 2, tailHeight);
                bufferGraphics.FillRectangle(tail2, position.X - paddleSpeed * 3, position.Y + 14, paddleSpeed * 2, tailHeight);

                bufferGraphics.FillRectangle(tail3, position.X -  paddleSpeed * 5, position.Y + 7, paddleSpeed * 2, tailHeight);
                bufferGraphics.FillRectangle(tail3, position.X -  paddleSpeed * 5, position.Y + 14, paddleSpeed * 2, tailHeight);

                bufferGraphics.FillRectangle(tail4, position.X - paddleSpeed * 7, position.Y + 7, paddleSpeed * 4, tailHeight);
                bufferGraphics.FillRectangle(tail4, position.X - paddleSpeed * 7, position.Y + 14, paddleSpeed * 4, tailHeight);
                //position.X += paddleSpeed;

                engineBrush.Transform = new Matrix(100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X + 8, position.Y); //adjusts position of texture
                bufferGraphics.FillRectangle(engineBrush, position.X - paddleSpeed, position.Y, height, height);

            }


        }

        public void Hit()
        {

            foreach (Ball eachBall in ballList)
            {
                if (rectangle.Contains(eachBall.BallTopMiddle, eachBall.BallBottom) || rectangle.Contains(eachBall.BallTopMiddle, eachBall.BallTop))
                {
                    eachBall.PaddleBounce();
                    //hit = true;
                }

                if (rectangle.Contains(eachBall.BallLeft, eachBall.BallSideMiddle) || rectangle.Contains(eachBall.BallRight, eachBall.BallSideMiddle))
                {
                    eachBall.BrickBounceSide();
                }

            }

            foreach (DropBall eachDrop in dropBallList)
            {
                drop = false;

                if (rectangle.Contains(eachDrop.X1, eachDrop.Y1 + 10) || rectangle.Contains(eachDrop.X1 + 70, eachDrop.Y1))
                {
                    drop = true;
                    eachDrop.Y1 += 100;
                }

            }


            //if (rectangle.Contains(ball.BallTopMiddle, ball.BallBottom) || rectangle.Contains(ball.BallTopMiddle, ball.BallTop))
            //{
            //    ball.PaddleBounce();
            //    //hit = true;
            //}

            //if (rectangle.Contains(ball.BallLeft, ball.BallSideMiddle) || rectangle.Contains(ball.BallRight, ball.BallSideMiddle))
            //{
            //    ball.BrickBounceSide();
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

            paddleImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("p" + (aniFrame).ToString()); //changes image to next frame in set
            tbrush = new TextureBrush(paddleImage); //applies new texture brush
            tbrush.Transform = new Matrix(100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X, position.Y); //adjusts position of texture
            bufferGraphics.FillRectangle(tbrush, position.X, position.Y, PaddleWidth, height); //draws image using textureBrush
            aniFrame++;

            
        }
        
        public Point Position { get => position; set => position = value; }
        public bool Drop { get => drop; set => drop = value; }
    }
}
