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
                        // แบบ 1 // PD,WO17001112,2,4,AA2,3of3,41217058036N1
                        if (Data.Length > 2)
                        {
                            txtPKTAG.Text = PKTAG;
                            txtRef.Text = Data[1];
                            txtType.Text = Data[0];
                            txtQty.Text = Data[2];
                            txtSNP.Text = Data[3];
                            txtLotNo.Text = Data[4];
                            txtOfTAG.Text = Data[5];
                            txtPartNo.Text = Data[6];
                        }
                        else
                        {
                         // แบบ 1 // PD,WO17001112,2,4,AA2,3of3,41217058036N1
                            txtPKTAG.Text = PKTAG;
                            txtPartNo.Text = PKTAG;
                            //txtRef.Text = Data[1];
                            txtType.Text = "Code";
                            // Data[0];
                            // txtQty.Text = "0";
                            txtLotNo.ReadOnly = false;
                           
                            //txtLotNo.Text = Data[4];
                           // txtOfTAG.Text = Data[5];
                           // txtPartNo.Text = PKTAG;
                        }


                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_CheckStockList im = db.tb_CheckStockLists.Where(i => i.CheckNo == CheckNo && i.Code == txtPartNo.Text).FirstOrDefault();
                        if (im != null)
                        {
                            var part = db.sp_001_TPIC_SelectItem(txtPartNo.Text).FirstOrDefault();
                            if (part != null)
                            {
                                txtPartName.Text = part.NAME.ToString();//db.getItemNoTPICS(txtPartNo.Text).ToString();
                                txtTypeF.Text = part.Detail; //db.getTypeTPICS(txtPartNo.Text).ToString();
                                if (Data.Length < 2)
                                {
                                    txtSNP.Text = part.LotSize.ToString();
                                    txtQty.Text = part.CurrentStock.ToString();
                                }
                            }
                            else
                            {
                               // MessageBox.Show("ไม่พบไอเท็มนี้ ในรายการเช็คสินค้า !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                              //  this.Close();
                            }

                        }
                        else
                        {
                            txtPartNo.Text = PKTAG;
                            txtQty.Text = "0";
                            txtSNP.Text = "0";
                            txtQtyR.Text = "";
                            txtQtyR.Focus();
                            ///////////////////////////////////////////////////////////
                            //MessageBox.Show("ไม่พบไอเท็มนี้ ในรายการเช็คสินค้า !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //this.Close();

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
                    int ZoneNo = 0;

                    if (QtyR > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_CheckStockTempCheck ck = db.tb_CheckStockTempChecks.Where(t => t.PKTAG == PKTAG && t.CheckNo==CheckNo).FirstOrDefault();
                            if (ck != null && !txtType.Text.Equals("Code"))
                            {

                                MessageBox.Show("ข้อมูลซ้ำกับที่ยงเข้าไปแล้ว! Qty [ " + ck.Quantity.ToString() + " ]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                            else
                            {
                                ZoneNo = Convert.ToInt32(db.getMaxZone(CheckNo, LW)) + 1;

                                tb_CheckStockTempCheck ci = new tb_CheckStockTempCheck();
                                ci.RefNo = txtRef.Text;                               
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
                                ci.ZoneNo = ZoneNo;
                                ci.TY = 0;
                                ci.Code2 = txtPartNo.Text;
                                if (txtPartName.Text.Trim().Equals(""))
                                    ci.Code = "";
                                else
                                    ci.Code = txtPartNo.Text;
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
