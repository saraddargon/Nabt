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
    public partial class Shipping : Telerik.WinControls.UI.RadRibbonForm
    {
        public Shipping()
        {
            InitializeComponent();
        }
        public Shipping(string SHNo,string CodeNo)
        {
            InitializeComponent();
            SHNo_t = SHNo;
            CodeNo_t = CodeNo;
        }
        string SHNo_t = "";
        string CodeNo_t = "";
        string Ac = "";
        DataTable dt_h = new DataTable();
        DataTable dt_d = new DataTable();

        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name, txtSHNo.Text);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            dt_h.Columns.Add(new DataColumn("ShippingNo", typeof(string)));
            dt_h.Columns.Add(new DataColumn("ShipName", typeof(string)));
            dt_h.Columns.Add(new DataColumn("ShipDate", typeof(DateTime)));
            dt_h.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt_h.Columns.Add(new DataColumn("CreateBy", typeof(string)));
            dt_h.Columns.Add(new DataColumn("UpdateDate", typeof(DateTime)));
            dt_h.Columns.Add(new DataColumn("UpdateBy", typeof(string)));
            dt_h.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_h.Columns.Add(new DataColumn("Status", typeof(string)));


            dt_d.Columns.Add(new DataColumn("ShippingNo", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ShipType", typeof(string)));
            dt_d.Columns.Add(new DataColumn("Seq", typeof(int)));          
            dt_d.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ItemNo", typeof(string)));
            dt_d.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt_d.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            dt_d.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_d.Columns.Add(new DataColumn("LineName", typeof(string)));
            dt_d.Columns.Add(new DataColumn("MachineName", typeof(string)));
            dt_d.Columns.Add(new DataColumn("UnitShip", typeof(string)));
            dt_d.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt_d.Columns.Add(new DataColumn("SerialNo", typeof(string)));
            dt_d.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dt_d.Columns.Add(new DataColumn("Calbit", typeof(bool)));
            dt_d.Columns.Add(new DataColumn("ClearFlag", typeof(bool)));
            dt_d.Columns.Add(new DataColumn("ClearDate", typeof(bool)));
            dt_d.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_d.Columns.Add(new DataColumn("BarCode", typeof(Image)));

            
        }

        private void Unit_Load(object sender, EventArgs e)
        {
            //dgvData.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            GETDTRow();
   
            DefaultItem();
            
            btnNew_Click(null, null);

            if (!SHNo_t.Equals(""))
            {
                btnNew.Enabled = true;
                txtSHNo.Text = SHNo_t;
                txtCodeNo.Text = "";
                DataLoad();
                Ac = "View";
               
            }
            else if (!CodeNo_t.Equals(""))
            {
                btnNew.Enabled = true;
                txtCodeNo.Text = CodeNo_t;
                Insert_data();
                txtCodeNo.Text = "";
            }

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
          
            dt_h.Rows.Clear();
            dt_d.Rows.Clear();
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    try
                    {
                        var g = (from ix in db.tb_ShippingHs select ix).Where(a => a.ShippingNo == txtSHNo.Text.Trim()).ToList();
                        if (g.Count() > 0)
                        {
                            DateTime? temp_date = null;
                            txtRemark.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Remark);

                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().ShipDate).Equals(""))
                                dtRequire.Value = Convert.ToDateTime(g.FirstOrDefault().ShipDate);
                            else
                                dtRequire.Value = Convert.ToDateTime(temp_date);


                            txtCreateBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().CreateBy);
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().UpdateBy).Equals(""))
                                txtCreateBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().UpdateBy);
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().CreateDate).Equals(""))
                            {
                                if (!StockControl.dbClss.TSt(g.FirstOrDefault().UpdateDate).Equals(""))
                                    txtCreateDate.Text = Convert.ToDateTime(g.FirstOrDefault().UpdateDate).ToString("dd/MMM/yyyy");
                                else
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
                                btnDel_Item.Enabled = false;
                            }
                            else if
                                (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Process"))
                            {
                                btnSave.Enabled = false;
                                //btnDelete.Enabled = false;
                                //btnView.Enabled = false;
                                //btnEdit.Enabled = false;
                                lblStatus.Text = "Process";
                                dgvData.ReadOnly = false;
                                btnNew.Enabled = true;
                                btnDel_Item.Enabled = false;
                            }
                            else if
                                (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Completed"))
                                
                            {
                                btnSave.Enabled = false;
                                //btnDelete.Enabled = false;
                                //btnView.Enabled = false;
                                //btnEdit.Enabled = false;
                                lblStatus.Text = "Completed";
                                dgvData.ReadOnly = true;
                                btnNew.Enabled = true;
                                btnDel_Item.Enabled = false;
                            }
                            else
                            {
                                btnNew.Enabled = true;
                                btnSave.Enabled = true;
                                //btnDelete.Enabled = true;
                                //btnView.Enabled = true;
                                //btnEdit.Enabled = true;
                                lblStatus.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Status);
                                dgvData.ReadOnly = false;
                                btnDel_Item.Enabled = false;
                            }
                            dt_h = StockControl.dbClss.LINQToDataTable(g);

                            //Detail
                            var d = (from ix in db.tb_Shippings select ix)
                            .Where(a => a.ShippingNo == txtSHNo.Text.Trim()
                                && a.Status != "Cancel").ToList();
                            if (d.Count() > 0)
                            {
                                int c = 0;
                                dgvData.DataSource = d;
                                dt_d = StockControl.dbClss.LINQToDataTable(d);

                                int id = 0;
                                foreach (var x in dgvData.Rows)
                                {
                                    c += 1;
                                    x.Cells["dgvNo"].Value = c;

                                    id = Convert.ToInt32(x.Cells["id"].Value);

                                    var s = (from ix in db.tb_Stocks select ix)
                                       .Where(a => a.DocNo == txtSHNo.Text.Trim()
                                           && a.Refid == id).FirstOrDefault();
                                    if (s != null)
                                    {
                                        x.Cells["RemainQty"].Value = Convert.ToDecimal(s.RemainQty);
                                        x.Cells["StandardCost"].Value = Convert.ToDecimal(s.UnitCost);
                                        x.Cells["Amount"].Value = Math.Abs(Convert.ToDecimal(s.AmountCost));
                                    }
                                   
                                }
                            }
                            Cal_Amount();
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

        private void ClearData()
        {
            
            txtSHNo.Text = "";
            txtRemark.Text = "";
            txtCodeNo.Text = "";
            dtRequire.Value = DateTime.Now;
            txtCreateBy.Text = ClassLib.Classlib.User;
            txtSHName.Text = ClassLib.Classlib.User;
            txtCreateDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblStatus.Text = "-";
            dgvData.Rows.Clear();
            txtTotal.Text = "0.00";
        }
      private void Enable_Status(bool ss,string Condition)
        {
            if (Condition.Equals("-") || Condition.Equals("New"))
            {
                txtCodeNo.Enabled = ss;
                txtSHName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                btnDel_Item.Enabled = ss;


            }
            else if (Condition.Equals("View"))
            {
                txtCodeNo.Enabled = ss;
                txtSHName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                txtRemark.Enabled = ss;
                btnDel_Item.Enabled = ss;
            }

            else if (Condition.Equals("Edit"))
            {
                txtCodeNo.Enabled = ss;
                txtSHName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                txtRemark.Enabled = ss;
                btnDel_Item.Enabled = ss;
            }
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

           // getมาไว้ก่อน แต่ยังไมได้ save
            txtSHNo.Text = StockControl.dbClss.GetNo(5, 0);
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
                
                if (txtSHName.Text.Equals(""))
                    err += "- “ผู้เบิกสินค้า:” เป็นค่าว่าง \n";
                //if (txtVendorNo.Text.Equals(""))
                //    err += "- “รหัสผู้ขาย:” เป็นค่าว่าง \n";
                if (dtRequire.Text.Equals(""))
                    err += "- “วันที่เบิกสินค้า:” เป็นค่าว่าง \n";
               
                if (dgvData.Rows.Count <= 0)
                    err += "- “รายการ:” เป็นค่าว่าง \n";
                int c = 0;
                foreach (GridViewRowInfo rowInfo in dgvData.Rows)
                {
                    if (rowInfo.IsVisible)
                    {
                        if (StockControl.dbClss.TInt(rowInfo.Cells["QTY"].Value) <= (0))
                        {
                            err += "- “จำนวนเบิก:” ต้องมากกว่า 0 \n";
                        }
                        else  if (StockControl.dbClss.TInt(rowInfo.Cells["QTY"].Value) != (0))
                        {
                            c += 1;
                            //if (StockControl.dbClss.TSt(rowInfo.Cells["PRNo"].Value).Equals(""))
                            //    err += "- “เลขที่ PR:” เป็นค่าว่าง \n";
                            //if (StockControl.dbClss.TSt(rowInfo.Cells["TempNo"].Value).Equals(""))
                            //    err += "- “เลขที่อ้างอิงเอกสาร PRNo:” เป็นค่าว่าง \n";
                            if (StockControl.dbClss.TSt(rowInfo.Cells["CodeNo"].Value).Equals(""))
                                err += "- “รหัสทูล:” เป็นค่าว่าง \n";
                            if (StockControl.dbClss.TDe(rowInfo.Cells["QTY"].Value) > StockControl.dbClss.TDe(rowInfo.Cells["RemainQty"].Value))
                                err += "- “จำนวนเบิก:” มากกว่าจำนวนคงเหลือ \n";
                            if (StockControl.dbClss.TDe(rowInfo.Cells["UnitShip"].Value).Equals(""))
                                err += "- “หน่วย:” เป็นค่าว่าง \n";

                        }
                    }
                }

                if (c <= 0)
                    err += "- “กรุณาระบุจำนวนที่จะเบิกสินค้า:” เป็นค่าว่าง \n";


                if (!err.Equals(""))
                    MessageBox.Show(err);
                else
                    re = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("Shipping", ex.Message, this.Name);
            }

            return re;
        }
        private void SaveHerder()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_ShippingHs
                         where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                         //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {
                    foreach (DataRow row in dt_h.Rows)
                    {
                        var gg = (from ix in db.tb_ShippingHs
                                  where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                                  //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                  select ix).First();

                        gg.UpdateBy = ClassLib.Classlib.User;
                        gg.UpdateDate = DateTime.Now;
                        dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtSHNo.Text);

                        if (StockControl.dbClss.TSt(gg.BarCode).Equals(""))
                            gg.BarCode = StockControl.dbClss.SaveQRCode2D(txtSHNo.Text.Trim());

                        if (!txtSHName.Text.Trim().Equals(row["ShipName"].ToString()))
                        {
                            gg.ShipName = txtSHName.Text;
                           
                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขผู้เบิกสินค้า [" + txtSHName.Text.Trim() + " เดิม :" + row["ShipName"].ToString() + "]", txtSHNo.Text);
                        }
                       
                        if (!txtRemark.Text.Trim().Equals(row["Remark"].ToString()))
                        {
                            gg.Remark = txtRemark.Text.Trim();
                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขหมายเหตุ [" + txtRemark.Text.Trim() + " เดิม :" + row["Remark"].ToString() + "]", txtSHNo.Text);
                        }
                      
                        if (!dtRequire.Text.Trim().Equals(""))
                        {
                            string date1 = "";
                            date1 = dtRequire.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                            string date2 = "";
                            DateTime temp = DateTime.Now;
                            if (!StockControl.dbClss.TSt(row["ShipDate"].ToString()).Equals(""))
                            {

                                temp = Convert.ToDateTime(row["ShipDate"]);
                                date2 = temp.ToString("yyyyMMdd", new CultureInfo("en-US"));

                            }
                            if (!date1.Equals(date2))
                            {
                                DateTime? RequireDate = DateTime.Now;
                                if (!dtRequire.Text.Equals(""))
                                    RequireDate = dtRequire.Value;
                                gg.ShipDate = RequireDate;
                                dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขวันที่เบิกสินค้า [" + dtRequire.Text.Trim() + " เดิม :" + temp.ToString() + "]", txtSHNo.Text);
                            }
                        }
                        db.SubmitChanges();
                    }
                }
                else //สร้างใหม่
                {
                    byte[] barcode = StockControl.dbClss.SaveQRCode2D(txtSHNo.Text.Trim());
                    DateTime? UpdateDate = null;

                    DateTime? RequireDate = DateTime.Now;
                    if (!dtRequire.Text.Equals(""))
                        RequireDate = dtRequire.Value;

                    tb_ShippingH gg = new tb_ShippingH();
                    gg.ShippingNo = txtSHNo.Text;
                    gg.ShipDate = RequireDate;
                    gg.UpdateBy = null;
                    gg.UpdateDate = UpdateDate;
                    gg.CreateBy = ClassLib.Classlib.User;
                    gg.CreateDate = DateTime.Now;
                    gg.ShipName = txtSHName.Text;
                    gg.Remark = txtRemark.Text;
                   
                    gg.BarCode = barcode;
                    gg.Status = "Completed";
                    db.tb_ShippingHs.InsertOnSubmit(gg);
                    db.SubmitChanges();

                    dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "สร้าง การเบิกสินค้า [" + txtSHNo.Text.Trim() + "]", txtSHNo.Text);
                }
            }
        }
        private void SaveDetail()
        {
            dgvData.EndEdit();
           
            DateTime? RequireDate = DateTime.Now;
            if (!dtRequire.Text.Equals(""))
                RequireDate = dtRequire.Value;
            int Seq = 0;
            DateTime? UpdateDate = null;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                decimal UnitCost = 0;
                foreach (var g in dgvData.Rows)
                {
                    string SS = "";
                    if (g.IsVisible.Equals(true))
                    {
                        if (StockControl.dbClss.TInt(g.Cells["QTY"].Value) != (0)) // เอาเฉพาะรายการที่ไม่เป็น 0 
                        {
                            if (StockControl.dbClss.TInt(g.Cells["id"].Value) <= 0)  //New ใหม่
                            {

                                //decimal RemainQty = 0;

                                UnitCost = Convert.ToDecimal(dbClss.Get_Stock(StockControl.dbClss.TSt(g.Cells["CodeNo"].Value), "", "", "Avg"));
                                Seq += 1;
                                tb_Shipping u = new tb_Shipping();
                                u.ShippingNo = txtSHNo.Text.Trim();
                                u.CodeNo = StockControl.dbClss.TSt(g.Cells["CodeNo"].Value);                              
                                u.ItemNo = StockControl.dbClss.TSt(g.Cells["ItemNo"].Value);
                                u.ItemDescription = StockControl.dbClss.TSt(g.Cells["ItemDescription"].Value);
                                u.QTY = StockControl.dbClss.TDe(g.Cells["QTY"].Value);
                                u.PCSUnit = StockControl.dbClss.TDe(g.Cells["PCSUnit"].Value);
                                u.UnitShip = StockControl.dbClss.TSt(g.Cells["UnitShip"].Value);                              
                                u.Remark = StockControl.dbClss.TSt(g.Cells["Remark"].Value);
                                u.LotNo = StockControl.dbClss.TSt(g.Cells["LotNo"].Value);
                                u.SerialNo = StockControl.dbClss.TSt(g.Cells["SerialNo"].Value);
                                u.MachineName = StockControl.dbClss.TSt(g.Cells["MachineName"].Value);
                                u.LineName = StockControl.dbClss.TSt(g.Cells["LineName"].Value);
                                u.Calbit = false;
                                u.ClearFlag = false;
                                u.ClearDate = UpdateDate;
                                u.Seq = Seq;
                                u.Status = "Completed";
                                u.UnitCost = UnitCost;
                                db.tb_Shippings.InsertOnSubmit(u);
                                db.SubmitChanges();
                                
                                //C += 1;
                                dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "เพิ่มรายการเบิก [" + u.CodeNo + " จำนวนเบิก :" + u.QTY.ToString() +" "+u.UnitShip+ "]", txtSHNo.Text);
                                
                            }
                            else
                            {
                                if (StockControl.dbClss.TInt(g.Cells["id"].Value) > 0)
                                {
                                    foreach (DataRow row in dt_d.Rows)
                                    {
                                        var u = (from ix in db.tb_Shippings
                                                 where ix.id == Convert.ToInt32(g.Cells["id"])
                                                     && ix.ShippingNo == txtSHNo.Text
                                                     && ix.CodeNo == StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                                                 select ix).First();
                                        

                                        dbClss.AddHistory(this.Name, "แก้ไขการเบิก", " แก้ไขรายการเบิก id :" + StockControl.dbClss.TSt(g.Cells["id"].Value)
                                       + " CodeNo :" + StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                                       + " แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtSHNo.Text);

                                        //u.Seq = Seq;

                                        if (!StockControl.dbClss.TSt(g.Cells["CodeNo"].Value).Equals(row["CodeNo"].ToString()))
                                        {
                                            u.CodeNo = StockControl.dbClss.TSt(g.Cells["CodeNo"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขรหัสพาร์ท [" + u.CodeNo + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["QTY"].Value).Equals(row["QTY"].ToString()))
                                        {
                                            decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(g.Cells["QTY"].Value), out QTY);
                                            u.QTY = StockControl.dbClss.TDe(g.Cells["QTY"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขจำนวนเบิก [" + QTY.ToString() + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["UnitShip"].Value).Equals(row["UnitShip"].ToString()))
                                        {
                                            u.UnitShip = StockControl.dbClss.TSt(g.Cells["UnitShip"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขหน่วย [" + u.UnitShip + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["PCSUnit"].Value).Equals(row["PCSUnit"].ToString()))
                                        {
                                            u.PCSUnit = StockControl.dbClss.TDe(g.Cells["PCSUnit"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขจำนวน/หน่วย [" + u.PCSUnit + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["LotNo"].Value).Equals(row["LotNo"].ToString()))
                                        {
                                            u.LotNo = StockControl.dbClss.TSt(g.Cells["LotNo"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไข LotNo [" + u.LotNo + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["SerialNo"].Value).Equals(row["SerialNo"].ToString()))
                                        {
                                            u.SerialNo = StockControl.dbClss.TSt(g.Cells["SerialNo"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไข ซีเรียล [" + u.SerialNo + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["MachineName"].Value).Equals(row["MachineName"].ToString()))
                                        {
                                            u.MachineName = StockControl.dbClss.TSt(g.Cells["MachineName"].Value);
                                            dbClss.AddHistory(this.Name + "แก้ไขการเบิก", "แก้ไขรายการเบิก", "แก้ไข ชื่อ Machine [" + u.MachineName + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["LineName"].Value).Equals(row["LineName"].ToString()))
                                        {
                                            u.LineName = StockControl.dbClss.TSt(g.Cells["LineName"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไข ชื่อ Line [" + u.LineName + "]", txtSHNo.Text);
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["Remark"].Value).Equals(row["Remark"].ToString()))
                                        {
                                            u.Remark = StockControl.dbClss.TSt(g.Cells["Remark"].Value);
                                            dbClss.AddHistory(this.Name, "แก้ไขการเบิก", "แก้ไขวัตถุประสงค์ [" + u.Remark + "]", txtSHNo.Text);
                                        }
                                        
                                        u.Status = "Completed";      
                                        db.SubmitChanges();
                                        
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
                if (Ac.Equals("New"))// || Ac.Equals("Edit"))
                {
                    if (Check_Save())
                        return;
                    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        if (Ac.Equals("New"))
                            txtSHNo.Text = StockControl.dbClss.GetNo(5, 2);

                    if (!txtSHNo.Text.Equals(""))
                    {
                        SaveHerder();
                        SaveDetail();

                        
                        DataLoad();
                        btnNew.Enabled = true;
                        btnDel_Item.Enabled = false;

                        ////insert Stock1
                        //Insert_Stock();

                        //insert sotck
                        InsertStock_new();
                        MessageBox.Show("บันทึกสำเร็จ!");
                        btnRefresh_Click(null,null);
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถโหลดเลขที่รับสินค้าได้ ติดต่อแผนก IT");
                    }
                    }
                }
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
                    var g = (from ix in db.tb_Shippings
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"

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
                            gg.App = "Shipping";
                            gg.Appid = Seq;
                            gg.CreateBy = ClassLib.Classlib.User;
                            gg.CreateDate = DateTime.Now;
                            gg.DocNo = CNNo;
                            gg.RefNo = txtSHNo.Text;
                            gg.Type = "Ship";
                            gg.QTY = -Convert.ToDecimal(vv.QTY);
                            gg.Inbound = 0;
                            gg.Outbound = -Convert.ToDecimal(vv.QTY); ;
                            gg.AmountCost = (-Convert.ToDecimal(vv.QTY)) * get_cost(vv.CodeNo);
                            gg.UnitCost = get_cost(vv.CodeNo);
                            gg.RemainQty = 0;
                            gg.RemainUnitCost = 0;
                            gg.RemainAmount = 0;
                            gg.CalDate = CalDate;
                            gg.Status = "Active";

                            db.tb_Stock1s.InsertOnSubmit(gg);
                            db.SubmitChanges();

                            dbClss.Insert_Stock(vv.CodeNo, (-Convert.ToDecimal(vv.QTY)), "Shipping", "Inv");


                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void InsertStock_new()
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 0;
                    string Type = "Shipping";
                    string Category = ""; //Temp,Invoice
                    decimal Cost = 0;
                   // int Flag_ClearTemp = 0;
                    decimal Qty_Inv = 0;
                    decimal Qty_DL = 0;
                    decimal Qty_Remain = 0;
                    decimal QTY = 0;
                    decimal QTY_temp = 0;

                    string Type_in_out = "Out";
                    decimal RemainQty = 0;
                    decimal Amount = 0;
                    decimal RemainAmount = 0;
                    decimal Avg = 0;
                    decimal UnitCost = 0;
                    decimal sum_Remain = 0;
                    decimal sum_Qty = 0;

                    //string CNNo = CNNo = StockControl.dbClss.GetNo(6, 2);
                    var g = (from ix in db.tb_Shippings
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"

                             select ix).ToList();
                    if (g.Count > 0)
                    {
                        //insert Stock

                        foreach (var vv in g)
                        {
                            Seq += 1;

                            QTY = Convert.ToDecimal(vv.QTY);
                            QTY_temp = 0;
                            Qty_Remain = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));  //sum ทั้งหมด
                            Qty_Inv = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "Invoice", 0))); //sum เฉพาะ Invoice
                            Qty_DL = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "Temp", 0))); // sum เฉพาะ DL
                            
                            if (QTY <= Qty_Remain)
                            {                               

                                if (Qty_Inv >= QTY) //ถ้า จำนวน remain มีมากกว่าจำนวนที่จะลบ
                                {
                                    UnitCost = Convert.ToDecimal(vv.UnitCost);
                                    if (UnitCost <= 0)
                                        UnitCost = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));

                                    Amount = (-QTY) * UnitCost;

                                    //แบบที่ 1 จะไป sum ใหม่
                                    RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                    //แบบที่ 2 จะไปดึงล่าสุดมา
                                    //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                    sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                        + Amount;

                                    sum_Qty = RemainQty + (-QTY);
                                    Avg = UnitCost;//sum_Remain / sum_Qty;
                                    RemainAmount = sum_Remain;


                                    Category = "Invoice";
                                    tb_Stock gg = new tb_Stock();
                                    gg.AppDate = AppDate;
                                    gg.Seq = Seq;
                                    gg.App = "Shipping";
                                    gg.Appid = Seq;
                                    gg.CreateBy = ClassLib.Classlib.User;
                                    gg.CreateDate = DateTime.Now;
                                    gg.DocNo = txtSHNo.Text;
                                    gg.RefNo = "";
                                    gg.CodeNo = vv.CodeNo;
                                    gg.Type = Type;
                                    gg.QTY = -Convert.ToDecimal(QTY);
                                    gg.Inbound = 0;
                                    gg.Outbound = -Convert.ToDecimal(QTY);
                                    gg.Type_i = 3;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                    gg.Category = Category;
                                    gg.Refid = vv.id;
                                    
                                    gg.CalDate = CalDate;
                                    gg.Status = "Active";
                                    gg.Flag_ClearTemp =0; //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                    gg.Type_in_out = Type_in_out;
                                    gg.AmountCost = Amount;
                                    gg.UnitCost = UnitCost;
                                    gg.RemainQty = sum_Qty;
                                    gg.RemainUnitCost = 0;
                                    gg.RemainAmount = RemainAmount;
                                    gg.Avg = Avg;


                                    db.tb_Stocks.InsertOnSubmit(gg);
                                    db.SubmitChanges();

                                    dbClss.AddHistory(this.Name, "เบิกสินค้า", " เบิกสินค้าเลขที่ : " + txtSHNo.Text + " เบิก : " + Category + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-QTY).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtSHNo.Text);

                                }
                                else
                                {
                                    QTY_temp = QTY - Qty_Inv;

                                    UnitCost = Convert.ToDecimal(vv.UnitCost);
                                    if (UnitCost <= 0)
                                        UnitCost = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                    
                                    Amount = (-QTY) * UnitCost;

                                    //แบบที่ 1 จะไป sum ใหม่
                                    RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                    //แบบที่ 2 จะไปดึงล่าสุดมา
                                    //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                    sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                        + Amount;

                                    sum_Qty = RemainQty + (-QTY);
                                    Avg = UnitCost;//sum_Remain / sum_Qty;
                                    RemainAmount = sum_Remain;

                                    Category = "Temp";
                                    tb_Stock gg = new tb_Stock();
                                    gg.AppDate = AppDate;
                                    gg.Seq = Seq;
                                    gg.App = "Shipping";
                                    gg.Appid = Seq;
                                    gg.CreateBy = ClassLib.Classlib.User;
                                    gg.CreateDate = DateTime.Now;
                                    gg.DocNo = txtSHNo.Text;
                                    gg.RefNo = "";
                                    gg.CodeNo = vv.CodeNo;
                                    gg.Type = Type;
                                    gg.QTY = -Convert.ToDecimal(QTY);
                                    gg.Inbound = 0;
                                    gg.Outbound = -Convert.ToDecimal(QTY);
                                    gg.Type_i = 3;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                    gg.Category = Category;
                                    gg.Refid = vv.id;
                                    
                                    gg.CalDate = CalDate;
                                    gg.Status = "Active";
                                    gg.Flag_ClearTemp = 1; //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                    gg.Type_in_out = Type_in_out;
                                    gg.AmountCost = Amount;
                                    gg.UnitCost = UnitCost;
                                    gg.RemainQty = sum_Qty;
                                    gg.RemainUnitCost = 0;
                                    gg.RemainAmount = RemainAmount;
                                    gg.Avg = Avg;

                                    db.tb_Stocks.InsertOnSubmit(gg);
                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "เบิกสินค้า", " เบิกสินค้าเลขที่ : " + txtSHNo.Text + " เบิก : " + Category + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-QTY).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtSHNo.Text);


                                    //// --Stock ใน Invoice ไม่พอ ต้องเอาที่ Temp มา

                                    //UnitCost = Convert.ToDecimal(vv.UnitCost);
                                    //if (UnitCost <= 0)
                                    //    UnitCost = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                    
                                    //Amount = (-QTY_temp) * UnitCost;

                                    ////แบบที่ 1 จะไป sum ใหม่
                                    //RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                    ////แบบที่ 2 จะไปดึงล่าสุดมา
                                    ////RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                    //sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                    //    + Amount;
                                    //sum_Qty = RemainQty + (-QTY_temp);
                                    //Avg = UnitCost;//sum_Remain / sum_Qty;
                                    //RemainAmount = sum_Remain;

                                    //Category = "Invoice";
                                    //tb_Stock aa = new tb_Stock();
                                    //aa.AppDate = AppDate;
                                    //aa.Seq = Seq;
                                    //aa.App = "Shipping";
                                    //aa.Appid = Seq;
                                    //aa.CreateBy = ClassLib.Classlib.User;
                                    //aa.CreateDate = DateTime.Now;
                                    //aa.DocNo = txtSHNo.Text;
                                    //aa.RefNo = "";
                                    //aa.CodeNo = vv.CodeNo;
                                    //aa.Type = Type;
                                    //aa.QTY = -Convert.ToDecimal(QTY_temp);
                                    //aa.Inbound = 0;
                                    //aa.Outbound = -Convert.ToDecimal(QTY_temp);
                                    //aa.Type_i = 3;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                    //aa.Category = Category;
                                    //aa.Refid = vv.id;
                                   
                                    //aa.CalDate = CalDate;
                                    //aa.Status = "Active";
                                    //aa.Flag_ClearTemp = 1; //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                    //aa.Type_in_out = Type_in_out;
                                    //aa.AmountCost = Amount;
                                    //aa.UnitCost = UnitCost;
                                    //aa.RemainQty = sum_Qty;
                                    //aa.RemainUnitCost = 0;
                                    //aa.RemainAmount = RemainAmount;
                                    //aa.Avg = Avg;

                                    //db.tb_Stocks.InsertOnSubmit(aa);
                                    //db.SubmitChanges();
                                    //dbClss.AddHistory(this.Name, "เบิกสินค้า", " เบิกสินค้าเลขที่ : " + txtSHNo.Text + " เบิก : " + Category + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-QTY_temp).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtSHNo.Text);

                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                dgvData.EndEdit();
                if (e.RowIndex >= -1)
                {

                    if (dgvData.Columns["QTY"].Index == e.ColumnIndex)
                    {
                        decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["QTY"].Value), out QTY);
                        decimal RemainQty = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["RemainQty"].Value), out RemainQty);
                        if (QTY > RemainQty)
                        {
                            MessageBox.Show("ไม่สามารถรับเกินจำนวนคงเหลือได้");
                            e.Row.Cells["QTY"].Value = 0;
                        }
                    }

                    if (dgvData.Columns["QTY"].Index == e.ColumnIndex
                        || dgvData.Columns["StandardCost"].Index == e.ColumnIndex
                        )
                    {
                        decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["QTY"].Value), out QTY);
                        decimal CostPerUnit = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["StandardCost"].Value), out CostPerUnit);
                        e.Row.Cells["Amount"].Value = QTY * CostPerUnit;
                        Cal_Amount();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Cal_Amount()
        {
            if (dgvData.Rows.Count() > 0)
            {
                decimal Amount = 0;
                decimal Total = 0;
                foreach (var rd1 in dgvData.Rows)
                {
                    Amount = StockControl.dbClss.TDe(rd1.Cells["Amount"].Value);
                    Total += Amount;
                }
                txtTotal.Text = Total.ToString();
            }
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
            //if (e.CellElement.ColumnInfo.Name == "ModelName")
            //{
            //    if (e.CellElement.RowInfo.Cells["ModelName"].Value != null)
            //    {
            //        if (!e.CellElement.RowInfo.Cells["ModelName"].Value.Equals(""))
            //        {
            //            e.CellElement.DrawFill = true;
            //            // e.CellElement.ForeColor = Color.Blue;
            //            e.CellElement.NumberOfColors = 1;
            //            e.CellElement.BackColor = Color.WhiteSmoke;
            //        }

            //    }
            //}
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

        //private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{
            
        //}

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
            btnDel_Item.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
           
        }

        private void txtCodeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {

                    Insert_data();
                    txtCodeNo.Text = "";

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private bool Duppicate(string CodeNo)
        {
            bool re = false;
            dgvData.EndEdit();
            foreach (var g in dgvData.Rows)
            {
                if(Convert.ToString(g.Cells["CodeNo"].Value).Equals(CodeNo))
                {
                    re = true;
                    MessageBox.Show("รหัสพาร์ทซ้ำ");
                    break;
                }
            }

            return re;
        }
        private void Insert_data()
        {
            if (!txtCodeNo.Text.Equals("") && !Duppicate(txtCodeNo.Text))
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //int No = 0;
                    //string CodeNo = "";
                    //string ItemNo = "";
                    //string ItemDescription = "";
                    //decimal QTY = 0;
                    //decimal RemainQty = 0;
                    //string UnitShip = "";
                    //decimal PCSUnit = 0;
                    //decimal StandardCost = 0;
                    //decimal Amount = 0;
                    ////string CRRNCY = "";
                    //string LotNo = "";
                    //string SerialNo = "";
                    //string Remark = "";
                    //string MachineName = "";
                    // string Status = "Waiting";
                    //int id = 0;
                    int dgvNo = 0;

                    var r = (from i in db.tb_Items
                                 //join s in db.tb_Stocks on i.CodeNo equals s.RefNo

                             where i.Status == "Active" //&& d.verticalID == VerticalID
                                && i.CodeNo == txtCodeNo.Text
                             //&& h.VendorNo.Contains(VendorNo_ss)
                             select new
                             {
                                 CodeNo = i.CodeNo,
                                 ItemNo = i.ItemNo,
                                 ItemDescription = i.ItemDescription,
                                 RemainQty = (Convert.ToDecimal(db.Cal_QTY(i.CodeNo, "", 0))),
                                 UnitShip = i.UnitShip,
                                 PCSUnit = i.PCSUnit,
                                 StandardCodt = Convert.ToDecimal(dbClss.Get_Stock(i.CodeNo, "", "", "Avg")),//i.StandardCost
                                 Amount = 0,
                                 QTY = 0,
                                 LotNo = "",
                                 SerialNo = "",
                                 MachineName = "",
                                 LineName ="",
                                 Remark ="",
                                 id = 0
                                 
                             }
                    ).ToList();
                    if (r.Count > 0)
                    {
                        dgvNo = dgvData.Rows.Count() + 1;

                        foreach (var vv in r)
                        {
                            dgvData.Rows.Add(dgvNo.ToString(),vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                        , vv.RemainQty, vv.QTY, vv.UnitShip, vv.PCSUnit, vv.StandardCodt, vv.Amount,
                                        vv.LotNo, vv.SerialNo, vv.MachineName, vv.LineName, vv.Remark, vv.id
                                        );
                        }

                    }
                    
                    Cal_Amount();
                     
                }
            }
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                btnDel_Item_Click(null, null);
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

        private void btnListItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                //btnEdit.Enabled = true;
                //btnView.Enabled = false;
                btnNew.Enabled = true;
                ClearData();
                Enable_Status(false, "View");

                this.Cursor = Cursors.WaitCursor;
                ShippingList sc = new ShippingList(txtSHNo, txtCodeNo);
                this.Cursor = Cursors.Default;
                sc.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
                //LoadData

                string CodeNo = txtCodeNo.Text;
                string SHNo = txtSHNo.Text;
                if (!txtSHNo.Text.Equals(""))
                {
                    txtCodeNo.Text = "";

                    DataLoad();
                    Ac = "View";
                    btnDel_Item.Enabled = false;
                    btnSave.Enabled = false;
                    btnNew.Enabled = true;
                }
                else
                {
                    btnDel_Item.Enabled = true;
                    btnNew_Click(null, null);
                    txtCodeNo.Text = CodeNo;

                    Insert_data();
                    txtCodeNo.Text = "";

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message + " : radButtonElement1_Click", this.Name); }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPR a = new PrintPR(txtSHNo.Text, txtSHNo.Text, "Shipping");
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
