﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDOnWorkLib;

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
            Dictionary<string, object> Row1 = new Dictionary<string, object> {
                {"ID", 2},
                {"Номер_блока", 5873},
                {"Номер_СИОМ", "СТ-1-2-3-4"}
            };
            Dictionary<string, object> Row2 = new Dictionary<string, object> {
                {"ID", 2},
                {"Номер_блока", 5873},
                {"Номер_СИОМ", "СТ-1-2-3-4"}
            };
            Dictionary<string, object> Row3 = new Dictionary<string, object> {
                {"ID", 2},
                {"Номер_блока", 5873},
                {"Номер_СИОМ", "СТ-4-3-2-1"}
            };
            SensitiveElement Element1 = new SensitiveElement(Row1);
            SensitiveElement Element2 = new SensitiveElement(Row2);
            bool result = Element1.Equals(Element2);
            Element2 = new SensitiveElement(Row3);
            result = Element1.Equals(Element2);

        }
            
    }
}
