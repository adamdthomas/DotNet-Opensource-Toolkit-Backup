using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel; // HSSFWorkbook, HSSFSheet
using NPOI.SS.UserModel;

namespace Orasi.Toolkit.Utils
{
    /// <summary>
    /// Class to help handle data from Excel files. 
    /// </summary>
    class ExcelDocumentReader
    {
        private string filePath;
        private string sheetName;
        private HSSFWorkbook wb;
        private FileStream fs;
        private ISheet iSh;

        /// <summary>
        /// Method to return ISheet object based on first sheet given a filepath.</summary>
        /// <param name="string">filePath</param>
        /// <returns>ISheet object "iSh" containing sheet at index 0 of workbook</returns>
        public ISheet Excel(string filePath)
        {
            this.filePath = filePath;

            GetWorkBook(filePath);
            //setting default sheet.
            iSh = wb.GetSheetAt(0);

            return iSh;
        }

        /// <summary>
        /// Method to return List containing all cells on filepath and sheetname.</summary>
        /// <param name="string">filePath</param>
        /// <param name="string">sheetName</param>
        /// <returns>@List<Dataset> object where each element of the List contains info regarding one cell.</returns>
        //public List<Dataset> Excel(string filePath, string sheetName)
        //{
        //    //Opens a workbook while opening a specific sheet within a workbook
        //    this.filePath = filePath;
        //    this.sheetName = sheetName;
        //    List<Dataset> myDataset = new List<Dataset>();

        //    myDataset = GetAllCells(filePath, sheetName);

        //    return myDataset;
        //}

        /// <summary>
        /// Method to return ISheet object based on sheet name and file path provided.</summary>
        /// <param name="string">filePath</param>
        /// <param name="string">sheetName</param>
        /// <returns>ISheet object containing sheet specified by sheet name and file path.</returns>
        private ISheet GetSheet(string SheetName)
        {
            
            iSh = wb.GetSheet(SheetName);

            return iSh;
        }

        /// <summary>
        /// Method to return HSSFWorkbook object based on file path provided.</summary>
        /// <param name="string">filePath</param>
        /// <returns>HSSFWorkbook object containing workbook specified by file path.</returns>
        private HSSFWorkbook GetWorkBook(string filePath)
        {
            this.filePath = filePath;

            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException e)
            {
                string message = e.Message;
                throw;
            } 

            wb = new HSSFWorkbook(fs);

            return wb;
        }

        /// <summary>
        /// Method to return ICell object based on row/column combination.</summary>
        /// <param name="int">cellRow</param>
        /// <param name="int">cellColumn</param>
        /// <returns>ICell object containing cell specified by row and column.</returns>
        private ICell GetCell(int cellRow, int cellColumn)
        {
            ICell iCell = null;
            var row = iSh.GetRow(cellRow);

            if (row != null)
            {
                iCell = iSh.GetRow(cellRow).GetCell(cellColumn);
            }
            
            return iCell;
        }

        /// <summary>
        /// Method to return List containing all cells on filepath and sheetname.</summary>
        /// <param name="string">filePath</param>
        /// <param name="string">sheetName</param>
        /// <returns>@List<Dataset> object where each element of the List contains info regarding one cell.</returns>
        public List<Dataset> GetAllCells(string filePath, string sheetName)
        {
            this.filePath = filePath;
            this.sheetName = sheetName;
            List<Dataset> myDataset = new List<Dataset>();

            GetWorkBook(filePath);
            GetSheet(sheetName);
            
            // loop for rows
            for (int r = 0; r < iSh.LastRowNum + 1; r++)
            {

                // loop for columns
                for (int c = 0; c != iSh.GetRow(r).LastCellNum; c++)
                {
                    var cell = GetCell(r, c);
                    string ColValue = "";

                    switch (cell.CellType)
                    {
                        case CellType.Unknown:
                            break;
                        case CellType.Numeric:
                            ColValue = (cell.NumericCellValue.ToString());
                            break;
                        case CellType.String:
                            ColValue = (cell.StringCellValue);
                            break;
                        case CellType.Formula:
                            break;
                        case CellType.Blank:
                            break;
                        case CellType.Boolean:
                            break;
                        case CellType.Error:
                            break;
                        default:
                            break;
                    }

                    if (iSh.DisplayRowColHeadings)
                    {
                        if (r != 0)
                        {
                            myDataset.Add(new Dataset() { ColName = GetCell(0, c).StringCellValue, ColValues = ColValue, Row = (iSh.GetRow(r).RowNum), Column = cell.ColumnIndex });
                        }
                        else
                        {
                            myDataset.Add(new Dataset() { ColName = GetCell(0, c).StringCellValue, ColValues = null, Row = (iSh.GetRow(r).RowNum), Column = cell.ColumnIndex });
                        }

                    }
                    else
                    {
                        myDataset.Add(new Dataset() { ColName = null, ColValues = ColValue, Row = (iSh.GetRow(r).RowNum) });
                    }
                }
            }
            
            return myDataset;
        }
    }
    /// <summary>
    /// Class to define Dataset as List Type</summary>
    class Dataset
    {
        public string ColName { get; set; }
        public string ColValues { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
