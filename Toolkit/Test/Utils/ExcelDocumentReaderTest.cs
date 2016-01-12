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
            string filePath = @"C:\Users\Paul\Source\Repos\DotNet-Opensource-Toolkit\Toolkit\test.xls";
            string sheetName = "panther";

            var edr = new ExcelDocumentReader();

            List<Dataset> myDataset = new List<Dataset>();

            myDataset = edr.GetAllCells(filePath, sheetName);

            Assert.NotNull(myDataset);
            Assert.True(myDataset.Count == 9);
            Assert.True(myDataset[3].ColValues.Equals("Cam"));
            Assert.True(myDataset[8].ColName.Equals("position"));
            Assert.True(myDataset[0].Column == 0);
        }

    }
}
