using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace StockControl
{
    public partial class UploadExList : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public UploadExList(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public UploadExList()
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
            dtDatetime.Value = DateTime.Now;
            dtETA.Value = DateTime.Now;
            dtETD.Value = DateTime.Now;
            txtInvoiceNo.Text = "";
            txtPath.Text = "";
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
               
                txtPath.Text = openFileDialog1.FileName;
            }
        }
        private void UploadExcel(string Path)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string InvoiceNo = "";
                string ShipBy = "";
                string IssueDate = "";
                string IssueBy = "";
                string ETADate = "";
                string ETDDate = "";
                string VERSION = "";
                string CountrySize2 = "";
                string Country2 = "";
                //New Cell in Excel import//
                string Code = "";
                string Fright = "";
                string attn = "";
                string ShipVia = "";
                string AirType = "";


                ////Code here
                progressBar1.Visible = true;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  Path, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                progressBar1.Maximum = 301;
                progressBar1.Minimum = 1;
                int EndofTAG = 1;
                int Rowx = 11;
                int RNo = 0;
                int countRow = 0;

                Excel.Range ExInv = worksheet.get_Range("C2");
                Excel.Range ExRev = worksheet.get_Range("N2");
                Excel.Range ExIssueName = worksheet.get_Range("N3");
                Excel.Range ExIssueDate = worksheet.get_Range("N4");
                Excel.Range ExShipBy = worksheet.get_Range("C6");
                Excel.Range ExETD = worksheet.get_Range("N5");
                Excel.Range ExETA = worksheet.get_Range("N6");
                Excel.Range CountrySize = worksheet.get_Range("C4");
                Excel.Range Country = worksheet.get_Range("C5");

                //New Cell///
                Excel.Range Cell1x = worksheet.get_Range("D5");
                    Code = Convert.ToString(Cell1x.Value2);
                Excel.Range Cell2x = worksheet.get_Range("C7");
                    Fright = Convert.ToString(Cell2x.Value2);
                Excel.Range Cell3x = worksheet.get_Range("C8");
                    attn = Convert.ToString(Cell3x.Value2);

                Excel.Range Cell4x = worksheet.get_Range("N7");
                    ShipVia = Convert.ToString(Cell4x.Value2);
                Excel.Range Cell5x = worksheet.get_Range("N8");
                    AirType = Convert.ToString(Cell5x.Value2);

                if (AirType==null)
                {
                    AirType = "";
                }
                //if (AirType.Equals(null))
                //    AirType = "";


                InvoiceNo = Convert.ToString(ExInv.Value2);
                VERSION = Convert.ToString(ExRev.Value2);
                IssueBy = Convert.ToString(ExIssueName.Value2);
                IssueDate = Convert.ToString(ExIssueDate.Value);
                ShipBy = Convert.ToString(ExShipBy.Value2);
                ETADate = dtETA.Value.ToString("yyyy-MM-dd"); // Convert.ToString(ExETA.Value);
                ETDDate = dtETD.Value.ToString("yyyy-MM-dd") ;// Convert.ToString(ExETD.Value);
                Country2 = Convert.ToString(Country.Value);
                CountrySize2 = Convert.ToString(CountrySize.Value);

                int Check1 = 0;
                int rows = 0;

                if (txtInvoiceNo.Text.ToLower().Equals(InvoiceNo.Trim().ToLower()))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ExportList iv = db.tb_ExportLists.Where(ivs => ivs.InvoiceNo.Trim().ToLower().Equals(InvoiceNo.ToLower().Trim())).FirstOrDefault();
                        if (iv != null)
                        {
                            //Delete Value
                            Check1 = 1;
                            MessageBox.Show("มีข้อมูล Import ไว้แล้ว! \n ต้องลบออกก่อน !");
                        }
                        else
                        {
                            //InsertHeader//
                           // MessageBox.Show(DateTime.FromOADate(Convert.ToDouble(ETDDate)).ToShortDateString());
                            tb_ExportList ed = new tb_ExportList();
                            ed.InvoiceNo = InvoiceNo;
                            ed.LoadDate = dtDatetime.Value;
                            ed.ShippingBy = ShipBy;
                            ed.ETDDate = ETDDate;
                            ed.ETADate = ETADate;
                            ed.IssueBy = IssueBy;
                            ed.IssueDate = IssueDate;
                            ed.Revision = VERSION;
                            ed.Status = "Waiting";
                            ed.Remark = "";
                            ed.Country = Country2;
                            ed.CountrySize = CountrySize2;
                            ed.CountryOriginal = "THANLAND";
                            ed.ShippingMark = "";
                            ed.CustomerSale = "";
                            ed.CustomerShip = "";
                            ed.AddressSale = "";
                            ed.AddressSale2 = "";
                            ed.AddressShip = "";
                            ed.AddressShip2 = "";
                            ed.AttnSale = "";
                            ed.AttnShip = "";
                            ed.TelSale = "";
                            ed.TelShip = "";
                            ed.FaxSale = "";
                            ed.FaxShip = "";
                            ed.InvoiceOrder = "";
                            ed.InvoiceFlag = false;
                            ed.paymentTerm = "60 Days";
                           // ed.InvoiceDate = null;
                      

                            ed.ETADatex = dtETA.Value;
                            ed.ETDDatex = dtETD.Value;
                            ed.Code = Convert.ToString(Code).Trim().ToUpper();
                            ed.Frieght = Convert.ToString(Fright).Trim().ToUpper();
                            ed.Attn = Convert.ToString(attn);
                            ed.ShipVia = Convert.ToString(ShipVia).ToUpper();
                            ed.AirType = Convert.ToString(AirType).ToUpper();

                            db.tb_ExportLists.InsertOnSubmit(ed);
                            db.SubmitChanges();

                            ////Insert Line//
                            //VAriable//
                            int qty = 0;
                            int stdPack = 0;
                            int qtyofPL = 0;
                            decimal QtyOfTAG = 0;
                            int GroupP = 0;

                            Rowx = 11;
                            for (int ixi = 0; ixi < 300; ixi++)
                            {
                                //while (EndofTAG == 1)
                                //{
                                    try
                                    {
                                        rows += 1;
                                        if (Rowx < 300)
                                        {
                                            progressBar1.Value = Rowx;
                                            progressBar1.PerformStep();
                                        }

                                        System.Array myvalues;
                                        Excel.Range range = worksheet.get_Range("A" + Rowx.ToString(), "O" + Rowx.ToString());


                                        myvalues = (System.Array)range.Cells.Value;
                                        string[] strArray = ConvertToStringArray(myvalues);
                                        if (int.TryParse(Convert.ToString(strArray[0]), out RNo))
                                        {
                                            if (!Convert.ToString(strArray[0]).Equals("") &&
                                                !Convert.ToString(strArray[0]).ToLower().Equals("end") &&
                                                !Convert.ToString(strArray[1]).Equals("")
                                                )
                                            {
                                                countRow += 1;
                                                //Insert Value///
                                                qty = 0;
                                                stdPack = 1;
                                                qtyofPL = 1;
                                                QtyOfTAG = 1;
                                                int.TryParse(Convert.ToString(strArray[3]), out qty);
                                                int.TryParse(Convert.ToString(strArray[4]), out qtyofPL);
                                                decimal.TryParse(Convert.ToString(strArray[7]), out QtyOfTAG);
                                                int.TryParse(Convert.ToString(strArray[9]), out stdPack);

                                                if (Convert.ToString(strArray[4]).Trim().Equals(""))
                                                {

                                                }
                                                else
                                                {
                                                    GroupP += 1;
                                                }
                                            try
                                            {

                                                tb_ExportDetail et = new tb_ExportDetail();
                                                et.InvoiceNo = InvoiceNo;
                                                et.ListNo = Convert.ToInt32(strArray[0]);
                                                et.PalletNo = Convert.ToString(strArray[14]);
                                                et.PartNo = Convert.ToString(strArray[1]);
                                                et.PartName = Convert.ToString(strArray[2]);
                                                et.Qty = qty;
                                                et.QtyOfPL = qtyofPL;
                                                et.QtyOfTAG = QtyOfTAG;
                                                et.ShipBy = "";
                                                et.ShipFlag = false;
                                                et.SNP = stdPack;
                                                et.Status = Convert.ToString(strArray[6]);
                                                et.Status2 = "";// Convert.ToString(strArray[10]);
                                                et.STDPacking = stdPack;
                                                et.SS = 1;
                                                et.ConfirmBy = "";
                                                et.ConfirmFlag = false; //Packing//
                                                et.LotNo = Convert.ToString(strArray[5]);
                                                et.Customer = Convert.ToString(strArray[8]);
                                                et.OrderNo = Convert.ToString(strArray[13]);
                                                et.GroupP = GroupP.ToString();
                                                et.ShippingDate = dtDatetime.Value;
                                                et.OldQty = qty;
                                                et.K = Convert.ToString(strArray[10]);
                                                et.L = Convert.ToString(strArray[11]);
                                                et.M = Convert.ToString(strArray[12]);
                                               
                                                db.tb_ExportDetails.InsertOnSubmit(et);
                                                db.SubmitChanges();
                                            }
                                            catch (Exception ex) { MessageBox.Show("Add ->"+ex.Message); EndofTAG = 0;  break; }
                                           

                                                //End insert//
                                            }
                                            else
                                            {
                                                EndofTAG = 0;
                                                break;

                                            }

                                        }
                                        else
                                        {
                                            EndofTAG = 0;
                                        }
                                        Rowx += 1;

                                        if (Rowx > 300 || rows > 300)
                                        {
                                            EndofTAG = 0;
                                            break;
                                        }
                                    }
                                    catch(Exception ex) { EndofTAG = 0; MessageBox.Show("End of tag ->" + ex.Message); }

                                //}
                            }

                            //excelBook.Save();

                            excelBook.Close(false);
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
                    }
                }
                else
                {
                    MessageBox.Show("Invoice Not Match!");
                }
                    
                ////////////
            }
            catch(Exception ex) { this.Cursor = Cursors.Default;  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
            progressBar1.Visible = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!txtPath.Text.Equals("") && !txtInvoiceNo.Text.Equals(""))
            {
                if (MessageBox.Show("คุณต้องการอัพโหลดหรือไม่ ?", "อัพโหลดรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UploadExcel(txtPath.Text);
                }
            }
            else
            {
                MessageBox.Show("Invoice No is Empty! \n PathFile is Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูล หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ExportList ex = db.tb_ExportLists.Where(exi => exi.InvoiceNo.ToLower() == txtInvoiceNo.Text.ToLower() && !exi.Status.Equals("Completed")).FirstOrDefault();
                        if (ex != null)
                        {
                            db.tb_ExportLists.DeleteOnSubmit(ex);
                            db.SubmitChanges();
                            var exd = db.tb_ExportDetails.Where(exi => exi.InvoiceNo.ToLower() == txtInvoiceNo.Text.ToLower()).ToList();
                            foreach (var rd in exd)
                            {
                                db.tb_ExportDetails.DeleteOnSubmit(rd);
                                db.SubmitChanges();
                            }
                        }
                    }
                    MessageBox.Show("ลบเรียบร้อยแล้ว!");
                }
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtInvoiceNo.Text = "";
            dtDatetime.Value = DateTime.Now;
            txtPath.Text = "";
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการเพิ่มข้อมูล หรือไม่ ?", "เพิ่มรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!txtPath.Text.Equals("") && !txtInvoiceNo.Text.Equals(""))
                    {                        
                            UploadExcelNew(txtPath.Text);                        
                    }
                    else
                    {
                        MessageBox.Show("Invoice No is Empty! \n PathFile is Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch { }
        }
        public string GetNameSheet(string FilePath1)
        {
            string NameSheet = "";
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  FilePath1, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                //////////////////
                NameSheet = worksheet.Name;
                ///////////////////
                excelBook.Close(false);
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
            }
            catch { NameSheet = "error"; }

            return NameSheet;
        }
        private void UploadExcelNew(string Path)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string InvoiceNo = "";
                string ShipBy = "";
                string IssueDate = "";
                string IssueBy = "";
                string ETADate = "";
                string ETDDate = "";
                string VERSION = "";
                string CountrySize2 = "";
                string Country2 = "";


                ////Code here
                progressBar1.Visible = true;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  Path, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                progressBar1.Maximum = 301;
                progressBar1.Minimum = 1;
                int EndofTAG = 1;
                int Rowx =11;
                int RNo = 0;
                int countRow = 0;

                Excel.Range ExInv = worksheet.get_Range("C2");
                Excel.Range ExRev = worksheet.get_Range("I2");
                Excel.Range ExIssueName = worksheet.get_Range("I3");
                Excel.Range ExIssueDate = worksheet.get_Range("I4");
                Excel.Range ExShipBy = worksheet.get_Range("C6");
                Excel.Range ExETD = worksheet.get_Range("I5");
                Excel.Range ExETA = worksheet.get_Range("I6");
                Excel.Range CountrySize = worksheet.get_Range("C4");
                Excel.Range Country = worksheet.get_Range("C5");


                InvoiceNo = Convert.ToString(ExInv.Value2);
                VERSION = Convert.ToString(ExRev.Value2);
                IssueBy = Convert.ToString(ExIssueName.Value2);
                IssueDate = Convert.ToString(ExIssueDate.Value);
                ShipBy = Convert.ToString(ExShipBy.Value2);
                ETADate = Convert.ToString(ExETA.Value);
                ETDDate = Convert.ToString(ExETD.Value);
                Country2 = Convert.ToString(Country.Value);
                CountrySize2 = Convert.ToString(CountrySize.Value);

                

                int Check1 = 0;
                int rows = 0;

                if (txtInvoiceNo.Text.ToLower().Equals(InvoiceNo.Trim().ToLower()))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ExportList iv = db.tb_ExportLists.Where(ivs => ivs.InvoiceNo.Trim().ToLower().Equals(InvoiceNo.ToLower().Trim())).FirstOrDefault();
                        if (iv!=null)
                        {
                            //Delete Value
                          //  Check1 = 1;
                           // MessageBox.Show("มีข้อมูล Import ไว้แล้ว! \n ต้องลบออกก่อน !");
                        
                            //InsertHeader//
                            // MessageBox.Show(DateTime.FromOADate(Convert.ToDouble(ETDDate)).ToShortDateString());
                            //tb_ExportList ed = new tb_ExportList();
                            //ed.InvoiceNo = InvoiceNo;
                            //ed.LoadDate = dtDatetime.Value;
                            //ed.ShippingBy = ShipBy;
                            //ed.ETDDate = ETDDate;
                            //ed.ETADate = ETADate;
                            //ed.IssueBy = IssueBy;
                            //ed.IssueDate = IssueDate;
                            //ed.Revision = VERSION;
                            //ed.Status = "Waiting";
                            //ed.Remark = "";
                            //ed.Country = Country2;
                            //ed.CountrySize = CountrySize2;
                            //db.tb_ExportLists.InsertOnSubmit(ed);
                            //db.SubmitChanges();

                            ////Insert Line//
                            //VAriable//
                            int qty = 0;
                            int stdPack = 0;
                            int qtyofPL = 0;
                            decimal QtyOfTAG = 0;
                            int GroupP = 0;
                            //tb_ExportDetail eg = db.tb_ExportDetails.Where(ep => ep.InvoiceNo.Equals(txtInvoiceNo.Text.ToLower())).FirstOrDefault();
                            GroupP = Convert.ToInt32(db.get_MaxPalletGroup(txtInvoiceNo.Text.Trim()));
                            if (GroupP > 0)
                            {
                                GroupP += 1;
                            }

                            Rowx = 11;
                            for (int ixi = 0; ixi < 300; ixi++)
                            {
                                //while (EndofTAG == 1)
                                //{
                                try
                                {
                                    rows += 1;
                                    if (Rowx < 300)
                                    {
                                        progressBar1.Value = Rowx;
                                        progressBar1.PerformStep();
                                    }

                                    System.Array myvalues;
                                    Excel.Range range = worksheet.get_Range("A" + Rowx.ToString(), "O" + Rowx.ToString());


                                    myvalues = (System.Array)range.Cells.Value;
                                    string[] strArray = ConvertToStringArray(myvalues);
                                    
                                        if (int.TryParse(Convert.ToString(strArray[0]), out RNo))
                                        {
                                            if (!Convert.ToString(strArray[0]).Equals("") &&
                                                !Convert.ToString(strArray[0]).ToLower().Equals("end") &&
                                                !Convert.ToString(strArray[1]).Equals("")
                                                )
                                            {
                                                countRow += 1;
                                                //Insert Value///
                                                qty = 0;
                                                stdPack = 1;
                                                qtyofPL = 1;
                                                QtyOfTAG = 1;
                                                int.TryParse(Convert.ToString(strArray[3]), out qty);
                                                int.TryParse(Convert.ToString(strArray[4]), out qtyofPL);
                                                decimal.TryParse(Convert.ToString(strArray[7]), out QtyOfTAG);
                                                int.TryParse(Convert.ToString(strArray[9]), out stdPack);

                                                if (Convert.ToString(strArray[4]).Trim().Equals(""))
                                                {

                                                }
                                                else
                                                {
                                                    GroupP += 1;
                                                }


                                                tb_ExportDetail et = new tb_ExportDetail();
                                                et.InvoiceNo = InvoiceNo;
                                                et.ListNo = Convert.ToInt32(strArray[0]);
                                                et.PalletNo = Convert.ToString(strArray[14]);
                                                et.PartNo = Convert.ToString(strArray[1]);
                                                et.PartName = Convert.ToString(strArray[2]);
                                                et.Qty = qty;
                                                et.QtyOfPL = qtyofPL;
                                                et.QtyOfTAG = QtyOfTAG;
                                                et.ShipBy = "";
                                                et.ShipFlag = false;
                                                et.SNP = stdPack;
                                                et.Status = Convert.ToString(strArray[6]);
                                                et.Status2 = "";// Convert.ToString(strArray[10]);
                                                et.STDPacking = stdPack;
                                                et.SS = 1;
                                                et.ConfirmBy = "";
                                                et.ConfirmFlag = false; //Packing//
                                                et.LotNo = Convert.ToString(strArray[5]);
                                                et.Customer = Convert.ToString(strArray[8]);
                                                et.OrderNo = Convert.ToString(strArray[13]);
                                                et.GroupP = GroupP.ToString();
                                                et.ShippingDate = dtDatetime.Value;
                                            et.K = Convert.ToString(strArray[10]);
                                            et.L = Convert.ToString(strArray[11]);
                                            et.M = Convert.ToString(strArray[12]);

                                            db.tb_ExportDetails.InsertOnSubmit(et);
                                                db.SubmitChanges();


                                                //End insert//
                                            }
                                            else
                                            {
                                                EndofTAG = 0;
                                                break;

                                            }

                                        }
                                        else
                                        {
                                            EndofTAG = 0;
                                        }
                                    


                                    Rowx += 1;

                                    if (Rowx > 300 || rows > 300)
                                    {
                                        EndofTAG = 0;
                                        break;
                                    }
                                }
                                catch { EndofTAG = 0; }

                                //}
                            }

                            //excelBook.Save();

                            excelBook.Close(false);
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
                    }
                }
                else
                {
                    MessageBox.Show("Invoice Not Match!");
                }

                ////////////
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
            progressBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string AAA = "";
            AAA=GetNameSheet(txtPath.Text);
            MessageBox.Show(AAA);

        }
    }
}
