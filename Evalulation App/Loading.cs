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
    public partial class Loading : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;
        public Loading()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form2 = new Form1();
            Hide();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT Name FROM Firm", connect);
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT * FROM Firm WHERE @name = Name", connect);
            cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();

            if (dt.Rows.Count == 0)
            {
                label2.Visible = true;
                return;
            }
            else
            {
                label2.Visible = false;
            }

            Chart form2 = new Chart(comboBox1.Text);
            Hide();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog();
        }
    }
}
