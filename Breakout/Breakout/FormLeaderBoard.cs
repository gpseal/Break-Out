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
    public partial class FormLeaderBoard : Form
    {
        private ListBox listbox1;
        private int test;

        public FormLeaderBoard()
        {
            InitializeComponent();

            //rows = 1;
            //columns = 1;
        }


        public void addToList(string score)
        {
            listBoxLeaders.Items.Add(score);
        }

        public ListBox Listbox1 { get => listbox1; set => listbox1 = value; }
        public ListBox Listbox11 { get => listbox1; set => listbox1 = value; }
        public int Test { get => test; set => test = value; }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


    }
}
