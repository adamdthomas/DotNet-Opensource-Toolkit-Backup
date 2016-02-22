# DotNet-Opensource-Toolkit

# Orasi Software Inc
Orasi is a software and professional services company focused on software quality testing and management.  As an organization, we are dedicated to best-in-class QA tools, practices and processes. We are agile and drive continuous improvement with our customers and within our own business.

# License
Licensed under [BSD License](https://github.com/Orasi/DotNet-Opensource-Toolkit/blob/master/LICENSE)

## Setup
* Install [Visual Studio 2015](https://www.visualstudio.com/downloads/download-visual-studio-vs)
* Go to 'Tools>Extensions and Updates' and install `NUnit3 Test Adapter`

##Creating a Solution
* Create a new C# Project.  The exact project type doesn't really matter since you can add or remove packages later.
* Go to 'Tools>NuGet Package Manager>Manage NuGet Packages for Solution' and install the following packages:
```
NUnit (Latest stable 3.0.1)
```

## Creating Test
* Create a new Class in your Project for your tests.  Add `using System`, `using NUnit.Framework`, and `RelevantCodes.ExtentReports` to the top of the class file.
* Add `[TestFixture]` as a header for your class
* Instantiate `private ExtentReports extent` and `private ExtentTest test`
* Add `[Test]` as a header to each of the methods you want to use as a test.
* Initialize test
* Start Test
* Add `[TearDown]` header to close class and flush tests.
* Your Test class should look something like this:
```
using System;
using NUnit.Framework;
using RelevantCodes.ExtentReports;

  [TestFixture]
  public class MyTests{
      private ExtentReports extent = ExtentManager.Instance;
      private ExtentTest test;
      
      [Test]
      public void SearchandBookFlight() {
          //Initializing Test
          test = extent
              .StartTest("Test Name", "Test Description")
              .AssignCategory("Test Category");
              
          // test.Log(Logstatus.Info, "Any information can be passed at anytime in this manner."
          
          try
          {
              //Test Actions/Calls
              test.Log(LogStatus.Pass, "Passed Message"
          }
          catch (Exception ex) //Exception types can be used instead of the basic exception, offering the possibility of more details
          {
              test.Log(LogStatus.Fail, "<pre>" + ex.StackTrace + "</pre>");
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
            _driver.Quit();
        }
  }
```
```
