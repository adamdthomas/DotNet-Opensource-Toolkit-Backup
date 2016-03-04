using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orasi.Toolkit.Utils
{
    class ChromeWindowHandler
    {

        /*
        //CustomRemoteDriver for Chrome

    public class CustomRemoteWebDriver : RemoteWebDriver
    {
        public static bool newSession = false;

        public static string sessiodIdPath = ConfigurationManager.AppSettings["SessionIdPath"];

        public CustomRemoteWebDriver(Uri remoteAddress, DesiredCapabilities dd)
            : base(remoteAddress, dd)
        {


        }

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                if (!newSession)
                {

                    var sidText = File.ReadAllText(sessiodIdPath);


                    return new Response
                    {
                        SessionId = sidText,

                    };
                }
                else
                {
                    var response = base.Execute(driverCommandToExecute, parameters);
                    File.WriteAllText(sessiodIdPath, response.SessionId);
                    return response;
                }
            }
            else
            {
                var response = base.Execute(driverCommandToExecute, parameters);
                return response;
            }
        }
    }


}




                  [TestInitialize]
                public void MyTestInitialize()
                {
                    if (ConfigurationManager.AppSettings["UseChromeDebugging"] == "true")
                    {

                        var pr = Process.GetProcessesByName("chromedriver");
                        if (pr.Length > 0)
                        {
                            CustomRemoteWebDriver.newSession = false;
                            var driver = new CustomRemoteWebDriver(new Uri("http://localhost:9515"), DesiredCapabilities.Chrome());
                            WebDriver = driver;
                        }

                        else
                        {
                            Process.Start(ConfigurationManager.AppSettings["ChromeDriverPath"]);
                            CustomRemoteWebDriver.newSession = true;
                            var driver = new CustomRemoteWebDriver(new Uri("http://localhost:9515"), DesiredCapabilities.Chrome());
                            WebDriver = driver;

                        }
                    }

                    else
                    {
                        //normal execution
                        WebDriver = new ChromeDriver(DRIVER_PATH);                
                    }


                }

                
        
        [Test]
                public void ChromeTest()
                {
                    ChromeDriver driver = new ChromeDriver();
                    try
                    {
                        driver.Navigate().GoToUrl("http://orasi.github.io/Selenium-Java-Core/sites/unitTests/orasi/utils/frameHandler.html");
                        driver.SwitchTo().Frame("menu_page");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                } */
    }
}
