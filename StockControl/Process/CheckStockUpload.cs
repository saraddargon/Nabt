using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace StockControl
{
    public partial class CheckStockUpload : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public CheckStockUpload(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            this.Name = "CheckStockUpload";
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public CheckStockUpload(string CheckNox)
        {
            this.Name = "CheckStockUpload";
            InitializeComponent();
            txtCheckNo.Text = CheckNox;
            screen = 1;
        }
        public CheckStockUpload()
        {
            this.Name = "CheckStockUpload";
            InitializeComponent();
        }

        string PR1 = "";
        string PR2 = "";
        string Type = "";    
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
           
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            getDT();
        }
        DataTable dt_d = new DataTable();
        private void getDT()
        {
            dt_d = new DataTable();
            dt_d.Columns.Add(new DataColumn("DKUBU", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ItemCode", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt_d.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_d.Columns.Add(new DataColumn("Revision", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ExclusionClass", typeof(string)));
            dt_d.Columns.Add(new DataColumn("StorageWorkCenter", typeof(string)));
            dt_d.Columns.Add(new DataColumn("StorageWorkCenterName", typeof(string)));
            dt_d.Columns.Add(new DataColumn("CurrentInventory", typeof(string)));
            dt_d.Columns.Add(new DataColumn("InventoryValue", typeof(string)));
            dt_d.Columns.Add(new DataColumn("StockBeforeInventory", typeof(string)));
            dt_d.Columns.Add(new DataColumn("PhysicalInventoryValue", typeof(string)));
            dt_d.Columns.Add(new DataColumn("UnitOfMeasure", typeof(string)));



        }
        private void radButton1_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (txtCheckNo.Text == "")
                {
                    MessageBox.Show("can't load data because 'Check No' is null");
                    return;
                }

                //Select File//
                //openFileDialog1.InitialDirectory = "c:\\";
                //openFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                dt_d.Rows.Clear();
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                openFileDialog1.DefaultExt = "*.xls";
                openFileDialog1.AddExtension = true;
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Excel 2003-2010  (*.xls,*.xlsx,*.csv)|*.xls;*xlsx;*.csv";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFileName.Text = openFileDialog1.FileName;
                    //string name = Path.GetFileName(openFileDialog1.FileName);
                    string Exten = Path.GetExtension(openFileDialog1.FileName);
                    if (Exten.ToUpper() == ".XLS" || Exten.ToUpper() == ".XLSX")
                        Import_Excel(openFileDialog1.FileName);
                    else if (Exten.ToUpper() == ".CSV")
                        Import_CSV(openFileDialog1.FileName);
                }

                //
                if (dt_d.Rows.Count > 0)
                    lblSS.Visible = true;
                else
                {
                    MessageBox.Show("can't load data import.");
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void Import_Excel(string Name)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook theWorkbook = excelApp.Workbooks.Open(
                  openFileDialog1.FileName, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);


                Excel.Sheets sheets = theWorkbook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                progressBar1.Visible = true;
                progressBar1.Maximum = 10003;
                progressBar1.Minimum = 1;
                int icount = 0;
             
                //int Sheet4 = 0;
                for (int j = 3; j < 10003; j++)
                {
                    if (progressBar1.Value < progressBar1.Maximum)
                    {
                        progressBar1.Value = icount + 1;
                        icount = icount + 1;
                        progressBar1.PerformStep();
                    }

                    System.Array myvalues;
                    Excel.Range range = worksheet.get_Range("A" + j.ToString(), "N" + j.ToString());
                    myvalues = (System.Array)range.Cells.Value;
                    string[] strArray = ConvertToStringArray(myvalues);
                    if (!Convert.ToString(strArray[1]).Equals("")
                        //!Convert.ToString(strArray[2]).Equals("")
                        )
                    {
                        GetDataSystem2(Convert.ToString(strArray[0]).Trim() //DKUBU
                            , Convert.ToString(strArray[1]).Trim()//ItemCode
                            , Convert.ToString(strArray[2]).Trim()//ItemDescription
                            , Convert.ToString(strArray[3]).Trim()//Type  
                            , Convert.ToString(strArray[4]).Trim()//Revision       
                            , Convert.ToString(strArray[5]).Trim()//ExclusionClass       
                            , Convert.ToString(strArray[6]).Trim()//StorageWorkCenter       
                            , Convert.ToString(strArray[7]).Trim()//StorageWorkCenterName       
                            , Convert.ToString(strArray[8]).Trim()//CurrentInventory       
                            , Convert.ToString(strArray[9]).Trim()//InventoryValue       
                            , Convert.ToString(strArray[10]).Trim()//StockBeforeInventory
                            , Convert.ToString(strArray[11]).Trim()//PhysicalInventoryValue
                            , Convert.ToString(strArray[12]).Trim()//UnitOfMeasure

                            );
                    }
                    else
                        break;
                }
                progressBar1.Value = progressBar1.Maximum;
                progressBar1.PerformStep();
                progressBar1.Visible = false;

                //excelBook.Save();
                //excelApp.Quit();
                releaseObject(worksheet);

                releaseObject(excelApp);
                //Marshal.FinalReleaseComObject(worksheet);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }
        int RowIndex_temp = 0;
        private void GetDataSystem2(string DKUBU, string ItemCode, string ItemDescription, string Type
            ,string Revision,string ExclusionClass,string StorageWorkCenter,string StorageWorkCenterName
            ,string CurrentInventory,string InventoryValue,string StockBeforeInventory,string PhysicalInventoryValue
            ,string UnitOfMeasure)

        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                RowIndex_temp = (dt_d.Rows.Count);
                DataRow dr = dt_d.NewRow();

                string Status = "OK";
   
                dr["DKUBU"] = DKUBU;
                if (dbClss.TSt(ItemCode).Trim() != "")
                    dr["ItemCode"] = ItemCode;
                else
                    Status = "NG";
                if (dbClss.TSt(ItemDescription).Trim() != "" && Status =="OK")
                    dr["ItemDescription"] = ItemDescription;
                else
                    Status = "NG";
                if (dbClss.TSt(Type).Trim() != "" && Status == "OK")
                    dr["Type"] = Type;
                else
                    Status = "NG";

                dr["Revision"] = Revision;
                dr["ExclusionClass"] = ExclusionClass;
                if (dbClss.TSt(StorageWorkCenter).Trim() != "" && Status == "OK")
                    dr["StorageWorkCenter"] = StorageWorkCenter;
                else
                    Status = "NG";

                if (dbClss.TSt(StorageWorkCenterName).Trim() != "" && Status == "OK")
                    dr["StorageWorkCenterName"] = StorageWorkCenterName;
                else
                    Status = "NG";
                
                dr["CurrentInventory"] = CurrentInventory;
                dr["InventoryValue"] = InventoryValue;
                dr["StockBeforeInventory"] = StockBeforeInventory;
                dr["PhysicalInventoryValue"] = PhysicalInventoryValue;
                if (dbClss.TSt(UnitOfMeasure).Trim() != "" && Status == "OK")
                    dr["UnitOfMeasure"] = UnitOfMeasure;
                else
                    Status = "NG";


                if (Status == "OK")
                {
                    dt_d.Rows.Add(dr);
                    RowIndex_temp += 1;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            //finally { this.Cursor = Cursors.Default; }
        }

        private void Import_CSV(string Name)
        {
            using (TextFieldParser parser = new TextFieldParser(Name, Encoding.GetEncoding("windows-874")))
            {
                this.Cursor = Cursors.WaitCursor;

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int a = 0;
                int c = 0;

                string DKUBU,  ItemCode,  ItemDescription,  Type
             , Revision, ExclusionClass, StorageWorkCenter, StorageWorkCenterName
             , CurrentInventory, InventoryValue, StockBeforeInventory, PhysicalInventoryValue
             , UnitOfMeasure = "";
                
                while (!parser.EndOfData)
                {
                    //Processing row
                    a += 1;
                    DKUBU = ""; ItemCode = ""; ItemDescription = ""; Type = "";
                    Revision = ""; ExclusionClass = ""; StorageWorkCenter = ""; StorageWorkCenterName = "";
                    CurrentInventory = ""; InventoryValue = ""; StockBeforeInventory = ""; PhysicalInventoryValue = "";
                    UnitOfMeasure = "";

                    string[] fields = parser.ReadFields();
                    c = 0;
                    foreach (string field in fields)
                    {
                        c += 1;
                        ////TODO: Process field
                        //    // MessageBox.Show(field);
                        if (a >= 3)
                        {
                            if (c == 2 && Convert.ToString(field).Equals(""))
                            {
                                break;
                            }

                            if (c == 1)
                                DKUBU = Convert.ToString(field);
                            else if (c == 2)
                                ItemCode = Convert.ToString(field);
                            else if (c == 3)
                                ItemDescription = Convert.ToString(field);
                            else if (c == 4)
                                Type = Convert.ToString(field);
                            else if (c == 5)
                                Revision = Convert.ToString(field);
                            else if (c == 6)
                                ExclusionClass = Convert.ToString(field);
                            else if (c == 7)
                                StorageWorkCenter = Convert.ToString(field);
                            else if (c == 8)
                                StorageWorkCenterName = Convert.ToString(field);
                            else if (c == 9)
                                CurrentInventory = Convert.ToString(field);
                            else if (c == 10)
                                InventoryValue = Convert.ToString(field);
                            else if (c == 11)
                                StockBeforeInventory = Convert.ToString(field);
                            else if (c == 12)
                                PhysicalInventoryValue = Convert.ToString(field);
                            else if (c == 13)
                                UnitOfMeasure = Convert.ToString(field);
                        }

                    }
                    if (ItemCode != "" && ItemDescription !="" && Type !="" 
                        && StorageWorkCenter !="" && StorageWorkCenterName !="" && UnitOfMeasure !="")
                    {
                        GetDataSystem2(DKUBU,ItemCode,ItemDescription,Type,Revision,ExclusionClass,StorageWorkCenter,StorageWorkCenterName,CurrentInventory
                            ,InventoryValue,StockBeforeInventory,PhysicalInventoryValue,UnitOfMeasure                                      
                             );
                    }
                    
                }
                
            }
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
        private void btnExport_Click(object sender, EventArgs e)
        {
            //tpics//UploadExcelTpics()
            UploadByDynamics();
        }
        private void UploadExcelTpics()
        {
            //Upload

            try
            {
                // tb_CheckStockList <= Insert to this table
                //Update Status tb_CheckStock to "Waiting Check"
                //สามารถอัพโหลดใหม่ได้ โดยการ ให้ลบ ข้อมูลเก่าทั้งหมดออกก่อน

                //    string DKUBU, ItemCode, ItemDescription, Type
                //, Revision, ExclusionClass, StorageWorkCenter, StorageWorkCenterName
                //, CurrentInventory, InventoryValue, StockBeforeInventory, PhysicalInventoryValue
                //, UnitOfMeasure = "";

                int C = 0;
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_CheckStock tc = db.tb_CheckStocks.Where(c => c.CheckNo == txtCheckNo.Text && c.Status != "Completed").FirstOrDefault();
                    if (tc != null)
                    {

                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt_d.Rows.Count + 1;
                        progressBar1.Visible = true;
                        progressBar1.Step = 1;

                        var Dl = db.tb_CheckStockLists.Where(w => w.CheckNo == txtCheckNo.Text).ToList();
                        db.tb_CheckStockLists.DeleteAllOnSubmit(Dl);
                        db.SubmitChanges();

                        foreach (DataRow dr in dt_d.Rows)
                        {

                            //DKUBU = ""; ItemCode = ""; ItemDescription = ""; Type = "";
                            //Revision = ""; ExclusionClass = ""; StorageWorkCenter = ""; StorageWorkCenterName = "";
                            //CurrentInventory = ""; InventoryValue = ""; StockBeforeInventory = ""; PhysicalInventoryValue = "";
                            //UnitOfMeasure = "";
                            //d = dr["DATE"].ToString();

                            if (dbClss.TSt(dr["StorageWorkCenter"]).Equals("WH01"))
                            {
                                //tb_CheckStockList ck = db.tb_CheckStockLists.Where(cp => cp.CheckNo == txtCheckNo.Text).FirstOrDefault();

                                //if (ck != null)
                                //{

                                //}
                                //else
                                //{

                                tb_CheckStockList u = new tb_CheckStockList();
                                u.CheckNo = txtCheckNo.Text.Trim();
                                u.Status = "Waiting";
                                u.Code = dbClss.TSt(dr["ItemCode"]);
                                u.PartName = dbClss.TSt(dr["ItemDescription"]);
                                u.Type = dbClss.TSt(dr["Type"]);
                                u.Location = dbClss.TSt(dr["StorageWorkCenter"]);
                                u.Revision = dbClss.TInt(dr["Revision"]);
                                u.ExclusionClass = dbClss.TInt(dr["ExclusionClass"]);
                                u.StorageWorkCenter = dbClss.TSt(dr["StorageWorkCenter"]);
                                u.StorageWorkCenterName = dbClss.TSt(dr["StorageWorkCenterName"]);
                                u.CurrentInventory = dbClss.TDe(dr["CurrentInventory"]);
                                u.InventoryValue = dbClss.TDe(dr["InventoryValue"]);
                                u.StockBeforeInventory = dbClss.TDe(dr["StockBeforeInventory"]);
                                u.PhysicalInventoryValue = 0;//dbClss.TDe(dr["PhysicalInventoryValue"]);
                                u.UnitOfMeasure = dbClss.TSt(dr["UnitOfMeasure"]);
                                u.Quantity = dbClss.TDe(dr["CurrentInventory"]);
                                u.Plant = db.getPlanTIDTPICS_Dynamics(dbClss.TSt(dr["ItemCode"]));
                                u.InputQty = 0;
                                u.Remark = "";
                                u.Diff = 0;

                                db.tb_CheckStockLists.InsertOnSubmit(u);
                                db.SubmitChanges();
                                //}
                                C += 1;
                            }

                            progressBar1.Value = C;
                            progressBar1.PerformStep();

                        }

                        if (C > 0)
                        {
                            var h = (from ix in db.tb_CheckStocks
                                     where ix.CheckNo == txtCheckNo.Text.Trim()
                                     select ix).ToList();
                            if (h.Count > 0)
                            {
                                var hh = (from ix in db.tb_CheckStocks
                                          where ix.CheckNo == txtCheckNo.Text.Trim()
                                          select ix).First();
                                //unit1.Status = "";
                                hh.CheckDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                                hh.CreateBy = dbClss.UserID;
                                hh.Status = "Waiting Check";
                                db.SubmitChanges();

                                dbClss.AddHistory(this.Name, "แก้ไข", "Import CheckStock [" + hh.CheckNo + "]", "");
                            }

                            MessageBox.Show("Import data Complete.");
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูล!");
                        }

                        //radProgressBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("เอกสารเสร็จสิ้นแล้ว แก้ไขไฟล์ไม่ได้!");
                    }

                }
                lblSS.Visible = false;
                txtFileName.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void UploadByDynamics()
        {
            //Upload

            try
            {
                // tb_CheckStockList <= Insert to this table
                //Update Status tb_CheckStock to "Waiting Check"
                //สามารถอัพโหลดใหม่ได้ โดยการ ให้ลบ ข้อมูลเก่าทั้งหมดออกก่อน

                //    string DKUBU, ItemCode, ItemDescription, Type
                //, Revision, ExclusionClass, StorageWorkCenter, StorageWorkCenterName
                //, CurrentInventory, InventoryValue, StockBeforeInventory, PhysicalInventoryValue
                //, UnitOfMeasure = "";

                int C = 0;
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_CheckStock tc = db.tb_CheckStocks.Where(c => c.CheckNo == txtCheckNo.Text && c.Status != "Completed").FirstOrDefault();
                    if (tc != null)
                    {
                        var Clist = db.sp_ZDynamics_UploadCheckStock_Dynamics(txtCheckNo.Text).ToList();
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = Clist.Count + 1;
                        progressBar1.Visible = true;
                        progressBar1.Step = 1;

                        //var Dl = db.tb_CheckStockLists.Where(w => w.CheckNo == txtCheckNo.Text).ToList();
                        //db.tb_CheckStockLists.DeleteAllOnSubmit(Dl);
                        //db.SubmitChanges();
                       

                        foreach (var dr in Clist)
                        {

                            //DKUBU = ""; ItemCode = ""; ItemDescription = ""; Type = "";
                            //Revision = ""; ExclusionClass = ""; StorageWorkCenter = ""; StorageWorkCenterName = "";
                            //CurrentInventory = ""; InventoryValue = ""; StockBeforeInventory = ""; PhysicalInventoryValue = "";
                            //UnitOfMeasure = "";
                            //d = dr["DATE"].ToString();

                            if (!dr.Item_No_.Equals(""))
                            {
                           

                                tb_CheckStockList u = new tb_CheckStockList();
                                u.CheckNo = txtCheckNo.Text.Trim();
                                u.Status = "Waiting";
                                u.Code = dr.Item_No_;
                                u.PartName = dr.Description;
                                u.Type = dr.Type1;
                                u.Location = dr.Location_Code;
                                u.Revision = 0;// dbClss.TInt(dr["Revision"]);
                                u.ExclusionClass = 0;
                                u.StorageWorkCenter = dr.Location_Code;
                                u.StorageWorkCenterName = dr.WorkCenter;
                                u.CurrentInventory = dbClss.TDe(dr.Qty);
                                u.InventoryValue = 0;// dbClss.TDe(dr["InventoryValue"]);
                                u.StockBeforeInventory = 0;// dbClss.TDe(dr["StockBeforeInventory"]);
                                u.PhysicalInventoryValue = 0;//dbClss.TDe(dr["PhysicalInventoryValue"]);
                                u.UnitOfMeasure = dr.Unit_of_Measure_Code;
                                u.Quantity = dbClss.TDe(dr.Qty);
                                u.Plant = dr.Plant;// db.getPlanTIDTPICS(dbClss.TSt(dr["ItemCode"]));
                                u.InputQty = 0;
                                u.Remark = "";
                                u.Diff = 0;

                                db.tb_CheckStockLists.InsertOnSubmit(u);
                                db.SubmitChanges();
                                //}
                                C += 1;
                            }

                            progressBar1.Value = C;
                            progressBar1.PerformStep();

                        }

                        if (C > 0)
                        {
                            var h = (from ix in db.tb_CheckStocks
                                     where ix.CheckNo == txtCheckNo.Text.Trim()
                                     select ix).ToList();
                            if (h.Count > 0)
                            {
                                var hh = (from ix in db.tb_CheckStocks
                                          where ix.CheckNo == txtCheckNo.Text.Trim()
                                          select ix).First();
                                //unit1.Status = "";
                                hh.CheckDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                                hh.CreateBy = dbClss.UserID;
                                hh.Status = "Waiting Check";
                                db.SubmitChanges();

                                dbClss.AddHistory(this.Name, "แก้ไข", "Import CheckStock [" + hh.CheckNo + "]", "");
                            }

                            MessageBox.Show("Import data Complete.");
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูล!");
                        }

                        //radProgressBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("เอกสารเสร็จสิ้นแล้ว แก้ไขไฟล์ไม่ได้!");
                    }

                }
                lblSS.Visible = false;
                txtFileName.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
