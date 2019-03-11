using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace BDOnWorkLib
{
    public class InteractionWithBase
    {

        public OleDbConnection oConn {get; private set; }     //  объект БД
        private string DirectionFile;
        
        private bool IsOpened = false;

        public InteractionWithBase(string directionFile)
        {
            DirectionFile = directionFile;
        }

        public bool SettingConnectToBD() //  Метод, который настраивает связь с БД и проверяет, есть ли соединение
        {
            try
            {
                oConn = new OleDbConnection();
                //  Проверка, есть ли вообще файл БД
                FileInfo BD = new FileInfo(DirectionFile);
                if (!BD.Exists) return false;
                //  Строка соединеия с БД. Место расположения берётся из конфига (DirBD)
                oConn.ConnectionString =
                "Provider=Microsoft.ACE.OLEDB.12.0;" +
                String.Format(@"Data Source={0};", BD.FullName) +
                "Extended Properties=\"Excel 12.0; HDR = YES\";";
                //  На всякий открыть и закрыть БД, проверить, всё ли открывается и закрывается
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

        public bool OpenBD ()
        {
            try
            {
                oConn.Open();
                IsOpened = true;
                return true;
            }
            catch
            {
                if (IsOpened) return true; //  если исключение появилось при уже открытой базе, то всёравно считать, что база открыта (повторное открытие) хотя это не точно!!
                MessageBox.Show("Не удалось открыть БД", "Ошибка подключения", MessageBoxButtons.OK);
                IsOpened = false;
                return false;
            }
        }

        public bool CloseBD ()
        {
            try
            {
                oConn.Close();
                IsOpened = false;
                return true;
            }
            catch
            {
               // повторное закрытие вообще не вызывает исключение, хз почему.
                MessageBox.Show("Не удалось закрыть БД", "Ошибка подключения", MessageBoxButtons.OK);
                IsOpened = true;
                return false;
            }
        }

    }
}
