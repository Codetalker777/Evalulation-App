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
    public partial class Finder : Form
    {
        int id;
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;
        public Finder(int a)
        {
            id = a;
            InitializeComponent();
        }

        private void Finder_Load(object sender, EventArgs e)
        {
            if (id == 2 || id == 3)
            {
                comboBox1.Text = "Select Industry";
                if (id == 2)
                {
                    button1.Text = "Delete";
                }
                else
                {
                    button1.Text = "Next";
                }

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
            else if (id == 4)
            {
                button1.Text = "Delete";
                comboBox1.Text = "Select Firm";
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Options form2 = new Options();
            Hide();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == 2)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM Industry WHERE Name = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                comboBox1.Items.Remove(comboBox1.Text);
            }
            else if (id == 3)
            {
                for (int i = 0; i<comboBox1.Items.Count; i++)
                {
                    if (comboBox1.Text ==  comboBox1.Items[i].ToString())
                    {
                        Update form = new Update(3, comboBox1.Text.Trim());
                        Hide();
                        form.Closed += (s, args) => this.Close();
                        form.ShowDialog();
                    }
                }
                label4.Visible = true;
                return;

            }

            else if (id == 4)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM Firm WHERE Name = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM GrowthFirm WHERE Name = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM FirmCustom WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM FirmHigh WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM FirmLow WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"DELETE FROM FirmNormal WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", comboBox1.Text.Trim());
                cmd.ExecuteNonQuery();

                comboBox1.Items.Remove(comboBox1.Text);
            }
        }
    }
}
