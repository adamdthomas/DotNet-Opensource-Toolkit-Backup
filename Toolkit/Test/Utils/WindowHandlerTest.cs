using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using System;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;


namespace Orasi.Toolkit.Test.Utils
{

    /*    [TestFixture, Category("Unit")]
        public class WindowHandlerTest
        {

            private ExtentReports extent = ExtentManager.Instance;
            public static ExtentTest test;
            FirefoxDriver _driver = new FirefoxDriver();

            [OneTimeSetUp]

            public void Startup()
            {

                extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
                test = extent
                   .StartTest("WindowHandlingStartUp", "Open the browser for testing Windows")
                   .AssignCategory("Windows");
                test.Log(LogStatus.Info, "First test in 'WindowHandlerTest' series");
            }
            */
    /*     .StartTest("WindowHandlingStartUp", "Open the browser for testing Windows")
               .AssignCategory("Windows");
    */
    [TestFixture]
    public class WindowHandlerTest
    {
        static IWebDriver _driver = TestSetup._driver;

        [Test]
        public void OpenBrowser()
        {
            TestSetup.TestStartup("OpenBrowserTest", "Opens the Browser", "Windows", "Test is beginning.Time is :" + DateTime.Now.ToString("h: mm:ss tt"));
            try
            {

                Uri uri = new Uri("http://localhost:/7055/hub");
                _driver = new RemoteWebDriver(uri, DesiredCapabilities.Firefox());
                TestSetup.test.Log(LogStatus.Info, "Executed on remote driver");

            }

            catch (Exception)
            {

                _driver = new FirefoxDriver();
                TestSetup.test.Log(LogStatus.Info, "Executed on New FireFox driver");

            }
        }



        [Test]
        public void ExistTest()
        {



            //object J1 = WindowHandler.WaitUntilExists(_driver, By.TagName("title"));
            dict.TryCatch("WaitUntilExists", "Testing Existense of a Window", "Windows", "Huzzah!", "Test is beginning.Time is :" + DateTime.Now.ToString("h: mm:ss tt"));
        }

        [Test]
        public void SetCurrentWindowTest()
        {
            //object MethodName = WindowHandler.setCurrentWindow(_driver);
            dict.TryCatch("SetCurrentWindowTest", "Test checks the Current Window and prints to the Console", "Windows",  "The Current Window is accurate", "Test is beginning. Time is :" + DateTime.Now.ToString("h:mm:ss tt"));
        }

        [Test]
        public void SwaptoParent()
        {
            var MainWindow = "";
            object MethodName = WindowHandler.SwapToParentWindow(_driver, MainWindow);
            dict.TryCatch("SwapToParentWindowTest", "Test moves to Main Window", "Windows",  "The Current Window is accurate", "Test is beginning. Time is :" + DateTime.Now.ToString("h:mm:ss tt"));


        }

        [Test]
        public void KillTest()
        {
            object MethodName = WindowHandler.Killer("Firefox.exe");
            dict.TryCatch("KillerTest", "Kills process (By Name) that user specifies", "Windows",  "The Current Window is accurate", "Test is beginning. Time is :" + DateTime.Now.ToString("h:mm:ss tt"));
        }

        [Test]
        public void NewWindowTest()
        {
            //var URL = @"http://google.com";
            //var WindowTitle = "Google";
            //var LinkMethod = By.Name("btnI");
            //string LogInfo2 = "Parent Window Title: ";
            //string LogInfo3 = "New window has been opened.";
            //string LogInfo4 = "New Window Title: ";
            //object WinMethodName = WindowHandler.SwapToNewWindow(_driver, URL, WindowTitle, LogInfo2, LogInfo3, LogInfo4);
            dict.TryCatch("NewWindowTest", "Test to open a new Window and change focus to it", "Windows", "Test has reached the new window.", "Test is beginning. Time is :" + DateTime.Now.ToString("h:mm:ss tt"));

        }

        [Test]
        public void NavigateToURL()
        {
            //var URL = @"http://google.com";
            //string WindowTitle = "Google";
            //object MethodName = WindowHandler.NavigateToURL(_driver, URL, WindowTitle);
            dict.TryCatch("NavigateToURLTest", "Test to navigate to a url", "Windows",  "Success");
        }

       




        [OneTimeTearDown]
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

            TestSetup.test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

            TestSetup.extent.EndTest(TestSetup.test);
            TestSetup.extent.Flush();
            _driver.Quit();
        }
    }

}