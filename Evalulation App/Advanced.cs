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
    public partial class Advanced : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;
        string firm;

        CashFlow previous;
        string lg, g, hg, cg;
        double[] cash = new double[20];
        double[] lg_cash = new double[20];
        double[] g_cash = new double[20];
        double[] hg_cash = new double[20];
        double[] cg_cash = new double[20];
        double growth, custom_growth, discount_rate;
        bool is_lg = false, is_g = false, is_hg = false, is_cg = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if (label22.Visible || label23.Visible || label24.Visible || label25.Visible || label26.Visible || label27.Visible ||
                label28.Visible || label29.Visible || label30.Visible || label31.Visible || label32.Visible || label33.Visible ||
                label34.Visible || label35.Visible || label36.Visible || label37.Visible || label38.Visible || label39.Visible ||
                label40.Visible || label41.Visible)
            {
                return;
            }
        double low_value, normal_value, high_value, custom_value;
        double low_horizon, normal_horizon, high_horizon, custom_horizon;
        double[] pv_low = new double[20];
        double[] pv_normal = new double[20];
        double[] pv_high = new double[20];
        double[] pv_custom = new double[20];
        double high_growth = growth * 1.1;
        double low_growth = growth * 0.9;
        custom_value = normal_value = low_value = high_value = 0;

         for (int i = 0; i < 20; i++)
            {
                pv_low[i] = lg_cash[i] / Math.Pow(1 + discount_rate, i + 1);
                pv_normal[i] = g_cash[i] / Math.Pow(1 + discount_rate, i + 1);
                pv_high[i] = hg_cash[i] / Math.Pow(1 + discount_rate, i + 1);
                pv_custom[i] = cg_cash[i] / Math.Pow(1 + discount_rate, i + 1);

                low_value += pv_low[i];
                normal_value += pv_normal[i];
                high_value += pv_high[i];
                custom_value += pv_custom[i];
            }
            low_horizon = ((lg_cash[19] * (1 + low_growth)) / (discount_rate - low_growth)) / Math.Pow(1 + discount_rate, 20);
            normal_horizon = ((g_cash[19] * (1 + growth)) / (discount_rate - growth)) / Math.Pow(1 + discount_rate, 20);
            high_horizon = ((hg_cash[19] * (1 + high_growth)) / (discount_rate - high_growth)) / Math.Pow(1 + discount_rate, 20);
            custom_horizon = ((cg_cash[19] * (1 + custom_growth)) / (discount_rate - custom_growth)) / Math.Pow(1 + discount_rate, 20);

            low_value += low_horizon;
            normal_value += normal_horizon;
            high_value += high_horizon;
            custom_value += custom_horizon;

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"UPDATE FirmNormal SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20 WHERE Firm = @name", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            cmd.Parameters.AddWithValue("@val", normal_value);
            cmd.Parameters.AddWithValue("@yr1", g_cash[0]);
            cmd.Parameters.AddWithValue("@yr2", g_cash[1]);
            cmd.Parameters.AddWithValue("@yr3", g_cash[2]);
            cmd.Parameters.AddWithValue("@yr4", g_cash[3]);
            cmd.Parameters.AddWithValue("@yr5", g_cash[4]);
            cmd.Parameters.AddWithValue("@yr6", g_cash[5]);
            cmd.Parameters.AddWithValue("@yr7", g_cash[6]);
            cmd.Parameters.AddWithValue("@yr8", g_cash[7]);
            cmd.Parameters.AddWithValue("@yr9", g_cash[8]);
            cmd.Parameters.AddWithValue("@yr10", g_cash[9]);
            cmd.Parameters.AddWithValue("@yr11", g_cash[10]);
            cmd.Parameters.AddWithValue("@yr12", g_cash[11]);
            cmd.Parameters.AddWithValue("@yr13", g_cash[12]);
            cmd.Parameters.AddWithValue("@yr14", g_cash[13]);
            cmd.Parameters.AddWithValue("@yr15", g_cash[14]);
            cmd.Parameters.AddWithValue("@yr16", g_cash[15]);
            cmd.Parameters.AddWithValue("@yr17", g_cash[16]);
            cmd.Parameters.AddWithValue("@yr18", g_cash[17]);
            cmd.Parameters.AddWithValue("@yr19", g_cash[18]);
            cmd.Parameters.AddWithValue("@yr20", g_cash[19]);
            cmd.ExecuteNonQuery();

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"UPDATE FirmLow SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20 WHERE Firm = @name", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            cmd.Parameters.AddWithValue("@val", low_value);
            cmd.Parameters.AddWithValue("@yr1", lg_cash[0]);
            cmd.Parameters.AddWithValue("@yr2", lg_cash[1]);
            cmd.Parameters.AddWithValue("@yr3", lg_cash[2]);
            cmd.Parameters.AddWithValue("@yr4", lg_cash[3]);
            cmd.Parameters.AddWithValue("@yr5", lg_cash[4]);
            cmd.Parameters.AddWithValue("@yr6", lg_cash[5]);
            cmd.Parameters.AddWithValue("@yr7", lg_cash[6]);
            cmd.Parameters.AddWithValue("@yr8", lg_cash[7]);
            cmd.Parameters.AddWithValue("@yr9", lg_cash[8]);
            cmd.Parameters.AddWithValue("@yr10", lg_cash[9]);
            cmd.Parameters.AddWithValue("@yr11", lg_cash[10]);
            cmd.Parameters.AddWithValue("@yr12", lg_cash[11]);
            cmd.Parameters.AddWithValue("@yr13", lg_cash[12]);
            cmd.Parameters.AddWithValue("@yr14", lg_cash[13]);
            cmd.Parameters.AddWithValue("@yr15", lg_cash[14]);
            cmd.Parameters.AddWithValue("@yr16", lg_cash[15]);
            cmd.Parameters.AddWithValue("@yr17", lg_cash[16]);
            cmd.Parameters.AddWithValue("@yr18", lg_cash[17]);
            cmd.Parameters.AddWithValue("@yr19", lg_cash[18]);
            cmd.Parameters.AddWithValue("@yr20", lg_cash[19]);
            cmd.ExecuteNonQuery();

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"UPDATE FirmHigh SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20 WHERE Firm = @name", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            cmd.Parameters.AddWithValue("@val", high_value);
            cmd.Parameters.AddWithValue("@yr1", hg_cash[0]);
            cmd.Parameters.AddWithValue("@yr2", hg_cash[1]);
            cmd.Parameters.AddWithValue("@yr3", hg_cash[2]);
            cmd.Parameters.AddWithValue("@yr4", hg_cash[3]);
            cmd.Parameters.AddWithValue("@yr5", hg_cash[4]);
            cmd.Parameters.AddWithValue("@yr6", hg_cash[5]);
            cmd.Parameters.AddWithValue("@yr7", hg_cash[6]);
            cmd.Parameters.AddWithValue("@yr8", hg_cash[7]);
            cmd.Parameters.AddWithValue("@yr9", hg_cash[8]);
            cmd.Parameters.AddWithValue("@yr10", hg_cash[9]);
            cmd.Parameters.AddWithValue("@yr11", hg_cash[10]);
            cmd.Parameters.AddWithValue("@yr12", hg_cash[11]);
            cmd.Parameters.AddWithValue("@yr13", hg_cash[12]);
            cmd.Parameters.AddWithValue("@yr14", hg_cash[13]);
            cmd.Parameters.AddWithValue("@yr15", hg_cash[14]);
            cmd.Parameters.AddWithValue("@yr16", hg_cash[15]);
            cmd.Parameters.AddWithValue("@yr17", hg_cash[16]);
            cmd.Parameters.AddWithValue("@yr18", hg_cash[17]);
            cmd.Parameters.AddWithValue("@yr19", hg_cash[18]);
            cmd.Parameters.AddWithValue("@yr20", hg_cash[19]);
            cmd.ExecuteNonQuery();

            connect = new SqlCeConnection(database);
            connect.Open();
            cmd = new SqlCeCommand(@"UPDATE FirmCustom SET Value = @val, Year1 = @yr1, Year2 = @yr2, Year3 = @yr3, Year4 = @yr4, Year5 = @yr5, Year6 = @yr6, Year7 = @yr7, Year8 = @yr8, Year9 = @yr9, Year10 = @yr10, Year11 = @yr11, Year12 = @yr12, Year13 = @yr13, Year14 = @yr14, Year15 = @yr15, Year16 = @yr16, Year17 = @yr17, Year18 = @yr18, Year19 = @yr19, Year20 = @yr20, GrowthRate = @growth WHERE Firm = @name", connect);
            cmd.Parameters.AddWithValue("@name", firm.Trim());
            cmd.Parameters.AddWithValue("@val", custom_value);
            cmd.Parameters.AddWithValue("@yr1", cg_cash[0]);
            cmd.Parameters.AddWithValue("@yr2", cg_cash[1]);
            cmd.Parameters.AddWithValue("@yr3", cg_cash[2]);
            cmd.Parameters.AddWithValue("@yr4", cg_cash[3]);
            cmd.Parameters.AddWithValue("@yr5", cg_cash[4]);
            cmd.Parameters.AddWithValue("@yr6", cg_cash[5]);
            cmd.Parameters.AddWithValue("@yr7", cg_cash[6]);
            cmd.Parameters.AddWithValue("@yr8", cg_cash[7]);
            cmd.Parameters.AddWithValue("@yr9", cg_cash[8]);
            cmd.Parameters.AddWithValue("@yr10", cg_cash[9]);
            cmd.Parameters.AddWithValue("@yr11", cg_cash[10]);
            cmd.Parameters.AddWithValue("@yr12", cg_cash[11]);
            cmd.Parameters.AddWithValue("@yr13", cg_cash[12]);
            cmd.Parameters.AddWithValue("@yr14", cg_cash[13]);
            cmd.Parameters.AddWithValue("@yr15", cg_cash[14]);
            cmd.Parameters.AddWithValue("@yr16", cg_cash[15]);
            cmd.Parameters.AddWithValue("@yr17", cg_cash[16]);
            cmd.Parameters.AddWithValue("@yr18", cg_cash[17]);
            cmd.Parameters.AddWithValue("@yr19", cg_cash[18]);
            cmd.Parameters.AddWithValue("@yr20", cg_cash[19]);
            cmd.Parameters.AddWithValue("@growth", custom_growth);
            cmd.ExecuteNonQuery();

            Chart form = new Chart(low_value, normal_value, high_value, low_growth, growth, high_growth, custom_value, custom_growth,previous,this);
            Hide();
            form.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (is_lg)
            {
                if (!double.TryParse(textBox1.Text, out lg_cash[0]))
                {
                    label22.Visible = true;
                }
                else
                {
                    label22.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out lg_cash[1]))
                {
                    label23.Visible = true;
                }
                else
                {
                    label23.Visible = false;
                }
                if (!double.TryParse(textBox3.Text, out lg_cash[2]))
                {
                    label24.Visible = true;
                }
                else
                {
                    label24.Visible = false;
                }
                if (!double.TryParse(textBox4.Text, out lg_cash[3]))
                {
                    label25.Visible = true;
                }
                else
                {
                    label25.Visible = false;
                }
                if (!double.TryParse(textBox5.Text, out lg_cash[4]))
                {
                    label26.Visible = true;
                }
                else
                {
                    label26.Visible = false;
                }
                if (!double.TryParse(textBox6.Text, out lg_cash[5]))
                {
                    label27.Visible = true;
                }
                else
                {
                    label27.Visible = false;
                }
                if (!double.TryParse(textBox7.Text, out lg_cash[6]))
                {
                    label28.Visible = true;
                }
                else
                {
                    label28.Visible = false;
                }
                if (!double.TryParse(textBox8.Text, out lg_cash[7]))
                {
                    label29.Visible = true;
                }
                else
                {
                    label29.Visible = false;
                }
                if (!double.TryParse(textBox9.Text, out lg_cash[8]))
                {
                    label30.Visible = true;
                }
                else
                {
                    label30.Visible = false;
                }
                if (!double.TryParse(textBox10.Text, out lg_cash[9]))
                {
                    label31.Visible = true;
                }
                else
                {
                    label31.Visible = false;
                }
                if (!double.TryParse(textBox11.Text, out lg_cash[10]))
                {
                    label32.Visible = true;
                }
                else
                {
                    label32.Visible = false;
                }
                if (!double.TryParse(textBox12.Text, out lg_cash[11]))
                {
                    label33.Visible = true;
                }
                else
                {
                    label33.Visible = false;
                }
                if (!double.TryParse(textBox13.Text, out lg_cash[12]))
                {
                    label34.Visible = true;
                }
                else
                {
                    label34.Visible = false;
                }
                if (!double.TryParse(textBox14.Text, out lg_cash[13]))
                {
                    label35.Visible = true;
                }
                else
                {
                    label35.Visible = false;
                }
                if (!double.TryParse(textBox15.Text, out lg_cash[14]))
                {
                    label36.Visible = true;
                }
                else
                {
                    label36.Visible = false;
                }
                if (!double.TryParse(textBox16.Text, out lg_cash[15]))
                {
                    label37.Visible = true;
                }
                else
                {
                    label37.Visible = false;
                }
                if (!double.TryParse(textBox17.Text, out lg_cash[16]))
                {
                    label38.Visible = true;
                }
                else
                {
                    label38.Visible = false;
                }
                if (!double.TryParse(textBox18.Text, out lg_cash[17]))
                {
                    label39.Visible = true;
                }
                else
                {
                    label39.Visible = false;
                }
                if (!double.TryParse(textBox19.Text, out lg_cash[18]))
                {
                    label40.Visible = true;
                }
                else
                {
                    label40.Visible = false;
                }
                if (!double.TryParse(textBox20.Text, out lg_cash[19]))
                {
                    label41.Visible = true;
                }
                else
                {
                    label41.Visible = false;
                }
            }
            if (is_g)
            {
                if (!double.TryParse(textBox1.Text, out g_cash[0]))
                {
                    label22.Visible = true;
                }
                else
                {
                    label22.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out g_cash[1]))
                {
                    label23.Visible = true;
                }
                else
                {
                    label23.Visible = false;
                }
                if (!double.TryParse(textBox3.Text, out g_cash[2]))
                {
                    label24.Visible = true;
                }
                else
                {
                    label24.Visible = false;
                }
                if (!double.TryParse(textBox4.Text, out g_cash[3]))
                {
                    label25.Visible = true;
                }
                else
                {
                    label25.Visible = false;
                }
                if (!double.TryParse(textBox5.Text, out g_cash[4]))
                {
                    label26.Visible = true;
                }
                else
                {
                    label26.Visible = false;
                }
                if (!double.TryParse(textBox6.Text, out g_cash[5]))
                {
                    label27.Visible = true;
                }
                else
                {
                    label27.Visible = false;
                }
                if (!double.TryParse(textBox7.Text, out g_cash[6]))
                {
                    label28.Visible = true;
                }
                else
                {
                    label28.Visible = false;
                }
                if (!double.TryParse(textBox8.Text, out g_cash[7]))
                {
                    label29.Visible = true;
                }
                else
                {
                    label29.Visible = false;
                }
                if (!double.TryParse(textBox9.Text, out g_cash[8]))
                {
                    label30.Visible = true;
                }
                else
                {
                    label30.Visible = false;
                }
                if (!double.TryParse(textBox10.Text, out g_cash[9]))
                {
                    label31.Visible = true;
                }
                else
                {
                    label31.Visible = false;
                }
                if (!double.TryParse(textBox11.Text, out g_cash[10]))
                {
                    label32.Visible = true;
                }
                else
                {
                    label32.Visible = false;
                }
                if (!double.TryParse(textBox12.Text, out g_cash[11]))
                {
                    label33.Visible = true;
                }
                else
                {
                    label33.Visible = false;
                }
                if (!double.TryParse(textBox13.Text, out g_cash[12]))
                {
                    label34.Visible = true;
                }
                else
                {
                    label34.Visible = false;
                }
                if (!double.TryParse(textBox14.Text, out g_cash[13]))
                {
                    label35.Visible = true;
                }
                else
                {
                    label35.Visible = false;
                }
                if (!double.TryParse(textBox15.Text, out g_cash[14]))
                {
                    label36.Visible = true;
                }
                else
                {
                    label36.Visible = false;
                }
                if (!double.TryParse(textBox16.Text, out g_cash[15]))
                {
                    label37.Visible = true;
                }
                else
                {
                    label37.Visible = false;
                }
                if (!double.TryParse(textBox17.Text, out g_cash[16]))
                {
                    label38.Visible = true;
                }
                else
                {
                    label38.Visible = false;
                }
                if (!double.TryParse(textBox18.Text, out g_cash[17]))
                {
                    label39.Visible = true;
                }
                else
                {
                    label39.Visible = false;
                }
                if (!double.TryParse(textBox19.Text, out g_cash[18]))
                {
                    label40.Visible = true;
                }
                else
                {
                    label40.Visible = false;
                }
                if (!double.TryParse(textBox20.Text, out g_cash[19]))
                {
                    label41.Visible = true;
                }
                else
                {
                    label41.Visible = false;
                }
            }
            if (is_hg)
            {
                if (!double.TryParse(textBox1.Text, out hg_cash[0]))
                {
                    label22.Visible = true;
                }
                else
                {
                    label22.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out hg_cash[1]))
                {
                    label23.Visible = true;
                }
                else
                {
                    label23.Visible = false;
                }
                if (!double.TryParse(textBox3.Text, out hg_cash[2]))
                {
                    label24.Visible = true;
                }
                else
                {
                    label24.Visible = false;
                }
                if (!double.TryParse(textBox4.Text, out hg_cash[3]))
                {
                    label25.Visible = true;
                }
                else
                {
                    label25.Visible = false;
                }
                if (!double.TryParse(textBox5.Text, out hg_cash[4]))
                {
                    label26.Visible = true;
                }
                else
                {
                    label26.Visible = false;
                }
                if (!double.TryParse(textBox6.Text, out hg_cash[5]))
                {
                    label27.Visible = true;
                }
                else
                {
                    label27.Visible = false;
                }
                if (!double.TryParse(textBox7.Text, out hg_cash[6]))
                {
                    label28.Visible = true;
                }
                else
                {
                    label28.Visible = false;
                }
                if (!double.TryParse(textBox8.Text, out hg_cash[7]))
                {
                    label29.Visible = true;
                }
                else
                {
                    label29.Visible = false;
                }
                if (!double.TryParse(textBox9.Text, out hg_cash[8]))
                {
                    label30.Visible = true;
                }
                else
                {
                    label30.Visible = false;
                }
                if (!double.TryParse(textBox10.Text, out hg_cash[9]))
                {
                    label31.Visible = true;
                }
                else
                {
                    label31.Visible = false;
                }
                if (!double.TryParse(textBox11.Text, out hg_cash[10]))
                {
                    label32.Visible = true;
                }
                else
                {
                    label32.Visible = false;
                }
                if (!double.TryParse(textBox12.Text, out hg_cash[11]))
                {
                    label33.Visible = true;
                }
                else
                {
                    label33.Visible = false;
                }
                if (!double.TryParse(textBox13.Text, out hg_cash[12]))
                {
                    label34.Visible = true;
                }
                else
                {
                    label34.Visible = false;
                }
                if (!double.TryParse(textBox14.Text, out hg_cash[13]))
                {
                    label35.Visible = true;
                }
                else
                {
                    label35.Visible = false;
                }
                if (!double.TryParse(textBox15.Text, out hg_cash[14]))
                {
                    label36.Visible = true;
                }
                else
                {
                    label36.Visible = false;
                }
                if (!double.TryParse(textBox16.Text, out hg_cash[15]))
                {
                    label37.Visible = true;
                }
                else
                {
                    label37.Visible = false;
                }
                if (!double.TryParse(textBox17.Text, out hg_cash[16]))
                {
                    label38.Visible = true;
                }
                else
                {
                    label38.Visible = false;
                }
                if (!double.TryParse(textBox18.Text, out hg_cash[17]))
                {
                    label39.Visible = true;
                }
                else
                {
                    label39.Visible = false;
                }
                if (!double.TryParse(textBox19.Text, out hg_cash[18]))
                {
                    label40.Visible = true;
                }
                else
                {
                    label40.Visible = false;
                }
                if (!double.TryParse(textBox20.Text, out hg_cash[19]))
                {
                    label41.Visible = true;
                }
                else
                {
                    label41.Visible = false;
                }
            }
            if (is_cg)
            {
                if (!double.TryParse(textBox1.Text, out cg_cash[0]))
                {
                    label22.Visible = true;
                }
                else
                {
                    label22.Visible = false;
                }
                if (!double.TryParse(textBox2.Text, out cg_cash[1]))
                {
                    label23.Visible = true;
                }
                else
                {
                    label23.Visible = false;
                }
                if (!double.TryParse(textBox3.Text, out cg_cash[2]))
                {
                    label24.Visible = true;
                }
                else
                {
                    label24.Visible = false;
                }
                if (!double.TryParse(textBox4.Text, out cg_cash[3]))
                {
                    label25.Visible = true;
                }
                else
                {
                    label25.Visible = false;
                }
                if (!double.TryParse(textBox5.Text, out cg_cash[4]))
                {
                    label26.Visible = true;
                }
                else
                {
                    label26.Visible = false;
                }
                if (!double.TryParse(textBox6.Text, out cg_cash[5]))
                {
                    label27.Visible = true;
                }
                else
                {
                    label27.Visible = false;
                }
                if (!double.TryParse(textBox7.Text, out cg_cash[6]))
                {
                    label28.Visible = true;
                }
                else
                {
                    label28.Visible = false;
                }
                if (!double.TryParse(textBox8.Text, out cg_cash[7]))
                {
                    label29.Visible = true;
                }
                else
                {
                    label29.Visible = false;
                }
                if (!double.TryParse(textBox9.Text, out cg_cash[8]))
                {
                    label30.Visible = true;
                }
                else
                {
                    label30.Visible = false;
                }
                if (!double.TryParse(textBox10.Text, out cg_cash[9]))
                {
                    label31.Visible = true;
                }
                else
                {
                    label31.Visible = false;
                }
                if (!double.TryParse(textBox11.Text, out cg_cash[10]))
                {
                    label32.Visible = true;
                }
                else
                {
                    label32.Visible = false;
                }
                if (!double.TryParse(textBox12.Text, out cg_cash[11]))
                {
                    label33.Visible = true;
                }
                else
                {
                    label33.Visible = false;
                }
                if (!double.TryParse(textBox13.Text, out cg_cash[12]))
                {
                    label34.Visible = true;
                }
                else
                {
                    label34.Visible = false;
                }
                if (!double.TryParse(textBox14.Text, out cg_cash[13]))
                {
                    label35.Visible = true;
                }
                else
                {
                    label35.Visible = false;
                }
                if (!double.TryParse(textBox15.Text, out cg_cash[14]))
                {
                    label36.Visible = true;
                }
                else
                {
                    label36.Visible = false;
                }
                if (!double.TryParse(textBox16.Text, out cg_cash[15]))
                {
                    label37.Visible = true;
                }
                else
                {
                    label37.Visible = false;
                }
                if (!double.TryParse(textBox17.Text, out cg_cash[16]))
                {
                    label38.Visible = true;
                }
                else
                {
                    label38.Visible = false;
                }
                if (!double.TryParse(textBox18.Text, out cg_cash[17]))
                {
                    label39.Visible = true;
                }
                else
                {
                    label39.Visible = false;
                }
                if (!double.TryParse(textBox19.Text, out cg_cash[18]))
                {
                    label40.Visible = true;
                }
                else
                {
                    label40.Visible = false;
                }
                if (!double.TryParse(textBox20.Text, out cg_cash[19]))
                {
                    label41.Visible = true;
                }
                else
                {
                    label41.Visible = false;
                }
            }
        }

        public Advanced(double[] c, double g, double cg, double d, CashFlow ob, string name)
        {
            InitializeComponent();
            cash[0] = c[0];
            cash[1] = c[1];
            cash[2] = c[2];
            cash[3] = c[3];
            cash[4] = c[4];
            growth = g;
            custom_growth = cg;
            discount_rate = d;
            previous = ob;
            firm = name;
        }

        private void Advanced_Load(object sender, EventArgs e)
        {

            int years;
            DateTime localdate = DateTime.Now;
            years = localdate.Year;
            label1.Text = (years + 1).ToString();
            label2.Text = (years + 2).ToString();
            label3.Text = (years + 3).ToString();
            label4.Text = (years + 4).ToString();
            label5.Text = (years + 5).ToString();
            label6.Text = (years + 6).ToString();
            label7.Text = (years + 7).ToString();
            label8.Text = (years + 8).ToString();
            label9.Text = (years + 9).ToString();
            label10.Text = (years + 10).ToString();
            label11.Text = (years + 11).ToString();
            label12.Text = (years + 12).ToString();
            label13.Text = (years + 13).ToString();
            label14.Text = (years + 14).ToString();
            label15.Text = (years + 15).ToString();
            label16.Text = (years + 16).ToString();
            label17.Text = (years + 17).ToString();
            label18.Text = (years + 18).ToString();
            label19.Text = (years + 19).ToString();
            label20.Text = (years + 20).ToString();

            textBox1.Text = cash[0].ToString();
            textBox2.Text = cash[1].ToString();
            textBox3.Text = cash[2].ToString();
            textBox4.Text = cash[3].ToString();
            textBox5.Text = cash[4].ToString();

            lg = (growth * 0.9 * 100).ToString() + "%";
            g = (growth * 100).ToString() + "%";
            hg = (growth * 1.1 * 100).ToString() + "%";
            cg = (custom_growth * 100).ToString() + "%";

            comboBox1.Items.Add(lg);
            comboBox1.Items.Add(g);
            comboBox1.Items.Add(hg);
            comboBox1.Items.Add(cg);

            for (int i = 0; i < 20; i++)
            {
                if (i < 5)
                {
                    lg_cash[i] = cash[i];
                    g_cash[i] = cash[i];
                    hg_cash[i] = cash[i];
                    cg_cash[i] = cash[i];
                }
                else
                {
                    lg_cash[i] = lg_cash[i - 1] * (1 + (growth * 0.9));
                    g_cash[i] = g_cash[i - 1] * (1 + (growth));
                    hg_cash[i] = hg_cash[i - 1] * (1 + (growth * 1.1));
                    cg_cash[i] = cg_cash[i - 1] * (1 + (custom_growth));
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == lg)
            {
                is_lg = true;
                is_g = false;
                is_hg = false;
                is_cg = false;
                textBox6.Text = lg_cash[5].ToString();
                textBox7.Text = lg_cash[6].ToString();
                textBox8.Text = lg_cash[7].ToString();
                textBox9.Text = lg_cash[8].ToString();
                textBox10.Text = lg_cash[9].ToString();
                textBox11.Text = lg_cash[10].ToString();
                textBox12.Text = lg_cash[11].ToString();
                textBox13.Text = lg_cash[12].ToString();
                textBox14.Text = lg_cash[13].ToString();
                textBox15.Text = lg_cash[14].ToString();
                textBox16.Text = lg_cash[15].ToString();
                textBox17.Text = lg_cash[16].ToString();
                textBox18.Text = lg_cash[17].ToString();
                textBox19.Text = lg_cash[18].ToString();
                textBox20.Text = lg_cash[19].ToString();
            }
            else if (comboBox1.Text == g)
            {
                is_lg = false;
                is_g = true;
                is_hg = false;
                is_cg = false;
                textBox6.Text = g_cash[5].ToString();
                textBox7.Text = g_cash[6].ToString();
                textBox8.Text = g_cash[7].ToString();
                textBox9.Text = g_cash[8].ToString();
                textBox10.Text = g_cash[9].ToString();
                textBox11.Text = g_cash[10].ToString();
                textBox12.Text = g_cash[11].ToString();
                textBox13.Text = g_cash[12].ToString();
                textBox14.Text = g_cash[13].ToString();
                textBox15.Text = g_cash[14].ToString();
                textBox16.Text = g_cash[15].ToString();
                textBox17.Text = g_cash[16].ToString();
                textBox18.Text = g_cash[17].ToString();
                textBox19.Text = g_cash[18].ToString();
                textBox20.Text = g_cash[19].ToString();
            }
            else if (comboBox1.Text == hg)
            {
                is_lg = false;
                is_g = false;
                is_hg = true;
                is_cg = false;
                textBox6.Text = hg_cash[5].ToString();
                textBox7.Text = hg_cash[6].ToString();
                textBox8.Text = hg_cash[7].ToString();
                textBox9.Text = hg_cash[8].ToString();
                textBox10.Text = hg_cash[9].ToString();
                textBox11.Text = hg_cash[10].ToString();
                textBox12.Text = hg_cash[11].ToString();
                textBox13.Text = hg_cash[12].ToString();
                textBox14.Text = hg_cash[13].ToString();
                textBox15.Text = hg_cash[14].ToString();
                textBox16.Text = hg_cash[15].ToString();
                textBox17.Text = hg_cash[16].ToString();
                textBox18.Text = hg_cash[17].ToString();
                textBox19.Text = hg_cash[18].ToString();
                textBox20.Text = hg_cash[19].ToString();
            }
            else if (comboBox1.Text == cg)
            {
                is_lg = false;
                is_g = false;
                is_hg = false;
                is_cg = true;
                textBox6.Text = cg_cash[5].ToString();
                textBox7.Text = cg_cash[6].ToString();
                textBox8.Text = cg_cash[7].ToString();
                textBox9.Text = cg_cash[8].ToString();
                textBox10.Text = cg_cash[9].ToString();
                textBox11.Text = cg_cash[10].ToString();
                textBox12.Text = cg_cash[11].ToString();
                textBox13.Text = cg_cash[12].ToString();
                textBox14.Text = cg_cash[13].ToString();
                textBox15.Text = cg_cash[14].ToString();
                textBox16.Text = cg_cash[15].ToString();
                textBox17.Text = cg_cash[16].ToString();
                textBox18.Text = cg_cash[17].ToString();
                textBox19.Text = cg_cash[18].ToString();
                textBox20.Text = cg_cash[19].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
