using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;

namespace Orasi.Toolkit.Utils
{
    [TestFixture, Category("Unit")]
    
    public class TestSetup
    {

        public static ExtentReports extent = ExtentManager.Instance;
        public static ExtentTest test;
        public static FirefoxDriver _driver;
        public static Action action;
        public static WebDriverWait wait;
        //public static TopNavigation = new TopNavigation;
        [OneTimeSetUp]
        
        /// <summary>
        /// Setup for all test starts. 
        /// </summary>
        ///
        public static void TestStartup(String TestName, String TestDescription, String TestCategory, String TestStartInfo)
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");

             test = extent
               .StartTest(TestName, TestDescription)
               .AssignCategory(TestCategory);
            test.Log(LogStatus.Info, TestStartInfo);

            IWebDriver WebDriver = null;

            try
            {
                System.Uri uri = new System.Uri("http://localhost:7055/hub");
                WebDriver = new RemoteWebDriver(uri, DesiredCapabilities.Firefox());
                Console.WriteLine("Executed on remote driver");

            }

            catch (Exception)
            {

                WebDriver = new FirefoxDriver();
                Console.WriteLine("Executed on New FireFox driver");

            }
 //           return true;
        }
    }
}