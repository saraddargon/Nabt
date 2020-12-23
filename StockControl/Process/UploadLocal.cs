using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace StockControl
{
    public partial class UploadLocal : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public UploadLocal(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public UploadLocal()
        {
            InitializeComponent();
        }

        string PR1 = "";
        string PR2 = "";
        string Type = "";
        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
           
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!txtPath.Text.Equals("") && !txtReferNo.Text.Equals(""))
            {
                if (MessageBox.Show("คุณต้องการอัพโหลดหรือไม่ ?", "อัพโหลดรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UploadExcel(txtPath.Text);
                }
            }
            else
            {
                MessageBox.Show("PathFile is Empty! or Refer No. Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadExcel(string Path)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
               


                ////Code here
                progressBar1.Visible = true;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  Path, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                progressBar1.Maximum = 200;
                progressBar1.Minimum = 1;
                int EndofTAG = 1;
                int Rowx = 2;
                int RNo = 0;
                int countRow = 0;         

                int Check1 = 0;


                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    //VAriable//
                    int qty = 0;
                    DateTime Date1 = DateTime.Now;
                    while (EndofTAG == 1)
                    {
                        if (Rowx <= 200)
                        {
                            progressBar1.Value = Rowx;
                            progressBar1.PerformStep();
                        }

                        System.Array myvalues;
                        Excel.Range range = worksheet.get_Range("A" + Rowx.ToString(), "O" + Rowx.ToString());


                        myvalues = (System.Array)range.Cells.Value;
                        string[] strArray = ConvertToStringArray(myvalues);

                        if (!Convert.ToString(strArray[0]).Equals("") &&
                            !Convert.ToString(strArray[0]).ToLower().Equals("end") &&
                            !Convert.ToString(strArray[1]).Equals("")
                            )
                        {
                            qty = 0;
                            countRow += 1;
                            Check1 = 0;
                            // Start Insert //
                            tb_LocalDelivery dlc = db.tb_LocalDeliveries.Where(ee => ee.SaleOrderNo == Convert.ToString(strArray[0]) && ee.UploadRefNo == txtReferNo.Text).FirstOrDefault();
                            if (dlc != null)
                            {
                                if (dlc.SS == 3)
                                {
                                    Check1 = 1;
                                }
                                else
                                {
                                    Check1 = 0;
                                    db.tb_LocalDeliveries.DeleteOnSubmit(dlc);
                                    db.SubmitChanges();
                                }
                            }

                            //Insert//
                            if(Check1==0)
                            {
                                DateTime.TryParse(Convert.ToString(strArray[7]), out Date1);
                                tb_LocalDelivery ld = new tb_LocalDelivery();
                                ld.SS = 1;
                                ld.UploadDate = DateTime.Now;
                                ld.UploadRefNo = txtReferNo.Text;
                                ld.ConfirmBy = "";
                                ld.ConfirmDate = null;
                                ld.ConfirmFlag = false;
                                ld.ShipBy = "";
                                ld.ShipDate = null;
                                ld.ShipFlag = false;
                                ld.ShippingDate = Date1;
                                ld.SaleOrderNo = Convert.ToString(strArray[0]);
                                ld.InvoiceNo = Convert.ToString(strArray[8]);
                                ld.PartNo = Convert.ToString(strArray[1]);
                                ld.PartName = Convert.ToString(strArray[3]);
                                ld.CustomerItemNo = Convert.ToString(strArray[2]);
                                ld.CustomerName = Convert.ToString(strArray[5]);
                                ld.CustomerNo = Convert.ToString(strArray[4]);
                                ld.OrderQty = Convert.ToInt32(strArray[6]);
                                ld.LotFlag = false;
                                ld.Remark = "";

                                db.tb_LocalDeliveries.InsertOnSubmit(ld);
                                db.SubmitChanges();
                            }


                            // End insert //
                        }
                        else
                        {
                            EndofTAG = 0;
                            break;

                        }

                        Rowx += 1;

                    }
                    //excelBook.Save();
                    excelBook.Close();
                    excelApp.Quit();

                    releaseObject(worksheet);
                    releaseObject(excelBook);
                    releaseObject(excelApp);
                    Marshal.FinalReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    GC.GetTotalMemory(false);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.GetTotalMemory(true);
                    /////////////////////////////////////

                    MessageBox.Show("Import Completed.\n row=" + countRow);
                }
                    
                

                ////////////
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
            progressBar1.Visible = false;
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                // MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private string[] ConvertToStringArray(System.Array values)
        {

            // create a new string array
            string[] theArray = new string[values.Length];

            // loop through the 2-D System.Array and populate the 1-D String Array
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }

            return theArray;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูล หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_015_DeleteLocalDelivery(txtReferNo.Text);
                    }
                    MessageBox.Show("ลบเรียบร้อยแล้ว!");
                }
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtReferNo.Text = "";
            txtPath.Text = "";
        }
    }
}
