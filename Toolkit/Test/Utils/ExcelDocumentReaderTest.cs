using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;
using System.Xml;


namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]
    public class ExcelDocumentReaderTest
    {
        private ExtentReports extent = ExtentManager.Instance;
        private ExtentTest test;
        
        [Test]
        public void readDataFilePathAllRows()
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            // Initializing test
            test = extent
                .StartTest("ExcelDocumentReader", "Opens and reads excel datasheet")
                .AssignCategory("ExcelUse");
            test.Log(LogStatus.Info,  "Retrieving xls sheet");
            

            // define test parameters
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "../../test.xls");
            string sheetName = "panther";
            test.Log(LogStatus.Pass, sheetName + "found!");

            test.Log(LogStatus.Info, "Creating a new List");
            try
            {
                // create new List to accept output of GetAllCells()
                List<Dataset> myDataset = new List<Dataset>();
                test.Log(LogStatus.Pass, "List Created");
           

            // create new ExcelDocumentReader object
            var edr = new ExcelDocumentReader();

            // call GettAllCells()
            myDataset = edr.GetAllCells(filePath, sheetName);

            test.Log(LogStatus.Info, "Asserting the data into previously created list");
            // Asserts
            Assert.NotNull(myDataset);
            Assert.True(myDataset.Count == 9);
            Assert.True(myDataset[3].ColValues.Equals("Cam"));
            Assert.True(myDataset[8].ColName.Equals("position"));
            Assert.True(myDataset[0].Column == 0);
                test.Log(LogStatus.Pass, "Data Asserted");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
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

            extent.EndTest(test);
            extent.Flush();
        }
    }
}
