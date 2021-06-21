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
    public partial class InvoiceLocalCre : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public InvoiceLocalCre(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public InvoiceLocalCre()
        {
            InitializeComponent();
      
        }
        public InvoiceLocalCre(string InvNox,string Typex,DateTime InvoiceDate,string CustNo,string CustName)
        {
            InitializeComponent();
            InvNo = InvNox;
            Type = Typex;
            txtInvNo.Text = InvNox;
            txtCustomerNo.Text = CustNo;
            txtCustomer.Text = CustName;
            dtDate1.Value = InvoiceDate;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var CustList2 = db.sp_043_Inv_LocalCust_Dynamics(txtCustomerNo.Text.Trim()).ToList();
                if (CustList2.Count > 0)
                {
                    if (CustList2.FirstOrDefault().PType.Equals(""))
                    {
                        Type = "A";
                    }
                    else
                        Type = "B";
                }
            }
            if (Type.Equals("B"))
            {
                chkNoVat.Checked = true;
            }
            else
            {
                chkNoVat.Checked = false;
            }
            CheckVAT();

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
            //  dtDate1.Value = DateTime.Now;
            //  radDateTimePicker2.Value = DateTime.Now;
            DataLoad();
            LoadCustomer();
            //UpdateCost();
            CheckVAT();
           
           

        }
        private void LoadCustomer()
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
        private void DataLoad()
        {
            if(!InvNo.Equals(""))
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView1.DataSource = null;
                    radGridView1.DataSource = db.sp_043_Inv_LocalTemp_Select(InvNo).ToList();
                    //int count1 = 0;
                    //decimal Total = 0;
                    //decimal Vat = 0;
                    UpdateCost();
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
                }
            }

        }

        private void btn_PrintPR_Click(object sender, EventArgs e)
        {
            //Create Inv.
            try
            {
                if (MessageBox.Show("ต้องการบันทึกเป็น Invoice.", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string LastDate = dtDate1.Value.ToString("yyyy-MM-dd");
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_InvoiceLocalHD cks = db.tb_InvoiceLocalHDs.Where(hd => hd.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                        if (cks == null)
                        {
                            decimal Total = 0;
                            decimal Vat = 0;
                            decimal TotalAmount = 0;
                            decimal.TryParse(txtTotal.Text, out Total);
                            decimal.TryParse(txtVat.Text, out Vat);
                            decimal.TryParse(txtAmount.Text, out TotalAmount);
                            //Insert Hd//
                            tb_InvoiceLocalHD hd = new tb_InvoiceLocalHD();
                            hd.InvoiceNo = txtInvNo.Text;
                            hd.InvoiceDate = dtDate1.Value;
                            hd.CustomerNo = txtCustomerNo.Text;
                            hd.CustomerName = txtCustomer.Text;
                            hd.Address = txtAddress.Text;
                            hd.Address2 = txtAddress2.Text;
                            hd.Branch = txtBRANCH.Text;
                            hd.CustomerRegisterVat = txtTAXID.Text;
                            hd.Credit = txtCredit.Text;
                            hd.RefNo = txtRefNo.Text;
                            hd.Remark1 = txtRemark.Text;
                            hd.Status = "Process";
                            hd.Total = Total;
                            hd.Vat = Vat;
                            hd.TotalAmount = TotalAmount;
                            hd.BathText = txtThaiBath.Text;
                            hd.CreateBy = dbClss.UserID;
                            hd.CreateDate = DateTime.Now;                           
                            byte[] barcode = dbClss.SaveQRCode2D(txtInvNo.Text);
                            hd.BarCode = barcode;
                            hd.TypeVat = Type;
                            if(chkNoVat.Checked)
                            {
                                hd.TypeVat = "B";
                            }else
                            {
                                hd.TypeVat = "A";
                            }
                            db.tb_InvoiceLocalHDs.InsertOnSubmit(hd);
                            db.SubmitChanges();

                            //insert Line///
                            int AC = 0;
                            if (rdoG1.IsChecked)
                                AC = 0;
                            if (rdoG2.IsChecked)
                                AC = 1;
                            if (rdoG3.IsChecked)
                                AC = 2;
                            if (rdoG4.IsChecked)
                                AC = 3;
                            if (rdoG5.IsChecked)
                                AC = 4;

                            int CountAAA = 0;
                            var ListInsert = db.sp_043_Inv_LocalLine_Insert(txtInvNo.Text, AC).ToList();
                            foreach (var rd in ListInsert)
                            {
                                CountAAA += 1;
                                tb_InvoiceLocalDT dt = new tb_InvoiceLocalDT();
                                dt.InvoiceNo = txtInvNo.Text;
                                dt.OrderNo = rd.OrderNo;
                                dt.Plant = rd.Plant;
                                dt.PartNo = rd.CodeNo;
                                dt.PartName = rd.CodeName;
                                dt.PastCustomer = rd.CodeCustomer;
                                dt.Qty = Convert.ToDecimal(rd.Qty);
                                dt.UnitPrice = Convert.ToDecimal(rd.UnitCost);
                                dt.Amount = Convert.ToDecimal(rd.Amount);
                                dt.Vat = Convert.ToDecimal(rd.Amount)*7/100;//  Convert.ToDecimal(rd.Vat);
                                dt.SS = 1;
                                dt.SortQ = CountAAA;
                                dt.Discount = 0;
                                dt.Unit = "PCS";
                                db.tb_InvoiceLocalDTs.InsertOnSubmit(dt);
                                db.SubmitChanges();

                            }
                            //Update tb_LocalDelivery01//
                            radGridView1.EndEdit();
                            foreach (GridViewRowInfo rs in radGridView1.Rows)
                            {
                                db.sp_043_Inv_LocalTemp_SelectUpdate_Dynamics(txtInvNo.Text, Convert.ToString(rs.Cells["OrderNo"].Value), Convert.ToString(rs.Cells["Plant"].Value), Convert.ToString(rs.Cells["CodeNo"].Value), txtCustomerNo.Text);
                            }

                            MessageBox.Show("บันทึกสำเร็จ!!");
                            btnDelete.Enabled = false;
                            btn_CreateInv.Enabled = false;


                        }
                        else
                        {
                            MessageBox.Show("เลขที่ Invoice นี้ถูกใช้ไปแล้ว!");
                        }

                    }
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
                if (MessageBox.Show("ต้องการลบหรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int id = 0;
                        int.TryParse(radGridView1.Rows[rows].Cells["id"].Value.ToString(), out id);
                        if (id > 0)
                        {
                            tb_InvoiceLocalTempList dl = db.tb_InvoiceLocalTempLists.Where(l => l.id == id).FirstOrDefault();
                            if (dl != null)
                            {
                                db.tb_InvoiceLocalTempLists.DeleteOnSubmit(dl);
                                db.SubmitChanges();
                            }
                        }

                    }
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

                if(chkNoVat.Checked)
                {
                    Vat = 0;
                    txtVat.Text = "0";
                }
                txtTotal.Text = Total.ToString("###,###,##0.00");
                if (!chkNoVat.Checked)
                {
                    txtVat.Text = ((Total * Convert.ToDecimal(1.07)) - Total).ToString("###,###,##0.00");
                    txtAmount.Text = (Total * Convert.ToDecimal(1.07)).ToString("###,###,##0.00");
                }else
                {
                    txtVat.Text = "0";
                    txtAmount.Text = txtTotal.Text;
                }
                txtDiscount.Text = "0.00";
                txtAfterDiscount.Text = "0.00";
                txtThaiBath.Text = dbClss.ThaiBaht(txtAmount.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            txtInvNo.Text = GetInvoiceNo(txtCustomerNo.Text, dtDate1.Value);
            CheckVAT();
        }
        private void CheckVAT()
        {
            if (txtInvNo.Text.Length > 1)
            {
               
                if (txtInvNo.Text.Substring(0, 2).ToUpper().Equals("FZ"))
                {
                    chkNoVat.Checked = true;
                }
                else
                {
                    chkNoVat.Checked = false;
                }
                UpdateCost();
            }
        }
        
        private string GetInvoiceNo(string CustNo, DateTime ShipDate)
        {
            string InvNo = "";
            string LastDate = ShipDate.ToString("yyyy-MM-dd");

            int RUNNING = 0;
            int runningRow = 0;
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
                            runningRow += 1;
                            if (runningRow > 20)
                            {
                                CKInv = false;
                            }
                            Type = "B";
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
                            //CKInv = false;
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
                            runningRow += 1;
                            if (runningRow > 20)
                            {
                                CKInv = false;
                            }
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
                            // CKInv = false;
                            CKInv = CheckUpdateInvNo(InvNo);
                            if (CKInv)
                            {
                                tb_InvoiceNoSery Ns2 = db.tb_InvoiceNoSeries.Where(w => w.LastDate.Equals(LastDate) && w.VatType.Equals("B")).FirstOrDefault();
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
                try
                {
                    tb_InvoiceLocalHD ckh = db.tb_InvoiceLocalHDs.Where(h => h.InvoiceNo.Equals(INVNo)).FirstOrDefault();
                    if (ckh != null)
                    {
                        ck = true;
                    }
                }catch { ck = false; }
            }
            return ck;
        }

        private void radGridView1_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                if(e.ColumnIndex==radGridView1.Columns["UnitCost"].Index)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        decimal UnitCost = 0;
                        UnitCost = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["UnitCost"].Value);
                        int id = 0;
                        int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                        if(id>0)
                        {
                            tb_InvoiceLocalTempList ed = db.tb_InvoiceLocalTempLists.Where(es => es.id == id).FirstOrDefault();
                            if(ed!=null)
                            {
                                decimal Qty = Convert.ToDecimal(ed.Qty);
                                decimal vat1 = 0;
                                ed.UnitCost = UnitCost;
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
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }
    }
}
