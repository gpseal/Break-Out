/* Program name: 	    Breakout
   Project file name:   Breakout.sln
   Author:		        Greg Seal
   Date:	            16/9/2021
   Language:		    C#
   Platform:		    Microsoft Visual Studio 2019
   Purpose:		        A game of BreakOut
   Description:		    Player moves a paddle horizontally along the bottom of the screen, bouncing a moving ball towards a lines of bricks at the
                        top.  When the ball hits a brick it disappears.  The game is won when all of the bricks have been removed.
                        The game is lost when the ball passes below the bottom of the screen three time.
   Known Bugs:		    On occasion the ball can get stuck inside the paddle
   Additional Features: 3 levels
                        Bricks drop pickups that spawn extra balls
                        Animated paddle
                        User can choose number of bricks on screen
                        User can choose speed of ball
                        User can choose frequency of item drops
                        High score leaderboard
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    public partial class Form1 : Form
    {
        private string playerName;
        private Bitmap bufferImage;
        private Graphics bufferGraphics;
        private Graphics graphics;
        private World world;
        private Size playArea;
        private int score;
        private Random random;
        private int rows;
        private int columns;
        private int level;
        private int lives;
        private int ballSpeed;
        private int dropFrequency;
        private string leaderBoardScore;

        private FormLeaderBoard leaders;

        public Form1()
        {
            leaders = new FormLeaderBoard();
            random = new Random();
            InitializeComponent();
            playArea = new Size(700, 540);
            bufferImage = new Bitmap(playArea.Width, playArea.Height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            graphics = CreateGraphics();
            ballSpeed = 5;

            //options = new FormOptions();
            rows = 5;
            columns = 10;
            dropFrequency = 5;
            trackBar4.Value = dropFrequency;
            trackBar1.Value = ballSpeed;
            trackBar2.Value = rows;
            trackBar3.Value = columns;
            playerName = "Player 1";
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, rows, columns, level, lives, score, panelTitle, gameTitle, levelName, ballSpeed, dropFrequency);
            levelName.Visible = false;
            //FormLeaderBoard leaderBoard = new FormLeaderBoard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            //Options Optionsform = new Options() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            //this.panelOptions.Controls.Add(Optionsform);
            //Optionsform.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bufferGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            if (world.LevelComplete == true)
            {
                level++;
            }

            switch (level)  //changes game level
            {
                case 1:
                    world.Run();
                    break;

                case 2:
                    if (world.LevelComplete == true)
                    {
                        Level2();  //initializes new world class for level 2
                    }
                    world.Run();
                    break;

                case 3:
                    if (world.LevelComplete == true)
                    {
                        Level3();  //initializes new world class for level 2
                    }
                    world.Run();
                    break;

                case 4:
                    gameTitle.Text = "YOU WON!";
                    panelTitle.Visible = !panelTitle.Visible;
                    leaders.addToList(score.ToString() + "\t\t" + playerName.ToString());
                    break;
            }

            //sends name nd score to leaderboard
            if (world.GameOver == true)
            {
                leaders.addToList(score.ToString() + "\t\t" +playerName.ToString());
                world.GameOver = false;
            }

            score = world.Score;
            lives = world.Lives;
            graphics.DrawImage(bufferImage, 0, 0);
            //Application.DoEvents();//make sure all images are drawn before the program proceeds
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    world.Key = "Left";
                    world.Keydown = true;  //this prevents a jump to begin movement when arrow key is held down
                    break;

                case Keys.Right:
                    world.Key = "Right";
                    world.Keydown = true; //this prevents a jump to begin movement when arrow key is held down
                    break;

                case Keys.P: //pause game
                    timer1.Enabled = !timer1.Enabled;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            world.Keydown = false; //when arrow key is released, paddle will stop moving
        }

        private void button2_Click_1(object sender, EventArgs e)  //START GAME BUTTON
        {
            panelTitle.Visible = !panelTitle.Visible; //removes title screen from view
            panelScore.Visible = true;  //shows scoreboard

            level = 1;
            score = 0;
            lives = 3;
            if (!string.IsNullOrEmpty(textBox1.Text)) //https://stackoverflow.com/questions/52751474/is-there-anything-in-c-sharp-that-is-the-opposite-of-isnullorempty
            {
                playerName = textBox1.Text;
            }
            
            //rows = options.Rows;
            //columns = options.Columns;

            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, rows, columns, level, lives, score, panelTitle, gameTitle, levelName, ballSpeed, dropFrequency);

            if (world.Dead == true)
            {
                world.Dead = false;
            }

            world.Run();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panelOptions.Visible = !panelOptions.Visible;
            //this.Hide();
            //FormOptions options = new FormOptions(); /*https://stackoverflow.com/questions/3965043/how-to-open-a-new-form-from-another-form*/
            //options.ShowDialog();
        }

        private void Level2()
        {
            level = 2;
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, 3, 10, level, lives, score, panelTitle, gameTitle, levelName, ballSpeed, dropFrequency);
            world.LevelComplete = false;
        }

        private void Level3()
        {
            level = 3;
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, 6, 4, level, lives, score, panelTitle, gameTitle, levelName, ballSpeed, dropFrequency);
            world.LevelComplete = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelOptions.Visible = !panelOptions.Visible;
            playerName = textBox1.Text;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            ballSpeed = trackBar1.Value;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            columns = trackBar3.Value;
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            dropFrequency = trackBar4.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            rows = trackBar2.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            leaders.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //leaders = new FormLeaderBoard();
            //if (string.IsNullOrEmpty(playerName))
            //    return;
            //leaders.listBox2.Items.Add(/*playerName.ToString() + "   " + score.ToString()*/"Test");
            //leaders.addToList(playerName.ToString());
        }
    }
}
