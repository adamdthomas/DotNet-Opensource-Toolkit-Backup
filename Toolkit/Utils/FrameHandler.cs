using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Orasi.Toolkit.Utils
{   /// <summary>
    /// Class to help handle Frames
    /// </summary>
    class FrameHandler
    {
        private static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(Constants.DEFAULT_PAGE_TIMEOUT);

        /// <summary>
        /// Method get the current frame name</summary>
        /// <param name="driver">Webdriver</param>
        /// <returns>If successful then return the current frames name. Otherwise return null</returns>
        public static string GetCurrentFrameName(IWebDriver driver)
        {
            string framename = (driver as IJavaScriptExecutor).ExecuteScript("return self.name").ToString();
            if(framename == null || framename == String.Empty)
            {
                return null;
            }
            return framename;
        }

        /// <summary>
        /// Simple helper to move to default context</summary>
        /// <param name="driver">Webdriver</param>
        public static void MoveToDefaultContext(IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Simple helper to move to the current frames parent</summary>
        /// <param name="driver">Webdriver</param>
        public static void MoveToParentFrame(IWebDriver driver)
        {
            driver.SwitchTo().ParentFrame();
        }

        /// <summary>
        /// Simple helper to move to a frame within the same domain as the current frame</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="frameLocator"> Allow user to locate a frame by what method they choose</param>
        public static void MoveToSiblingFrame(IWebDriver driver, By frameLocator)
        {
            MoveToParentFrame(driver);
            SwitchToFrame(driver, frameLocator);
        }

        /// <summary>
        /// Simple helper to move to a frame that is in the domain of the current frame</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="frameLocator"> Allow user to locate a frame by what method they choose</param>
        public static void MoveToChildFrame(IWebDriver driver, By frameLocator)
        {
            SwitchToFrame(driver, frameLocator);
        }

        /// <summary>
        /// Drill down multiple domains of frames using each By locator in array</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="frameLocator[]"> Allow user to locate a frame by what method they choose</param>
        public static void MoveToChildFrame(IWebDriver driver, By[] frameLocator)
        {
            for (int x = 0; x < frameLocator.Length; x++)
            {
                SwitchToFrame(driver, frameLocator[x]);
            }
        }

        /// <summary>
        /// Internal method that does the frame swap</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="frameLocator"> Allow user to locate a frame by what method they choose</param>
        private static void SwitchToFrame(IWebDriver driver, By frameLocator) 
        {
            try {
                var wait = new WebDriverWait(driver, DefaultTimeout);
                wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameLocator));
            }
            catch ( NoSuchFrameException nsfe) {
                throw nsfe;    
            }

        }
    }
}
