using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;
using System.Collections.Generic;

namespace UnitTestSensitiveElement
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EqualsTest()
        {
            Dictionary<string, object> Row1 = new Dictionary<string, object> {
                {"ID", 2},
                {"Номер_блока", 5873},
                {"Номер_СИОМ", "СТ-1-2-3-4"}
            };
            Dictionary<string, object> Row2 = new Dictionary<string, object> {
                {"ID", 2},
                {"Номер_блока", 5873},
                {"Номер_СИОМ", "СТ-1-2-3-4"}
            };
            Dictionary<string, object> Row3 = new Dictionary<string, object> {
                {"ID", 2},
                {"Номер_блока", 5873},
                {"Номер_СИОМ", "СТ-4-3-2-1"}
            };
            SensitiveElement Element1 = new SensitiveElement(Row1);
            SensitiveElement Element2 = new SensitiveElement(Row2);
            bool result = Element1.Equals(Element2);
            Assert.IsTrue(result);
            Element2 = new SensitiveElement(Row3);
            result = Element1.Equals(Element2);
            Assert.IsFalse(result);

        }
    }
}
