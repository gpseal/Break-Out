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
        private Size playArea;

        public Form1()
        {
            InitializeComponent();
            playArea = new Size(700, 540);
            bufferImage = new Bitmap(playArea.Width, playArea.Height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            graphics = CreateGraphics();
            world = new World(bufferGraphics, playArea); //clientSize automatically generated, tells the boundaries of the program

            timer1.Enabled = true;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            
            
            bufferGraphics.FillRectangle(Brushes.MidnightBlue, 0, 0, Width, Height);
            world.Run();


            graphics.DrawImage(bufferImage, 0, 0);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
