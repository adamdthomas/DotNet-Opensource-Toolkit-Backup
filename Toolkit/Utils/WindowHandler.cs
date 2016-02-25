using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Utils
{

    /// <summary>
    /// Class to Handle Windows
    /// </summary>
    class WindowHandler
    {
        private static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(Constants.DEFAULT_PAGE_TIMEOUT);

        /// <summary>
        /// Method to Wait for something to Exist ( I.e Window [By.TagName("title")])
        /// <param name="driver">Webdriver</param>
        /// <param name="title">Allows user to input desired method for discovery
        /// </summary>


        public static Boolean WaitUntilExists(IWebDriver driver, By title)
        {

           // string PageTitle = driver.Title;
            try 
            {
                var wait = new WebDriverWait(driver, DefaultTimeout);
                wait.Until(ExpectedConditions.ElementExists(title));
            }
            catch (WebDriverTimeoutException wdte)
            {
                return false;
                throw wdte;
            }

            return true;

           
	    }
        
    }
}
