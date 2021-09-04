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
        private Size playArea;
        private Ball ball;

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


        }

        public void Intro()
        {
            position.Y -= introSpeed;
            introSpeed--;
        }

        public void MoveLeft()
        {
            
            
        }

        public void MoveRight()
        {

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
            
            bufferGraphics.FillRectangle(brush, position.X, position.Y, width, height); //draws image using textureBrush
            aniFrame++;
        }

    }
}
