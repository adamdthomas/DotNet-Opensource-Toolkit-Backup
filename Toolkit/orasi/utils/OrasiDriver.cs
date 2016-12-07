using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.PhantomJS;

using OpenQA.Selenium.Remote;
using Orasi.Toolkit.orasi.utils;

namespace Orasi.Toolkit.orasi.core.Internal
{
    public class OrasiDriver
    {
        private IWebDriver driver;

        public OrasiDriver(TestEnvironment env)
        {
            //At a minimum, the dirver will need to be instantiated with a browser type. 
            //As the OrasiDriver matures, we will need to account for capabilities and other browser/device/location settings. E.g. mobile and grid. 
            switch (env.browserUnderTest)
            {
                case "ff":
                case "firefox":
                case "fire fox":
                case "fox":
                case "fire":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                case "ch":
                case "chromium":
                    driver = new ChromeDriver();
                    break;
                case "ie":
                case "internet explorer":
                case "internetexplorer":
                case "explorer":
                    driver = new InternetExplorerDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                case "phantomjs":
                case "phantom":
                case "headless":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
        }



        private void setDriverWithCapabilties(DesiredCapabilities caps)
        {
            
        }
        



    }
}
