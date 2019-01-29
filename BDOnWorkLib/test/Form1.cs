using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            try
            {
                throw new Exception("123");
            }
            catch (Exception q)
            {
                MessageBox.Show(q.Message.ToString(), "Рукожёп детектед", MessageBoxButtons.OK);

            }

        }
    }
}
