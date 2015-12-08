using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Toolkit
{
    [TestClass]
    public class FirstTest
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
            IWebElement logout = wait.Until(d => d.FindElement(By.XPath("//a[text()='Logout']")));
            Assert.IsTrue(logout.Displayed);
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}