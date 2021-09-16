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

            //sorting the listBox https://www.csharp-console-examples.com/winform/sort-listbox-items-on-descending-order-in-c/
            List<string> scoreList = new List<string>();

            foreach (object eachScore in listBoxLeaders.Items) 
            {
                scoreList.Add(eachScore.ToString());
            }
            scoreList.Sort();
            scoreList.Reverse();
            listBoxLeaders.Items.Clear();

            foreach (object scores in scoreList)
            {
                listBoxLeaders.Items.Add(scores);
            }
            //ArrayList list = new ArrayList();

            //foreach (object o in listBox1.Items)
            //{
            //    list.Add(o);
            //}
            //list.Sort();
            //list.Reverse();
            //listBox1.Items.Clear();
            //foreach (object o in list)
            //{
            //    listBox1.Items.Add(o);
            //}

        }

        public int Test { get => test; set => test = value; }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


    }
}
