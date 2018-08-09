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
    public partial class AllData : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;
        string firm;

        string lg, g, hg, cg;
        double[] cash = new double[20];
        double value;

        public double low_growth, growth, high_growth, custom_growth;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == lg)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM FirmLow WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();
                value = Double.Parse(dt.Rows[0][1].ToString());
                value = System.Math.Round(value, 2);
                label42.Text = value.ToString();

                int count = 0;
                for (int i =2; i<22; i++)
                {
                    cash[count] = Double.Parse(dt.Rows[0][i].ToString());
                    cash[count] = System.Math.Round(cash[count], 2);
                    count++;
                }
                label31.Text = cash[0].ToString();
                label30.Text = cash[1].ToString();
                label29.Text = cash[2].ToString();
                label28.Text = cash[3].ToString();
                label27.Text = cash[4].ToString();
                label26.Text = cash[5].ToString();
                label25.Text = cash[6].ToString();
                label24.Text = cash[7].ToString();
                label23.Text = cash[8].ToString();
                label22.Text = cash[9].ToString();

                label41.Text = cash[10].ToString();
                label40.Text = cash[11].ToString();
                label39.Text = cash[12].ToString();
                label38.Text = cash[13].ToString();
                label37.Text = cash[14].ToString();
                label36.Text = cash[15].ToString();
                label35.Text = cash[16].ToString();
                label34.Text = cash[17].ToString();
                label33.Text = cash[18].ToString();
                label32.Text = cash[19].ToString();
            }
            else if (comboBox1.Text == g)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM FirmNormal WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();
                value = Double.Parse(dt.Rows[0][1].ToString());
                value = System.Math.Round(value, 2);
                label42.Text = value.ToString();

                int count = 0;
                for (int i = 2; i < 22; i++)
                {
                    cash[count] = Double.Parse(dt.Rows[0][i].ToString());
                    cash[count] = System.Math.Round(cash[count], 2);
                    count++;
                }

                label31.Text = cash[0].ToString();
                label30.Text = cash[1].ToString();
                label29.Text = cash[2].ToString();
                label28.Text = cash[3].ToString();
                label27.Text = cash[4].ToString();
                label26.Text = cash[5].ToString();
                label25.Text = cash[6].ToString();
                label24.Text = cash[7].ToString();
                label23.Text = cash[8].ToString();
                label22.Text = cash[9].ToString();

                label41.Text = cash[10].ToString();
                label40.Text = cash[11].ToString();
                label39.Text = cash[12].ToString();
                label38.Text = cash[13].ToString();
                label37.Text = cash[14].ToString();
                label36.Text = cash[15].ToString();
                label35.Text = cash[16].ToString();
                label34.Text = cash[17].ToString();
                label33.Text = cash[18].ToString();
                label32.Text = cash[19].ToString();
            }
            else if (comboBox1.Text == hg)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM FirmHigh WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();
                value = Double.Parse(dt.Rows[0][1].ToString());
                value = System.Math.Round(value, 2);
                label42.Text = value.ToString();

                int count = 0;
                for (int i = 2; i < 22; i++)
                {
                    cash[count] = Double.Parse(dt.Rows[0][i].ToString());
                    cash[count] = System.Math.Round(cash[count], 2);
                    count++;
                }

                label31.Text = cash[0].ToString();
                label30.Text = cash[1].ToString();
                label29.Text = cash[2].ToString();
                label28.Text = cash[3].ToString();
                label27.Text = cash[4].ToString();
                label26.Text = cash[5].ToString();
                label25.Text = cash[6].ToString();
                label24.Text = cash[7].ToString();
                label23.Text = cash[8].ToString();
                label22.Text = cash[9].ToString();

                label41.Text = cash[10].ToString();
                label40.Text = cash[11].ToString();
                label39.Text = cash[12].ToString();
                label38.Text = cash[13].ToString();
                label37.Text = cash[14].ToString();
                label36.Text = cash[15].ToString();
                label35.Text = cash[16].ToString();
                label34.Text = cash[17].ToString();
                label33.Text = cash[18].ToString();
                label32.Text = cash[19].ToString();
            }
            else if (comboBox1.Text == cg)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM FirmCustom WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();
                value = Double.Parse(dt.Rows[0][1].ToString());
                value = System.Math.Round(value, 2);
                label42.Text = value.ToString();

                int count = 0;
                for (int i = 2; i < 22; i++)
                {
                    cash[count] = Double.Parse(dt.Rows[0][i].ToString());
                    cash[count] = System.Math.Round(cash[count], 2);
                    count++;
                }

                label31.Text = cash[0].ToString();
                label30.Text = cash[1].ToString();
                label29.Text = cash[2].ToString();
                label28.Text = cash[3].ToString();
                label27.Text = cash[4].ToString();
                label26.Text = cash[5].ToString();
                label25.Text = cash[6].ToString();
                label24.Text = cash[7].ToString();
                label23.Text = cash[8].ToString();
                label22.Text = cash[9].ToString();

                label41.Text = cash[10].ToString();
                label40.Text = cash[11].ToString();
                label39.Text = cash[12].ToString();
                label38.Text = cash[13].ToString();
                label37.Text = cash[14].ToString();
                label36.Text = cash[15].ToString();
                label35.Text = cash[16].ToString();
                label34.Text = cash[17].ToString();
                label33.Text = cash[18].ToString();
                label32.Text = cash[19].ToString();
            }
        }

        public AllData(string s)
        {
            InitializeComponent();
            firm = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chart form = new Chart(firm);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void AllData_Load(object sender, EventArgs e)
        {
            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT Growth FROM GrowthFirm WHERE @name = Name", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();

            growth = double.Parse(dt.Rows[0][0].ToString());
            high_growth = growth * 1.1;
            low_growth = growth * 0.9;

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT GrowthRate FROM FirmCustom WHERE @name = Firm", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();

            custom_growth = double.Parse(dt.Rows[0][0].ToString());

            lg = (low_growth * 100).ToString() + "%";
            g = (growth * 100).ToString() + "%";
            hg = (high_growth * 100).ToString() + "%";
            cg = (custom_growth * 100).ToString() + "%";

            comboBox1.Items.Add(lg);
            comboBox1.Items.Add(g);
            comboBox1.Items.Add(hg);
            comboBox1.Items.Add(cg);
        }
    }
}
