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
        private int PaddleWidth;
        private int height;
        private int introSpeed;
        private int aniFrame;
        private Size playArea;
        private Ball ball;
        private int paddleSpeed;

        public Paddle(Point position, Color colour, Graphics bufferGraphics, int width, int height, Size playArea, Ball ball)
        {
            this.ball = ball;
            this.playArea = playArea;
            this.height = height;
            this.PaddleWidth = width;
            this.bufferGraphics = bufferGraphics;
            brush = new SolidBrush(colour);
            this.position = position;
            highlightPen = new Pen(Brushes.White);
            shadowPen = new Pen(Brushes.Black);
            introSpeed = 15;
            aniFrame = 0;
            paddleSpeed = 12;

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

        }

        public void Draw()
        {
            if (aniFrame == 20)
            {
                aniFrame = 0;
            }
            
            bufferGraphics.FillRectangle(brush, position.X, position.Y, PaddleWidth, height); //draws image using textureBrush
            aniFrame++;
        }

    }
}
