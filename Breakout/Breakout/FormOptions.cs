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
    public partial class FormOptions : Form
    {
        public int rows;
        public int columns;
        public FormOptions()
        {
            InitializeComponent();
            //rows = 1;
            //columns = 1;

            trackBar2.Value = 2;
            trackBar3.Value = 10;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }



        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            columns = trackBar3.Value;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            rows = trackBar2.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            this.Hide();
        }

        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }

    }
}
