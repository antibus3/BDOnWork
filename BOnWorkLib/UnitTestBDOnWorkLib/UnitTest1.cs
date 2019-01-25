using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;

namespace UnitTestBDOnWorkLib
{
    [TestClass]
    public class UnitTestBDOnWorkLib
    {
        [TestMethod]
        public void ConnectToBDTest()
        {
            bool expected = true;
            InteractionWithBase BD = new InteractionWithBase();
            bool actual = BD.SettingConnectToBD();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OpenBDTest()
        {
            bool expected = true;
            InteractionWithBase BD = new InteractionWithBase();
            BD.SettingConnectToBD();
            bool actual = BD.OpenBD();
            Assert.AreEqual(expected, actual);
        }
    }
}
