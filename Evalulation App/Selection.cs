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
    public partial class Selection : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;
        public int a = 1;
        public double beta, market, riskfree, discount_rate, growth;
        string industry;

        private void Selection_Load(object sender, EventArgs e)
        {
            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT * FROM Market", connect);
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();

            market = double.Parse(dt.Rows[0][0].ToString());
            riskfree = double.Parse(dt.Rows[0][1].ToString());

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT Name FROM Industry", connect);
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (textBox1.Text == "Enter Firm Name" || textBox1.Text == "")
            {
                label1.Visible = true;
                return;
            }
            else
            {
                label1.Visible = false;
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM Industry WHERE @name = Name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                industry = dt.Rows[0][0].ToString();
                beta = double.Parse(dt.Rows[0][1].ToString());
                growth = double.Parse(dt.Rows[0][2].ToString());
                label2.Visible = false;
            }
        }

        public Selection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Firm Name" || textBox1.Text == "")
            {
                label1.Visible = true;
                return;
            }
            else
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM Firm WHERE @name = Name", connect);
                cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                if (dt.Rows.Count != 0)
                {
                    label3.Visible = true;
                    return;
                }
                else
                {
                    label3.Visible = false;
                }
            }

                if (beta != 0)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO Firm (Name, Industry) VALUES (@name, @industry)", connect);
                cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@industry", industry.Trim());
                cmd.ExecuteNonQuery();

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO GrowthFirm (Name, Growth) VALUES (@name, @g)", connect);
                cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@g", growth);
                cmd.ExecuteNonQuery();

                CashFlow form = new CashFlow(beta, market, riskfree, growth, textBox1.Text);
                Hide();
                form.Closed += (s, args) => this.Close();
                form.ShowDialog();
            }
            else
            {
                label2.Visible = true;
                return;
            }
        }
    }
}
