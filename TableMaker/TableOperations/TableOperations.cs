using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using OnBarcode.Barcode;

namespace TableMaker.TableOperations
{
    public class TableOperations
    {
        public Worksheet ExecuteExcel(string fileFullPath, bool isVisiable, out Workbook clsWorkbook)
        {
            clsWorkbook = null;
            Application clsExcel = new Application {Visible = isVisiable};
            string path = Environment.CurrentDirectory;
            Workbooks workbooks = clsExcel.Workbooks;

            clsWorkbook = workbooks.Open(
                fileFullPath,
                2,
                false,
                5,
                "",
                "",
                true,
                XlPlatform.xlWindows, "",
                false, true, 0, false, true,
                XlCorruptLoad.xlNormalLoad);
            Worksheet clsWorksheet = clsWorkbook.Sheets[1];
            return clsWorksheet;
        }

        public string GetSetUpName(Worksheet clsWorksheet)
        {

            var value = ((Range) clsWorksheet.Cells[1, 4]).Value;

            return value.ToString();
        }

        public void DeleteColumns(Worksheet clsWorksheet)
        {
            for (int i = 0; i < 4; i++)
            {
                ((Range) clsWorksheet.Cells[1, 1]).EntireColumn.Delete(null);
            }

            for (int i = 0; i < 4; i++)
            {
                ((Range) clsWorksheet.Cells[2, 2]).EntireColumn.Delete(null);
            }
            ((Range) clsWorksheet.Cells[3, 3]).EntireColumn.Delete(null);
            for (int i = 0; i < 3; i++)
            {
                ((Range) clsWorksheet.Cells[4, 4]).EntireColumn.Delete(null);
            }
        }

        public void AddHeader(Worksheet clsWorksheet)
        {
            Range rangeForSorting = clsWorksheet.UsedRange;
            rangeForSorting.Sort(rangeForSorting.Columns[3], XlSortOrder.xlAscending);
            Range line = (Range) clsWorksheet.Rows[1];
            line.Insert();
            //order number
            clsWorksheet.Cells[1, 1] = "№ Поръчка";
            //serial number of pcb
            clsWorksheet.Cells[1, 2] = "№ Изделие";
            //pcb quantity
            clsWorksheet.Cells[1, 3] = "Бр. Платки";
            //panels quantity
            clsWorksheet.Cells[1, 4] = "Бр. Панели";
            //serial number of the panels
            clsWorksheet.Cells[1, 5] = "№ Панел";
            //Which site must produce
            clsWorksheet.Cells[1, 6] = "Страни";
            //Ducumentetion folder
            clsWorksheet.Cells[1, 7] = "№ Папка";
            ((Range) clsWorksheet.Rows[1]).EntireRow.Font.Bold = true;
        }

        public int GetRowsRange(Worksheet clsWorksheet, Range defaultRange)
        {
            int lastRow = 0;
            for (int row = 1; row < defaultRange.Rows.Count; row++)
            {
                var columnA = ((Range) clsWorksheet.Cells[row, 1]).Value;
                Convert.ToString(columnA);
                var columnB = ((Range) clsWorksheet.Cells[row, 2]).Value;
                Convert.ToString(columnB);
                var columnC = ((Range) clsWorksheet.Cells[row, 3]).Value;
                Convert.ToString(columnC);
                if (columnA == null && columnB == null && columnC == null)
                {
                    lastRow = row;
                    break;
                }
            }
            return lastRow;
        }

        public void FormatTable(Worksheet clsWorksheet, Range range)
        {
            clsWorksheet.Columns.EntireColumn.ColumnWidth = 15;
            clsWorksheet.Rows.EntireRow.RowHeight = 28;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignCenter;

            //delete '.' and ',' from cells with pcb quantity
            for (int row = 2; row < range.Rows.Count; row++)
            {
                dynamic cellValue = ((Range) clsWorksheet.Cells[row, 3]).Value;
                string cellData = Convert.ToString(cellValue);
                if (cellData != null)
                {
                    if (cellData.Contains("."))
                    {
                        string newValue = cellData.Replace(".", "");
                        newValue = newValue.Remove(newValue.Length - 4);
                        clsWorksheet.Cells[row, 3] = newValue;
                    }
                }
            }
            //make first column vertical alignment bottom to have room for data matrix code image
            for (int row = 2; row < range.Rows.Count; row++)
            {
                range[row, 1].VerticalAlignment = XlVAlign.xlVAlignBottom;

            }
        }

        public void AddDataInCells(Worksheet clsWorksheet, Range range)
        {
            //add panels quantity in order
            for (int row = 2; row < range.Rows.Count; row++)
            {
                //column C
                clsWorksheet.Cells[row, 4] = $"=C{row}/VLOOKUP(B{row},Sheet2!A$1:L$700,5,0)";
            }

            // add pcb bord serial number
            for (int row = 2; row < range.Rows.Count; row++)
            {
                //column D
                clsWorksheet.Cells[row, 5] = $"=VLOOKUP(B{row},Sheet2!A$1:L$700,3,0)";
            }

            //Add site
            for (int row = 2; row < range.Rows.Count; row++)
            {
                //column F
                clsWorksheet.Cells[row, 6] = $"=VLOOKUP(B{row},Sheet2!A$1:L$700,4,0)";
            }

            //Add documentation folder
            for (int row = 2; row < range.Rows.Count; row++)
            {
                //column G
                clsWorksheet.Cells[row, 7] = $"=VLOOKUP(B{row},Sheet2!A$1:L$700,8,0)";
            }
        }

        public void CreateDataMatrixCode(Worksheet clsWorksheet, Range range, string path)
        {
            for (int row = 2; row < range.Rows.Count; row++)
            {
                dynamic cellValue = ((Range) clsWorksheet.Cells[row, 1]).Value;
                string orderNumber = Convert.ToString(cellValue);

                DataMatrix datamatrix = new DataMatrix();
                datamatrix.Data = orderNumber;
                datamatrix.DataMode = DataMatrixDataMode.ASCII;
                datamatrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                datamatrix.drawBarcode($"{path}\\{orderNumber}.bmp");

                Range oRange = range.Cells[row, 1];
                float Left = (float) ((double) oRange.Left + 35);
                float Top = (float) ((double) oRange.Top + 3);
                const float ImageSize = 13;
                FileInfo file = new FileInfo($"{path}\\{orderNumber}.bmp") {IsReadOnly = false};
                FileInfo pathFileInfo = new FileInfo(path) { IsReadOnly = false };

                string bmpPath = file.Name;
                clsWorksheet.Shapes.AddPicture($"{path}\\{bmpPath}",
                    Microsoft.Office.Core.MsoTriState.msoFalse,
                    Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                File.Delete($"{path}\\{orderNumber}.bmp");
            }
        }

        public void AddSetupName(Worksheet clsWorksheet, string setupName)
        {
            Range line = clsWorksheet.Rows[1];
            line.Insert();
            clsWorksheet.get_Range("A1", "G1").Merge();
            clsWorksheet.Cells[1, 1] = setupName;
            clsWorksheet.get_Range("A1", "A1").Font.Bold = true;
            clsWorksheet.get_Range("A1", "A1").Font.Size = 24;
            clsWorksheet.get_Range("A1", "A1").HorizontalAlignment = XlHAlign.xlHAlignCenter;
            clsWorksheet.get_Range("A1", "A1").VerticalAlignment = XlVAlign.xlVAlignCenter;

        }

        public void AddBorders(Worksheet clsWorksheet, Range range)
        {
            Borders borders = range.Borders;
            borders.LineStyle = XlLineStyle.xlContinuous;
            borders.Weight = 2d;
        }
    }
}
