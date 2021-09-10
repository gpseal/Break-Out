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
        private bool keydown;
        private string key;
        private string keyPress;
        private Size playArea;
        private int score;
        private Random random;
        private List<Label> labels;

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


            //sound = new SoundPlayer(Properties.Resources.move);
            random = new Random();
            InitializeComponent();
            playArea = new Size(700, 540);
            bufferImage = new Bitmap(playArea.Width, playArea.Height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            graphics = CreateGraphics();
            world = new World(bufferGraphics, playArea, timer1, timer2, label1, label2, label3, Title, random, button2, button3) ; //clientSize automatically generated, tells the boundaries of the program
            this.KeyPreview = true;
            timer2.Enabled = true;
            //timer1.Enabled = true;
            score = 0;
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bufferGraphics.FillRectangle(Brushes.MidnightBlue, 0, 0, Width, Height);
            world.Intro();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            bufferGraphics.FillRectangle(Brushes.MidnightBlue, 0, 0, Width, Height);
            world.Run();

            if (keydown == true) //if key has been pressed
            {
                world.PaddleMove(key);
            }

            graphics.DrawImage(bufferImage, 0, 0);
            //Application.DoEvents();//make sure all images are drawn before the program proceeds
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            SoundPlayer rocket = new SoundPlayer(Properties.Resources.rocket2);
            key = e.KeyCode.ToString();

            switch (key)
            {
                case "Left":
                    
                    //rocket.Play();
                    world.PaddleMove(key);
                    keydown = true;
                    break;

                case "Right":
                    //rocket.Play();
                    world.PaddleMove(key);
                    keydown = true;
                    break;

                case "Space":
                    world.Pause();
                    break;

                case "S":
                    //world.SpawnDropBall(brickPos);
                    break;

            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keydown = false;
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

            if (world.Dead == true)
            {
                world.Reset();
                world.Dead = false;
            }

            world.Run();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label7.Visible = true;
            Title.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            timer2.Enabled = false;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            label7.Visible = true;
            button4.Visible = true;
            Title.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            timer2.Enabled = false;
        }
    }
}
