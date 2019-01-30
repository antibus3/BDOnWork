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
            oCon.OpenBD();

            SensitiveElement actual = new SensitiveElement(
                    id: 10,
                    numberVK: "10",
                    numberSIOM: "СТ-1-2-3-4",
                    signalLeftSIOM: 1000,
                    signalRigthSIOM: 346,
                    sPILeftSIOM: 32,
                    sPIRigthSIOM: 33,
                    lengthLeftSIOM: 70,
                    lengthRigthSIOM: 70,
                    signalVK: 500,
                    sPIVK: 25,
                    constantSignal: 201,
                    lengthLeftVK: 100,
                    lengthRigthVK: 130,
                    numberTemperatureSensor: "125",
                    isExperement: false
                    );

            QueriesToBD.SelectFromBD(oCon, new SensitiveElement());
            oCon.CloseBD();

        }
            
    }
}
