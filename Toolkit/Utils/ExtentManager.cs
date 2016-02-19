using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using System.Xml;

namespace Orasi.Toolkit.Utils
{
    public class ExtentManager
    {
        private static ExtentReports extent;
        public static ExtentReports Instance
        {
            get
            {
                if (extent == null)
                {
                    extent = new ExtentReports(AppDomain.CurrentDomain.BaseDirectory + "/Extent_Report.html", false, DisplayOrder.OldestFirst);
                }
                return extent;
            }
        }
    }
}
