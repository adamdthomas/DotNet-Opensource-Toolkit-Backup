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
    public class FrameHandlerTest
    {
        private ExtentReports extent = ExtentManager.Instance;
        private ExtentTest test;
        FirefoxDriver _driver;

        [OneTimeSetUp]
        //[TestFixtureSetUp] - obsolete
        public void Startup()
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
               .StartTest("FrameHandlingStartUp", "Open the browser for testing frames")
               .AssignCategory("BrowserOpen");
            test.Log(LogStatus.Info, "First test in 'FrameHandlerTest' series");
            try
            {
                _driver = new FirefoxDriver();
                _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/frameHandler.html");
                test.Log(LogStatus.Pass, "Navigation Successful");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }
               

        [Test]
        public void _01_MoveToChildFrame()
        {
            test = extent
             .StartTest("MoveToChildFrame", "Verifies the link is displayed")
             .AssignCategory("Frames");
            test.Log(LogStatus.Info, "Move to appropriate frame and discover link");

            try
            { 
                FrameHandler.MoveToChildFrame(_driver, By.Name("menu_page"));
                Assert.True(_driver.FindElement(By.Id("googleLink")).Displayed, "Link was not found in 'menu_page'");
                test.Log(LogStatus.Pass, "The link " + "menu_page" + " is displayed ");
        }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }

}

        [Test]
        public void _02_GetCurrentFrameName()
        {
            test = extent
             .StartTest("GetCurrentFrameName ", "Searches for frame name " + "menu_page")
             .AssignCategory("Frames");
            try
            {
                Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("menu_page"), "Frame name was not 'menu_page' ");
                test.Log(LogStatus.Pass, "The frame is titled " + "'menu_page'");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [Test]
        public void _03_GetDefaultContext()
        {
            test = extent
             .StartTest("GetDefaultContent", "Moves to Default Content")
             .AssignCategory("Frames");
            try
            {
                FrameHandler.MoveToDefaultContext(_driver);
                Assert.Null(FrameHandler.GetCurrentFrameName(_driver), "Failed to move to default Content ");
                test.Log(LogStatus.Pass, "Moved properly to default content");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [Test]
        public void _04_MoveToChildFrameArray()
        {
            test = extent
             .StartTest("MovedToChildFrameArray", "Moves into "  + "'main_frame1'")
             .AssignCategory("Frames");
            try
            {
                By locatorParentFrame = By.Name("main_page");
                By locatorChildFrame = By.Name("main_frame1");
            
                FrameHandler.MoveToChildFrame(_driver, new By[] { locatorParentFrame, locatorChildFrame });
                Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("main_frame1"), "Failed to move to 'main_frame1'  ");
                test.Log(LogStatus.Pass, "The current frame is " + "main_frame1");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [Test]
        public void _05_MoveToParentFrame()
        {
            test = extent
             .StartTest("MoveToParentFrame", "Moves to frame " + "'main_page'")
             .AssignCategory("Frames");
            try
            {
                FrameHandler.MoveToParentFrame(_driver);
                Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("main_page"), "Frame name was not 'main_page' ");
                test.Log(LogStatus.Pass, "The current frame is " + "'main_page'");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [Test]
        public void _06_MoveToSiblingFrame()
        {
            test = extent
             .StartTest("MoveToSiblingFrame", " Moves to frame " + "'main_frame2'")
             .AssignCategory("Frames");
            test.Log(LogStatus.Info, "Final Test in 'FrameHandlerTest' series");
            try
            {
                FrameHandler.MoveToChildFrame(_driver, By.Name("main_frame1"));
                FrameHandler.MoveToSiblingFrame(_driver,By.Name("main_frame2"));
                Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("main_frame2"), "Frame name was not 'main_frame2' ");
                test.Log(LogStatus.Pass, "The current frame is " + "'main_frame2'");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [OneTimeTearDown]
        //[TestFixtureTearDown] - obsolete
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
