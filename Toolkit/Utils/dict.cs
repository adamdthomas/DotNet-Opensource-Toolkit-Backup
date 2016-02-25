using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using System;
using NUnit.Framework.Interfaces;

namespace Orasi.Toolkit.Utils
{
    public class dict
    {
        private static ExtentReports extent = ExtentManager.Instance;
        private static ExtentTest test;
        

        /*
        public void tryCatch(string StartTitle, string StartDescription, string CatName, Action MethodInput, string PassLog, string LogInfo1, string LogInfo2, string LogInfo3, string LogInfo4, string LogInfo5)
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
               .StartTest(StartTitle, StartDescription)
               .AssignCategory(CatName);
            test.Log(LogStatus.Info, LogInfo1);


            try
            {
                MethodInput();
                test.Log(LogStatus.Pass, PassLog);
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }
        */
        internal static void TryCatch(string StartTitle, string StartDescription, string CatName, ref object MethodInput, string PassLog, string LogInfo1 =default(string), string LogInfo2 = default(string), string LogInfo3 = default(string), string LogInfo4 = default(string), string LogInfo5 = default(string))
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

        }
    }
}
