using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;
using System.Collections.Generic;

namespace UnitTestQueriesToBD
{
    [TestClass]
    public class UnitTestQueriesToBD
    {
        [TestMethod]
        public void SelectFromBDTest()
        {
            SensitiveElement FindElement = new SensitiveElement();
            InteractionWithBase bd = new InteractionWithBase();
            bd.SettingConnectToBD();
            bd.OpenBD();
            var find = QueriesToBD.SelectFromBD(bd, FindElement);
            bd.CloseBD();
            bool condition = find is List<SensitiveElement>;
            Assert.IsTrue(condition);
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
