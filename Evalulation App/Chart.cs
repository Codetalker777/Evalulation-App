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
    public partial class Chart : Form
    {
        public string database = "DataSource=\"DB.sdf\"; Password=\"\"";
        private SqlCeCommand cmd;
        private SqlCeConnection connect;
        private SqlCeDataAdapter da;
        private DataTable dt;

        public double low_value, value, high_value, custom_value;
        public double low_growth, growth, high_growth, custom_growth;
        public CashFlow previous = null;
        public Advanced previous2 = null;
        string firm;

        int id = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            AllData form = new AllData(firm);
            Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                if (previous != null)
                {
                    previous.Close();
                }
                if (previous2 != null)
                {
                    previous2.Close();
                }
                Form1 form = new Form1();
                Hide();
                form.Closed += (s, args) => this.Close();
                form.ShowDialog();
            }
            else if (id == 1)
            {
                Form1 form = new Form1();
                Hide();
                form.Closed += (s, args) => this.Close();
                form.ShowDialog();
            }
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            if (id == 0)
            {
                chart1.Series.Clear();
                growth = growth * 100;
                high_growth = high_growth * 100;
                low_growth = low_growth * 100;
                custom_growth = custom_growth * 100;
                chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;

                chart1.Series.Add(low_growth.ToString() + "%");
                chart1.Series.Add(growth.ToString() + "%");
                chart1.Series.Add(high_growth.ToString() + "%");
                chart1.Series.Add(custom_growth.ToString() + "%");

                chart1.Series[low_growth.ToString() + "%"].Points.AddY(low_value);
                chart1.Series[growth.ToString() + "%"].Points.AddY(value);
                chart1.Series[high_growth.ToString() + "%"].Points.AddY(high_value);
                chart1.Series[custom_growth.ToString() + "%"].Points.AddY(custom_value);
            }
            else if (id == 1)
            {
                button2.Visible = true;
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
                cmd = new SqlCeCommand(@"SELECT Value, GrowthRate FROM FirmCustom WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                custom_value = double.Parse(dt.Rows[0][0].ToString());
                custom_growth = double.Parse(dt.Rows[0][1].ToString());

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT Value FROM FirmNormal WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                value = double.Parse(dt.Rows[0][0].ToString());

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT Value FROM FirmLow WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                low_value = double.Parse(dt.Rows[0][0].ToString());

                connect = new SqlCeConnection(database);
                connect.Open();
                cmd = new SqlCeCommand(@"SELECT Value FROM FirmHigh WHERE @name = Firm", connect);
                cmd.Parameters.AddWithValue("@name", firm.Trim());
                da = new SqlCeDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteReader();

                high_value = double.Parse(dt.Rows[0][0].ToString());

                growth = growth * 100;
                high_growth = high_growth * 100;
                low_growth = low_growth * 100;
                custom_growth = custom_growth * 100;

                chart1.Series.Clear();
                chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;

                chart1.Series.Add(low_growth.ToString() + "%");
                chart1.Series.Add(growth.ToString() + "%");
                chart1.Series.Add(high_growth.ToString() + "%");
                chart1.Series.Add(custom_growth.ToString() + "%");

                chart1.Series[low_growth.ToString() + "%"].Points.AddY(low_value);
                chart1.Series[growth.ToString() + "%"].Points.AddY(value);
                chart1.Series[high_growth.ToString() + "%"].Points.AddY(high_value);
                chart1.Series[custom_growth.ToString() + "%"].Points.AddY(custom_value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                Close();
            }
            else if  (id == 1)
            {
                Loading form2 = new Loading();
                Hide();
                form2.Closed += (s, args) => this.Close();
                form2.ShowDialog();
            }
        }

        public Chart(string f)
        {
            InitializeComponent();
            id = 1;
            firm = f;
        }

        public Chart(double lv, double v, double hv, double lg, double g, double hg, double cv, double cg, CashFlow ob)
        {
            InitializeComponent();
            low_value = lv;
            value = v;
            high_value = hv;
            low_growth = lg;
            growth = g;
            high_growth = hg;
            custom_value = cv;
            custom_growth = cg;
            previous = ob;
        }
        public Chart(double lv, double v, double hv, double lg, double g, double hg, double cv, double cg, CashFlow ob,Advanced ob2)
        {
            InitializeComponent();
            low_value = lv;
            value = v;
            high_value = hv;
            low_growth = lg;
            growth = g;
            high_growth = hg;
            custom_value = cv;
            custom_growth = cg;
            previous = ob;
            previous2 = ob2;
        }
    }
}
