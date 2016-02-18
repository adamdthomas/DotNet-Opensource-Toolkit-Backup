using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using System;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]
    public class AlertHandlerTest
    {
        
        private ExtentReports extent = ExtentManager.Instance;
        private ExtentTest test;
        FirefoxDriver _driver;


        [SetUp]
        public void Startup()
        {
            var extent = new ExtentReports(AppDomain.CurrentDomain.BaseDirectory, false);
            test = extent
                .StartTest("Startup", "Opens Firefox browser and navigates to github page");

            test.Log(LogStatus.Info, "Navigating to Github page");
                
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/alertHandler.html");
        }

        [Test]
        public void IsAlertPresent()
        {
            try
            {
                Assert.True(AlertHandler.IsAlertPresent(_driver, 3));
                test.Log(LogStatus.Pass, "The Alert is Present");
            }
            catch (AssertionException ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
            
        }


        [Test]
        public void HandleAlertTest()
        {
            try
            {
                Assert.IsTrue(AlertHandler.HandleAlert(_driver, 3));
                test.Log(LogStatus.Pass, "The Handle Alert is Present");
            }
            catch (AssertionException ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
            
        }

        [Test]
        public void HandleAllAlertTest()
        {
            try
            {
                Assert.IsTrue(AlertHandler.HandleAllAlerts(_driver, 2));
                test.Log(LogStatus.Pass, "The Handle Alerts are Present");
            }
            catch (AssertionException ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
            try
            {
                Assert.IsTrue(_driver.FindElement(By.Id("button")).Enabled);
                test.Log(LogStatus.Pass, "The Button Is Present");
            }
            catch (AssertionException ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
            
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            extent.EndTest(test);
            extent.Flush();
           
        }
    }
}