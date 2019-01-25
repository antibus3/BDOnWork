using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDOnWorkLib;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            InteractionWithBase BD = new InteractionWithBase();
            Assert.AreEqual(1, 1);
        }
    }
}
