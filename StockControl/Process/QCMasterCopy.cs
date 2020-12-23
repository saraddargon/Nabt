﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
namespace StockControl
{
    public partial class QCMasterCopy : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCMasterCopy()
        {
            InitializeComponent();
        }
        public QCMasterCopy(string Code,string ISOx)
        {
            InitializeComponent();
            txtPartTo.Text = Code;
            ISO = ISOx;
            
        }
        string UserIDEdit = "";
        string ISO = "";
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
            
        }
        private void Unit_Load(object sender, EventArgs e)
        {

           
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
           // DeleteUnit();
            //DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            //EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
           // ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
           // NewClick();

        }

        private void DataLoad()
        {
           
            
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
            //DataLoad();
        }
        private void NewClick()
        {
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //btnEdit.Enabled = false;
           // btnView.Enabled = true;
           // radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
          //  radGridView1.ReadOnly = false;
            //btnEdit.Enabled = false;
            //btnView.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
           // radGridView1.ReadOnly = true;
           // btnView.Enabled = false;
            //btnEdit.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            //EditClick();
        }
        private void Saveclick()
        {
            try
            {
                if (MessageBox.Show("ต้องการ copy หรือไม่ ?", "copy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!txtPartTo.Text.Equals("") && !txtPartFrom.Text.Equals(""))
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_46_QCDeleteMaster01(ISO, txtPartTo.Text);
                            db.sp_46_QCMaster_Copy(ISO, txtPartFrom.Text, txtPartTo.Text);
                            MessageBox.Show("Completed.");
                        }
                    }
                }
            }
            catch { }
           
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
                //    //AddUnit();
                //    //DataLoad();
                //}
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                //if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    //NewClick();
                //}
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
          // dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
           
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
            //radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
           // radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
