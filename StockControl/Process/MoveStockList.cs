﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Telerik.WinControls.UI;
namespace StockControl
{
    public partial class MoveStockList : Telerik.WinControls.UI.RadRibbonForm
    {
        public MoveStockList()
        {
            
            //this.Name = "MoveStockList";
            //if (!dbClss.PermissionScreen(this.Name))
            //{
            //    MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            InitializeComponent();
            lblCount.Text = "Count 0";
            LinkPage = "";
        }

        public MoveStockList(TextBox tx)
        {
            
            InitializeComponent();
            lblCount.Text = "Count 0";
            textRQ = tx;
            LinkPage = "Link";
        }

        //private int RowView = 50;
        //private int ColView = 10;
        string LinkPage = "";
        TextBox textRQ;
        DataTable dt3 = new DataTable();
        DataTable dt = new DataTable();
        string PathFile = "";
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
            
            //dt3.Columns.Add(new DataColumn("Code", typeof(string)));
            //dt3.Columns.Add(new DataColumn("NAME", typeof(string)));
            //dt3.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            //dt3.Columns.Add(new DataColumn("SHELVES", typeof(string)));

        }
        private void Unit_Load(object sender, EventArgs e)
        {
            // RMenu3.Click += RMenu3_Click;
            //  RMenu4.Click += RMenu4_Click;
            // RMenu5.Click += RMenu5_Click;
            //  RMenu6.Click += RMenu6_Click;
            // radGridView1.ReadOnly = true;
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            dtDate1.Value = firstDayOfMonth;
            dtDate2.Value = lastDayOfMonth;
            radGridView1.AutoGenerateColumns = false;
           //  GETDTRow();


           // LoadDataDefault();
            DataLoad();
        }
        private void LoadDataDefault()
        {
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        tb_Path ph = db.tb_Paths.Where(p => p.PathCode == "Image").FirstOrDefault();
            //        if(ph!=null)
            //        {
            //            PathFile = ph.PathFile;
            //        }
            //    }
            //}
            //catch { }
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
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int ck = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string TY = "";
                    if (cboType.Text.Equals("สำหรับเบิก"))
                    {
                        TY = "A";

                    }else if(cboType.Text.Equals("สำหรับคืน"))
                    {
                        TY = "B";
                    }
                    radGridView1.DataSource = db.sp_41_MoveStockSelecItem02_LotListL2(txtItemNo.Text, txtPlant.Text, TY, dtSelectDate.Checked,dtDate1.Value,dtDate2.Value).ToList();
                    foreach (var x in radGridView1.Rows)
                    {

                        // x.Cells["dgvCodeTemp"].Value = x.Cells["UnitCode"].Value.ToString();
                        //  x.Cells["UnitCode"].ReadOnly = true;
                        //if (row >= 0 && row == ck && radGridView1.Rows.Count > 0)
                        //{

                        //    x.ViewInfo.CurrentRow = x;

                        //}
                        ck += 1;
                        x.Cells["No"].Value = ck;
                    }

                }
            }
            catch { }
            this.Cursor = Cursors.Default;

            
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
         
            //int C = 0;
            //try
            //{
                
            //    if (row >= 0)
            //    {
            //        string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["UnitCode"].Value);
            //        string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
            //        radGridView1.EndEdit();
            //        if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            using (DataClasses1DataContext db = new DataClasses1DataContext())
            //            {

            //                if (!CodeDelete.Equals(""))
            //                {
            //                    if (!CodeTemp.Equals(""))
            //                    {

            //                        var unit1 = (from ix in db.tb_Units
            //                                     where ix.UnitCode == CodeDelete
            //                                     select ix).ToList();
            //                        foreach (var d in unit1)
            //                        {
            //                            db.tb_Units.DeleteOnSubmit(d);
            //                            dbClss.AddHistory(this.Name, "ลบ Unit", "Delete Unit Code ["+d.UnitCode+"]","");
            //                        }
            //                        C += 1;



            //                        db.SubmitChanges();
            //                    }
            //                }

            //            }
            //        }
            //    }
            //}

            //catch (Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("DeleteUnit", ex.Message, this.Name);
            //}

            //if (C > 0)
            //{
            //        row = row - 1;
            //        MessageBox.Show("ลบรายการ สำเร็จ!");
            //}
              

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void NewClick()
        {
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            ////btnEdit.Enabled = false;
            //btnView.Enabled = true;
            //radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            //radGridView1.ReadOnly = false;
            ////btnEdit.Enabled = false;
            //btnView.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
           // radGridView1.ReadOnly = true;
            //btnView.Enabled = false;
            ////btnEdit.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();

            //genLOt();
        }

        private void genLOt()
        {
            string LotMap = "";
            string LotY = "";
            string LotM = "";
            string LotNo = "";
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                DateTime thisDay = DateTime.Now.AddDays(-500);
                for (int i = 1; i <= 500; i++)
                {
                    LotMap = "";
                    LotY = "";
                    LotM = "";
                    LotNo = "";
                    tb_GenerateLotMap g = db.tb_GenerateLotMaps.Where(t => t.Daysx == thisDay.Day).FirstOrDefault();
                    if (g != null)
                    {
                        LotMap = g.KeyLot;
                        LotY = thisDay.Year.ToString().Substring(3, 1);
                        LotM = thisDay.Month.ToString();


                        if (thisDay.Month == 10)
                            LotM = "X";
                        else if (thisDay.Month == 11)
                            LotM = "Y";
                        else if (thisDay.Month == 12)
                            LotM = "Z";
                        LotNo = LotY + LotM + LotMap + "T";

                        tb_LotNo nl = new tb_LotNo();
                        nl.LotNo = LotNo;
                        nl.LotDate = thisDay;
                        db.tb_LotNos.InsertOnSubmit(nl);
                        db.SubmitChanges();


                    }
                    thisDay = thisDay.AddDays(1);
                }
                MessageBox.Show("Completed");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
            //if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    AddUnit();
            //    DataLoad();
            //}
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
                if(e.ColumnIndex==radGridView1.Columns["RemarkCancel"].Index && e.RowIndex>=0)
                {
                    // radGridView1.EndEdit();
                    int id = Convert.ToInt32(radGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_RequisitionDT hd = db.tb_RequisitionDTs.Where(d => d.id == id).FirstOrDefault();
                        if(hd!=null)
                        {
                            hd.RemarkCancel = radGridView1.Rows[e.RowIndex].Cells["RemarkCancel"].Value.ToString();
                            db.SubmitChanges();
                        }
                    }
                }
                if (e.ColumnIndex == radGridView1.Columns["LotNo"].Index && e.RowIndex >= 0)
                {
                    // radGridView1.EndEdit();
                    int id = Convert.ToInt32(radGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_RequisitionDT hd = db.tb_RequisitionDTs.Where(d => d.id == id).FirstOrDefault();
                        if (hd != null)
                        {
                            if (MessageBox.Show("ต้องการเปลี่ยน Lot หรือไม่ ?", "Change Lot", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                hd.LotNo = radGridView1.Rows[e.RowIndex].Cells["LotNo"].Value.ToString();
                                db.SubmitChanges();
                            }
                        }
                    }
                }


            }
            catch(Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
            //else if (e.KeyData == (Keys.Control | Keys.N))
            //{
            //    if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        NewClick();
            //    }
            //}

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
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
           dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            if (op.ShowDialog() == DialogResult.OK)
            {


                using (TextFieldParser parser = new TextFieldParser(op.FileName))
                {
                    dt.Rows.Clear();
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int a = 0;
                    int c = 0;
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        a += 1;
                        DataRow rd = dt.NewRow();
                        // MessageBox.Show(a.ToString());
                        string[] fields = parser.ReadFields();
                        c = 0;
                        foreach (string field in fields)
                        {
                            c += 1;
                            //TODO: Process field
                            //MessageBox.Show(field);
                            if (a>1)
                            {
                                if(c==1)
                                    rd["UnitCode"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["UnitDetail"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["UnitActive"] = Convert.ToBoolean(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["UnitCode"] = "";
                                else if (c == 2)
                                    rd["UnitDetail"] = "";
                                else if (c == 3)
                                    rd["UnitActive"] = false;




                            }

                            //
                            //rd[""] = "";
                            //rd[""]
                        }
                        dt.Rows.Add(rd);

                    }
                }
                if(dt.Rows.Count>0)
                {
                    dbClss.AddHistory(this.Name, "Import", "Import file CSV in to System", "");
                    ImportData();
                    MessageBox.Show("Import Completed.");

                    DataLoad();
                }
               
            }
        }

        private void ImportData()
        {
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
                   
            //        foreach (DataRow rd in dt.Rows)
            //        {
            //            if (!rd["UnitCode"].ToString().Equals(""))
            //            {

            //                var x = (from ix in db.tb_Units where ix.UnitCode.ToLower().Trim() == rd["UnitCode"].ToString().ToLower().Trim() select ix).FirstOrDefault();

            //                if(x==null)
            //                {
            //                    tb_Unit ts = new tb_Unit();
            //                    ts.UnitCode = Convert.ToString(rd["UnitCode"].ToString());
            //                    ts.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
            //                    ts.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
            //                    db.tb_Units.InsertOnSubmit(ts);
            //                    db.SubmitChanges();
            //                }
            //                else
            //                {
            //                    x.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
            //                    x.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
            //                    db.SubmitChanges();

            //                }

                       
            //            }
            //        }
                   
            //    }
            //}
            //catch(Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("InportData", ex.Message, this.Name);
            //}
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

        private void radButtonElement2_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radButtonElement2_Click_1(object sender, EventArgs e)
        {
            if (row >= 0)
            {
                //string code = radGridView1.Rows[row].Cells["Code"].Value.ToString();
                //ItemListImage im = new ItemListImage(code);
                //im.ShowDialog();
            }

        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {               

                if (!LinkPage.Equals(""))
                {
                    textRQ.Text = radGridView1.Rows[e.RowIndex].Cells["ReqNo"].Value.ToString();
                    this.Close();
                }else
                {
                    ReqMoveStock rq = new ReqMoveStock(radGridView1.Rows[e.RowIndex].Cells["ReqNo"].Value.ToString());
                    rq.ShowDialog();
                }
            }
        }

        private void txtItemNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                DataLoad();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PrintTAG();
            this.Cursor = Cursors.Default; ;

        }
        private void PrintTAG()
        {
            //Temp TAG//
            try
            {
                //TextBox tempTag = new TextBox();
                string Code = radGridView1.Rows[row].Cells["Code"].Value.ToString();
                if (!Code.Equals(""))
                {
                    PrintTEMPTAG pt = new PrintTEMPTAG(Code);
                    pt.Show();
                }

                //Report.Reportx1.WReport = "PDTAG";
                //Report.Reportx1.Value = new string[2];
                //Report.Reportx1.Value[0] = "BomNo";
                //Report.Reportx1.Value[1] = dbClss.UserID;
                //Report.Reportx1 op = new Report.Reportx1("TEMPTAG.rpt");
                //op.Show();
            }
            catch { }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor ;
            try
            {
                if (row >= 0)
                {
                    string Code = radGridView1.Rows[row].Cells["Code"].Value.ToString();
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.RP_StockCard_Cal_Dynamics(dbClss.UserID, Code, dbClss.UserID, DateTime.Now);
                    }
                        

                    Report.Reportx1.WReport = "StockCard";
                    Report.Reportx1.Value = new string[2];
                    Report.Reportx1.Value[0] = Code;
                    Report.Reportx1.Value[1] = dbClss.UserID;
                    Report.Reportx1 op = new Report.Reportx1("StockCard.rpt");
                    op.Show();
                }
            }
            catch { }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            ////FG_TAG
            ////Report.Reportx1.WReport = "PDTAG";
            ////Report.Reportx1.Value = new string[2];
            ////Report.Reportx1.Value[0] = "BomNo";
            ////Report.Reportx1.Value[1] = dbClss.UserID;
            ////Report.Reportx1 op = new Report.Reportx1("FG_TAG.rpt");
            ////op.Show();
            //PrintPDTAG pd = new PrintPDTAG("");
            //pd.Show();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var tp = db.TempPrints.Where(t => !t.CodeNo.Equals("")).ToList();
                    if(tp!=null)
                    {
                        foreach(var rd in tp)
                        {
                            db.TempPrints.DeleteOnSubmit(rd);
                            db.SubmitChanges();
                        }

                    }

                    radGridView1.EndEdit();
                    radGridView1.EndUpdate();
                    int ck = 1;
                    int Gp = 1;
                    foreach(DataRow rd in dt3.Rows)
                    {
                       

                            if (ck == 8)
                            {
                                ck = 1;
                                Gp += 1;
                            }
                            TempPrint tm = new TempPrint();
                            tm.CodeNo = rd["Code"].ToString();
                            tm.Name = rd["NAME"].ToString();
                            tm.PLANTID = rd["PLANTID"].ToString();
                            tm.SHELVES = rd["SHELVES"].ToString();
                            tm.No = ck;
                            tm.GP = Gp;
                            db.TempPrints.InsertOnSubmit(tm);
                            db.SubmitChanges();
                            ck += 1;
                        
                    }

                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        Report.Reportx1.WReport = "TAGITEM";
                        Report.Reportx1.Value = new string[1];
                        Report.Reportx1.Value[0] = Gp.ToString();

                        Report.Reportx1 op = new Report.Reportx1("TAGItem.rpt");
                        op.Show();
                    }
                    catch { }
                    this.Cursor = Cursors.Default;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radCheckBox1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radCheckBox1.Checked)
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    rd.Cells["chk"].Value = true;
                }

            }
            else
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    rd.Cells["chk"].Value = false;
                }
            }
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                radGridView1.EndEdit();
                radGridView1.EndUpdate();
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    if(Convert.ToBoolean(rd.Cells["chk"].Value))
                    {
                        DataRow nr = dt3.NewRow();
                        nr["Code"] = rd.Cells["Code"].Value.ToString();
                        nr["NAME"]= rd.Cells["NAME"].Value.ToString();
                        nr["PLANTID"] = rd.Cells["PLANTID"].Value.ToString();
                        nr["SHELVES"] = rd.Cells["SHELVES"].Value.ToString();

                        dt3.Rows.Add(nr);

                        rd.Cells["chk"].Value = false;

                    }
                }
                lblCount.Text = "Count " + dt3.Rows.Count.ToString();
                this.Cursor = Cursors.Default;
            }
            catch { }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            dt3.Rows.Clear();
            lblCount.Text = "Count 0";
        }
    }
}
