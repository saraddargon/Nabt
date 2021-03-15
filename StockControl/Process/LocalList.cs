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
using Telerik.WinControls;
using Microsoft.VisualBasic;
namespace StockControl
{
    public partial class LocalList : Telerik.WinControls.UI.RadRibbonForm
    {
        public LocalList()
        {
            InitializeComponent();
        }

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
        int CCRow = 0;
        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            dt.Columns.Add(new DataColumn("edit", typeof(bool)));
            dt.Columns.Add(new DataColumn("code", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Active", typeof(bool)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateBy", typeof(string)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            radGridView1.AutoGenerateColumns = false;
           // chkShipDate.Checked = false;
            txtSaleOrderNo.Text = "";
            txtPartNo.Text = "";
            dtDate1.Value = DateTime.Now;
            dtDate2.Value = DateTime.Now;
            LoadData();
            LoadDefault1();
            // cboStatus.Text = "Waiting";
        }

        private void LoadDefault1()
        {

            cboCustomer.DataSource = null;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var cp = db.sp_034_SelectCustomer_Dynamics().ToList();
                if(cp.Count>0)
                {
                    cboCustomer.DataSource = cp;
                    cboCustomer.DisplayMember="CustNo";
                    cboCustomer.ValueMember = "CustNo";
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CCRow = 0;
            LoadData();
        }
        int Row = 0;
        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_019_LocaDeliveryList_DynamicsUPDate();
                    radGridView1.DataSource = null;
                    radGridView1.DataSource = db.sp_019_LocaDeliveryList_Dynamics(dtDate1.Value, dtDate2.Value, txtSaleOrderNo.Text, txtPartNo.Text, txtPlant.Text,txtCust.Text).ToList();
                    int CRow = 0;
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                        CRow += 1;
                        rd.Cells["No"].Value = CRow;
                    }
                }
            }
            catch { }
            this.Cursor = Cursors.Default;

        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
           
            PirntTAGA();
            Report.Reportx1.WReport = "PDTAG";
            Report.Reportx1.Value = new string[3];
            Report.Reportx1.Value[0] = dbClss.UserID;
            Report.Reportx1.Value[1] = dbClss.UserID;
            Report.Reportx1 op = new Report.Reportx1("FG_TAG_EX.rpt");
            op.Show();
        }
        private void PirntTAGA()
        {
            //Print TAG//
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int Qty = 0;
                int snp = 1;
                int TAG = 0;
                int a = 0;
                double ap = 0;
                int Remain = 0;
                int OrderQty = 0;
                //int.TryParse(txtQty.Text, out Qty);
                // int.TryParse(txtsNP.Text, out snp);
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var tm = db.tb_ProductTAGs.Where(t => t.UserID.ToLower() == dbClss.UserID.ToLower()).ToList();
                    if (tm.Count > 0)
                    {
                        db.tb_ProductTAGs.DeleteAllOnSubmit(tm);
                        db.SubmitChanges();
                    }
                }
                string OfTAG = "";
                string QrCode = "";
                radGridView1.EndEdit();
                int RowSEQ = 0;
                foreach (GridViewRowInfo rr in radGridView1.Rows)
                {
                    //37400010171
                    if (Convert.ToBoolean(rr.Cells["S"].Value))
                    {
                        Qty = 0;
                        snp = 1;
                        // int.TryParse(Convert.ToString(rr.Cells["OrderQty"].Value), out Qty);
                        // int.TryParse(Convert.ToString(rr.Cells["SNP"].Value), out snp);
                        snp = Convert.ToInt32(rr.Cells["SNP"].Value);
                        Qty = Convert.ToInt32(rr.Cells["OrderQty"].Value);
                        if (snp == 0)
                            snp = 1;
                        OrderQty = Qty;

                        if (Qty > 0)
                        {
                            // string TMNo = dbClss.GetSeriesNo(2, 2);
                            if (Qty != 0 && snp != 0)
                            {
                                a = 0;
                                ap = (Qty % snp);
                                if (ap > 0)
                                    a = 1;
                                TAG = Convert.ToInt32(Math.Floor((Convert.ToDouble(Qty) / Convert.ToDouble(snp)) + a));//.ToString("###");

                                //txtOftag.Text = Math.Ceiling((double)1.7 / 10).ToString("###");

                                Remain = Qty;
                            }
                            int C = 0;
                            string ImagePath = "";
                            string ImageName = "";

                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                tb_Path ph = db.tb_Paths.Where(p => p.PathCode == "Image").First();
                                if (ph != null)
                                {
                                    ImagePath = ph.PathFile;
                                }
                                tb_ItemList il = db.tb_ItemLists.Where(i => i.CodeNo == rr.Cells["PartNo"].Value.ToString()).FirstOrDefault();
                                if (il != null)
                                {
                                    ImageName = il.PathImage;
                                }


                               

                                for (int i = 1; i <= TAG; i++)
                                {
                                    RowSEQ += 1;
                                    OfTAG = "";
                                    QrCode = "";
                                    if (Remain > snp)
                                    {
                                        Qty = snp;
                                        Remain = Remain - snp;
                                    }
                                    else
                                    {
                                        Qty = Remain;
                                        Remain = 0;
                                    }
                                    OfTAG = i + "of" + TAG;
                                    QrCode = "";
                                    QrCode = "PD," + rr.Cells["SaleOrderNo"].Value.ToString() + "," + Qty + "," + OrderQty + "," + "";
                                    QrCode += "," + OfTAG + "," + rr.Cells["PartNo"].Value.ToString() + "," +Convert.ToDateTime(rr.Cells["ShippingDate"].Value).ToString("ddMMyy");
                                    //MessageBox.Show(QrCode);
                                    byte[] barcode = dbClss.SaveQRCode2D(QrCode);

                                    ///////////////////////////////
                                    tb_ProductTAG ts = new tb_ProductTAG();
                                    ts.UserID = dbClss.UserID;
                                    ts.BOMNo = rr.Cells["SaleOrderNo"].Value.ToString();
                                    ts.LotNo = "";
                                    // ts. = dtDate1.Value.ToString("dd/MM/yyyy");
                                    ts.QRCode = barcode;
                                    ts.PartName = rr.Cells["PartName"].Value.ToString();
                                    ts.PartNo = rr.Cells["PartNo"].Value.ToString();
                                    ts.Machine = Environment.MachineName;
                                    ts.OFTAG = i + "/" + TAG;
                                    if (!ImageName.Equals(""))
                                        ts.PathPic = ImagePath + ImageName;
                                    else
                                        ts.PathPic = "";

                                    ts.Qty = Qty;
                                    ts.Seq = RowSEQ;
                                    ts.CSTMShot = "";
                                    ts.CustomerName = rr.Cells["CustomerName"].Value.ToString();
                                    ts.CSTMItem = rr.Cells["CustomerItemNo"].Value.ToString();
                                    ts.CustItem2 = "";
                                    ts.SHIFT = Convert.ToDateTime(rr.Cells["CDate"].Value).ToString("yyyy-MM-dd");

                                    //// ลูกค้า ISUSU  ///
                                    if (rr.Cells["CustomerName"].Value.ToString().ToUpper().Trim().Contains("ISUZU"))
                                    {
                                        ts.CSTMItem = "A" + rr.Cells["CustomerItemNo"].Value.ToString();// + ""+dtDate1.Value.Year.ToString();
                                    }
                                    ///////////////////

                                    //ts.s = snp;
                                    // ts.Company = "Nabtesco Autmotive Corporation";
                                    //ts.Quantity = Qty;
                                    // ts.OfTAG = i + " / " + TAG;
                                    ///////////////////////////////////////////////
                                    db.tb_ProductTAGs.InsertOnSubmit(ts);
                                    db.SubmitChanges();
                                    C += 1;
                                }

                            }

                            

                            ////////////////////////////////////////////



                        }
                        //else
                        //{
                        //    MessageBox.Show("Qty invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                    }

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                GuidLineLot();
                radGridView1.EndEdit();
                int CountA = 0;
                int id = 0;
                int Qty = 0;
                string QRCode = "";
                string inv = "";
                string SaleOrderNo = "";
                string PartNo = "";
                string HNo = "";
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_018_LocalDeliveryShip();
                    
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                        //db.sp_016_GildLineLotDeleteLot();
                        //foreach (GridViewRowInfo rd2 in radGridView1.Rows)
                        //{
                        //    id = 0;
                        //    if (Convert.ToBoolean(rd2.Cells["S"].Value) &&
                        //        Convert.ToBoolean(rd2.Cells["DocumentFlag"].Value) &&
                        //        !Convert.ToBoolean(rd2.Cells["PackingFlag"].Value)
                        //        )
                        //    {

                        //        //CountA += 1;
                        //        db.sp_016_GildLineLot(rd2.Cells["SaleOrderNo"].Value.ToString(), rd2.Cells["PartNo"].Value.ToString());

                        //    }
                        //}

                        QRCode = "";
                        id = 0;
                        Qty = 0;
                        if (Convert.ToBoolean(rd.Cells["S"].Value) && Convert.ToBoolean(rd.Cells["DocumentFlag"].Value))
                        {

                            CountA += 1;
                            SaleOrderNo = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                            PartNo = Convert.ToString(rd.Cells["PartNo"].Value);
                            Qty= Convert.ToInt32(rd.Cells["OrderQty"].Value);
                            tb_LocalDeliveryShip de = new tb_LocalDeliveryShip();
                            de.idOrder = id;
                            de.SaleOrder = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                            de.Invoice = Convert.ToDateTime(rd.Cells["ShippingDate"].Value).ToString("yyyyMMdd") + "," + Convert.ToString(rd.Cells["CustomerNo"].Value) + "," +
                                Convert.ToString(rd.Cells["SaleOrderNo"].Value) + ","+ Convert.ToString(rd.Cells["PartNo"].Value);
                            de.PartName = Convert.ToString(rd.Cells["PartName"].Value);
                            de.PartNo = Convert.ToString(rd.Cells["PartNo"].Value);
                            de.CustItem = Convert.ToString(rd.Cells["CustomerItemNo"].Value);
                            de.CustomerName = Convert.ToString(rd.Cells["CustomerNo"].Value)+" "+Convert.ToString(rd.Cells["CustomerName"].Value);
                            de.Remark = "";
                            de.PlantA2 = Convert.ToString(rd.Cells["Plant"].Value);
                            de.ShippingDate = Convert.ToDateTime(rd.Cells["CDate"].Value);
                            de.Qty = Qty;
                            //id,OrderNo,PartNo,Qty
                            //Order,PartNo,CustomerItemNo,Qty,OrderQty,ofTAG,CustomerTAG
                            //TAG LIST=Order,PartNo,CustomerNo,OrderQty,Plant
                            QRCode = Convert.ToString(rd.Cells["SaleOrderNo"].Value) + "," + Convert.ToString(rd.Cells["PartNo"].Value);
                            QRCode += "," + Convert.ToString(rd.Cells["CustomerItemNo"].Value) + "," + Qty.ToString()+","+ Convert.ToString(rd.Cells["Plant"].Value);

                            byte[] barcode = dbClss.SaveQRCode2D(QRCode);
                            de.QRCode = barcode;
                            byte[] barcode2 = dbClss.SaveQRCode2D(Convert.ToString(rd.Cells["InvoiceNo"].Value));
                            de.QRCode2 = barcode2;

                            db.tb_LocalDeliveryShips.InsertOnSubmit(de);
                            db.SubmitChanges();

                            ////Update Print Flag////
                            try
                            {
                                HNo = dbClss.GetSeriesNo(86, 2);
                                db.sp_016_GildLineLot_DynamicsHistory(Convert.ToString(rd.Cells["SaleOrderNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value), Convert.ToString(rd.Cells["Plant"].Value), HNo);
                                ////Keep History////
                            }
                            catch { }

                            tb_LocalListDeliverly01 up = db.tb_LocalListDeliverly01s.Where(rc => rc.SaleOrder == SaleOrderNo
                            && rc.PartNo == PartNo && rc.PrintFlag == false
                            ).FirstOrDefault();
                            if(up!=null)
                            {
                                up.PrintFlag = true;
                                up.PrintDate = DateTime.Now;
                                up.PrintBy = dbClss.UserID;
                                db.SubmitChanges();
                            }
                        }
                        
                    }
                    if (CountA > 0)
                    {
                        this.Cursor = Cursors.Default; 
                        //Print//
                        Report.Reportx1.Value = new string[3];
                        Report.Reportx1.Value[0] = dtDate1.Value.ToString();
                        Report.Reportx1.Value[1] = dtDate2.Value.ToString();                        
                        Report.Reportx1.WReport = "PackingList";
                        Report.Reportx1 op = new Report.Reportx1("LocalForConfirm.rpt");
                        op.Show();
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default;  MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default; 
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    if (MessageBox.Show("ต้องการค้นหา Lot No. หรือไม่ ?\n ใช้เวลาสักครู่.....", "อัพเดต Lot No.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        radGridView1.EndEdit();
            //        int CountA = 0;
            //        int id = 0;
            //        progressBar1.Visible = true;
            //        using (DataClasses1DataContext db = new DataClasses1DataContext())
            //        {
            //            db.sp_016_GildLineLotDeleteLot();
            //            var rList = db.sp_32_SelectList().ToList();
            //            progressBar1.Minimum = 1;
            //            progressBar1.Maximum = rList.Count+1; 
            //            foreach (var rd in rList)
            //            {
            //                id = 0;
            //                CountA += 1;
            //                progressBar1.Value = CountA;
            //                progressBar1.PerformStep();
            //                db.sp_016_GildLineLot(rd.SORDER,rd.CODE);                                                   
            //            }
            //        }

            //        if (CountA > 0)
            //        {
            //            MessageBox.Show("อัพเดต Lot No เรียบร้อย");
            //            LoadData();
            //        }
            //    }
            //}
            //catch (Exception ex) { this.Cursor = Cursors.Default;  MessageBox.Show(ex.Message); }
            //this.Cursor = Cursors.Default;
            //progressBar1.Visible = false;
            GuidLineLot();
        }
        private void GuidLineLot()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (MessageBox.Show("ต้องการค้นหา Lot No. หรือไม่ ?\n ใช้เวลาสักครู่.....", "อัพเดต Lot No.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    radGridView1.EndEdit();
                    int CountA = 0;
                    int id = 0;
                    progressBar1.Visible = true;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_016_GildLineLotDeleteLot();
                        db.sp_33_DeleteLocalLot();
                        var rList = db.sp_32_SelectList_Dynamics().ToList();
                        progressBar1.Minimum = 1;
                        progressBar1.Maximum = rList.Count + 1;
                        foreach (var rd in rList)
                        {
                            id = 0;
                            CountA += 1;
                            progressBar1.Value = CountA;
                            progressBar1.PerformStep();
                            //Note = Plant
                            db.sp_016_GildLineLot_Dynamics(rd.SORDER,rd.DocNo, rd.CODE,rd.NOTE);
                        }
                    }

                    if (CountA > 0)
                    {
                        MessageBox.Show("อัพเดต Lot No เรียบร้อย");
                        //LoadData();
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
            progressBar1.Visible = false;
        }

        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            Row = e.RowIndex;
        }

        private void chkALL_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkALL.Checked)
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    rd.Cells["S"].Value = true;
                }

            }
            else
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    rd.Cells["S"].Value = false;
                }
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            //Print Lot//
            try
            {
                GuidLineLot();
                this.Cursor = Cursors.WaitCursor;
                radGridView1.EndEdit();
                int CountA = 0;
                int id = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_017_GildLineLotTempDelete();
                  //  db.sp_33_DeleteLocalLot();
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                        id = 0;
                        if (Convert.ToBoolean(rd.Cells["S"].Value))
                        {
                            //if (Convert.ToString(rd.Cells["PartNo"].Value).Equals("") || Convert.ToString(rd.Cells["PartNo"].Value).Equals(""))
                            //{
                            //    db.RP_LocalStock_Cal(Convert.ToString(rd.Cells["PartNo"].Value));
                            //}
                           
                            CountA += 1;
                            int.TryParse(Convert.ToString(rd.Cells["id"].Value), out id);
                            var getList = db.tb_GuideLineLots.Where(u => u.idOrder == id).ToList();
                            
                            foreach(var rdx in getList)
                            {
                                CountA += 1;
                                tb_GuideLineLotTempPrint ad = new tb_GuideLineLotTempPrint();
                                ad.OrderNo = rdx.OrderNo;
                                ad.InvoiceNo = rdx.InvoiceNo;
                                ad.PartName = rdx.PartName;
                                ad.LotNo = rdx.LotNo;
                                ad.Code = rdx.Code;
                                ad.idOrder = rdx.idOrder;
                                ad.idRef = rdx.idRef;
                                ad.SNP = rdx.SNP;
                                ad.Qty = rdx.Qty;
                                ad.RefNo = rdx.RefNo;
                                db.tb_GuideLineLotTempPrints.InsertOnSubmit(ad);
                                db.SubmitChanges();
                                   
                            }

                        }
                    }
                    if(CountA>0)
                    {
                        this.Cursor = Cursors.Default;
                        //Print//
                        Report.Reportx1.Value = new string[1];
                        Report.Reportx1.Value[0] = "";
                       
                        Report.Reportx1.WReport = "Guideline";
                        Report.Reportx1 op = new Report.Reportx1("GuideLineLot.rpt");
                        op.Show();
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            try
            {
                LocationTAGCheck ck1 = new LocationTAGCheck("");
                ck1.Show();
                
            }
            catch { }
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = false;
        }

        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการปริ้น","ปริ้นเอกสาร Manual TAG",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        radGridView1.EndEdit();
                        db.sp_030_DeleteTempPrintReport(1);
                        int SNP = 0;
                        int Qty = 0;
                        int REmain = 0;
                        int a = 0;
                        int TAG = 0;
                        int ap = 0;
                        int TAGA = 0;
                        string QrCode = "";
                        string QrCode2 = "";
                        foreach (var rd in radGridView1.Rows)

                        {

                            QrCode = "";
                            if (Convert.ToBoolean(rd.Cells["S"].Value))
                            {
                                SNP = Convert.ToInt32(rd.Cells["SNP"].Value);
                                Qty = Convert.ToInt32(rd.Cells["OrderQty"].Value);

                                if (SNP > 0)
                                {
                                    a = 0;
                                    ap = (Qty % SNP);
                                    if (ap > 0)
                                        a = 1;
                                    TAG = Convert.ToInt32(Math.Floor((Convert.ToDouble(Qty) / Convert.ToDouble(SNP)) + a));//.ToString("###");
                                    REmain = Qty;

                                    TAGA = Qty;
                                    for (int i = 1; i <= TAG; i++)
                                    {
                                        TAGA = TAGA - Convert.ToInt32(rd.Cells["SNP"].Value);
                                        tb_LocalPrintN ls = new tb_LocalPrintN();
                                        ls.CustomerName = Convert.ToString(rd.Cells["CustomerName"].Value); ;// "NISSAN MOTOR (THAILAND) CO.,LTD";
                                        ls.CustomerItem = Convert.ToString(rd.Cells["CustomerItemNo"].Value);
                                        ls.NaptItemCode = Convert.ToString(rd.Cells["PartNo"].Value);
                                        ls.PartName = Convert.ToString(rd.Cells["PartName"].Value);
                                        ls.Plant = db.getPlanTIDTPICS_Dynamics(Convert.ToString(rd.Cells["PartNo"].Value));
                                        ls.PONo = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                                        ls.SNPBOX = Convert.ToInt32(rd.Cells["SNP"].Value).ToString("###,###,###");
                                        ls.DeliveryDate = Convert.ToDateTime(rd.Cells["ShippingDate"].Value).ToString("dd/MM/yyyy");
                                        ls.OFTAG = i.ToString()+"  /  "+TAG.ToString();
                                        ls.NaptCompany = "Nabtesco Automotive Products(Thailand) Co.,Ltd.";
                                        ls.Remark = "";
                                        if(i==TAG)
                                        {
                                            // TAGA = TAG * Convert.ToInt32(rd.Cells["SNP"].Value);
                                            TAGA += Convert.ToInt32(rd.Cells["SNP"].Value);
                                            ls.SNPBOX = Convert.ToInt32(TAGA).ToString("###,###,###");
                                        }
                                        QrCode = "";
                                        QrCode2 = "";
                                        QrCode = Convert.ToString(rd.Cells["CustomerItemNo"].Value);
                                        QrCode2 = Convert.ToString(rd.Cells["PartNo"].Value);
                                        //MessageBox.Show(QrCode);
                                        byte[] barcode = dbClss.SaveQRCode2D(QrCode);
                                        byte[] barcode2 = dbClss.SaveQRCode2D(QrCode2);
                                        ls.BRCode = barcode;
                                        ls.BRCode2 = barcode2;

                                        db.tb_LocalPrintNs.InsertOnSubmit(ls);
                                        db.SubmitChanges();
                                    }


                                }



                            }
                        }
                        Report.Reportx1.WReport = "NSTAG";
                        Report.Reportx1.Value = new string[1];
                        Report.Reportx1.Value[0] = ""; 
                        Report.Reportx1 op = new Report.Reportx1("LO_ReportNS.rpt");
                        op.Show();
                        

                    }
                }
                catch { }
            }
        }

        private void radButtonElement6_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("ต้องการปริ้น MITSUBISHI", "ปริ้นเอกสาร MITSUBISHI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        radGridView1.EndEdit();
                        db.sp_030_DeleteTempPrintReport(2);
                        int SNP = 0;
                        int Qty = 0;
                        int REmain = 0;
                        int a = 0;
                        int TAG = 0;
                        int TAGA = 0;
                        int ap = 0;
                        string Barcode = "";
                        string Box = "";

                        foreach (var rd in radGridView1.Rows)
                        {
                            if (Convert.ToBoolean(rd.Cells["S"].Value))
                            {
                                SNP = Convert.ToInt32(rd.Cells["SNP"].Value);
                                Qty = Convert.ToInt32(rd.Cells["OrderQty"].Value);
                                if (SNP==0)
                                {
                                    SNP = 1;
                                }

                                a = 0;
                                ap = (Qty % SNP);
                                if (ap > 0)
                                    a = 1;
                                TAG = Convert.ToInt32(Math.Floor((Convert.ToDouble(Qty) / Convert.ToDouble(SNP)) + a));//.ToString("###");
                                TAGA = Qty;
                                for (int i = 1; i <= TAG; i++)
                                {
                                   
                                    Barcode = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                                    Barcode = Barcode + "-" + dbClss.Right("0000" + i.ToString(), 4) + "-" + dbClss.Right("0000"+Qty, 4);
                                    tb_LocalMITSUBISHI ms = new tb_LocalMITSUBISHI();
                                    ms.PartNo = Convert.ToString(rd.Cells["PartNo"].Value);
                                    ms.PartName = Convert.ToString(rd.Cells["PartName"].Value);
                                    ms.OriginalDate = Convert.ToDateTime(rd.Cells["ShippingDate"].Value);
                                    ms.OrderNo = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                                    ms.BOXNo = i.ToString() + " / " + TAG.ToString();
                                    ms.Period1 = 1;
                                    ms.Period2 = 1;
                                    ms.PLANT = "X";
                                    ms.RA = "H03";
                                    ms.DeliveryLotQty = SNP + "/" + SNP;
                                    if (i == TAG)
                                    {
                                        ms.DeliveryLotQty = TAGA + "/" + SNP;
                                    }
                                   
                                    ms.TotalQty = Convert.ToInt32(rd.Cells["OrderQty"].Value);
                                    ms.VendorName = "NABTESCO AUTOMOTIVE (THAILAND) CO.,LTD.";
                                    ms.VendorNo = "N138";
                                    ms.ConfirmDate = Convert.ToDateTime(rd.Cells["ShippingDate"].Value);
                                    ms.CustomerItemName = Convert.ToString(rd.Cells["CustomerItemNo"].Value);
                                    ms.Remark2 = Barcode;
                                    db.tb_LocalMITSUBISHIs.InsertOnSubmit(ms);
                                    TAGA = TAGA - SNP;
                                    //tb_LocalPrintN ls = new tb_LocalPrintN();
                                    //ls.CustomerName = "NISSAN MOTOR (THAILAND) CO.,LTD";
                                    //ls.CustomerItem = Convert.ToString(rd.Cells["CustomerItemNo"].Value);
                                    //ls.NaptItemCode = Convert.ToString(rd.Cells["PartNo"].Value);
                                    //ls.PartName = Convert.ToString(rd.Cells["PartName"].Value);
                                    //ls.Plant = "KM.32";
                                    //ls.PONo = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                                    //ls.SNPBOX = Convert.ToString(rd.Cells["SNP"].Value);
                                    //ls.DeliveryDate = Convert.ToDateTime(rd.Cells["ShippingDate"].Value).ToString("dd/MM/yyyy");
                                    //ls.OFTAG = "     /     ";
                                    //ls.NaptCompany = "Nabtesco Automotive Products(Thailand) Co.,Ltd.";
                                    db.SubmitChanges();
                                }
                            }
                        }
                        Report.Reportx1.WReport = "NSTAG";
                        Report.Reportx1.Value = new string[1];
                        Report.Reportx1.Value[0] = "";
                        Report.Reportx1 op = new Report.Reportx1("LO_ReportMITSU.rpt");
                        op.Show();


                    }
                }
                catch { }
            }
        }

        private void radButtonElement7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการปริ้น Control Lot", "ปริ้นเอกสาร Control Lot", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        radGridView1.EndEdit();
                        db.sp_030_DeleteTempPrintReport(3);
                        int SNP = 0;
                        int Qty = 0;
                        int REmain = 0;
                        int a = 0;
                        int TAG = 0;
                        int ap = 0;
                        string Barcode = "";
                        string Box = "";
                        foreach (var rd in radGridView1.Rows)
                        {
                            if (Convert.ToBoolean(rd.Cells["S"].Value))
                            {
                                SNP = Convert.ToInt32(rd.Cells["SNP"].Value);
                                Qty = Convert.ToInt32(rd.Cells["OrderQty"].Value);
                               
                            
                                tb_LocalLotControl lc = new tb_LocalLotControl();
                                lc.SupplierName = "Nabtesco Automotive Products (Thailand) Co.,Ltd.";
                                lc.PartNo = Convert.ToString(rd.Cells["PartNo"].Value);
                                lc.PartName = Convert.ToString(rd.Cells["PartName"].Value);
                                lc.Model = "VD00";
                                lc.Quantity = Qty;
                                lc.LotNo = "";
                                lc.DeliveryDate = Convert.ToDateTime(rd.Cells["CDate"].Value);
                                lc.AsemblyDate = Convert.ToDateTime(rd.Cells["CDate"].Value);
                                lc.FirstUnit = "";
                                lc.FinalUnit = "";
                                lc.QtyPass = "";
                                lc.Remark = Convert.ToString(rd.Cells["SaleOrderNo"].Value);
                                lc.Customer = "";
                                lc.CustomerItemNo = Convert.ToString(rd.Cells["CustomerItemNo"].Value);
                                db.tb_LocalLotControls.InsertOnSubmit(lc);
                                db.SubmitChanges();
                                
                            }
                        }
                        Report.Reportx1.WReport = "NSTAG";
                        Report.Reportx1.Value = new string[1];
                        Report.Reportx1.Value[0] = "";
                        Report.Reportx1 op = new Report.Reportx1("LO_ReportControl.rpt");
                        op.Show();

                    }
                }
                catch { }
            }
        }

        private void radButtonElement8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if(!cboCustomer.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        //MessageBox.Show(cboCustomer.Text);
                        int CKA = 0;
                        radGridView1.EndEdit();
                        foreach (GridViewRowInfo rx in radGridView1.Rows)
                        {
                            if (Convert.ToBoolean(rx.Cells["S"].Value) )
                            {
                                CKA += 1;
                            }
                        }
                        int CountA = 0;
                        int CC = 0;
                        
                        db.sp_031_DeleteLocalDeliveryListQC();
                        db.sp_031_SelectLocalDeliveryUpdate_Dynamics(dtDate1.Value, dtDate2.Value);
                        var rlist = db.sp_031_SelectLocalDeliveryListQC_Dynamics(dtDate1.Value, dtDate2.Value,cboCustomer.Text.Trim()).ToList();
                        foreach (var rd in rlist)
                        {
                            CC = 0;
                            CountA = 1;


                            foreach (GridViewRowInfo rx in radGridView1.Rows)
                            {
                                if (rx.Cells["SaleOrderNo"].Value.ToString().Equals(rd.SaleOrder)
                                    && rx.Cells["PartNo"].Value.ToString().Equals(rd.PartNo)
                                    && rx.Cells["Plant"].Value.ToString().Equals(rd.Plant)
                                     && Convert.ToBoolean(rx.Cells["S"].Value)
                                    )
                                {
                                    CountA += 1;
                                    CC += 1;
                                }
                            }

                            string GroupA = "";
                            if(CKA==0)
                            {
                                CC = 1;
                            }


                            if (CC > 0)
                            {
                                int ic = 0;
                                var listLot = db.sp_031_SelectLocalDeliveryListLotNo_Dynamics(rd.SaleOrder, rd.PartNo,rd.Plant).ToList();
                                if (listLot.Count > 0)
                                {
                                    foreach (var rs in listLot)
                                    {
                                        ic = 0;
                                        //if (!rd.PartNo.Equals("37400010152") && !rd.PartNo.Equals("37400010171") 
                                        //    && !rd.PartNo.Equals("36111127040") 
                                        //    && !rd.PartNo.Equals("41211048054"))
                                        //{
                                        ic = Convert.ToInt32(db.g_getItemExport(rd.PartNo));
                                        if (ic == 0)
                                        {
                                            GroupA = rd.CustomerNo;
                                            tb_LocalDeliveryQCCheck qc = new tb_LocalDeliveryQCCheck();
                                            qc.ItemCode = rd.PartNo;
                                            qc.CustItem = rd.CustomerItemNo;
                                            qc.CustomerNo = rd.CustomerNo;
                                            qc.CustomerName = rd.CustomerName;
                                            qc.CustAddress = rd.CustAddress1;
                                            qc.PartName = rd.CustomerItemName;
                                            qc.CustAddress2 = rd.CustAddress2;
                                            qc.DeliveryDate = rd.ShippingDate;
                                            qc.InvoiceDate = rd.CDate;
                                            qc.IssueDate = DateTime.Now;

                                            if (Convert.ToString(rd.CustomerNo).Trim().Equals("3001"))
                                            {
                                                if (dbClss.Right(rd.PartNo, 1).ToLower().Equals("E"))
                                                {
                                                    GroupA = "3001(E)";
                                                }
                                            }

                                            qc.LotNo = rs.LotNo;
                                            qc.ShipQty = rs.Qty;
                                            qc.GroupB = GroupA;

                                            qc.Plant = rd.Plant;
                                            qc.SaleOrderNo = rd.SaleOrder;
                                            qc.QtyOrder = rd.ORderQty;
                                            db.tb_LocalDeliveryQCChecks.InsertOnSubmit(qc);
                                            db.SubmitChanges();
                                        }
                                    }

                                }
                            }


                            //}
                        }


                        ////Report///
                        Report.Reportx1.WReport = "DeliveryCheck";
                        Report.Reportx1.Value = new string[1];
                        Report.Reportx1.Value[0] = "";

                        Report.Reportx1 op = new Report.Reportx1("LO_DeliveryCheck.rpt");
                        op.Show();
                    }


                }else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("ต้องเลือกลูกค้าที่ต้องการก่อน!");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement9_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {                    
                    //////////////////////////////////////
                    db.sp_33_DeleteLocalLot();
                    /*
                    db.RP_LocalStock_Cal_Dynamics("37400010152");
                    db.RP_LocalStock_Cal_Dynamics("37400010171");
                    db.RP_LocalStock_Cal_Dynamics("36100027040");
                    db.RP_LocalStock_Cal_Dynamics("36111127040");
                    db.RP_LocalStock_Cal_Dynamics("41211048054");
                    */
                    db.RP_LocalStock_Cal_Dynamics("");

                    int CC = 0;
                    int CC2 = 0;
                    //////////////////////////////////////
                    radGridView1.EndEdit();
                    db.sp_031_DeleteLocalDeliveryListQC();
                    db.sp_031_SelectLocalDeliveryUpdate_Dynamics(dtDate1.Value, dtDate2.Value);
                    var rlist = db.sp_031_SelectLocalDeliveryListQC22_Dynamics(dtDate1.Value, dtDate2.Value, cboCustomer.Text.Trim()).ToList();
                    foreach (var rd in rlist)
                    {
                        CC = 0;
                        CC2 = 0;
                        //AAA+
                        foreach (GridViewRowInfo rx in radGridView1.Rows)
                        {
                            if (rx.Cells["SaleOrderNo"].Value.ToString().Equals(rd.SaleOrder)
                                && rx.Cells["PartNo"].Value.ToString().Equals(rd.PartNo)
                                //&& rx.Cells["Plant"].Value.ToString().Equals(rd.Plant)
                                 && Convert.ToBoolean(rx.Cells["S"].Value)
                                )
                            {
                                CC += 1;
                            }                            
                        }

                        if (CC > 0)
                        {
                            db.sp_031_SelectLocalDeliveryListLotNo22_Dynamics(rd.SaleOrder, rd.PartNo, "", Convert.ToInt32(rd.ORderQty));
                            string GroupA = "";
                            var listLot = db.sp_031_SelectLocalDeliveryListLotNo23_Dynamics(rd.SaleOrder, rd.PartNo,"").ToList();
                            if (listLot.Count > 0)
                            {
                                //foreach (GridViewRowInfo rx in radGridView1.Rows)
                                //{
                                //    if (rx.Cells["SaleOrderNo"].Value.ToString().Equals(rd.SaleOrder) 
                                //        && rx.Cells["PartNo"].Value.ToString().Equals(rd.PartNo)
                                //       // && Convert.ToBoolean(rx.Cells["S"].Value)
                                //        )
                                //        CC += 1;
                                //}

                                string QRCode = "";
                                foreach (var rs in listLot)
                                {

                                    GroupA = rd.CustomerNo;
                                    tb_LocalDeliveryQCCheck qc = new tb_LocalDeliveryQCCheck();
                                    qc.ItemCode = rd.PartNo;
                                    qc.CustItem = rd.CustomerItemNo;
                                    qc.CustomerNo = rd.CustomerNo;
                                    qc.CustomerName = rd.CustomerName;
                                    qc.CustAddress = rd.CustAddress1;
                                    qc.PartName = rd.CustomerItemName;
                                    qc.CustAddress2 = rd.CustAddress2;
                                    qc.DeliveryDate = rd.ShippingDate;
                                    qc.InvoiceDate = rd.CDate;
                                    qc.IssueDate = DateTime.Now;

                                    if (Convert.ToString(rd.CustomerNo).Trim().Equals("3001"))
                                    {
                                        if (dbClss.Right(rd.PartNo, 1).ToLower().Equals("E"))
                                        {
                                            GroupA = "3001(E)";
                                        }
                                    }

                                    qc.LotNo = rs.LotName;
                                    qc.ShipQty = rs.Qty;
                                    qc.GroupB = GroupA;

                                    qc.Plant = rd.Plant;
                                    qc.SaleOrderNo = rd.SaleOrder;
                                    qc.QtyOrder = rd.ORderQty;


                                    QRCode = Convert.ToString(rd.SaleOrder) + "," + Convert.ToString(rd.PartNo);
                                    QRCode += "," + Convert.ToString(rd.CustomerItemNo) + "," + Convert.ToDecimal(rd.ORderQty).ToString("######")+","+Convert.ToString(rd.Plant);
                                    byte[] barcode = dbClss.SaveQRCode2D(QRCode);
                                    qc.QRCode = barcode;
                                    db.tb_LocalDeliveryQCChecks.InsertOnSubmit(qc);
                                    db.SubmitChanges();

                                }
                            }
                        }

                        //}
                        //AA-
                    }


                    ////Report///
                    Report.Reportx1.WReport = "DeliveryCheck";
                    Report.Reportx1.Value = new string[1];
                    Report.Reportx1.Value[0] = "";

                    Report.Reportx1 op = new Report.Reportx1("LO_DeliveryCheck2.rpt");
                    op.Show();
                }



            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radGridView1_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && radGridView1.Columns["PL"].Index == e.ColumnIndex)
                {
                    // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    //int PalletNo = 0;
                    //int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    string OrderNo = radGridView1.Rows[e.RowIndex].Cells["SaleOrderNo"].Value.ToString();
                    string PartNo = radGridView1.Rows[e.RowIndex].Cells["PartNo"].Value.ToString();
                    DateTime dta = Convert.ToDateTime(radGridView1.Rows[e.RowIndex].Cells["ShippingDate"].Value);
                    string PL = radGridView1.Rows[e.RowIndex].Cells["PL"].Value.ToString();
                    if (!PL.Equals(""))
                    {
                        if (PL.Equals("0"))
                        {
                            PL = "";
                        }
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_041_UpdatePL(OrderNo, PartNo, dta, PL);
                            //tb_LocalListDeliverly01 pl1 = db.tb_LocalListDeliverly01s.Where(p => p.id == id).FirstOrDefault();
                            //if(pl1!=null)
                            //{
                            //    if(PL.Equals("0"))
                            //    {
                            //        PL = "";
                            //    }
                            //    pl1.PL = PL;
                            //    db.SubmitChanges();
                            //}
                        }
                    }
                }
                else if (e.RowIndex >= 0 && radGridView1.Columns["InvoiceNo"].Index == e.ColumnIndex)
                {
                    string InvoiceNox = radGridView1.Rows[e.RowIndex].Cells["InvoiceNo"].Value.ToString();
                    if (InvoiceNox.Equals("0"))
                    {
                        InvoiceNox = "";
                    }
                    string OrderNo = radGridView1.Rows[e.RowIndex].Cells["SaleOrderNo"].Value.ToString();
                    string PartNo = radGridView1.Rows[e.RowIndex].Cells["PartNo"].Value.ToString();
                    string Plant = radGridView1.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    DateTime dta = Convert.ToDateTime(radGridView1.Rows[e.RowIndex].Cells["ShippingDate"].Value);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_041_UpdateInvoice(OrderNo, PartNo, dta, InvoiceNox, Plant);
                    }
                }
            }
            catch { }
        }

        private void radButtonElement10_Click(object sender, EventArgs e)
        {
            PrintHINO ph = new PrintHINO();
            ph.Show();
        }

        private void radButtonElement11_Click(object sender, EventArgs e)
        {
            ScanPDAList spl = new ScanPDAList("Local");
            spl.Show();
        }

        private void radButtonElement12_Click(object sender, EventArgs e)
        {
            InvoiceList iv = new InvoiceList();
            iv.Show();
        }

        private void radButtonElement13_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการสร้าง Invoice No. หรือไม่ ?","Create Inv.",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    radGridView1.EndEdit();
                    int CheckRow = 0;
                    string CustomerNo = "";
                    string CustomerName = "";
                    string InvNo = "";
                    decimal VAT = 0;
                    decimal Price = 0;
                    DateTime ShipDate = DateTime.Now;
                    int countRow1 = 0;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_043_Inv_Local_TempDelete(dbClss.UserID);
                        radGridView1.EndEdit();
                        foreach (GridViewRowInfo rd in radGridView1.Rows)
                        {
                            if (Convert.ToBoolean(rd.Cells["S"].Value) && Convert.ToString(rd.Cells["InvoiceNo"].Value).Equals(""))
                            {
                                if (CustomerNo.Equals(""))
                                {
                                    CustomerNo = Convert.ToString(rd.Cells["CustomerNo"].Value).Trim();
                                    CustomerName = Convert.ToString(rd.Cells["CustomerName"].Value);
                                    InvNo = GetInvoiceNo(CustomerNo, Convert.ToDateTime(Convert.ToString(rd.Cells["CDate"].Value)));
                                    ShipDate = Convert.ToDateTime(Convert.ToString(rd.Cells["CDate"].Value));                                    
                                }

                                if (Convert.ToString(rd.Cells["CustomerNo"].Value).Trim().Equals(CustomerNo))
                                {
                                    //if (Convert.ToBoolean(rd.Cells["PackingFlag"].Value))
                                    //{
                                        countRow1 += 1;
                                        Price = Convert.ToDecimal(db.get_InvoicePRICEVAT_Dynamics(Convert.ToString(rd.Cells["DocuNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value).Trim(), Convert.ToString(rd.Cells["Plant"].Value).Trim(), 0));
                                        VAT = Convert.ToDecimal(db.get_InvoicePRICEVAT_Dynamics(Convert.ToString(rd.Cells["DocuNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value).Trim(), Convert.ToString(rd.Cells["Plant"].Value).Trim(), 1));
                                        tb_InvoiceLocalTempList Cr = new tb_InvoiceLocalTempList();
                                        Cr.InvoiceNo = InvNo;
                                        Cr.InvoiceDate = Convert.ToDateTime(rd.Cells["CDate"].Value.ToString());
                                        Cr.CustomerNo = CustomerNo;
                                        Cr.CustomerName = CustomerName;
                                        Cr.CodeNo = Convert.ToString(rd.Cells["PartNo"].Value).Trim();
                                        Cr.CodeName = Convert.ToString(rd.Cells["PartName"].Value).Trim();
                                        Cr.CodeCustomer = Convert.ToString(rd.Cells["CustomerItemNo"].Value).Trim();
                                      
                                        Cr.Qty = Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value));
                                        Cr.UnitCost = Price;
                                        Cr.Amount = Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value)) * Price;
                                        Cr.Discount = 0;
                                        Cr.Vat = (Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value)) * Price) * 7 / 100; // VAT;
                                        Cr.OrderNo = Convert.ToString(rd.Cells["SaleOrderNo"].Value).Trim();
                                        Cr.Plant = Convert.ToString(rd.Cells["Plant"].Value).Trim();
                                        Cr.UserID = dbClss.UserID;
                                        db.tb_InvoiceLocalTempLists.InsertOnSubmit(Cr);

                                        db.SubmitChanges();
                                    //}
                                }
                            }
                        }
                    }

                    ////Open Dialog//
                    if (countRow1 > 0)
                    {
                        InvoiceLocalCre Cnew = new InvoiceLocalCre(InvNo, Type, ShipDate, CustomerNo, CustomerName);
                        Cnew.ShowDialog();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            LoadData();
        }
        string Type = "A";
        private string GetInvoiceNo(string CustNo,DateTime ShipDate)
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
                  //  tb_InvoiceCustomerSetup gC = db.tb_InvoiceCustomerSetups.Where(c => c.CustomerNo.Equals(CustNo)).FirstOrDefault();
                    var CustomterA = db.sp_043_Inv_LocalCust_Dynamics(CustNo).FirstOrDefault();

                    if(!CustomterA.PType.Equals("EXP"))
                    {
                        Vat = true;
                    }

                    if (Vat)
                    {
                        //UpdateBefore//                     


                        while (CKInv)
                        {
                            runningRow += 1;
                            if(runningRow>20)
                            {
                                CKInv = false;
                            }
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
                            //CKInv = false;
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
                }
                catch { ck = false; }
            }
            return ck;
        }

        private void radGridView1_ViewRowFormatting(object sender, RowFormattingEventArgs e)
        {
            
        }

        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            try
            {
                
                if (CCRow > 5000)
                {
                    CCRow += 1;
                }
                else
                {
                    if (Convert.ToBoolean(e.RowElement.RowInfo.Cells["ShipFlag"].Value).Equals(true))
                    {

                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightGreen;

                    }
                    else if (Convert.ToBoolean(e.RowElement.RowInfo.Cells["PackingFlag"].Value).Equals(true)
                        && Convert.ToBoolean(e.RowElement.RowInfo.Cells["PDACheckFlag"].Value).Equals(true))
                    {

                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.Yellow;

                    }
                    else if (Convert.ToBoolean(e.RowElement.RowInfo.Cells["DocumentFlag"].Value).Equals(true)
                        && Convert.ToBoolean(e.RowElement.RowInfo.Cells["PrintFlag"].Value).Equals(true)
                        && !Convert.ToString(e.RowElement.RowInfo.Cells["InvoiceNo"].Value).Equals("")
                        && Convert.ToBoolean(e.RowElement.RowInfo.Cells["CheckFlag"].Value).Equals(true)
                        )
                    {

                        e.RowElement.DrawFill = true;
                        e.RowElement.GradientStyle = GradientStyles.Solid;
                        e.RowElement.BackColor = Color.LightSkyBlue;

                    }

                    //else if (!e.RowElement.RowInfo.Cells["InvoiceNo"].Value.Equals("") )
                    //{
                    //    //e.RowElement.DrawFill = true;
                    //    //e.RowElement.GradientStyle = GradientStyles.Solid;
                    //    //e.RowElement.BackColor = Color.LightGreen;

                    //}                
                    else
                    {
                        e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    }
                }
            }
            catch { }
        }

        private void เพมInvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการสร้าง Invoice No. เพิ่ม หรือไม่ ?", "Add Inv.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    radGridView1.EndEdit();
                    int CheckRow = 0;
                    string CustomerNo = "";
                    string CustomerName = "";
                    string InvNo = "";
                    decimal VAT = 0;
                    decimal Price = 0;
                    DateTime ShipDate = DateTime.Now;
                    string InvNo2 = "";
                    int countRow1 = 0;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_043_Inv_Local_TempDelete(dbClss.UserID);
                        radGridView1.EndEdit();

                        //Find Invoice No////
                        InvNo2 = Interaction.InputBox("Input Invoice No?", "Invoice No", "");
                        tb_InvoiceLocalHD ihd = db.tb_InvoiceLocalHDs.Where(w => w.InvoiceNo.Equals(InvNo2)).FirstOrDefault();
                        if (ihd != null)
                        {

                            CustomerNo = Convert.ToString(ihd.CustomerNo).Trim();
                            CustomerName = Convert.ToString(ihd.CustomerName);
                            ShipDate = Convert.ToDateTime(ihd.InvoiceDate);

                            foreach (GridViewRowInfo rd in radGridView1.Rows)
                            {
                                if (Convert.ToBoolean(rd.Cells["S"].Value) && Convert.ToString(rd.Cells["InvoiceNo"].Value).Equals(""))
                                {
                                    tb_InvoiceLocalDT dtc = db.tb_InvoiceLocalDTs.Where(d => d.InvoiceNo.Equals(InvNo2)
                                    && d.PartNo.Equals(Convert.ToString(rd.Cells["PartNo"].Value).Trim())
                                    && d.Plant.Equals(Convert.ToString(rd.Cells["Plant"].Value).Trim())
                                    && d.OrderNo.Equals(Convert.ToString(rd.Cells["SaleOrderNo"].Value).Trim())
                                    ).FirstOrDefault();

                                    if (dtc == null)
                                    {
                                        Price = 0;
                                        VAT = 0;
                                        Price = Convert.ToDecimal(db.get_InvoicePRICEVAT_Dynamics(Convert.ToString(rd.Cells["SaleOrderNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value).Trim(), Convert.ToString(rd.Cells["Plant"].Value).Trim(), 0));
                                        VAT = Convert.ToDecimal(db.get_InvoicePRICEVAT_Dynamics(Convert.ToString(rd.Cells["SaleOrderNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value).Trim(), Convert.ToString(rd.Cells["Plant"].Value).Trim(), 1));

                                        countRow1 += 1;
                                        tb_InvoiceLocalDT dta = new tb_InvoiceLocalDT();
                                        dta.SortQ = db.get_InvSeqMax(InvNo2) + 1;
                                        dta.InvoiceNo = InvNo2;
                                        dta.Plant = Convert.ToString(rd.Cells["Plant"].Value).Trim();
                                        dta.PartNo = Convert.ToString(rd.Cells["PartNo"].Value).Trim();
                                        dta.PastCustomer = Convert.ToString(rd.Cells["CustomerItemNo"].Value).Trim();
                                        dta.PartName = Convert.ToString(rd.Cells["PartName"].Value).Trim();
                                        dta.OrderNo = Convert.ToString(rd.Cells["SaleOrderNo"].Value).Trim();
                                        dta.Qty = Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value));
                                        dta.Unit = "PCS";
                                        dta.UnitPrice = Price;
                                        dta.Vat = (Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value)) * Price)*7/100; //VAT;
                                        dta.Amount = Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value)) * Price;
                                        dta.Discount = 0;
                                        dta.SS = 1;
                                        db.tb_InvoiceLocalDTs.InsertOnSubmit(dta);
                                        db.SubmitChanges();

                                        db.sp_043_Inv_LocalTemp_SelectUpdate_Dynamics(InvNo2, Convert.ToString(rd.Cells["SaleOrderNo"].Value).Trim()
                                            , Convert.ToString(rd.Cells["Plant"].Value).Trim()
                                            , Convert.ToString(rd.Cells["PartNo"].Value).Trim()
                                            , ihd.CustomerNo);

                                    }



                                    //if (CustomerNo.Equals(""))
                                    //{
                                    //    CustomerNo = Convert.ToString(rd.Cells["CustomerNo"].Value).Trim();
                                    //    CustomerName = Convert.ToString(rd.Cells["CustomerName"].Value);
                                    //    InvNo = GetInvoiceNo(CustomerNo, Convert.ToDateTime(Convert.ToString(rd.Cells["CDate"].Value)));
                                    //    ShipDate = Convert.ToDateTime(Convert.ToString(rd.Cells["CDate"].Value));
                                    //}
                                    //if (Convert.ToString(rd.Cells["CustomerNo"].Value).Trim().Equals(CustomerNo))
                                    //{
                                    //    //if (Convert.ToBoolean(rd.Cells["PackingFlag"].Value))
                                    //    //{
                                    //    countRow1 += 1;
                                    //    Price = Convert.ToDecimal(db.get_InvoicePRICEVAT(Convert.ToString(rd.Cells["SaleOrderNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value).Trim(), Convert.ToString(rd.Cells["Plant"].Value).Trim(), 0));
                                    //    VAT = Convert.ToDecimal(db.get_InvoicePRICEVAT(Convert.ToString(rd.Cells["SaleOrderNo"].Value), Convert.ToString(rd.Cells["PartNo"].Value).Trim(), Convert.ToString(rd.Cells["Plant"].Value).Trim(), 1));
                                    //    tb_InvoiceLocalTempList Cr = new tb_InvoiceLocalTempList();
                                    //    Cr.InvoiceNo = InvNo2;
                                    //    Cr.InvoiceDate = Convert.ToDateTime(rd.Cells["CDate"].Value.ToString());
                                    //    Cr.CustomerNo = CustomerNo;
                                    //    Cr.CustomerName = CustomerName;
                                    //    Cr.CodeNo = Convert.ToString(rd.Cells["PartNo"].Value).Trim();
                                    //    Cr.CodeName = Convert.ToString(rd.Cells["PartName"].Value).Trim();
                                    //    Cr.CodeCustomer = Convert.ToString(rd.Cells["CustomerItemNo"].Value).Trim();
                                    //    Cr.Qty = Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value));
                                    //    Cr.UnitCost = Price;
                                    //    Cr.Amount = Convert.ToDecimal(Convert.ToString(rd.Cells["OrderQty"].Value)) * Price;
                                    //    Cr.Discount = 0;
                                    //    Cr.Vat = VAT;
                                    //    Cr.OrderNo = Convert.ToString(rd.Cells["SaleOrderNo"].Value).Trim();
                                    //    Cr.Plant = Convert.ToString(rd.Cells["Plant"].Value).Trim();
                                    //    Cr.UserID = dbClss.UserID;
                                    //    db.tb_InvoiceLocalTempLists.InsertOnSubmit(Cr);
                                    //    db.SubmitChanges();
                                    //    //}
                                    //}
                                }
                            }
                        }//  ihd //
                    }

                    //Open Dialog//
                    if (countRow1 > 0)
                    {
                        MessageBox.Show("Insert Completed.");
                        InvoiceLocalShow ivsh = new InvoiceLocalShow(InvNo2);
                        ivsh.ShowDialog();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            LoadData();
        }

        private void แกไขวนทShippingDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("แก้ไขวันที่ Shipping Date หรือไม่ ?", "Edit Shipping Date", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //
                if (Row >= 0)
                {
                    string SHD = "";
                    string SO = radGridView1.Rows[Row].Cells["SaleOrderNo"].Value.ToString();
                    string PL = radGridView1.Rows[Row].Cells["Plant"].Value.ToString();
                    string PartNo = radGridView1.Rows[Row].Cells["PartNo"].Value.ToString();

                    SHD = Interaction.InputBox("Input Shipping Date? format = dd/MM/yyyy", "Shipping Date", "");
                    DateTime DT1 = DateTime.Now;
                    DateTime.TryParse(SHD, out DT1);

                    if (MessageBox.Show("เลือกวันที่นี้ -> " + DT1.ToString("dd/MM/yyyy"), " ใช่วันที่นี้หรือไม่ ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_LocalListDeliverly01 Sr = db.tb_LocalListDeliverly01s.Where(s => s.SaleOrder.Equals(SO) && s.PartNo.Equals(PartNo) && s.Plant.Equals(PL)).FirstOrDefault();
                            if (Sr != null)
                            {
                                Sr.ShippingDate = DT1;
                                db.SubmitChanges();
                                MessageBox.Show("Update Completed.");

                            }

                        }
                    }
                }
            }
        }

        private void historyGuideLotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Row >= 0)
                {
                    GuideLineLotHistory hl = new GuideLineLotHistory(Convert.ToString(radGridView1.Rows[Row].Cells["SaleOrderNo"].Value), Convert.ToString(radGridView1.Rows[Row].Cells["PartNo"].Value)
                        , Convert.ToString(radGridView1.Rows[Row].Cells["Plant"].Value));
                    hl.ShowDialog();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButtonElement14_Click(object sender, EventArgs e)
        {
            LocationTAGCheckIV ck1 = new LocationTAGCheckIV("");
            ck1.Show();
        }

        private void editPlantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string OrderNo = Convert.ToString(radGridView1.Rows[Row].Cells["SaleOrderNo"].Value);
            string PartNo = Convert.ToString(radGridView1.Rows[Row].Cells["PartNo"].Value);
            if(!OrderNo.Equals("") && !PartNo.Equals(""))
            {
                try
                {
                    PlantEdit pe = new PlantEdit(OrderNo, PartNo, "");
                    pe.ShowDialog();
                    LoadData();
                }
                catch { }
            }
        }
    }
}
