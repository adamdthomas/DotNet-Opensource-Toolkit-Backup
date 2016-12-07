using Orasi.Toolkit.orasi.core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orasi.Toolkit.orasi.utils
{
    public class TestEnvironment
    {

        public string browserVersion;
        public string environment;
        public string URL;
        public string runLocation;
        public string browserUnderTest;
        public string operatingSystem;

        public TestEnvironment()
        {
        }

        public TestEnvironment(string BrowserUnderTest)
        {
            browserUnderTest = BrowserUnderTest;
        }

        public TestEnvironment(string BrowserUnderTest, string URL)
        {
            browserUnderTest = BrowserUnderTest;
            this.URL = URL;
        }


    }
}
