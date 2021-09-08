using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    public partial class Form1 : Form
    {
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

        public Form1()
        {
            random = new Random();
            InitializeComponent();
            playArea = new Size(700, 540);
            bufferImage = new Bitmap(playArea.Width, playArea.Height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            graphics = CreateGraphics();
            world = new World(bufferGraphics, playArea, timer1, textBox1, random); //clientSize automatically generated, tells the boundaries of the program
            this.KeyPreview = true;
            timer1.Enabled = true;
            score = 0;
            
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
            Application.DoEvents();//make sure all images are drawn before the program proceeds
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            key = e.KeyCode.ToString();

            switch (key)
            {
                case "Left":
                    world.PaddleMove(key);
                    keydown = true;
                    break;

                case "Right":
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

    }
}
