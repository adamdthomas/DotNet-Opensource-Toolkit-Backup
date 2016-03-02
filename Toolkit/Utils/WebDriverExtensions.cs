using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Diagnostics;

namespace Orasi.Toolkit.Utils
{
    namespace WebDriverExtensions
    {
        public static class WebElementExtensions
        {
            /// <summary>
            /// Finds an element
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="by"></param>
            /// <returns></returns>
            public static bool ElementIsPresent(this IWebDriver driver, By by)
            {
                try
                {
                    return driver.FindElement(by).Displayed;
                }
                catch (NoSuchElementException nsee)
                {
                    return false;
                    throw nsee;
                }
            }
            /// <summary>
            /// Waits until an element is present until timeout
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="by"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            public static bool WaitUntilElementIsPresent(this IWebDriver driver, By by, int timeout = 30)
            {
                Debug.WriteLine("WUEIP Start");
                for (var i=0; i < timeout; i++)
                {
                    if (driver.ElementIsPresent(by)) return true;
                    Thread.Sleep(1000);
                }
                return false;
                //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                //return wait.Until(d => d.ElementIsPresent(by));
            }
        }
    }
}
