using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;

namespace UnitTestBDOnWorkLib
{
    [TestClass]
    public class UnitTestBDOnWorkLib
    {
        [TestMethod]
        public void SettingConnectToBDTest()
        {
            bool expected = true;
            InteractionWithBase BD = new InteractionWithBase();
            bool actual = BD.SettingConnectToBD();
            Assert.AreEqual(expected, actual);
        }
    }
}
