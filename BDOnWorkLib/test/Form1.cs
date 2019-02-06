using System;
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

            InteractionWithBase oCon = new InteractionWithBase();
            oCon.SettingConnectToBD();

            Dictionary<string, object> actualRow = new Dictionary<string, object>
            {
                { "ID", 10},
                {"Номер_блока",  "555"},
                {"Номер_СИОМ", "СТ-1-2-3-4"},
                { "Uвыx_лев",  157},
                { "Uвых_прав", 346},
                { "СПИлев", 32},
                { "СПИправ", 33},
                { "LСИОМлев", 70},
                { "LСИОМправ", 70},
                { "Uвк", 500},
                { "СПИвк", 25},
                { "Uпост", 201},
                { "LВКлев", 100},
                { "LВКправ", 130},
                { "ТД", "125"},
                { "IsExperemental", "false" }
            };
            SensitiveElement actual = new SensitiveElement(actualRow);
            bool b = QueriesToBD.UpdateFromBD(oCon, actual);

        }
            
    }
}
