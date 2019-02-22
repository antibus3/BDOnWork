using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;
using System.Collections.Generic;
using System.Configuration;

namespace UnitTestQueriesToBD
{
    [TestClass]
    public class UnitTestQueriesToBD
    {

        [TestMethod]
        public void SelectFromBDTest()
        {

            //  Тест на возвращение вообще чегонибудь
            SensitiveElement FindElement = new SensitiveElement();
            InteractionWithBase bd = new InteractionWithBase(ConfigurationManager.AppSettings["DirBD"]);
            bd.SettingConnectToBD();
            var find = QueriesToBD.SelectFromBD(bd,"БД", FindElement);
            bool condition = find is List<SensitiveElement>;
            Assert.IsTrue(condition);
        
            //  Тест на возвращение 1 строки БД (блока с id = 1, Для теста он должен быть в бд)
            Dictionary<string, object> row = new Dictionary<string, object> { { "ID", 1 } };
            FindElement = new SensitiveElement(row);
            List<SensitiveElement> expected = QueriesToBD.SelectFromBD(bd, "БД", FindElement);

            Dictionary<string, object> actualRow = new Dictionary<string, object>
            {
                { "ID", 1},
                {"Номер_блока",  "1"},
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
                
            Assert.AreEqual(expected[0], actual); 

        }
        
        [TestMethod]
        public void InsertFromBDTest ()
        {
            
            InteractionWithBase bd = new InteractionWithBase(ConfigurationManager.AppSettings["DirBD"]);
            bd.SettingConnectToBD();
            Dictionary<string, object> actualRow = new Dictionary<string, object>
            {
                { "ID", 10},
                {"Номер_блока",  "1"},
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
            SensitiveElement TestElement = new SensitiveElement(actualRow);
            bool IsThere =  QueriesToBD.InsertFromBD(bd, "БД", TestElement);
            Assert.IsTrue(IsThere); //  Вставлено ли?
            List<SensitiveElement> expected = QueriesToBD.SelectFromBD(bd, "БД", TestElement);
            Assert.AreEqual(expected[0], TestElement); //  Сравнивает, то ли он вставил
            Assert.AreEqual(expected.Count, 1); //  вставил ли он 1 строку
            

        }

        //  Я незнаю, как тестит приватные методы, поэтому в задницу его, на на всякий оставлю
        /*
        public void FormRequestTest ()
        {
            SensitiveElement Element = new SensitiveElement(numberVK: "1234", numberSIOM: "CQT-1-2-3-4", isExperement : true);
            string expected = QueriesToBD.FormRequest(Element);
            string actual = "select * from[БД$] where Номер_блока = \"1234\" and Номер_СИОМ = \"CQT-1-2-3-4\" and IsExperemental = true and ";
            Assert.AreEqual(expected, actual);

        }
        */


    }
}
