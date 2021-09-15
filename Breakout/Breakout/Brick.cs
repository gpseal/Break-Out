using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    class Brick
    {
        
        private Color colour;
        private Graphics bufferGraphics;
        private Point position;
        private int width;
        private int height;
        private Rectangle rectangle;
        private bool hit;
        private bool drop;
        private Random random;
        private int dropNum;
        private Brush death; //brush used when brick eliminated
        private bool dead;
        private int transparent;
        private Size playArea; //keeps track of brick positions in level 2
        private int moveCount;  //keeps track of brick movement in level 2
        private bool dropable;
        private int brickMoveHorizontal;
        private int brickMove;

        public Brick(Point position, Color colour, Graphics bufferGraphics, int width, int height, Random random, Size playArea)
        {
            this.colour = colour;

            this.playArea = playArea;
            transparent = 255;
            this.height = height;
            this.width = width;
            this.bufferGraphics = bufferGraphics;
       
            this.position = position;
            hit = false;
            rectangle = new Rectangle(position.X, position.Y, width, height);
            drop = false;
            this.random = random;
            dropNum = random.Next(5);  //chance that that brick drops item
            dead = false;

            //Brick movement
            moveCount = 0;
            brickMove = 1;
            brickMoveHorizontal = 5;
            dropable = true;  //means that brick has the ability to drop an item
        }

        //bricks fall from above top of screen to begin game, runs for 6 frames
        public void IntroAnim()
        {
            position.Y += 25; 
        }

        //draws bricks
        public void Draw()
        {
            Pen highlightPen = new Pen(Brushes.White);
            Pen shadowPen = new Pen(Brushes.Black);
            SolidBrush shadow = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            SolidBrush highlight = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
            SolidBrush brush = new SolidBrush(colour);

            rectangle.X = position.X;
            rectangle.Y = position.Y;
            bufferGraphics.FillRectangle(brush, position.X, position.Y, width, height);
            bufferGraphics.FillRectangle(highlight, position.X + 2, position.Y + 2, width - 4, height - 4);
            bufferGraphics.DrawLine(highlightPen, new Point(position.X, position.Y), new Point(position.X, position.Y + height));
            bufferGraphics.DrawLine(highlightPen, new Point(position.X, position.Y), new Point(position.X + width, position.Y));
            bufferGraphics.DrawLine(shadowPen, new Point(position.X, position.Y + height), new Point(position.X + width, position.Y + height));
            bufferGraphics.DrawLine(shadowPen, new Point(position.X + width, position.Y + height), new Point(position.X + width, position.Y));
            
        }

        public void Kill()
        {
            if (transparent>0)  //fades brick from sight after it has been hit
            {
                death = new SolidBrush(Color.FromArgb(transparent, 255, 255, 255));
                bufferGraphics.FillRectangle(death, position.X, position.Y, width, height);
                transparent -= 20;
            }

            if (dropNum == 1 && dropable == true)
            {
                SoundPlayer dropItem = new SoundPlayer(Properties.Resources.drop);
                dropItem.Play();
                drop = true; //triggers item drop for brick
                dropable = false;
            }
        }

        //moves bricks in level 2
        public void Move()
        {
            position.Y += brickMove;
            moveCount++;
            if (moveCount >= 100)  //amount the bricks will move before moving back up the screen
            {
                brickMove *= -1;
                moveCount = 0;
            }
        }

        //moves bricks in level 3
        public void MoveHorizontal()
        {
            position.X += brickMoveHorizontal;
            if (position.X < 0 || position.X + width > playArea.Width)  //amount the bricks will move before moving back up the screen
            {
                brickMoveHorizontal *= -1;
            }
        }

        public bool Hit1 { get => hit; set => hit = value; }
        public Point Position { get => position; set => position = value; }
        public bool Drop { get => drop; set => drop = value; }
        public bool Dead { get => dead; set => dead = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
    }



}
