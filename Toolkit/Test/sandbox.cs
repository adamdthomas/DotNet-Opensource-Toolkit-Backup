using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Toolkit.Utils;

namespace Toolkit
{
    [TestClass]
    public class Sandbox

    {
        FirefoxDriver _driver;
        [TestMethod]
        public void TestMethod1()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");
            _driver.FindElement(By.Id("employee_username")).SendKeys("company.admin");
            _driver.FindElement(By.Id("employee_password")).SendKeys("blah");
            _driver.FindElement(By.XPath("//input[@value='Login']")).Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            AlertHandler.handleAlert(_driver,3);
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Logout']"))).Displayed);
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}