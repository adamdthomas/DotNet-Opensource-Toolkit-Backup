using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
import java.io.File;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;
import java.util.ResourceBundle;

import org.openqa.selenium.Platform;
import org.openqa.selenium.remote.CapabilityType;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.testng.ITestContext;
import org.testng.ITestResult;

import com.orasi.core.interfaces.Element;
import com.saucelabs.common.SauceOnDemandAuthentication;
import com.saucelabs.saucerest.SauceREST;
*/

using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support;
using System.Threading;
using System.Resources;
using SaucelabsApiDotNet;
using System.IO;
using NPOI.Util;
using System.Diagnostics;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;
using System.Net;

namespace Orasi.Toolkit.Utils
{
    class TestEnvironment
    {
        /* 
        Test Environment Fields
        */
        private string applicationUnderTest;
        private string browserUnderTest;
        private string browserVersion;
        private string operatingSystem;

        private string runLocation;
        private string testEnvironment;
        private string testName;
        private string pageUrl;

        public class Timeout : ITimeouts
        {
            public ITimeouts ImplicitlyWait(TimeSpan timeToWait)
            {
                throw new NotImplementedException();
            }

            public ITimeouts SetPageLoadTimeout(TimeSpan timeToWait)
            {
                throw new NotImplementedException();
            }

            public ITimeouts SetScriptTimeout(TimeSpan timeToWait)
            {
                throw new NotImplementedException();
            }
        }

        /* 
        WebDriver Fields
        */
        private RemoteWebDriver driver;
        private ThreadLocal<RemoteWebDriver> threadedDriver = new ThreadLocal<RemoteWebDriver>();
        private bool setThreadDriver = false;
        private ThreadLocal<string> sessionId = new ThreadLocal<string>();

        /*
        * URL and Credential Repository Field
        */
        //protected ResourceBundle appURLRepository = ResourceBundle.getBundle(Constants.ENVIRONMENT_URL_PATH);
        //TODO: protected ResourceManager appURLRepository = ResourceManager.CreateFileBasedResourceManager(;

        /*
         Sauce Labs Fields
        */

        /**
        * Constructs a {@link com.saucelabs.common.SauceOnDemandAuthentication}
        * instance using the supplied user name/access key. To use the
        * authentication supplied by environment variables or from an external
        * file, use the no-arg
        * {@link com.saucelabs.common.SauceOnDemandAuthentication} constructor.
        */
        string SauceLabsURL = new Client("@ondemand.saucelabs.com:80/wd/hub", Constants.SAUCELABS_USERNAME, Constants.SAUCELABS_KEY).ToString();

        //protected sauc SauceOnDemandAuthentication authentication = new SauceOnDemandAuthentication(
        //Base64Coder.decodeString(appURLRepository.getString("SAUCELABS_USERNAME")),
        //Base64Coder.decodeString(appURLRepository.getString("SAUCELABS_KEY")));

        //protected String sauceLabsURL = "http://" + authentication.getUsername() + ":" + authentication.getAccessKey()
        //+ "@ondemand.saucelabs.com:80/wd/hub";


        /*
        * Constructors for TestEnvironment class
        */
        public string Application
        {
            get
            {
                return applicationUnderTest;
            }
            set
            {
                applicationUnderTest = value;
            }
        }

        public string PageURL
        {
            get
            {
                return pageUrl;
            }
            set
            {
                pageUrl = value;
            }
        }

        public string BrowserUnderTest
        {
            get
            {
                browserUnderTest = browserUnderTest.ToUpper().Replace(" ", "");
                return browserUnderTest;
            }
            set
            {
                browserUnderTest = value;
            }
        }

        public string BrowserVersion
        {
            get
            {
                return browserVersion;
            }
            set
            {
                browserVersion = value;
            }
        }

        public string OperatingSystem
        {
            get
            {
                operatingSystem.ToUpper().Replace(" ", "");
                return operatingSystem;
            }
            set
            {
                operatingSystem = value;
            }
        }

        public string RunLocation
        {
            get
            {
                runLocation.ToUpper();
                return runLocation;
            }
            set
            {
                runLocation = value;
            }
        }

        public string TestingEnvironment
        {
            get
            {
                return testEnvironment;
            }
            set
            {
                testEnvironment = value;
            }
        }

        public string TestName
        {
            get
            {
                return testName;
            }
            set
            {
                testName = value;
            }
        }

        public int GlobalTimeout
        {
            get
            {
                return Constants.DEFAULT_GLOBAL_DRIVER_TIMEOUT;
            }
            set
            {
                int globalTimeout = value;
            }
        }

        public string RemoteURL
        {
            get
            {
                if (RunLocation.Equals("SAUCE") | RunLocation.Equals("REMOTE"))
                    return SauceLabsURL;
                else if (RunLocation.Equals("GRID"))
                    return SeleniumHubURL;
                else
                    return "";
            }
        }

        public string SeleniumHubURL
        {
            get
            {
                return Constants.SELENIUM_HUB_URL;
            }
            set
            {
                string seleniumHubUrl = value;
            }
        }

        // ************************************
        // ************************************
        // ************************************
        // WEBDRIVER SETUP
        // ************************************
        // ************************************
        // ************************************

        /*
         * Getter and setter for the actual WebDriver
         */
        private void setDriver(RemoteWebDriver driverSession)
        {
            if (SetThreadDriver)
                threadedDriver.Value = driverSession;
            else
                driver = driverSession;
        }

        public RemoteWebDriver getDriver()
        {
            if (SetThreadDriver)
                return threadedDriver.Value;
            else
                return driver;
        }

        /*
         * User controls to see the driver to be threaded or not. Only use when
         * using data provider threading
         */
        public bool SetThreadDriver
        {
            get
            {
                return setThreadDriver;
            }
            set
            {
                setThreadDriver = value;
            }
        }

        /*
         * Method to retrive the URL and Credential Repository
         */
        //NEEDED? protected ResourceBundle getEnvironmentURLRepository()
        //{
        //    return appURLRepository;
        //}

        /**
         * Launches the application under test. Gets the URL from an environment
         * properties file based on the application.
         * 
         * @param None
         * @version 12/16/2014
         * @author Justin Phlegar
         * @return Nothing
         */
        // @Step("Launch \"{0}\"")
        private void launchApplication(string URL)
        {
            getDriver().Url = URL;
        }

        private void launchApplication()
        {
            if (TestingEnvironment == null)
            {
                launchApplication((Application.ToString().ToUpper()));
            }
            else
            {
                launchApplication(Application.ToString().ToUpper() + "_" + TestingEnvironment.ToString().ToUpper());
            }
        }

        /**
         * Sets up the driver type, location, browser under test, os
         * 
         * @param None
         * @version 12/16/2014
         * @author Justin Phlegar
         * @return Nothing
         * @throws IOException
         * @throws InterruptedException
         */
        private void driverSetup()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            // If the location is local, grab the drivers for each browser type from
            // within the project
            if (RunLocation.Equals("LOCAL"))
            {
                //File file = null;

                switch (OperatingSystem)
                {
                    case "WINDOWS":
                        if (BrowserUnderTest.Equals("FIREFOX") || BrowserUnderTest.Equals("FF"))
                        {
                            caps = DesiredCapabilities.Firefox();
                        }

                        // Internet explorer
                        else if (BrowserUnderTest.Equals("IE") || BrowserUnderTest.Equals("INTERNETEXPLORER"))
                        {
                            caps = DesiredCapabilities.InternetExplorer();
                            caps.SetCapability("ignoreZoomSetting", true);
                            caps.SetCapability("enablePersistentHover", false);

                            Environment.SetEnvironmentVariable("webdriver.ie.driver", Constants.DRIVERS_PATH_LOCAL + "IEDriverServer.exe");

                        }
                        // Chrome
                        else if (BrowserUnderTest.Equals("CHROME"))
                        {
                            Environment.SetEnvironmentVariable("webdriver.chrome.driver", Constants.DRIVERS_PATH_LOCAL + "ChromeDriver.exe");
                            caps = DesiredCapabilities.Chrome();

                        }
                        // Headless - HTML unit driver
                        else if (BrowserUnderTest.Equals("HTML"))
                        {
                            caps = DesiredCapabilities.HtmlUnitWithJavaScript();
                        }
                        // Safari
                        else if (BrowserUnderTest.Equals("SAFARI"))
                        {
                            caps = DesiredCapabilities.Safari();
                        }
                        else if (BrowserUnderTest.Equals("MICROSOFTEDGE"))
                        {
                            Environment.SetEnvironmentVariable("webdriver.edge.driver", Constants.DRIVERS_PATH_LOCAL + "MicrosoftWebDriver.exe");
                            caps = DesiredCapabilities.Edge();
                        }
                        else
                        {
                            throw new RuntimeException("Parameter not set for browser type");
                        }
                        break;
                    case "mac":
                    case "macos":
                        if (BrowserUnderTest.Equals("FIREFOX") || BrowserUnderTest.Equals("FF"))
                        {
                            caps = DesiredCapabilities.Firefox();

                        }
                        // Internet explorer
                        else if (BrowserUnderTest.Equals("IE")
                                || BrowserUnderTest.Equals("INTERNETEXPLORER"))
                        {
                            throw new RuntimeException("Currently there is no support for IE with Mac OS.");
                        }
                        // Chrome
                        else if (BrowserUnderTest.Equals("CHROME"))
                        {
                            Environment.SetEnvironmentVariable("webdriver.chrome.driver", Constants.DRIVERS_PATH_LOCAL + "mac/chromedriver");
                            try
                            {
                                // Ensure the permission on the driver include
                                // executable permissions
                                Process p = new Process();
                                ProcessStartInfo info = new ProcessStartInfo();
                                info.FileName = @"C:\cygwin\bin\bash.exe";

                                p.StartInfo = info;
                                p.Start();

                                using (StreamWriter sw = p.StandardInput)
                                {
                                    if (sw.BaseStream.CanWrite)
                                    {
                                        sw.WriteLine("-c");
                                        sw.WriteLine("chmod 777 " + Environment.GetEnvironmentVariable("webdriver.chrome.driver"));
                                    }
                                }
                                p.WaitForExit();
                                caps = DesiredCapabilities.Chrome();

                            }
                            catch (UnauthorizedAccessException uae)
                            {
                                Console.WriteLine(uae.StackTrace);
                                throw new UnauthorizedAccessException(
                                        "This has been seen to occur when the chromedriver file does not have executable permissions. In a terminal, navigate to the directory to which Maven pulls the drivers at runtime (e.g \"/target/classes/drivers/\") and execute the following command: chmod +rx chromedriver");
                            }
                            catch (IOException ioe)
                            {
                                Console.WriteLine(ioe.StackTrace);
                            }
                            //catch (InterruptedException ie)
                            //{
                            //    ie.printStackTrace();
                            //}
                        }
                        // Headless - HTML unit driver
                        else if (BrowserUnderTest.Equals("HTML"))
                        {
                            caps = DesiredCapabilities.HtmlUnitWithJavaScript();

                        }
                        // Safari
                        else if (BrowserUnderTest.Equals("SAFARI"))
                        {
                            caps = DesiredCapabilities.Safari();
                        }
                        else
                        {
                            throw new RuntimeException("Parameter not set for browser type");
                        }
                        break;
                    case "linux":
                        if (BrowserUnderTest.Equals("HTML"))
                        {
                            caps = DesiredCapabilities.HtmlUnitWithJavaScript();
                        }
                        else if (BrowserUnderTest.Equals("FIREFOX") || BrowserUnderTest.Equals("FF"))
                        {
                            caps = DesiredCapabilities.Firefox();
                        }
                        break;
                    default:
                        break;
                }

                setDriver(new RemoteWebDriver(caps));
                // Code for running on the selenium grid
            }
            else if (RunLocation.Equals("GRID"))
            {
                caps.SetCapability(CapabilityType.BrowserName, BrowserUnderTest);
                if (BrowserVersion != null)
                {
                    caps.SetCapability(CapabilityType.Version, BrowserVersion);
                }

                caps.SetCapability(CapabilityType.Platform, GridPlatformTypeByOS(OperatingSystem));

                if (BrowserUnderTest.Equals("IE") || BrowserUnderTest.Equals("IEXPLORE"))
                {
                    caps.SetCapability("ignoreZoomSetting", true);
                }

            }
            else if (RunLocation.Equals("REMOTE") | RunLocation.Equals("SAUCE"))
            {

                caps = new DesiredCapabilities();
                caps.SetCapability(CapabilityType.BrowserName, BrowserUnderTest);
                if (BrowserVersion != null)
                {
                    caps.SetCapability(CapabilityType.Version, BrowserVersion);
                }
                caps.SetCapability(CapabilityType.Platform, OperatingSystem);

                if (BrowserUnderTest.Equals("IE") || BrowserUnderTest.Equals("IEXPLORE"))
                {
                    caps.SetCapability("ignoreZoomSetting", true);
                }
                caps.SetCapability("name", TestName);

                Uri uri;
                string someUrl = SauceLabsURL;
                if (!Uri.TryCreate(someUrl, UriKind.Absolute, out uri))
                {
                    // the someUrl string did not contain a valid url 
                    // inform your users about that
                }
                else
                {
                    var request = WebRequest.Create(uri);
                    // ... safely proceed with executing the request
                }

                caps.SetCapability("name", TestName);
                setDriver(new RemoteWebDriver(uri, caps));

            }
            else
            {
                throw new RuntimeException(
                        "Parameter for run [Location] was not set to 'Local', 'Grid', 'Sauce', or 'Remote'");
            }

            // OrasiDriver feature: getDriver().SetElementTimeout(Constants.ELEMENT_TIMEOUT);
            // OrasiDriver feature: getDriver().setPageTimeout(Constants.PAGE_TIMEOUT);
            // OrasiDriver feature: getDriver().setScriptTimeout(Constants.DEFAULT_GLOBAL_DRIVER_TIMEOUT);

            // setDefaultTestTimeout(Constants.DEFAULT_GLOBAL_DRIVER_TIMEOUT);

            if (!BrowserUnderTest.ToUpper().Contains("EDGE"))
            {
                getDriver().Manage().Cookies.DeleteAllCookies();
                getDriver().Manage().Window.Maximize();
            }
        }
        /**
        * Initializes the webdriver, sets up the run location, driver type,
        * launches the application.
        * 
        * @param testName
        *            - Name of the test
        * @version 12/16/2014
        * @author Jessica Marshall
        */
        protected RemoteWebDriver testStart(string testName)
        {
            // Uncomment the following line to have TestReporter outputs output to
            // the console
            //TODO: TestReporter.setPrintToConsole(true);
            TestName = testName;
            driverSetup();
            if (PageURL.Length.Equals(0))
                launchApplication();
            else
                launchApplication(PageURL);
            return getDriver();
        }

        protected void endTest(string testName)
        {
            if (getDriver() != null && getDriver().CurrentWindowHandle.Length > 0)
            {
                getDriver().Close();
            }
        }

        /*
         * Use ITestResult from @AfterMethod to determine run status before closing
         * test if reporting to sauce labs
         */
        protected void endTest(string testName, ITestResult testResults)
        {
            if (RunLocation.Equals("REMOTE") | runLocation.Equals("SAUCE"))
            {
                //endSauceTest(testResults.ResultState);
            }

            endTest(testName);
        }

        // ************************************
        // ************************************
        // ************************************
        // PAGE OBJECT METHODS
        // ************************************
        // ************************************
        // ************************************

        /**
         * loops for a predetermined amount of time (defined by
         * WebDriverSetup.getDefaultTestTimeout()) to determine if the DOM
         * is in a ready-state
         * @return boolean: true is the DOM is completely loaded, false otherwise
         * @param N/A
         */
        // TODO: OrasiDriver Feature
        //public bool pageLoaded()
        //{
        //    return new PageLoaded().isDomComplete(getDriver());
        //}

        /**
         * loops for a predetermined amount of time (defined by
         * OrasiDriver.getElementTimeout()) to determine if the
         * Element is present in the DOM
         * @param element - element with which to determine if a page is loaded
         * @return boolean: true if the element is present in the DOM, false otherwise
         */
        // TODO: needs com.orasi.core.interfaces.Element
        //public bool pageLoaded(Element element)
        //{
        //    return PageLoaded.syncPresent(getDriver(), element);
        //}
        private PlatformType GridPlatformTypeByOS(string os)
        {
            switch (os.ToUpper())
            {
                case "ANDROID":
                    return PlatformType.Android;
                case "WIN8":
                case "WIN8.1":
                case "WINDOWS":
                    return PlatformType.Windows;
                case "XP":
                    return PlatformType.XP;
                case "LINUX":
                    return PlatformType.Linux;
                case "MAC":
                    return PlatformType.Mac;
                default:
                    return PlatformType.Any;
            }
        }
    }
}