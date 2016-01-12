using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture, Category("Unit")]
    public class FrameHandlerTest
    {
        FirefoxDriver _driver;

        [OneTimeSetUp]
        //[TestFixtureSetUp] - obsolete
        public void Startup()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/frameHandler.html");
        }

        [Test, Category("Unit")]
        public void _01_MoveToChildFrame()
        {
            FrameHandler.MoveToChildFrame(_driver, By.Name("menu_page"));
            Assert.True(_driver.FindElement(By.Id("googleLink")).Displayed, "Link was not found in 'menu_page'");
        }

        [Test, Category("Unit")]
        public void _02_GetCurrentFrameName()
        {
            Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("menu_page"), "Frame name was not 'menu_page' ");
        }

        [Test, Category("Unit")]
        public void _03_GetDefaultContext()
        {
            FrameHandler.MoveToDefaultContext(_driver);
            Assert.Null(FrameHandler.GetCurrentFrameName(_driver), "Failed to move to default Content ");
        }

        [Test, Category("Unit")]
        public void _04_MoveToChildFrameArray()
        {
            By locatorParentFrame = By.Name("main_page");
            By locatorChildFrame = By.Name("main_frame1");
            FrameHandler.MoveToChildFrame(_driver, new By[] { locatorParentFrame, locatorChildFrame });
            Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("main_frame1"), "Failed to move to 'main_frame1'  ");
        }

        [Test, Category("Unit")]
        public void _05_MoveToParentFrame()
        {
            FrameHandler.MoveToParentFrame(_driver);
            Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("main_page"), "Frame name was not 'main_page' ");
        }

        [Test, Category("Unit")]
        public void _06_MoveToSiblingFrame()
        {
            FrameHandler.MoveToChildFrame(_driver, By.Name("main_frame1"));
            FrameHandler.MoveToSiblingFrame(_driver,By.Name("main_frame2"));
            Assert.True(FrameHandler.GetCurrentFrameName(_driver).Equals("main_frame2"), "Frame name was not 'main_frame2' ");
        }

        [OneTimeTearDown]
        //[TestFixtureTearDown] - obsolete
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}