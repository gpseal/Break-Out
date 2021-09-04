using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    
    class World
    {
        private Graphics bufferGraphics;
        private Ball ball;
        private List<Brick> brickList;
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

        public World(Graphics bufferGraphics, Size playArea)
        {

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
            ball = new Ball(new Point(400, 300), new Point(10, 10), Color.DimGray, bufferGraphics, playArea, 20);
            paddle = new Paddle(new Point(300, 590), Color.PaleVioletRed, bufferGraphics, 100, 20, playArea, ball);
            brickList = new List<Brick>();

            for (int rows = 0; rows < 5; rows++)
            { 
                for (int i = 0; i < 10; i++)
                {
                    brickList.Add(new Brick(new Point(brickX, brickY), brickColour[rows], bufferGraphics, brickWidth, brickHeight, ball, brickNum));
                    brickX += (brickWidth + brickGap);
                    brickNum++;
                    introCount++;
                }
                brickX = 0;
                brickY += (brickHeight + brickGap);
            }

            introCount--;
            background = (Bitmap)Properties.Resources.ResourceManager.GetObject("city");
            tbrush = new TextureBrush(background);
            

            introNum = 0;
        }



        public void BrickIntro()
        {
            brickList[introCount].IntroAnim();
            introNum++;

            if (introNum >= 3)
            {
                introCount--;
                introNum = 0;
            }
        }

        public void Run()
        {
            Draw();
            if (introCount > -1)
            {
                BrickIntro();
            }

            brickNum = 0;
            foreach (Brick eachBrick in brickList)
            {
                //eachBrick.Intro();
                eachBrick.Draw();
                eachBrick.Hit();
                if (brickList[brickNum].Hit1 == true)
                { 
                    /*brickList.RemoveAt(brickNum);*/
                }
                brickNum++;
            }

            paddle.Hit();

            if (introCount > 43)
            {
                paddle.Intro();
            }
            paddle.Draw();

            ball.Bounce();
            ball.Move();
            ball.Draw();
        }

        public void Draw()
        {
            bufferGraphics.FillRectangle(tbrush, 0, 0, playArea.Width, playArea.Height);
        }
        public int BrickNum { get => brickNum; set => brickNum = value; }



    }
}
