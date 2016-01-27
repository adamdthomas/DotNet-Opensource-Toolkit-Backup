using System.Collections.Generic;
using NUnit.Framework;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Test.Utils
{
    [TestFixture]
    class ExcelDocumentReaderTest
    {
        [Test]
        public void readDataFilePathAllRows()
        {
            // define test parameters
            //Properties.Resources.test.GetValue.filePath;
            //string test = Properties.Resources.test.ToString();
            
            string filePath = @"C:\Users\Paul\Source\Repos\DotNet-Opensource-Toolkit\Toolkit\test.xls";
            string sheetName = "panther";
            // create new List to accept output of GetAllCells()
            List<Dataset> myDataset = new List<Dataset>();

            // create new ExcelDocumentReader object
            var edr = new ExcelDocumentReader();
            
            // call GettAllCells()
            myDataset = edr.GetAllCells(filePath, sheetName);
            
            // Asserts
            Assert.NotNull(myDataset);
            Assert.True(myDataset.Count == 9);
            Assert.True(myDataset[3].ColValues.Equals("Cam"));
            Assert.True(myDataset[8].ColName.Equals("position"));
            Assert.True(myDataset[0].Column == 0);
        }

    }
}
