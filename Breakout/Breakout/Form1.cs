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

        private FormOptions options;

        public Form1()
        {
            rows = 2;
            columns = 10;
            random = new Random();
            InitializeComponent();
            playArea = new Size(700, 540);
            bufferImage = new Bitmap(playArea.Width, playArea.Height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            graphics = CreateGraphics();

            options = new FormOptions();

            //Options Optionsform = new Options() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            //this.panelOptions.Controls.Add(Optionsform);
            //Optionsform.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bufferGraphics.FillRectangle(Brushes.MidnightBlue, 0, 0, Width, Height);

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

            rows = options.Rows;
            columns = options.Columns;

            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, rows, columns, level, lives, score, panelTitle);

            if (world.Dead == true)
            {
                world.Dead = false;
            }

            world.Run();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //panel1.Visible = !panel1.Visible;
            //this.Hide();
            FormOptions options = new FormOptions(); /*https://stackoverflow.com/questions/3965043/how-to-open-a-new-form-from-another-form*/
            options.ShowDialog();
        }

        private void Level2()
        {
            level = 2;
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, 3, 10, level, lives, score, panelTitle);
            world.LevelComplete = false;
        }

        private void Level3()
        {
            level = 3;
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, random, 6, 4, level, lives, score, panelTitle);
            world.LevelComplete = false;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            //rows = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            //columns = trackBar3.Value;
        }

        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }

    }
}
