using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BDOnWorkLib;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.OleDb.OleDbConnection oConn = new System.Data.OleDb.OleDbConnection();      //  объект БД
            oConn.ConnectionString =
             "Provider=Microsoft.ACE.OLEDB.12.0;" +
            String.Format(@"Data Source={0};", ConfigurationManager.AppSettings["DirBD"]) +
             "Extended Properties=\"Excel 12.0; HDR = YES\";";
            oConn.Open();
            oConn.Close();

        }
    }
}
