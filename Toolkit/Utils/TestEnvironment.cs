using System;
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
using System.Threading;
using System.Resources;
using SaucelabsApiDotNet;
using System.IO;

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
        private string environment;
        private string testName;
        private string pageUrl;

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
                return runLocation;
            }
            set
            {
                runLocation = value;
            }
        }

        public string Environment
        {
            get
            {
                return environment;
            }
            set
            {
                environment = value;
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

        public int Timeout
        {
            get
            {
                return Constants.DEFAULT_GLOBAL_DRIVER_TIMEOUT;
            }
            set
            {
                int timeout = value;
            }
        }

        public string RemoteURL
        {
            get
            {
                if (RunLocation.Equals("sauce") | RunLocation.Equals("remote"))
                    return  SauceLabsURL;
                else if (RunLocation.Equals("grid"))
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
            if (Environment == null)
            {
                launchApplication((Application.ToString().ToUpper()));
            }
            else
            {
                launchApplication(Application.ToString().ToUpper() + "_" + Environment.ToString().ToUpper());
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
            if (RunLocation.ToUpper().Equals("LOCAL"))
            {
                //File file = null;

                switch (OperatingSystem.ToUpper().Trim().Replace(" ","")
                {
                    case "WINDOWS":
                        if (getBrowserUnderTest().equalsIgnoreCase("Firefox") || getBrowserUnderTest().equalsIgnoreCase("FF"))
                        {
                            caps = DesiredCapabilities.firefox();
                        }

                        // Internet explorer
                        else if (BrowserUnderTest.ToUpper().Equals("IE")
                              || BrowserUnderTest.Replace(" ","").ToUpper().Equals("INTERNETEXPLORER"))
                        {
                            caps = DesiredCapabilities.internetExplorer();
                            caps.setCapability("ignoreZoomSetting", true);
                            caps.setCapability("enablePersistentHover", false);
                            File file = new File(
                                    this.getClass().getResource(Constants.DRIVERS_PATH_LOCAL + "IEDriverServer.exe").getPath());
                            System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
                            caps = DesiredCapabilities.InternetExplorer();

                        }
                        // Chrome
                        else if (getBrowserUnderTest().equalsIgnoreCase("Chrome"))
                        {
                            file = new File(
                                    this.getClass().getResource(Constants.DRIVERS_PATH_LOCAL + "ChromeDriver.exe").getPath());
                            System.setProperty("webdriver.chrome.driver", file.getAbsolutePath());
                            caps = DesiredCapabilities.chrome();

                        }
                        // Headless - HTML unit driver
                        else if (getBrowserUnderTest().equalsIgnoreCase("html"))
                        {
                            caps = DesiredCapabilities.htmlUnitWithJs();
                        }
                        /*
                         * else if (getBrowserUnderTest().equalsIgnoreCase("phantom")) {
                         * caps = DesiredCapabilities.phantomjs();
                         * caps.setCapability("ignoreZoomSetting", true);
                         * caps.setCapability("enablePersistentHover", false); file =
                         * new File(this.getClass().getResource(Constants.
                         * DRIVERS_PATH_LOCAL+ "phantomjs.exe").getPath());
                         * caps.setCapability(PhantomJSDriverService.
                         * PHANTOMJS_EXECUTABLE_PATH_PROPERTY , file.getAbsolutePath());
                         * driver = new PhantomJSDriver(caps);
                         * 
                         * }
                         */
                        // Safari
                        else if (getBrowserUnderTest().equalsIgnoreCase("safari"))
                        {
                            caps = DesiredCapabilities.safari();

                        }
                        else if (getBrowserUnderTest().equalsIgnoreCase("microsoftedge"))
                        {
                            file = new File(this.getClass().getResource(Constants.DRIVERS_PATH_LOCAL + "MicrosoftWebDriver.exe")
                                    .getPath());
                            System.setProperty("webdriver.edge.driver", file.getAbsolutePath());
                            caps = DesiredCapabilities.edge();
                        }
                        else
                        {
                            throw new RuntimeException("Parameter not set for browser type");
                        }
                        break;
                    case "mac":
                    case "macos":
                        if (getBrowserUnderTest().equalsIgnoreCase("Firefox") || getBrowserUnderTest().equalsIgnoreCase("FF"))
                        {
                            caps = DesiredCapabilities.firefox();

                        }
                        // Internet explorer
                        else if (getBrowserUnderTest().equalsIgnoreCase("IE")
                                || getBrowserUnderTest().replace(" ", "").equalsIgnoreCase("internetexplorer"))
                        {
                            throw new RuntimeException("Currently there is no support for IE with Mac OS.");
                        }
                        // Chrome
                        else if (getBrowserUnderTest().equalsIgnoreCase("Chrome"))
                        {
                            file = new File(
                                    this.getClass().getResource(Constants.DRIVERS_PATH_LOCAL + "mac/chromedriver").getPath());
                            System.setProperty("webdriver.chrome.driver", file.getAbsolutePath());
                            try
                            {
                                // Ensure the permission on the driver include
                                // executable permissions
                                Process proc = Runtime.getRuntime()
                                        .exec(new String[] { "/bin/bash", "-c", "chmod 777 " + file.getAbsolutePath() });
                                proc.waitFor();
                                caps = DesiredCapabilities.chrome();

                            }
                            catch (IllegalStateException ise)
                            {
                                ise.printStackTrace();
                                throw new IllegalStateException(
                                        "This has been seen to occur when the chromedriver file does not have executable permissions. In a terminal, navigate to the directory to which Maven pulls the drivers at runtime (e.g \"/target/classes/drivers/\") and execute the following command: chmod +rx chromedriver");
                            }
                            catch (IOException ioe)
                            {
                                ioe.printStackTrace();
                            }
                            catch (InterruptedException ie)
                            {
                                ie.printStackTrace();
                            }
                        }
                        // Headless - HTML unit driver
                        else if (getBrowserUnderTest().equalsIgnoreCase("html"))
                        {
                            caps = DesiredCapabilities.htmlUnitWithJs();

                        }
                        // Safari
                        else if (getBrowserUnderTest().equalsIgnoreCase("safari"))
                        {
                            caps = DesiredCapabilities.safari();
                        }
                        else
                        {
                            throw new RuntimeException("Parameter not set for browser type");
                        }
                        break;
                    case "linux":
                        if (getBrowserUnderTest().equalsIgnoreCase("html"))
                        {
                            caps = DesiredCapabilities.htmlUnitWithJs();
                        }
                        else if (getBrowserUnderTest().equalsIgnoreCase("Firefox")
                              || getBrowserUnderTest().equalsIgnoreCase("FF"))
                        {
                            caps = DesiredCapabilities.firefox();
                        }
                    default:
                        break;
                }

                setDriver(new OrasiDriver(caps));
                // Code for running on the selenium grid
            }
            else if (getRunLocation().equalsIgnoreCase("grid"))
            {
                caps.setCapability(CapabilityType.BROWSER_NAME, getBrowserUnderTest());
                if (getBrowserVersion() != null)
                {
                    caps.setCapability(CapabilityType.VERSION, getBrowserVersion());
                }

                caps.setCapability(CapabilityType.PLATFORM, getGridPlatformByOS(getOperatingSystem()));
                if (getBrowserUnderTest().toLowerCase().contains("ie")
                        || getBrowserUnderTest().toLowerCase().contains("iexplore"))
                {
                    caps.setCapability("ignoreZoomSetting", true);
                }

                try
                {
                    setDriver(new OrasiDriver(caps, new URL(getRemoteURL())));
                }
                catch (MalformedURLException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }

            }
            else if (getRunLocation().equalsIgnoreCase("remote") | getRunLocation().equalsIgnoreCase("sauce"))
            {

                caps = new DesiredCapabilities();
                caps.setCapability(CapabilityType.BROWSER_NAME, getBrowserUnderTest());
                if (getBrowserVersion() != null)
                {
                    caps.setCapability(CapabilityType.VERSION, getBrowserVersion());
                }
                caps.setCapability(CapabilityType.PLATFORM, getOperatingSystem());

                if (getBrowserUnderTest().toLowerCase().contains("ie")
                        || getBrowserUnderTest().toLowerCase().contains("iexplore"))
                {
                    caps.setCapability("ignoreZoomSetting", true);
                }
                caps.setCapability("name", getTestName());
                URL sauceURL = null;
                try
                {
                    sauceURL = new URL("http://" + authentication.getUsername() + ":" + authentication.getAccessKey()
                            + "@ondemand.saucelabs.com:80/wd/hub");
                }
                catch (MalformedURLException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }

                caps.setCapability("name", getTestName());
                setDriver(new OrasiDriver(caps, sauceURL));

            }
            else
            {
                throw new RuntimeException(
                        "Parameter for run [Location] was not set to 'Local', 'Grid', 'Sauce', or 'Remote'");
            }

            getDriver().setElementTimeout(Constants.ELEMENT_TIMEOUT);
            getDriver().setPageTimeout(Constants.PAGE_TIMEOUT);
            getDriver().setScriptTimeout(Constants.DEFAULT_GLOBAL_DRIVER_TIMEOUT);
            // setDefaultTestTimeout(Constants.DEFAULT_GLOBAL_DRIVER_TIMEOUT);
            if (!getBrowserUnderTest().toLowerCase().contains("edge"))
            {
                getDriver().manage().deleteAllCookies();
                getDriver().manage().window().maximize();
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
            if (getPageURL().isEmpty())
                launchApplication();
            else
                launchApplication(getPageURL());
            return getDriver();
        }

        protected void endTest(string testName)
        {
            if (getDriver() != null && getDriver().getWindowHandles().size() > 0)
            {
                getDriver().Quit;
            }
        }

        /*
         * Use ITestResult from @AfterMethod to determine run status before closing
         * test if reporting to sauce labs
         */
        protected void endTest(String testName, ITestResult testResults)
        {
            if (runLocation.equalsIgnoreCase("remote") | runLocation.equalsIgnoreCase("sauce"))
            {
                endSauceTest(testResults.getStatus());
            }

            endTest(testName);
        }
    }
}