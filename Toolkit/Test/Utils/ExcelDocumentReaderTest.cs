using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using NUnit.Common;
using Orasi.Toolkit.Utils;
using System.Resources;
using System.IO;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]
    class ExcelDocumentReaderTest
    {
        [Test]
        public void readDataFilePathConstructorAllRows()
        {
            /* Java Base for tests
            Object[][] data = null;
            String filepath = getClass().getResource("/excelsheets/TestData.xlsx").getPath();
            data = new ExcelDocumentReader(filepath).readData("Data");

            Assert.assertNotNull(data);
            Assert.assertTrue(data.length == 3);
            Assert.assertTrue(data[0].length == 5);
            Assert.assertTrue(data[0][0].toString().equals("DataSet1"));
            */

            string filePath = @"C:\Users\Paul\Source\Repos\DotNet-Opensource-Toolkit\Toolkit\test.xls";
            string sheetName = "panther";

            var edr = new ExcelDocumentReader();

            List<Dataset> myDataset = new List<Dataset>();

            myDataset = edr.GetAllCells(filePath, sheetName);
            
            string cap = myDataset[7].ColValues.ToString();

        }

    }
}
