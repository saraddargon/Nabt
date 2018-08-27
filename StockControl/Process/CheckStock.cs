using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace StockControl
{
    public partial class CheckStock : Telerik.WinControls.UI.RadRibbonForm
    {
        public CheckStock()
        {
            InitializeComponent();
        }
        public CheckStock(string CheckNo1)
        {
            InitializeComponent();
            txtCheckNo.Text = CheckNo1;
        }

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
            dt.Columns.Add(new DataColumn("edit", typeof(bool)));
            dt.Columns.Add(new DataColumn("code", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Active", typeof(bool)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateBy", typeof(string)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            RMenu3.Click += RMenu3_Click;
            RMenu4.Click += RMenu4_Click;
            RMenu5.Click += RMenu5_Click;
            RMenu6.Click += RMenu6_Click;
           // radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            //GETDTRow();
           
            
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int ck = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    radGridView1.DataSource = db.tb_CheckStockLists.Where(c => c.CheckNo == txtCheckNo.Text).ToList();
                    foreach (var x in radGridView1.Rows)
                    {
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
            int C = 0;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                radGridView1.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in radGridView1.Rows)
                    {
                        if (!Convert.ToString(g.Cells["Code"].Value).Equals("") && dbClss.TBo(g.Cells["C"].Value))
                        {
                            C += 1;
                            var h = (from ix in db.tb_CheckStockLists
                                         where ix.CheckNo == txtCheckNo.Text.Trim()
                                         && ix.Code == dbClss.TSt(g.Cells["Code"].Value)
                                         && ix.id == dbClss.TInt(g.Cells["id"].Value)
                                         && ix.Status != "Cancel"
                                         select ix).ToList();
                            if (h.Count > 0)
                            {
                                var hh = (from ix in db.tb_CheckStockLists
                                          where ix.CheckNo == txtCheckNo.Text.Trim()
                                          && ix.Code == dbClss.TSt(g.Cells["Code"].Value)
                                          && ix.id == dbClss.TInt(g.Cells["id"].Value)
                                          && ix.Status != "Cancel"
                                          select ix).First();
                                //unit1.Status = "";
                                //hh.CheckDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                                //hh.CreateBy = dbClss.UserID;
                                hh.InputQty = dbClss.TDe(g.Cells["InputQty"].Value);
                                hh.Remark = dbClss.TSt(g.Cells["Remark"].Value);
           
                                db.SubmitChanges();
                                dbClss.AddHistory(this.Name, "แก้ไข", "Update CheckStock [" + hh.CheckNo +" จำนวน Input Qty" +dbClss.TDe(g.Cells["InputQty"].Value).ToString() + "]", "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError(this.Name, ex.Message, this.Name);
                this.Cursor = Cursors.Default;
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

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
            radGridView1.ReadOnly = false;
            radGridView1.AllowAddNewRow = false;
            //btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            radGridView1.ReadOnly = false;
            //btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
           // radGridView1.ReadOnly = true;
            btnView.Enabled = false;
            //btnEdit.Enabled = true;
            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
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
                radGridView1.Rows[e.RowIndex].Cells["C"].Value = true;
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
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AddUnit();
                    DataLoad();
                }
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewClick();
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

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AddUnit();
                    DataLoad();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtCheckNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                DataLoad();
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการ Compare ใช่หรือไม่?", "Compare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Compare();
                    DataLoad();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Compare()
        {
            try
            {
                int C = 0;
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    db.sp_003_Compare_CheckStock(txtCheckNo.Text);

                    //var h = (from ix in db.tb_CheckStockLists
                    //         where ix.CheckNo == txtCheckNo.Text.Trim()
                    //         //&& ix.Code == dbClss.TSt(g.Cells["Code"].Value)
                    //         //&& ix.id == dbClss.TInt(g.Cells["id"].Value)
                    //         && ix.Status != "Cancel"
                    //         select ix).ToList();
                    //if (h.Count > 0)
                    //{
                    //    foreach (var gg in h)
                    //    {
                    //        C += 1;
                    //        var hh = (from ix in db.tb_CheckStockLists
                    //                  where ix.CheckNo == txtCheckNo.Text.Trim()
                    //                  && ix.Code == dbClss.TSt(gg.Code)
                    //                  && ix.id == dbClss.TInt(gg.id)
                    //                  && ix.Status != "Cancel"
                    //                  select ix).First();

                    //        hh.Diff = dbClss.TDe(hh.Quantity) - dbClss.TDe(hh.InputQty);
                    //        db.SubmitChanges();
                    //        //dbClss.AddHistory(this.Name, "แก้ไข", "Update CheckStock [" + hh.CheckNo + " จำนวน Input Qty" + dbClss.TDe(g.Cells["InputQty"].Value).ToString() + "]", "");
                    //    }
                    //}

                    if (C > 0)
                        MessageBox.Show("Compare complete.");
                }
            }
            catch (Exception ex) { dbClss.AddError(this.Name, ex.Message, this.Name); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
