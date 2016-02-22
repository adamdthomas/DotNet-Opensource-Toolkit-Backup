using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;


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
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
                .StartTest("SampleLoginTest", "A Login Test");

            test.Log(LogStatus.Info, "Opening the browser");

            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://bluesourcestaging.heroku.com");

            try
            {
                _driver.FindElement(By.Id("employee_username")).SendKeys("company.admin");
                _driver.FindElement(By.Id("employee_password")).SendKeys("blah");
                _driver.FindElement(By.XPath("//input[@value='Login']")).Submit();
                test.Log(LogStatus.Pass, "Successfully Logged in");
            }
            catch (NoSuchElementException ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
            }


            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(45));
                Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Logout']"))).Displayed);
            }
            catch (AssertionException ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
            }
        }

        [TearDown]
        //[OneTimeTearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            LogStatus logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = LogStatus.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

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
