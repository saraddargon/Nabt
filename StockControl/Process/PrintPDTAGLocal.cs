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
    public partial class PrintPDTAGLocal : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;

        public PrintPDTAGLocal(string Code,string PartNox,string Plantx)
        {
            this.Name = "PrintTEMPTAG";
            InitializeComponent();            
            txtBomNo.Text = Code;
            PartNo = PartNox;
            Plant = Plantx;
        }
        int idx = 0;
        string PartNo = "";
        string Plant = "";
       // TextBox Lot;
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
           
        }
        private void Unit_Load(object sender, EventArgs e)
        {

            dtDate1.Value = DateTime.Now;
            if(!txtBomNo.Text.Equals(""))
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    idx = Convert.ToInt32(db.g_getID_SalesOrder(txtBomNo.Text, PartNo, Plant));
                }
                    LoadBomNo();
            }
        

        }
        private void LoadBomNo()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int ac = 1;
                    int.TryParse(txtAC.Text, out ac);
                    

                    var getCode = (from ix in db.sp_003_TPIC_GETBOMNo_DynamicsPIX(idx) select ix).ToList();
                    if (getCode.Count > 0)
                    {
                        var rd = getCode.FirstOrDefault();
                        dtDate1.Value = DateTime.Now;
                        txtPartName.Text = rd.ItemName.ToString();
                        txtPartNo.Text = rd.PartNo.ToString();
                        txtCustItemName.Text = rd.CCODE.ToString();
                        txtCustItemNo.Text = rd.CCODE.ToString();
                        txtCustomerName.Text = rd.NaptName.ToString();
                        txtCustomerShortName.Text = rd.CShortName.ToString();                        
                        txtsNP.Text = Convert.ToInt32(rd.SNP).ToString();
                        txtShift.Text = ""; 
                        txtLotNo.Text = "";
                        txtQty.Text = Convert.ToInt32(rd.Qty).ToString("#####0");
                        txtLotNo.Text = "";

                        
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            PirntTAGA("1111");
            //Print TAG//
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    int Qty = 0;
            //    int snp = 1;
            //    int TAG = 0;
            //    int a = 0;
            //    double ap = 0;
            //    int Remain = 0;
            //    int.TryParse(txtQty.Text, out Qty);
            //    int.TryParse(txtsNP.Text, out snp);

            //    string OfTAG = "";
            //    string QrCode = "";

            //    if (Qty > 0)
            //    {
            //       // string TMNo = dbClss.GetSeriesNo(2, 2);
            //        if (Qty != 0 && snp != 0)
            //        {
            //            a = 0;
            //            ap = (Qty % snp);
            //            if (ap > 0)
            //                a = 1;
            //            TAG = Convert.ToInt32(Math.Floor((Convert.ToDouble(Qty) / Convert.ToDouble(snp)) + a));//.ToString("###");

            //            //txtOftag.Text = Math.Ceiling((double)1.7 / 10).ToString("###");

            //            Remain = Qty;
            //        }
            //        int C = 0;
            //        string ImagePath = "";
            //        string ImageName = "";
            //        using (DataClasses1DataContext db = new DataClasses1DataContext())
            //        {
            //            tb_Path ph = db.tb_Paths.Where(p => p.PathCode == "Image").First();
            //            if(ph!=null)
            //            {
            //                ImagePath = ph.PathFile;
            //            }
            //            tb_ItemList il = db.tb_ItemLists.Where(i => i.CodeNo == txtPartNo.Text).FirstOrDefault();
            //            if(il!=null)
            //            {
            //                ImageName = il.PathImage;
            //            }


            //            var tm = db.tb_ProductTAGs.Where(t => t.UserID.ToLower() == dbClss.UserID.ToLower()).ToList();
            //            if (tm.Count > 0)
            //            {
            //                db.tb_ProductTAGs.DeleteAllOnSubmit(tm);
            //                db.SubmitChanges();
            //            }

            //            for (int i = 1; i <= TAG; i++)
            //            {
            //                OfTAG = "";
            //                QrCode = "";
            //                if (Remain > snp)
            //                {
            //                    Qty = snp;
            //                    Remain = Remain - snp;
            //                }
            //                else
            //                {
            //                    Qty = Remain;
            //                    Remain = 0;
            //                }
            //                OfTAG = i + "of" + TAG;
            //                QrCode = "";
            //                QrCode = "PD," + txtBomNo.Text + "," + Qty + "," + snp + "," + txtLotNo.Text + "," + OfTAG + "," + txtPartNo.Text;
            //                //MessageBox.Show(QrCode);
            //                byte[] barcode = dbClss.SaveQRCode2D(QrCode);

            //                ///////////////////////////////
            //                tb_ProductTAG ts = new tb_ProductTAG();
            //                ts.UserID = dbClss.UserID;
            //                ts.BOMNo = txtBomNo.Text;
            //                ts.LotNo = txtLotNo.Text;
            //               // ts. = dtDate1.Value.ToString("dd/MM/yyyy");
            //                ts.QRCode = barcode;
            //                ts.PartName = txtPartName.Text;
            //                ts.PartNo = txtPartNo.Text;
            //                ts.Machine = Environment.MachineName;
            //                ts.OFTAG= i + "/" + TAG;
            //                if (!ImageName.Equals(""))
            //                    ts.PathPic = ImagePath + ImageName;
            //                else
            //                    ts.PathPic = "";
            //                ts.Qty = Qty;
            //                ts.Seq = i;
            //                ts.CSTMShot = txtCustomerShortName.Text;
            //                ts.CustomerName = txtCustomerName.Text;
            //                ts.CSTMItem = txtCustItemNo.Text;
            //                ts.CustItem2 = txtCustItemName.Text;

            //                //ts.s = snp;
            //                // ts.Company = "Nabtesco Autmotive Corporation";
            //                //ts.Quantity = Qty;
            //                // ts.OfTAG = i + " / " + TAG;
            //                ///////////////////////////////////////////////
            //                db.tb_ProductTAGs.InsertOnSubmit(ts);
            //                db.SubmitChanges();
            //                C += 1;
            //            }

            //        }
            //        if (txtAC.Text == "")
            //            txtAC.Text = "1";
            //        Report.Reportx1.WReport = "PDTAG";
            //        Report.Reportx1.Value = new string[3];
            //        Report.Reportx1.Value[0] = txtBomNo.Text;
            //        Report.Reportx1.Value[1] = dbClss.UserID;
            //       // Report.Reportx1.Value[2] = txtAC.Text;

            //        Report.Reportx1 op = new Report.Reportx1("FG_TAG.rpt");
            //        op.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Qty invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}
            //catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            //this.Cursor = Cursors.Default;
        }

        private void txtBomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                LoadBomNo();
            }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            PirntTAGA("222");
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
                int.TryParse(txtQty.Text, out Qty);
                int.TryParse(txtsNP.Text, out snp);
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
                            QrCode = "PD," + txtBomNo.Text + "," + Qty + "," + OrderQty + "," + txtLotNo.Text + "," + OfTAG + "," + txtPartNo.Text + "," + dtDate1.Value.ToString("ddMMyy");
                            //MessageBox.Show(QrCode);
                            byte[] barcode = dbClss.SaveQRCode2D(QrCode);

                            ///////////////////////////////
                            tb_ProductTAG ts = new tb_ProductTAG();
                            ts.UserID = dbClss.UserID;
                            ts.BOMNo = txtBomNo.Text;
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
                            ts.CSTMShot = txtCustomerShortName.Text;
                            ts.CustomerName = txtCustomerName.Text;
                            ts.CSTMItem = txtCustItemNo.Text;
                            ts.CustItem2 = txtCustItemName.Text;
                            ts.SHIFT = "";// txtShift.Text;

                            //// ลูกค้า ISUSU  ///
                            if (txtCustomerShortName.Text.Trim().Equals("ISUZU"))
                            {
                                ts.CSTMItem = "A" + txtCustItemNo.Text;// + ""+dtDate1.Value.Year.ToString();
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
                    if (txtAC.Text == "")
                        txtAC.Text = "1";
                    Report.Reportx1.WReport = "PDTAG";
                    Report.Reportx1.Value = new string[3];
                    Report.Reportx1.Value[0] = txtBomNo.Text;
                    Report.Reportx1.Value[1] = dbClss.UserID;
                    // Report.Reportx1.Value[2] = txtAC.Text;
                    if (AAA.Equals("222"))
                    {
                        Report.Reportx1 op = new Report.Reportx1("FG_TAG_RM.rpt");
                        op.Show();
                    }
                    else
                    {
                        Report.Reportx1 op = new Report.Reportx1("FG_TAGLocal.rpt");
                        op.Show();                        ////////////////////////////////////////////


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
    }
}
