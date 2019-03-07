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
            ParseKoffer q = new ParseKoffer("E:/181206nmnpn120k5054mCT8108162_sko02_koffer_ComputeTrend.png");
            q.Parent = panel1;

        }
    }
}
