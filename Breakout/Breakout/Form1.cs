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
        //private SoundPlayer sound;

        private Bitmap bufferImage;
        private Graphics bufferGraphics;
        private Graphics graphics; //will have a graphics object
        private World world;
        private World world2;
        private World world3;
        private bool keydown;
        private string key;
        private string keyPress;
        private Size playArea;
        private int score;
        private Random random;
        private List<Label> labels;
        private int rows;
        private int columns;
        private int level;
        private int lives;
        private SoundPlayer paddleMove;


        private FormOptions options;

        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }

        public Form1()
        {
            //labels = new List<Label>();
            //labels.Add(label1);
            //labels.Add(label2);
            //labels.Add(label3);
            //labels.Add(label4);
            //labels.Add(label5);
            //labels.Add(label6);
            //labels.Add(label8);
            rows = 2;
            columns = 10;

            //sound = new SoundPlayer(Properties.Resources.move);
            random = new Random();
            InitializeComponent();
            playArea = new Size(700, 540);
            bufferImage = new Bitmap(playArea.Width, playArea.Height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            graphics = CreateGraphics();

            options = new FormOptions();

            //world = new World(bufferGraphics, playArea, timer1, timer2, label1, label2, label3, Title, random, button2, button3, rows, columns, level); //clientSize automatically generated, tells the boundaries of the program
            //world2 = new World(bufferGraphics, playArea, timer1, timer2, label1, label2, label3, Title, random, button2, button3, 2, 6, 2);
            //world3 = new World(bufferGraphics, playArea, timer1, timer2, label1, label2, label3, Title, random, button2, button3, 6, 4, 3); //clientSize automatically generated, tells the boundaries of the program

            //clientSize automatically generated, tells the boundaries of the program

            this.KeyPreview = true;
            timer2.Enabled = true;
            //timer1.Enabled = true;
            score = 0;
            level = 1;
            lives = 3;

            //Options Optionsform = new Options() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
            //this.panelOptions.Controls.Add(Optionsform);
            //Optionsform.Show();

        }

        ////private void timer2_Tick(object sender, EventArgs e)
        ////{
        ////    bufferGraphics.FillRectangle(Brushes.MidnightBlue, 0, 0, Width, Height);
        ////    //world.Intro();
        ////}

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (world.LevelComplete == true)
            {
                level++;
                //world.LevelComplete = false;
            }


            bufferGraphics.FillRectangle(Brushes.MidnightBlue, 0, 0, Width, Height);

            switch (level)
            {
                case 1:
                    //if (keydown == true)
                    //{
                    //    world.PaddleMove(key);
                    //}
                    world.Run();

                    break;

                case 2:
                    if (world.LevelComplete == true)
                    {
                        Level2();
                    }
                    world.Run();
                    //world2.Run();
                    //if (keydown == true)
                    //{
                    //    world2.PaddleMove(key);
                    //}
                    break;

                case 3:
                    if (world.LevelComplete == true)
                    {
                        Level3();
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

            key = e.KeyCode.ToString();

            switch (key)
            {
                case "Left":

                    world.Key = key;
                    world.Keydown = true;
                    //world2.Key = key;
                    //world2.Keydown = true;
                    //world3.Key = key;
                    //world3.Keydown = true;
                    break;

                case "Right":
                    world.Key = key;
                    world.Keydown = true;
                    //world2.Key = key;
                    //world2.Keydown = true;
                    //world3.Key = key;
                    //world3.Keydown = true;
                    break;

                case "P":
                    timer1.Enabled = !timer1.Enabled;
                    //world.Pause();
                    //world2.Pause();
                    //world3.Pause();
                    break;

                case "S":
                    //world.SpawnDropBall(brickPos);
                    break;

            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            world.Keydown = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //sound.Play();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            

            Title.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            timer2.Enabled = false;

            pictureBox1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            level = 1;
            score = 0;
            lives = 3;

            rows = options.Rows;
            columns = options.Columns;
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, Title, random, button2, button3, rows, columns, level, lives, score);

            if (world.Dead == true)
            {
                
                world.Dead = false;
            }

            world.Run();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //label7.Visible = true;

            //this.Hide();

            //FormOptions options = new FormOptions();
            //options.ShowDialog();

            //options.ShowDialog();
            //Title.Visible = false;
            //button2.Visible = false;
            //button3.Visible = false;
            //timer2.Enabled = false;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //panel1.Visible = !panel1.Visible;
            //this.Hide();

            FormOptions options = new FormOptions(); /*https://stackoverflow.com/questions/3965043/how-to-open-a-new-form-from-another-form*/
            options.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //panel1.Visible = !panel1.Visible;
        }



        private void Level2()
        {
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, Title, random, button2, button3, 3, 10, 2, lives, score);
            world.LevelComplete = false;
        }

        private void Level3()
        {
            world = new World(bufferGraphics, playArea, timer1, label1, label2, label3, Title, random, button2, button3, 6, 4, 3, lives, score);
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

    }
}
