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
            var condition = find is List<SensitiveElement>;
            Assert.IsTrue(condition);
        }
    }
}
