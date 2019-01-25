using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;


namespace BDOnWorkLib
{
    public class InteractionWithBase
    {

        OleDbConnection oConn = new OleDbConnection();      //  объект БД

        public InteractionWithBase() { }

        public bool SettingConnectToBD() //  Метод, который настраивает связь с БД и проверяет, есть ли соединение
        {
            try
            {
                //  Строка соединеия с БД. Место расположения берётся из конфига (DirBD)
                oConn.ConnectionString =
                "Provider=Microsoft.ACE.OLEDB.12.0;" +
                String.Format(@"Data Source={0};", ConfigurationManager.AppSettings["DirBD"]) +
                "Extended Properties=\"Excel 12.0; HDR = YES\";";
                oConn.Open();
                oConn.Close();

            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к БД", "Ошибка подключения", MessageBoxButtons.OK);
                return false;
            }

            
            return true;
        }
    }
}
