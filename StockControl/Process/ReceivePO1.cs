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
    public partial class ReceivePO1 : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ReceivePO1(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            this.Name = "ReceivePO1";
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public ReceivePO1(string RCNox,string PONox)
        {
            this.Name = "ReceivePO1";
            InitializeComponent();
            PONo = PONox;
            RCNo = RCNox;
            screen = 1;
        }

        string RCNo = "";
        string PONo = "";

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
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {

                txtReceiveNo.Text = RCNo;
                txtPONo.Text = PONo;
               
                var getpo = db.sp_002_1_TPIC_SelectPOItem_Dynamics(txtPONo.Text).FirstOrDefault();
                if (getpo != null)
                {
                    txtQtyInPO.Text = getpo.TotalResults.ToString();
                    txtsnp.Text = getpo.LotSize.ToString();
                    txtPartNo.Text = getpo.CODE;
                    txtPartName.Text = getpo.NAME;
                    txtOrderQty.Text = getpo.OrderQty.ToString();
                    txtPackageQty.Text = "";
                    txtQuantitiyReceive.Text = "";
                    txtLotNo.Text = "";
                    txtQuantitiyReceive.Focus();

                }
                else
                {
                    MessageBox.Show("ไม่พบเลขที่ P/O นี้!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการบันทึก หรือไม่ ?", "บันทึกรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if(CheckData())
                    {
                        decimal Qty = 0;
                        decimal OrderQty = 0;
                        decimal snp = 0;
                        decimal.TryParse(txtQtyInPO.Text, out OrderQty);
                        decimal.TryParse(txtQuantitiyReceive.Text, out Qty);
                        decimal.TryParse(txtsnp.Text, out snp);
                        string Status = "";
                        if (Qty == OrderQty)
                            Status = "Full";
                        else
                            Status = "Partial";

                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_ReceiveTemp rct = db.tb_ReceiveTemps.Where(r => r.RCNo == RCNo && r.PONo == PONo).FirstOrDefault();
                            if (rct != null)
                            {
                                db.tb_ReceiveTemps.DeleteOnSubmit(rct);
                                db.SubmitChanges();
                            }
                            tb_ReceiveTemp rt = new tb_ReceiveTemp();
                            rt.RCNo = RCNo;
                            rt.PONo = PONo;
                            rt.PartName = txtPartName.Text;
                            rt.Code = txtPartNo.Text;
                            rt.Remark = "";
                            rt.SNP = snp;
                            rt.Qty = Qty;
                            rt.UserID = dbClss.UserID;
                            rt.CreateBy = dbClss.UserID;
                            rt.CreateDate = DateTime.Now;
                            rt.Status = Status;

                            db.tb_ReceiveTemps.InsertOnSubmit(rt);
                            db.SubmitChanges();
                            MessageBox.Show("Insert Completed.");
                            

                        }
                        this.Close();





                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                
            }
        }
        private bool CheckData()
        {
            bool ck = false;
            string err = "";

            decimal Qty = 0;
            decimal OrderQty = 0;
            decimal.TryParse(txtQtyInPO.Text, out OrderQty);
            decimal.TryParse(txtQuantitiyReceive.Text, out Qty);
            if(Qty>OrderQty)
            {
                err += "Qty is not Valid! \n";
            }
            if(Qty<=0)
            {
                err += "Qty = 0 \n";
            }
            if(txtLotNo.Text.Equals(""))
            {
                err += "LotNo is Empty! \n";
            }
            if(err.Equals(""))
            {
                ck = true;
            }else
            {
                MessageBox.Show(err);
                ck = false;
            }


            return ck;
        }

        private void CalLotTAG()
        {
            try
            {

            }
            catch { }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            txtLotNo.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการลบหรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
                
        }
    }
}
