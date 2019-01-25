﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace BDOnWorkLib
{
    public class InteractionWithBase
    {

        OleDbConnection oConn = new OleDbConnection();      //  объект БД
        private bool IsOpened = false;

        public InteractionWithBase() { }

        public bool SettingConnectToBD() //  Метод, который настраивает связь с БД и проверяет, есть ли соединение
        {
            try
            { 
                //  Проверка, есть ли вообще файл БД
                FileInfo BD = new FileInfo(ConfigurationManager.AppSettings["DirBD"]);
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
                MessageBox.Show("Не удалось открыть БД", "Ошибка подключения", MessageBoxButtons.OK);
                IsOpened = false;
                return false;
            }
        }
    }
}
