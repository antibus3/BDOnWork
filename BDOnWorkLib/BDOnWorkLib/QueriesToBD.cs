using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace BDOnWorkLib
{
    static public class QueriesToBD
    {

        //  Этом метод ищет в БД блоки со всеми полями, которые не равны 0 в передоваемом блоке
        static public List<SensitiveElement> SelectFromBD (InteractionWithBase oConn, SensitiveElement SelectElement)
        {
            oConn.OpenBD();
            OleDbCommand oCmd = new OleDbCommand();
            OleDbDataAdapter oAdapt = new OleDbDataAdapter();
            DataTable tDT = new DataTable();
            oCmd.Connection = oConn.oConn;
            oCmd.CommandText = FormRequest(SelectElement);
            oAdapt.SelectCommand = oCmd;
            oAdapt.Fill(tDT);  //  После этого, у нас есть таблица с найдеными блоками
            oConn.CloseBD();
            

        }

        static private string FormRequest (SensitiveElement Element) //  Формирование запроса в строке
        {
            try
            {
                string result = "select * from[БД$] where ";  //  Это должно быть обязательно
                //  Далее все поля, которые не равны нулю по умолчанию (т.е. которые надо найти)

                if (Element.NumberVK != null) result += String.Format("Номер_блока = \"{0}\" and ", Element.NumberVK);
                if (Element.NumberSIOM != null) result += String.Format("Номер_СИОМ = \"{0}\" and ", Element.NumberSIOM);
                if (Element.SignalLeftSIOM != 0) result += String.Format("Uвых.лев. = \"{0}\" and ", Element.SignalLeftSIOM);
                if (Element.SignalRigthSIOM != 0) result += String.Format("Uвых.прав. = \"{0}\" and ", Element.SignalRigthSIOM);
                if (Element.SPILeftSIOM != 0) result += String.Format("СПИлев. = \"{0}\" and ", Element.SPILeftSIOM);
                if (Element.SPIRigthSIOM != 0) result += String.Format("СПИправ. = \"{0}\" and ", Element.SPIRigthSIOM);
                if (Element.LengthLeftSIOM != 0) result += String.Format("LСИОМлев. = \"{0}\" and ", Element.LengthLeftSIOM);
                if (Element.LengthRigthSIOM != 0) result += String.Format("LСИОМправ. = \"{0}\" and ", Element.LengthRigthSIOM);
                if (Element.SignalVK != 0) result += String.Format("Uвк. = \"{0}\" and ", Element.SignalVK);
                if (Element.SPIVK != 0) result += String.Format("СПИвк. = \"{0}\" and ", Element.SPIVK);
                if (Element.ConstantSignal != 0) result += String.Format("Uпост. = \"{0}\" and ", Element.ConstantSignal);
                if (Element.LengthLeftVK != 0) result += String.Format("LВКлев. = \"{0}\" and ", Element.LengthLeftVK);
                if (Element.LengthRigthVK != 0) result += String.Format("LВКправ. = \"{0}\" and ", Element.LengthRigthVK);
                if (Element.NumberTemperatureSensor != null) result += String.Format("ТД = \"{0}\" and ", Element.NumberTemperatureSensor);
                if (Element.IsExperement != false) result += String.Format("ТД = \"{0}\" and ", Element.IsExperement);
                //  Надеюсь, последний and не закосячит запрос 
                return result;
            } catch 
            {
                MessageBox.Show("Возникла ошибка на операции создания запроса. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }

        //  Метод, разбирающий таблицу на строки
        static private List<SensitiveElement> ParseTable (DataTable dt )
        {
            try
            {
                List<SensitiveElement> result = new List<SensitiveElement>();
                foreach (DataRow r in dt.Rows)
                {
                    if (r.ItemArray[0] != DBNull.Value)  //  Вернуть может эта фигня что угодно(дохрена пустых строк), поэтому проверяю id
                        result.Add(ParseRow(r));
                }
                return result;
            } catch
            {
                MessageBox.Show("Возникла ошибка на операции парсинга таблицы. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }

        //  Метод парсинга строки
        static private SensitiveElement ParseRow(DataRow r)
        {
            try
            {
                if (r.ItemArray[0] == DBNull.Value) throw new Exception("Гавно какоето! Найден блок без ID"); //  такого быть не может по хорошему, но мало ли.
                                                                                                              //  Тупо парсить данные из ячеек в переменные.
                int id = Int32.Parse((string)r.ItemArray[0]);
                string numberVK = (r.ItemArray[1] != DBNull.Value) ? (string)r.ItemArray[1] : null;
                string numberSIOM = (r.ItemArray[2] != DBNull.Value) ? (string)r.ItemArray[2] : null;
                float signalLeftSIOM = (r.ItemArray[3] != DBNull.Value) ? (float)r.ItemArray[2] : 0f;
                float signalRigthSIOM = (r.ItemArray[4] != DBNull.Value) ? (float)r.ItemArray[4] : 0f;
                float sPILeftSIOM = (r.ItemArray[5] != DBNull.Value) ? (float)r.ItemArray[5] : 0f;
                float sPIRigthSIOM = (r.ItemArray[6] != DBNull.Value) ? (float)r.ItemArray[6] : 0f;
                float lengthLeftSIOM = (r.ItemArray[7] != DBNull.Value) ? (float)r.ItemArray[7] : 0f;
                float lengthRigthSIOM = (r.ItemArray[8] != DBNull.Value) ? (float)r.ItemArray[8] : 0f;
                float signalVK = (r.ItemArray[9] != DBNull.Value) ? (float)r.ItemArray[9] : 0f;
                float sPIVK = (r.ItemArray[10] != DBNull.Value) ? (float)r.ItemArray[10] : 0f;
                float constantSignal = (r.ItemArray[11] != DBNull.Value) ? (float)r.ItemArray[11] : 0f;
                float lengthLeftVK = (r.ItemArray[12] != DBNull.Value) ? (float)r.ItemArray[12] : 0f;
                float lengthRigthVK = (r.ItemArray[13] != DBNull.Value) ? (float)r.ItemArray[13] : 0f;
                string numberTemperatureSensor = (r.ItemArray[14] != DBNull.Value) ? (string)r.ItemArray[14] : null;
                bool isExperement = ((string)r.ItemArray[15] == "true") ? true : false;
                //  Ну и возвратить результат
                return new SensitiveElement(
                                            id: id,
                                            numberVK: numberVK,
                                            numberSIOM: numberSIOM,
                                            signalLeftSIOM: signalLeftSIOM,
                                            signalRigthSIOM: signalRigthSIOM,
                                            sPILeftSIOM: sPILeftSIOM,
                                            sPIRigthSIOM: sPIRigthSIOM,
                                            lengthLeftSIOM: lengthLeftSIOM,
                                            lengthRigthSIOM: lengthRigthSIOM,
                                            signalVK: signalVK,
                                            sPIVK: sPIVK,
                                            lengthLeftVK: lengthLeftVK,
                                            lengthRigthVK: lengthRigthVK,
                                            constantSignal: constantSignal,
                                            numberTemperatureSensor: numberTemperatureSensor,
                                            isExperement: isExperement
                                            );
            }catch
            {
                MessageBox.Show("Возникла ошибка на операции парсинга строки. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }




    }
}
