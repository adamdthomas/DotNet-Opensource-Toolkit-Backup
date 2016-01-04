using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Orasi.Toolkit.Utils;
using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orasi.Toolkit
{
    [TestFixture]
    //[TestClass]
    public class Sandbox
    {
        //FirefoxDriver _driver;
        IWebDriver _driver;

        //[TestInitialize]
        //[TestFixtureSetUp]
        /*public void TestSetup()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");
        }*/

        [Test]
        //[TestCase()]
        //[TestMethod]
        public void SampleLoginTest()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");
            _driver.FindElement(By.Id("employee_username")).SendKeys("company.admin");
            _driver.FindElement(By.Id("employee_password")).SendKeys("blah");
            _driver.FindElement(By.XPath("//input[@value='Login']")).Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Logout']"))).Displayed);
        }

        [TearDown]
        //[TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }

        //[Test]
        public void sleeper()
        {
            Sleeper.sleep(3000);
        }

        
    }
}