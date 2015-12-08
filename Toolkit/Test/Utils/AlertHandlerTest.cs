using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Toolkit.Utils;

namespace Toolkit
{
    [TestClass]
    public class AlertHandlerTest
    {
        FirefoxDriver _driver;



        [TestMethod]
        public void isAlertPresent()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
            Assert.IsTrue(AlertHandler.isAlertPresent(_driver, 3));
        }

        [TestMethod]
        public void handleAlertTest()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
            Assert.IsTrue(AlertHandler.handleAlert(_driver,3));
        }

        [TestMethod]
        public void handleAllAlertTest()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
            Assert.IsTrue(AlertHandler.handleAllAlerts(_driver, 2));
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}