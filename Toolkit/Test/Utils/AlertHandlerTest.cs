using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]
    public class AlertHandlerTest
    {
        FirefoxDriver _driver;


        [SetUp]
        public void Startup()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
        }

        [Test]
        public void IsAlertPresent()
        {
            Assert.True(AlertHandler.IsAlertPresent(_driver, 3));
        }

        [Test]
        public void HandleAlertTest()
        {
            Assert.IsTrue(AlertHandler.HandleAlert(_driver,3));
        }

        [Test]
        public void HandleAllAlertTest()
        {
            Assert.IsTrue(AlertHandler.HandleAllAlerts(_driver, 2));
            Assert.IsTrue(_driver.FindElement(By.Id("button")).Enabled);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}