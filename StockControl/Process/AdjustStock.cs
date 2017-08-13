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
using System.Globalization;

namespace StockControl
{
    public partial class AdjustStock : Telerik.WinControls.UI.RadRibbonForm
    {
        public AdjustStock()
        {
            InitializeComponent();
        }

        public AdjustStock(string ADNo, string CodeNo)
        {
            InitializeComponent();
            ADNo_tt = ADNo;
            CodeNo_tt = CodeNo;
        }

        string ADNo_tt = "";
        string CodeNo_tt = "";
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
            dt_ADD.Columns.Add(new DataColumn("AdjustNo", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("Seq", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("StockType", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("ItemNo", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("RemainQty", typeof(decimal)));
            dt_ADD.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            dt_ADD.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt_ADD.Columns.Add(new DataColumn("StandardCost", typeof(decimal)));
            dt_ADD.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt_ADD.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_ADD.Columns.Add(new DataColumn("id", typeof(int)));

            dt_ADH.Columns.Add(new DataColumn("id", typeof(int)));
            dt_ADH.Columns.Add(new DataColumn("ADNo", typeof(string)));
            dt_ADH.Columns.Add(new DataColumn("ADDate", typeof(DateTime)));
            dt_ADH.Columns.Add(new DataColumn("ADBy", typeof(string)));
            dt_ADH.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_ADH.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt_ADH.Columns.Add(new DataColumn("CreateBy", typeof(string)));
            dt_ADH.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_ADH.Columns.Add(new DataColumn("UpdateDate", typeof(DateTime)));
            dt_ADH.Columns.Add(new DataColumn("UpdateBy", typeof(string)));
            dt_ADH.Columns.Add(new DataColumn("BarCode", typeof(Image)));
            


        }

        private void Unit_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnNew_Click(null, null);
                dgvData.AutoGenerateColumns = false;
                GETDTRow();

                DefaultItem();

                if (!ADNo_tt.Equals(""))
                {
                    btnNew.Enabled = true;
                    txtADNo.Text = ADNo_tt;
                    txtCodeNo.Text = "";
                    DataLoad();
                    Ac = "View";
                }
                else if (!CodeNo_tt.Equals(""))
                {
                    btnNew.Enabled = true;
                    txtCodeNo.Text = CodeNo_tt;
                    Insert_data(txtCodeNo.Text);
                    txtCodeNo.Text = "";
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
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

            dt_ADH.Rows.Clear();
            dt_ADD.Rows.Clear();
            dgvData.Rows.Clear();
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    try
                    {
                        var g = (from ix in db.tb_StockAdjustHs select ix).Where(a => a.ADNo == txtADNo.Text.Trim()).ToList();
                        if (g.Count() > 0)
                        {
                            DateTime? temp_date = null;
                           
                            txtRemark.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Remark);
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().ADDate).Equals(""))
                                dtRequire.Value = Convert.ToDateTime(g.FirstOrDefault().ADDate);
                            else
                                dtRequire.Value = Convert.ToDateTime(temp_date);


                            txtAdjustBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().ADBy);
                            txtCreateBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().CreateBy);
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().CreateDate).Equals(""))
                            {
                                    txtCreateDate.Text = Convert.ToDateTime(g.FirstOrDefault().CreateDate).ToString("dd/MMM/yyyy");
                            }
                            else
                                txtCreateDate.Text = "";

                            //lblStatus.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Status);
                            if (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Cancel"))
                            {
                                btnSave.Enabled = false;
                                //btnDelete.Enabled = false;
                                //btnView.Enabled = false;
                                //btnEdit.Enabled = false;
                                lblStatus.Text = "Cancel";
                                dgvData.ReadOnly = true;
                                btnNew.Enabled = true;
                            }
                           
                            else if
                                (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Completed")
                                
                                )
                            {
                                btnSave.Enabled = false;
                                //btnDelete.Enabled = false;
                                //btnView.Enabled = false;
                                //btnEdit.Enabled = false;
                                lblStatus.Text = "Completed";
                                dgvData.ReadOnly = true;
                                btnNew.Enabled = true;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                //btnDelete.Enabled = true;
                                //btnView.Enabled = true;
                                //btnEdit.Enabled = true;
                                lblStatus.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Status);
                                dgvData.ReadOnly = false;
                                btnNew.Enabled = true;
                            }
                            dt_ADH = StockControl.dbClss.LINQToDataTable(g);


                            int dgvNo = 0;
                            //detail
                            var r = (from d in db.tb_StockAdjusts
                                     join i in db.tb_Items on d.CodeNo equals i.CodeNo
                                     where d.AdjustNo == txtADNo.Text

                                        && d.Status != "Cancel"

                                     select new
                                     {
                                         CodeNo = d.CodeNo,
                                         S = false,
                                         ItemNo = d.ItemNo,
                                         ItemDescription = d.ItemDescription,
                                        
                                         QTY = d.Qty,
                                         
                                         RemainQty = (Convert.ToDecimal(db.Cal_QTY(d.CodeNo, "", 0))),// i.StockInv,
                                         Unit = d.Unit,
                                         PCSUnit = d.PCSUnit,
                                         MaxStock = i.MaximumStock,
                                         MinStock = i.MinimumStock,
                                         StandardCost = d.StandardCost,//i.StandardCost,
                                         Amount =d.Amount,
                                         LotNo = d.LotNo,
                                         Remark = d.Reason,
                                         id = d.id
                                     }
                            ).ToList();
                            if (r.Count > 0)
                            {
                                dgvNo = dgvData.Rows.Count() + 1;

                                foreach (var vv in r)
                                {
                                    dgvData.Rows.Add(dgvNo.ToString(),
                                        vv.CodeNo,
                                        vv.ItemNo,
                                        vv.ItemDescription,
                                        vv.RemainQty,
                                        vv.QTY,
                                        vv.Unit,
                                        vv.PCSUnit,
                                        vv.StandardCost,
                                        vv.Amount,
                                        vv.LotNo,
                                        vv.Remark,
                                        vv.id);
                                }

                            }






                            ////Detail  แบบที่ สอง
                            //var d = (from ix in db.tb_StockAdjusts select ix)
                            //.Where(a => a.AdjustNo == txtADNo.Text.Trim()
                            //    && a.Status != "Cancel").ToList();
                            //if (d.Count() > 0)
                            //{
                            //    int c = 0;
                            //    dgvData.DataSource = d;
                                
                            //    dt_ADD = StockControl.dbClss.LINQToDataTable(d);
                            //    string SS = "";
                            //    foreach (var x in dgvData.Rows)
                            //    {
                            //        c += 1;
                            //        x.Cells["dgvNo"].Value = c;

                            //        //if (Convert.ToString(x.Cells["Status"].Value).Equals("Waiting"))
                            //        //{
                            //        //    x.Cells["QTY"].ReadOnly = false;
                            //        //    x.Cells["Unit"].ReadOnly = false;
                            //        //    x.Cells["PCSUnit"].ReadOnly = false;
                            //        //    x.Cells["StandardCost"].ReadOnly = false;
                            //        //    x.Cells["Remark"].ReadOnly = false;
                            //        //    x.Cells["LotNo"].ReadOnly = false;
                            //        //    x.Cells["Remark"].ReadOnly = false;
                            //        //}
                            //        //else if (Convert.ToString(x.Cells["Status"].Value).Equals("Completed")
                            //        //    || Convert.ToString(x.Cells["Status"].Value).Equals("Cancel")
                            //        //    )
                                        
                            //        //{
                            //        //    x.Cells["QTY"].ReadOnly = true;
                            //        //    x.Cells["Unit"].ReadOnly = true;
                            //        //    x.Cells["PCSUnit"].ReadOnly = true;
                            //        //    x.Cells["StandardCost"].ReadOnly = true;
                            //        //    x.Cells["Remark"].ReadOnly = true;
                            //        //    x.Cells["LotNo"].ReadOnly = true;
                            //        //    x.Cells["Remark"].ReadOnly = true;
                            //        //}
                            //    }
                            //}
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            catch { }
            finally { this.Cursor = Cursors.Default; }


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
            txtCreateDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtRemark.Text = "";
           
            txtCodeNo.Text = "";
           
            dgvData.Rows.Clear();
            dt_ADH.Rows.Clear();
            dt_ADD.Rows.Clear();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            btnDel_Item.Enabled = true;
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
        private bool Check_Save()
        {
            bool re = true;
            string err = "";
            try
            {
                //if (txtCodeNo.Text.Equals(""))
                //    err += " “รหัสพาร์ท:” เป็นค่าว่าง \n";
                //if (txtRCNo.Text.Equals(""))
                //    err += " “เลขที่รับสินค้า:” เป็นค่าว่าง \n";
                if (txtAdjustBy.Text.Equals(""))
                    err += "- “ผู้ร้องขอ:” เป็นค่าว่าง \n";
                //if (txtVendorNo.Text.Equals(""))
                //    err += "- “รหัสผู้ขาย:” เป็นค่าว่าง \n";
                if (dtRequire.Text.Equals(""))
                    err += "- “วันที่รับสินค้า:” เป็นค่าว่าง \n";
               
                if (dgvData.Rows.Count <= 0)
                    err += "- “รายการ:” เป็นค่าว่าง \n";
                int c = 0;
                foreach (GridViewRowInfo rowInfo in dgvData.Rows)
                {
                    if (rowInfo.IsVisible)
                    {
                        //if (StockControl.dbClss.TInt(rowInfo.Cells["QTY"].Value) != (0))
                        //{
                            c += 1;
                           
                            if (StockControl.dbClss.TSt(rowInfo.Cells["CodeNo"].Value).Equals(""))
                                err += "- “รหัสพาร์ท:” เป็นค่าว่าง \n";
                            //if (StockControl.dbClss.TDe(rowInfo.Cells["QTY"].Value) <= 0)
                            //    err += "- “จำนวนรับ:” น้อยกว่า 0 \n";
                            if (StockControl.dbClss.TDe(rowInfo.Cells["Unit"].Value).Equals(""))
                                err += "- “หน่วย:” เป็นค่าว่าง \n";

                        //}
                    }
                }

                if (c <= 0)
                    err += "- “กรุณาระบุจำนวนที่จะปรับสต็อกสินค้า:” เป็นค่าว่าง \n";


                if (!err.Equals(""))
                    MessageBox.Show(err);
                else
                    re = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("AdjustStock", ex.Message, this.Name);
            }

            return re;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ac.Equals("New"))// || Ac.Equals("Edit"))
                {
                    if (Check_Save())
                        return;
                    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        if (Ac.Equals("New"))
                            txtADNo.Text = StockControl.dbClss.GetNo(7, 2);

                        if (!txtADNo.Text.Equals("")) //&& Ac.Equals("New"))
                        {
                            
                            SaveHerder();
                            SaveDetail();
                            string ADNo = txtADNo.Text;
                            ClearData();
                            txtADNo.Text = ADNo;
                            

                            DataLoad();
                            btnNew.Enabled = true;
                            btnDel_Item.Enabled = false;

                            ////insert Stock
                            //Insert_Stock();
                            Insert_Stock_new();
                            MessageBox.Show("บันทึกสำเร็จ!");
                            DataLoad();
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถโหลดเลขที่รับสินค้าได้ ติดต่อแผนก IT");
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void Insert_Stock_new()
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 0;
                    string Type_in_out = "In";
                    decimal RemainQty = 0;
                    decimal Amount = 0;
                    decimal RemainAmount = 0;
                    decimal Avg = 0;
                    decimal UnitCost = 0;
                    decimal sum_Remain = 0;
                    decimal sum_Qty = 0;

                    //string Type = "";
                    string Category = "Invoice"; //Temp,Invoice
                    //if (rdoInvoice.IsChecked)
                    //{
                    //    Category = "Invoice";
                    //    Type = "รับด้วยใบ Invoice";
                    //}
                    //else
                    //{
                    //    Category = "Temp";
                    //    Type = "ใบส่งของชั่วคราว";
                    //}

                    //string CNNo = CNNo = StockControl.dbClss.GetNo(6, 2);
                    var g = (from ix in db.tb_StockAdjusts
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.AdjustNo.Trim() == txtADNo.Text.Trim() && ix.Status != "Cancel"

                             select ix).ToList();
                    if (g.Count > 0)
                    {
                        //insert Stock

                        foreach (var vv in g)
                        {
                            Seq += 1;

                            tb_Stock gg = new tb_Stock();
                            gg.CodeNo = vv.CodeNo;
                            gg.AppDate = AppDate;
                            gg.Seq = Seq;
                            gg.App = "Adjust";
                            gg.Appid = Seq;
                            gg.CreateBy = ClassLib.Classlib.User;
                            gg.CreateDate = DateTime.Now;
                            gg.DocNo = txtADNo.Text;
                            gg.RefNo = "";
                            gg.Type = "Adjust";
                            gg.QTY = Convert.ToDecimal(vv.Qty);
                            if (Convert.ToDecimal(vv.Qty) < 0)
                            {
                                gg.Outbound = Convert.ToDecimal(vv.Qty);
                                gg.Inbound = 0;
                                Type_in_out = "Out";

                                UnitCost = Convert.ToDecimal(vv.StandardCost);//Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                Amount = Convert.ToDecimal(vv.Qty) * UnitCost;

                                //แบบที่ 1 จะไป sum ใหม่
                                RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                //แบบที่ 2 จะไปดึงล่าสุดมา
                                //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                    + Amount;

                                sum_Qty = RemainQty + Convert.ToDecimal(vv.Qty);
                                Avg = UnitCost;//sum_Remain / sum_Qty;
                                RemainAmount = sum_Remain;

                            }
                            else
                            {
                                gg.Inbound = Convert.ToDecimal(vv.Qty);
                                gg.Outbound = 0;
                                Type_in_out = "In";

                                Amount = Convert.ToDecimal(vv.Qty) * Convert.ToDecimal(vv.StandardCost);
                                UnitCost = Convert.ToDecimal(vv.StandardCost);

                                //แบบที่ 1 จะไป sum ใหม่
                                RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                //แบบที่ 2 จะไปดึงล่าสุดมา
                                //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                                sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                    + Amount;

                                sum_Qty = RemainQty + Convert.ToDecimal(vv.Qty);
                                Avg = sum_Remain / sum_Qty;
                                RemainAmount = sum_Qty * Avg;

                            }

                            //gg.AmountCost = (Convert.ToDecimal(vv.Qty)) * get_cost(vv.CodeNo);
                            //gg.UnitCost = get_cost(vv.CodeNo);
                            //gg.RemainQty = 0;
                            //gg.RemainUnitCost = 0;
                            //gg.RemainAmount = 0;
                            gg.CalDate = CalDate;
                            gg.Status = "Active";

                            gg.Type_i = 5;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                            gg.Category = Category;
                            gg.Refid = vv.id;
                            gg.Flag_ClearTemp = 0;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว

                            gg.Type_in_out = Type_in_out;
                            gg.AmountCost = Amount;
                            gg.UnitCost = UnitCost;
                            gg.RemainQty = sum_Qty;
                            gg.RemainUnitCost = 0;
                            gg.RemainAmount = RemainAmount;
                            gg.Avg = Avg;

                            db.tb_Stocks.InsertOnSubmit(gg);
                            db.SubmitChanges();

                            //update item
                            //dbClss.Insert_Stock(vv.CodeNo, (Convert.ToDecimal(vv.Qty)), "Adjust", "Inv");

                            //update Stock เข้า item
                            db.sp_010_Update_StockItem(Convert.ToString(vv.CodeNo), "");
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Insert_Stock()
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 0;



                    string CNNo = CNNo = StockControl.dbClss.GetNo(6, 2);
                    var g = (from ix in db.tb_StockAdjusts
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.AdjustNo.Trim() == txtADNo.Text.Trim() && ix.Status != "Cancel"

                             select ix).ToList();
                    if (g.Count > 0)
                    {
                        //insert Stock

                        foreach (var vv in g)
                        {
                            Seq += 1;

                            tb_Stock1 gg = new tb_Stock1();
                            gg.AppDate = AppDate;
                            gg.Seq = Seq;
                            gg.App = "Adjust";
                            gg.Appid = Seq;
                            gg.CreateBy = ClassLib.Classlib.User;
                            gg.CreateDate = DateTime.Now;
                            gg.DocNo = CNNo;
                            gg.RefNo = txtADNo.Text;
                            gg.Type = "Adjust";
                            gg.QTY = Convert.ToDecimal(vv.Qty);
                            if (Convert.ToDecimal(vv.Qty) < 0)
                            {
                                gg.Outbound = Convert.ToDecimal(vv.Qty);
                                gg.Inbound = 0;
                            }
                            else
                            {
                                gg.Inbound = Convert.ToDecimal(vv.Qty);
                                gg.Outbound = 0;
                            }

                            gg.AmountCost = (Convert.ToDecimal(vv.Qty)) * get_cost(vv.CodeNo);
                            gg.UnitCost = get_cost(vv.CodeNo);
                            gg.RemainQty = 0;
                            gg.RemainUnitCost = 0;
                            gg.RemainAmount = 0;
                            gg.CalDate = CalDate;
                            gg.Status = "Active";

                            db.tb_Stock1s.InsertOnSubmit(gg);
                            db.SubmitChanges();

                            dbClss.Insert_Stock(vv.CodeNo, (Convert.ToDecimal(vv.Qty)), "Adjust", "Inv");


                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private decimal get_cost(string Code)
        {
            decimal re = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_Items
                         where ix.CodeNo == Code && ix.Status == "Active"
                         select ix).First();
                re = Convert.ToDecimal(g.StandardCost);

            }
            return re;
        }
        private void SaveHerder()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_StockAdjustHs
                         where ix.ADNo.Trim() == txtADNo.Text.Trim() && ix.Status != "Cancel"
                         //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {
                    foreach (DataRow row in dt_ADH.Rows)
                    {
                        var gg = (from ix in db.tb_StockAdjustHs
                                  where ix.ADNo.Trim() == txtADNo.Text.Trim() && ix.Status != "Cancel"
                                  //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                  select ix).First();

                        gg.UpdateBy = ClassLib.Classlib.User;
                        gg.UpdateDate = DateTime.Now;
                        dbClss.AddHistory(this.Name, txtADNo.Text, "แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                        if (StockControl.dbClss.TSt(gg.BarCode).Equals(""))
                            gg.BarCode = StockControl.dbClss.SaveQRCode2D(txtADNo.Text.Trim());

                        
                        if (!txtAdjustBy.Text.Trim().Equals(row["ADBy"].ToString()))
                        {
                            gg.ADBy = txtAdjustBy.Text.Trim();
                            dbClss.AddHistory(this.Name, txtADNo.Text, "แก้ไข ผู้ร้องขอ[" + txtAdjustBy.Text.Trim() + " เดิม :" + row["ADBy"].ToString() + "]", "");
                        }
                        if (!txtRemark.Text.Trim().Equals(row["Remark"].ToString()))
                        {
                            gg.Remark = txtRemark.Text.Trim();
                            dbClss.AddHistory(this.Name , txtADNo.Text, "แก้ไขหมายเหตุ [" + txtRemark.Text.Trim() + " เดิม :" + row["Remark"].ToString() + "]", "");
                        }
                       
                        
                        if (!dtRequire.Text.Trim().Equals(""))
                        {
                            string date1 = "";
                            date1 = dtRequire.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                            string date2 = "";
                            DateTime temp = DateTime.Now;
                            if (!StockControl.dbClss.TSt(row["ADDate"].ToString()).Equals(""))
                            {

                                temp = Convert.ToDateTime(row["ADDate"]);
                                date2 = temp.ToString("yyyyMMdd", new CultureInfo("en-US"));

                            }
                            if (!date1.Equals(date2))
                            {
                                DateTime? RequireDate = DateTime.Now;
                                if (!dtRequire.Text.Equals(""))
                                    RequireDate = dtRequire.Value;
                                gg.ADDate = RequireDate;
                                dbClss.AddHistory(this.Name, txtADNo.Text, "แก้ไขวันที่ปรับสต็อกสินค้า [" + dtRequire.Text.Trim() + " เดิม :" + temp.ToString() + "]", "");
                            }
                        }
                        db.SubmitChanges();
                    }
                }
                else //สร้างใหม่
                {
                    byte[] barcode = StockControl.dbClss.SaveQRCode2D(txtADNo.Text.Trim());
                    DateTime? UpdateDate = null;

                    DateTime? RequireDate = DateTime.Now;
                    if (!dtRequire.Text.Equals(""))
                        RequireDate = dtRequire.Value;

                    tb_StockAdjustH gg = new tb_StockAdjustH();
                    gg.ADNo = txtADNo.Text;
                    gg.ADBy = txtAdjustBy.Text;
                    gg.ADDate = RequireDate;
                    gg.UpdateBy = null;
                    gg.UpdateDate = UpdateDate;
                    gg.CreateBy = ClassLib.Classlib.User;
                    gg.CreateDate = DateTime.Now;
                    gg.Remark = txtRemark.Text;
                    gg.BarCode = barcode;
                    gg.Status = "Completed";
                    db.tb_StockAdjustHs.InsertOnSubmit(gg);
                    db.SubmitChanges();

                    dbClss.AddHistory(this.Name , txtADNo.Text, "สร้าง การปรับสต็อกสินค้า [" + txtADNo.Text.Trim() + "]", "");
                }
            }
        }
        private void SaveDetail()
        {
            dgvData.EndEdit();

            string ADNo = txtADNo.Text;
            DateTime? RequireDate = DateTime.Now;
            if (!dtRequire.Text.Equals(""))
                RequireDate = dtRequire.Value;
            int Seq = 0;
            DateTime? UpdateDate = null;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                foreach (var g in dgvData.Rows)
                {
                    string SS = "";
                    if (g.IsVisible.Equals(true))
                    {
                        if (StockControl.dbClss.TInt(g.Cells["QTY"].Value) != (0)) // เอาเฉพาะรายการที่ไม่เป็น 0 
                        {
                            if (StockControl.dbClss.TInt(g.Cells["id"].Value) <= 0)  //New ใหม่
                            {
                               
                                Seq += 1;
                                tb_StockAdjust u = new tb_StockAdjust();
                                u.AdjustNo = txtADNo.Text;
                               
                                u.CodeNo = StockControl.dbClss.TSt(g.Cells["CodeNo"].Value);
                                u.ItemNo = StockControl.dbClss.TSt(g.Cells["ItemNo"].Value);
                                u.ItemDescription = StockControl.dbClss.TSt(g.Cells["ItemDescription"].Value);
                                u.Qty = StockControl.dbClss.TDe(g.Cells["QTY"].Value);
                                u.PCSUnit = StockControl.dbClss.TDe(g.Cells["PCSUnit"].Value);
                                u.Unit = StockControl.dbClss.TSt(g.Cells["Unit"].Value);
                                u.Amount = StockControl.dbClss.TDe(g.Cells["Amount"].Value);
                                u.Reason = StockControl.dbClss.TSt(g.Cells["Remark"].Value);                              
                                u.LotNo = StockControl.dbClss.TSt(g.Cells["LotNo"].Value);
                               
                                //u.RCDate = RequireDate;
                                u.Seq = Seq;
                                u.Status = "Completed";
                                u.StandardCost = StockControl.dbClss.TDe(g.Cells["StandardCost"].Value);

                                u.CreateBy = ClassLib.Classlib.User;
                                u.CreateDate = DateTime.Now;
                               
                                db.tb_StockAdjusts.InsertOnSubmit(u);
                                db.SubmitChanges();

                                ////// update Remainใน tb_receive ที่เป็น PRID เดียวกัน ทั้งหมด
                                //update_remainqty_Receive(PRNo, Temp, PRID, RemainQty);

                                //////หมายถึงรับสินค้าครบหมดแล้ว ให้ไป เปลี่ยนสถาะ เป็น Completed ทุกตัวที่เป็น PRID เดียวกัน
                                //if (!SS.Equals(""))
                                //    Save_Status_Receive(PRNo, Temp, PRID, RemainQty);

                                //C += 1;
                                dbClss.AddHistory(this.Name , txtADNo.Text, "เพิ่มรายการ ปรับสต็อก [" + u.CodeNo + " จำนวนรับ :" + u.Qty.ToString()  + "]", "");
                                
                            }
                            //else
                            //{
                            //    if (StockControl.dbClss.TInt(g.Cells["id"].Value) > 0)
                            //    {
                            //        foreach (DataRow row in dt_ADD.Rows)
                            //        {
                            //            var u = (from ix in db.tb_Receives
                            //                     where ix.ID == Convert.ToInt32(g.Cells["ID"])
                            //                         && ix.TempNo == StockControl.dbClss.TSt(g.Cells["TempNo"].Value)
                            //                         && ix.PRNo == StockControl.dbClss.TSt(g.Cells["PRNo"].Value)
                            //                         && ix.CodeNo == StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                            //                     select ix).First();

                            //            //u.CreateBy = ClassLib.Classlib.User;
                            //            //u.CreateDate = DateTime.Now;
                            //            u.UpdateBy = ClassLib.Classlib.User;
                            //            u.CreateDate = DateTime.Now;

                            //            dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "ID :" + StockControl.dbClss.TSt(g.Cells["ID"].Value)
                            //           + " CodeNo :" + StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                            //           + " แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                            //            //u.Seq = Seq;

                            //            if (!StockControl.dbClss.TSt(g.Cells["CodeNo"].Value).Equals(row["CodeNo"].ToString()))
                            //            {
                            //                u.CodeNo = StockControl.dbClss.TSt(g.Cells["CodeNo"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขรหัสพาร์ท [" + u.CodeNo + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["QTY"].Value).Equals(row["QTY"].ToString()))
                            //            {
                            //                decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(g.Cells["QTY"].Value), out QTY);
                            //                decimal RemainQty = 0; decimal.TryParse(StockControl.dbClss.TSt(g.Cells["RemainQty"].Value), out RemainQty);

                            //                u.QTY = StockControl.dbClss.TDe(g.Cells["QTY"].Value);
                            //                u.RemainQty = RemainQty - QTY;//StockControl.dbClss.TDe(g.Cells["dgvRemainQty"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขจำนวน [" + QTY.ToString() + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["Unit"].Value).Equals(row["Unit"].ToString()))
                            //            {
                            //                u.Unit = StockControl.dbClss.TSt(g.Cells["Unit"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขหน่วย [" + u.Unit + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["PCSUnit"].Value).Equals(row["PCSUnit"].ToString()))
                            //            {
                            //                u.PCSUnit = StockControl.dbClss.TDe(g.Cells["PCSUnit"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขจำนวน/หน่วย [" + u.PCSUnit + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["CostPerUnit"].Value).Equals(row["CostPerUnit"].ToString()))
                            //            {
                            //                u.CostPerUnit = StockControl.dbClss.TDe(g.Cells["CostPerUnit"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขราคา/หน่วย [" + u.CostPerUnit + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["ShelfNo"].Value).Equals(row["ShelfNo"].ToString()))
                            //            {
                            //                u.ShelfNo = StockControl.dbClss.TSt(g.Cells["ShelfNo"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขเลขที่ ShelfNo [" + u.ShelfNo + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["Remark"].Value).Equals(row["Remark"].ToString()))
                            //            {
                            //                u.Remark = StockControl.dbClss.TSt(g.Cells["Remark"].Value);
                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไขวัตถุประสงค์ [" + u.Remark + "]", "");
                            //            }
                            //            if (!StockControl.dbClss.TSt(g.Cells["PRID"].Value).Equals(row["PRID"].ToString()))
                            //            {
                            //                decimal PRID = 0; decimal.TryParse(StockControl.dbClss.TSt(g.Cells["PRID"].Value), out PRID);

                            //                u.PRID = StockControl.dbClss.TInt(g.Cells["PRID"].Value);

                            //                dbClss.AddHistory(this.Name + txtRCNo.Text, "แก้ไขรายการ Receive", "แก้ไข PRID [" + PRID.ToString() + "]", "");
                            //            }

                                    

                            //            //update รายการใน PR
                            //            var p = (from ix in db.tb_PurchaseRequestLines
                            //                     where ix.id == StockControl.dbClss.TInt(g.Cells["PRID"].Value)
                            //                     // && ix.TempNo == StockControl.dbClss.TSt(g.Cells["TempNo"].Value)
                            //                     //&& ix.PRNo == StockControl.dbClss.TSt(g.Cells["PRNo"].Value)
                            //                     //&& ix.CodeNo == StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                            //                     select ix).First();

                            //            p.RemainQty = StockControl.dbClss.TDe(p.RemainQty) - StockControl.dbClss.TDe(g.Cells["QTY"].Value);

                            //            //update herder pr
                            //            var h = (from ix in db.tb_PurchaseRequests
                            //                     where ix.PRNo == StockControl.dbClss.TSt(g.Cells["PRNo"].Value)
                            //                     // && ix.TempNo == StockControl.dbClss.TSt(g.Cells["TempNo"].Value)
                            //                     //&& ix.PRNo == StockControl.dbClss.TSt(g.Cells["PRNo"].Value)
                            //                     //&& ix.CodeNo == StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                            //                     select ix).First();
                            //            h.Status = "Completed";

                            //            dbClss.AddHistory(this.Name + StockControl.dbClss.TSt(g.Cells["PRNo"].Value), "รับรายการสินค้า Receive", "ID :" + StockControl.dbClss.TSt(g.Cells["ID"].Value)
                            //                  + " CodeNo :" + StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                            //                  + " แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                            //            db.SubmitChanges();


                            //        }
                            //    }
                            //}

                        }
                    }
                }
            }
        }
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                dgvData.EndEdit();
                if (e.RowIndex >= -1)
                {
                    //if (dgvData.Columns["CodeNo"].Index == e.ColumnIndex)
                    //{

                    //}
                    //if (dgvData.Columns["QTY"].Index == e.ColumnIndex)
                    //{
                    //    decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["QTY"].Value), out QTY);
                    //    decimal RemainQty = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["RemainQty"].Value), out RemainQty);
                    //    if (QTY > RemainQty)
                    //    {
                    //        MessageBox.Show("ไม่สามารถรับเกินจำนวนคงเหลือได้");
                    //        e.Row.Cells["QTY"].Value = 0;
                    //    }
                    //}

                    if (dgvData.Columns["QTY"].Index == e.ColumnIndex
                        || dgvData.Columns["StandardCost"].Index == e.ColumnIndex
                        )
                    {
                        decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["QTY"].Value), out QTY);
                        decimal CostPerUnit = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["StandardCost"].Value), out CostPerUnit);

                        //if (QTY<0)
                        //{
                        //    CostPerUnit =  Convert.ToDecimal(dbClss.Get_Stock(StockControl.dbClss.TSt(e.Row.Cells["CodeNo"].Value), "", "", "Avg"));
                        //    e.Row.Cells["StandardCost"].Value = CostPerUnit;
                        //}
                        

                        e.Row.Cells["Amount"].Value = QTY * CostPerUnit;
                        //Cal_Amount();
                    }
                }

            }
            catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.S))
            {
                btnSave_Click(null, null);
            }
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
        
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            
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
                    
                    Insert_data(txtCodeNo.Text);
                    txtCodeNo.Text = "";

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Insert_data(string CodeNo)
        {

            try

            {
                if (!CodeNo.Equals(""))
                {
                    this.Cursor = Cursors.WaitCursor;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int No = 0;

                        string ItemNo = "";
                        string ItemDescription = "";
                        decimal QTY = 0;
                        decimal RemainQty = 0;
                        string Unit = "";
                        decimal PCSUnit = 0;
                        decimal CostPerUnit = 0;
                        decimal Amount = 0;
                        //string CRRNCY = "";
                        string LotNo = "";

                        string Remark = "";

                        int duppicate_CodeNo = 0;
                        //string Status = "Waiting";

                        var d1 = (from ix in db.tb_Items select ix)
                            .Where(a => a.CodeNo == CodeNo.Trim() && a.Status == "Active"

                            ).ToList();
                        if (d1.Count > 0)
                        {
                            var d = (from ix in db.tb_Items select ix)
                            .Where(a => a.CodeNo == CodeNo.Trim() && a.Status == "Active"

                            ).First();

                            ItemNo = d.ItemNo;
                            ItemDescription = d.ItemDescription;
                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(Convert.ToString(CodeNo), "Invoice", 0)));//Convert.ToDecimal(d.StockInv);
                            Unit = d.UnitBuy;
                            PCSUnit = Convert.ToDecimal(d.PCSUnit);
                            CostPerUnit = Convert.ToDecimal(d.StandardCost); // Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "Avg"));//Convert.ToDecimal(d.StandardCost);

                            No = dgvData.Rows.Count() + 1;
                            if (!check_Duppicate(CodeNo))
                            {
                                dgvData.Rows.Add(No,
                                                    CodeNo,
                                                    ItemNo,
                                                    ItemDescription,
                                                    RemainQty,
                                                    QTY,
                                                    Unit,
                                                    PCSUnit,
                                                    CostPerUnit,
                                                    Amount,
                                                    LotNo,
                                                    Remark,
                                                    "0"
                                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private bool check_Duppicate(string CodeNo)
        {
            bool re = false;
            foreach (var rd1 in dgvData.Rows)
            {
                if (StockControl.dbClss.TSt(rd1.Cells["CodeNo"].Value).Equals(CodeNo))
                    re = true;
            }

            return re;

        }

        private void btnListitem_Click(object sender, EventArgs e)
        {
            try
            {
                btnDel_Item.Enabled = false;
                btnSave.Enabled = false;
                //btnEdit.Enabled = true;
                //btnView.Enabled = false;
                btnNew.Enabled = true;
                ClearData();
                Enable_Status(false, "View");

                this.Cursor = Cursors.WaitCursor;
                AdjustStock_List sc = new AdjustStock_List(txtADNo, txtCodeNo);
                this.Cursor = Cursors.Default;
                sc.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
                //LoadData

                string CodeNo = txtCodeNo.Text;
                string ADNo = txtADNo.Text;
                if (!txtADNo.Text.Equals(""))
                {
                    txtCodeNo.Text = "";
                    DataLoad();
                    Ac = "View";
                    
                }
                else
                {

                    btnNew_Click(null, null);
                    txtCodeNo.Text = CodeNo;

                    Insert_data(txtCodeNo.Text);
                    txtCodeNo.Text = "";

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message + " : radButtonElement1_Click", this.Name); }

        }

        private void btnDel_Item_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvData.Rows.Count < 0)
                    return;


                if (Ac.Equals("New"))// || Ac.Equals("Edit"))
                {
                    this.Cursor = Cursors.WaitCursor;

                    int id = 0;
                    int.TryParse(StockControl.dbClss.TSt(dgvData.CurrentRow.Cells["id"].Value), out id);
                    if (id <= 0)
                        dgvData.Rows.Remove(dgvData.CurrentRow);

                    else
                    {
                        string CodeNo = ""; StockControl.dbClss.TSt(dgvData.CurrentRow.Cells["CodeNo"]);
                        if (MessageBox.Show("ต้องการลบรายการ ( " + CodeNo + " ) ออกจากรายการ หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dgvData.CurrentRow.IsVisible = false;
                        }
                    }
                    SetRowNo1(dgvData);
                }
                else
                {
                    MessageBox.Show("ไม่สามารถทำการลบรายการได้");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        public static void SetRowNo1(RadGridView Grid)//เลขลำดับ
        {
            int i = 1;
            Grid.Rows.Where(o => o.IsVisible).ToList().ForEach(o =>
            {
                o.Cells["dgvNo"].Value = i;
                i++;
            });
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPR a = new PrintPR(txtADNo.Text, txtADNo.Text, "AdjustStock");
                a.ShowDialog();

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    var g = (from ix in db.sp_R004_ReportShipping(txtSHNo.Text, DateTime.Now) select ix).ToList();
                //    if (g.Count() > 0)
                //    {

                //        Report.Reportx1.Value = new string[2];
                //        Report.Reportx1.Value[0] = txtSHNo.Text;
                //        Report.Reportx1.WReport = "ReportShipping";
                //        Report.Reportx1 op = new Report.Reportx1("ReportShipping.rpt");
                //        op.Show();

                //    }
                //    else
                //        MessageBox.Show("not found.");
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
