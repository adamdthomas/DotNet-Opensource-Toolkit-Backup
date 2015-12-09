using System;
 
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]
    public class AlertHandlerTest
    {
        FirefoxDriver _driver;


        [SetUp]
        public void startup()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
        }

        [Test]
        public void isAlertPresent()
        {
            Assert.True(AlertHandler.isAlertPresent(_driver, 3));
        }

        [Test]
        public void handleAlertTest()
        {
            Assert.IsTrue(AlertHandler.handleAlert(_driver,3));
        }

        [Test]
        public void handleAllAlertTest()
        {
            Assert.IsTrue(AlertHandler.handleAllAlerts(_driver, 2));
            Assert.IsTrue(_driver.FindElement(By.Id("button")).Enabled);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}