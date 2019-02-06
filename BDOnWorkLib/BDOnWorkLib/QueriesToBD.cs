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
     /*   

        //  Этом метод ищет в БД блоки со всеми полями, которые не равны 0 в передоваемом блоке
        static public List<SensitiveElement> SelectFromBD (InteractionWithBase oConn, SensitiveElement SelectElement)
        {
            try
            {
                oConn.OpenBD();
                DataTable tDT = new DataTable();
                string request = "select * from[БД$]";  //  Сам запрос
                request += FormFildRequest(SelectElement);
                GetAdapter(oConn, request).Fill(tDT);
                oConn.CloseBD();
                return ParseTable(tDT);
            } catch
            {
                MessageBox.Show("Возникла ошибка на операции выборки из БД. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }

        }

        // Метод, который формирует адаптер. Не вижу смысла его детлать, но рекомендуют весь повторяющийся код делать
        // в отдельный метод, поэтому сделал
        static private OleDbDataAdapter GetAdapter (InteractionWithBase oConn, string request)
        {
            try
            {
                OleDbDataAdapter oAdapt = new OleDbDataAdapter();
                OleDbCommand oCmd = new OleDbCommand();
                oCmd.Connection = oConn.oConn;
                oCmd.CommandText = request;
                oAdapt.SelectCommand = oCmd;
                return oAdapt;
            } catch
            {
                MessageBox.Show("Возникла ошибка на операции создания адаптера. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }

        }

        static private string FormFildRequest (SensitiveElement Element) //  Формирование части where в запроса в строке
        {
            try
            {
                string result = " where";
                //  Далее все поля, которые не равны нулю по умолчанию (т.е. которые надо найти)
                if (Element.Id != null) result += String.Format(" ID = \"{0}\" and", Element.Id);
                if (Element.NumberVK != null) result += String.Format(" Номер_блока = \"{0}\" and", Element.NumberVK);
                if (Element.NumberSIOM != null) result += String.Format(" Номер_СИОМ = \"{0}\" and", Element.NumberSIOM);
                if (Element.SignalLeftSIOM != null) result += String.Format(" Uвыx_лев = \"{0}\" and", Element.SignalLeftSIOM);
                if (Element.SignalRigthSIOM != null) result += String.Format(" Uвых_прав = \"{0}\" and", Element.SignalRigthSIOM);
                if (Element.SPILeftSIOM != null) result += String.Format(" СПИлев = \"{0}\" and", Element.SPILeftSIOM);
                if (Element.SPIRigthSIOM != null) result += String.Format(" СПИправ = \"{0}\" and", Element.SPIRigthSIOM);
                if (Element.LengthLeftSIOM != null) result += String.Format(" LСИОМлев = \"{0}\" and", Element.LengthLeftSIOM);
                if (Element.LengthRigthSIOM != null) result += String.Format(" LСИОМправ = \"{0}\" and", Element.LengthRigthSIOM);
                if (Element.SignalVK != null) result += String.Format(" Uвк = \"{0}\" and", Element.SignalVK);
                if (Element.SPIVK != null) result += String.Format(" СПИвк = \"{0}\" and", Element.SPIVK);
                if (Element.ConstantSignal != null) result += String.Format(" Uпост = \"{0}\" and", Element.ConstantSignal);
                if (Element.LengthLeftVK != null) result += String.Format(" LВКлев = \"{0}\" and", Element.LengthLeftVK);
                if (Element.LengthRigthVK != null) result += String.Format(" LВКправ = \"{0}\" and", Element.LengthRigthVK);
                if (Element.NumberTemperatureSensor != null) result += String.Format(" ТД = \"{0}\" and", Element.NumberTemperatureSensor);
                if (Element.IsExperement != null) result += String.Format(" IsExperemental = \"{0}\" and", Element.IsExperement);
                //  Надеюсь, последний and не закосячит запрос 
                //  Закосячит(
                //  Нужно удалить последнее слово. and или where, если не выбрано не одно поле
                for (int i=result.Length;i>0;i--)
                {
                    if (result[i-1] != ' ') result = result.Remove(i - 1); else break;
                }
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
                int? id = Int32.Parse((string)r.ItemArray[0]);
                string numberVK;
                if (r.ItemArray[1] != DBNull.Value) numberVK = (string)r.ItemArray[1]; else numberVK = null;
                string numberSIOM;
                if (r.ItemArray[2] != DBNull.Value) numberSIOM = (string)r.ItemArray[2]; else numberSIOM = null;
                double? signalLeftSIOM;
                if (r.ItemArray[3] != DBNull.Value) signalLeftSIOM = Double.Parse((string)r.ItemArray[3]); else signalLeftSIOM = null;
                double? signalRigthSIOM;
                if (r.ItemArray[4] != DBNull.Value) signalRigthSIOM = Double.Parse((string)r.ItemArray[4]); else signalRigthSIOM = null;
                double? sPILeftSIOM;
                if (r.ItemArray[5] != DBNull.Value) sPILeftSIOM = Double.Parse((string)r.ItemArray[5]); else sPILeftSIOM = null;
                double? sPIRigthSIOM;
                if (r.ItemArray[6] != DBNull.Value) sPIRigthSIOM = Double.Parse((string)r.ItemArray[6]); else sPIRigthSIOM = null;
                double? lengthLeftSIOM;
                if (r.ItemArray[7] != DBNull.Value) lengthLeftSIOM = Double.Parse((string)r.ItemArray[7]); else lengthLeftSIOM = null;
                double? lengthRigthSIOM;
                if (r.ItemArray[8] != DBNull.Value) lengthRigthSIOM = Double.Parse((string)r.ItemArray[8]); else lengthRigthSIOM = null;
                double? signalVK;
                if (r.ItemArray[9] != DBNull.Value) signalVK = Double.Parse((string)r.ItemArray[9]); else signalVK = null;
                double? sPIVK;
                if (r.ItemArray[10] != DBNull.Value) sPIVK = Double.Parse((string)r.ItemArray[10]); else sPIVK = null;
                double? constantSignal;
                if (r.ItemArray[11] != DBNull.Value) constantSignal = Double.Parse((string)r.ItemArray[11]); else constantSignal = null;
                double? lengthLeftVK;
                if (r.ItemArray[12] != DBNull.Value) lengthLeftVK = Double.Parse((string)r.ItemArray[12]); else lengthLeftVK = null;
                double? lengthRigthVK;
                if (r.ItemArray[13] != DBNull.Value) lengthRigthVK = Double.Parse((string)r.ItemArray[13]); else lengthRigthVK = null;
                string numberTemperatureSensor = (r.ItemArray[14] != DBNull.Value) ? (string)r.ItemArray[14] : null;
                bool? isExperement;
                if (r.ItemArray[15] != DBNull.Value) isExperement = Boolean.Parse((string)r.ItemArray[15]); else isExperement = null;
                // почемуто упрощёнка не работает с double?
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
        
        //  Метод вставки в БД
        static public bool InsertFromBD (InteractionWithBase oConn, SensitiveElement SelectElement)
        {
            try
            {
                oConn.OpenBD();
                //  Проверка на дубляж
                if (SelectFromBD(oConn, SelectElement).Count != 0) throw new ErrorInsertExceptions("Ошибка! Такой элемент уже содержится в БД.");
                string request = "insert into [БД$] (ID, Номер_блока, Номер_СИОМ, Uвыx_лев, Uвых_прав, " +      //  Основа insert
                                                    "СПИлев, СПИправ, LСИОМлев, LСИОМправ, Uвк, СПИвк, Uпост, LВКлев, LВКправ, ТД, IsExperemental) " +
                //  значения вставки
                String.Format("values ({0}, \"{1}\", \"{2}\", {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, \"{14}\", \"{15}\")",
                            SelectElement.Id, SelectElement.NumberVK, SelectElement.NumberSIOM, SelectElement.SignalLeftSIOM, SelectElement.SignalRigthSIOM,
                            SelectElement.SPILeftSIOM, SelectElement.SPIRigthSIOM, SelectElement.LengthLeftSIOM, SelectElement.LengthRigthSIOM,
                            SelectElement.SignalVK, SelectElement.SPIVK, SelectElement.ConstantSignal, SelectElement.LengthLeftVK, SelectElement.LengthRigthVK,
                            SelectElement.NumberTemperatureSensor, SelectElement.IsExperement);
                GetAdapter(oConn, request).Fill(new DataTable());
                oConn.CloseBD();
                return true;
            } catch (ErrorInsertExceptions ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                return false;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка на операции вставки строки. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }

       
        
        static public bool UpdateFromBD (InteractionWithBase oConn, SensitiveElement SelectElement)
        {
            try
            {
                if (SelectFromBD(oConn, SelectElement).Count == 0) throw new ErrorDeleteException("Ошибка! Такой элемент не содержится в БД.");
                if (SelectFromBD(oConn, SelectElement).Count > 1) throw new Exception("В БД содержатся с элемента с одним ID"); 
                oConn.OpenBD();
                string request = "update [БД$]";
                request += String.Format(" set Номер_блока = \"{1}\", Номер_СИОМ = \"{2}\", Uвыx_лев ={3}, Uвых_прав = {4}, СПИлев = {5}, СПИправ = {6}, LСИОМлев = {7}, LСИОМправ = {8}, Uвк = {9}, СПИвк = {10}, Uпост = {11}, LВКлев = {12}, LВКправ = {13}, ТД = \"{14}\", IsExperemental = \"{15}\"",
                                SelectElement.Id, SelectElement.NumberVK, SelectElement.NumberSIOM, SelectElement.SignalLeftSIOM, SelectElement.SignalRigthSIOM,
                                SelectElement.SPILeftSIOM, SelectElement.SPIRigthSIOM, SelectElement.LengthLeftSIOM, SelectElement.LengthRigthSIOM,
                                SelectElement.SignalVK, SelectElement.SPIVK, SelectElement.ConstantSignal, SelectElement.LengthLeftVK, SelectElement.LengthRigthVK,
                                SelectElement.NumberTemperatureSensor, SelectElement.IsExperement);
                request += String.Format(" where ID = \"{0}\"", SelectElement.Id);
                GetAdapter(oConn, request).Fill(new DataTable());
                return true;
            } catch(ErrorDeleteException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                return false;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка на операции обновления строки. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }
    
    */

    }
}
