using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orasi.Toolkit
{
    [TestFixture]
    public class Sandbox
        {
        FirefoxDriver _driver;
        [Test]
        public void SampleLoginTest()
        {
            exceldocumentreader();
            
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");
            _driver.FindElement(By.Id("employee_username")).SendKeys("company.admin");
            _driver.FindElement(By.Id("employee_password")).SendKeys("blah");
            _driver.FindElement(By.XPath("//input[@value='Login']")).Submit();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Logout']"))).Displayed);
        }

        [TearDown]
        //[OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        //[Test]
        public void sleeper()
        {
            Sleeper.sleep(3000);
        }

        public void exceldocumentreader()
        {
            var edr = new ExcelDocumentReader();

            edr.ReadData("C:\\Users\\Paul\\Documents\\test.xls", "panther");
        }
    }
}