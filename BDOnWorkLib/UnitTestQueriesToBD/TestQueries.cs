using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;
using System.Collections.Generic;

namespace UnitTestQueriesToBD
{
    [TestClass]
    public class UnitTestQueriesToBD
    {
        /*
        [TestMethod]
        public void SelectFromBDTest()
        {
            //  Тест на возвращение вообще чегонибудь
            SensitiveElement FindElement = new SensitiveElement();
            InteractionWithBase bd = new InteractionWithBase();
            bd.SettingConnectToBD();
            bd.OpenBD();
            var find = QueriesToBD.SelectFromBD(bd, FindElement);
            bd.CloseBD();
            bool condition = find is List<SensitiveElement>;
            Assert.IsTrue(condition);
            
            //  Тест на возвращение 1 строки БД
            FindElement = new SensitiveElement(id: 1);
            bd.OpenBD();
            List<SensitiveElement> expected = QueriesToBD.SelectFromBD(bd, FindElement);
            bd.CloseBD();

            SensitiveElement actual = new SensitiveElement(
                id: 1,
                numberVK: "1",
                numberSIOM: "СТ-1-2-3-4",
                signalLeftSIOM: 157,
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
            Assert.AreEqual(expected[0], actual); 

        }

        [TestMethod]
        public void InsertFromBDTest ()
        {
            /*
            InteractionWithBase bd = new InteractionWithBase();
            bd.SettingConnectToBD();
            SensitiveElement TestElement = new SensitiveElement(
                                                            id: 1,
                                                            numberVK: "1",
                                                            numberSIOM: "СТ-1-2-3-4",
                                                            signalLeftSIOM: 157,
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
            bd.OpenBD();

            bool IsThere =  QueriesToBD.InsertFromBD(bd, TestElement);
            bd.CloseBD();
            Assert.IsTrue(IsThere); //  Вставлено ли?
            List<SensitiveElement> expected = QueriesToBD.SelectFromBD(bd, TestElement);
            Assert.AreEqual(expected[0], TestElement); //  Сравнивает, то ли он вставил
            Assert.AreEqual(expected.Count, 1); //  вставил ли он 1 строку
            
            Assert.IsTrue(false);

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
