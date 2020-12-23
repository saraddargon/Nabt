using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
namespace StockControl
{
    public partial class ReceiveCheck : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
     
        public ReceiveCheck(string RCNox,string PONox,string INV)
        {
            this.Name = "ReceiveCheck";
            this.Text = "Receive Item";
            InitializeComponent();
            BarcodeText = PONox;
            string[] Data = PONox.Split('$');
            if (Data.Length > 1)
                PONo = Data[0];
            else
                PONo = Data[0];
           // PONo = PONox;
            RCNo = RCNox;
            Invoice = INV;
            screen = 1;
        }

        string RCNo = "";
        string PONo = "";
        string Invoice = "";
        string BarcodeText = "";
        string PR1 = "";
        string PR2 = "";
        string Type = "";
        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            txtReceiveNo.Text = RCNo;
            txtPONo.Text = PONo;
            
                LoadData();
            
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!PONo.Equals(""))
                    {


                        var gp = db.sp_007_TPIC_SelectPO_Dynamics(BarcodeText).ToList();
                        if (gp.Count > 0)
                        {
                            txtPartNo.Text = gp.FirstOrDefault().CODE;
                            txtPartName.Text = gp.FirstOrDefault().NAME;
                            txtSNP.Text = Convert.ToString(gp.FirstOrDefault().LotSize);
                            txtQtyInPO.Text = gp.FirstOrDefault().OrderQty.ToString("###,##0.##");
                            txtRemain.Text = (gp.FirstOrDefault().OrderQty - gp.FirstOrDefault().TotalResults).ToString("###,##0.##");
                            txtVendorName.Text = gp.FirstOrDefault().VendorName;
                            txtUnit.Text = gp.FirstOrDefault().Unit;
                            txtVendorNo.Text = gp.FirstOrDefault().VENDOR;
                            txtLocation.Text = gp.FirstOrDefault().Location.ToUpper();
                            txtPrice.Text = gp.FirstOrDefault().PRICE.ToString("###,###,##0.#####");
                            txtReceiveQty.Text = "0";
                            txtLotNo.Text = DateTime.Now.ToString("yyyyMMdd") + "T";
                            txtid.Text = "";

                            double QtyRemain = 0;
                            double.TryParse(txtRemain.Text, out QtyRemain);
                            tb_ReceiveLineTemp tm = db.tb_ReceiveLineTemps.Where(t => t.RCNo == RCNo && t.PONo == PONo && t.BarcodeText.Equals(BarcodeText)).FirstOrDefault();
                            if (tm != null)
                            {
                                txtid.Text = tm.id.ToString();
                                txtLotNo.Text = tm.LotNo;
                                txtReceiveQty.Text = Convert.ToDecimal(tm.Qty).ToString("###,##0.##");
                                txtRemark.Text = tm.Remark;

                            }
                            if (QtyRemain > 0)
                            {
                              //  txtReceiveQty.Text = "0";
                                txtReceiveQty.Enabled = true;
                                txtLotNo.Enabled = true;
                                txtReceiveQty.Focus();
                                btnExport.Enabled = true;
                            }
                            else
                            {
                                //txtReceiveQty.Text = "0";
                                txtReceiveQty.Enabled = false;
                                txtLotNo.Enabled = false;
                                btnExport.Enabled = false;
                            }


                        }
                    }
                }

            }
            catch { }
            this.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int id = 0;
                        int.TryParse(txtid.Text, out id);
                        decimal SNP = 0;
                        decimal OrderQty = 0;
                        decimal Remain = 0;
                        decimal ReceiveQty = 0;
                        decimal price = 0;
                        decimal.TryParse(txtReceiveQty.Text, out ReceiveQty);
                        decimal.TryParse(txtRemain.Text, out Remain);
                        decimal.TryParse(txtSNP.Text, out SNP);
                        decimal.TryParse(txtQtyInPO.Text, out OrderQty);
                        decimal.TryParse(txtPrice.Text, out price);
                        string Status = "";
                        if ((Remain - ReceiveQty) <= 0)
                            Status = "FULL";
                        else if ((Remain - ReceiveQty) > 0)
                            Status = "Partial";

                        if (ReceiveQty <= Remain)
                        {

                            tb_ReceiveLineTemp tm = db.tb_ReceiveLineTemps.Where(t => t.id == id && !t.StatusTranfer.Equals("Completed") && t.CreateBy.Equals(dbClss.UserID)).FirstOrDefault();
                            if (tm != null)
                            {
                                //Edit//

                                tm.Qty = ReceiveQty;
                                tm.BeforeRemain = Remain;
                                tm.SNP = SNP.ToString();
                                tm.Unit = txtUnit.Text;
                                tm.TAG = "";
                                tm.QRScan = "";
                                tm.CreateBy = dbClss.UserID;
                                tm.CreateDate = DateTime.Now;
                                tm.Location = txtLocation.Text;
                                tm.LotNo = txtLotNo.Text;
                                tm.Remark = txtRemark.Text;
                                tm.BarcodeText = BarcodeText;

                                tm.Status = Status;
                                tm.StatusTranfer = "Waiting";

                                db.SubmitChanges();
                                this.Close();

                            }
                            else
                            {

                                if (ReceiveQty > 0)
                                {


                                    tb_ReceiveLineTemp tn = new tb_ReceiveLineTemp();
                                    tn.Code = txtPartNo.Text;
                                    tn.PartName = txtPartName.Text;
                                    tn.Remark = txtRemark.Text;
                                    tn.RCNo = RCNo;
                                    tn.PONo = PONo;
                                    tn.InvoiceNo = Invoice;
                                    tn.VendorNo = txtVendorNo.Text;
                                    tn.VendorName = txtVendorName.Text;
                                    tn.Price = price;
                                    tn.OrderQty = OrderQty;
                                    tn.Qty = ReceiveQty;
                                    tn.BeforeRemain = Remain;
                                    tn.SNP = SNP.ToString();
                                    tn.Unit = txtUnit.Text;
                                    tn.TAG = "";
                                    tn.QRScan = "";
                                    tn.LocalLotNo = DateTime.Now.ToString("yyyyMMdd")+"T";
                                    tn.CreateBy = dbClss.UserID;
                                    tn.CreateDate = DateTime.Now;
                                    tn.LotNo = txtLotNo.Text;
                                    tn.Remark = txtRemark.Text;
                                    tn.Location = txtLocation.Text;
                                    tn.BarcodeText = BarcodeText;


                                    tn.Status = Status;
                                    tn.StatusTranfer = "Waiting";
                                    db.tb_ReceiveLineTemps.InsertOnSubmit(tn);
                                    db.SubmitChanges();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("จำนวนไม่ถูกต้อง!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("รับมากกว่าจำนวนคงเหลือ ไม่ได้!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                            
                        
                    }
                }
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtReceiveQty.Text = txtRemain.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLotNo.Text = DateTime.Now.ToString("yyyyMMdd")+"T";
        }
    }
}
