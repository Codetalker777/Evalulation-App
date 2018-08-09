using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evalulation_App
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update form = new Update(5);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Finder form = new Finder(2);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Finder form = new Finder(3);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Finder form = new Finder(4);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Update form = new Update(1);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }
    }
}
