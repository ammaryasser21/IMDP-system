using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace Software_Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        CrystalReport3 cr1;
        CrystalReport1 cr2;
        private void Form2_Load(object sender, EventArgs e)
        {
            cr1 = new CrystalReport3();
            cr2 = new CrystalReport1();
            foreach (ParameterDiscreteValue v in cr1.ParameterFields[0].DefaultValues)
                comboBox1.Items.Add(v.Value);
            crystalReportViewer4.ReportSource = cr2;
        }



        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            cr1.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer3.ReportSource = cr1;
        }
    }
}
