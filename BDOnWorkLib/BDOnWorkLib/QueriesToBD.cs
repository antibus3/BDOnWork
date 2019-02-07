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
            try
            {
                DataTable tDT = new DataTable();
                string request = "select * from[БД$]";  //  Сам запрос
                request += FormFildRequest(SelectElement);
                oConn.OpenBD();
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
                foreach(string s in Element.GetListHead())
                {
                    result += String.Format(" {0} = \"{1}\" and", s, Element.Filds[s]);
                }
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
                Dictionary<string, object> resultFilds = new Dictionary<string, object>();                                                                                     
                foreach (DataColumn s in r.Table.Columns)
                {
                    resultFilds.Add(s.Caption, r[s]);
                }
                //  Ну и возвратить результат
                return new SensitiveElement(resultFilds);
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
                //  Проверка на дубляж
                SensitiveElement findElement = new SensitiveElement(new Dictionary<string, object> { { "ID", SelectElement.Filds["ID"] } });
                if (SelectFromBD(oConn, findElement ).Count != 0)
                    throw new ErrorInsertExceptions("Ошибка! Такой элемент уже содержится в БД.");
                if (SelectElement.Filds["ID"] == null)
                    throw new ErrorInsertExceptions("Ошибка! Элемент должен содержать ID.");
                //  Составление части запроса на вставку со столбцами
                string heads = "(";
                string values = " values (";
                foreach (string s in SelectElement.GetListHead())
                {
                    heads += String.Format(" {0},", s);
                    values += String.Format(" \"{0}\",", SelectElement.Filds[s]);
                }
                //  Удалить последние запятые и добавить закрывающеюся скобку
                heads = heads.Remove(heads.Length - 1); heads += ")";
                values = values.Remove(values.Length - 1); values += ")";
                string request = String.Format("insert into [БД$] {0} {1}", heads, values);
                oConn.OpenBD();
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
                Dictionary<string, object> UpdateID = new Dictionary<string, object>
                {
                    { "ID", SelectElement.Filds["ID"]}
                };
                SensitiveElement UpdateElement = new SensitiveElement(UpdateID);
                if (SelectFromBD(oConn, UpdateElement).Count == 0) throw new ErrorDeleteException("Ошибка! Такой элемент не содержится в БД.");
                if (SelectFromBD(oConn, UpdateElement).Count > 1) throw new Exception("В БД содержатся несколько элементов с одним ID"); 
                //  Формирование запроса на обновление
                string request = "update [БД$]";
                string set = " set";
                foreach (string s in SelectElement.GetListHead())
                {
                    set += String.Format(" {0} = \"{1}\",", s, SelectElement.Filds[s]);
                }
                //  Удалить последнюю запятую
                set = set.Remove(set.Length - 1);
                request += set;
                /*
                request += String.Format(" set Номер_блока = \"{1}\", Номер_СИОМ = \"{2}\", Uвыx_лев ={3}, Uвых_прав = {4}, СПИлев = {5}, СПИправ = {6}, LСИОМлев = {7}, LСИОМправ = {8}, Uвк = {9}, СПИвк = {10}, Uпост = {11}, LВКлев = {12}, LВКправ = {13}, ТД = \"{14}\", IsExperemental = \"{15}\"",
                                SelectElement.Id, SelectElement.NumberVK, SelectElement.NumberSIOM, SelectElement.SignalLeftSIOM, SelectElement.SignalRigthSIOM,
                                SelectElement.SPILeftSIOM, SelectElement.SPIRigthSIOM, SelectElement.LengthLeftSIOM, SelectElement.LengthRigthSIOM,
                                SelectElement.SignalVK, SelectElement.SPIVK, SelectElement.ConstantSignal, SelectElement.LengthLeftVK, SelectElement.LengthRigthVK,
                                SelectElement.NumberTemperatureSensor, SelectElement.IsExperement);
                */
                request += String.Format(" where ID = \"{0}\"", SelectElement.Filds["ID"]);
                oConn.OpenBD();
                GetAdapter(oConn, request).Fill(new DataTable());
                oConn.CloseBD();
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
    
    

    }
}
