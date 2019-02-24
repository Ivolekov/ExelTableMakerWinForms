using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace TableMaker
{
    public partial class TableMaker : Form
    {
        public TableMaker()
        {
            InitializeComponent();
        }

        private void TableMaker_Load(object sender, EventArgs e)
        {

            string filefullPath = Environment.CurrentDirectory + @"\" + "Config.xml";
            XmlTextReader reader = new XmlTextReader(filefullPath);
            try
            {
                while (reader.Read())
                {
                    tbPathFile.Text = reader.ReadElementString(Common.c_xml_el_TableXlsxPath);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                reader.Close();
            }
            finally
            {
                reader.Close();
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Executable files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != "")
                tbPathFile.Text = ofd.FileName;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            progressBar.Increment(-100);
            rtbLogs.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(tbPathFile.Text))
            {
                MessageBox.Show("Choose full path to excel file!", "ERROR!!!", MessageBoxButtons.OK);
                return; ;
            }
            var isVisible = cbVisible.Checked;
            string filefullPathToCobfigFile = Environment.CurrentDirectory + @"\" + "Config.xml";
            string fileFullPathToExcelFile = tbPathFile.Text;
            XmlTextWriter writer = null;
            TableOperations.TableOperations tableOperations = new TableOperations.TableOperations();

            rtbLogs.Text += "Open Excel..." + Environment.NewLine;
            Application clsExcel = new Application { Visible = isVisible };
            string pathForDataMatrixCodeImg = Environment.CurrentDirectory;
            Workbooks workbooks = clsExcel.Workbooks;
            rtbLogs.Text += "Open Template..." + Environment.NewLine;
            Workbook clsWorkbook = workbooks.Open(
                fileFullPathToExcelFile,
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

            rtbLogs.Text += "Start..." + Environment.NewLine;
            progressBar.Increment(10);
            try
            {
                rtbLogs.Text += "Get setup name..." + Environment.NewLine;
                string setupName = tableOperations.GetSetUpName(clsWorksheet);
                progressBar.Increment(10);

                rtbLogs.Text += "Delete columns..." + Environment.NewLine;
                tableOperations.DeleteColumns(clsWorksheet);
                progressBar.Increment(10);

                rtbLogs.Text += "Add header..." + Environment.NewLine;
                tableOperations.AddHeader(clsWorksheet);
                progressBar.Increment(10);

                Range defaultRange = clsWorksheet.get_Range("A1", "G200");
                int rowsRange = tableOperations.GetRowsRange(clsWorksheet, defaultRange);
                Range range = clsWorksheet.get_Range("A1", $"G{rowsRange}");

                rtbLogs.Text += "Format table..." + Environment.NewLine;
                tableOperations.FormatTable(clsWorksheet, range);
                progressBar.Increment(10);

                rtbLogs.Text += "Fill table with data..." + Environment.NewLine;
                tableOperations.AddDataInCells(clsWorksheet, range);
                progressBar.Increment(10);

                rtbLogs.Text += "Create data matrix codes..." + Environment.NewLine;

                tableOperations.CreateDataMatrixCode(clsWorksheet, range, pathForDataMatrixCodeImg);
                progressBar.Increment(10);

                rtbLogs.Text += "Add borders and headline to the table..." + Environment.NewLine;
                tableOperations.AddSetupName(clsWorksheet, setupName);
                range = clsWorksheet.get_Range("A1", $"G{rowsRange}");
                tableOperations.AddBorders(clsWorksheet, range);
                progressBar.Increment(10);
                if (!isVisible)
                {
                    rtbLogs.Text += "Saving..." + Environment.NewLine;
                    
                }
                rtbLogs.Text += "DONE" + Environment.NewLine;

                progressBar.Increment(10);
            }
            catch (Exception exception)
            {
                rtbLogs.SelectionColor = Color.Red;
                string error = "------------ERROR------------" + Environment.NewLine;
                error += exception.Message + Environment.NewLine;
                error += "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^" + Environment.NewLine;
                rtbLogs.SelectedText = error;
                throw new ArgumentException();
            }
            finally
            {
                if (!isVisible)
                {
                    clsWorkbook.Save();
                    Marshal.FinalReleaseComObject(clsWorksheet);
                    clsWorkbook.Close();
                    Marshal.ReleaseComObject(clsWorkbook);
                    workbooks.Close();
                    Marshal.ReleaseComObject(workbooks);
                    clsExcel.Application.Quit();
                    Marshal.ReleaseComObject(clsExcel);
                    clsExcel = null;
                }
            }
            rtbLogs.SelectionColor = Color.Green;
            string succsess = "-----YOUR TABLE WAS CREATED SUCCESSFULLY-----"+Environment.NewLine;
            rtbLogs.SelectedText = succsess;
            rtbLogs.SelectionColor = Color.Black;
            progressBar.Increment(5);
            try
            {
                writer = new XmlTextWriter(filefullPathToCobfigFile, Encoding.UTF8) { Formatting = Formatting.Indented };
                writer.WriteStartDocument();
                writer.WriteElementString(Common.c_xml_el_TableXlsxPath, tbPathFile.Text);
                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                rtbLogs.SelectionColor = Color.Blue;
                string error = ex.InnerException + Environment.NewLine;
                rtbLogs.SelectedText = error;
                writer?.Close();
            }
            finally
            {
                progressBar.Increment(5);
                writer?.Close();
            }



        }

        private void cbVisible_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
