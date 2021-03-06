﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;

using System.IO;

using NPOI.HSSF.Model; // InternalWorkbook
using NPOI.HSSF.UserModel; // HSSFWorkbook, HSSFSheet

namespace Orasi.Toolkit.Utils //Read___write_XLS_via_NPOI___display_in_GRID
{
    /// <summary>
    /// Class to read in Excel sheet to a collection 
    /// </summary>
   
    class ExcelDocumentReader
    {
        private HSSFSheet sh;
        private HSSFWorkbook wb;
        private HSSFCell cell;
        private string filePath;       

        List<DataSet> myDataset = new List<DataSet>();

        /// <summary>
        /// Method to read in Excel(xls) file to Collection</summary>
        /// <param name="filepath">Path of the file</param>
        /// <param name="sheetname">Name of the sheet</param> 
        /// <returns>Collection, eventually</returns>
        public object ReadData(string filePath, string sheetName)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Getting all the data from a given sheet
                //int startRow;
                //int headingRow;

                //// Determines if headings are being used
                //if (wb.GetSheet(sheetName).DisplayRowColHeadings)
                //{
                //    headingRow = 1;
                //    startRow = 2;
                //}
                //else
                //{                    
                //    startRow = 1;
                //}
                                
                wb = new HSSFWorkbook(fs);

                // get sheet
                sh = (HSSFSheet)wb.GetSheet(sheetName);

                for (int i = 0; i < sh.LastRowNum; i++)
                {
                    for (int n = 0; n < sh.GetRow(i).LastCellNum; n++)
                    {
                        myDataset.Add(new DataSet() { ColName = (sh.GetRow(0).GetCell(n).StringCellValue), ColValues = (sh.GetRow(i + 1).GetCell(n).StringCellValue), Row = (sh.GetRow(i).RowNum) });

                        var panther = from dataset in myDataset
                                    select dataset;

                        foreach (var dataset in panther)
                            Console.WriteLine("{0} - {1} - {2}", dataset.ColName, dataset.ColValues, dataset.Row);
                        Console.ReadLine();
                    }
                    
                }
                return myDataset;
             }
            }
    }

        /*
        public void Read(string filename, string path, string sheet)
        {
            // get sheets list from xls
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                wb = new HSSFWorkbook(fs);
                
                for (int i = 0; i < wb.Count; i++)
                {
                    DataSet.Add(new DataSet() { ColName = (wb.GetSheet(sheet).GetRow(i).GetCell(i)}
                    wb.cou
                    //parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });
                    //comboBox1.Items.Add(wb.GetSheetAt(i).SheetName);
                }
            }
        }*/

        /// <summary>
        /// Method to write out Excel(xls) file from Collection</summary>
        /// <param name="filename">something</param>
        /// <param name="path">something else</param> 
        /// <returns>void</returns>
        /*public void Write(string filename, string path)
        {
            //TODO
        }

        /*private void Form1_Load(object sender, EventArgs e)
        {
            // create xls if not exists
            if (!File.Exists("test.xls"))
            {
                wb = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());

                // create sheet
                sh = (HSSFSheet)wb.CreateSheet("Sheet1");
                // 3 rows, 2 columns
                for (int i = 0; i < 3; i++)
                {
                    var r = sh.CreateRow(i);
                    for (int j = 0; j < 2; j++)
                    {
                        r.CreateCell(j);
                    }
                }

                using (var fs = new FileStream("test.xls", FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fs);
                }
            }

            // get sheets list from xls
            using (var fs = new FileStream("test.xls", FileMode.Open, FileAccess.Read))
            {
                wb = new HSSFWorkbook(fs);

                for (int i = 0; i < wb.Count; i++)
                {
                    comboBox1.Items.Add(wb.GetSheetAt(i).SheetName);
                }
            }
        }*/

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            // clear grid before filling
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // get sheet
            sh = (HSSFSheet)wb.GetSheet(comboBox1.SelectedItem.ToString());

            int i = 0;
            while (sh.GetRow(i) != null)
            {
                // add necessary columns
                if (dataGridView1.Columns.Count < sh.GetRow(i).Cells.Count)
                {
                    for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                    {
                        dataGridView1.Columns.Add("", "");
                    }
                }

                // add row
                dataGridView1.Rows.Add();

                // write row value
                for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                {
                    var cell = sh.GetRow(i).GetCell(j);

                    if (cell != null)
                    {
                        // TODO: you can add more cell types capability, e. g. formula
                        switch (cell.CellType)
                        {
                            case NPOI.SS.UserModel.CellType.Numeric:
                                dataGridView1[j, i].Value = sh.GetRow(i).GetCell(j).NumericCellValue;
                                break;
                            case NPOI.SS.UserModel.CellType.String:
                                dataGridView1[j, i].Value = sh.GetRow(i).GetCell(j).StringCellValue;
                                break;
                        }
                    }
                }

                i++;
            }
        }*/

        /*
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (sh.GetRow(i) == null)
                    sh.CreateRow(i);

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (sh.GetRow(i).GetCell(j) == null)
                        sh.GetRow(i).CreateCell(j);

                    if (dataGridView1[j, i].Value != null)
                    {
                        sh.GetRow(i).GetCell(j).SetCellValue(dataGridView1[j, i].Value.ToString());
                    }
                }
            }

            using (var fs = new FileStream("test.xls", FileMode.Open, FileAccess.Write))
            {
                wb.Write(fs);
            }
        }*/
        class DataSet
        {
            public string ColName { get; set; }
            public string ColValues { get; set; }
            public int Row { get; set; }
        }
    }
