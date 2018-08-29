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
    public partial class PrintTEMPTAG : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;

        public PrintTEMPTAG(string Code)
        {
            this.Name = "PrintTEMPTAG";
            InitializeComponent();
            txtPartNo.Text = Code;
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
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var getCode = (from ix in db.sp_001_TPIC_SelectItem(txtPartNo.Text.ToString()) select ix).ToList();
                    if (getCode.Count > 0)
                    {
                        var rd = getCode.FirstOrDefault();
                        txtPartName.Text = rd.NAME.ToString();
                        //dtDate1.Value=Convert.ToDateTime(rd.d)
                        txtsNP.Text = Convert.ToInt32(rd.LotSize).ToString();
                        txtLotNo.Text = "";
                        txtQty.Text = "0";
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
                    string TMNo = dbClss.GetSeriesNo(2, 2);
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
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var tm = db.TEMPTAGs.Where(t => t.UserID == dbClss.UserID && t.ItemNo == txtPartNo.Text).ToList();
                        if (tm.Count > 0)
                        {
                            db.TEMPTAGs.DeleteAllOnSubmit(tm);
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
                            QrCode = "TM," + TMNo + "," + Qty + "," + snp + "," + txtLotNo.Text + "," + OfTAG + "," + txtPartNo.Text;
                            //MessageBox.Show(QrCode);
                            byte[] barcode = dbClss.SaveQRCode2D(QrCode);

                            ///////////////////////////////
                            TEMPTAG ts = new TEMPTAG();
                            ts.UserID = dbClss.UserID;
                            ts.TempNo = TMNo;
                            ts.LotNo = txtLotNo.Text;
                            ts.TAGRemark = dtDate1.Value.ToString("dd/MM/yyyy");
                            ts.QRCode = barcode;
                            ts.PartName = txtPartName.Text;
                            ts.ItemNo = txtPartNo.Text;
                            ts.SNP = snp;
                            ts.Company = "Nabtesco Autmotive Corporation";
                            ts.Quantity = Qty;
                            ts.OfTAG = i + " / " + TAG;
                            ///////////////////////////////////////////////
                            db.TEMPTAGs.InsertOnSubmit(ts);
                            db.SubmitChanges();
                        }

                    }
                    Report.Reportx1.WReport = "TEMPTAG";
                    Report.Reportx1.Value = new string[1];
                    Report.Reportx1.Value[0] = dbClss.UserID;
                  //  Report.Reportx1.Value[1] = dbClss.UserID;
                    Report.Reportx1 op = new Report.Reportx1("TEMPTAG.rpt");
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
    }
}
