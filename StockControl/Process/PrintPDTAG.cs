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
    public partial class PrintPDTAG : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;

        public PrintPDTAG(string Code)
        {
            this.Name = "PrintTEMPTAG";
            InitializeComponent();
            txtBomNo.Text = Code;
        }
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
                LoadBomNo();
            }
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        var getCode = (from ix in db.sp_001_TPIC_SelectItem(txtPartNo.Text.ToString()) select ix).ToList();
            //        if (getCode.Count > 0)
            //        {
            //            var rd = getCode.FirstOrDefault();
            //            txtPartName.Text = rd.NAME.ToString();
            //            //dtDate1.Value=Convert.ToDateTime(rd.d)
            //            txtsNP.Text = Convert.ToInt32(rd.LotSize).ToString();
            //            txtLotNo.Text = "";
            //            txtQty.Text = "0";
            //        }
            //    }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void LoadBomNo()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int ac = 1;
                    int.TryParse(txtAC.Text, out ac);
                    

                    var getCode = (from ix in db.sp_003_TPIC_GETBOMNo(txtBomNo.Text,ac) select ix).ToList();
                    if (getCode.Count > 0)
                    {
                        var rd = getCode.FirstOrDefault();
                        dtDate1.Value = Convert.ToDateTime(rd.CreateDate);
                        txtPartName.Text = rd.NAME.ToString();
                        txtPartNo.Text = rd.CODE.ToString();
                        txtCustItemName.Text = rd.CustItemName.ToString();
                        txtCustItemNo.Text = rd.CustItemNo.ToString();
                        txtCustomerName.Text = rd.CSTMName.ToString();
                        txtCustomerShortName.Text = rd.CustomerNo.ToString();                        
                        txtsNP.Text = Convert.ToInt32(rd.LotSize).ToString();

                        txtLotNo.Text = "";
                        txtQty.Text = Convert.ToInt32(rd.OrderQty).ToString("#####0");

                        tb_LotNo gl = db.tb_LotNos.Where(l => (l.LotDate.Year == dtDate1.Value.Year && l.LotDate.Month==dtDate1.Value.Month && l.LotDate.Day==dtDate1.Value.Day)).FirstOrDefault();
                        if(gl!=null)
                        {
                            txtLotNo.Text = gl.LotNo.ToString();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnExport_Click(object sender, EventArgs e)
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
                int.TryParse(txtQty.Text, out Qty);
                int.TryParse(txtsNP.Text, out snp);

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
                        if(ph!=null)
                        {
                            ImagePath = ph.PathFile;
                        }
                        tb_ItemList il = db.tb_ItemLists.Where(i => i.CodeNo == txtPartNo.Text).FirstOrDefault();
                        if(il!=null)
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
                            QrCode = "PD," + txtBomNo.Text + "," + Qty + "," + snp + "," + txtLotNo.Text + "," + OfTAG + "," + txtPartNo.Text;
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
                            ts.OFTAG= i + "/" + TAG;
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

                    Report.Reportx1 op = new Report.Reportx1("FG_TAG.rpt");
                    op.Show();
                }
                else
                {
                    MessageBox.Show("Qty invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
        }

        private void txtBomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                LoadBomNo();
            }
        }
    }
}
