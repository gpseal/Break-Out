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
        private const int PADDLEWIDTH = 100;

        private int counter;

        private Graphics bufferGraphics;
        //private Ball ball;
        private List<Brick> brickList;
        private List<Ball> ballList;

        private int lives;
        private List<DropBall> dropBallList;
        
        private int brickX;
        private int brickY;
        private int brickWidth;
        private int brickHeight;
        private int brickGap;
        private Color[]brickColour;
        private int brickNum;
        private Paddle paddle;
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

        private int activeBalls;

        //Menus and intro
        private int titleColor;
        private Label title;
        private Label label1;
        private Label label2;
        private Label label3;
        private List<Label> labels;
        private Button button2;
        private Button button3;
        private int paddleIntro;  //used for timing of paddle intro

        private bool dead;
        private bool intro; //allows intro to run
        private bool levelComplete;

        //brick variables
        private int rows;
        private int columns;
        private int brickCount;

        private bool keydown;  //to signify that the paddle should move
        private string key;

        //Levels
        private int level;

        public World(Graphics bufferGraphics, Size playArea, Timer timer1, Label label1, Label label2, Label label3, Label title, Random random, Button button2, Button button3, int rows, int columns, int level, int lives, int score)
        {
            dead = false;
            //level = 1;
            //this.labels = labels;
            this.level = level;
            this.label1 = label1;
            this.label2 = label2;
            this.label3 = label3;
            this.lives = lives;
            this.random = random;
            this.timer1 = timer1;
            this.playArea = playArea;
            this.bufferGraphics = bufferGraphics;
            this.rows = rows;
            this.columns = columns;
            this.score = score;

            Color[] brickColour = new Color[6];
            brickColour[0] = Color.Yellow;
            brickColour[1] = Color.Orange;
            brickColour[2] = Color.Red;
            brickColour[3] = Color.Blue;
            brickColour[4] = Color.Purple;
            brickColour[5] = Color.Black;

            if (level == 2)
            {
                brickColour[0] = Color.Blue;
                brickColour[1] = Color.BlueViolet;
                brickColour[2] = Color.DarkTurquoise;
                brickColour[3] = Color.DodgerBlue;
                brickColour[4] = Color.SkyBlue;
                brickColour[5] = Color.DeepSkyBlue;
            }

            brickX = 0;
            brickY = -120;
            brickWidth = 69;
            brickHeight = 19;
            brickGap = 1;
            introCount = 0;

            ballList = new List<Ball>();
            ballList.Add(new Ball(new Point(150, 300), new Point(5, 5), Color.DimGray, bufferGraphics, playArea, 20, PADDLEWIDTH));
            //ballList.Add(new Ball(new Point(150, 300), new Point(-5, 5), Color.DimGray, bufferGraphics, playArea, 20, paddleWidth));

            activeBalls = ballList.Count;

            dropBallList = new List<DropBall>();
            //ball2 = new Ball(new Point(450, 300), new Point(-5, 5), Color.DimGray, bufferGraphics, playArea, 20, paddleWidth);

            paddle = new Paddle(new Point(300, 600), Color.PaleVioletRed, bufferGraphics, PADDLEWIDTH, 20, playArea, ballList, dropBallList, level);
            brickList = new List<Brick>();



            for (int j = 0; j < rows; j++)
            { 
                for (int i = 0; i < columns; i++)
                {
                    brickList.Add(new Brick(new Point(brickX, brickY), brickColour[j], bufferGraphics, brickWidth, brickHeight/*, ball*/, ballList, brickNum, random, playArea));
                    brickX += (brickWidth + brickGap);
                    brickNum++;
                    introCount++;
                }
                brickX = 0;
                brickY += (brickHeight + brickGap);
            }

            brickCount = brickList.Count;

            //paddleIntro = 0;
            introCount--; //takes a number off introcount as it is used again to cycle through list of bricks for intro


            //counter = 0;

            introNum = 0;

            brickPos = new Point(500, 400);

            backGroundAnimation = 0;

            //timer2.Enabled = true;
            titleColor = 0;

            //INTRO SCREEN
            this.title = title;
            this.button2 = button2;
            this.button3 = button3;
            intro = true;
        }

        public void BallOut()
        {
            activeBalls -= 1;  //keeps count of balls active, when extra balls are spawned this counter will have gone up
            if (activeBalls == 0)  //once active balls have reached zero, a life will be taken off
            {
                lives--;
                ballList[0].Reset();
                activeBalls++;
            }

             if (lives == 0)
            {
                dead = true;  //dead set to true so that start button will now reset game
                timer1.Enabled = false;
                Brush black = new SolidBrush(Color.Black);
                bufferGraphics.FillRectangle(black, 0, 0, playArea.Width, playArea.Height);
                label2.Text = "LIVES: ";
                title.Text = "YOU LOSE!";
                button2.Visible = true;
                button3.Visible = true;
                title.Visible = true;
            }

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


        public void Run()
        {
            timer1.Enabled = true;
            Background();

            //*************************COLLISION DETECTION*****************************

            foreach (Ball eachBall in ballList)
            {
                if (eachBall.BallOut1 == true && eachBall.Dead == false) //check to see if the ball has gone below the bottom of the screen
                {
                    eachBall.Dead = true;
                    BallOut();
                }

                eachBall.Bounce();
                eachBall.BrickBounce(paddle.Rectangle);

                foreach (Brick eachbrick in brickList)
                {  
                    if (eachbrick.Dead == false)  //only executes collision detection if the brick is not dead
                    {
                        eachBall.BrickBounce(eachbrick.Rectangle);
                        if (eachBall.BrickDead == true)  
                        {
                            brickCount--;
                            score += 10;
                            eachbrick.Dead = true; //kills brick so it will no longer be detected for collisions
                        }
                    }
                }

                eachBall.PaddleX = paddle.Position.X;
                eachBall.Move();
                eachBall.Draw();
            }

            paddle.Draw();

            if (brickCount == 0)
            {
                levelComplete = true;
            }

            label1.Text = "SCORE: " + (score).ToString();
            label3.Text = "LEVEL: " + (level).ToString();

            switch (lives)
            {
                case 3:
                    label2.Text = "LIVES: == == ==";
                    break;

                case 2:
                    label2.Text = "LIVES: == ==";
                    break;

                case 1:
                    label2.Text = "LIVES: ==";
                    break;

                case 0:
                    label2.Text = "LIVES: ";
                    break;
            }

            

            if (introCount > -1)
            {
                BrickIntro();
            }

            
            foreach (Brick eachBrick in brickList)
            {

                if (eachBrick.Drop == true) //checks if brick has an item drop
                {
                    SpawnDropBall(eachBrick.Position);
                    eachBrick.Drop = false;
                }

                if (eachBrick.Dead == true)
                {
                    eachBrick.Kill();
                }

                else
                {
                    if (level == 2)
                    {
                        eachBrick.Move();
                    }

                    if (level == 3)
                    {
                        eachBrick.MoveHorizontal();
                    }
                    brickPos = eachBrick.Position;
                    eachBrick.Draw();
                }
                
            }

            

            if (paddle.Drop == true)
            {
                SpawnBall();
                paddle.Drop = false;
            }
            
            paddle.Hit();

            foreach (DropBall eachDrop in dropBallList) //draws and animates brick drop for extra ball
            {
                eachDrop.Draw();
                eachDrop.Move();
            }

            if (keydown == true)
            {
                PaddleMove(key);
            }

            if (paddleIntro < 15)
            {
                paddle.Intro();
                paddleIntro++;
            }

        }

        public void SpawnBall()
        {
            SoundPlayer newBall = new SoundPlayer(Properties.Resources.newBall);
            newBall.Play();
            ballList.Add(new Ball(new Point(200, 200), new Point(5, 5), Color.DimGray, bufferGraphics, playArea, 20, PADDLEWIDTH));
            activeBalls ++;
        }

        public void SpawnDropBall(Point position)
        {
            dropBallList.Add(new DropBall(bufferGraphics, position));
        }

        public void Background()
        {
            background = (Bitmap)Properties.Resources.ResourceManager.GetObject("b" + (level).ToString()); //applies new image to background
            tbrush = new TextureBrush(background); //applies new texture brush

            bufferGraphics.FillRectangle(tbrush, 0, 0, playArea.Width, playArea.Height);
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

        public int BrickNum { get => brickNum; set => brickNum = value; }
        public bool Dead { get => dead; set => dead = value; }
        public bool LevelComplete { get => levelComplete; set => levelComplete = value; }
        public bool Keydown { get => keydown; set => keydown = value; }
        public string Key { get => key; set => key = value; }
        public int Score { get => score; set => score = value; }
        public int Lives { get => lives; set => lives = value; }
    }
}
