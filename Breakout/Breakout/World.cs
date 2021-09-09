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
    
    class World
    {
        private int counter;

        private Graphics bufferGraphics;
        //private Ball ball;
        private List<Brick> brickList;
        private List<Ball> ballList;

        private List<DropBall> dropBallList;

        private int brickX;
        private int brickY;
        private int brickWidth;
        private int brickHeight;
        private int brickGap;
        private Color[]brickColour;
        private int brickNum;
        private Paddle paddle;
        private int paddleWidth;
        private int introCount;
        private int introNum;
        private Image background;
        private TextureBrush tbrush;
        private Size playArea;
        private Timer timer1;
        private TextBox texBox1;
        private Random random;
        private Point brickPos;
        private int backGroundAnimation;

        private int score;


        private int paddleIntro;  //used for timing of paddle intro

        public World(Graphics bufferGraphics, Size playArea, Timer timer1, TextBox texBox1, Random random)
        {
            this.random = random;
            this.texBox1 = texBox1;
            this.timer1 = timer1;
            this.playArea = playArea;
            this.bufferGraphics = bufferGraphics;
            Color[] brickColour = new Color[6];
            brickColour[0] = Color.Yellow;
            brickColour[1] = Color.Orange;
            brickColour[2] = Color.Red;
            brickColour[3] = Color.Blue;
            brickColour[4] = Color.Purple;
            brickColour[5] = Color.Black;

            brickNum = 0;
            brickX = 0;
            brickY = -120;
            brickWidth = 69;
            brickHeight = 19;
            brickGap = 1;
            introCount = 0;
            paddleWidth = 100;

            ballList = new List<Ball>();
            ballList.Add(new Ball(new Point(150, 300), new Point(5, 5), Color.DimGray, bufferGraphics, playArea, 20, paddleWidth));
            //ballList.Add(new Ball(new Point(150, 300), new Point(-5, 5), Color.DimGray, bufferGraphics, playArea, 20, paddleWidth));

            dropBallList = new List<DropBall>();
            //ball2 = new Ball(new Point(450, 300), new Point(-5, 5), Color.DimGray, bufferGraphics, playArea, 20, paddleWidth);

            paddle = new Paddle(new Point(300, 588), Color.PaleVioletRed, bufferGraphics, paddleWidth, 20, playArea/*, ball*/, ballList, dropBallList);
            brickList = new List<Brick>();

            for (int rows = 0; rows < 5; rows++)
            { 
                for (int i = 0; i < 10; i++)
                {
                    brickList.Add(new Brick(new Point(brickX, brickY), brickColour[rows], bufferGraphics, brickWidth, brickHeight/*, ball*/, ballList, brickNum, texBox1, random));
                    brickX += (brickWidth + brickGap);
                    brickNum++;
                    introCount++;
                }
                brickX = 0;
                brickY += (brickHeight + brickGap);
            }

            paddleIntro = introCount - 4;
            introCount--; //takes a number off introcount as it is used again to cycle through list of bricks for intro


            counter = 0;

            introNum = 0;

            brickPos = new Point(500, 400);

            backGroundAnimation = 0;

            score = 0;
        }

        public void PaddleMove(string direction)
        {
            switch (direction)
            {
                case "Left":
                    paddle.MoveLeft();
                    break;

                case "Right":
                    paddle.MoveRight();
                    break;
            }
        }

        public void Pause()
        {
            
            timer1.Enabled = !timer1.Enabled;
            
        }

        public void BrickIntro()
        {
            brickList[introCount].IntroAnim(); //runs intro anmimation for brick
            introNum++;

            if (introNum >= 6) //intro takes 6 frames
            {
                introCount--; //intro animation moves to next brick
                introNum = 0;  //resets fram count
            }
        }

        public void Run()
        {

            texBox1.Text = score.ToString();
            Background();

            if (introCount > -1)
            {
                BrickIntro();
            }

            brickNum = 0;
            foreach (Brick eachBrick in brickList)
            {
                
                if (eachBrick.Score == true)
                {
                    score += 10;
                    eachBrick.Score = false;
                }

                if (eachBrick.Dead == true)
                {
                    eachBrick.Kill();
                }

                else
                {
                    //eachBrick.Intro();
                    brickPos = eachBrick.Position;

                    eachBrick.Draw();
                    eachBrick.Hit();
                    if (eachBrick.Drop == true) //checks if brick has an item drop
                    {
                        SpawnDropBall(brickPos);
                        eachBrick.Drop = false;
                    }
                    brickNum++;
                }

            }

            //foreach (Ball eachBall in ballList)
            //{
            //    eachBall.PaddleX = paddle.Position.X;
            //}

            //ball.PaddleX = paddle.Position.X;  //gives ball class information regarding position of paddle (for bouncing near edges of panel)
            paddle.Hit();

            if (introCount > paddleIntro)
            {
                paddle.Intro();
            }

            if (paddle.Drop == true)
            {
                SpawnBall();
            }


            paddle.Draw();


            foreach (Ball eachBall in ballList)
            {
                eachBall.PaddleX = paddle.Position.X;
                eachBall.Move();
                eachBall.Bounce();
                eachBall.Draw();
            }

            foreach (DropBall eachDrop in dropBallList) //draws and animates brick drop for extra ball
            {
                eachDrop.Move();
                eachDrop.Draw();
            }

            //ball.Bounce();
            //ball.Move();
            //ball.Draw();
        }

        public void SpawnBall()
        {
            ballList.Add(new Ball(new Point(200, 200), new Point(5, 5), Color.DimGray, bufferGraphics, playArea, 20, paddleWidth));
        }

        public void SpawnDropBall(Point position)
        {

            dropBallList.Add(new DropBall(bufferGraphics, position));
        }

        public void Background()
        {
            if (backGroundAnimation == 4)
            {
                backGroundAnimation = 0;
            }

            background = (Bitmap)Properties.Resources.ResourceManager.GetObject("b" + (backGroundAnimation).ToString()); //applies new image to background
            tbrush = new TextureBrush(background); //applies new texture brush

            bufferGraphics.FillRectangle(tbrush, 0, 0, playArea.Width, playArea.Height);

            if (counter % 5 == 0)  //if counter is divisible by 5 add one to animation counter
            {
                backGroundAnimation++;
            }

            counter++;
        }
        public int BrickNum { get => brickNum; set => brickNum = value; }



    }
}
