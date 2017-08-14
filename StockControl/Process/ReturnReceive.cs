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
    public partial class ReturnReceive : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReturnReceive()
        {
            InitializeComponent();
        }

      
        DataTable dt_PRID = new DataTable();
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

            dt_PRID.Columns.Add(new DataColumn("PRID", typeof(int)));
            dt_PRID.Columns.Add(new DataColumn("PRNo", typeof(string)));
            dt_PRID.Columns.Add(new DataColumn("OrderQty", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            //dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        }
       
        private void Unit_Load(object sender, EventArgs e)
        {
          
            GETDTRow();
            //DefaultItem();
            

           
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
        private void DataLoad()
        {
           
            try
            {

                this.Cursor = Cursors.WaitCursor;
               
                   
                
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;

        }
        //private bool CheckDuplicate(string code, string Code2)
        //{
        //    bool ck = false;

        //    using (DataClasses1DataContext db = new DataClasses1DataContext())
        //    {
        //        int i = (from ix in db.tb_Models
        //                 where ix.ModelName == code

        //                 select ix).Count();
        //        if (i > 0)
        //            ck = false;
        //        else
        //            ck = true;
        //    }

        //    return ck;
        //}

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            return;
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //radGridView1.Rows.AddNew();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //radGridView1.ReadOnly = true;
            //btnView.Enabled = false;
            ////btnEdit.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           // radGridView1.ReadOnly = false;
           //// btnEdit.Enabled = false;
           // btnView.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }
        private decimal Cal_RemainQty(int PRID,int RCID)
        {
            decimal re = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                decimal sum = 0;
                var vv = (from ix in db.tb_Receives
                          where  //ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                            ix.PRID == PRID
                            && ix.ID != RCID  //sum ทั้งหมดที่ไม่ใช่ตัวมัน เพราะจะรบมันทั้ง
                          //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                          select ix).ToList();
                if (vv.Count > 0)
                {
                    foreach (var vvd in vv)
                    {
                        sum += Convert.ToDecimal(vvd.QTY);
                    }
                }
                re = sum;
            }
                    return re;
        }
        private decimal Cal_RemainQty(int PRID)
        {
            decimal re = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                decimal sum = 0;
                var vv = (from ix in db.tb_Receives
                          where  //ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                            ix.PRID == PRID
                           
                          select ix).ToList();
                if (vv.Count > 0)
                {
                    foreach (var vvd in vv)
                    {
                        sum += Convert.ToDecimal(vvd.QTY);
                    }
                }
                re = sum;
            }
            return re;
        }
        private decimal update_RemainQty()
        {
            decimal re = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //decimal OrderQty = 0;
                int PRID = 0;
                string PRNo = "";
                foreach (DataRow dr in dt_PRID.Rows)
                {
                    PRID = Convert.ToInt32(dr["PRID"]);
                    PRNo = Convert.ToString(dr["PRNo"]);
                    //OrderQty = Convert.ToDecimal(dr["OrderQty"]);
                    if (PRID > 0)
                    {
                        db.sp_006_Update_PR_Remain(PRID, PRNo);
                    }


                    var g = (from ix in db.tb_PurchaseRequestLines
                              where  //ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                                ix.id == PRID
                                && ix.PRNo == PRNo
                                && ix.SS != 0
                              select ix).ToList();
                    if(g.Count>0)
                    {
                        foreach( var gg in g)
                        {
                            db.sp_010_Update_StockItem(Convert.ToString(gg.CodeNo), "");
                        }
                    }

                }

                //foreach (DataRow dr in dt_PRID.Rows)
                //{
                //    PRID =  Convert.ToInt32(dr["PRID"]);
                //    PRNo =  Convert.ToString(dr["PRNo"]);
                //    OrderQty = Convert.ToDecimal(dr["OrderQty"]);
                //    if (PRID > 0)
                //    {
                //        var vv = (from ix in db.tb_Receives
                //                  where  //ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                //                    ix.PRID == PRID

                //                  select ix).ToList();
                //        if (vv.Count > 0)
                //        {
                //            foreach (var vvd in vv)
                //            {
                //                vvd.RemainQty = OrderQty-Cal_RemainQty(PRID);
                //                if(vvd.RemainQty >0)
                //                    vvd.Status = "Partial";
                //                else
                //                    vvd.Status = "Completed";

                //                db.SubmitChanges();
                //            }
                //        }
                //    }

                //}

                //var distinctRows = (from DataRow dRow in dt_PRID.Rows
                //                    select dRow["PRNo"]).Distinct();

                //if (distinctRows.Count() > 0)
                //{
                //    foreach (var gg in distinctRows)
                //    {
                //        //update Status pr

                //        var hh = (from ix in db.tb_PurchaseRequestLines
                //                  where ix.PRNo == gg.ToString()
                //                        && ix.RemainQty < ix.OrderQty
                //                  select ix).ToList();
                //        if (hh.Count > 0)
                //        {
                //            var pp = (from ix in db.tb_PurchaseRequests
                //                      where ix.PRNo == gg.ToString()
                //                      select ix).First();
                //            pp.Status = "Completed";
                //        }
                //        else
                //        {
                //            var pp = (from ix in db.tb_PurchaseRequests
                //                      where ix.PRNo == gg.ToString()
                //                      select ix).First();
                //            pp.Status = "Waiting";
                //        }

                //        db.SubmitChanges();
                //    }
                //}
            }
            return re;
        }
        
        private void Save_Return1()
        {
            try
            {

                if (txtInvoiceNo.Text.Equals(""))
                {
                    MessageBox.Show("กรุณาเลือก Invoice No , DL No");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var g = (from ix in db.tb_ReceiveHs
                                 where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                                 //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                 select ix).ToList();
                        if (g.Count > 0)  //มีรายการในระบบ
                        {
                            //Herder
                            string RCNo = StockControl.dbClss.TSt(g.FirstOrDefault().RCNo);
                            var gg = (from ix in db.tb_ReceiveHs
                                      where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                                      //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                      select ix).First();

                            gg.UpdateBy = ClassLib.Classlib.User;
                            gg.UpdateDate = DateTime.Now;
                            gg.Status = "Cancel";


                            //detail
                            var vv = (from ix in db.tb_Receives
                                      where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                                      //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                      select ix).ToList();
                            if (vv.Count > 0)
                            {

                                foreach (var vvd in vv)
                                {

                                    vvd.Status = "Cancel";

                                    int PRID = 0;
                                    string PRNo = "";
                                    PRID = Convert.ToInt32(vvd.PRID);
                                    var pp = (from ix in db.tb_PurchaseRequestLines
                                              where ix.id == PRID
                                              // && ix.TempNo == StockControl.dbClss.TSt(g.Cells["TempNo"].Value)
                                              //&& ix.PRNo == StockControl.dbClss.TSt(g.Cells["PRNo"].Value)
                                              //&& ix.CodeNo == StockControl.dbClss.TSt(g.Cells["CodeNo"].Value)
                                              select ix).ToList();

                                    if (pp.Count > 0)
                                    {
                                        foreach (var ppd in pp)
                                        {
                                            ppd.RemainQty = ppd.OrderQty;
                                            PRNo = vvd.PRNo;
                                            db.SubmitChanges();
                                            dbClss.AddHistory(this.Name + PRNo, "คืนการรับ Receive", " คืนการรับเลขที่ : " + txtInvoiceNo.Text
                                                + " PRID : " + vvd.PRID.ToString() + " CodeNo : " + vvd.CodeNo
                                                + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                                        }
                                    }

                                    db.SubmitChanges();
                                }
                            }

                            dbClss.AddHistory(this.Name , "คืนการรับ Receive", " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtInvoiceNo.Text.Trim());
                            dbClss.AddHistory(this.Name , "คืนการรับ Receive", " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", RCNo);
                            dbClss.AddHistory(this.Name, "คืนการรับ Receive", " คืนการรับเลขที่ : " + txtInvoiceNo.Text + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", "");

                        }
                    }
                    MessageBox.Show("บันทึกสำเร็จ!");
                    txtInvoiceNo.Text = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void Save_Return2()
        {
            try
            {

                if (txtInvoiceNo.Text.Equals(""))
                {
                    MessageBox.Show("กรุณาเลือก Invoice No , DL No");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int seq = 0;
                    string CRNo = StockControl.dbClss.GetNo(8, 2);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var g = (from ix in db.tb_ReceiveHs
                                 where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                                 //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                 select ix).ToList();
                        if (g.Count > 0)  //มีรายการในระบบ
                        {
                            //Herder 
                            string RCNo = StockControl.dbClss.TSt(g.FirstOrDefault().RCNo);

                            //insert tb_ReceiveH เข้า tb_ReceiveH_Del
                            tb_ReceiveH_Del gg = new tb_ReceiveH_Del();
                            gg.RCNo = g.FirstOrDefault().RCNo;
                            gg.RCDate = g.FirstOrDefault().RCDate;
                            gg.UpdateBy = g.FirstOrDefault().UpdateBy;
                            gg.UpdateDate = g.FirstOrDefault().UpdateDate;
                            gg.CreateBy = g.FirstOrDefault().CreateBy;
                            gg.CreateDate = g.FirstOrDefault().CreateDate;
                            gg.VendorName = g.FirstOrDefault().VendorName;
                            gg.VendorNo = g.FirstOrDefault().VendorNo;
                            gg.RemarkHD = g.FirstOrDefault().RemarkHD;
                            gg.Type = g.FirstOrDefault().Type;
                            gg.InvoiceNo = g.FirstOrDefault().InvoiceNo;
                            gg.Barcode = g.FirstOrDefault().Barcode;
                            gg.Status = g.FirstOrDefault().Status;
                            gg.TempNo = g.FirstOrDefault().TempNo;
                            gg.id = g.FirstOrDefault().id;

                            db.tb_ReceiveH_Dels.InsertOnSubmit(gg);
                            db.SubmitChanges();


                            //string invno = "";
                            //string TempNo = "";
                            string Type = "";
                            Type = StockControl.dbClss.TSt(g.FirstOrDefault().Type);
                            //if (StockControl.dbClss.TSt(g.FirstOrDefault().Type).Equals("รับด้วยใบ Invoice"))
                            //{
                            //    invno = StockControl.dbClss.TSt(g.FirstOrDefault().InvoiceNo);
                            //}
                            //else
                            //    TempNo = StockControl.dbClss.TSt(g.FirstOrDefault().TempNo);


                            //detail

                            var vv = (from ix in db.tb_Receives
                                      where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                                      //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                                      select ix).ToList();
                            if (vv.Count > 0)
                            {

                                foreach (var vvd in vv)
                                {
                                    //vvd.Status = "Cancel";
                                    int PRID = 0;
                                    string PRNo = "";
                                    PRID = Convert.ToInt32(vvd.PRID);
                                    var pp = (from ix in db.tb_PurchaseRequestLines
                                              where ix.id == PRID
                                                    && ix.SS != 0
                                              select ix).ToList();

                                    if (pp.Count > 0)
                                    {
                                        dt_PRID.Rows.Add(Convert.ToInt32(vvd.PRID), Convert.ToString(vvd.PRNo)
                                            , Convert.ToDecimal(pp.FirstOrDefault().OrderQty)
                                            );

                                        foreach (var ppd in pp)
                                        {
                                            //ppd.RemainQty = ppd.OrderQty - Cal_RemainQty(PRID,Convert.ToInt32(vvd.ID));//ppd.OrderQty;

                                            //PRNo = vvd.PRNo;
                                            //db.SubmitChanges();

                                            dbClss.AddHistory(this.Name , "คืนการรับ Receive", " คืนการรับเลขที่ : " + txtInvoiceNo.Text
                                                + " PRID : " + vvd.PRID.ToString() + " CodeNo : " + vvd.CodeNo
                                                + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", PRNo);
                                        }
                                    }
                                    
                                    //insert tb_Receives เข้า tb_receive_Del
                                    tb_Receive_Del u = new tb_Receive_Del();
                                    u.PRNo = StockControl.dbClss.TSt(vvd.PRNo);
                                    u.TempNo = StockControl.dbClss.TSt(vvd.TempNo);
                                    u.CodeNo = StockControl.dbClss.TSt(vvd.CodeNo);
                                    u.ItemNo = StockControl.dbClss.TSt(vvd.ItemNo);
                                    u.ItemDescription = StockControl.dbClss.TSt(vvd.ItemDescription);
                                    u.RemainQty = vvd.RemainQty;

                                    u.QTY = vvd.QTY;
                                    u.PCSUnit = vvd.PCSUnit;
                                    u.Unit = vvd.Unit;
                                    u.CostPerUnit = vvd.CostPerUnit;
                                    u.Amount = vvd.Amount;
                                    u.Remark = vvd.Remark;
                                    u.LotNo = vvd.LotNo;
                                    u.SerialNo = vvd.SerialNo;
                                    u.CRRNCY = vvd.CRRNCY;
                                    u.RCNo = vvd.RCNo;
                                    u.InvoiceNo = vvd.InvoiceNo;
                                    u.PRID = vvd.PRID;
                                    u.ShelfNo = vvd.ShelfNo;
                                    u.TempInvNo = vvd.TempInvNo;
                                    u.RCDate = vvd.RCDate;
                                    u.Seq = vvd.Seq;
                                    u.Status = vvd.Status;
                                    u.ClearFlag = vvd.ClearFlag;
                                    u.ClearDate = vvd.ClearDate;
                                    u.CreateDate = vvd.CreateDate;
                                    u.CreateBy = vvd.CreateBy;
                                    u.UpdateDate = vvd.UpdateDate;
                                    u.UpdateBy = vvd.UpdateBy;
                                    u.Calbit = vvd.Calbit;
                                    u.TempQty = vvd.TempQty;
                                    u.TempRemain = vvd.TempRemain;
                                    u.TempShip = vvd.TempShip;

                                    db.tb_Receive_Dels.InsertOnSubmit(u);
                                    
                                    seq += 1;

                                    //// Insert Stock1
                                    //Insert_Stock(seq, Convert.ToInt32(vvd.ID), vvd.RCNo, CNNo);

                                    //New Stock
                                    InsertStock_new(seq, Convert.ToInt32(vvd.ID), vvd.RCNo, CRNo, vvd.InvoiceNo, Type);
                                    
                                }


                                //delete tb_receive
                                db.tb_Receives.DeleteAllOnSubmit(vv);
                                db.SubmitChanges();
                            }

                            db.tb_ReceiveHs.DeleteAllOnSubmit(g);
                            db.SubmitChanges();

                            //update remain tb_receive ที่เหลือ
                            if (dt_PRID.Rows.Count > 0)
                            {
                                update_RemainQty();
                            }



                            dbClss.AddHistory(this.Name, "คืนการรับ Receive", " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtInvoiceNo.Text.Trim());
                            dbClss.AddHistory(this.Name, "คืนการรับ Receive", " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", RCNo);
                            dbClss.AddHistory(this.Name, "คืนการรับ Receive", " คืนการรับเลขที่ : " + txtInvoiceNo.Text + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", txtInvoiceNo.Text);

                        }
                    }
                    MessageBox.Show("บันทึกสำเร็จ!");
                    txtInvoiceNo.Text = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save_Return1();//วิธีที่ 1 เป็นการปรับแต่สถานะ ซึ่งทำให้ผิดเพราะ database ไม่ได้ออกแบบมาแบบนี้

            if(Check_QTY())
                Save_Return2(); //วิธีที่ 2 การปรับแบบการ Insert เข้า table delete
            

        }

        private void Insert_Stock(int seq, int id, string RCNo,string CNNo)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_Receives
                             //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                         where ix.RCNo.Trim() == RCNo.Trim() && ix.Status != "Cancel"
                         && ix.ID == id
                         select ix).First();
                
                //insert Stock
                DateTime? CalDate = null;
                DateTime? AppDate = DateTime.Now;
                int Seq = seq;

                //tb_Stock1 gg = new tb_Stock1();
                //gg.AppDate = AppDate;
                //gg.Seq = Seq;
                //gg.App = "Cancel RC";
                //gg.Appid = Seq;
                //gg.CreateBy = ClassLib.Classlib.User;
                //gg.CreateDate = DateTime.Now;
                //gg.DocNo = CNNo;
                //gg.RefNo = RCNo;
                //gg.Type = "Inv/DL";
                //gg.QTY = -Convert.ToDecimal(g.QTY);
                //gg.Inbound = 0;
                //gg.Outbound = -Convert.ToDecimal(g.QTY);
                //gg.AmountCost = -Convert.ToDecimal(g.QTY) * Convert.ToDecimal(g.CostPerUnit);
                //gg.UnitCost = Convert.ToDecimal(g.CostPerUnit);
                //gg.RemainQty = 0;
                //gg.RemainUnitCost = 0;
                //gg.RemainAmount = 0;
                //gg.CalDate = CalDate;
                //gg.Status = "Active";

                //db.tb_Stock1s.InsertOnSubmit(gg);
                //db.SubmitChanges();

                //udpate Stock Item StockInv,StockDL
                //dbClss.Insert_Stock(g.CodeNo, (-Convert.ToDecimal(g.QTY)), "CNRC", "Inv");

                //update stock StockBackOrder item
                dbClss.Insert_StockTemp(g.CodeNo, Convert.ToDecimal(g.QTY), "CNRC_Temp", "Inv");
            }
        }
        private bool Check_QTY()
        {
            bool re = true;
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int ID = 0;
                    decimal Qty = 0;
                    decimal Qty_Cancel = 0;

                    var g = (from ix in db.tb_ReceiveHs
                             where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                             //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                             select ix).ToList();
                    if (g.Count > 0)  //มีรายการในระบบ
                    {
                        //Herder 
                        string RCNo = StockControl.dbClss.TSt(g.FirstOrDefault().RCNo);

                        //detail
                        string invno = "";
                        string TempNo = "";
                        if(StockControl.dbClss.TSt(g.FirstOrDefault().Type).Equals("รับด้วยใบ Invoice"))
                            invno = StockControl.dbClss.TSt(g.FirstOrDefault().InvoiceNo);
                        else
                            TempNo = StockControl.dbClss.TSt(g.FirstOrDefault().TempNo);


                        var vv = (from ix in db.tb_Receives
                                  where ix.Status != "Cancel" 
                                  && (((ix.InvoiceNo).Trim() == invno && invno != "")
                                  || ((ix.TempInvNo).Trim() == TempNo && TempNo != ""))
                                  select ix).ToList();
                        if (vv.Count > 0)
                        {

                            foreach (var gg in vv)
                            {
                                Qty = 0;
                                ID = gg.ID;
                                Qty_Cancel = Convert.ToDecimal(gg.QTY);
                                if (ID > 0)
                                {
                                    Qty = (Convert.ToDecimal(db.Cal_QTY(gg.CodeNo, "", 0)));

                                    if (Qty < Qty_Cancel)
                                    {
                                        MessageBox.Show("จำนวนสินค้าบางรายการไม่พอในการยกเลิกการรับสินค้า");
                                        re = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบ Invoice No , DL No");
                            re = false;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return re;
        }
        private void InsertStock_new(int seq, int id, string RCNo, string CRNo,string inv,string Type)
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DateTime? CalDate = null;
                    DateTime? AppDate = DateTime.Now;
                    int Seq = 0;
                    //string Category = "";
                    decimal Qty_Inv = 0;
                    decimal Qty_DL = 0;
                    int ID = 0;
                    decimal Qty_Remain = 0;
                    decimal Qty_Cancel = 0;

                    string Type_in_out = "Out";
                    decimal RemainQty = 0;
                    decimal Amount = 0;
                    decimal RemainAmount = 0;
                    decimal Avg = 0;
                    decimal UnitCost = 0;
                    decimal sum_Remain = 0;
                    decimal sum_Qty = 0;

                    var g = (from ix in db.tb_Receives
                                 //join i in db.tb_Items on ix.CodeNo equals i.CodeNo
                             where ix.Status != "Cancel"
                                  && ((ix.InvoiceNo).Trim() == inv && inv != "")
                                  && ix.ID == id
                                  //&& (StockControl.dbClss.TSt(ix.TempInvNo).Trim() == TempNo && TempNo != "")

                             select ix).ToList();
                    if (g.Count > 0)
                    {
                        //insert Stock
                        foreach (var vv in g)
                        {
                            Seq += 1;
                            Qty_Remain = 0;
                            ID = vv.ID;
                            Qty_Cancel = Convert.ToDecimal(vv.QTY);
                            if (ID>0)
                            {
                                Qty_Remain = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));  //sum ทั้งหมด
                                Qty_Inv = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "Invoice", 0))); //sum เฉพาะ Invoice
                                Qty_DL = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "Temp", 0))); // sum เฉพาะ DL
                                
                                if (Qty_Cancel <= Qty_Remain)
                                {
                                    if (Type.Equals("รับด้วยใบ Invoice"))
                                    {
                                        if(Qty_Inv >= Qty_Cancel)//Cancel Invoice ก่อน
                                        {

                                            UnitCost = Convert.ToDecimal(vv.CostPerUnit);// Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                            Amount = (-Qty_Cancel) * UnitCost;

                                            //แบบที่ 1 จะไป sum ใหม่
                                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                            //แบบที่ 2 จะไปดึงล่าสุดมา
                                            //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                            sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                                + Amount;

                                            sum_Qty = RemainQty + (-Qty_Cancel);
                                            Avg = UnitCost;//sum_Remain / sum_Qty;
                                            RemainAmount = sum_Remain;


                                            tb_Stock gg = new tb_Stock();
                                            gg.AppDate = AppDate;
                                            gg.Seq = Seq;
                                            gg.App = "Cancel RC";
                                            gg.Appid = Seq;
                                            gg.CreateBy = ClassLib.Classlib.User;
                                            gg.CreateDate = DateTime.Now;
                                            gg.DocNo = CRNo;
                                            gg.RefNo = RCNo;
                                            gg.CodeNo = vv.CodeNo;
                                            gg.Type = Type;
                                            gg.QTY = -Qty_Cancel;
                                            gg.Inbound = 0;
                                            gg.Outbound = -Qty_Cancel;
                                            gg.Type_i = 2;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                            gg.Category = "Invoice";
                                            gg.Refid = ID;                                           
                                            gg.CalDate = CalDate;
                                            gg.Status = "Active";
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

                                            dbClss.AddHistory(this.Name, "คืนการรับสินค้า", " คืนการรับเลขที่ : " + inv + " ประเภท : "+ Type +" CodeNo : " + vv.CodeNo + " จำนวน : " +(-Qty_Cancel).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", inv);

                                        }
                                        else  // ต้องตัดสต็อก 2 ที่ ทั้ง invoice, Temp
                                        {
                                            //ตัด Invoice จริงก่อน

                                            decimal Qty_temp = 0;
                                            Qty_temp = Qty_Cancel - Qty_Inv;

                                            UnitCost = Convert.ToDecimal(vv.CostPerUnit);// Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                            Amount = (-Qty_Cancel) * UnitCost;

                                            //แบบที่ 1 จะไป sum ใหม่
                                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                            //แบบที่ 2 จะไปดึงล่าสุดมา
                                            //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                            sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                                + Amount;

                                            sum_Qty = RemainQty + (-Qty_Cancel);
                                            Avg = UnitCost;//sum_Remain / sum_Qty;
                                            RemainAmount = sum_Remain;

                                            tb_Stock gg = new tb_Stock();
                                            gg.AppDate = AppDate;
                                            gg.Seq = Seq;
                                            gg.App = "Cancel RC";
                                            gg.Appid = Seq;
                                            gg.CreateBy = ClassLib.Classlib.User;
                                            gg.CreateDate = DateTime.Now;
                                            gg.DocNo = CRNo;
                                            gg.RefNo = RCNo;
                                            gg.CodeNo = vv.CodeNo;
                                            gg.Type = Type;
                                            gg.QTY = -Qty_Cancel;
                                            gg.Inbound = 0;
                                            gg.Outbound = -Qty_Cancel;
                                            gg.Type_i = 2;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                            gg.Category = "Invoice";
                                            gg.Refid = ID;                                           
                                            gg.CalDate = CalDate;
                                            gg.Status = "Active";
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

                                            dbClss.AddHistory(this.Name, "คืนการรับสินค้า", " คืนการรับเลขที่ : " + inv+ " ประเภท : " + Type+ " CodeNo : " + vv.CodeNo + " จำนวน : " + (-Qty_Cancel).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", inv);


                                            //Temp

                                            UnitCost = Convert.ToDecimal(vv.CostPerUnit);// Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                            Amount = (-Qty_temp) * UnitCost;

                                            //แบบที่ 1 จะไป sum ใหม่
                                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                            //แบบที่ 2 จะไปดึงล่าสุดมา
                                            //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                            sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                                + Amount;

                                            sum_Qty = RemainQty + (-Qty_temp);
                                            Avg = UnitCost;//sum_Remain / sum_Qty;
                                            RemainAmount = sum_Remain;

                                            tb_Stock tt = new tb_Stock();
                                            tt.AppDate = AppDate;
                                            tt.Seq = Seq;
                                            tt.App = "Cancel RC";
                                            tt.Appid = Seq;
                                            tt.CreateBy = ClassLib.Classlib.User;
                                            tt.CreateDate = DateTime.Now;
                                            tt.DocNo = CRNo;
                                            tt.RefNo = RCNo;
                                            tt.CodeNo = vv.CodeNo;
                                            tt.Type = "ใบส่งของชั่วคราว";
                                            tt.QTY = -Qty_temp;
                                            tt.Inbound = 0;
                                            tt.Outbound = -Qty_temp;
                                            tt.Type_i = 2;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                            tt.Category = "Temp";
                                            tt.Refid = ID;                                            
                                            tt.CalDate = CalDate;
                                            tt.Status = "Active";
                                            tt.Flag_ClearTemp = 1;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                            tt.Type_in_out = Type_in_out;
                                            tt.AmountCost = Amount;
                                            tt.UnitCost = UnitCost;
                                            tt.RemainQty = sum_Qty;
                                            tt.RemainUnitCost = 0;
                                            tt.RemainAmount = RemainAmount;
                                            tt.Avg = Avg;

                                            db.tb_Stocks.InsertOnSubmit(tt);
                                            db.SubmitChanges();
                                            dbClss.AddHistory(this.Name, "คืนการรับสินค้า", " คืนการรับเลขที่ : " + inv + " ประเภท : " + "ใบส่งของชั่วคราว" + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-Qty_temp).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", inv);

                                        }

                                    }
                                    else
                                    {
                                        if (Qty_DL >= Qty_Cancel)//Cancel Temp ก่อน
                                        {
                                            //Temp

                                            UnitCost = Convert.ToDecimal(vv.CostPerUnit);// Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                            Amount = (-Qty_Cancel) * UnitCost;

                                            //แบบที่ 1 จะไป sum ใหม่
                                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                            //แบบที่ 2 จะไปดึงล่าสุดมา
                                            //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                            sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                                + Amount;

                                            sum_Qty = RemainQty + (-Qty_Cancel);
                                            Avg = UnitCost;//sum_Remain / sum_Qty;
                                            RemainAmount = sum_Remain;

                                            tb_Stock aa = new tb_Stock();
                                            aa.AppDate = AppDate;
                                            aa.Seq = Seq;
                                            aa.App = "Cancel RC";
                                            aa.Appid = Seq;
                                            aa.CreateBy = ClassLib.Classlib.User;
                                            aa.CreateDate = DateTime.Now;
                                            aa.DocNo = CRNo;
                                            aa.RefNo = RCNo;
                                            aa.CodeNo = vv.CodeNo;
                                            aa.Type = "ใบส่งของชั่วคราว";
                                            aa.QTY = -Qty_Cancel;
                                            aa.Inbound = 0;
                                            aa.Outbound = -Qty_Cancel;
                                            aa.Type_i = 2;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                            aa.Category = "Temp";
                                            aa.Refid = ID;                                           
                                            aa.CalDate = CalDate;
                                            aa.Status = "Active";
                                            aa.Flag_ClearTemp = 1;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                            aa.Type_in_out = Type_in_out;
                                            aa.AmountCost = Amount;
                                            aa.UnitCost = UnitCost;
                                            aa.RemainQty = sum_Qty;
                                            aa.RemainUnitCost = 0;
                                            aa.RemainAmount = RemainAmount;
                                            aa.Avg = Avg;

                                            db.tb_Stocks.InsertOnSubmit(aa);
                                            db.SubmitChanges();
                                            dbClss.AddHistory(this.Name, "คืนการรับสินค้า", " คืนการรับเลขที่ : " + inv + " ประเภท : " + "ใบส่งของชั่วคราว" + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-Qty_Cancel).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", inv);

                                        }
                                        else // ต้องตัดสต็อก 2 ที่ ทั้ง invoice, Temp
                                        {

                                            decimal Qty_temp = 0;
                                            Qty_temp = Qty_Cancel - Qty_DL;

                                            UnitCost = Convert.ToDecimal(vv.CostPerUnit);// Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                            Amount = (-Qty_Cancel) * UnitCost;

                                            //แบบที่ 1 จะไป sum ใหม่
                                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                            //แบบที่ 2 จะไปดึงล่าสุดมา
                                            //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                            sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                                + Amount;

                                            sum_Qty = RemainQty + (-Qty_Cancel);
                                            Avg = UnitCost;//sum_Remain / sum_Qty;
                                            RemainAmount = sum_Remain;

                                            //Temp
                                            tb_Stock aa = new tb_Stock();
                                            aa.AppDate = AppDate;
                                            aa.Seq = Seq;
                                            aa.App = "Cancel RC";
                                            aa.Appid = Seq;
                                            aa.CreateBy = ClassLib.Classlib.User;
                                            aa.CreateDate = DateTime.Now;
                                            aa.DocNo = CRNo;
                                            aa.RefNo = RCNo;
                                            aa.CodeNo = vv.CodeNo;
                                            aa.Type = "ใบส่งของชั่วคราว";
                                            aa.QTY = -Qty_Cancel;
                                            aa.Inbound = 0;
                                            aa.Outbound = -Qty_Cancel;
                                            aa.Type_i = 2;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                            aa.Category = "Temp";
                                            aa.Refid = ID;                                          
                                            aa.CalDate = CalDate;
                                            aa.Status = "Active";
                                            aa.Flag_ClearTemp = 0;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                            aa.Type_in_out = Type_in_out;
                                            aa.AmountCost = Amount;
                                            aa.UnitCost = UnitCost;
                                            aa.RemainQty = sum_Qty;
                                            aa.RemainUnitCost = 0;
                                            aa.RemainAmount = RemainAmount;
                                            aa.Avg = Avg;

                                            db.tb_Stocks.InsertOnSubmit(aa);
                                            db.SubmitChanges();
                                            dbClss.AddHistory(this.Name, "คืนการรับสินค้า", " คืนการรับเลขที่ : " + inv + " ประเภท : " + "ใบส่งของชั่วคราว" + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-Qty_Cancel).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", inv);

                                            //Invoice   

                                            UnitCost = Convert.ToDecimal(vv.CostPerUnit);// Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "Avg"));
                                            Amount = (-Qty_temp) * UnitCost;

                                            //แบบที่ 1 จะไป sum ใหม่
                                            RemainQty = (Convert.ToDecimal(db.Cal_QTY(vv.CodeNo, "", 0)));
                                            //แบบที่ 2 จะไปดึงล่าสุดมา
                                            //RemainQty = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainQty"));
                                            sum_Remain = Convert.ToDecimal(dbClss.Get_Stock(vv.CodeNo, "", "", "RemainAmount"))
                                                + Amount;

                                            sum_Qty = RemainQty + (-Qty_temp);
                                            Avg = UnitCost;//sum_Remain / sum_Qty;
                                            RemainAmount = sum_Remain;

                                            tb_Stock bb = new tb_Stock();
                                            bb.AppDate = AppDate;
                                            bb.Seq = Seq;
                                            bb.App = "Cancel RC";
                                            bb.Appid = Seq;
                                            bb.CreateBy = ClassLib.Classlib.User;
                                            bb.CreateDate = DateTime.Now;
                                            bb.DocNo = CRNo;
                                            bb.RefNo = RCNo;
                                            bb.CodeNo = vv.CodeNo;
                                            bb.Type = "Invoice";
                                            bb.QTY = -Qty_temp;
                                            bb.Inbound = 0;
                                            bb.Outbound = -Qty_temp;
                                            bb.Type_i = 2;  //Receive = 1,Cancel Receive 2,Shipping = 3,Cancel Shipping = 4,Adjust stock = 5,ClearTemp = 6
                                            bb.Category = "Invoice";
                                            bb.Refid = ID;
                                            bb.UnitCost = vv.CostPerUnit;
                                            bb.AmountCost = vv.CostPerUnit * -Qty_temp;
                                            bb.RemainQty = 0;
                                            bb.RemainUnitCost = 0;
                                            bb.RemainAmount = 0;
                                            bb.CalDate = CalDate;
                                            bb.Status = "Active";
                                            bb.Flag_ClearTemp = 0;   //0 คือ invoice,1 คือ Temp , 2 คือ clear temp แล้ว
                                            bb.Type_in_out = Type_in_out;
                                            bb.AmountCost = Amount;
                                            bb.UnitCost = UnitCost;
                                            bb.RemainQty = sum_Qty;
                                            bb.RemainUnitCost = 0;
                                            bb.RemainAmount = RemainAmount;
                                            bb.Avg = Avg;

                                            db.tb_Stocks.InsertOnSubmit(bb);
                                            db.SubmitChanges();
                                            dbClss.AddHistory(this.Name, "คืนการรับสินค้า", " คืนการรับเลขที่ : " + inv + " ประเภท : " + "ใบส่งของชั่วคราว" + " CodeNo : " + vv.CodeNo + " จำนวน : " + (-Qty_temp).ToString() + " โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", inv);
                                            
                                        }

                                     
                                    }
                                    
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
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxBuaaons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
                    
            //    }
            //}
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
            try
            {
               

                this.Cursor = Cursors.WaitCursor;
                ReturnReceiveList sc = new ReturnReceiveList(txtInvoiceNo,"ReturnReceive");
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
            //if (crow == 0)
            //    DataLoad();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnListItem_Click(object sender, EventArgs e)
        {
            ReturnReceiveList_Del a = new ReturnReceiveList_Del();
            a.ShowDialog();
        }
    }
}
