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
    public partial class Update : Form
    {
        int id;

        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;

        double beta, growth;
        string firm;

        double market_id, market, riskfree;

        double rf, g;
        public Update(int a)
        {
            InitializeComponent();
            id = a;
        }

        public Update(int a, string s)
        {
            InitializeComponent();
            id = a;
            firm = s;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (id == 1 || id == 5)
            {
                Options form2 = new Options();
                Hide();
                form2.Closed += (s, args) => this.Close();
                form2.ShowDialog();
            }
            else
            {
                Finder form2 = new Finder(id);
                Hide();
                form2.Closed += (s, args) => this.Close();
                form2.ShowDialog();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ycharts.com/indicators/sandp_500_total_return_annual");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form2 = new Form1();
            Hide();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            if (id == 1)
            {
                label1.Text = "Industry";
                label2.Text = "Beta";
                label3.Text = "Growth Rate";
                label10.Visible = true;
                button2.Text = "Add";
            }
            else if(id == 3)
            {
                label1.Text = "Beta";
                label2.Text = "Growth Rate";
                label9.Visible = true;
                label3.Visible = false;
                textBox3.Visible = false;
                button2.Text = "Update";

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM Industry WHERE Name = @name", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                textBox1.Text = dt.Rows[0][1].ToString();
                g = Double.Parse(dt.Rows[0][2].ToString());
                g = g * 100;
                textBox2.Text = g.ToString();
            }
            else if (id == 5)
            {
                label1.Text = "Market Rate";
                label8.Visible = true;
                label9.Visible = true;
                label2.Text = "Risk Free Rate";
                label3.Visible = false;
                textBox3.Visible = false;
                button2.Text = "Update";
                linkLabel1.Visible = true;
                label7.Visible = true;

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM Market", connect);
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                market_id = Double.Parse(dt.Rows[0][0].ToString());
                rf = Double.Parse(dt.Rows[0][1].ToString());
                market_id = market_id * 100;
                textBox1.Text = market_id.ToString();
                market_id = market_id / 100;
                rf = rf * 100;
                textBox2.Text = rf.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == 1)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM Industry WHERE Name = @name", connect);
                cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();
                if (dt.Rows.Count != 0)
                {
                    label4.Visible = true;
                    return;
                }
                if (textBox1.Text == "")
                {
                    label4.Visible = true;
                    return;
                }
                else
                {
                    label4.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out beta)) {
                    label5.Visible = true;
                    return;
                }
                else
                {
                    label5.Visible = false;
                }
                if (!double.TryParse(textBox3.Text, out growth))
                {
                    label6.Visible = true;
                    return;
                }
                else
                {
                    label6.Visible = false;
                }
                growth = growth / 100;
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO Industry (Name, Beta, GrowthRate) VALUES (@name, @beta, @g)", connect);
                cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@beta", beta);
                cmd.Parameters.AddWithValue("@g", growth);
                cmd.ExecuteNonQuery();
            }
            else if (id == 3)
            {
                if (!double.TryParse(textBox1.Text, out beta))
                {
                    label4.Visible = true;
                    return;
                }
                else
                {
                    label4.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out growth))
                {
                    label5.Visible = true;
                    return;
                }
                else
                {
                    label5.Visible = false;
                }
                growth = growth / 100;
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"UPDATE Industry SET Beta = @beta, GrowthRate = @g WHERE Name = @name", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@beta", beta);
                cmd.Parameters.AddWithValue("@g", growth);
                cmd.ExecuteNonQuery();
            }
            else if (id == 5)
            {
                if (!double.TryParse(textBox1.Text, out market))
                {
                    label4.Visible = true;
                    return;
                }
                else
                {
                    label4.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out riskfree))
                {
                    label5.Visible = true;
                    return;
                }
                else
                {
                    label5.Visible = false;
                }
                int years;
                DateTime localdate = DateTime.Now;
                years = localdate.Year;

                market = market / 100;
                riskfree = riskfree / 100;
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"UPDATE Market SET MarketRate = @market, Riskfree = @risk, Date = @date WHERE MarketRate = @m", connect);
                cmd.Parameters.AddWithValue("@market", market);
                cmd.Parameters.AddWithValue("@risk", riskfree);
                cmd.Parameters.AddWithValue("@date", years);
                cmd.Parameters.AddWithValue("@m", market_id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
