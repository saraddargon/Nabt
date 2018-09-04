using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Runtime.InteropServices;
using Telerik.WinControls.UI;
using System.Linq;
namespace StockControl.Process
{
    public partial class ChckStockPC : Telerik.WinControls.UI.RadForm
    {
        public ChckStockPC(string PKTAGx,string LWx,string UserCheck,string CheckNox)
        {
            InitializeComponent();
            PKTAG = PKTAGx;
            UserCK = UserCheck;
            LW = LWx;
            CheckNo = CheckNox;
        }
        string PKTAG = "";
        string LW = "";
        string UserCK = "";
        string CheckNo = "";
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                // Alt+F pressed
                //  ClearData();
                SaveData();
                return false;
                //txtSeriesNo.Focus();
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChckStockPC_Load(object sender, EventArgs e)
        {
            try
            {
                if (!PKTAG.Equals(""))
                {
                    string[] Data = PKTAG.Split(',');
                    if(Data.Length>2)
                    {
                        // PD,WO17001112,2,4,AA2,3of3,41217058036N1
                        txtPKTAG.Text = PKTAG;
                        txtRef.Text = Data[1];
                        txtType.Text = Data[0];
                        txtQty.Text = Data[2];
                        txtSNP.Text = Data[3];
                        txtLotNo.Text = Data[4];
                        txtOfTAG.Text = Data[5];
                        txtPartNo.Text = Data[6];
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            txtPartName.Text = db.getItemNoTPICS(txtPartNo.Text).ToString();
                            txtTypeF.Text = db.getTypeTPICS(txtPartNo.Text).ToString();
                        }

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            txtQtyR.Text = "";
            txtQtyR.Focus();
        }

        private void SaveData()
        {
            try
            {
                if (!PKTAG.Equals(""))
                {
                    decimal QtyR = 0;
                    decimal.TryParse(txtQtyR.Text, out QtyR);
                    int SNP = 0;
                    int.TryParse(txtSNP.Text, out SNP);
                    if (QtyR > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_CheckStockTempCheck ck = db.tb_CheckStockTempChecks.Where(t => t.PKTAG == PKTAG).FirstOrDefault();
                            if (ck != null)
                            {

                                MessageBox.Show("ข้อมูลซ้ำกับที่ยงเข้าไปแล้ว! Qty [ " + ck.Quantity.ToString() + " ]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                            else
                            {
                                tb_CheckStockTempCheck ci = new tb_CheckStockTempCheck();
                                ci.RefNo = txtRef.Text;
                                ci.Code = txtPartNo.Text;
                                ci.ItemName = txtPartName.Text;
                                ci.PKTAG = txtPKTAG.Text;
                                ci.ofTAG = txtOfTAG.Text;
                                ci.LotNo = txtLotNo.Text;
                                ci.Location = LW;
                                ci.CheckMachine = Environment.MachineName;
                                ci.CreateBy = dbClss.UserID;
                                ci.CreateDate = DateTime.Now;
                                ci.CheckBy = UserCK;
                                ci.CheckNo = CheckNo;
                                ci.SNP = SNP;
                                ci.Quantity = QtyR;
                                ci.Remark = "";
                                ci.Package = "";
                                ci.Status = "Waiting";
                                ci.SP = txtType.Text;
                                ci.Type = txtTypeF.Text;

                                db.tb_CheckStockTempChecks.InsertOnSubmit(ci);
                                db.SubmitChanges();



                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("โปรดระบุจำนวน..");
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void txtQtyR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                SaveData();
            }
        }
    }
}
