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
        * Selenium Hub Field
        */
        protected string seleniumHubURL = "http://10.238.242.50:4444/wd/hub";

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

        //public int Timeout
        //{
        //    get
        //    {
        //        System
        //        // TODO System.setProperty(Constants.TEST_DRIVER_TIMEOUT, Integer.toString(timeout));
        //    }
        //    set
        //    {
        //        // TODO return Integer.parseInt(System.getProperty(Constants.TEST_DRIVER_TIMEOUT));
        //    }
        //}

        //public string RemoteURL
        //{
        //    get
        //    {
        //        if (getRunLocation().equalsIgnoreCase("sauce") | getRunLocation().equalsIgnoreCase("remote"))
        //            return sauceLabsURL;
        //        else if (getRunLocation().equalsIgnoreCase("grid"))
        //            return seleniumHubURL;
        //        else
        //            return "";
        //    }
        //}
    }
}