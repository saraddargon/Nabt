﻿using System;
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

namespace StockControl
{
    public partial class UploadMappingBOM : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public UploadMappingBOM(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            this.Name = "UploadMappingBOM";
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public UploadMappingBOM()
        {
            this.Name = "UploadMappingBOM";
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
            getDT();
        }
        DataTable dt_d = new DataTable();
        private void getDT()
        {
            dt_d = new DataTable();
            dt_d.Columns.Add(new DataColumn("ParentCode", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ItemCode", typeof(string)));
            dt_d.Columns.Add(new DataColumn("Qty", typeof(decimal)));
            

        }
        private void radButton1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = true;
            //openFileDialog1.FileName = "";

            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtPartFile.Text = "";
                dt_d.Rows.Clear();
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                openFileDialog1.DefaultExt = "*.xls";
                openFileDialog1.AddExtension = true;
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Excel 2003-2010  (*.xls,*.xlsx,*.csv)|*.xls;*xlsx;*.csv";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtPartFile.Text = openFileDialog1.FileName;
                    //string name = Path.GetFileName(openFileDialog1.FileName);
                    string Exten = Path.GetExtension(openFileDialog1.FileName);
                    if (Exten.ToUpper() == ".XLS" || Exten.ToUpper() == ".XLSX")
                        Import_Excel(openFileDialog1.FileName);
                    //else if (Exten.ToUpper() == ".CSV")
                    //    Import_CSV(openFileDialog1.FileName);

                    if (dt_d.Rows.Count > 0)
                        lblSS.Visible = true;
                    else
                    {
                        MessageBox.Show("can't load data import.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                progressBar1.Maximum = 1000;
                progressBar1.Minimum = 1;
                int icount = 0;

                //int Sheet4 = 0;
                for (int j = 2; j < 1003; j++)
                {
                    if (progressBar1.Value < progressBar1.Maximum)
                    {
                        progressBar1.Value = icount + 1;
                        icount = icount + 1;
                        progressBar1.PerformStep();
                    }

                    System.Array myvalues;
                    Excel.Range range = worksheet.get_Range("A" + j.ToString(), "C" + j.ToString());
                    myvalues = (System.Array)range.Cells.Value;
                    string[] strArray = ConvertToStringArray(myvalues);
                    if (!Convert.ToString(strArray[0]).Equals("")
                        && !Convert.ToString(strArray[1]).Equals("")
                        )
                    {
                        //MessageBox.Show(Convert.ToString(strArray[1]));
                        GetDataSystem2(Convert.ToString(strArray[0]).Trim() //Parent Item
                            , Convert.ToString(strArray[1]).Trim()//Child Item
                            , Convert.ToString(strArray[2]).Trim()//Qty Set                            
                            );
                    }
                    else
                        break;
                }
                //progressBar1.Value = progressBar1.Maximum;
                //progressBar1.PerformStep();
               // progressBar1.Visible = false;

                //excelBook.Save();
                //excelApp.Quit();
                releaseObject(worksheet);
                releaseObject(excelApp);
                //Marshal.FinalReleaseComObject(worksheet);


            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        int RowIndex_temp = 0;
        private void GetDataSystem2(string Code,string CustItemName, string Qty )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                RowIndex_temp = (dt_d.Rows.Count);
                DataRow dr = dt_d.NewRow();

                string Status = "OK";

                    dr["ParentCode"] = Code;             
                
                    dr["ItemCode"] = CustItemName;

                if (!Qty.Equals(""))
                {
                    dr["Qty"] = Convert.ToDecimal(Qty);
                }else
                {
                    dr["Qty"] = 0;
                }

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
            return;
            /*
            using (TextFieldParser parser = new TextFieldParser(Name, Encoding.GetEncoding("windows-874")))
            {
                this.Cursor = Cursors.WaitCursor;

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int a = 0;
                int c = 0;

                string Code, CustItemNo, CustItemName, CustomerNo = "";
                string CSTMName = "";
                while (!parser.EndOfData)
                {
                    //Processing row
                    a += 1;
                    Code = ""; CustItemNo = ""; CustItemName = ""; CustomerNo = "";
                  
                    string[] fields = parser.ReadFields();
                    c = 0;
                    foreach (string field in fields)
                    {
                        c += 1;
                        ////TODO: Process field
                        //    // MessageBox.Show(field);
                        if (a >= 2)
                        {
                            if (c == 1 && Convert.ToString(field).Equals(""))
                            {
                                break;
                            }

                            if (c == 1)
                                Code = Convert.ToString(field);
                            else if (c == 2)
                                CustItemName = Convert.ToString(field);
                            else if (c == 3)
                                CustItemNo = Convert.ToString(field);                            
                            else if (c == 4)
                                CSTMName = Convert.ToString(field);
                            else if (c == 5)
                                CustomerNo = Convert.ToString(field);
                         
                        }

                    }
                    if (Code != "")
                    {
                       // GetDataSystem2(Code,  CustItemName, CustItemNo,CSTMName, CustomerNo);
                    }

                }

            }
            */
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
        private void btnExport_Click(object sender, EventArgs e)
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
                    //radProgressBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    //radProgressBarElement1.Minimum = 0;
                    //radProgressBarElement1.Maximum = dt_d.Rows.Count;

                    foreach (DataRow dr in dt_d.Rows)
                    {

                        //DKUBU = ""; ItemCode = ""; ItemDescription = ""; Type = "";
                        //Revision = ""; ExclusionClass = ""; StorageWorkCenter = ""; StorageWorkCenterName = "";
                        //CurrentInventory = ""; InventoryValue = ""; StockBeforeInventory = ""; PhysicalInventoryValue = "";
                        //UnitOfMeasure = "";

                        //d = dr["DATE"].ToString();
                        if (!dbClss.TSt(dr["ParentCode"]).Equals("") && !dbClss.TSt(dr["ItemCode"]).Equals(""))
                        {

                            var h = (from ix in db.tb_ProductoinPartBoms
                                     where ix.Code == dbClss.TSt(dr["ParentCode"])
                                     && ix.PartNo == dbClss.TSt(dr["ItemCode"])
                                     select ix).ToList();
                            if (h.Count > 0)
                            {
                                var hh = (from ix in db.tb_ProductoinPartBoms
                                          where ix.Code == dbClss.TSt(dr["ParentCode"])
                                               && ix.PartNo == dbClss.TSt(dr["ItemCode"])
                                          select ix).First();

                                hh.QtySet = Convert.ToDecimal(dr["Qty"]);
                                db.SubmitChanges();

                            }
                            else
                            {
                                tb_ProductoinPartBom u = new tb_ProductoinPartBom();
                                u.Code = dbClss.TSt(dr["ParentCode"]);
                                u.PartNo = dbClss.TSt(dr["ItemCode"]);
                                u.QtySet = Convert.ToDecimal(dr["Qty"]);                               
                                db.tb_ProductoinPartBoms.InsertOnSubmit(u);
                                db.SubmitChanges();
                            }
                            C += 1;
                        }

                    }

                    if (C > 0)
                    {                       
                        MessageBox.Show("Import data Complete.");
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล!");
                    }

                    //radProgressBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                }
                lblSS.Visible = false;
                txtPartFile.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (MessageBox.Show("คุณต้องการลบข้อมูลทั้งหมด หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var mp = db.tb_ProductoinPartBoms.Where(m => m.Code != "").ToList();
                    if (mp.Count > 0)
                    {
                        db.tb_ProductoinPartBoms.DeleteAllOnSubmit(mp);
                        db.SubmitChanges();
                        MessageBox.Show("ลบข้อมูลเรียบร้อย");
                    }
                }
            }
            this.Cursor = Cursors.Default;

        }
    }
}
