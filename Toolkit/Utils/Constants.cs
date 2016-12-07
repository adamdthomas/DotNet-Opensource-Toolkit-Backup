namespace Orasi.Toolkit.Utils
{ /// <summary>
  /// Class to store Constant fields
  /// </summary>
    public static class Constants
    {
#pragma warning disable CS0649 // Field 'Constants.SAUCELABS_USERNAME' is never assigned to, and will always have its default value null
        internal static string SAUCELABS_USERNAME;
#pragma warning restore CS0649 // Field 'Constants.SAUCELABS_USERNAME' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Constants.SAUCELABS_KEY' is never assigned to, and will always have its default value null
        internal static string SAUCELABS_KEY;
#pragma warning restore CS0649 // Field 'Constants.SAUCELABS_KEY' is never assigned to, and will always have its default value null

#pragma warning disable CS0649 // Field 'Constants.DRIVERS_PATH_LOCAL' is never assigned to, and will always have its default value null
        internal static string DRIVERS_PATH_LOCAL;
#pragma warning restore CS0649 // Field 'Constants.DRIVERS_PATH_LOCAL' is never assigned to, and will always have its default value null
        /// <summary>
        /// Default Selenium implicit wait time in seconds</summary>
        public static int DEFAULT_GLOBAL_DRIVER_TIMEOUT { get { return 30; } }

        /// <summary>
        /// Default timeout in seconds for page/DOM/transitions</summary>
        public static int PAGE_TIMEOUT { get { return 15; } }

        /// <summary>
        /// Default timeout in seconds for finding web elements on a page</summary>
        public static int ELEMENT_TIMEOUT { get { return 5; } }

        /// <summary>
        /// Location of the environment URLs properties file</summary> 
        public static string ENVIRONMENT_URL_PATH { get { return "EnvironmentURLs"; } }
        
        /// <summary>
        /// Selenium Hub Field </summary>
        public static string SELENIUM_HUB_URL { get { return "http://10.238.242.50:4444/wd/hub"; } }




    }
}
