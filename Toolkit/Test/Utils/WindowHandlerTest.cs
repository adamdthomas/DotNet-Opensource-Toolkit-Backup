using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using System;
using NUnit.Framework.Interfaces;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture, Category("Unit")]
    public class WindowHandlerTest
    {

        private ExtentReports extent = ExtentManager.Instance;
        private ExtentTest test;
        FirefoxDriver _driver;

        [OneTimeSetUp]
        public void Startup()
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
               .StartTest("WindowHandlingStartUp", "Open the browser for testing Windows")
               .AssignCategory("BrowserOpen");
            test.Log(LogStatus.Info, "First test in 'WindowHandlerTest' series");
            try
            {
                _driver = new FirefoxDriver();
                test.Log(LogStatus.Pass, "Navigation Successful");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [Test]
        public void ExistTest()
        {
            object J1 = WindowHandler.WaitUntilExists(_driver, By.TagName("title"));
            dict.TryCatch("Does It Exist?", "Testing Existense of a Window", "Windows",ref J1, "Huzzah!", "Test beginning!");
        }







        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
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




        }

    }
}