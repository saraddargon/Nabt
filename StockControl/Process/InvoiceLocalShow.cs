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
    public partial class InvoiceLocalShow : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public InvoiceLocalShow(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public InvoiceLocalShow()
        {
            InitializeComponent();
      
        }
        public InvoiceLocalShow(string InvNox)
        {
            InitializeComponent();
            InvNo = InvNox;
            //Type = Typex;
            //txtInvNo.Text = InvNox;
            //txtCustomerNo.Text = CustNo;
            //txtCustomer.Text = CustName;
            //dtDate1.Value = InvoiceDate;
        }
        string InvNo = "";
        string Type = "";
       

        //private int RowView = 50;
        //private int ColView = 10;
        DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            //dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
            //dt.Columns.Add(new DataColumn("CustomerNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
            //dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {         
            DataLoad();  
        }
        private void DataLoad()
        {
            if(!InvNo.Equals(""))
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    
                    tb_InvoiceLocalHD hd = db.tb_InvoiceLocalHDs.Where(h => h.InvoiceNo.Equals(InvNo)).FirstOrDefault();
                    if (hd != null)
                    {
                        /////////// Header //////////////
                        txtInvNo.Text = hd.InvoiceNo;
                        dtDate1.Value = Convert.ToDateTime(hd.InvoiceDate);
                        txtCustomerNo.Text = hd.CustomerNo;
                        txtCustomer.Text = hd.CustomerName;
                        txtTAXID.Text = hd.CustomerRegisterVat;
                        txtCredit.Text = hd.Credit;
                    
                        txtRefNo.Text = hd.RefNo;
                        txtRemark.Text = hd.Remark1;
                        txtBRANCH.Text = hd.Branch;
                        txtThaiBath.Text = hd.BathText;
                        txtAddress.Text = hd.Address;
                        txtAddress2.Text = hd.Address2;

                        if (hd.TypeVat.Equals("B"))
                            chkNoVat.Checked = true;

                        /////////// End Header///////////

                        /////////// Line ////////////////
                        radGridView1.DataSource = null;
                        radGridView1.DataSource = db.sp_043_Inv_LocalDT_Select(InvNo).ToList();
                        UpdateCost();
                        //int count1 = 0;
                        //decimal Total = 0;
                        //decimal Vat = 0;

                        //foreach (GridViewRowInfo rd in radGridView1.Rows)
                        //{
                        //    count1 += 1;
                        //    rd.Cells["No"].Value = count1;
                        //    Total += Convert.ToDecimal(rd.Cells["Amount"].Value);
                        //    Vat += Convert.ToDecimal(rd.Cells["Vat"].Value);
                        //}
                        //txtTotal.Text = Total.ToString("###,###,##0.00");
                        //txtVat.Text = Vat.ToString("###,###,##0.00");
                        //txtAmount.Text = (Total + Vat).ToString("###,###,##0.00");
                        //txtDiscount.Text = "0.00";
                        //txtAfterDiscount.Text = "0.00";
                        //txtThaiBath.Text = dbClss.ThaiBaht(txtAmount.Text);
                        /////////// end //////////////////
                    }
                }
            }

        }

        private void btn_PrintPR_Click(object sender, EventArgs e)
        {
            //Create Inv.
            try
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string LastDate = dtDate1.Value.ToString("yyyy-MM-dd");
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_InvoiceLocalHD hd = db.tb_InvoiceLocalHDs.Where(h => h.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                        if (hd != null)
                        {
                            decimal Total = 0;
                            decimal Vat = 0;
                            decimal TotalAmount = 0;
                            decimal.TryParse(txtTotal.Text, out Total);
                            decimal.TryParse(txtVat.Text, out Vat);
                            decimal.TryParse(txtAmount.Text, out TotalAmount);
                            //Insert Hd//
                         
                           // hd.InvoiceNo = txtInvNo.Text;
                           // hd.InvoiceDate = dtDate1.Value;
                            hd.CustomerNo = txtCustomerNo.Text;
                            hd.CustomerName = txtCustomer.Text;
                            hd.Address = txtAddress.Text;
                            hd.Address2 = txtAddress2.Text;
                            hd.Branch = txtBRANCH.Text;
                            hd.CustomerRegisterVat = txtTAXID.Text;
                            hd.Credit = txtCredit.Text;
                            hd.RefNo = txtRefNo.Text;
                            hd.Remark1 = txtRemark.Text;
                            //hd.Status = "Process";
                            hd.Total = Total;
                            hd.Vat = Vat;
                            hd.TotalAmount = TotalAmount;
                            hd.BathText = txtThaiBath.Text;
                            hd.InvoiceDate = dtDate1.Value;
                            byte[] barcode = dbClss.SaveQRCode2D(txtInvNo.Text);
                            hd.BarCode = barcode;
                            //hd.CreateBy = dbClss.UserID;
                            //  hd.CreateDate = DateTime.Now;
                            //  db.tb_InvoiceLocalHDs.InsertOnSubmit(hd);
                            db.SubmitChanges();

                            //insert Line///
                            //int AC = 0;
                            //if (rdoG1.IsChecked)
                            //    AC = 0;
                            //if (rdoG2.IsChecked)
                            //    AC = 1;
                            //if (rdoG3.IsChecked)
                            //    AC = 2;
                            //if (rdoG4.IsChecked)
                            //    AC = 3;
                            //if (rdoG5.IsChecked)
                            //    AC = 4;

                            //int CountAAA = 0;
                            //var ListInsert = db.sp_043_Inv_LocalLine_Insert(txtInvNo.Text, AC).ToList();
                            //foreach (var rd in ListInsert)
                            //{
                            //    CountAAA += 1;
                            //    tb_InvoiceLocalDT dt = new tb_InvoiceLocalDT();
                            //    dt.InvoiceNo = txtInvNo.Text;
                            //    dt.OrderNo = rd.OrderNo;
                            //    dt.Plant = rd.Plant;
                            //    dt.PartNo = rd.CodeNo;
                            //    dt.PartName = rd.CodeName;
                            //    dt.PastCustomer = rd.CodeCustomer;
                            //    dt.Qty = Convert.ToDecimal(rd.Qty);
                            //    dt.UnitPrice = Convert.ToDecimal(rd.UnitCost);
                            //    dt.Amount = Convert.ToDecimal(rd.Amount);
                            //    dt.Vat = Convert.ToDecimal(rd.Vat);
                            //    dt.SS = 1;
                            //    dt.SortQ = CountAAA;
                            //    dt.Discount = 0;
                            //    dt.Unit = "PCS";
                            //    db.tb_InvoiceLocalDTs.InsertOnSubmit(dt);
                            //    db.SubmitChanges();

                            //}
                            ////Update tb_LocalDelivery01//
                            //foreach (GridViewRowInfo rs in radGridView1.Rows)
                            //{
                            //    db.sp_043_Inv_LocalTemp_SelectUpdate(txtInvNo.Text, rs.Cells["OrderNo"].Value.ToString(), rs.Cells["Plant"].Value.ToString(), rs.Cells["CodeNo"].Value.ToString(), txtCustomerNo.Text);
                            //}

                            MessageBox.Show("บันทึกสำเร็จ!!");
                           
                            // btnDelete.Enabled = false;
                            // btn_CreateInv.Enabled = false;


                        }
                       

                    }
                    DataLoad();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete Inv. Item
            if(rows>=0)
            {
                if (MessageBox.Show("ต้องการลบหรือไม่ (รายการเดียว) ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int id = 0;
                        int.TryParse(radGridView1.Rows[rows].Cells["id"].Value.ToString(), out id);
                        if (id > 0)
                        {
                            tb_InvoiceLocalDT dl = db.tb_InvoiceLocalDTs.Where(l => l.id == id).FirstOrDefault();
                            if (dl != null)
                            {
                                //dl.SS = 0;
                                db.tb_InvoiceLocalDTs.DeleteOnSubmit(dl);
                                db.SubmitChanges();
                                db.sp_043_Inv_LocalDT_DeleteUpdate(txtInvNo.Text, dl.OrderNo, dl.Plant, dl.PartNo, txtCustomerNo.Text);
                            }
                        }
                    }
                    MessageBox.Show("หลังจากลบรายการแล้ว ให้ทำการ กด Save. อีกครั้งด้วย");
                    DataLoad();
                }
               
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Print Inv.
            try
            {
               
                Report.Reportx1.WReport = "InvoiceLocal";
                Report.Reportx1.Value = new string[2];
                Report.Reportx1.Value[0] = txtInvNo.Text;
                // Report.Reportx1.Value[1] = dbClss.UserID;                 
                Report.Reportx1 op = new Report.Reportx1("InvoiceDot.rpt");
                op.Show();

            }
            catch { }
        }
        int rows = 0;
        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            rows = e.RowIndex;
        }

        private void btnCalBath_Click(object sender, EventArgs e)
        {
            txtThaiBath.Text = dbClss.ThaiBaht(txtAmount.Text);
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            UpdateCost();

        }
        private void UpdateCost()
        {
            try
            {
                int count1 = 0;
                decimal Total = 0;
                decimal Vat = 0;

                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    count1 += 1;
                    rd.Cells["No"].Value = count1;
                    Total += Convert.ToDecimal(rd.Cells["Amount"].Value);
                    Vat += Convert.ToDecimal(rd.Cells["Vat"].Value);

                }

                if (chkNoVat.Checked)
                {
                    Vat = 0;
                    txtVat.Text = "0";
                }

                txtTotal.Text = Total.ToString("###,###,##0.00");
                txtVat.Text = Vat.ToString("###,###,##0.00");
                txtAmount.Text = (Total + Vat).ToString("###,###,##0.00");
                txtDiscount.Text = "0.00";
                txtAfterDiscount.Text = "0.00";
                txtThaiBath.Text = dbClss.ThaiBaht(txtAmount.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            // txtInvNo.Text = GetInvoiceNo(txtCustomerNo.Text, dtDate1.Value);
            //Switp invoice
            //Microsoft.VisualBasic.Interaction.InputBox("");
            if (MessageBox.Show("ต้องการสลับเลข Invoice?","Change Invoice",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string Data1 = Microsoft.VisualBasic.Interaction.InputBox("Invoice No A?", "Invoice A", "");
                string Data2 = Microsoft.VisualBasic.Interaction.InputBox("Invoice No B?", "Invoice B", "");
                if (Data1 != "" && Data2 != "")
                {
                    try
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_52_ChangeInvoice(Data1, Data2);
                        }
                            MessageBox.Show("เรียบร้อย โปรดปิดหน้าต่างเพิ่มเริ่มใหม่!");
                        InvNo = Data2;
                        DataLoad();
                    }
                    catch { }
                }
            }
        }
        
        private string GetInvoiceNo(string CustNo, DateTime ShipDate)
        {
            string InvNo = "";
            string LastDate = ShipDate.ToString("yyyy-MM-dd");

            int RUNNING = 0;
            //A=19092901
            //B=FZ19092901
            bool Vat = false;
            bool CKInv = true;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   
                    var CustomterA = db.sp_043_Inv_LocalCust_Dynamics(CustNo).FirstOrDefault();

                    if (!CustomterA.PType.Equals("EXP"))
                    {
                        Vat = true;
                    }
                    if (Vat)
                    {
                        //UpdateBefore//                     


                        while (CKInv)
                        {
                            Type = "A";
                            tb_InvoiceNoSery Ns = db.tb_InvoiceNoSeries.Where(w => w.LastDate.Equals(LastDate) && w.VatType.Equals("A")).FirstOrDefault();
                            if (Ns != null)
                            {
                                RUNNING = Convert.ToInt32(Ns.LastRunning);
                            }
                            else
                            {

                                tb_InvoiceNoSery NAd = new tb_InvoiceNoSery();
                                NAd.VatType = "A";
                                NAd.LastRunning = 0;
                                NAd.LastDate = LastDate;
                                NAd.LastNo = "";
                                db.tb_InvoiceNoSeries.InsertOnSubmit(NAd);
                                db.SubmitChanges();
                                RUNNING = 0;

                            }

                            InvNo = ShipDate.ToString("yyMMdd") + (RUNNING + 1).ToString("00");
                            CKInv = CheckUpdateInvNo(InvNo);
                            if (CKInv)
                            {
                                tb_InvoiceNoSery Ns2 = db.tb_InvoiceNoSeries.Where(w => w.LastDate.Equals(LastDate) && w.VatType.Equals("A")).FirstOrDefault();
                                if (Ns2 != null)
                                {
                                    Ns2.LastRunning = Ns2.LastRunning + 1;
                                    db.SubmitChanges();
                                }
                            }
                        }

                    }
                    else
                    {
                        while (CKInv)
                        {
                            Type = "B";
                            tb_InvoiceNoSery Ns = db.tb_InvoiceNoSeries.Where(w => w.LastDate.Equals(LastDate) && w.VatType.Equals("B")).FirstOrDefault();
                            if (Ns != null)
                            {
                                RUNNING = Convert.ToInt32(Ns.LastRunning);
                            }
                            else
                            {
                                tb_InvoiceNoSery NAd = new tb_InvoiceNoSery();
                                NAd.VatType = "B";
                                NAd.LastRunning = 0;
                                NAd.LastDate = LastDate;
                                NAd.LastNo = "";
                                db.tb_InvoiceNoSeries.InsertOnSubmit(NAd);
                                db.SubmitChanges();
                                RUNNING = 0;
                            }
                            InvNo = "FZ" + ShipDate.ToString("yyMMdd") + (RUNNING + 1).ToString("00");

                            CKInv = CheckUpdateInvNo(InvNo);
                            if (CKInv)
                            {
                                tb_InvoiceNoSery Ns2 = db.tb_InvoiceNoSeries.Where(w => w.LastDate.Equals(LastDate) && w.VatType.Equals("A")).FirstOrDefault();
                                if (Ns2 != null)
                                {
                                    Ns2.LastRunning = Ns2.LastRunning + 1;
                                    db.SubmitChanges();
                                }
                            }

                        }
                    }

                }

            }
            catch { }
            return InvNo;
        }
        private bool CheckUpdateInvNo(string INVNo)
        {
            bool ck = false;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {

                tb_InvoiceLocalHD ckh = db.tb_InvoiceLocalHDs.Where(h => h.InvoiceNo.Equals(INVNo)).FirstOrDefault();
                if (ckh != null)
                {
                    ck = true;
                }
            }
            return ck;
        }

        private void radGridView1_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                if (e.ColumnIndex == radGridView1.Columns["UnitCost"].Index)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        decimal UnitCost = 0;
                        UnitCost = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["UnitCost"].Value);
                        int id = 0;
                        int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                        if (id > 0)
                        {
                            tb_InvoiceLocalDT ed = db.tb_InvoiceLocalDTs.Where(es => es.id == id).FirstOrDefault();
                            if (ed != null)
                            {
                                decimal Qty = Convert.ToDecimal(ed.Qty);
                                decimal vat1 = 0;
                                ed.UnitPrice = UnitCost;
                                ed.Amount = UnitCost * Qty;
                                vat1 = (UnitCost * Qty) * 7 / 100;
                                ed.Vat = vat1;

                                db.SubmitChanges();
                                radGridView1.Rows[e.RowIndex].Cells["Amount"].Value = UnitCost * Qty;
                                radGridView1.Rows[e.RowIndex].Cells["Vat"].Value = vat1;


                            }
                            UpdateCost();
                        }



                    }
                }
                else if (e.ColumnIndex == radGridView1.Columns["PartName"].Index)
                {
                    int id = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_InvoiceLocalDT ed = db.tb_InvoiceLocalDTs.Where(es => es.id == id).FirstOrDefault();
                            if (ed != null)
                            {
                                ed.PartName = radGridView1.Rows[e.RowIndex].Cells["PartName"].Value.ToString();
                                db.SubmitChanges();                            
                                
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == radGridView1.Columns["CodeCustomer"].Index)
                {
                    int id = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_InvoiceLocalDT ed = db.tb_InvoiceLocalDTs.Where(es => es.id == id).FirstOrDefault();
                            if (ed != null)
                            {
                                ed.PastCustomer = radGridView1.Rows[e.RowIndex].Cells["CodeCustomer"].Value.ToString();
                                
                                db.SubmitChanges();

                            }
                        }
                    }
                }


            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการลบหรือไม่ (ทั้งหมด) ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {


                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int id = 0;

                        foreach(GridViewRowInfo rd in radGridView1.Rows)
                        {
                            db.sp_043_Inv_LocalDT_DeleteUpdate(txtInvNo.Text, rd.Cells["OrderNo"].Value.ToString(), rd.Cells["Plant"].Value.ToString(), rd.Cells["CodeNo"].Value.ToString(), txtCustomerNo.Text);

                        }
                        db.sp_043_Inv_Local_DeleteHD(txtInvNo.Text);
                        MessageBox.Show("ลบรายการเรียบร้อยแล้ว !");
                    }
                    btn_CreateInv.Enabled = false;
                    btnDelete.Enabled = false;
                    radButtonElement1.Enabled = false;
                }
                catch { }
            }
        }

        private void เพมใหมToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var CustList = db.sp_043_Inv_LocalTemp_InfoCustomer(txtCustomerNo.Text.Trim()).ToList();
                //if(CustList.Count>0)
                //{
                //    var info = CustList.FirstOrDefault();
                //    txtAddress.Text = info.ADR1;
                //    txtAddress2.Text = info.ADR2;
                //}
                var CustList2 = db.sp_043_Inv_LocalCust_Dynamics(txtCustomerNo.Text.Trim()).ToList();
                if (CustList2.Count > 0)
                {
                    var info = CustList2.FirstOrDefault();
                    txtCustomer.Text = Convert.ToString(info.CNAME);
                    txtBRANCH.Text = Convert.ToString(info.Branch);
                    txtCredit.Text = Convert.ToString(info.Credit);
                    txtTAXID.Text = Convert.ToString(info.TAXID);
                    txtAddress.Text = Convert.ToString(info.ADR1);
                    txtAddress2.Text = Convert.ToString(info.ADR2);
                    if (info.PType.Equals("EXP"))
                    {
                        Type = "B";
                    }
                    else
                    {
                        Type = "A";
                    }
                }
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //
            if(MessageBox.Show("ต้องการเปลี่ยน ลูกค้า!!","เปลี่ยนแปลงลูกค้า",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        string Data1 = Microsoft.VisualBasic.Interaction.InputBox("Customer No?", "Cust. No", "");

                        if (Data1 != "" && radGridView1.Rows.Count==0)
                        {
                            var CustList2 = db.sp_043_Inv_LocalCust_Dynamics(Data1).ToList();
                            if (CustList2.Count > 0)
                            {
                                var info = CustList2.FirstOrDefault();
                                txtCustomerNo.Text = Data1;
                                txtCustomer.Text = Convert.ToString(info.CNAME);
                                txtBRANCH.Text = Convert.ToString(info.Branch);
                                txtCredit.Text = Convert.ToString(info.Credit);
                                txtTAXID.Text = Convert.ToString(info.TAXID);
                                txtAddress.Text = Convert.ToString(info.ADR1);
                                txtAddress2.Text = Convert.ToString(info.ADR2);
                                if (info.PType.Equals("EXP"))
                                {
                                    Type = "B";
                                }
                                else
                                {
                                    Type = "A";
                                }
                            }
                            MessageBox.Show("ทำการกด บันทึกการแก้ไข เพื่อ Save");
                        }
                    }
                }
                catch { }
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Update Price!!", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!txtInvNo.Text.Equals(""))
                    {
                        db.sp_53_UpdatePriceInvoice(txtInvNo.Text);
                        InvNo = txtInvNo.Text;
                        DataLoad();
                        UpdateCost();
                        MessageBox.Show("ทำการบันทึก อีกครั้ง");
                    }
                }
            }
        }
    }
}
