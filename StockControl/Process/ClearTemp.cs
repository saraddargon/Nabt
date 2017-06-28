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
    public partial class ClearTemp : Telerik.WinControls.UI.RadRibbonForm
    {
        public ClearTemp()
        {
            InitializeComponent();
        }
        public ClearTemp(string RCNo)
        {
            InitializeComponent();
            RCNo_L = RCNo;

        }
        string Ac = "";
        string RCNo_L = "";

        DataTable dt_RCH = new DataTable();
        DataTable dt_RCD = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name, txtRCNo.Text);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            dt_RCH.Columns.Add(new DataColumn("RCNo", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("TempNo", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("VendorName", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("RemarkHD", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_RCH.Columns.Add(new DataColumn("RCDate", typeof(DateTime)));

            dt_RCD.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("ItemNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            dt_RCD.Columns.Add(new DataColumn("RemainQty", typeof(decimal)));
            dt_RCD.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt_RCD.Columns.Add(new DataColumn("CostPerUnit", typeof(decimal)));
            dt_RCD.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt_RCD.Columns.Add(new DataColumn("CRRNCY", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("SerialNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("ShelfNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("TempNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("PRNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("RCNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt_RCD.Columns.Add(new DataColumn("ID", typeof(int)));
            dt_RCD.Columns.Add(new DataColumn("PRID", typeof(int)));


        }
        //int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnNew_Click(null, null);
                dgvData.AutoGenerateColumns = false;
                GETDTRow();

                DefaultItem();

                if (!RCNo_L.Equals(""))
                {
                    btnNew.Enabled = true;
                    txtRCNo.Text = RCNo_L;
                    txtPRNo.Text = "";
                    DataLoad();
                    Ac = "Add";

                    txtDLNo.Enabled = false;
                    rdoDL.Enabled = false;
                    rdoInvoice.Enabled = false;
                    txtInvoiceNo.Enabled = true;
                    btnSave.Enabled = true;
                    dgvData.ReadOnly = false;

                }



            } catch (Exception ex) { MessageBox.Show(ex.Message); }
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

            dt_RCD.Rows.Clear();
            dt_RCH.Rows.Clear();
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    try
                    {
                        var g = (from ix in db.tb_ReceiveHs select ix).Where(a => a.RCNo == txtRCNo.Text.Trim()).ToList();
                        if (g.Count() > 0)
                        {
                            DateTime? temp_date = null;
                            txtVendorNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo);
                            txtVendorName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorName);
                            txtRemark.Text = StockControl.dbClss.TSt(g.FirstOrDefault().RemarkHD);



                            if (StockControl.dbClss.TSt(g.FirstOrDefault().Type).Equals("รับด้วยใบ Invoice"))
                            {
                                rdoInvoice.IsChecked = true;
                                txtInvoiceNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().InvoiceNo);
                            }
                            else //ใบส่งของชั่วคราว
                            {
                                rdoDL.IsChecked = true;
                                txtDLNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().TempNo);
                            }
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().RCDate).Equals(""))
                                dtRequire.Value = Convert.ToDateTime(g.FirstOrDefault().RCDate);
                            else
                                dtRequire.Value = Convert.ToDateTime(temp_date);


                            txtReceiveBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().CreateBy);
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().UpdateBy).Equals(""))
                                txtReceiveBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().UpdateBy);
                            if (!StockControl.dbClss.TSt(g.FirstOrDefault().CreateDate).Equals(""))
                            {
                                if (!StockControl.dbClss.TSt(g.FirstOrDefault().UpdateDate).Equals(""))
                                    txtReceiveDate.Text = Convert.ToDateTime(g.FirstOrDefault().UpdateDate).ToString("dd/MMM/yyyy");
                                else
                                    txtReceiveDate.Text = Convert.ToDateTime(g.FirstOrDefault().CreateDate).ToString("dd/MMM/yyyy");
                            }
                            else
                                txtReceiveDate.Text = "";

                            ////lblStatus.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Status);
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
                                (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Partial"))
                            {
                                btnSave.Enabled = false;
                                //btnDelete.Enabled = false;
                                //btnView.Enabled = false;
                                //btnEdit.Enabled = false;
                                lblStatus.Text = "Partial";
                                dgvData.ReadOnly = false;
                                btnNew.Enabled = true;
                            }
                            else if
                                (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Completed")
                                || StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("Discon")
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
                                btnNew.Enabled = true; dgvData.ReadOnly = true;
                            }
                            dt_RCH = StockControl.dbClss.LINQToDataTable(g);

                            //Detail
                            var d = (from ix in db.tb_Receives select ix)
                            .Where(a => a.RCNo == txtRCNo.Text.Trim()
                                && a.Status != "Cancel").ToList();
                            if (d.Count() > 0)
                            {
                                int c = 0;
                                dgvData.DataSource = d;
                                int id = 0;
                                dt_RCD = StockControl.dbClss.LINQToDataTable(d);
                                string SS = "";
                                foreach (var x in dgvData.Rows)
                                {
                                    c += 1;
                                    x.Cells["dgvNo"].Value = c;

                                    id = Convert.ToInt32(x.Cells["id"].Value);

                                    var s = (from ix in db.tb_Stocks select ix)
                                       .Where(a => a.DocNo == txtRCNo.Text.Trim()
                                            && a.Category == "Temp"
                                           && a.Refid == id).FirstOrDefault();
                                    if (s != null)
                                    {
                                        x.Cells["RemainQty"].Value = Convert.ToDecimal(s.RemainQty);
                                        x.Cells["CostPerUnit"].Value = Convert.ToDecimal(s.UnitCost);
                                        x.Cells["Amount"].Value = Math.Abs(Convert.ToDecimal(s.AmountCost));
                                    }
                                }

                                Cal_Amount();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            catch { }
            finally { this.Cursor = Cursors.Default; }


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
        private void ClearData()
        {
            txtInvoiceNo.Text = "";
            txtDLNo.Text = "";
            txtDLNo.Enabled = false;
            txtRCNo.Text = "";

            dtRequire.Value = DateTime.Now;
            txtReceiveBy.Text = ClassLib.Classlib.User;
            txtReceiveDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtRemark.Text = "";
            txtVendorName.Text = "";
            txtVendorNo.Text = "";
            txtPRNo.Text = "";
            rdoInvoice.IsChecked = true;
            dgvData.Rows.Clear();
            dt_RCD.Rows.Clear();
            dt_RCH.Rows.Clear();
        }
        private void Enable_Status(bool ss, string Condition)
        {
            if (Condition.Equals("-") || Condition.Equals("New"))
            {
                txtPRNo.Enabled = ss;
                //txtVendorName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                rdoInvoice.Enabled = ss;
                rdoDL.Enabled = ss;

            }
            else if (Condition.Equals("View"))
            {
                txtPRNo.Enabled = ss;
                //txtVendorName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                rdoInvoice.Enabled = ss;
                rdoDL.Enabled = ss;
            }

            else if (Condition.Equals("Edit"))
            {
                txtPRNo.Enabled = ss;
                //txtVendorName.Enabled = ss;
                //txtRCNo.Enabled = ss;
                txtRemark.Enabled = ss;
                //txtTempNo.Enabled = ss;
                dtRequire.Enabled = ss;
                dgvData.ReadOnly = false;
                rdoInvoice.Enabled = ss;
                rdoDL.Enabled = ss;
            }
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
            txtRCNo.Text = StockControl.dbClss.GetNo(4, 0);
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

                if (txtInvoiceNo.Text.Equals(""))
                    err += "- “Invoice No:” เป็นค่าว่าง \n";



                if (!err.Equals(""))
                    MessageBox.Show(err);
                else
                    re = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("ClearTemp", ex.Message, this.Name);
            }

            return re;
        }
        private void SaveHerder(string DocNo)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_ReceiveHs
                         where ix.RCNo.Trim() == txtRCNo.Text.Trim() && ix.Status != "Cancel"
                         //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {
                    foreach (DataRow row in dt_RCH.Rows)
                    {
                        var gg = (from ix in db.tb_ReceiveHs
                                  where ix.RCNo.Trim() == txtRCNo.Text.Trim() && ix.Status != "Cancel"
                                  //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                  select ix).First();

                        gg.UpdateBy = ClassLib.Classlib.User;
                        gg.UpdateDate = DateTime.Now;
                        dbClss.AddHistory(this.Name, "แก้ไข Receive", "Clear Temp : " + DocNo + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtRCNo.Text.Trim());
                        gg.Flag_Temp = false;
                        gg.InvoiceNo = txtInvoiceNo.Text.Trim();
                        gg.Type = "รับด้วยใบ Invoice";

                        dbClss.AddHistory(this.Name, "แก้ไข Receive", "Clear Temp : " + DocNo + "[" + txtInvoiceNo.Text.Trim() + " DL No :" + txtDLNo.Text + "]", txtRCNo.Text.Trim());

                        db.SubmitChanges();
                    }
                }

            }
        }
        private string Cal_Status()
        {
            string re = "Completed";

            decimal QTY = 0;
            decimal RemainQty = 0;
            foreach (var rd1 in dgvData.Rows)
            {
                QTY = StockControl.dbClss.TDe(rd1.Cells["QTY"].Value);
                if (QTY != 0)
                {
                    RemainQty = StockControl.dbClss.TDe(rd1.Cells["RemainQty"].Value);
                    if (QTY < RemainQty)
                    {
                        re = "Partial";
                        break;
                    }
                }

            }
            return re;
        }
        private void SaveDetail(string DocNo)
        {
            dgvData.EndEdit();

            string RCNo = txtRCNo.Text;
            //DateTime? RequireDate = DateTime.Now;
            //if (!dtRequire.Text.Equals(""))
            //    RequireDate = dtRequire.Value;
            //int Seq = 0;
            //DateTime? UpdateDate = null;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int ID = 0;
                decimal CostPerUnit = 0;
                foreach (var g in dgvData.Rows)
                {

                    ID = StockControl.dbClss.TInt(g.Cells["ID"].Value);
                    if (StockControl.dbClss.TInt(g.Cells["ID"].Value) > 0)
                    {
                        var gg = (from ix in db.tb_Receives
                                  where ix.RCNo.Trim() == txtRCNo.Text.Trim() && ix.Status != "Cancel"
                                  && ix.ID == ID
                                  select ix).First();

                        gg.InvoiceNo = txtInvoiceNo.Text.Trim();
                        //gg.TempInvNo = txtDLNo.Text;
                        gg.UpdateBy = ClassLib.Classlib.User;
                        gg.UpdateDate = DateTime.Now;

                        dbClss.AddHistory(this.Name, "แก้ไขรายการ Receive", "Clear Temp : " + DocNo + "ID :" + StockControl.dbClss.TSt(g.Cells["ID"].Value)
                       + " CodeNo :" + StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                       + " แก้ไขโดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtRCNo.Text);

                        db.SubmitChanges();

                        CostPerUnit = Convert.ToDecimal(g.Cells["CostPerUnit"].Value);

                        //insert stock
                        Save_Stock(ID, StockControl.dbClss.TSt(g.Cells["CodeNo"].Value), DocNo, CostPerUnit);


                    }
                }
            }
        }
        private void Save_Stock(int ID, string CodeNo, string DocNo, decimal CostPerUnit)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string Type = "ClearTemp";
                //string Category = "Invoice"; //Temp,Invoice
                decimal QTY = 0;
                decimal RemainQty = 0;
                decimal Amount = 0;
                decimal RemainAmount = 0;
                decimal Avg = 0;
                decimal UnitCost = 0;
                decimal sum_Remain = 0;
                decimal sum_Qty = 0;


                var g = (from ix in db.tb_Stocks
                             //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                         where ix.CodeNo.Trim() == CodeNo.Trim() && ix.Status != "Cancel"
                               && ix.Category == "Temp"
                               && ix.Type != "ClearTemp"
                               && ix.Flag_ClearTemp == 1
                               && ix.Refid == ID
                         select ix).ToList();
                if (g.Count > 0)
                {
                    foreach (var vv in g)
                    {

                        //ปรับให้เป็น ทำการ clear temp แล้ว
                        vv.Flag_ClearTemp = 2;
                        db.SubmitChanges();

                        decimal.TryParse(vv.QTY.ToString(), out QTY);
                        QTY = -QTY;

                        UnitCost = CostPerUnit;// Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "Avg"));
                        Amount = (QTY) * UnitCost;

                        //แบบที่ 1 จะไป sum ใหม่
                        RemainQty = (Convert.ToDecimal(db.Cal_QTY(CodeNo, "", 0)));
                        //แบบที่ 2 จะไปดึงล่าสุดมา
                        //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                        sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "RemainAmount"))
                            + Amount;

                        sum_Qty = RemainQty + (QTY);
                        Avg = UnitCost;//sum_Remain / sum_Qty;
                        RemainAmount = sum_Remain;

                        //insert Stock
                        //DateTime? CalDate = null;
                        DateTime? AppDate = DateTime.Now;

                        tb_Stock gg = new tb_Stock();
                        gg.AppDate = AppDate;
                        gg.Seq = vv.Seq;
                        gg.App = vv.App;
                        gg.Appid = vv.Appid;
                        gg.CreateBy = ClassLib.Classlib.User;
                        gg.CreateDate = DateTime.Now;
                        gg.DocNo = DocNo;
                        gg.RefNo = txtRCNo.Text;
                        gg.Type = Type;
                        gg.QTY = QTY;
                        gg.Inbound = QTY;
                        gg.Outbound = vv.Outbound;
                        gg.CalDate = vv.CalDate;
                        gg.Status = vv.Status;
                        gg.Flag_ClearTemp = 2;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                        gg.Type_i = vv.Type_i;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                        gg.Category = "Temp";
                        gg.Refid = Convert.ToInt32(vv.Refid);
                        gg.CodeNo = CodeNo;
                        gg.Type_in_out = "Out";
                        gg.AmountCost = Amount;
                        gg.UnitCost = UnitCost;
                        gg.RemainQty = sum_Qty;
                        gg.RemainUnitCost = vv.RemainUnitCost;
                        gg.RemainAmount = RemainAmount;
                        gg.Avg = Avg;

                        db.tb_Stocks.InsertOnSubmit(gg);
                        ////ปรับให้เป็น ทำการ clear temp แล้ว
                        //vv.Flag_ClearTemp = 2;
                        db.SubmitChanges();

                        decimal.TryParse(vv.QTY.ToString(), out QTY);
                        UnitCost = CostPerUnit;//Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "Avg"));
                        Amount = (QTY) * UnitCost;

                        //แบบที่ 1 จะไป sum ใหม่
                        RemainQty = (Convert.ToDecimal(db.Cal_QTY(CodeNo, "", 0)));
                        //แบบที่ 2 จะไปดึงล่าสุดมา
                        //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                        sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "RemainAmount"))
                            + Amount;

                        sum_Qty = RemainQty + Convert.ToDecimal(vv.QTY);
                        if (sum_Qty > 0)
                            Avg = sum_Remain / sum_Qty;
                        else
                            Avg = 0;
                        RemainAmount = sum_Qty * Avg;

                        tb_Stock bb = new tb_Stock();
                        bb.AppDate = AppDate;
                        bb.Seq = vv.Seq;
                        bb.App = vv.App;
                        bb.Appid = vv.Appid;
                        bb.CreateBy = ClassLib.Classlib.User;
                        bb.CreateDate = DateTime.Now;
                        bb.DocNo = DocNo;
                        bb.RefNo = txtRCNo.Text;
                        bb.Type = Type;
                        bb.QTY = vv.QTY;
                        bb.Inbound = vv.QTY;
                        bb.Outbound = vv.Outbound;
                        bb.CalDate = vv.CalDate;
                        bb.Status = vv.Status;
                        bb.Flag_ClearTemp = 2;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                        bb.Type_i = vv.Type_i;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                        bb.Category = "Invoice";
                        bb.Refid = Convert.ToInt32(vv.Refid);
                        bb.CodeNo = CodeNo;
                        bb.Type_in_out = "In";
                        bb.AmountCost = Amount;
                        bb.UnitCost = UnitCost;
                        bb.RemainQty = sum_Qty;
                        bb.RemainUnitCost = vv.RemainUnitCost;
                        bb.RemainAmount = RemainAmount;
                        bb.Avg = Avg;

                        //db.tb_Stocks.InsertOnSubmit(gg);
                        db.tb_Stocks.InsertOnSubmit(bb);
                        db.SubmitChanges();

                        dbClss.AddHistory(this.Name, "ClearTemp", "ClearTemp [" + txtInvoiceNo.Text + " id : " + vv.id.ToString() + " CodeNo : " + vv.CodeNo + " จำนวน : " + QTY.ToString() + "]", DocNo);
                        
                    }
                }
                //Clear temp shipping
                var s = (from ix in db.tb_Stocks
                             //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                         where ix.CodeNo.Trim() == CodeNo.Trim() && ix.Status != "Cancel"
                               && ix.Category == "Temp"
                               && ix.Type != "ClearTemp"
                               && ix.Flag_ClearTemp == 1
                               && ix.Type == "Shipping"
                         select ix).ToList();
                if (s.Count > 0)
                {
                    foreach (var vv in s)
                    {
                        //ปรับให้เป็น ทำการ clear temp แล้ว
                        vv.Flag_ClearTemp = 2;
                        db.SubmitChanges();

                        decimal.TryParse(vv.QTY.ToString(), out QTY);
                        QTY = -QTY;


                        var sh = (from ix in db.tb_Shippings
                                      //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                                  where ix.CodeNo.Trim() == CodeNo.Trim() //&& ix.Status != "Cancel"
                                        && ix.id == Convert.ToInt32(vv.Refid)
                                  select ix).First();

                        //เริ่มต้นเอา cost จาก Stock ก่อน
                        decimal.TryParse(Convert.ToString(vv.UnitCost), out CostPerUnit);
                        //เอา cost tb_shipping 
                        decimal.TryParse(Convert.ToString(sh.UnitCost), out CostPerUnit);

                        UnitCost = CostPerUnit;// Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "Avg"));
                        Amount = (QTY) * UnitCost;

                        //แบบที่ 1 จะไป sum ใหม่
                        RemainQty = (Convert.ToDecimal(db.Cal_QTY(CodeNo, "", 0)));
                        //แบบที่ 2 จะไปดึงล่าสุดมา
                        //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                        sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "RemainAmount"))
                            + Amount;

                        sum_Qty = RemainQty + (QTY);
                        Avg = UnitCost;//sum_Remain / sum_Qty;
                        RemainAmount = sum_Remain;

                        //insert Stock
                        //DateTime? CalDate = null;
                        DateTime? AppDate = DateTime.Now;

                        tb_Stock gg = new tb_Stock();
                        gg.AppDate = AppDate;
                        gg.Seq = vv.Seq;
                        gg.App = vv.App;
                        gg.Appid = vv.Appid;
                        gg.CreateBy = ClassLib.Classlib.User;
                        gg.CreateDate = DateTime.Now;
                        gg.DocNo = DocNo;
                        gg.RefNo = txtRCNo.Text;
                        gg.Type = Type;
                        gg.QTY = QTY;
                        gg.Inbound = QTY;
                        gg.Outbound = vv.Outbound;
                        gg.CalDate = vv.CalDate;
                        gg.Status = vv.Status;
                        gg.Flag_ClearTemp = 2;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                        gg.Type_i = vv.Type_i;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                        gg.Category = "Temp";
                        gg.Refid = Convert.ToInt32(vv.Refid);
                        gg.CodeNo = CodeNo;
                        gg.Type_in_out = "Out";
                        gg.AmountCost = Amount;
                        gg.UnitCost = UnitCost;
                        gg.RemainQty = sum_Qty;
                        gg.RemainUnitCost = vv.RemainUnitCost;
                        gg.RemainAmount = RemainAmount;
                        gg.Avg = Avg;

                        db.tb_Stocks.InsertOnSubmit(gg);
                        ////ปรับให้เป็น ทำการ clear temp แล้ว
                        //vv.Flag_ClearTemp = 2;
                        db.SubmitChanges();

                        decimal.TryParse(vv.QTY.ToString(), out QTY);


                        //เริ่มต้นเอา cost จาก Stock ก่อน
                        decimal.TryParse(Convert.ToString(vv.UnitCost), out CostPerUnit);
                        //เอา cost tb_shipping 
                        decimal.TryParse(Convert.ToString(sh.UnitCost), out CostPerUnit);

                        UnitCost = CostPerUnit;//Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "Avg"));

                        Amount = (QTY) * UnitCost;

                        //แบบที่ 1 จะไป sum ใหม่
                        RemainQty = (Convert.ToDecimal(db.Cal_QTY(CodeNo, "", 0)));
                        //แบบที่ 2 จะไปดึงล่าสุดมา
                        //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                        sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(CodeNo, "", "", "RemainAmount"))
                            + Amount;

                        sum_Qty = RemainQty + Convert.ToDecimal(vv.QTY);
                        if (sum_Qty > 0)
                            Avg = sum_Remain / sum_Qty;
                        else
                            Avg = 0;
                        RemainAmount = sum_Qty * Avg;

                        tb_Stock bb = new tb_Stock();
                        bb.AppDate = AppDate;
                        bb.Seq = vv.Seq;
                        bb.App = vv.App;
                        bb.Appid = vv.Appid;
                        bb.CreateBy = ClassLib.Classlib.User;
                        bb.CreateDate = DateTime.Now;
                        bb.DocNo = DocNo;
                        bb.RefNo = txtRCNo.Text;
                        bb.Type = Type;
                        bb.QTY = vv.QTY;
                        bb.Inbound = vv.QTY;
                        bb.Outbound = vv.Outbound;
                        bb.CalDate = vv.CalDate;
                        bb.Status = vv.Status;
                        bb.Flag_ClearTemp = 2;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                        bb.Type_i = vv.Type_i;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                        bb.Category = "Invoice";
                        bb.Refid = Convert.ToInt32(vv.Refid);
                        bb.CodeNo = CodeNo;
                        bb.Type_in_out = "In";
                        bb.AmountCost = Amount;
                        bb.UnitCost = UnitCost;
                        bb.RemainQty = sum_Qty;
                        bb.RemainUnitCost = vv.RemainUnitCost;
                        bb.RemainAmount = RemainAmount;
                        bb.Avg = Avg;

                        //db.tb_Stocks.InsertOnSubmit(gg);
                        db.tb_Stocks.InsertOnSubmit(bb);
                        db.SubmitChanges();

                        dbClss.AddHistory(this.Name, "ClearTemp", "ClearTemp [" + txtInvoiceNo.Text + " id : " + vv.id.ToString() + " CodeNo : " + vv.CodeNo + " จำนวน : " + QTY.ToString() + "]", DocNo);
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ac.Equals("Add"))// || Ac.Equals("Edit"))
                {
                    if (Check_Save())
                        return;
                    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        string DocNo = StockControl.dbClss.GetNo(9, 2);

                        if (!txtRCNo.Text.Equals(""))
                        {
                            SaveHerder(DocNo);
                            SaveDetail(DocNo);


                            MessageBox.Show("บันทึกสำเร็จ!");

                            DataLoad();
                            btnNew.Enabled = false;
                            txtPRNo.Enabled = false;
                            btnSave.Enabled = false;
                           
                            
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถโหลดเลขที่รับสินค้าได้ ติดต่อแผนก IT");
                        }
                    }
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void Insert_Stock1()
        {
            try
            {
                
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 0;
                    string Type = "";
                    if (rdoInvoice.IsChecked)
                        Type = "รับด้วยใบ Invoice";
                    else
                        Type = "ใบส่งของชั่วคราว";

                    decimal Cost = 0;


                    string CNNo = CNNo = StockControl.dbClss.GetNo(6, 2);
                    var g = (from ix in db.tb_Receives
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.RCNo.Trim() == txtRCNo.Text.Trim() && ix.Status != "Cancel"
                             
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
                            gg.App = "Receive";
                            gg.Appid = Seq;
                            gg.CreateBy = ClassLib.Classlib.User;
                            gg.CreateDate = DateTime.Now;
                            gg.DocNo = CNNo;
                            gg.RefNo = txtRCNo.Text;
                            gg.Type = Type;
                            gg.QTY = Convert.ToDecimal(vv.QTY);
                            gg.Inbound = Convert.ToDecimal(vv.QTY);
                            gg.Outbound = 0;

                            if(rdoDL.IsChecked)
                            {
                                gg.UnitCost = 0;
                                gg.AmountCost = 0;
                            }
                            else
                            {
                                gg.AmountCost = Convert.ToDecimal(vv.QTY) * Convert.ToDecimal(vv.CostPerUnit);
                                gg.UnitCost = Convert.ToDecimal(vv.CostPerUnit);
                            }
                            gg.RemainQty = 0;
                            gg.RemainUnitCost = 0;
                            gg.RemainAmount = 0;
                            gg.CalDate = CalDate;
                            gg.Status = "Active";

                           
                            db.tb_Stock1s.InsertOnSubmit(gg);
                            db.SubmitChanges();

                            //tb_Items inv,DL
                            dbClss.Insert_Stock(vv.CodeNo, Convert.ToDecimal(vv.QTY), "Receive", "Inv");
                            //tb_Items temp
                            dbClss.Insert_StockTemp(vv.CodeNo, (Convert.ToDecimal(vv.QTY)), "RC_Temp", "Inv");
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
                    string Type = "";
                    string Category = "";
                    if (rdoInvoice.IsChecked)
                    {
                        Category = "Invoice";
                        Type = "รับด้วยใบ Invoice";
                    }
                    else
                    {
                        Category = "Temp";
                        Type = "ใบส่งของชั่วคราว";
                    }
                    decimal Cost = 0;


                    string CNNo = CNNo = StockControl.dbClss.GetNo(6, 2);
                    var g = (from ix in db.tb_Receives
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.RCNo.Trim() == txtRCNo.Text.Trim() && ix.Status != "Cancel"

                             select ix).ToList();
                    if (g.Count > 0)
                    {
                        //insert Stock

                        foreach (var vv in g)
                        {
                            Seq += 1;

                            tb_Stock gg = new tb_Stock();
                            gg.AppDate = AppDate;
                            gg.Seq = Seq;
                            gg.App = "Receive";
                            gg.Appid = Seq;
                            gg.CreateBy = ClassLib.Classlib.User;
                            gg.CreateDate = DateTime.Now;
                            gg.DocNo = CNNo;
                            gg.RefNo = txtRCNo.Text;
                            gg.CodeNo = vv.CodeNo;
                            gg.Type = Type;
                            gg.QTY = Convert.ToDecimal(vv.QTY);
                            gg.Inbound = Convert.ToDecimal(vv.QTY);
                            gg.Outbound = 0;
                            gg.Type_i = 1;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                            gg.Category = Category;
                            gg.Refid = vv.ID;

                            if (rdoDL.IsChecked)
                            {
                                gg.UnitCost = 0;
                                gg.AmountCost = 0;
                            }
                            else
                            {
                                gg.AmountCost = Convert.ToDecimal(vv.QTY) * Convert.ToDecimal(vv.CostPerUnit);
                                gg.UnitCost = Convert.ToDecimal(vv.CostPerUnit);
                            }
                            gg.RemainQty = 0;
                            gg.RemainUnitCost = 0;
                            gg.RemainAmount = 0;
                            gg.CalDate = CalDate;
                            gg.Status = "Active";


                            db.tb_Stocks.InsertOnSubmit(gg);
                            db.SubmitChanges();


                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
       
        private void update_remainqty_Receive(string PRNo, string TempNo, int PRID, decimal RemainQty)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var u = (from ix in db.tb_Receives
                //         where
                //              //ix.TempNo == TempNo
                //              ix.PRNo == PRNo
                //             && ix.PRID == PRID
                //         select ix).ToList();

                //if (u.Count > 0)
                //{
                //    foreach (var gg in u)
                //    {
                //       gg.RemainQty = RemainQty;

                //        db.SubmitChanges();
                //        //dbClss.AddHistory(this.Name + txtRCNo.Text, "รับสินค้าครบแล้ว Receive", "รับสินค้าครบแล้ว Receive [ PRNo : " + u.PRNo + " TempNo : " + TempNo + " PRID : " + PRID.ToString() + "]", "");
                //    }
                //    //dbClss.AddHistory(this.Name + txtRCNo.Text, "รับสินค้าครบแล้ว Receive", "รับสินค้าครบแล้ว Receive [ PRNo : " + PRNo + " TempNo : " + TempNo + " PRID : " + PRID.ToString() + "]", "");
                //}
                db.sp_003_Cal_Receive_Eemain(PRID, PRNo, TempNo, RemainQty);

            }
        }
        private void Save_Status_Receive(string PRNo,string TempNo,int PRID,decimal RemainQty)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.sp_004_Update_Receive_Remain(PRID, PRNo, TempNo, RemainQty);

                //string RCNo = "";
                //var e = (from ix in db.tb_Receives
                //         where
                //              //ix.TempNo == TempNo
                //              //ix.PRNo == PRNo
                //              ix.PRID == PRID
                //         select ix).ToList();

                //if (e.Count > 0)
                //{
                //    RCNo = Convert.ToString(e.FirstOrDefault().RCNo);

                //    foreach (var gg in e)
                //    {
                //        gg.Status = "Completed";
                //        gg.RemainQty = 0;

                //        db.SubmitChanges();
                //        //dbClss.AddHistory(this.Name + txtRCNo.Text, "รับสินค้าครบแล้ว Receive", "รับสินค้าครบแล้ว Receive [ PRNo : " + u.PRNo + " TempNo : " + TempNo + " PRID : " + PRID.ToString() + "]", "");
                //    }
                //    dbClss.AddHistory(this.Name + txtRCNo.Text, "รับสินค้าครบแล้ว Receive", "รับสินค้าครบแล้ว Receive [ PRNo : " + PRNo + " TempNo : " + TempNo + " PRID : " + PRID.ToString() + "]", "");
                //}

                ////เช็คและ update ทั้ง RC
                //var r = (from ix in db.tb_Receives
                //         where
                //              ix.RCNo == RCNo
                //              && ix.RemainQty ==0
                //         select ix).ToList();
                //if(r.Count>0)
                //{
                //    foreach (var gg in r)
                //    {
                //        gg.Status = "Completed";
                //        gg.RemainQty = 0;
                //        db.SubmitChanges();
                //        //dbClss.AddHistory(this.Name + txtRCNo.Text, "รับสินค้าครบแล้ว Receive", "รับสินค้าครบแล้ว Receive [ PRNo : " + u.PRNo + " TempNo : " + TempNo + " PRID : " + PRID.ToString() + "]", "");
                //    }

                //    var h = (from ix in db.tb_ReceiveHs
                //             where
                //                  ix.RCNo == RCNo
                //              select ix).First();
                //    h.Status = "Completed";
                //    db.SubmitChanges();

                //}


            }
        }
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                /*gvData.Rows[e.RowIndex].Cells["dgvC"].Value = "T";*/
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
                        || dgvData.Columns["CostPerUnit"].Index == e.ColumnIndex
                        )
                    {
                        decimal QTY = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["QTY"].Value), out QTY);
                        decimal CostPerUnit = 0; decimal.TryParse(StockControl.dbClss.TSt(e.Row.Cells["CostPerUnit"].Value), out CostPerUnit);
                        e.Row.Cells["Amount"].Value = QTY * CostPerUnit;
                        Cal_Amount();
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

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void txtModelName_TextChanged(object sender, EventArgs e)
        {
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

        private void txtPRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if(e.KeyChar == 13)
                {

                    Insert_data();
                    txtPRNo.Text = "";
                    
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Insert_data()
        {
            if (!txtPRNo.Text.Equals(""))
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int No = 0;
                    string CodeNo = "";
                    string ItemNo = "";
                    string ItemDescription = "";
                    decimal QTY = 0;
                    decimal RemainQty = 0;
                    string Unit = "";
                    decimal PCSUnit = 0;
                    decimal CostPerUnit = 0;
                    decimal Amount = 0;
                    string CRRNCY = "";
                    string LotNo = "";
                    string SerialNo = "";
                    string Remark = "";
                    string PRNo = "";
                    string RCNo = "";
                    string TempNo = "";
                    string InvoiceNo = "";
                    if (rdoInvoice.IsChecked)
                        InvoiceNo = txtInvoiceNo.Text;
                    else
                        InvoiceNo = txtDLNo.Text;

                    int duppicate_vendor = 0;
                    string Status = "Waiting";
                    int ID = 0;
                    int PRID = 0;
                    string ShelfNo = "";


                    var g = (from ix in db.tb_PurchaseRequests select ix).Where(a => a.PRNo == txtPRNo.Text.Trim()).ToList();
                    if (g.Count() > 0)
                    {
                        if (txtVendorNo.Text.Equals(""))
                        {
                            txtVendorName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorName);
                            txtVendorNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo);
                            //txtTempNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().TEMPNo);
                            
                        }
                        else
                        {
                            if (!txtVendorNo.Text.Equals(StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo)))
                            {
                                MessageBox.Show("ไม่สามารถรับสินค้าต่างผู้ขายได้");
                                duppicate_vendor = 1;
                            }
                        }

                        CRRNCY = StockControl.dbClss.TSt(g.FirstOrDefault().CRRNCY);

                        if (duppicate_vendor <=0)
                        {
                            No = dgvData.Rows.Count() + 1;

                            var d = (from ix in db.tb_PurchaseRequestLines select ix)
                                .Where(a => a.PRNo == txtPRNo.Text.Trim() && a.SS == 1 
                                && (Convert.ToDecimal(a.RemainQty) > Convert.ToDecimal(0.00))
                                )
                                
                                .ToList();
                            if (d.Count() > 0)

                            {
                                foreach (var gg in d)
                                {
                                    CodeNo = StockControl.dbClss.TSt(gg.CodeNo);
                                    if (!check_Duppicate(CodeNo))
                                    {
                                        TempNo = StockControl.dbClss.TSt(gg.TempNo);

                                        ItemNo = StockControl.dbClss.TSt(gg.ItemName);
                                        ItemDescription = StockControl.dbClss.TSt(gg.ItemDesc);
                                        QTY = 0;//StockControl.dbClss.TDe(gg.OrderQty);
                                                //RemainQty ต้อง Cal ใหม่ ว่ารับเข้าทั้งหมดเท่าแล้วค่อยเอามาหักลบกัน
                                        RemainQty = StockControl.dbClss.TDe(gg.RemainQty);
                                        Unit = StockControl.dbClss.TSt(gg.UnitCode);
                                        // จำนวนต่อหน่วย
                                        PCSUnit = StockControl.dbClss.TDe(gg.PCSUnit);
                                        // ราคาต่อหน่วย
                                        CostPerUnit = StockControl.dbClss.TDe(gg.StandardCost);
                                        if (rdoDL.IsChecked)
                                            CostPerUnit = 0;
                                        Amount = QTY * CostPerUnit;
                                        //CRRNCY = CRRNCY;  //มาจาก herder
                                        LotNo = StockControl.dbClss.TSt(gg.LotNo);
                                        SerialNo = StockControl.dbClss.TSt(gg.SerialNo);
                                        Remark = "";
                                        PRNo = txtPRNo.Text;
                                        RCNo = "";
                                        PRID = StockControl.dbClss.TInt(gg.id);

                                        if (StockControl.dbClss.TDe(gg.OrderQty)
                                                == StockControl.dbClss.TDe(gg.RemainQty))
                                            Status = "Waiting";
                                        else
                                            Status = "Partial";


                                        dgvData.Rows.Add(No.ToString(), Status, CodeNo, ItemNo, ItemDescription, QTY, RemainQty, Unit
                                            , PCSUnit, CostPerUnit, Amount, CRRNCY, LotNo, SerialNo, ShelfNo, Remark, TempNo, PRNo, RCNo, InvoiceNo
                                            , ID.ToString(), PRID.ToString()
                                            );
                                    }
                                }
                            }
                            Cal_Amount();
                        }
                        duppicate_vendor = 0;
                    }
    
                }
            }
        }
        private void Cal_Amount()
        {
            if(dgvData.Rows.Count() >0)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //btnEdit.Enabled = true;
            //btnView.Enabled = false;
            btnNew.Enabled = true;

            string PR = txtPRNo.Text;
            ClearData();
            Enable_Status(false, "View");
            txtPRNo.Text = PR;
            DataLoad();
            btnSave.Enabled = false;
            Ac = "View";
        }

        private void btnListITem_Click(object sender, EventArgs e)
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
                ClearTempList sc = new ClearTempList(txtRCNo);
                this.Cursor = Cursors.Default;
                sc.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
                //LoadData

                //string PRNo = txtPRNo.Text;
                string RCNo = txtRCNo.Text;
                if (!txtRCNo.Text.Equals(""))
                {
                    txtPRNo.Text = "";
                    
                    DataLoad();
                    Ac = "Add";

                    txtDLNo.Enabled = false;
                    rdoDL.Enabled = false;
                    rdoInvoice.Enabled = false;
                    txtInvoiceNo.Enabled = true;
                    btnSave.Enabled = true;
                    dgvData.ReadOnly = false;
                   

                }
                //else
                //{

                //    btnNew_Click(null, null);
                //    txtPRNo.Text = PRNo;

                //    Insert_data();
                //    txtPRNo.Text = "";

                //}
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("ClearTemp", ex.Message + " : radButtonElement1_Click", this.Name); }

        }

        private void rdoDL_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if(rdoDL.IsChecked)
            {
                txtDLNo.Enabled = true;
                txtInvoiceNo.Enabled = false;
            }
        }

        private void rdoInvoice_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (rdoInvoice.IsChecked)
            {
                txtDLNo.Enabled = false;
                txtInvoiceNo.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPR a = new PrintPR(txtRCNo.Text, txtRCNo.Text, "Receive");
                a.ShowDialog();

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    var g = (from ix in db.sp_R003_ReportReceive(txtRCNo.Text, DateTime.Now) select ix).ToList();
                //    if (g.Count() > 0)
                //    {

                //        Report.Reportx1.Value = new string[2];
                //        Report.Reportx1.Value[0] = txtRCNo.Text;
                //        Report.Reportx1.WReport = "ReportReceive";
                //        Report.Reportx1 op = new Report.Reportx1("ReportReceive.rpt");
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
