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
        private SoundPlayer brickHit;
        private Graphics bufferGraphics;
        private Point position;
        private Brush brush;
        private Pen highlightPen;
        private Pen shadowPen;
        private Brush highlight;
        private Brush shadow;
        private int width;
        private int height;
        //private Ball ball;
        private bool hit;
        private bool drop;
        private int brickNum;
        private Rectangle rectangle;
        private TextBox texBox1;
        private List<Ball> ballList;
        private List<DropBall> items;
        private Random random;
        private int dropNum;
        private Brush death; //brush used when brick eliminated
        private bool dead;
        private int transparent;
        private bool score;
        private Size playArea;

        private int moveCount;  //keeps track of brick movement in level 2
        private int brickMove;
        private int brickMoveHorizontal;
        private bool dropable;

        public Brick(Point position, Color colour, Graphics bufferGraphics, int width, int height, /*Ball ball,*/ List<Ball> ballList, Random random, Size playArea)
        {

            this.playArea = playArea;
            brickHit = new SoundPlayer(Properties.Resources.brickHit);
            transparent = 255;
            this.ballList = ballList;
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
            //this.ball = ball;
            hit = false;
            this.brickNum = brickNum;
            rectangle = new Rectangle(position.X, position.Y, width, height);
            items = new List<DropBall>();
            drop = false;
            this.random = random;
            dropNum = 1;  /*random.Next(5);*/  //
            dead = false;

            //Brick movement
            brickMove = 1;  //amount bricks will move in certain levels
            brickMoveHorizontal = 5;
            moveCount = 0;
            dropable = true;  //means that brick has the ability to drop an item

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

        public void MoveHorizontal()
        {
            position.X += brickMoveHorizontal;
            //moveCount++;
            if (position.X < 0 || position.X + width > playArea.Width)  //amount the bricks will move before moving back up the screen
            {
                brickMoveHorizontal *= -1;
                //moveCount = 0;
            }
        }


        public bool Hit1 { get => hit; set => hit = value; }
        public int BrickNum { get => brickNum; set => brickNum = value; }
        public Point Position { get => position; set => position = value; }
        public bool Drop { get => drop; set => drop = value; }
        public bool Dead { get => dead; set => dead = value; }
        public bool Score { get => score; set => score = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
    }



}
