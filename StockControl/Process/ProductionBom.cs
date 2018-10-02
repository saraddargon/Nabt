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
using CrystalDecisions.Shared;
using System.Runtime.InteropServices;
namespace StockControl
{
    public partial class ProductionBom : Telerik.WinControls.UI.RadRibbonForm
    {
        public ProductionBom()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                // Alt+F pressed
                //  ClearData();

                return false;
                //txtSeriesNo.Focus();
            }
            else if (keyData == (Keys.F8))
            {
                NewClick();
            }
            else if (keyData == (Keys.F9))
            {
                ReceiveClick();
            }
            else if (keyData == (Keys.F5))
            {
                NewClick();
            }
            else if (keyData == (Keys.F10))
            {
                ReceiveClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
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
            // RMenu3.Click += RMenu3_Click;
            //RMenu4.Click += RMenu4_Click;
            // RMenu5.Click += RMenu5_Click;
            // RMenu6.Click += RMenu6_Click;
            //radGridView1.ReadOnly = true;
            // radGridView1.AutoGenerateColumns = false;
            //  GETDTRow();   
            // DataLoad();
            DefaultLoad();
        }
        private void DefaultLoad()
        {

        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
            //DeleteUnit();
          //  DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
          //  EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
          //  ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
           // NewClick();

        }

        private void DataLoad()
        {
            
            int ck = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {

               // radGridView1.DataSource = db.tb_LocationlWHs.ToList();
                //foreach(var x in radGridView1.Rows)
                //{

                //    x.Cells["dgvCodeTemp"].Value = x.Cells["UnitCode"].Value.ToString();
                //    x.Cells["UnitCode"].ReadOnly = true;
                //    if(row>=0 && row==ck && radGridView1.Rows.Count > 0)
                //    {

                //        x.ViewInfo.CurrentRow = x;
                        
                //    }
                //    ck += 1;
                //}
               
            }


            
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            //using (DataClasses1DataContext db = new DataClasses1DataContext())
            //{
            //    int i = (from ix in db.tb_Units where ix.UnitCode == code select ix).Count();
            //    if (i > 0)
            //        ck = false;
            //    else
            //        ck = true;
            //}
            return ck;
        }

        private bool AddUnit()
        {
            bool ck = false;
          

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;
         

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void NewClick()
        {
            ClearRec();
            radPageView1.SelectedPage = radPageViewPage2;
            txtOrderNo.Text = "";
            txtOrderNo.Focus();
        }
        private void EditClick()
        {
          
        }
        private void ViewClick()
        {
            getWO(txtOrderNo.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
            //ClearRec();
            //radPageView1.SelectedPage = radPageViewPage2;
            //txtOrderNo.Text = "";
            //txtOrderNo.Focus();
        }
        private void ClearRec()
        {
            txtOrderNo.Text = "";
            txtOrderDate.Text = "";
            txtQuantity.Text = "";
            txtReqDte.Text = "";
            txtScan.Text = "";
            txtScanPO.Text = "";
            txtCustomer.Text = "";
            txtPartName.Text = "";
            txtReceived.Text = "";
            txtPartNo.Text = "";
            txtCustomerItem.Text = "";
            txtSNP.Text = "";
            txtWorkCenter.Text = "";
            txtLotNo.Text = "";
            txtQtyofTAG.Text = "";
            txtWorkName.Text = "";
            chkPrinted.Checked = false;
            chkCheckPart.Checked = false;
            chkClose.Checked = false;
            chkClosed.Checked = false;
            dtNdate.Value = DateTime.Now;
            
            radGridView1.DataSource = null;
            radGridView2.DataSource = null;

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AddUnit();
                DataLoad();
            }
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                
        

            }
            catch(Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
            //else if (e.KeyData == (Keys.Control | Keys.N))
            //{
            //    if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        NewClick();
            //    }
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
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
            // dbClss.ExportGridXlSX(radGridView1);
            if (chkCheckPart.Checked)
            {
                PrintRW pr = new PrintRW(txtOrderNo.Text);
                pr.ShowDialog();
            }
            else
            {
                MessageBox.Show("ยังเช็คพาร์สไม่ครบ");
            }


        }


        private void ImportData()
        {
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
                   
            //        foreach (DataRow rd in dt.Rows)
            //        {
            //            if (!rd["UnitCode"].ToString().Equals(""))
            //            {

            //                var x = (from ix in db.tb_Units where ix.UnitCode.ToLower().Trim() == rd["UnitCode"].ToString().ToLower().Trim() select ix).FirstOrDefault();

            //                if(x==null)
            //                {
            //                    tb_Unit ts = new tb_Unit();
            //                    ts.UnitCode = Convert.ToString(rd["UnitCode"].ToString());
            //                    ts.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
            //                    ts.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
            //                    db.tb_Units.InsertOnSubmit(ts);
            //                    db.SubmitChanges();
            //                }
            //                else
            //                {
            //                    x.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
            //                    x.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
            //                    db.SubmitChanges();

            //                }

                       
            //            }
            //        }
                   
            //    }
            //}
            //catch(Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("InportData", ex.Message, this.Name);
            //}
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //ReceiveCheck rc = new ReceiveCheck();
            //rc.ShowDialog();
        }

        private void ProductionBom_Load(object sender, EventArgs e)
        {

        }

        private void ReceiveClick()
        {
            radPageView1.SelectedPage = radPageViewPage1;
            ReceiveData();
            txtScanPO.Text = "";
            txtScanPO.Focus();
        }
        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            ReceiveClick();

        }
        private void ReceiveData()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView1.AutoGenerateColumns = false;
                    radGridView1.DataSource = null;
                    radGridView1.DataSource = db.tb_ProductionReceives.Where(p => p.OrderNo.ToLower() == txtOrderNo.Text.ToLower()).ToList();
                    int ck = 0;
                    decimal qty = 0;
                    decimal OrderQty = 0;
                    decimal SumQty = 0;
                    decimal SumRemain = 0;
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                        ck += 1;
                        rd.Cells["No"].Value = ck;

                        decimal.TryParse(rd.Cells["Qty"].Value.ToString(), out qty);
                        decimal.TryParse(rd.Cells["SNP"].Value.ToString(), out OrderQty);
                        SumQty += qty;
                        SumRemain = OrderQty;
                    }
                    decimal.TryParse(txtQuantity.Text, out OrderQty);
                    txtOrderqty1.Text = txtQuantity.Text;// SumRemain.ToString("###,###,##0");
                    txtTotalQty1.Text = SumQty.ToString("###,###,##0");
                    if(OrderQty==SumQty)
                    {
                        if (SumQty > 0 && OrderQty > 0)
                        {
                            //Closed Production HD//
                            tb_ProductionHD ph = db.tb_ProductionHDs.Where(p => p.OrderNo.ToLower() == txtOrderNo.Text.ToLower() && p.CheckOK==true && p.Closed==false).FirstOrDefault();
                            if (ph != null)
                            {
                                ph.Closed = true;
                                ph.CreateBy = dbClss.UserID;
                                ph.CreateDate = DateTime.Now;
                                db.SubmitChanges();
                                chkClose.Checked = true;
                                chkClosed.Checked = true;
                            }
                        }

                    }
                    
                    

                }
            }
            catch { }
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                getWO(txtOrderNo.Text);
            }
        }

        private void getWO(string WO)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (!WO.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int a = 0;
                        double ap = 0;
                        var getwo = db.sp_003_TPIC_GETBOMNo(WO,1).ToList();
                        if (getwo.Count > 0)
                        {
                            var rd = getwo.FirstOrDefault();
                            txtOrderNo.Text = WO;
                            txtOrderDate.Text = Convert.ToDateTime(rd.DeliveryDate).ToString("dd/MM/yyyy");
                            txtQuantity.Text = Convert.ToDecimal(rd.OrderQty).ToString("###,###,##0");
                            txtReceived.Text = Convert.ToDecimal(rd.TotalResults).ToString("###,###,##0");
                            txtReqDte.Text = Convert.ToDateTime(rd.DeliveryDate).ToString("dd/MM/yyyy");
                            dtDate.Value = Convert.ToDateTime(rd.CreateDate);
                            dtNdate.Value = Convert.ToDateTime(rd.DeliveryDate);
                            //chkClosed.Checked=Convert.ToBoolean(rd.)


                            txtCustomer.Text = rd.CustomerNo;
                            txtCustomerItem.Text =rd.CustItemNo;
                            if(txtCustomer.Text.Equals(""))
                            {
                                txtCustomer.Text = rd.BUNR;
                                txtCustomerItem.Text = rd.BUNR;
                            }
                            txtPartName.Text = rd.NAME.ToString();
                            txtPartNo.Text = rd.CODE.ToString();                          
                            txtSNP.Text = Convert.ToDecimal(rd.LotSize).ToString("###,###,##0");
                            txtWorkCenter.Text = rd.BUMO.ToString();
                            txtWorkName.Text = rd.BUMOName.ToString();

                            txtLotNo.Text = rd.LotNo.ToString();
                            chkPrinted.Checked = false;
                            chkCheckPart.Checked = false;
                            radGridView2.DataSource = null;

                            if (!txtSNP.Text.Equals("0") && !txtSNP.Text.Equals(""))
                            {
                                a = 0;
                                ap = (Convert.ToDouble(rd.OrderQty) % Convert.ToDouble(rd.LotSize));
                                if (ap > 0)
                                    a = 1;
                                txtQtyofTAG.Text = Convert.ToInt32(Math.Floor((Convert.ToDouble(txtQuantity.Text) / Convert.ToDouble(txtSNP.Text)) + a)).ToString();//.ToString("###");
                            }

                            tb_ProductionHD ph = db.tb_ProductionHDs.Where(p => p.OrderNo == txtOrderNo.Text).FirstOrDefault();
                            if(ph!=null)
                            {
                                chkCheckPart.Checked = Convert.ToBoolean(ph.CheckOK);
                                chkPrinted.Checked= Convert.ToBoolean(ph.OrderPrint);
                                chkClosed.Checked = Convert.ToBoolean(ph.Closed);
                                chkClose.Checked= Convert.ToBoolean(ph.Closed); 
                            }



                            //Insert///
                           // string WIP = "";
                            var getbom = (from ix in db.sp_TPICS_BOMList(txtOrderNo.Text.ToUpper()) select ix).ToList();
                            if(getbom.Count>0)
                            {
                                tb_ProductionHD pha = db.tb_ProductionHDs.Where(p => p.OrderNo == txtOrderNo.Text).FirstOrDefault();
                                if(pha!=null)
                                {
                                    
                                }else
                                {
                                    tb_ProductionHD ph1 = new tb_ProductionHD();
                                    ph1.OrderNo = txtOrderNo.Text;
                                    ph1.OrderPrint = false;
                                    ph1.CheckOK = false;
                                    ph1.PartFG = txtPartNo.Text;
                                    ph1.Qty = Convert.ToDecimal(rd.OrderQty);
                                    ph1.Status = "Process";
                                    ph1.CreateBy = dbClss.UserID;
                                    ph1.CreateDate = DateTime.Now;
                                    db.tb_ProductionHDs.InsertOnSubmit(ph1);
                                    db.SubmitChanges();
                                }
                                
                                foreach(var rdx in getbom)
                                {
                                    tb_ProductionRM pr = db.tb_ProductionRMs.Where(p => p.OrderNo == txtOrderNo.Text && p.PartNoRM.ToLower() == rdx.CODE.ToLower()).FirstOrDefault();
                                    if(pr!=null)
                                    {

                                    }
                                    else
                                    {
                                        decimal Qty = 0;
                                        decimal.TryParse(txtQuantity.Text, out Qty);
                                        if (Qty > 0)
                                        {
                                            tb_ProductionRM rm = new tb_ProductionRM();
                                            rm.OrderNo = txtOrderNo.Text.ToUpper();
                                            rm.PartNoRM = rdx.CODE;
                                            rm.Supplier = rdx.BUMONAME.ToString();
                                            rm.PartType = rdx.BUMO;
                                            rm.UseQty = Convert.ToDecimal(rdx.KVOL) / Qty;
                                            rm.TotalUse = Convert.ToDecimal(rdx.KVOL);
                                            rm.Shelf = rdx.SHELVES;
                                            rm.PartName = rdx.NAME;
                                            rm.CheckOK = "";
                                            rm.CheckSkip = false;
                                            db.tb_ProductionRMs.InsertOnSubmit(rm);
                                            db.SubmitChanges();
                                        }
                                    }
                                }
                            }

                        }
                    }
                    LoadBOMList();
                    txtScan.Text = "";
                    txtScan.Focus();
                }
            }
            catch(Exception ex) { this.Cursor = Cursors.Default;  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
        }

        private void LoadBOMList()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView2.AutoGenerateColumns = false;
                    radGridView2.DataSource = db.tb_ProductionRMs.Where(r => r.OrderNo == txtOrderNo.Text).ToList();
                    if(radGridView2.Rows.Count>0)
                    {
                        int ck = 0;
                        int ck2 = 1;
                        foreach (GridViewRowInfo rd in radGridView2.Rows)
                        {
                            ck += 1;
                            rd.Cells["No"].Value = ck;
                            if(rd.Cells["CheckOK"].Value.Equals(""))
                            {
                                ck2 = 0;
                            }
                        }
                        if(ck2==1)
                        {
                            chkCheckPart.Checked = true;
                            tb_ProductionHD ph = db.tb_ProductionHDs.Where(w => w.OrderNo == txtOrderNo.Text).FirstOrDefault();
                            if(ph!=null)
                            {
                                ph.CheckOK = true;
                                db.SubmitChanges();
                            }
                        }
                    }

                }
            }
            catch { }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (chkCheckPart.Checked)
            {
                PirntTAGA("1111");
            }else
            {
                MessageBox.Show("ยังเช็คพาร์สไม่ครบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PirntTAGA(string AAA)
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
                int.TryParse(txtQuantity.Text, out Qty);
                int.TryParse(txtSNP.Text, out snp);
                OrderQty = Qty;

                string OfTAG = "";
                string QrCode = "";

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
                        tb_ItemList il = db.tb_ItemLists.Where(i => i.CodeNo == txtPartNo.Text).FirstOrDefault();
                        if (il != null)
                        {
                            ImageName = il.PathImage;
                        }


                        var tm = db.tb_ProductTAGs.Where(t => t.UserID.ToLower() == dbClss.UserID.ToLower()).ToList();
                        if (tm.Count > 0)
                        {
                            db.tb_ProductTAGs.DeleteAllOnSubmit(tm);
                            db.SubmitChanges();
                        }

                        for (int i = 1; i <= TAG; i++)
                        {
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
                            QrCode = "PD," + txtOrderNo.Text + "," + Qty + "," + OrderQty + "," + txtLotNo.Text + "," + OfTAG + "," + txtPartNo.Text + "," + dtNdate.Value.ToString("ddMMyy");
                            //MessageBox.Show(QrCode);
                            byte[] barcode = dbClss.SaveQRCode2D(QrCode);

                            ///////////////////////////////
                            tb_ProductTAG ts = new tb_ProductTAG();
                            ts.UserID = dbClss.UserID;
                            ts.BOMNo = txtOrderNo.Text;
                            ts.LotNo = txtLotNo.Text;
                            // ts. = dtDate1.Value.ToString("dd/MM/yyyy");
                            ts.QRCode = barcode;
                            ts.PartName = txtPartName.Text;
                            ts.PartNo = txtPartNo.Text;
                            ts.Machine = Environment.MachineName;
                            ts.OFTAG = i + "/" + TAG;
                            if (!ImageName.Equals(""))
                                ts.PathPic = ImagePath + ImageName;
                            else
                                ts.PathPic = "";

                            ts.Qty = Qty;
                            ts.Seq = i;
                            ts.CSTMShot = txtCustomer.Text;
                            ts.CustomerName = "Nabtesco Automotive Products(Thailand) Co.,Ltd.";
                            ts.CSTMItem = txtCustomerItem.Text;
                            ts.CustItem2 = txtCustomerItem.Text;
                            ts.SHIFT = "";// txtShift.Text;

                            //// ลูกค้า ISUSU  ///
                            if (txtCustomer.Text.Trim().ToUpper().Equals("ISUZU"))
                            {
                                ts.CSTMItem = "A" + txtCustomerItem.Text;// + "" +dtDate.Value.Year.ToString();
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

                        tb_ProductionHD pha = db.tb_ProductionHDs.Where(p => p.OrderNo == txtOrderNo.Text).FirstOrDefault();
                        if(pha!=null)
                        {
                            pha.OrderPrint = true;
                            db.SubmitChanges();
                            chkPrinted.Checked = true;
                        }

                    }
                    if (AAA.Equals("1112"))
                    {
                        string DATA = "";
                        DATA = AppDomain.CurrentDomain.BaseDirectory;
                        DATA = DATA + @"Report\FG_TAG.rpt";
                        PrintDialog printPrompt = new PrintDialog();
                        printPrompt.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
                        printPrompt.AllowSomePages = true;
                        PageMargins margin = new PageMargins();
                        PrintLayoutSettings pl = new PrintLayoutSettings();
                        pl.Scaling = PrintLayoutSettings.PrintScaling.DoNotScale;
                        CrystalDecisions.CrystalReports.Engine.ReportDocument reportx3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        reportx3.Load(DATA);
                        margin = reportx3.PrintOptions.PageMargins;
                        margin.leftMargin = 0;
                        margin.rightMargin = 0;
                        margin.topMargin = 0;
                        margin.bottomMargin = 0;
                        Report.Reportx1.SetDataSourceConnection(reportx3);


                        reportx3.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                        reportx3.PrintOptions.ApplyPageMargins(margin);
                        reportx3.SetParameterValue("@BomNo", txtPartNo.Text);
                        reportx3.SetParameterValue("@USERID", dbClss.UserID);
                        reportx3.SetParameterValue("@Datex", DateTime.Now);                      
                        reportx3.PrintToPrinter(printPrompt.PrinterSettings, printPrompt.PrinterSettings.DefaultPageSettings, false, pl);
                    }
                    else
                    {
                        Report.Reportx1.WReport = "PDTAG";
                        Report.Reportx1.Value = new string[3];
                        Report.Reportx1.Value[0] = txtOrderNo.Text;
                        Report.Reportx1.Value[1] = dbClss.UserID;
                        // Report.Reportx1.Value[2] = txtAC.Text;
                        if (AAA.Equals("222"))
                        {
                            Report.Reportx1 op = new Report.Reportx1("FG_TAG_RM.rpt");
                            op.Show();
                        }
                        else
                        {
                            Report.Reportx1 op = new Report.Reportx1("FG_TAG.rpt");
                            op.Show();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Qty invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
        }

        private void btnUpdateSkip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการอัพเดต หรือไม่ ?", "อัพเดต Skip", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        if(!txtOrderNo.Text.Equals(""))
                        {
                            radGridView2.EndUpdate();
                            radGridView2.EndEdit();
                            int id = 0;
                            foreach(GridViewRowInfo rd in radGridView2.Rows)
                            {
                                id = 0;
                                if(Convert.ToBoolean(rd.Cells["SKIP"].Value))
                                {
                                    int.TryParse(rd.Cells["id"].Value.ToString(), out id);
                                    if(id>0)
                                    {
                                        tb_ProductionRM re = db.tb_ProductionRMs.Where(r => r.id == id).FirstOrDefault();
                                        if(re!=null)
                                        {
                                            rd.Cells["CheckOK"].Value = "OK";
                                            re.CheckOK = "OK";
                                            re.CheckSkip = true;
                                            db.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            LoadBOMList();
                        }
                    }
                }
                catch { }
            }
        }

        private void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                ScanPartCheck(txtScan.Text.Trim());
                if(chkCheckPart.Checked && !chkPrinted.Checked && chkPrintAuto.Checked)
                {
                    //Print Auto
                    //PirntTAGA("1112");
                }
            }
        }
        private void ScanPartCheck(string SCAN)
        {
            try
            {
                //SP,PO17228088,46,46,1891T,1of5,41241038010N1,17102017
                string[] wk = SCAN.Split(',');
                string PartCheck = "";
                if (wk.Length == 1)
                {
                    PartCheck = wk[0];
                }
                else if (wk.Length > 3)
                {
                    PartCheck = wk[6];
                }
                else
                {
                    PartCheck = SCAN;
                }

                int c = 0;
                int id = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (GridViewRowInfo rd in radGridView2.Rows)
                    {
                        id = 0;
                        if (rd.Cells["PartNoRM"].Value.ToString().Equals(PartCheck))
                        {
                            c += 1;
                            if (!rd.Cells["CheckOK"].Value.Equals("OK"))
                            {
                                
                                int.TryParse(rd.Cells["id"].Value.ToString(), out id);
                                if (id > 0)
                                {
                                    tb_ProductionRM re = db.tb_ProductionRMs.Where(r => r.id == id).FirstOrDefault();
                                    if (re != null)
                                    {
                                        rd.Cells["CheckOK"].Value = "OK";
                                        re.CheckOK = "OK";                                       
                                        db.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                if (c > 0)
                {
                    LoadBOMList();
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                    player.Play();
                }
                else
                {
                    // System.Media.SystemSounds.Beep.Play();
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory+ @"\beep-05.wav");
                    player.Play();
                }
                txtScan.Text = "";
                txtScan.Focus();


            }
            catch { }
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการลบหรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (row2 >= 0)
                    {
                        int id = 0;
                        int.TryParse(radGridView2.Rows[row2].Cells["id"].Value.ToString(), out id);
                        if (id > 0)
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                tb_ProductionRM rm = db.tb_ProductionRMs.Where(p => p.id == id).FirstOrDefault();
                                if(rm!=null)
                                {
                                    db.tb_ProductionRMs.DeleteOnSubmit(rm);
                                    db.SubmitChanges();
                                    LoadBOMList();
                                }
                            }
                        }
                    }
                }
                catch { }
            }

        }

        int row2 = 0;
        private void radGridView2_CellClick(object sender, GridViewCellEventArgs e)
        {
            row2 = e.RowIndex;
        }

        private void txtScanPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ReceivePD(txtScanPO.Text.Trim());
            }
                    
        }
        private void ReceivePD(string PKTAG)
        {
            try
            {
                //PD,PO17228088,46,46,1891T,1of5,41241038010N1,17102017
                string[]wk = PKTAG.Split(',');
                if(wk.Length>7)
                {
                    decimal Qty = 0;
                    decimal OrderQty = 0;
                    decimal.TryParse(wk[2], out Qty);
                    decimal.TryParse(wk[3], out OrderQty);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ProductionHD ph = db.tb_ProductionHDs.Where(p => p.OrderNo.ToLower() == wk[1].ToLower() && p.Closed == true).FirstOrDefault();
                        if (ph != null)
                        {

                        }
                        else
                        {

                            tb_ProductionReceive rm = db.tb_ProductionReceives.Where(p => p.PKTAG == PKTAG).FirstOrDefault();
                            if (rm != null)
                            {

                            }
                            else
                            {
                                if (wk[6].ToLower().Equals(txtPartNo.Text.ToLower())
                                    && wk[1].ToLower().Equals(txtOrderNo.Text.Trim().ToLower()))
                                {
                                    tb_ProductionReceive rd = new tb_ProductionReceive();
                                    rd.CreateBy = dbClss.UserID;
                                    rd.CreateDate = DateTime.Now;
                                    rd.DateCreate = wk[7];
                                    rd.LotNo = wk[4];
                                    rd.OfTAG = wk[5];
                                    rd.OrderNo = wk[1];
                                    rd.PartNo = wk[6];
                                    rd.PKTAG = PKTAG;
                                    rd.Qty = Qty;
                                    rd.SNP = OrderQty;
                                    rd.PartName = "";
                                    rd.Status = "Waiting";
                                    db.tb_ProductionReceives.InsertOnSubmit(rd);
                                    db.SubmitChanges();

                                    ReceiveData();
                                }

                            }
                        }
                    }
                }
                   
            }
            catch { }
            txtScanPO.Text = "";
            txtScanPO.Focus();
        }

        private void ลบรายการรบToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการลบหรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (row1 > 0)
                {
                    try
                    {
                        int id = 0;
                        int.TryParse(radGridView1.Rows[row1].Cells["id"].Value.ToString(), out id);
                        if (id > 0)
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                tb_ProductionReceive rm = db.tb_ProductionReceives.Where(p => p.id == id && !p.Status.Equals("Completed")).FirstOrDefault();
                                if (rm != null)
                                {
                                    db.tb_ProductionReceives.DeleteOnSubmit(rm);
                                    db.SubmitChanges();
                                    tb_ProductionHD ph = db.tb_ProductionHDs.Where(p => p.OrderNo.ToLower() == txtOrderNo.Text.ToLower() && p.CheckOK == true && p.Closed==true).FirstOrDefault();
                                    if (ph != null)
                                    {
                                        ph.Closed = false;
                                        ph.CreateBy = dbClss.UserID;
                                        ph.CreateDate = DateTime.Now;
                                        db.SubmitChanges();
                                        chkClose.Checked = false;
                                        chkClosed.Checked = false;
                                    }

                                    ReceiveData();
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
        }
        int row1 = 0;
        private void radGridView1_CellClick_1(object sender, GridViewCellEventArgs e)
        {
            row1 = e.RowIndex;
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {
                
               // MessageBox.Show(radPageView1.SelectedPage.Name.ToString());
               if(radPageView1.SelectedPage.Name.ToString().Equals("radPageViewPage1"))
                {
                    ReceiveData();
                }
                   
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getWO(txtOrderNo.Text);
        }

        private void btnCloseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการ ปิด Order หรือไม่ ? \n จะไม่สามารถรับได้อีก", "ปิดรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        decimal qty = 0;
                        decimal orderqty = 0;
                        decimal.TryParse(txtTotalQty1.Text, out qty);
                        decimal.TryParse(txtOrderqty1.Text, out orderqty);
                        if (qty > 0 && orderqty > 0)
                        {
                            //Closed Production HD//
                            tb_ProductionHD ph = db.tb_ProductionHDs.Where(p => p.OrderNo.ToLower() == txtOrderNo.Text.ToLower() && p.CheckOK==true && p.Closed==false).FirstOrDefault();
                            if (ph != null)
                            {
                                ph.Closed = true;
                                ph.CreateBy = dbClss.UserID;
                                ph.CreateDate = DateTime.Now;
                                db.SubmitChanges();
                                chkClose.Checked = true;
                                chkClosed.Checked = true;
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
