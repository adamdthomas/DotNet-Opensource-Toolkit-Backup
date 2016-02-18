using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;

public class ExtentManager
{
    private static ExtentReports extent;
    public static ExtentReports Instance
    {
        get
        {
            if (extent == null)
            {
                extent = new ExtentReports(@"file-path", true);
            }
            return extent;
        }
    }
}
namespace Orasi.Toolkit
{

    [TestFixture]
    public class Sandbox
    {
        private ExtentReports extent = ExtentManager.Instance;
        private ExtentTest test;
        FirefoxDriver _driver;
        [Test]
        public void SampleLoginTest()
        {
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
            extent.EndTest(test);
            extent.Flush();
            _driver.Quit();
        }

        //[Test]
        public void sleeper()
        {
            Sleeper.sleep(3000);
        }

        //public void exceldocumentreader()
        //{
        //    var edr = new Excel();

        //    edr.ReadData(@"C:\Users\Paul\Documents\test.xls", "panther");
        //}
    }
}