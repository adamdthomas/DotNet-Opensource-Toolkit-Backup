using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Toolkit.Utils
{
    class AlertHandler
    {

        public static Boolean handleAllAlerts(IWebDriver driver, double timeout)
        {
            Boolean foundAlert = false;
            while (isAlertPresent(driver,timeout)) {
                handleAlert(driver, timeout);
                foundAlert = true;
            }
            return foundAlert;
        }

        public static Boolean handleAlert(IWebDriver driver, double timeout)
        {
            try
            {
                if (isAlertPresent(driver, timeout)) {
                  
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();
                    try
                    {
                        driver.SwitchTo().DefaultContent();
                    }
                    catch (UnhandledAlertException uhae) { }

                }
            }
            catch (NoAlertPresentException nape) {
                return false;
            }

            return true;
        }

        public static Boolean isAlertPresent(IWebDriver driver, double timeout)
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
