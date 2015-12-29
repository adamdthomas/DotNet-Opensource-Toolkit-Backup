using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Orasi.Toolkit.Utils
{
    /// <summary>
    /// Class to help handle Alerts 
    /// </summary>
    class AlertHandler
    {
        /// <summary>
        /// Method to handle all popups that appear</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="timeout">Number of seconds to wait</param> 
        /// <returns>true if a popup was handled successfully, false if no popup was present to handle</returns>
        public static Boolean HandleAllAlerts(IWebDriver driver, double timeout)
        {
            Boolean foundAlert = false;
            while (IsAlertPresent(driver,timeout)) {
                HandleAlert(driver, timeout);
                foundAlert = true;
            }
            return foundAlert;
        }

        /// <summary>
        /// Method to handle a single popup</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="timeout">Number of seconds to wait</param> 
        /// <returns>true if a popup was handled successfully, false if no popup was present to handle</returns>
        public static Boolean HandleAlert(IWebDriver driver, double timeout)
        {
            Boolean found = false;
            try
            {
                if (IsAlertPresent(driver, timeout)) {                 
                    driver.SwitchTo().Alert().Accept();
                    found = true;
                }
            }
            catch (NoAlertPresentException nape) { }

            return found;
        }

        /// <summary>
        /// Method see if any popups are present</summary>
        /// <param name="driver">Webdriver</param>
        /// <param name="timeout">Number of seconds to wait</param> 
        /// <returns>true if popup was present, false if no popup was found</returns>

        public static Boolean IsAlertPresent(IWebDriver driver, double timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (WebDriverTimeoutException wdte)
            {
                return false;
            }

            return true;
        }
    }
}
