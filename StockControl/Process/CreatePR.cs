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
using System.Globalization;

namespace StockControl
{
    public partial class CreatePR : Telerik.WinControls.UI.RadRibbonForm
    {
        public CreatePR()
        {
            InitializeComponent();
        }
        public CreatePR(string PRNo)
        {
            InitializeComponent();
        }
        public CreatePR(List<GridViewRowInfo> RetDT)
        {
            InitializeComponent();
            this.RetDT = RetDT;
        }
        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
        List<GridViewRowInfo> RetDT;
        DataTable dt_PRH = new DataTable();
        DataTable dt_PRD = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name + txtPRNo.Text.Trim());
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            dt_PRH.Columns.Add(new DataColumn("PRNo", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("TEMPNo", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("VendorName", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("Address", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("ContactName", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("Tel", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("Fax", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("Email", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("RequireDate", typeof(DateTime)));
            dt_PRH.Columns.Add(new DataColumn("HDRemark", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt_PRH.Columns.Add(new DataColumn("CreateBy", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("UpdateDate", typeof(DateTime)));
            dt_PRH.Columns.Add(new DataColumn("UpdateBy", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("CRRNCY", typeof(string)));
            dt_PRH.Columns.Add(new DataColumn("Barcode", typeof(string)));


            dt_PRD.Columns.Add(new DataColumn("TempNo", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("PRNo", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("GroupCode", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("OrderQty", typeof(decimal)));
            dt_PRD.Columns.Add(new DataColumn("RemainQty", typeof(decimal)));
            dt_PRD.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt_PRD.Columns.Add(new DataColumn("StandardCost", typeof(decimal)));
            dt_PRD.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt_PRD.Columns.Add(new DataColumn("DeliveryDate", typeof(DateTime)));
            dt_PRD.Columns.Add(new DataColumn("LineName", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("MCName", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("SerialNo", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_PRD.Columns.Add(new DataColumn("SS", typeof(int)));
            
    }
        
        string Ac = "";
        private void Unit_Load(object sender, EventArgs e)
        {
            //dgvData.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            GETDTRow();
            DefaultItem();
            ClearData();
           

            if(RetDT.Count>0)
            {
                btnNew_Click(null, null);
                CreatePR_from_WaitingPR();
            }
            else
                DataLoad();


        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboVendorName.AutoCompleteMode = AutoCompleteMode.Append;
                cboVendorName.DisplayMember = "VendorName";
                cboVendorName.ValueMember = "VendorNo";
                cboVendorName.DataSource = (from ix in db.tb_Vendors.Where(s => s.Active == true)
                                        select new { ix.VendorNo,ix.VendorName,ix.CRRNCY }).ToList();
                cboVendorName.SelectedIndex = 0;


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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_PurchaseRequests select ix).Where(a => a.PRNo == txtPRNo.Text.Trim()).ToList();
                    if (g.Count() > 0)
                    {
                        

                        //txtPRNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().PRNo);
                        txtTempNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().TEMPNo);
                        txtVendorNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo);
                        cboVendorName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorName);
                        txtTel.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Tel);
                        txtContactName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().ContactName);
                        txtCurrency.Text = StockControl.dbClss.TSt(g.FirstOrDefault().CRRNCY);
                        txtFax.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Fax);
                        txtEmail.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Email);
                        txtAddress.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Address);
                        txtRemarkHD.Text = StockControl.dbClss.TSt(g.FirstOrDefault().HDRemark);

                        //lblStatus.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Status);
                        if (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Cancel"))
                        {
                            btnSave.Enabled = false;
                            btnDelete.Enabled = false;
                            btnView.Enabled = false;
                            btnEdit.Enabled = false;
                            lblStatus.Text = "Cancel";
                            dgvData.ReadOnly = true;
                            btnAdd_Item.Enabled = false;
                            btnDel_Item.Enabled = false;
                        }
                        else if
                            (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Completed"))
                        {
                            btnSave.Enabled = false;
                            btnDelete.Enabled = false;
                            btnView.Enabled = false;
                            btnEdit.Enabled = false;
                            lblStatus.Text = "Completed";
                            dgvData.ReadOnly = true;
                            btnAdd_Item.Enabled = false;
                            btnDel_Item.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnDelete.Enabled = true;
                            btnView.Enabled = true;
                            btnEdit.Enabled = true;
                            lblStatus.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Status);
                            dgvData.ReadOnly = true;
                            btnAdd_Item.Enabled = false;
                            btnDel_Item.Enabled = false;
                        }
                        dt_PRH = StockControl.dbClss.LINQToDataTable(g);

                        //Detail
                        var d = (from ix in db.tb_PurchaseRequestLines select ix)
                            .Where(a => a.PRNo == txtPRNo.Text.Trim() && a.SS == 1 ).ToList();
                        if (d.Count() > 0)
                        {
                            int c = 0;
                            dgvData.DataSource = d;
                            foreach (var x in dgvData.Rows)
                            {
                                c += 1;
                                x.Cells["dgvNo"].Value = c;
                            }
                        }
                       
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private bool CheckDuplicate(string code, string Code2)
        {
            bool ck = false;

            //using (DataClasses1DataContext db = new DataClasses1DataContext())
            //{
            //    int i = (from ix in db.tb_Models
            //             where ix.ModelName == code

            //             select ix).Count();
            //    if (i > 0)
            //        ck = false;
            //    else
            //        ck = true;
            //}

            return ck;
        }
        private void SaveHerder()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_PurchaseRequests
                         where ix.PRNo.Trim() == txtPRNo.Text.Trim() && ix.Status != "Cancel" 
                         //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {
                    foreach (DataRow row in dt_PRH.Rows)
                    {

                        var gg = (from ix in db.tb_PurchaseRequests
                                  where ix.PRNo.Trim() == txtPRNo.Text.Trim() 
                                  //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                  select ix).First();

                        //gg.Status = "Waiting";
                        //gg.TEMPNo = txtTempNo.Text;
                        gg.UpdateBy = ClassLib.Classlib.User;
                        gg.UpdateDate = DateTime.Now;
                        dbClss.AddHistory(this.Name + txtPRNo.Text.Trim(), "แก้ไข CreatePR", "แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                        if (StockControl.dbClss.TSt(gg.Barcode).Equals(""))
                            gg.Barcode = StockControl.dbClss.SaveQRCode2D(txtPRNo.Text.Trim());

                        if (!txtVendorNo.Text.Trim().Equals(row["VendorNo"].ToString()))
                        {
                            gg.VendorName = cboVendorName.Text;
                            gg.VendorNo = txtVendorNo.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขรหัสผู้ขาย [" + txtVendorNo.Text.Trim() + "]", "");
                        }
                        if (!txtCurrency.Text.Trim().Equals(row["CRRNCY"].ToString()))
                        {
                            gg.CRRNCY = txtCurrency.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขสกุลเงิน [" + txtCurrency.Text.Trim() + "]", "");
                        }
                        if (!txtContactName.Text.Trim().Equals(row["ContactName"].ToString()))
                        {
                            gg.ContactName = txtContactName.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขผู้ติดต่อ [" + txtContactName.Text.Trim() + "]", "");
                        }
                        if (!txtAddress.Text.Trim().Equals(row["Address"].ToString()))
                        {
                            gg.Address = txtAddress.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขที่อยู่ [" + txtAddress.Text.Trim() + "]", "");
                        }
                        if (!txtTel.Text.Trim().Equals(row["Tel"].ToString()))
                        {
                            gg.Tel = txtTel.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขเบอร์โทร [" + txtTel.Text.Trim() + "]", "");
                        }
                        if (!txtFax.Text.Trim().Equals(row["Fax"].ToString()))
                        {
                            gg.Fax = txtFax.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขเบอร์แฟกซ์ [" + txtFax.Text.Trim() + "]", "");
                        }
                        
                        if (!txtEmail.Text.Trim().Equals(row["Email"].ToString()))
                        {
                            gg.Email = txtEmail.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขอีเมลล์ [" + txtEmail.Text.Trim() + "]", "");
                        }

                        if (!dtRequire.Text.Trim().Equals(""))
                        {
                            string date1 = "";
                            date1 = dtRequire.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                            string date2 = "";
                            if(!StockControl.dbClss.TSt(row["RequireDate"].ToString()).Equals(""))
                            {
                                DateTime temp = DateTime.Now;
                                temp = Convert.ToDateTime(row["RequireDate"]);
                                date2 = temp.ToString("yyyyMMdd", new CultureInfo("en-US"));

                            }
                            if(!date1.Equals(date2))
                            {
                                DateTime? RequireDate = DateTime.Now;
                                if (!dtRequire.Text.Equals(""))
                                    RequireDate = dtRequire.Value;
                                gg.RequireDate = RequireDate;
                                dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขวันที่ต้องการ [" + dtRequire.Text.Trim() + "]", "");

                            }
                            
                        }
                        if (!txtRemarkHD.Text.Trim().Equals(row["HDRemark"].ToString()))
                        {
                            gg.HDRemark = txtRemarkHD.Text.Trim();
                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข CreatePR", "แก้ไขหมายเหตุ [" + txtRemarkHD.Text.Trim() + "]", "");
                        }
                        db.SubmitChanges();
                    }
                }
                else  // Add ใหม่
                {
                    byte[] barcode = StockControl.dbClss.SaveQRCode2D(txtPRNo.Text.Trim());
                    DateTime? UpdateDate = null;
                    
                    tb_PurchaseRequest gg = new tb_PurchaseRequest();
                    gg.UpdateBy = null;
                    gg.UpdateDate = UpdateDate;
                    gg.CreateBy = ClassLib.Classlib.User;
                    gg.CreateDate = DateTime.Now;
                    gg.VendorName = cboVendorName.Text;
                    gg.VendorNo = txtVendorNo.Text.Trim();
                    gg.Address = txtAddress.Text.Trim();
                    gg.Tel = txtTel.Text.Trim();
                    gg.Fax = txtFax.Text.Trim();
                    gg.ContactName = txtContactName.Text.Trim();
                    gg.Email = txtEmail.Text.Trim();
                    gg.Barcode = barcode;
                    gg.PRNo = txtPRNo.Text;
                    gg.TEMPNo = txtTempNo.Text;

                    DateTime? RequireDate = DateTime.Now;
                    if (!dtRequire.Text.Equals(""))
                        RequireDate = dtRequire.Value;

                    gg.RequireDate = RequireDate;
                    gg.HDRemark = txtRemarkHD.Text.Trim();
                    gg.CRRNCY = txtCurrency.Text.Trim();
                    gg.Status = "Waiting";

                    db.tb_PurchaseRequests.InsertOnSubmit(gg);
                    db.SubmitChanges();
                    
                    dbClss.AddHistory(this.Name + txtPRNo.Text.Trim(), "สร้าง CreatePR", "สร้าง PRNo [" + txtPRNo.Text.Trim() + ",เลขที่อ้างอิง :"+txtTempNo.Text+ "]", "");

                }
            }
        }
        private bool AddPR_d()
        {
          
            bool ck = false;
            //int C = 0;
            //try
            //{


                dgvData.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in dgvData.Rows)
                   {
                        if (g.IsVisible.Equals(true))
                        {
                            DateTime? d = null;
                            DateTime? DeliveryDate = DateTime.Now;
                            if (StockControl.dbClss.TInt(g.Cells["dgvid"].Value)<=0)  //New ใหม่
                            {

                                tb_PurchaseRequestLine u = new tb_PurchaseRequestLine();
                                u.PRNo = txtPRNo.Text;
                                u.TempNo = txtTempNo.Text;
                                u.CodeNo = StockControl.dbClss.TSt(g.Cells["dgvCodeNo"].Value);
                                u.ItemName = StockControl.dbClss.TSt(g.Cells["dgvItemName"].Value);
                                u.ItemDesc = StockControl.dbClss.TSt(g.Cells["dgvItemDesc"].Value);
                                u.GroupCode = StockControl.dbClss.TSt(g.Cells["dgvGroupCode"].Value);
                                u.OrderQty = StockControl.dbClss.TDe(g.Cells["dgvOrderQty"].Value);
                                u.PCSUnit = StockControl.dbClss.TDe(g.Cells["dgvPCSUnit"].Value);
                                u.UnitCode = StockControl.dbClss.TSt(g.Cells["dgvUnitCode"].Value);
                                u.StandardCost = StockControl.dbClss.TDe(g.Cells["dgvStandardCost"].Value);
                                u.Amount = StockControl.dbClss.TDe(g.Cells["dgvAmount"].Value);
                                u.Remark = StockControl.dbClss.TSt(g.Cells["dgvRemark"].Value);
                                u.LotNo = StockControl.dbClss.TSt(g.Cells["dgvLotNo"].Value);
                                u.SerialNo = StockControl.dbClss.TSt(g.Cells["dgvSerialNo"].Value);
                                u.MCName = StockControl.dbClss.TSt(g.Cells["dgvMCName"].Value);
                                u.LineName = StockControl.dbClss.TSt(g.Cells["dgvLineName"].Value);

                                if (!StockControl.dbClss.TSt(g.Cells["dgvDeliveryDate"].Value).Equals(""))
                                    DeliveryDate = Convert.ToDateTime((g.Cells["dgvDeliveryDate"].Value));
                                else
                                    DeliveryDate = d;
                                u.DeliveryDate = DeliveryDate;
                                u.RemainQty = StockControl.dbClss.TDe(g.Cells["dgvRemainQty"].Value);
                                u.SS = 1;
                                db.tb_PurchaseRequestLines.InsertOnSubmit(u);
                                db.SubmitChanges();
                                //C += 1;
                                dbClss.AddHistory(this.Name, "เพิ่ม Item PR", "เพิ่มรายการ Create PR [" + u.CodeNo + "]", "");

                            }
                            else  // อัพเดต
                            {

                                if (StockControl.dbClss.TInt(g.Cells["dgvid"].Value) > 0)
                                {
                                    foreach (DataRow row in dt_PRD.Rows)
                                    {
                                        var u = (from ix in db.tb_PurchaseRequestLines
                                                 where ix.PRNo == txtPRNo.Text.Trim() 
                                                // && ix.TempNo == txtTempNo.Text
                                                 && ix.id == StockControl.dbClss.TInt(g.Cells["dgvid"].Value)
                                                 select ix).First();

                                        dbClss.AddHistory(this.Name + txtPRNo.Text, "เพิ่ม Item PR", "id :" + StockControl.dbClss.TSt(g.Cells["dgvid"].Value)
                                        + " CodeNo :" + StockControl.dbClss.TSt(g.Cells["dgvCodeNo"].Value)
                                        + " แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                                        if (!StockControl.dbClss.TSt(g.Cells["dgvCodeNo"].Value).Equals(row["CodeNo"].ToString()))
                                        {
                                            u.CodeNo = StockControl.dbClss.TSt(g.Cells["dgvCodeNo"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขรหัสพาร์ท [" + u.CodeNo + "]", "");
                                        }
                                       
                                        u.ItemName = StockControl.dbClss.TSt(g.Cells["dgvItemName"].Value);
                                        u.ItemDesc = StockControl.dbClss.TSt(g.Cells["dgvItemDesc"].Value);
                                        u.GroupCode = StockControl.dbClss.TSt(g.Cells["dgvGroupCode"].Value);

                                        if (!StockControl.dbClss.TSt(g.Cells["dgvOrderQty"].Value).Equals(row["OrderQty"].ToString()))
                                        {
                                            decimal OrderQty = 0; decimal.TryParse(StockControl.dbClss.TSt(g.Cells["dgvOrderQty"].Value), out OrderQty);
                                            u.OrderQty = StockControl.dbClss.TDe(g.Cells["dgvOrderQty"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขจำนวน [" + OrderQty.ToString() + "]", "");
                                        }
                                        
                                        u.PCSUnit = StockControl.dbClss.TDe(g.Cells["dgvPCSUnit"].Value);

                                        if (!StockControl.dbClss.TSt(g.Cells["dgvUnitCode"].Value).Equals(row["UnitCode"].ToString()))
                                        {
                                            u.UnitCode = StockControl.dbClss.TSt(g.Cells["dgvUnitCode"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขหน่วย [" + u.UnitCode + "]", "");
                                        }
                                       
                                        u.StandardCost = StockControl.dbClss.TDe(g.Cells["dgvStandardCost"].Value);
                                        u.Amount = StockControl.dbClss.TDe(g.Cells["dgvAmount"].Value);
                                        if (!StockControl.dbClss.TSt(g.Cells["dgvRemark"].Value).Equals(row["Remark"].ToString()))
                                        {
                                            u.Remark = StockControl.dbClss.TSt(g.Cells["dgvRemark"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขวัตถุประสงค์ [" + u.Remark + "]", "");
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["dgvLotNo"].Value).Equals(row["LotNo"].ToString()))
                                        {
                                            u.LotNo = StockControl.dbClss.TSt(g.Cells["dgvLotNo"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไข LotNo [" + u.LotNo + "]", "");
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["dgvSerialNo"].Value).Equals(row["SerialNo"].ToString()))
                                        {
                                            u.SerialNo = StockControl.dbClss.TSt(g.Cells["dgvSerialNo"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขซีเรียล [" + u.SerialNo + "]", "");
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["dgvMCName"].Value).Equals(row["MCName"].ToString()))
                                        {
                                            u.MCName = StockControl.dbClss.TSt(g.Cells["dgvMCName"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขชื่อ Machine [" + u.MCName + "]", "");
                                        }
                                        if (!StockControl.dbClss.TSt(g.Cells["dgvLineName"].Value).Equals(row["LineName"].ToString()))
                                        {
                                            u.LineName = StockControl.dbClss.TSt(g.Cells["dgvLineName"].Value);
                                            dbClss.AddHistory(this.Name + txtPRNo.Text, "แก้ไข Item PR", "แก้ไขชื่อ Line [" + u.LineName + "]", "");
                                        }
                                        

                                        if (!StockControl.dbClss.TSt(g.Cells["dgvDeliveryDate"].Value).Equals(""))
                                            DeliveryDate = Convert.ToDateTime((g.Cells["dgvDeliveryDate"].Value));
                                        else
                                            DeliveryDate = d;
                                        u.DeliveryDate = DeliveryDate;

                                        u.RemainQty = StockControl.dbClss.TDe(g.Cells["dgvRemainQty"].Value);
                                        u.SS = 1;


                                        //C += 1;
                                        db.SubmitChanges();
                                    }
                                }

                            }
                        }
                        else //Del
                        {
                            if (StockControl.dbClss.TInt(g.Cells["dgvid"].Value) > 0)
                            {
                                var u = (from ix in db.tb_PurchaseRequestLines
                                         where ix.PRNo == txtPRNo.Text.Trim() 
                                         //&& ix.TempNo == txtTempNo.Text 
                                         &&  ix.id == StockControl.dbClss.TInt(g.Cells["dgvid"].Value)
                                         select ix).First();
                                u.SS = 0;
                               
                                dbClss.AddHistory(this.Name + txtPRNo.Text, "ลบ Item PR", "id :"+StockControl.dbClss.TSt(g.Cells["dgvid"].Value)
                                    + " CodeNo :" + StockControl.dbClss.TSt(g.Cells["dgvCodeNo"].Value)
                                    + " ลบโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");


                                db.SubmitChanges();
                            }
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    dbClss.AddError("CreatePR", ex.Message, this.Name);
            //}

            //if (C > 0)
            //    MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
           
        }
        private void Enable_Status(bool ss, string Condition)
        {
            if (Condition.Equals("-") || Condition.Equals("New"))
            {
                txtPRNo.Enabled = ss;
                cboVendorName.Enabled = ss;
                txtTel.Enabled = ss;
                txtFax.Enabled = ss;
                txtContactName.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                txtRemarkHD.Enabled = ss;
                //txtCurrency.Enabled = ss;
                btnAdd_Item.Enabled = ss;
                btnDel_Item.Enabled = ss;
                //txtVendorNo.Enabled = ss;
                txtEmail.Enabled = ss;
                txtAddress.Enabled = ss;
            }
            else if (Condition.Equals("View"))
            {
                txtPRNo.Enabled = ss;
                cboVendorName.Enabled = ss;
                txtTel.Enabled = ss;
                txtFax.Enabled = ss;
                txtContactName.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = !ss;
                txtRemarkHD.Enabled = ss;
                //txtCurrency.Enabled = ss;
                btnAdd_Item.Enabled = ss;
                btnDel_Item.Enabled = ss;
                //txtVendorNo.Enabled = ss;
                txtEmail.Enabled = ss;
                txtAddress.Enabled = ss;
            }
            else if (Condition.Equals("Edit"))
            {
                txtPRNo.Enabled = ss;
                cboVendorName.Enabled = ss;
                txtTel.Enabled = ss;
                txtFax.Enabled = ss;
                txtContactName.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = !ss;
                txtRemarkHD.Enabled = ss;
                //txtCurrency.Enabled = ss;
                //txtVendorNo.Enabled = ss;
                btnAdd_Item.Enabled = ss;
                btnDel_Item.Enabled = ss;
                txtEmail.Enabled = ss;
                txtAddress.Enabled = ss;
            }
        }
       
        private void ClearData()
        {
            txtPRNo.Text = "";
            cboVendorName.Text = "";
            txtTempNo.Text = "";
            txtTel.Text = "";
            txtFax.Text = "";
            txtAddress.Text = "";
            txtContactName.Text = "";
            lblStatus.Text = "-";
            dtRequire.Value = DateTime.Now;
            dgvData.Rows.Clear();
            txtRemarkHD.Text = "";
            txtCurrency.Text = "";
            txtVendorNo.Text = "";
            txtEmail.Text = "";

            dt_PRH.Rows.Clear();
            dt_PRD.Rows.Clear();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            btnView.Enabled = true;
            btnEdit.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            ClearData();
            Enable_Status(true, "New");
            lblStatus.Text = "New";
            Ac = "New";

            //getมาไว้ก่อน แต่ยังไมได้ save 
            txtTempNo.Text = StockControl.dbClss.GetNo(3, 0);
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            btnView.Enabled = false;
            btnEdit.Enabled = true;
            btnNew.Enabled = true;
            btnSave.Enabled = true;
          
            Enable_Status(false, "View");
            lblStatus.Text = "View";
            Ac = "View";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnView.Enabled = true;
            btnEdit.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = true;
            

            Enable_Status(true, "Edit");
            lblStatus.Text = "Edit";
            Ac = "Edit";
           

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                Ac = "Del";
                if (MessageBox.Show("ต้องการลบรายการ ( " + txtPRNo.Text + " ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var g = (from ix in db.tb_PurchaseRequests
                                 where ix.PRNo.Trim() == txtPRNo.Text.Trim() && ix.Status != "Cancel" 
                                 //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                 select ix).ToList();
                        if (g.Count > 0)  //มีรายการในระบบ
                        {
                            var gg = (from ix in db.tb_PurchaseRequests
                                      where ix.PRNo.Trim() == txtPRNo.Text.Trim() 
                                      //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                      select ix).First();


                            gg.Status = "Cancel";
                            gg.UpdateBy = ClassLib.Classlib.User;
                            gg.UpdateDate = DateTime.Now;

                            dbClss.AddHistory(this.Name + txtPRNo.Text.Trim(), "ลบ PR", "Delete PRNo [" + txtPRNo.Text.Trim() + "]", "");

                            db.SubmitChanges();
                            btnNew_Click(null, null);
                            btnSave.Enabled = true;
                        }
                        else // ไม่มีในระบบ
                        {
                            btnNew_Click(null, null);
                            btnSave.Enabled = true;
                        }
                    }

                    MessageBox.Show("ลบรายการ สำเร็จ!");
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }
        private bool Check_Save()
        {
            bool re = true;
            string err = "";
            try
            {
                //if (txtCodeNo.Text.Equals(""))
                //    err += " “รหัสพาร์ท:” เป็นค่าว่าง \n";
                if (txtPRNo.Text.Equals(""))
                    err += " “เลขที่ใบขอสั่งซื้อ:” เป็นค่าว่าง \n";
                if (cboVendorName.Text.Equals(""))
                    err += "- “เลือกผู้ขาย:” เป็นค่าว่าง \n";
                if (txtVendorNo.Text.Equals(""))
                    err += "- “รหัสผู้ขาย:” เป็นค่าว่าง \n";
                if (txtCurrency.Text.Equals(""))
                    err += "- “สกุลเงิน:” เป็นค่าว่าง \n";
                if (txtContactName.Text.Equals(""))
                    err += "- “ผู้ติดต่อ:” เป็นค่าว่าง \n";
                if (txtAddress.Text.Equals(""))
                    err += "- “ที่อยู่:” เป็นค่าว่าง \n";
                if (txtTel.Text.Equals(""))
                    err += "- “เบอร์โทร:” เป็นค่าว่าง \n";
                if (dtRequire.Text.Equals(""))
                    err += "- “วันที่ต้องการ:” เป็นค่าว่าง \n";

                foreach (GridViewRowInfo rowInfo in dgvData.Rows)
                {
                    if (rowInfo.IsVisible)
                    {
                       if(StockControl.dbClss.TSt(rowInfo.Cells["dgvCodeNo"].Value).Equals(""))
                           err += "- “รหัสพาร์ท:” เป็นค่าว่าง \n";
                        if (StockControl.dbClss.TDe(rowInfo.Cells["dgvOrderQty"].Value)<=0)
                            err += "- “จำนวน:” น้อยกว่า 0 \n";
                        if(StockControl.dbClss.TDe(rowInfo.Cells["dgvUnitCode"].Value).Equals(""))
                            err += "- “หน่วย:” เป็นค่าว่าง \n";
                    }
                }


                        if (!err.Equals(""))
                    MessageBox.Show(err);
                else
                    re = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("CreatePR", ex.Message, this.Name);
            }

            return re;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ac.Equals("New") || Ac.Equals("Edit"))
                {
                    if (Check_Save())
                        return;
                    else
                    {

                        if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.Cursor = Cursors.WaitCursor;

                            if (Ac.Equals("New"))
                                txtTempNo.Text = StockControl.dbClss.GetNo(3, 2);

                            if (!txtTempNo.Text.Equals(""))
                            {
                                SaveHerder();
                                AddPR_d();
                                DataLoad();
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("สถานะต้องเป็น New หรือ Edit เท่านั่น");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
       
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                dgvData.EndEdit();
                 if (e.RowIndex >= -1)
                {
                    if (dgvData.Columns["dgvOrderQty"].Index == e.ColumnIndex
                        || dgvData.Columns["dgvStandardCost"].Index == e.ColumnIndex
                        )
                    {
                        decimal OrderQty = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["dgvOrderQty"].Value), out OrderQty);
                        decimal StandardCost = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["dgvStandardCost"].Value), out StandardCost);
                        e.Row.Cells["dgvAmount"].Value = OrderQty * StandardCost;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());

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

        private void radLabel5_Click(object sender, EventArgs e)
        {

        }

        private void เพมพารทToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtVendorNo.Text.Equals(""))
                {
                    List<GridViewRowInfo> dgvRow_List = new List<GridViewRowInfo>();
                    //dgvRow_List.Clear();
                    ListPart_CreatePR MS = new ListPart_CreatePR(dgvRow_List, txtVendorNo.Text);
                    MS.ShowDialog();
                    if (dgvRow_List.Count > 0)
                    {
                        string CodeNo = "";
                        this.Cursor = Cursors.WaitCursor;
                        int OrderQty = 1;
                        foreach (GridViewRowInfo ee in dgvRow_List)
                        {
                            CodeNo = Convert.ToString(ee.Cells["CodeNo"].Value).Trim();
                            if (!CodeNo.Equals(""))
                            {
                                Add_Part(CodeNo, OrderQty);
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("เลือกผู้ขายก่อน !!!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void Add_Part(string CodeNo,int OrderQty)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int Row = 0; Row = dgvData.Rows.Count()+1;
                var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo.Contains(CodeNo)).ToList();
                if (g.Count > 0)
                {
                    dgvData.Rows.Add(Row.ToString(), CodeNo,
                        StockControl.dbClss.TSt(g.FirstOrDefault().ItemNo)
                        , StockControl.dbClss.TSt(g.FirstOrDefault().ItemDescription)
                        , StockControl.dbClss.TSt(g.FirstOrDefault().GroupCode)
                        , OrderQty
                        , StockControl.dbClss.TDe(g.FirstOrDefault().PCSUnit)
                        , StockControl.dbClss.TSt(g.FirstOrDefault().UnitBuy)
                        , StockControl.dbClss.TDe(g.FirstOrDefault().StandardCost)
                        , 1 * StockControl.dbClss.TDe(g.FirstOrDefault().StandardCost)
                        , ""
                        , "" //Lotno
                        , "" //SerialNo
                        , "" //MCName
                        , "" //LineName
                        , DateTime.Now
                        ,0.0 // RemainQty
                        ,0
                        );
                }
            }
        }
        private void ลบพารทToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (dgvData.Rows.Count < 0)
                    return;


                if (Ac.Equals("New") || Ac.Equals("Edit"))
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    int id = 0;
                    int.TryParse(StockControl.dbClss.TSt(dgvData.CurrentRow.Cells["dgvid"].Value), out id);
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
        private void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!cboVendorName.Text.Equals(""))
                    {
                        var I = (from ix in db.tb_Vendors select ix).Where(a => a.Active == true && a.VendorName.Equals(cboVendorName.Text)).ToList();
                        if (I.Count > 0)
                        {
                            //StockControl.dbClss.TBo(a.Active).Equals(true)
                            txtCurrency.Text = I.FirstOrDefault().CRRNCY;
                            txtAddress.Text = I.FirstOrDefault().Address;
                            txtVendorNo.Text = I.FirstOrDefault().VendorNo;
                            var g = (from ix in db.tb_VendorContacts select ix).Where(a => a.VendorNo.Equals(txtVendorNo.Text)).OrderByDescending(b => b.DefaultNo).ToList();
                            if (g.Count > 0)
                            {
                                txtContactName.Text = g.FirstOrDefault().ContactName;
                                txtTel.Text = g.FirstOrDefault().Tel;
                                txtFax.Text = g.FirstOrDefault().Fax;
                                txtEmail.Text = g.FirstOrDefault().Email;
                            }
                            else
                            {
                                txtContactName.Text = "";
                                txtTel.Text = "";
                                txtFax.Text = "";
                                txtEmail.Text = "";
                            }
                        }
                        else
                        {
                            txtCurrency.Text = "";
                            txtAddress.Text = "";
                            txtVendorNo.Text = "";
                            txtContactName.Text = "";
                            txtTel.Text = "";
                            txtFax.Text = "";
                        }
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void MasterTemplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                ลบพารทToolStripMenuItem_Click(null, null);
        }

        private void btnListItem_Click(object sender, EventArgs e)
        {
            ////DataLoad();
            try
            {
                btnEdit.Enabled = true;
                btnView.Enabled = false;
                btnNew.Enabled = true;
                ClearData();
                Enable_Status(false, "View");

                this.Cursor = Cursors.WaitCursor;
                CreatePR_List sc = new CreatePR_List(txtPRNo);
                this.Cursor = Cursors.Default;
                sc.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
                //LoadData
                DataLoad();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message + " : radButtonElement1_Click", this.Name); }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnView.Enabled = false;
            btnNew.Enabled = true;
            
            string PR = txtPRNo.Text;
            ClearData();
            Enable_Status(false, "View");
            txtPRNo.Text = PR;
            DataLoad();
        }

        private void txtPRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !txtPRNo.Text.Equals(""))
                DataLoad();
        }
        private void CreatePR_from_WaitingPR()
        {
            try
            {
                if (RetDT.Count > 0)
                {
                    string CodeNo = "";
                    this.Cursor = Cursors.WaitCursor;
                    string VendorNo = "";
                    foreach (GridViewRowInfo ee in RetDT)
                    {
                        VendorNo = Convert.ToString(ee.Cells["VendorNo"].Value).Trim();
                        if(!VendorNo.Equals(""))
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                var I = (from ix in db.tb_Vendors select ix).Where(a => a.Active == true 
                                && a.VendorNo.Equals(VendorNo)).ToList();
                                if (I.Count > 0)
                                {
                                    //StockControl.dbClss.TBo(a.Active).Equals(true)
                                    txtCurrency.Text = I.FirstOrDefault().CRRNCY;
                                    txtAddress.Text = I.FirstOrDefault().Address;
                                    txtVendorNo.Text = I.FirstOrDefault().VendorNo;
                                    cboVendorName.Text = I.FirstOrDefault().VendorName;
                                    var g = (from ix in db.tb_VendorContacts select ix).Where(a => a.VendorNo.Equals(txtVendorNo.Text)).OrderByDescending(b => b.DefaultNo).ToList();
                                    if (g.Count > 0)
                                    {
                                        txtContactName.Text = g.FirstOrDefault().ContactName;
                                        txtTel.Text = g.FirstOrDefault().Tel;
                                        txtFax.Text = g.FirstOrDefault().Fax;
                                        txtEmail.Text = g.FirstOrDefault().Email;
                                        
                                    }
                                }
                            }

                        }

                        CodeNo = Convert.ToString(ee.Cells["CodeNo"].Value).Trim();
                        if (!CodeNo.Equals(""))
                        {
                            Add_Part(CodeNo,StockControl.dbClss.TInt(ee.Cells["Order"].Value));

                        }
                        
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}