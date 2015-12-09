using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit
{
    [TestClass]
    public class AlertHandlerTest
    {
        FirefoxDriver _driver;


        [TestInitialize]
        public void startup()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
        }
        [TestMethod]
        public void isAlertPresent()
        {
            Assert.IsTrue(AlertHandler.isAlertPresent(_driver, 3));
        }

        [TestMethod]
        public void handleAlertTest()
        {
            Assert.IsTrue(AlertHandler.handleAlert(_driver,3));
        }

        [TestMethod]
        public void handleAllAlertTest()
        {
            Assert.IsTrue(AlertHandler.handleAllAlerts(_driver, 2));
            Assert.IsTrue(_driver.FindElement(By.Id("button")).Enabled);
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}