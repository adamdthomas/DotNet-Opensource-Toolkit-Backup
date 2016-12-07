using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using System;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Support.UI;

namespace Orasi.Toolkit.Utils
{
    public class dict
    {
        private static ExtentReports extent = ExtentManager.Instance;
        private static ExtentTest test;

        internal static void TryCatch(string StartTitle, string StartDescription, string CatName, ref object MethodInput, string PassLog, string LogInfo1 = default(string), string LogInfo2 = default(string), string LogInfo3 = default(string), string LogInfo4 = default(string), string LogInfo5 = default(string))
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
               .StartTest(StartTitle, StartDescription)
               .AssignCategory(CatName);
            test.Log(LogStatus.Info, LogInfo1);


            try
            {
                MethodInput = new object();
                test.Log(LogStatus.Pass, PassLog);
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }

            // Checking to see if this throws
            test.Log(LogStatus.Info, LogInfo2);

        }

    /*    // Test End
        internal static void EndTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
        LogStatus logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = LogStatus.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    break;
            }
    test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

        
        } */

       
    }
}

/*{
    public static bool WaitUntilElementIsPresent(this IWebDriver driver, By by, int timeout = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        return wait.Until(d => d.ElementExists(by));
    }
}*/