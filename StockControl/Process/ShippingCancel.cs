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
    public partial class ShippingCancel : Telerik.WinControls.UI.RadRibbonForm
    {
        public ShippingCancel()
        {
            InitializeComponent();
        }

        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
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
        //private void GETDTRow()
        //{

        //    dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
        //    dt.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Order", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
        //    dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        //}
        
        private void Unit_Load(object sender, EventArgs e)
        {
            ddlType.Text = "ทั้งใบ";
            // txtCNNo.Text = StockControl.dbClss.GetNo(6, 0);
            ClearData();
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //cboVendor.AutoCompleteMode = AutoCompleteMode.Append;
                //cboVendor.DisplayMember = "VendorName";
                //cboVendor.ValueMember = "VendorNo";
                //cboVendor.DataSource =(from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo,ix.VendorName}).ToList();
                //cboVendor.SelectedIndex = -1;

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
           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            return;
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //radGridView1.Rows.AddNew();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           // radGridView1.ReadOnly = false;
           //// btnEdit.Enabled = false;
           // btnView.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }
        private bool Check_Save()
        {
            bool re = true;
            string err = "";
            try
            {

                if (ddlType.Text.Equals(""))
                {
                    //MessageBox.Show("กรุณาเลือกประเภทการคืนรายการ");
                    err += "- “กรุณาเลือกประเภทการคืนรายการ :” เป็นค่าว่าง \n";
                }

                else if (ddlType.Text.Equals("ตามรายการ") && ((txtid.Text.Equals(""))) && txtid.Text.Equals("0") && txtCodeNo.Text.Equals(""))
                {
                    //MessageBox.Show("ไม่สามารถทำการคืนรายการได้");
                    err += "- “ไม่สามารถทำการคืนรายการได้ รหัสทูล :” เป็นค่าว่าง \n";
                }
                else if (ddlType.Text.Equals("ทั้งใบ") && txtSHNo.Text.Equals(""))
                {
                    //MessageBox.Show("ไม่สามารถทำการคืนรายการได้");
                    err += "- “ไม่สามารถทำการคืนรายการได้ เลขที่เบิกทูล :” เป็นค่าว่าง \n";
                }

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    decimal Qty_Inv = 0;
                //    decimal Qty_DL = 0;
                //    decimal Qty_Remain = 0;
                //    decimal QTY = 0;
                //    decimal QTY_temp = 0;

                    //if (ddlType.Text.Equals("ตามรายการ"))
                    //{
                    //    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    //    {
                    //        decimal.TryParse(txtQTY.Text, out QTY);
                    //        QTY_temp = 0;
                    //        Qty_Remain = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "", 0)));  //sum ทั้งหมด
                    //        Qty_Inv = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "Invoice", 0))); //sum เฉพาะ Invoice
                    //        Qty_DL = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "Temp", 0))); // sum เฉพาะ DL

                    //    }
                    //}

                    //int c = 0;
                    //if (ddlType.Text.Equals("ทั้งใบ"))
                    //{
                    //    var g = (from ix in db.tb_Shippings
                    //             where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"

                    //             select ix).ToList();
                    //    if (g.Count > 0)
                    //    {

                    //        foreach (var gg in g)
                    //        {
                    //            c += 1;
                    //            QTY_temp = 0;
                    //            Qty_Remain = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "", 0)));  //sum ทั้งหมด
                    //            Qty_Inv = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "Invoice", 0))); //sum เฉพาะ Invoice
                    //            Qty_DL = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "Temp", 0))); // sum เฉพาะ DL
                    //                                                                                 //if (StockControl.dbClss.TDe(gg.QTY)
                    //                                                                                 //    err += "- “จำนวนเบิก:” มากกว่าจำนวนคงเหลือ \n";

                    //        }
                    //    }
                    //}
                //}
            
                //if (c <= 0)
                //    err += "- “กรุณาระบุจำนวนที่จะเบิกสินค้า:” เป็นค่าว่าง \n";


                if (!err.Equals(""))
                    MessageBox.Show(err);
                else
                    re = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("ShippingCancel", ex.Message, this.Name);
            }

            return re;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               

                if (Check_Save())
                    return;

                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    txtCNNo.Text = StockControl.dbClss.GetNo(6, 2);

                    if (ddlType.Text.Equals("ตามรายการ"))
                        Save_detail2();
                    else if (ddlType.Text.Equals("ทั้งใบ"))
                        Save_herder();

                    MessageBox.Show("บันทึกสำเร็จ!");

                    ClearData();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
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
            return re ;
        }
        private void Save_detail()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int id = 0;
                int.TryParse(txtid.Text, out id);
                if (id > 0)
                {
                    var g = (from ix in db.tb_Shippings
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                             && ix.id == Convert.ToInt32(txtid.Text)
                             select ix).First();

                    g.Status = "Cancel";

                    db.SubmitChanges();

                    //insert Stock
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 1;

                    tb_Stock1 gg = new tb_Stock1();
                    gg.AppDate = AppDate;
                    gg.Seq = Seq;
                    gg.App = "Cancel SH";
                    gg.Appid = Seq;
                    gg.CreateBy = ClassLib.Classlib.User;
                    gg.CreateDate = DateTime.Now;
                    gg.DocNo = txtCNNo.Text;
                    gg.RefNo = txtSHNo.Text;
                    gg.Type = ddlType.Text;
                    gg.QTY = Convert.ToDecimal(txtQTY.Text);
                    gg.Inbound = Convert.ToDecimal(txtQTY.Text);
                    gg.Outbound = 0;
                    gg.AmountCost = Convert.ToDecimal(txtQTY.Text) * get_cost(g.CodeNo);
                    gg.UnitCost = get_cost(g.CodeNo);
                    gg.RemainQty = 0;
                    gg.RemainUnitCost = 0;
                    gg.RemainAmount = 0;
                    gg.CalDate = CalDate;
                    gg.Status = "Active";
                    db.tb_Stock1s.InsertOnSubmit(gg);
                    db.SubmitChanges();

                    dbClss.AddHistory(this.Name, "เพิ่ม Stock", "Cancel รายการ Shipping [" + txtSHNo.Text.Trim() + " id : " + g.id.ToString() + "]", "");

                    //update stock item
                    dbClss.Insert_Stock(g.CodeNo, Convert.ToDecimal(g.QTY), "CNSH", "Inv");

                    //update Status
                    db.sp_007_Update_SH_Status(g.ShippingNo, Convert.ToString(g.id));
                }
            }
        }
        private void Save_detail2()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                
                int id = 0;
                int.TryParse(txtid.Text, out id);
                if (id > 0)
                {
                    string Type = "CNShipping";
                    string Category = "Invoice"; //Temp,Invoice

                    var g = (from ix in db.tb_Shippings
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                             && ix.id == Convert.ToInt32(txtid.Text)
                             select ix).First();

                    g.Status = "Cancel";

                    db.SubmitChanges();
                   

                    //insert Stock
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 1;


                    //decimal Qty_Inv = 0;
                    //decimal Qty_DL = 0;
                    //decimal Qty_Remain = 0;
                    //decimal QTY = 0;
                    //decimal QTY_temp = 0;

                    //QTY = Convert.ToDecimal(g.QTY);
                    //QTY_temp = 0;
                    //Qty_Remain = (Convert.ToDecimal(db.Cal_QTY(g.CodeNo, "", 0)));  //sum ทั้งหมด
                    //Qty_Inv = (Convert.ToDecimal(db.Cal_QTY(g.CodeNo, "Invoice", 0))); //sum เฉพาะ Invoice
                    //Qty_DL = (Convert.ToDecimal(db.Cal_QTY(g.CodeNo, "Temp", 0))); // sum เฉพาะ DL

                    string Type_in_out = "In";
                    decimal RemainQty = 0;
                    decimal Amount = 0;
                    decimal RemainAmount = 0;
                    decimal Avg = 0;
                    decimal UnitCost = 0;
                    decimal sum_Remain = 0;
                    decimal sum_Qty = 0;

                    UnitCost = Convert.ToDecimal(g.UnitCost); //get_cost(g.CodeNo);
                    Amount = Convert.ToDecimal(txtQTY.Text) * UnitCost;

                    //แบบที่ 1 จะไป sum ใหม่
                    RemainQty = (Convert.ToDecimal(db.Cal_QTY(g.CodeNo, "", 0)));
                    //แบบที่ 2 จะไปดึงล่าสุดมา
                    //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                    sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(g.CodeNo, "", "", "RemainAmount"))
                        + Amount;

                    sum_Qty = RemainQty + Convert.ToDecimal(txtQTY.Text);
                    Avg = sum_Remain / sum_Qty;
                    RemainAmount = sum_Qty * Avg;


                    //กรณีที่ Shipping แบบ Temp แล้ว cancel ให้มาปรับ Flag ด้วย

                    var s1 = (from ix in db.tb_Stocks
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.RefNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                             && ix.Refid == Convert.ToInt32(txtid.Text)
                             && ix.Category == "Temp"
                             && ix.Flag_ClearTemp == 1
                             && ix.Type == "Shipping"
                             select ix).ToList();
                    if (s1.Count > 0)
                    {
                        var s = (from ix in db.tb_Stocks
                                     //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                                 where ix.RefNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                                 && ix.Refid == Convert.ToInt32(txtid.Text)
                                 && ix.Category == "Temp"
                                 && ix.Flag_ClearTemp == 1
                                 && ix.Type == "Shipping"
                                 select ix).First();
                        if (s != null)
                        {
                            s.Flag_ClearTemp = 2;
                            db.SubmitChanges();
                        }
                    }
                    tb_Stock gg = new tb_Stock();
                    gg.AppDate = AppDate;
                    gg.Seq = Seq;
                    gg.App = "Cancel SH";
                    gg.Appid = Seq;
                    gg.CreateBy = ClassLib.Classlib.User;
                    gg.CreateDate = DateTime.Now;
                    gg.DocNo = txtCNNo.Text;
                    gg.RefNo = txtSHNo.Text;
                    gg.Type = ddlType.Text;
                    gg.CodeNo = txtCodeNo.Text.Trim();
                    gg.QTY = Convert.ToDecimal(txtQTY.Text);
                    gg.Inbound = Convert.ToDecimal(txtQTY.Text);
                    gg.Outbound = 0;
                    //gg.AmountCost = Convert.ToDecimal(txtQTY.Text) * get_cost(g.CodeNo);
                    //gg.UnitCost = get_cost(g.CodeNo);
                    //gg.RemainQty = 0;
                    //gg.RemainUnitCost = 0;
                    //gg.RemainAmount = 0;
                    gg.CalDate = CalDate;
                    gg.Status = "Active";

                    gg.Type_i = 4;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                    gg.Category = Category;
                    gg.Refid = Convert.ToInt32(txtid.Text);
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

                    dbClss.AddHistory(this.Name, "เพิ่ม Stock", "Cancel รายการ Shipping [" + txtSHNo.Text.Trim() + " id : " + g.id.ToString() + " CodeNo : " + g.CodeNo + " จำนวน : " + txtQTY.Text +"]", txtCNNo.Text);

                    //update Stock เข้า item
                    db.sp_010_Update_StockItem(Convert.ToString(txtCodeNo.Text.Trim()), "");

                    //update Status
                    db.sp_007_Update_SH_Status(g.ShippingNo, Convert.ToString(g.id));
                }
            }
        }
        private void Save_herder()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
               
                int seq = 0;
                var g = (from ix in db.tb_Shippings
                         where ix.ShippingNo.Trim() == txtSHNo.Text.Trim() && ix.Status != "Cancel"
                         
                         select ix).ToList();
                if (g.Count > 0) 
                {
                    dbClss.AddHistory(this.Name, "เพิ่ม Stock", "Cancel รายการ Shipping [" + txtSHNo.Text.Trim() + "]", "");

                    foreach (var gg in g)
                    {
                        seq += 1;
                        Save_detail2(seq, Convert.ToInt32(gg.id), gg.ShippingNo);
                    }

                    //update Status
                    db.sp_007_Update_SH_Status(txtSHNo.Text,"0");
                }
            }
        }
        private void Save_detail(int seq,int id,string SHNo)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_Shippings
                             //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                         where ix.ShippingNo.Trim() == SHNo.Trim() && ix.Status != "Cancel"
                         && ix.id == id
                         select ix).First();

                g.Status = "Cancel";
                
                //insert Stock
                DateTime? CalDate = null;
                DateTime? AppDate = DateTime.Now;
                int Seq = seq;

                tb_Stock1 gg = new tb_Stock1();
                gg.AppDate = AppDate;
                gg.Seq = Seq;
                gg.App = "Cancel SH";
                gg.Appid = Seq;
                gg.CreateBy = ClassLib.Classlib.User;
                gg.CreateDate = DateTime.Now;
                gg.DocNo = txtCNNo.Text;
                gg.RefNo = SHNo;
                gg.Type = ddlType.Text;
                gg.QTY = Convert.ToDecimal(g.QTY);
                gg.Inbound = Convert.ToDecimal(g.QTY);
                gg.Outbound = 0;
                gg.AmountCost = Convert.ToDecimal(g.QTY) * get_cost(g.CodeNo);
                gg.UnitCost = get_cost(g.CodeNo);
                gg.RemainQty = 0;
                gg.RemainUnitCost = 0;
                gg.RemainAmount = 0;
                gg.CalDate = CalDate;
                gg.Status = "Active";

                db.tb_Stock1s.InsertOnSubmit(gg);
                db.SubmitChanges();

                //update stock item
                dbClss.Insert_Stock(g.CodeNo, Convert.ToDecimal(g.QTY), "CNSH", "Inv");
            }
        }
        private void Save_detail2(int seq, int id, string SHNo)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string Type = "CNShipping";
                string Category = "Invoice"; //Temp,Invoice

                string Type_in_out = "In";
                decimal RemainQty = 0;
                decimal Amount = 0;
                decimal RemainAmount = 0;
                decimal Avg = 0;
                decimal UnitCost = 0;
                decimal sum_Remain = 0;
                decimal sum_Qty = 0;
                
                var g = (from ix in db.tb_Shippings
                             //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                         where ix.ShippingNo.Trim() == SHNo.Trim() && ix.Status != "Cancel"
                         && ix.id == id
                         select ix).First();

                g.Status = "Cancel";
                //insert Stock
                DateTime? CalDate = null;
                DateTime? AppDate = DateTime.Now;
                int Seq = seq;

                UnitCost = Convert.ToDecimal(g.UnitCost); //get_cost(g.CodeNo);
                Amount = Convert.ToDecimal(g.QTY) * UnitCost;

                //แบบที่ 1 จะไป sum ใหม่
                RemainQty = (Convert.ToDecimal(db.Cal_QTY(g.CodeNo, "", 0)));
                //แบบที่ 2 จะไปดึงล่าสุดมา
                //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));

                sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(g.CodeNo, "", "", "RemainAmount"))
                    + Amount;

                sum_Qty = RemainQty + Convert.ToDecimal(g.QTY);
                Avg = sum_Remain / sum_Qty;
                RemainAmount = sum_Qty * Avg;

                //กรณีที่ Shipping แบบ Temp แล้ว cancel ให้มาปรับ Flag ด้วย

                var s1 = (from ix in db.tb_Stocks
                             //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                         where ix.DocNo.Trim() == SHNo.Trim()
                         && ix.Status != "Cancel"
                         && ix.Refid == id
                         && ix.Category == "Temp"
                         && ix.Flag_ClearTemp == 1
                         && ix.Type == "Shipping"
                         select ix).ToList();
                if (s1.Count > 0)
                {
                    var s = (from ix in db.tb_Stocks
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.DocNo.Trim() == SHNo.Trim()
                             && ix.Status != "Cancel"
                             && ix.Refid == id
                             && ix.Category == "Temp"
                             && ix.Flag_ClearTemp == 1
                             && ix.Type == "Shipping"
                             select ix).First();
                    if (s != null)
                    {
                        s.Flag_ClearTemp = 2;
                        db.SubmitChanges();
                    }
                }
                tb_Stock gg = new tb_Stock();
                gg.AppDate = AppDate;
                gg.Seq = Seq;
                gg.App = "Cancel SH";
                gg.Appid = Seq;
                gg.CreateBy = ClassLib.Classlib.User;
                gg.CreateDate = DateTime.Now;
                gg.DocNo = txtCNNo.Text;
                gg.RefNo = SHNo;
                gg.Type = ddlType.Text;
                gg.QTY = Convert.ToDecimal(g.QTY);
                gg.Inbound = Convert.ToDecimal(g.QTY);
                gg.Outbound = 0;
                gg.CodeNo = g.CodeNo;
                gg.Status = "Active";

                gg.Type_i = 4;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                gg.Category = Category;
                gg.Refid = Convert.ToInt32(txtid.Text);
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

                dbClss.AddHistory(this.Name, "เพิ่ม Stock", "Cancel รายการ Shipping [" + txtSHNo.Text.Trim() + " id : " + g.id.ToString() + " CodeNo : " + g.CodeNo + " จำนวน : " + g.QTY.ToString() + "]", txtCNNo.Text);

                //update Stock เข้า item
                db.sp_010_Update_StockItem(Convert.ToString(g.CodeNo), "");

            }
        }
        private void ClearData()
        {
            ddlType.Text = "ทั้งใบ";
            txtSHNo.Text = "";
            txtid.Text = "0";
            txtCodeNo.Text = "";
            txtItemDescription.Text = "";
            txtQTY.Text = "";
            txtCNNo.Text =StockControl.dbClss.GetNo(6, 0);
        }
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
               // radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
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
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        return;
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        
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

       
        private void btnFilter1_Click(object sender, EventArgs e)
        {
          //  radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
           // radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
         
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            if(!ddlType.Text.Equals(""))
            {
                try
                {


                    this.Cursor = Cursors.WaitCursor;
                    ShippingList2 sc = new ShippingList2(txtSHNo,txtCodeNo,txtItemDescription,txtQTY,txtid);
                    this.Cursor = Cursors.Default;
                    sc.ShowDialog();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                    ClassLib.Memory.Heap();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("ReturnReceive", ex.Message + " : radButton1_Click_1", this.Name); }
                finally { this.Cursor = Cursors.Default; }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกประเภทการคืนก่อน");
            }
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
          
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (crow == 0)
            //    DataLoad();
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
           
        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if(radCheckBox1.Checked)
            //{
            //    foreach(var rd in radGridView1.Rows)
            //    {
            //        rd.Cells["S"].Value = true;
            //    }
            //}else
            //{
            //    foreach (var rd in radGridView1.Rows)
            //    {
            //        rd.Cells["S"].Value = false;
            //    }
            //}
        }

        private void radTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ddlType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            string temp = ddlType.Text;
            ClearData();
            ddlType.Text = temp;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnListItem_Click(object sender, EventArgs e)
        {
            ShippingCancelList a = new ShippingCancelList();
            a.Show();
        }
    }
}
