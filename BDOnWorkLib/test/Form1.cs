using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDOnWorkLib;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ParseChart c = new ParseChart("E:/CQT-3-06-1-33 110717.xlsx");
            c.Parent = panel1;
            GC.Collect();



        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                abc();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Рукожёп детектед", MessageBoxButtons.OK);
            }

        }
        private  void abc ()
        {
                Double.Parse("qwe");
        }
        
    }
}
