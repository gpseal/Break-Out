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
        private Pen highlightPen;
        private Pen shadowPen;
        private Brush highlight;
        private Brush shadow;
        private int width;
        private int height;
        private int introSpeed;
        private int aniFrame;
        private Image paddleImage;
        private Image flameImage;
        private TextureBrush tbrush;
        private TextureBrush flameBrush;
        private Size playArea;
        private Ball ball;
        private int flameCount;

        public Paddle(Point position, Color colour, Graphics bufferGraphics, int width, int height, Size playArea, Ball ball)
        {
            this.ball = ball;
            this.playArea = playArea;
            this.height = height;
            this.width = width;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(colour);
            this.position = position;
            highlightPen = new Pen(Brushes.White);
            shadowPen = new Pen(Brushes.Black);
            introSpeed = 15;
            aniFrame = 0;
            paddleImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("p" + (aniFrame).ToString());
            tbrush = new TextureBrush(paddleImage);
            flameImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("flame");
            flameBrush = new TextureBrush(flameImage);
            flameCount = 0;

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
                position.X -= 12;
                if (flameCount > 1)
                {
                    bufferGraphics.FillRectangle(flameBrush, position.X + 112, position.Y, 20, 20);
                    flameCount = 0;
                }
                flameCount++;
            }
            
        }

        public void MoveRight()
        {
            
            if (position.X + 100 < playArea.Width)
            {
                position.X += 12;
                if (flameCount > 1)
                {
                    bufferGraphics.FillRectangle(flameBrush, position.X-30, position.Y, 20, 20);
                    flameCount = 0;
                }
                flameCount++;
            }
        }

        public void Hit()
        {
            if (ball.BallBottom >= position.Y && ball.BallMiddle >= position.X && ball.BallMiddle <= position.X + width)
            {
                ball.BrickBounce();
            }
        }

        public void Draw()
        {
            if (aniFrame == 20)
            {
                aniFrame = 0;
            }
            
            paddleImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("p" + (aniFrame).ToString()); //changes image to next frame in set
            tbrush = new TextureBrush(paddleImage); //applies new texture brush
            tbrush.Transform = new Matrix (100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X, position.Y); //adjusts position of texture
            bufferGraphics.FillRectangle(tbrush, position.X, position.Y, width, height); //draws image using textureBrush
            aniFrame++;
        }

    }
}
