/*
 * Moves ball, draws ball, bounces ball if it hits an object
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Ball
    {
        private const int NEG = -1;
        
        private Point position;
        private Point velocity;
        private Size playArea;
        private Graphics bufferGraphics;
        private int size;
        private int ballTop;
        private int ballLeft;
        private int ballBottom;
        private int ballRight;
        private int ballTopMiddle;
        private int ballSideMiddle;
        private bool ballOut;
        private bool dead;
        private bool brickDead;
        private Color colour;

        public Ball(Point position, Point velocity, Color colour, Graphics bufferGraphics, Size playArea, int size)
        {
            this.colour = colour;
            this.size = size;
            this.position = position;
            this.velocity = velocity;
            this.bufferGraphics = bufferGraphics;
            this.playArea = playArea;
            ballOut = false;
            brickDead = false;
        }

        //Draw ball
        public void Draw()
        {
            Brush brush = new SolidBrush(colour);
            Brush highlight = new SolidBrush(Color.White);
            Brush LightGray = new SolidBrush(Color.LightGray);
            Brush DarkGray = new SolidBrush(Color.DarkGray);
            bufferGraphics.FillEllipse(brush, position.X, position.Y, size, size);
            bufferGraphics.FillEllipse(DarkGray, position.X, position.Y, size - 3, size - 3);
            bufferGraphics.FillEllipse(LightGray, position.X + 1, position.Y + 1, size - 8, size - 8);
            bufferGraphics.FillEllipse(highlight, position.X + 3, position.Y + 3, size - 15, size - 15);
        }

        //Move ball by adding or subtracting velocity points to position points
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

        //detects if ball has come into contact with a brick, calls Bounce method if it has, sets touched brick to "dead"
        public void BrickBounce(Rectangle brick)
        {
            SoundPlayer brickHit = new SoundPlayer(Properties.Resources.brickDeath);
            brickDead = false;

            if (brick.Contains(BallTopMiddle, BallBottom) || brick.Contains(BallTopMiddle, BallTop)) //Checks to see if the mid bottom point, or midtop point of the ball have entered a brick or the paddle
            {
                brickHit.Play();
                brickDead = true; //brick will no longer be detected
                BounceUpDown();
            }

            if (brick.Contains(BallLeft, BallSideMiddle) || brick.Contains(BallRight, BallSideMiddle)) //Checks to see if the mid side points, have entered a brick or the paddle
            {
                brickHit.Play();
                brickDead = true;
                BounceLeftRight();
            }
        }

        //detects if ball has come into contact with the paddle, calls Bounce method if it has
        public void PaddleBounce(Rectangle brick)
        {
            SoundPlayer paddleHit = new SoundPlayer(Properties.Resources.paddleHit);
            brickDead = false;

            if (brick.Contains(ballLeft+8, ballTop+8) || brick.Contains(ballLeft + 2, ballTop + 8))
            {

            }

            if (brick.Contains(BallTopMiddle, BallBottom) || brick.Contains(BallTopMiddle, BallTop)) //Checks to see if the mid bottom point, or midtop point of the ball have entered a brick or the paddle
            {
                paddleHit.Play();
                BounceUpDown();
            }

            if (brick.Contains(BallLeft, BallSideMiddle) || brick.Contains(BallRight, BallSideMiddle)) //Checks to see if the mid side points, have entered a brick or the paddle
            {
                paddleHit.Play();
                BounceLeftRight();
            }
        }

        //detects if ball has come into contact with sides or top of playing area, calls appropriate bounce method if it has
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

        //detects if ball has left playing area
        public void BallOut()
        {
            if (position.Y >= playArea.Height && dead == false)
            {
                ballOut = true;
            }
        }

        //changes Y velocity of ball
        public void BounceUpDown()
        {
            velocity.Y *= NEG;
        }

        //changes X velocity of ball
        public void BounceLeftRight()
        {
            velocity.X *= NEG;
        }

        //changes X and Y velocity of ball
        public void BounceCorner()
        {
            velocity.X *= NEG;
            velocity.Y *= NEG;
        }

        //resets position of ball after player life is lost
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
        public bool BallOut1 { get => ballOut; set => ballOut = value; }
        public bool Dead { get => dead; set => dead = value; }
        public bool BrickDead { get => brickDead; set => brickDead = value; }
    }
}
