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
        private List<string> scoreList;

        public FormLeaderBoard()
        {
            InitializeComponent();
            scoreList = new List<string>();
        }

        //Adds sorted score stats to listBox list
        public void addToList(string score)
        {
            //sorting the listBox https://www.csharp-console-examples.com/winform/sort-listbox-items-on-descending-order-in-c/
            int count = 1;
            scoreList.Add(score.ToString());
            scoreList.Sort();
            scoreList.Reverse();

            listBoxLeaders.Items.Clear();

            foreach (object scores in scoreList)
            {
                listBoxLeaders.Items.Add(count.ToString() + "    " + scores.ToString());
                count++;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
