# DotNet-Opensource-Toolkit

# Orasi Software Inc
Orasi is a software and professional services company focused on software quality testing and management.  As an organization, we are dedicated to best-in-class QA tools, practices and processes. We are agile and drive continuous improvement with our customers and within our own business.

# License
Licensed under [BSD License](https://github.com/Orasi/Ruby-Opensource-Toolkit/blob/master/License)

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
* Create a new Class in your Project for your tests.  Add `using NUnit.Framework` to the top of the class file.
* Add `[TestFixture]` as a header for your class
* Add `[Test]` as a header to each of the methods you want to use as a test.
* Your Test class should look something like this:
```
using NUnit.Framework;
  [TestFixture]
  public class MyTests{
      [Test]
      public void SearchandBookFlight() {
          //Test Actions/Calls
      }
  }
```
