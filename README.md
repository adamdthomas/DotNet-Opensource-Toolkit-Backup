# DotNet-Opensource-Toolkit

# Orasi Software Inc
Orasi is a software and professional services company focused on software quality testing and management.  As an organization, we are dedicated to best-in-class QA tools, practices and processes. We are agile and drive continuous improvement with our customers and within our own business.

# License
Licensed under [BSD License](https://github.com/Orasi/DotNet-Opensource-Toolkit/blob/master/LICENSE)

# Setup
* Install [Visual Studio 2015](https://www.visualstudio.com/downloads/download-visual-studio-vs)
* Go to 'Tools>Extensions and Updates' and install `NUnit3 Test Adapter`

##Creating a Solution
* Create a new C# Project.  The exact project type doesn't really matter since you can add or remove packages later.
* Go to 'Tools>NuGet Package Manager>Manage NuGet Packages for Solution' and install the following packages:
```
NUnit (Latest stable 3.0.1)
GitHub Extension for Visual Studio
NUnit3 Test Adapter
```
Properties of package items should be set to `Copy if newer` to enable running on any system

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
# Jenkins setup for C# ExtentReports
* Manage Plugins
* Set Slave
* Configure System Setting
* Configure Build





### Manage Jenkins Plugins	
	There are a variety of Jenkins plugins that are quite useful (and fun), but only a few that are required
to utilize Extent Reports.

* On a Jenkins administrative account, go to `Manage Jenkins > Manage Plugins > Available`
* In the `Filter` box in the top right corner enter the following and click the checkbox for each:
	- Git 
	- GitHub (Generate Token on GitHub including admin:repo_hook, repo)
	- HTML Publisher 
	- MSBuild 
	- MSTestRunner 
	- NUnit 
	- (optional) ChuckNorris
	- (optional) Emotional Jenkins

### Configure System 

* Install Git on master and slave machines and note location of `git.exe`
* Ensure proper .Net versions are installed on Windows Slave and note location of `MSBuild.exe` for each version.

* `Manage Jenkins > Configure System > Git`
	- Click `Add Git > Git` twice
	- In `Name' enter a logical name for each Git instance (i.e. Default/ Windows Git)
	- In `Path to Git executable` enter fully qualified paths for each `git.exe' location. 
		The default for linux is `/usr/bin/git`. The default for Windows is `C:\Program Files\Cmd\git.exe`
	
	* 'MSBuild > MSBuild installations'
		- Click `Add MSBuild' for as many .Net Frameworks available
		- In `Name' enter a logical name for each MSBuild instance (i.e. .Net 3.5/ .Net 4.0.30319)
		- In `Path to MSBuild` enter fully qualified paths for each `MSBuild.exe' location. 
			The default for .Net  4.0.30319 is `C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe`
	
	
	
* `Manage Jenkins > Script Console`
	- Enter `System.setProperty("hudson.model.DirectoryBrowserSupport.CSP", "")` 
		into script console and run to allow permissions for ExtentReports. ***This may need to be done everytime Jenkins restarts!***
		

* extent-config.xml is the configuration file for ExtentReports. 
	More info regarding configuration options are available at http://extentreports.relevantcodes.com/net/docs.html.

	
### Set Up Windows Slave
A Windows Slave is neccesary when running Jenkins on a Linux server in order to run C# appropriately.

* On the master machine, go to `Manage Jenkins > Manage Nodes > New Node`
* Enter Node Name, select `Dumb Slave` and click `OK`
* Enter values for the following:
	- `# of executors` (must be at least one, but can be as many as the number of processors on the Windows system)
	- `Remote root directory` ( May be any directory as desired. I prefer `C:/Slave`)
	- `Usage` Set to `Utilize this node as much as possible`
	- `Launch Method` Set to 'Launch slave agents via Java Web Start`
	- `Availability` Set to `Keep this slave on-line as much as possible`
* Under `Node Properties > Tool Locations` add the fully qualified locations of `git.exe` and each `MSBuild.exe`
	in `Home` under the appropriate `Name`
* Click Save

* Create a user for `slaves` with overall read and all Slave permissions or in slave role

* Connect the slave machine to the master:
	- Open a browser on the slave machine and navigate to the Jenkins master server.
	- Go to `Manage Jenkins > Manage Nodes` and click the new slave machine. Administrative privileges are neccesary.
	- Click the `Launch` button to launch the slave agent.
	- Run the program as Administrator
	- The slave machine should be listed as connected under `Nodes` in the browser.
	- In the slave agent, select `File > Install as Windows Service`
	- Access `Services` program (as Administrator) and locate `Jenkins Slave` in the list and double-click to open
	- Enter `This service runs a slave for Jenkins continuous integration system.` in description and set 
		`Startup type` to `Automatic`
	- Go to the `Log On` tab and change the `Log on as:` to a user of choice.
	- Click `OK`



### Configure Build

* Click `New Item` and enter a name for the project
* Select `Freestyle project`
* Click `OK`
* On the Project configuration page:
	- Select `GitHub project` and enter the project URL (https://github.com/User/Project/)
	- Select `Restrict where this project can be run` and enter the name of the slave
* Under `Source Code Management` 
	- Select `Git` and enter the same repository url as above.
	- Select your credentials for GitHub. **If Credentials have not been created come back after you have created them.
	- Select the `Git Executable` for the Windows slave
	- Click `Add` and select `Force polling using workspace`
* Under `Build Triggers` select `Build when a change is pushed to GitHub` and `Poll SCM` 
	- Enter a schedule (ex. checks for changes every 15 minutes `H/15 * * * *`)
* Under `Build` click `Add build step` and select `Build a Visual Studio project...`
	 	and again for `Execute Shell`

* `Build a Visual Studio project or solution using MSBuild`
	- Select the .Net version that relates to your project.
	- Enter the location of the .sln or. proj from the root directory (ex: "Toolkit/Toolkit.sln")
	- In `Command Line Arguments` specify the command-line arguments 
		(ex  /p:Configuration=Debug /p:Platform="Any CPU")
* `Execute Shell` 
	- Use command line arguments to tell Jenkins to use the NUnit console application on the .dll from the project
```
	#!/bin/sh
	"C:\dotnet_Selenium\dotnet_Selenium\SeleniumToolkit\packages\NUnit.Console.3.0.1\tools\nunit3-console.exe"
	"C:\Slave\test\Toolkit\bin\Debug\Orasi.Toolkit.dll" 
```
* Under `Post-build Actions` click `Add post-build action` and select `Publish HTML reports`
	- `HTML directory to archive` is the location where the .dll project file is located in relation to the linux workspace
		(`\Toolkit\bin\Debug`)
	- `Index page[s]` is where you input the savename of your report file (`ExtentReport.html`)
	- `Report Title` is where you input the desired title for your Report (`Dot Net Orasi Toolkit`)
	- Click `Publishing options...` and select `Keep past HTML reports`
* Emotional Jenkins and Activate Chuck Norris are available in the Post-Build section and should be enabled if desired.
