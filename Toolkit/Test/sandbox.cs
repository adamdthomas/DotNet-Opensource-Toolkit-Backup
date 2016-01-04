using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
//using NUnit.Framework;
using Orasi.Toolkit.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orasi.Toolkit
{
    //[TestFixture]
    [TestClass]
    public class Sandbox
    {
        //FirefoxDriver _driver;
        IWebDriver driver;

        [TestInitialize]
        public void TestSetup()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");
        }
        // [Test]
        [TestMethod]
        public void SampleLoginTest()
        {
            //_driver = new FirefoxDriver();
            //_driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");
            driver.FindElement(By.Id("employee_username")).SendKeys("company.admin");
            driver.FindElement(By.Id("employee_password")).SendKeys("blah");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Logout']"))).Displayed);
        }

        // [TearDown]
        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        //[Test]
        public void sleeper()
        {
            Sleeper.sleep(3000);
        }

        
    }
}