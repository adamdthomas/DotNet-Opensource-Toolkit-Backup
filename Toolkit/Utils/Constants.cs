namespace Orasi.Toolkit.Utils
{ /// <summary>
  /// Class to store Constant fields
  /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Default Selenium implicit wait time in seconds</summary>
        public static int DEFAULT_GLOBAL_DRIVER_TIMEOUT { get { return 30; } }

        /// <summary>
        /// Default timeout in seconds for page/DOM/transitions</summary>
        public static int DEFAULT_PAGE_TIMEOUT { get { return 15; } }

        /// <summary>
        /// Default timeout in seconds for finding web elements on a page</summary>
        public static int DEFAULT_ELEMENT_TIMEOUT { get { return 5; } }

        /// <summary>
        /// Location of the environment URLs properties file</summary> 
        public static string ENVIRONMENT_URL_PATH { get { return "EnvironmentURLs"; } }

    }
}
