using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using Orasi.Toolkit.Test.Utils;


namespace Orasi.Toolkit.Utils
{

    /// <summary>
    /// Class to Handle Windows outside of browser
    /// </summary>
    class WindowHandler
    {
        
        private static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(Constants.DEFAULT_PAGE_TIMEOUT);
        public static string MainWindow;



        /// <summary>
        /// Method to Wait for something to Exist ( I.e Window [By.TagName("title")])
        /// <param name="driver">Webdriver</param>
        /// <param name="ElementMethod">Allows user to input desired method for discovery
        /// </summary>
        public static Boolean WaitUntilExists(IWebDriver driver, By ElementMethod)
        {

            try
            {
                var wait = new WebDriverWait(driver, DefaultTimeout);
                wait.Until(ExpectedConditions.ElementExists(ElementMethod));
            }
            catch (WebDriverTimeoutException wdte)
            {
                return false;
                throw wdte;
            }

            return true;

        }

        //*******************************************************************************************************************

        /// <summary>
        /// Read and write current Window's handle
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static Boolean setCurrentWindow(IWebDriver driver)
        {
            try
            {
                String MainWindow = driver.CurrentWindowHandle;

                //get the collection of all open windows
                ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                foreach (string handle in windowHandles)
                {
                    if (handle != MainWindow)
                    {
                        var newWindowHandle = handle;
                        driver.SwitchTo().Window(newWindowHandle);
                        Console.WriteLine(driver.Title);
                        driver.Close();

                        break;
                    }
                }

            }

            catch (Exception ex)

            {

                Console.WriteLine("Exception ******" + ex.StackTrace);
                return false;
            }
            return true;
        }

        //*******************************************************************************************************************

        /// <summary>
        /// Swap to the Main Window
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>

        public static Boolean SwapToParentWindow(IWebDriver driver, string MainWindow)
        {
            try
            {
                driver.SwitchTo().Window(MainWindow); // switch back to the original window
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception ******" + ex.StackTrace);
                return false;
            }
            return true;
        }

        //*******************************************************************************************************************

        /// <summary>
        /// Terminates the program fed from the user
        /// </summary>
        /// <param name="ProcessName">Name of the Process to be terminated</param>
        /// <returns></returns>
        public static Boolean Killer(string ProcessName)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(ProcessName))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;


            // processes list
            // String Kill = "taskkill /IM ";
            // String processName = "IEDriverServer.exe"; //IE process 
            // String processName = "MicrosoftWebDriver.exe"; //IEEdge process
            // String processName = "chrome.exe"; //Chrome process
            // String processName = "chromedriver.exe"; //Chrome process
            // String processName = "Firefox.exe"; //Firefox process

        }

        internal static object SwapToParentWindow(FirefoxDriver _driver, bool mainWindow)
        {
            throw new NotImplementedException();
        }

        //*******************************************************************************************************************

        /// <summary>
        /// Method opens a link in a new window by finding element and shifts focus to it. '(By.Id("")
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <param name="URL">Desired page URL</param>
        /// <param name="WindowTitle"> Title to be expected for expectedNewWindowTitle</param>
        /// <param name="ElementMethod">Allows user to input desired method for discovery</param>
        /// <param name="LinkElement">Defines Element to Wait for in OpenPage</param>
        /// <returns></returns>
        public static bool SwapToNewWindow(IWebDriver driver, string URL, string WindowTitle, By LinkElement, string LogInfo2 = null, string LogInfo3 = null, string LogInfo4 = null)
        {
           
                
            try
            {
                //Navigate to URL 
                driver.Navigate().GoToUrl(URL);
                driver.Manage().Window.Maximize();

                //Get Parent Window Handle
                string parentWindow = driver.CurrentWindowHandle;

                string newWindow = "";
                string expectedNewWindowTitle = WindowTitle;

                Thread.Sleep(2000); //Static wait is not recommended
                WindowHandlerTest.test.Log(LogStatus.Info, (LogInfo2 + " " + driver.Title));

                //Click on the link to open new window
                IWebElement OpenPage = driver.FindElement(LinkElement);
                OpenPage.Click();

                Thread.Sleep(2000); //Static wait is not recommended

                //Store all window handles in a list
                IList<string> allWindowHandles = driver.WindowHandles;

                //If allWindowHandles.Count is greater than 1 then you can say that new window has been opened.
                if (allWindowHandles.Count > 1)
                {
                    WindowHandlerTest.test.Log(LogStatus.Info, (LogInfo3));
                }

                //Get new window handle
                for (int i = 0; i < allWindowHandles.Count; i++)
                {
                    if (allWindowHandles[i] != parentWindow)
                    {
                        newWindow = allWindowHandles[i];
                    }
                }

                //Switch to new window handle.
                driver.SwitchTo().Window(newWindow);

                WindowHandlerTest.test.Log(LogStatus.Info, (LogInfo4 + " " + driver.Title));

                //You can verify the title of new window to verify whether is in focus or not.
                Assert.AreEqual(expectedNewWindowTitle, driver.Title);
            }
            catch (NoSuchWindowException nswe)
            {
                throw nswe;
            }
            return false;
        }
    }
}
