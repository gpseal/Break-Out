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
        private const int BRICKWIDTH = 69;
        private const int BRICKHEIGHT = 19;
        private const int BRICKGAP = 1;

        private Graphics bufferGraphics;
        private List<Brick> brickList;
        private List<Ball> ballList;
        private List<DropBall> dropBallList;
        private int lives;
        private Paddle paddle;
        private TextureBrush tbrush;
        private Size playArea;
        private Timer timer1;

        private Random random;
        private int score;
        private int activeBalls;

        //Menus and intro
        private Panel panelTitle;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label gameTitle;
        private Label levelName;

        private bool dead;
        private bool levelComplete;

        //brick variables
        private int rows;
        private int columns;
        private int brickCount;

        //paddle key movements
        private bool keydown;  //to signify that the paddle should move
        private string key;
        
        //Levels
        private int level;
        private int levelCounter;

        //Brick & Paddle intro counters
        private int brickIntroCount;
        private int introNum;
        private int paddleIntro;  //used for timing of paddle intro


        public World(Graphics bufferGraphics, Size playArea, Timer timer1, Label label1, Label label2, Label label3, Random random, int rows, int columns, int level, int lives, int score, Panel panelTitle, Label gameTitle, Label levelName)
        {
            this.panelTitle = panelTitle;
            dead = false;
            this.level = level;
            this.label1 = label1;
            this.label2 = label2;
            this.label3 = label3;
            this.levelName = levelName;
            this.gameTitle = gameTitle;
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

            ballList = new List<Ball>();
            ballList.Add(new Ball(new Point(150, 300), new Point(5, 5), Color.DimGray, bufferGraphics, playArea, 20));
            activeBalls = ballList.Count;

            dropBallList = new List<DropBall>(); //list created to store items dropped from bricks

            paddle = new Paddle(new Point(300, 600), Color.PaleVioletRed, bufferGraphics, PADDLEWIDTH, 20, playArea, ballList, dropBallList, level);

            brickList = new List<Brick>();
            int brickX = 0;
            int brickY = -120;  //starts offscreen to allow room for intro
            brickIntroCount = 0;

            for (int j = 0; j < rows; j++)
            { 
                for (int i = 0; i < columns; i++)
                {
                    brickList.Add(new Brick(new Point(brickX, brickY), brickColour[j], bufferGraphics, BRICKWIDTH, BRICKHEIGHT, random, playArea));
                    brickX += (BRICKWIDTH + BRICKGAP);
                    brickIntroCount++;
                }
                brickX = 0;
                brickY += (BRICKHEIGHT + BRICKGAP);
            }

            brickCount = brickList.Count;
            brickIntroCount--; //takes a number off introcount as it is used again to cycle through list of bricks for intro
            introNum = 0;
            levelCounter = 0;
            levelName.Visible = true;
        }

        public void Run()
        {
            timer1.Enabled = true;
            Background();
            

            //*************************COLLISION DETECTION*****************************
            //checks if ball has entered each brick or paddle, then bounces accordingly

            foreach (Ball eachBall in ballList)
            {
                //if ball has moved below bottom of screen, will run this and decide whether to remove a life or not
                if (eachBall.BallOut1 == true && eachBall.Dead == false) 
                {
                    eachBall.Dead = true;
                    BallOut(); //local method that removes 1 from active ballcount
                }

                eachBall.Bounce();  //detects if ball has hit side or top of playing area
                eachBall.PaddleBounce(paddle.Rectangle);  //paddle collision detection

                foreach (Brick eachbrick in brickList)
                {  
                    if (eachbrick.Dead == false)  //only executes collision detection if the brick is not dead
                    {
                        eachBall.BrickBounce(eachbrick.Rectangle);  //brick collision detection
                        if (eachBall.BrickDead == true)  // will run if brick has been hit
                        {
                            brickCount--;
                            score += 10;
                            eachbrick.Dead = true; //kills brick so it will no longer be detected for collisions
                        }
                    }
                }

                eachBall.Move();
                eachBall.Draw();
            }

            paddle.Draw();

            if (brickIntroCount > -1)
            {
                BrickIntro();
            }

            foreach (Brick eachBrick in brickList)
            {
                //checks if brick has an item drop
                if (eachBrick.Drop == true)
                {
                    SpawnDropBall(eachBrick.Position); //spawn drop item from brick that has been hit
                    eachBrick.Drop = false;  //stops brick from dropping items continuously
                }

                //begins brick fade animation
                if (eachBrick.Dead == true)
                {
                    eachBrick.Kill();
                }

                //different levels have different movements for bricks
                else
                {
                    if (level == 2)
                    {
                        eachBrick.Move(); //bricks will move on level 2
                    }

                    if (level == 3)
                    {
                        eachBrick.MoveHorizontal(); //bricks will move on level 3
                    }
                    eachBrick.Draw();
                }
            }

            //moves paddle using key code from form1
            if (keydown == true)
            {
                PaddleMove(key);  
            }

            //paddle intro runs for first 15 frames of game
            if (paddleIntro < 15)
            {
                paddle.Intro();
                paddleIntro++;
            }

            //draws and animates items that are dropped from bricks
            foreach (DropBall eachDrop in dropBallList)
            {
                eachDrop.Draw();
                eachDrop.Move();
                eachDrop.Hit(paddle.Rectangle);

                if (eachDrop.Drop == true)
                {
                    SpawnBall();
                    eachDrop.Drop = false;
                }

            }

            if (brickCount == 0)
            {
                levelComplete = true;
            }

            //displays level name for 100 frames at beginning of each level
            levelName.Text = "LEVEL " + level.ToString();
            if (levelCounter >= 100)
            {
                levelName.Visible = false;
            }
            levelCounter++;

            //to display lives on scoreboard
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

            //displays score and level on scoreboard
            label1.Text = "SCORE: " + (score).ToString();
            label3.Text = "LEVEL: " + (level).ToString();

            End();//checks to see if player has lost all lives
        }

        //Runs when ball leaves bottom of screen. Keeps track of number of active balls, 
        public void BallOut()
        {
            SoundPlayer ballDeath = new SoundPlayer(Properties.Resources.playerDeath);
            activeBalls --;  //keeps count of balls active, when extra balls are spawned this counter will have gone up
            if (activeBalls == 0)  //once active balls have reached zero, a life will be taken off
            {
                ballDeath.Play();
                lives--;
                ballList[0].Reset();
                activeBalls++;
            }
        }

        //ends game when lives = 0
        public void End()
        {
            if (lives == 0)
            {
                dead = true;  //dead set to true so that start button will now reset game
                timer1.Enabled = false;
                Brush black = new SolidBrush(Color.Black);
                gameTitle.Text = "GAMEOVER";
                bufferGraphics.FillRectangle(black, 0, 0, playArea.Width, playArea.Height);
                panelTitle.Visible = !panelTitle.Visible;
            }
        }

        //moves paddle while arrow keys are being held down
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

        // Spawns ball if item drop comes into contact with paddle
        public void SpawnBall()
        {
            SoundPlayer newBall = new SoundPlayer(Properties.Resources.newBall);
            newBall.Play();
            ballList.Add(new Ball(new Point(random.Next(300), 200), new Point(5, 5), Color.DimGray, bufferGraphics, playArea, 20));
            activeBalls ++;
        }

        //Spawns item drop if dead brick meets criteria
        public void SpawnDropBall(Point position)
        {
            dropBallList.Add(new DropBall(bufferGraphics, position));
        }

        //draws background of level
        public void Background()
        {
            Image background = (Bitmap)Properties.Resources.ResourceManager.GetObject("b" + (level).ToString()); //applies new image to background
            tbrush = new TextureBrush(background); //applies new texture brush
            bufferGraphics.FillRectangle(tbrush, 0, 0, playArea.Width, playArea.Height);
        }

        //plays brick intro animations
        public void BrickIntro()
        {
            brickList[brickIntroCount].IntroAnim(); //runs intro anmimation for brick
            introNum++;

            if (introNum >= 6) //intro takes 6 frames
            {
                brickIntroCount--; //intro animation moves to next brick
                introNum = 0;  //resets frame count
            }
        }

        public bool Dead { get => dead; set => dead = value; }
        public bool LevelComplete { get => levelComplete; set => levelComplete = value; }
        public bool Keydown { get => keydown; set => keydown = value; }
        public string Key { get => key; set => key = value; }
        public int Score { get => score; set => score = value; }
        public int Lives { get => lives; set => lives = value; }
    }
}
