using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Orasi.Toolkit.Utils;
using RelevantCodes.ExtentReports;

namespace Orasi.Toolkit.Test.Utils
{
    class Base64EncoderTest
    {
        private ExtentReports extent = ExtentManager.Instance;
        private ExtentTest test;

        [Test]
        public void Encode()
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
               .StartTest("Encode", "An Encoding Test");

            try
            {
                Assert.True(Base64Encoder.Encode("String to encode").Equals("U3RyaW5nIHRvIGVuY29kZQ=="));
                test.Log(LogStatus.Pass, "String Encoded");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre");
            }

        }

        [Test]
        public void Decode()
        {
            extent.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "../../extent-config.xml");
            test = extent
             .StartTest("Decode", "A Decoding  Test");
            try
            {
                Assert.True(Base64Encoder.Decode("U3RyaW5nIHRvIGVuY29kZQ==").Equals("String to encode"));
                test.Log(LogStatus.Pass, "String was decoded");
            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre");
            }
        }
        [TearDown]
        //[OneTimeTearDown]
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
