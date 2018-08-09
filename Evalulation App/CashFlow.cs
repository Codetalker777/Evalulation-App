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
    public partial class CashFlow : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;
        bool check = true;

        public double beta, market, riskfree, growth;
        public double[] cash = new double[20];
        public double[] pv_low = new double[20];
        public double[] pv_normal = new double[20];
        public double low_horizon, low_value, low_growth;
        public double normal_horizon, normal_value;
        public double[] pv_high = new double[20];
        string firm;

        private void button2_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out cash[0]))
            {
                label9.Visible = true;
            }
            else
            {
                label9.Visible = false;
            }
            if (!double.TryParse(textBox2.Text, out cash[1]))
            {
                label10.Visible = true;
            }
            else
            {
                label10.Visible = false;
            }
            if (!double.TryParse(textBox3.Text, out cash[2]))
            {
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
            }
            if (!double.TryParse(textBox4.Text, out cash[3]))
            {
                label12.Visible = true;
            }
            else
            {
                label12.Visible = false;
            }
            if (!double.TryParse(textBox5.Text, out cash[4]))
            {
                label13.Visible = true;
            }
            else
            {
                label13.Visible = false;
            }
            if (!double.TryParse(textBox6.Text, out custom_growth))
            {
                label14.Visible = true;
            }
            else
            {
                custom_growth = custom_growth / 100;
                label14.Visible = false;
            }

            if (label9.Visible == true || label10.Visible == true || label11.Visible == true || label12.Visible == true || label13.Visible == true || label14.Visible == true)
            {
                label8.Visible = true;
                return;
            }
            else
            {
                discount_rate = riskfree + beta * (market - riskfree);

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT * FROM FirmNormal WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                if (dt.Rows.Count != 0)
                {
                    check = false;
                }

                if (check == true)
                {
                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"INSERT INTO FirmNormal (Firm, Year1, Year2, Year3, Year4, Year5) VALUES (@name, @yr1, @yr2, @yr3, @yr4, @yr5)", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.ExecuteNonQuery();

                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"INSERT INTO FirmLow (Firm, Year1, Year2, Year3, Year4, Year5) VALUES (@name, @yr1, @yr2, @yr3, @yr4, @yr5)", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.ExecuteNonQuery();

                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"INSERT INTO FirmHigh (Firm, Year1, Year2, Year3, Year4, Year5) VALUES (@name, @yr1, @yr2, @yr3, @yr4, @yr5)", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.ExecuteNonQuery();

                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"INSERT INTO FirmCustom (Firm, Year1, Year2, Year3, Year4, Year5, GrowthRate) VALUES (@name, @yr1, @yr2, @yr3, @yr4, @yr5, @growth)", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.Parameters.AddWithValue("@growth", custom_growth);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"UPDATE FirmNormal SET Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5 WHERE Firm = @name", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.ExecuteNonQuery();

                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"UPDATE FirmLow SET Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5 WHERE Firm = @name", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.ExecuteNonQuery();

                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"UPDATE FirmHigh SET Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5 WHERE Firm = @name", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.ExecuteNonQuery();

                    connect = new SqlCeConnection(database);
                    connect.Open();
                    cmd = new SqlCeCommand(@"UPDATE FirmCustom SET Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, GrowthRate = @growth WHERE Firm = @name", connect);
                    cmd.Parameters.AddWithValue("@name", firm.Trim());
                    cmd.Parameters.AddWithValue("@yr1", cash[0]);
                    cmd.Parameters.AddWithValue("@yr2", cash[1]);
                    cmd.Parameters.AddWithValue("@yr3", cash[2]);
                    cmd.Parameters.AddWithValue("@yr4", cash[3]);
                    cmd.Parameters.AddWithValue("@yr5", cash[4]);
                    cmd.Parameters.AddWithValue("@growth", custom_growth);
                    cmd.ExecuteNonQuery();

                }
                Advanced form = new Advanced(cash, growth, custom_growth, discount_rate, this, firm);
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void CashFlow_Load(object sender, EventArgs e)
        {
            int years;
            DateTime localdate = DateTime.Now;
            years = localdate.Year;
            label1.Text = (years + 1).ToString();
            label2.Text = (years + 2).ToString();
            label3.Text = (years + 3).ToString();
            label4.Text = (years + 4).ToString();
            label5.Text = (years + 5).ToString();

        }

        public double high_horizon, high_value, high_growth;
        public double[] pv_custom = new double[20];
        public double custom_growth, discount_rate, custom_value, custom_horizon;
        private void button1_Click(object sender, EventArgs e)
        {
            if(!double.TryParse(textBox1.Text, out cash[0]))
            {
                label9.Visible = true;   
            }
            else
            {
                label9.Visible = false;
            }
            if (!double.TryParse(textBox2.Text, out cash[1]))
            {
                label10.Visible = true;    
            }
            else
            {
                label10.Visible = false;
            }
            if (!double.TryParse(textBox3.Text, out cash[2]))
            {
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
            }
            if (!double.TryParse(textBox4.Text, out cash[3]))
            {
                label12.Visible = true;
            }
            else
            {
                label12.Visible = false;
            }
            if (!double.TryParse(textBox5.Text, out cash[4]))
            {
                label13.Visible = true;
            }
            else
            {
                label13.Visible = false;
            }
            if (!double.TryParse(textBox6.Text, out custom_growth))
            {
                label14.Visible = true;
            }
            else
            {
                custom_growth = custom_growth / 100;
                label14.Visible = false;
            }
            if (label9.Visible == true || label10.Visible == true || label11.Visible == true || label12.Visible == true || label13.Visible == true || label14.Visible == true)
            {
                return;
            }
            discount_rate = riskfree + beta * (market - riskfree);
            custom_value = normal_value = low_value = high_value = 0;

            for (int i = 0; i < 20; i++)
            {
                if (i >= 5)
                {
                    cash[i] = cash[i - 1] * (1 + growth);
                }
                pv_normal[i] = cash[i] / Math.Pow(1 + discount_rate, i + 1);
                normal_value += pv_normal[i];
            }
            normal_horizon = ((cash[19] * (1 + growth)) / (discount_rate - growth)) / Math.Pow(1 + discount_rate, 20);
            normal_value += normal_horizon;

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"SELECT * FROM FirmNormal WHERE @name = Firm", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            da = new SqlCeDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();

            if (dt.Rows.Count != 0)
            {
                check = false;
            }

            if (check == true)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO FirmNormal (Firm, Value, Year1, Year2, Year3, Year4, Year5, Year6, Year7, Year8, Year9, Year10, Year11, Year12, Year13, Year14, Year15, Year16, Year17, Year18, Year19, Year20) VALUES (@name, @val, @yr1, @yr2, @yr3, @yr4, @yr5, @yr6, @yr7, @yr8, @yr9, @yr10, @yr11, @yr12, @yr13, @yr14, @yr15, @yr16, @yr17, @yr18, @yr19, @yr20)", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", normal_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.ExecuteNonQuery();
            }
            else
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"UPDATE FirmNormal SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20 WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", normal_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.ExecuteNonQuery();
            }

            low_growth = growth * 0.9;

            for (int i = 0; i < 20; i++)
            {
                if (i >= 5)
                {
                    cash[i] = cash[i - 1] * (1 + low_growth);
                }
                pv_low[i] = cash[i] / Math.Pow(1 + discount_rate, i + 1);
                low_value += pv_low[i];
            }
            low_horizon = ((cash[19] * (1 + low_growth)) / (discount_rate - low_growth)) / Math.Pow(1 + discount_rate, 20);
            low_value += low_horizon;

            if (check == true)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO FirmLow (Firm, Value, Year1, Year2, Year3, Year4, Year5, Year6, Year7, Year8, Year9, Year10, Year11, Year12, Year13, Year14, Year15, Year16, Year17, Year18, Year19, Year20) VALUES (@name, @val, @yr1, @yr2, @yr3, @yr4, @yr5, @yr6, @yr7, @yr8, @yr9, @yr10, @yr11, @yr12, @yr13, @yr14, @yr15, @yr16, @yr17, @yr18, @yr19, @yr20)", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", low_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.ExecuteNonQuery();
            }
            else
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"UPDATE FirmLow SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20 WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", low_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.ExecuteNonQuery();
            }

            high_growth = growth * 1.1;

            for (int i = 0; i < 20; i++)
            {
                if (i >= 5)
                {
                    cash[i] = cash[i - 1] * (1 + high_growth);
                }
                pv_high[i] = cash[i] / Math.Pow(1 + discount_rate, i + 1);
                high_value += pv_high[i];
            }
            high_horizon = ((cash[19] * (1 + high_growth)) / (discount_rate - high_growth)) / Math.Pow(1 + discount_rate, 20);
            high_value += high_horizon;

            if (check == true)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO FirmHigh (Firm, Value, Year1, Year2, Year3, Year4, Year5, Year6, Year7, Year8, Year9, Year10, Year11, Year12, Year13, Year14, Year15, Year16, Year17, Year18, Year19, Year20) VALUES (@name, @val, @yr1, @yr2, @yr3, @yr4, @yr5, @yr6, @yr7, @yr8, @yr9, @yr10, @yr11, @yr12, @yr13, @yr14, @yr15, @yr16, @yr17, @yr18, @yr19, @yr20)", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", high_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.ExecuteNonQuery();
            }
            else
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"UPDATE FirmHigh SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20 WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", high_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.ExecuteNonQuery();
            }

            for (int i = 0; i < 20; i++)
            {
                if (i >= 5)
                {
                    cash[i] = cash[i - 1] * (1 + custom_growth);
                }
                pv_custom[i] = cash[i] / Math.Pow(1 + discount_rate, i + 1);
                custom_value += pv_custom[i];
            }
            custom_horizon = ((cash[19] * (1 + custom_growth)) / (discount_rate - custom_growth)) / Math.Pow(1 + discount_rate, 20);
            custom_value += custom_horizon;

            if (check == true)
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"INSERT INTO FirmCustom (Firm, Value, Year1, Year2, Year3, Year4, Year5, Year6, Year7, Year8, Year9, Year10, Year11, Year12, Year13, Year14, Year15, Year16, Year17, Year18, Year19, Year20, GrowthRate) VALUES (@name, @val, @yr1, @yr2, @yr3, @yr4, @yr5, @yr6, @yr7, @yr8, @yr9, @yr10, @yr11, @yr12, @yr13, @yr14, @yr15, @yr16, @yr17, @yr18, @yr19, @yr20, @growth)", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", custom_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.Parameters.AddWithValue("@growth", custom_growth);
                cmd.ExecuteNonQuery();
            }
            else
            {
                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"UPDATE FirmCustom SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20, GrowthRate = @growth WHERE Firm = @name", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                cmd.Parameters.AddWithValue("@val", custom_value);
                cmd.Parameters.AddWithValue("@yr1", cash[0]);
                cmd.Parameters.AddWithValue("@yr2", cash[1]);
                cmd.Parameters.AddWithValue("@yr3", cash[2]);
                cmd.Parameters.AddWithValue("@yr4", cash[3]);
                cmd.Parameters.AddWithValue("@yr5", cash[4]);
                cmd.Parameters.AddWithValue("@yr6", cash[5]);
                cmd.Parameters.AddWithValue("@yr7", cash[6]);
                cmd.Parameters.AddWithValue("@yr8", cash[7]);
                cmd.Parameters.AddWithValue("@yr9", cash[8]);
                cmd.Parameters.AddWithValue("@yr10", cash[9]);
                cmd.Parameters.AddWithValue("@yr11", cash[10]);
                cmd.Parameters.AddWithValue("@yr12", cash[11]);
                cmd.Parameters.AddWithValue("@yr13", cash[12]);
                cmd.Parameters.AddWithValue("@yr14", cash[13]);
                cmd.Parameters.AddWithValue("@yr15", cash[14]);
                cmd.Parameters.AddWithValue("@yr16", cash[15]);
                cmd.Parameters.AddWithValue("@yr17", cash[16]);
                cmd.Parameters.AddWithValue("@yr18", cash[17]);
                cmd.Parameters.AddWithValue("@yr19", cash[18]);
                cmd.Parameters.AddWithValue("@yr20", cash[19]);
                cmd.Parameters.AddWithValue("@growth", custom_growth);
                cmd.ExecuteNonQuery();
            }

            Chart form = new Chart(low_value, normal_value, high_value, low_growth, growth, high_growth, custom_value, custom_growth, this);
            Hide();
            form.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Selection form = new Selection();
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        public CashFlow(double b, double m ,double r ,double g, string name)
        {
            InitializeComponent();
            beta = b;
            market = m;
            riskfree = r;
            growth = g;
            firm = name;
        }
    }
}
