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
using System.IO;
using System.Data.OleDb;

namespace StockControl
{
    public partial class QCSetupMaster : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCSetupMaster(string Code)
        {
            this.Name = "QCSetupMaster";
            //if (!dbClss.PermissionScreen(this.Name))
            //{
            //    MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            InitializeComponent();
            PartNo = Code;
            txtPartNo.Text = Code.ToUpper();
        }
        string PartNo = "";
        //private int RowView = 50;
        //private int ColView = 10;
        DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            // dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        string PathFile = "";
        private void Unit_Load(object sender, EventArgs e)
        {
            RMenu3.Click += RMenu3_Click;
            RMenu4.Click += RMenu4_Click;
            RMenu5.Click += RMenu5_Click;
            RMenu6.Click += RMenu6_Click;
            radGridView2.AutoGenerateColumns = true;
           // radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            GETDTRow();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                tb_Path ph = db.tb_Paths.Where(p => p.PathCode.Equals("QCImage")).FirstOrDefault();
                if(ph!=null)
                {
                    PathFile = ph.PathFile;
                }

            }

                DataLoad();
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {

            DeleteUnit();
            DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
            NewClick();

        }

        private void DataLoad()
        {

           
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
              //  radGridView1.AutoGenerateColumns = true;
                radGridView1.DataSource = db.sp_46_QCMaster(cboISO.Text, txtPartNo.Text).ToList();
                    //db.tb_QCGroupParts.Where(p => p.FormISO.Equals(cboISO.Text) && p.PartNo.Equals(txtPartNo.Text)).ToList();

                //int ck1 = 1;
                //foreach (var x in radGridView1.Rows)
                //{
                //    x.Cells["No"].Value = ck1;
                //    ck1 += 1;
                //}
            }



        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            //using (DataClasses1DataContext db = new DataClasses1DataContext())
            //{
            //    int i = (from ix in db.tb_Units where ix.UnitCode == code select ix).Count();
            //    if (i > 0)
            //        ck = false;
            //    else
            //        ck = true;
            //}
            return ck;
        }

        private bool AddUnit()
        {
            bool ck = false;
            //int C = 0;
            //try
            //{


            //    radGridView1.EndEdit();
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        foreach (var g in radGridView1.Rows)
            //        {
            //            if (!Convert.ToString(g.Cells["UnitCode"].Value).Equals(""))
            //            {
            //                if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
            //                {

            //                    if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
            //                    {
            //                       // MessageBox.Show("11");

            //                        tb_Unit u = new tb_Unit();
            //                        u.UnitCode = Convert.ToString(g.Cells["UnitCode"].Value);
            //                        u.UnitActive = Convert.ToBoolean(g.Cells["UnitActive"].Value);
            //                        u.UnitDetail= Convert.ToString(g.Cells["UnitDetail"].Value);
            //                        db.tb_Units.InsertOnSubmit(u);
            //                        db.SubmitChanges();
            //                        C += 1;
            //                        dbClss.AddHistory(this.Name, "เพิ่ม", "Insert Unit Code [" + u.UnitCode+"]","");
            //                    }
            //                    else
            //                    {

            //                        var unit1 = (from ix in db.tb_Units
            //                                     where ix.UnitCode == Convert.ToString(g.Cells["dgvCodeTemp"].Value)
            //                                     select ix).First();
            //                           unit1.UnitDetail = Convert.ToString(g.Cells["UnitDetail"].Value);
            //                           unit1.UnitActive = Convert.ToBoolean(g.Cells["UnitActive"].Value);

            //                        C += 1;

            //                        db.SubmitChanges();
            //                        dbClss.AddHistory(this.Name, "แก้ไข", "Update Unit Code [" + unit1.UnitCode+"]","");

            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("AddUnit", ex.Message, this.Name);
            //}

            //if (C > 0)
            //    MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;

            try
            {
                if (MessageBox.Show("คุณต้องการลบรายการ หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                   
                      //  string UserID = radGridView1.Rows[row].Cells["dgvUserID"].Value.ToString();
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_46_QCDeleteMaster01(cboISO.Text, txtPartNo.Text);
                            MessageBox.Show("ลบเรียบร้อย");
                        }

                    
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }



            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void NewClick()
        {
            radGridView1.ReadOnly = true;
            radGridView1.AllowAddNewRow = false;
            UserAdd ua = new UserAdd();
            ua.ShowDialog();
            DataLoad();
            // btnEdit.Enabled = false;
            // btnView.Enabled = true;
            // radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            radGridView1.ReadOnly = true;
            try
            {
                if (row >= 0)
                {
                    string userid = radGridView1.Rows[row].Cells["dgvUserID"].Value.ToString();
                    UserAdd ua = new UserAdd(userid);
                    ua.ShowDialog();
                    DataLoad();
                }
            }
            catch { }
        }
        private void ViewClick()
        {
            radGridView1.ReadOnly = true;
            //btnView.Enabled = false;
            // btnEdit.Enabled = true;
            // radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                int CC = 0;
                decimal Value1 = 0;
                decimal Value2 = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var rd in radGridView1.Rows)
                    {
                        id = 0;
                        int.TryParse(rd.Cells["id"].Value.ToString(), out id);
                        decimal.TryParse(Convert.ToString(rd.Cells["Value1"].Value), out Value1);
                        decimal.TryParse(Convert.ToString(rd.Cells["Value2"].Value), out Value2);
                        if (id > 0)
                        {
                            db.sp_46_QCMaster_Edit(id, Convert.ToString(rd.Cells["TopPic"].Value), Convert.ToString(rd.Cells["SetData"].Value));
                            //Update Value2
                            db.sp_46_QCMaster_Edit2(id, Value1, Value2);

                            CC += 1;
                        }
                    }
                }
                
                if (CC>0) 
                     MessageBox.Show("บันทึกสำเร็จ");
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            QCMasterCopy mc = new QCMasterCopy(txtPartNo.Text.ToUpper(), cboISO.Text.ToUpper());
            mc.ShowDialog();
            DataLoad(); 

        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AddUnit();
                DataLoad();
            }
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                //radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
                //string check1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["UnitCode"].Value);
                //string TM= Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                //if (!check1.Trim().Equals("") && TM.Equals(""))
                //{
                    
                //    if (!CheckDuplicate(check1.Trim()))
                //    {
                //        MessageBox.Show("ข้อมูล รหัสหน่วย ซ้ำ");
                //        radGridView1.Rows[e.RowIndex].Cells["UnitCode"].Value = "";
                //        radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                //    }
                //}
        

            }
            catch(Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyData == (Keys.Control | Keys.S))
            {
                //if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    AddUnit();
                //    DataLoad();
                //}
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                  //  NewClick();
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                DeleteUnit();
                DataLoad();
            
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
            if(e.RowIndex>=0)
            {
                try
                {
                    lblSeq.Text = "ลำดับที่ " + radGridView1.Rows[e.RowIndex].Cells["Seq"].Value.ToString() ;
                }
                catch { }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("ต้องการออก Templatae ?","Template",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    ExportData();
                }

            }
            catch { }
           
        }
        private void ExportData()
        {
            try
            {
                string  DATA = AppDomain.CurrentDomain.BaseDirectory;               
                string  tempPath = System.IO.Path.GetTempPath();
                string FileName = "TeplateQC1.xlsx";
                string tempfile = tempPath + FileName;
                DATA = DATA + @"QC\" + FileName;

                if (File.Exists(tempfile))
                {
                    try
                    {
                        File.Delete(tempfile);
                    }
                    catch { }
                }
                //File.Copy(DATA, tempfile);

                //progressBar1.Visible = true;
                
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  DATA, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);
               
               // progressBar1.Maximum = 51;
               // progressBar1.Minimum = 1;
                int row1 = 1;
                int Seq = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    for(int j= 0;j <= 50;j++)
                    {
                        row1 += 1;
                        Excel.Range Col0 = worksheet.get_Range("B" + row1.ToString(), "B" + row1.ToString());
                       // Excel.Range Col1 = worksheet.get_Range("E" + row1.ToString(), "E" + row1.ToString());
                        Excel.Range Col2 = worksheet.get_Range("F" + row1.ToString(), "F" + row1.ToString());
                        Excel.Range Col3 = worksheet.get_Range("C" + row1.ToString(), "C" + row1.ToString());
                        string Value1 = Convert.ToString(Col0.Value2);
                        if(Value1==null)
                        {
                            Value1 = "";
                        }
                        if (!Convert.ToString(Value1).Equals(""))
                        {
                            Seq = 0;
                            int.TryParse(Value1, out Seq);
                            Col2.Value =  db.QC_GetTemplate(cboISO.Text, txtPartNo.Text, Seq);
                            Col3.Value = txtPartNo.Text.ToUpper();
                           
                        }

                    }
                }
               
                excelBook.SaveAs(tempfile);
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
              System.Diagnostics.Process.Start(tempfile);

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

       

        private void btnImport_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.xlsx)|*.xlsx";
            if (op.ShowDialog() == DialogResult.OK)
            {
                ImportData(op.FileName);
            }
            DataLoad();
            
        }

        private void ImportData(string Path)
        {
            try
            {

                progressBar1.Visible = true;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  Path, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);
                progressBar1.Maximum = 61;
                progressBar1.Minimum = 1;
                int row1 = 0;
                int RNo = 0;
                decimal Value1 = 0;
                decimal Value2 = 0;
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_46_QCDeleteMaster01(cboISO.Text, txtPartNo.Text);
                        for (int ixi = 0; ixi <= 60; ixi++)
                        {
                            row1 += 1;
                            progressBar1.Value = row1;
                            progressBar1.PerformStep();
                            System.Array myvalues;
                            Excel.Range range = worksheet.get_Range("A" + row1.ToString(), "J" + row1.ToString());
                            myvalues = (System.Array)range.Cells.Value;

                            string[] strArray = ConvertToStringArray(myvalues);
                            if (int.TryParse(Convert.ToString(strArray[1]), out RNo))
                            {
                                Value2 = 0;
                                Value1 = 0;
                                decimal.TryParse(Convert.ToString(strArray[8]), out Value1);
                                decimal.TryParse(Convert.ToString(strArray[9]), out Value2);

                                if (!Convert.ToString(strArray[0]).Equals("")
                                    && !Convert.ToString(strArray[1]).Equals("")
                                    && !Convert.ToString(strArray[2]).Equals("")
                                    && !Convert.ToString(strArray[3]).Equals(""))
                                {

                                    tb_QCGroupPart qg = new tb_QCGroupPart();
                                    qg.FormISO = cboISO.Text.ToUpper();
                                    qg.PartNo = txtPartNo.Text.ToUpper();
                                    qg.Seq = Convert.ToInt32(strArray[1]);
                                    qg.SetData = Convert.ToString(strArray[5]);
                                    qg.StepPart = Convert.ToString(strArray[3]);
                                    qg.TopPic2 = "";
                                    qg.GroupPart = Convert.ToString(strArray[7]);
                                    qg.SetDate2 = Convert.ToString(strArray[6]);
                                    qg.TopPic = Convert.ToString(strArray[4]);
                                    qg.Value1 = Value1;
                                    qg.Value2 = Value2;
                                    qg.Stamp = "";
                                    // qg.Stamp = dbClss.Right(txtPartNo.Text, 7);

                                    db.tb_QCGroupParts.InsertOnSubmit(qg);
                                    db.SubmitChanges();

                                }
                            }
                        }//for

                    }
                }
                catch { }
                   
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
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
            progressBar1.Visible = false;

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
        private void btnFilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if(row>=0)
            {
                string uid = radGridView1.Rows[row].Cells["id"].Value.ToString();
                if(uid!="")
                {
                    
                    try
                    {
                        if (MessageBox.Show("ต้องการลบหรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            //  PictureBox px = null;
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                tb_QCGroupPart us = db.tb_QCGroupParts.Where(u => u.id.Equals(uid)).FirstOrDefault();
                                if (us != null)
                                {
                                    us.Image1 = "";
                                    db.SubmitChanges();
                                }
                                DataLoad();
                            }
                        }
                        
                    }
                    catch { }
                }
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files (*.PNG)|*.PNG|JPEG files (*.JPEG)|*.JPEG";
            if (op.ShowDialog() == DialogResult.OK)
            {

                txtBoxImage.Text = op.FileName;               
                //string Ex = System.IO.Path.GetExtension(txtBoxImage.Text);
                //txtImage.Text = txtItemNo.Text + "" + Ex;

            }
        }

        private void btnAddimg_Click(object sender, EventArgs e)
        {
            int id = 0;
            
            if(row>=0 && !txtBoxImage.Text.Equals(""))
            {
                //FormISO
                //PartNo
                //Seq
                int.TryParse(radGridView1.Rows[row].Cells["id"].Value.ToString(), out id);
                if (id > 0)
                {
                    if (MessageBox.Show("ต้องการบันทึกรูป ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string FileName = radGridView1.Rows[row].Cells["FormISO"].Value.ToString();
                        FileName += "_" + radGridView1.Rows[row].Cells["PartNo"].Value.ToString();
                        FileName += "_" + radGridView1.Rows[row].Cells["Seq"].Value.ToString();
                        string Ex = System.IO.Path.GetExtension(txtBoxImage.Text);
                        FileName += Ex;
                        try
                        {
                            if (File.Exists(PathFile + FileName))
                            {
                                try
                                {
                                    File.Delete(PathFile + FileName);
                                }
                                catch { }

                            }
                            ////Insert Image
                            File.Copy(txtBoxImage.Text, PathFile + FileName, true);
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                tb_QCGroupPart gp = db.tb_QCGroupParts.Where(g => g.id.Equals(id)).FirstOrDefault();
                                if(gp!=null)
                                {
                                    gp.Image1 = FileName;
                                    db.SubmitChanges();
                                }
                            }

                            DataLoad();

                        }
                        catch { }
                    }
                }

            }
        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == radGridView1.Columns["Image1"].Index)
                    {
                        string FileN = radGridView1.Rows[e.RowIndex].Cells["Image1"].Value.ToString();
                        if (!FileN.Equals("") && !PathFile.Equals(""))
                        {
                            System.Diagnostics.Process.Start(PathFile+FileN);
                        }
                    }
                }
            }
            catch { }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var tp = db.TempPrints.Where(t => !t.CodeNo.Equals("")).ToList();
                    if (tp != null)
                    {
                        foreach (var rd in tp)
                        {
                            db.TempPrints.DeleteOnSubmit(rd);
                            db.SubmitChanges();
                        }

                    }

                    radGridView1.EndEdit();
                    radGridView1.EndUpdate();
                    int ck = 1;
                    int Gp = 1;
                    foreach (var rd in radGridView1.Rows)
                    {


                        //if (ck == 8)
                        //{
                        //    ck = 1;
                        //    Gp += 1;
                        //}
                        TempPrint tm = new TempPrint();
                        tm.CodeNo = "QC Machine";
                        tm.Name = rd.Cells["StepPart"].Value.ToString();
                        tm.PLANTID = rd.Cells["TopPic"].Value.ToString();
                        tm.SHELVES = "";
                        tm.No = Convert.ToInt32(rd.Cells["Seq"].Value.ToString());
                        tm.GP = Gp;
                        db.TempPrints.InsertOnSubmit(tm);
                        db.SubmitChanges();
                        ck += 1;

                    }

                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        Report.Reportx1.WReport = "QCQRCode";
                        Report.Reportx1.Value = new string[1];
                      //  Report.Reportx1.Value[0] = Gp.ToString();
                        Report.Reportx1 op = new Report.Reportx1("QCQRCode.rpt");
                        op.Show();
                    }
                    catch { }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridXlSX(radGridView1);
            if(MessageBox.Show("Export ออกทั้งหมด","All Data",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    try
                    {
                        radGridView2.DataSource = null;
                        radGridView2.DataSource = db.sp_46_QCMasterALL();
                        dbClss.ExportGridXlSX(radGridView2);
                    }
                    catch { }
                }
            }          
            
        }
    }
}
