using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Evalulation_App
{
    public partial class Form1 : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;

        int check;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Selection form = new Selection();
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Options form2 = new Options();
            Hide();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT Date FROM Market", connect);
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();

            check = int.Parse(dt.Rows[0][0].ToString());

            int years;
            DateTime localdate = DateTime.Now;
            years = localdate.Year;

            if (check < years)
            {
                label2.Visible = true;
                label2.Text = "Please Update Market information " + "(" + check.ToString() + ")";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Loading form2 = new Loading();
            Hide();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog();
        }
    }
}
