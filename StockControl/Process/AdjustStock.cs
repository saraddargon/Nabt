using System;
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
    public partial class AdjustStock : Telerik.WinControls.UI.RadRibbonForm
    {
        public AdjustStock()
        {
            InitializeComponent();
        }

        string Ac = "";
        DataTable dt_ADH = new DataTable();
        DataTable dt_ADD = new DataTable();

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
            //dt.Columns.Add(new DataColumn("YYYY", typeof(int)));
            //dt.Columns.Add(new DataColumn("ModelName", typeof(string)));
            //dt.Columns.Add(new DataColumn("JAN", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("FEB", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MAR", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("APR", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MAY", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("JUN", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("JUL", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("AUG", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("SEP", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("OCT", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("NOV", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("DEC", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("Active", typeof(bool)));
           
        }
        int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {
            //dgvData.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            //GETDTRow();
   
           // DefaultItem();
          
            DataLoad();

           
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //cboVendor.AutoCompleteMode = AutoCompleteMode.Append;
                //cboVendor.DisplayMember = "VendorName";
                //cboVendor.ValueMember = "VendorNo";
                //cboVendor.DataSource = (from ix in db.tb_Vendors.Where(s => s.Active == true)
                //                        select new { ix.VendorNo,ix.VendorName,ix.CRRNCY }).ToList();
                //cboVendor.SelectedIndex = 0;


                try
                {

                    

                    //GridViewMultiComboBoxColumn col = (GridViewMultiComboBoxColumn)radGridView1.Columns["CodeNo"];
                    //col.DataSource = (from ix in db.tb_Items.Where(s => s.Status.Equals("Active")) select new { ix.CodeNo, ix.ItemDescription }).ToList();
                    //col.DisplayMember = "CodeNo";
                    //col.ValueMember = "CodeNo";

                    //col.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                    //col.FilteringMode = GridViewFilteringMode.DisplayMember;

                    // col.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;
                }
                catch { }

                //col.TextAlignment = ContentAlignment.MiddleCenter;
                //col.Name = "CodeNo";
                //this.radGridView1.Columns.Add(col);

                //this.radGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                //this.radGridView1.CellEditorInitialized += radGridView1_CellEditorInitialized;
            }
        }
        private void DataLoad()
        {
            return;
            //dt.Rows.Clear();
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    try
                    {
                        //int year1 = 2017;
                        //int.TryParse(cboYear.Text, out year1);
                        //radGridView1.DataSource = db.tb_ProductionForecasts.Where(s => s.ModelName.Contains(cboVendor.Text.Trim()) 
                        //&& s.YYYY== year1).ToList();
                        

                        foreach (var x in dgvData.Rows)
                        {
                            x.Cells["dgvCodeTemp"].Value = x.Cells["ModelName"].Value.ToString();
                            x.Cells["dgvCodeTemp2"].Value = x.Cells["YYYY"].Value.ToString();
                            x.Cells["ModelName"].ReadOnly = true;
                            x.Cells["YYYY"].ReadOnly = true;
                            //x.Cells["MMM"].ReadOnly = true;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch { }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code, string Code2)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Models
                         where ix.ModelName == code

                         select ix).Count();
                if (i > 0)
                    ck = false;
                else
                    ck = true;
            }

            return ck;
        }

        private bool AddUnit()
        {
          
            bool ck = false;
            int C = 0;
            try
            {


                dgvData.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in dgvData.Rows)
                    {
                        if (!Convert.ToString(g.Cells["ModelName"].Value).Equals("")
                            )
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                                //int yyyy = 0;
                                //int mmm = 0;
                                //decimal wk = 0;
                                //int.TryParse(Convert.ToString(g.Cells["YYYY"].Value), out yyyy);
                                //int.TryParse(Convert.ToString(g.Cells["MMM"].Value), out mmm);
                                //decimal.TryParse(Convert.ToString(g.Cells["WorkDays"].Value), out wk);
                                DateTime? d = null;
                                DateTime d1 = DateTime.Now;
                                if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
                                {

                                    tb_Model u = new tb_Model();
                                    u.ModelName = Convert.ToString(g.Cells["ModelName"].Value);
                                    u.ModelDescription = Convert.ToString(g.Cells["ModelDescription"].Value);
                                    u.ModelActive = Convert.ToBoolean(Convert.ToString(g.Cells["ModelActive"].Value));
                                    u.LineName = Convert.ToString(g.Cells["LineName"].Value);
                                    u.MCName = Convert.ToString(g.Cells["MCName"].Value);
                                    u.Limit = Convert.ToBoolean(g.Cells["Limit"].Value);
                                    if (DateTime.TryParse(Convert.ToString(g.Cells["ExpireDate"].Value), out d1))
                                    {
                                        d = dbClss.ChangeFormat(Convert.ToString(g.Cells["ExpireDate"].Value));
                                        //Convert.ToDateTime(Convert.ToString(g.Cells["ExpireDate"].Value));

                                    }
                                    u.ExpireDate = d;


                                    db.tb_Models.InsertOnSubmit(u);
                                    db.SubmitChanges();
                                    C += 1;
                                    dbClss.AddHistory(this.Name, "เพิ่ม", "Insert Model [" + u.ModelName + "]", "");

                                }
                                else
                                {

                                    var u = (from ix in db.tb_Models
                                             where ix.ModelName == Convert.ToString(g.Cells["dgvCodeTemp"].Value)

                                             select ix).First();

                                    u.ModelDescription = Convert.ToString(g.Cells["ModelDescription"].Value);
                                    u.ModelActive = Convert.ToBoolean(Convert.ToString(g.Cells["ModelActive"].Value));
                                    u.LineName = Convert.ToString(g.Cells["LineName"].Value);
                                    u.MCName = Convert.ToString(g.Cells["MCName"].Value);
                                    u.Limit = Convert.ToBoolean(g.Cells["Limit"].Value);

                                    if (DateTime.TryParse(Convert.ToString(g.Cells["ExpireDate"].Value), out d1))
                                    {
                                        d = dbClss.ChangeFormat(Convert.ToString(g.Cells["ExpireDate"].Value));
                                        //Convert.ToDateTime(Convert.ToString(g.Cells["ExpireDate"].Value));

                                    }
                                    u.ExpireDate = d;

                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Model [" + u.ModelName + "]", "");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("AddUnit", ex.Message, this.Name);
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;

            int C = 0;
            try
            {

                if (row >= 0)
                {
                    string CodeDelete = Convert.ToString(dgvData.Rows[row].Cells["ModelName"].Value);
                    string CodeTemp = Convert.ToString(dgvData.Rows[row].Cells["dgvCodeTemp"].Value);
                    string CodeTemp2 = Convert.ToString(dgvData.Rows[row].Cells["dgvCodeTemp2"].Value);
                    dgvData.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( " + CodeDelete + " ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals(""))
                            {
                                if (!CodeTemp.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_Models
                                                 where ix.ModelName == Convert.ToString(CodeTemp)

                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_Models.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบรายการ ModelName", "Delete Model [" + d.ModelName + "]", "");
                                    }
                                    C += 1;



                                    db.SubmitChanges();
                                }
                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("DeleteUnit", ex.Message, this.Name);
            }

            if (C > 0)
            {
                MessageBox.Show("ลบรายการ สำเร็จ!");
            }




            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void ClearData()
        {
            txtADNo.Text = "";
            txtAdjustBy.Text = ClassLib.Classlib.User;

            dtRequire.Value = DateTime.Now;
            txtCreateBy.Text = ClassLib.Classlib.User;
            txtAdjustBy.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtRemark.Text = "";
           
            txtCodeNo.Text = "";
           
            dgvData.Rows.Clear();
            dt_ADH.Rows.Clear();
            dt_ADD.Rows.Clear();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            ClearData();
            Enable_Status(true, "New");
            lblStatus.Text = "New";
            Ac = "New";

            //getมาไว้ก่อน แต่ยังไมได้ save 
            txtADNo.Text = StockControl.dbClss.GetNo(7, 0);
        }
        private void Enable_Status(bool ss, string Condition)
        {
            if (Condition.Equals("-") || Condition.Equals("New"))
            {
                txtCodeNo.Enabled = ss;
                //txtVendorName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
               

            }
            else if (Condition.Equals("View"))
            {
                txtCodeNo.Enabled = ss;
                //txtVendorName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                
            }

            else if (Condition.Equals("Edit"))
            {
                txtCodeNo.Enabled = ss;
                //txtVendorName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
               
            }
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            //radGridView1.ReadOnly = true;
            //btnView.Enabled = false;
            //btnEdit.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //radGridView1.ReadOnly = false;
            //btnEdit.Enabled = false;
            //btnView.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ac.Equals("New"))// || Ac.Equals("Edit"))
                {
                    //if (Check_Save())
                    //    return;
                    //if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    this.Cursor = Cursors.WaitCursor;

                    //    if (Ac.Equals("New"))
                    //        txtRCNo.Text = StockControl.dbClss.GetNo(4, 2);

                    //    if (!txtRCNo.Text.Equals(""))
                    //    {
                    //        SaveHerder();
                    //        SaveDetail();
                    //        DataLoad();
                    //        btnNew.Enabled = true;

                    //        //insert Stock
                    //        Insert_Stock();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("ไม่สามารถโหลดเลขที่รับสินค้าได้ ติดต่อแผนก IT");
                    //    }
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                //dgvData.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
                //string TM1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value);
                ////string TM2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["MMM"].Value);
                //string Chk = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                //if (Chk.Equals("") && !TM1.Equals(""))
                //{

                //    if (!CheckDuplicate(TM1, Chk))
                //    {
                //        MessageBox.Show("ข้อมูล รายการซ้า");
                //        radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value = "";
                //        //  radGridView1.Rows[e.RowIndex].Cells["MMM"].Value = "";
                //        //  radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                //    }
                //}


            }
            catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
                
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            return;
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
            dbClss.ExportGridXlSX(dgvData);
        }

       
        private void btnFilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
          
        }

        private void txtModelName_TextChanged(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (crow == 0)
                DataLoad();
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (crow == 0)
                DataLoad();
        }

        private void radLabel5_Click(object sender, EventArgs e)
        {

        }

        private void radLabel2_Click(object sender, EventArgs e)
        {

        }

        private void radTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radLabel4_Click(object sender, EventArgs e)
        {

        }

        private void txtCodeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {

                   // Insert_data(txtCodeNo.Text);
                    txtCodeNo.Text = "";

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        //private void Insert_data(string CodeNo)
        //{
        //    if (!CodeNo.Equals(""))
        //    {
        //        using (DataClasses1DataContext db = new DataClasses1DataContext())
        //        {
        //            int No = 0;
                  
        //            string ItemNo = "";
        //            string ItemDescription = "";
        //            decimal QTY = 0;
        //            decimal RemainQty = 0;
        //            string Unit = "";
        //            decimal PCSUnit = 0;
        //            decimal CostPerUnit = 0;
        //            decimal Amount = 0;
        //            //string CRRNCY = "";
        //            string LotNo = "";
                    
        //            string Remark = "";
                   
        //            int duppicate_CodeNo = 0;
        //            string Status = "Waiting";
                    


        //            var g = (from ix in db.tb_PurchaseRequests select ix).Where(a => a.PRNo == txtPRNo.Text.Trim()).ToList();
        //            if (g.Count() > 0)
        //            {
        //                if (txtVendorNo.Text.Equals(""))
        //                {
        //                    txtVendorName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorName);
        //                    txtVendorNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo);
        //                    //txtTempNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().TEMPNo);

        //                }
        //                else
        //                {
        //                    if (!txtVendorNo.Text.Equals(StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo)))
        //                    {
        //                        MessageBox.Show("ไม่สามารถรับสินค้าต่างผู้ขายได้");
        //                        duppicate_vendor = 1;
        //                    }
        //                }

        //                CRRNCY = StockControl.dbClss.TSt(g.FirstOrDefault().CRRNCY);

        //                if (duppicate_vendor <= 0)
        //                {
        //                    No = dgvData.Rows.Count() + 1;

        //                    var d = (from ix in db.tb_PurchaseRequestLines select ix)
        //                        .Where(a => a.PRNo == txtPRNo.Text.Trim() && a.SS == 1
        //                        && (Convert.ToDecimal(a.RemainQty) > Convert.ToDecimal(0.00))
        //                        )

        //                        .ToList();
        //                    if (d.Count() > 0)

        //                    {
        //                        foreach (var gg in d)
        //                        {
        //                            CodeNo = StockControl.dbClss.TSt(gg.CodeNo);
        //                            if (!check_Duppicate(CodeNo))
        //                            {
        //                                TempNo = StockControl.dbClss.TSt(gg.TempNo);

        //                                ItemNo = StockControl.dbClss.TSt(gg.ItemName);
        //                                ItemDescription = StockControl.dbClss.TSt(gg.ItemDesc);
        //                                QTY = 0;//StockControl.dbClss.TDe(gg.OrderQty);
        //                                        //RemainQty ต้อง Cal ใหม่ ว่ารับเข้าทั้งหมดเท่าแล้วค่อยเอามาหักลบกัน
        //                                RemainQty = StockControl.dbClss.TDe(gg.RemainQty);
        //                                Unit = StockControl.dbClss.TSt(gg.UnitCode);
        //                                // จำนวนต่อหน่วย
        //                                PCSUnit = StockControl.dbClss.TDe(gg.PCSUnit);
        //                                // ราคาต่อหน่วย
        //                                CostPerUnit = StockControl.dbClss.TDe(gg.StandardCost);
        //                                Amount = QTY * CostPerUnit;
        //                                //CRRNCY = CRRNCY;  //มาจาก herder
        //                                LotNo = StockControl.dbClss.TSt(gg.LotNo);
        //                                SerialNo = StockControl.dbClss.TSt(gg.SerialNo);
        //                                Remark = "";
        //                                PRNo = txtPRNo.Text;
        //                                RCNo = "";
        //                                PRID = StockControl.dbClss.TInt(gg.id);

        //                                if (StockControl.dbClss.TDe(gg.OrderQty)
        //                                        == StockControl.dbClss.TDe(gg.RemainQty))
        //                                    Status = "Waiting";
        //                                else
        //                                    Status = "Partial";


        //                                dgvData.Rows.Add(No.ToString(), Status, CodeNo, ItemNo, ItemDescription, QTY, RemainQty, Unit
        //                                    , PCSUnit, CostPerUnit, Amount, CRRNCY, LotNo, SerialNo, ShelfNo, Remark, TempNo, PRNo, RCNo, InvoiceNo
        //                                    , ID.ToString(), PRID.ToString()
        //                                    );
        //                            }
        //                        }
        //                    }
        //                    Cal_Amount();
        //                }
        //                duppicate_vendor = 0;
        //            }

        //        }
        //    }
        //}
    }
}
