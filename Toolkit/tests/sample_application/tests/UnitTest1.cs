using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orasi.Toolkit.orasi.utils;

namespace Orasi.Toolkit.tests.sample_application.tests
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public void TestMethod1()
        {
            TestEnvironment te = new TestEnvironment("FireFox");
        }
    }
}
