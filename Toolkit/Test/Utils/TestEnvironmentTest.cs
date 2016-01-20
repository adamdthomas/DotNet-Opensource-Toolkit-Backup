using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Orasi.Toolkit.Utils;


namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]

    class TestEnvironmentTest
    {
        private string application = "application";
        private string browser = "firefox";
        private string browserVersion = "1";
        private string operatingSystem = "windows";
        private string runLocation = "local";
        private string testingEnvironment = "stage";
        private string testingName = "TestEnvironment";
        private string pageURL = "http://google.com";

        [Test]
        public void testEnvironment()
        {
            TestEnvironment te = new TestEnvironment();
            Assert.NotNull(te);
        }

        [Test]
        public void testEnvironmentWithStringConstructor()
        {
            TestEnvironment te = new TestEnvironment(application, browser, browserVersion, operatingSystem, runLocation, testingEnvironment);
            Assert.NotNull(te);
        }

        [Test]
        public void testEnvironmentWithEnvironmentConstructor()
        {
            TestEnvironment te = new TestEnvironment();
            TestEnvironment te2 = new TestEnvironment(te);

            Assert.NotNull(te2);
        }

        [Test]
        public void applicationUnderTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.Application = application;
            Assert.True(te.Application.Equals(application));
        }

        [Test]
        public void browserUnderTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.BrowserUnderTest = browser;
            Assert.True(te.BrowserUnderTest.Equals(browser));
        }

        [Test]
        public void browserVersionUnderTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.BrowserVersion = browserVersion;
            Assert.True(te.BrowserVersion.Equals(browserVersion));
        }

        [Test]
        public void operatingSystemUnderTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.OperatingSystem = operatingSystem;
            Assert.True(te.OperatingSystem.Equals(operatingSystem));
        }

        [Test]
        public void runLocationTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.RunLocation = runLocation;
            Assert.True(te.RunLocation.Equals(runLocation));
        }

        [Test]
        public void testEnvironmentTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.TestingEnvironment = testingEnvironment;
            Assert.True(te.TestingEnvironment.Equals(testingEnvironment));
        }

        [Test]
        public void testNameTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.TestName = testingName;
            Assert.True(te.TestName.Equals(testingName));

        }

        [Test]
        public void pageURLTest()
        {
            TestEnvironment te = new TestEnvironment();
            te.PageURL = pageURL;
            Assert.True(te.PageURL.Equals(pageURL));
        }

        [Test]
        public void testStart()
        {
            TestEnvironment te = new TestEnvironment(application, browser, browserVersion, operatingSystem, runLocation, 
                testingEnvironment);
            te.PageURL = pageURL;
            te.testStart(testingName);
            Assert.True(te.getDriver().Title.Equals("Google"));
            te.endTest(testingName);
        }

        //@Test(groups = "regression", dependsOnMethods = "testStart")

        //public void pageLoaded()
        //{
        //TestEnvironment te = new TestEnvironment(application, browserUnderTest, browserVersion, operatingSystem,
        //runLocation, testingEnvironment);
        //te.setPageURL(pageURL);
        //te.testStart(testingName);
        //Assert.assertTrue(te.pageLoaded());
        //}


        //@Test(groups = "regression", dependsOnMethods = "testStart")

        //public void pageLoadedWithElement()
        //{
        //TestEnvironment te = new TestEnvironment(application, browserUnderTest, browserVersion, operatingSystem,
        //runLocation, testingEnvironment);
        //te.setPageURL(pageURL);
        //OrasiDriver driver = te.testStart(testingName);
        //Assert.assertTrue(new PageLoaded(driver).isDomComplete());
        //Assert.assertTrue(PageLoaded.syncPresent(driver, driver.findElement(By.id("text1"))));

        //}    
    }
}
