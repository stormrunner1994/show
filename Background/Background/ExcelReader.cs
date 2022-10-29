using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Background
{
    class ExcelReader
    {
        private string text = "";
        private DateTime tag = DateTime.Now;
        private string dateipfad = @"D:\Studium\SS2019\Plan September 2019.xlsx";

        public string getAufgaben()
        {
            return text;
        }

        public ExcelReader(string dateipfad, DateTime tag)
        {
            this.dateipfad = dateipfad;
            this.tag = tag;
            CommonConstructor();
        }

        public ExcelReader(string dateipfad)
        {
            this.dateipfad = dateipfad;
            CommonConstructor();
        }

        private void CommonConstructor()
        {
            if (!File.Exists(dateipfad))
                return;

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(dateipfad);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.Cells;

            this.text = getText(xlWorksheet, xlRange);

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        public ExcelReader()
        {
            CommonConstructor();
        }

        private string getText(Excel._Worksheet xlWorksheet, Excel.Range xlRange)
        {
            int rows = Convert.ToInt32(xlWorksheet.UsedRange.Rows.Count);
            int columns = Convert.ToInt32(xlWorksheet.UsedRange.Columns.Count);
            DateTime firstday = Convert.ToDateTime("1900-01-01");
            int row = -1;

            for (int zeile = 1; zeile <= rows; zeile++)
            {
                try
                {
                    string cell = xlRange[zeile, 1].Value2.ToString();
                    int iout;
                    if (Int32.TryParse(cell, out iout))
                    {
                        int add = Convert.ToInt32(cell);
                        DateTime test = firstday.AddDays(add - 2);

                        if (test.Day == tag.Day
                        && test.Month == tag.Month
                        && test.Year == tag.Year)
                        {
                            row = zeile;
                            break;
                        }

                    }
                }
                catch (Exception ex)
                {
                }
            }

            string text = "";

            for (int a = 1; a < 4; a++)
            {
                try
                {
                    string cell = xlRange[row, a + 2].Value2.ToString();
                    cell = cell.Replace(',', ';');

                    if (text != "")
                        text += ";" + cell;
                    else
                        text += cell;
                }
                catch (Exception ex)
                {


                }
            }

            return text;
        }
        
    }
}
