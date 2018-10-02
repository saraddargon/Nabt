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
    public partial class CheckStockEdit : Telerik.WinControls.UI.RadForm
    {
        public CheckStockEdit(int idx)
        {
            this.Name = "CheckStockEdit";

            InitializeComponent();
            idr = idx;
          
        }
        string PKTAG = "";
        string LW = "";
        string UserCK = "";
        string CheckNo = "";
        int idr = 0;
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
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_CheckStockTempCheck tm = db.tb_CheckStockTempChecks.Where(t => t.id == idr).FirstOrDefault();
                    if (tm != null)
                    {
                        txtPKTAG.Text = tm.PKTAG;
                        txtPartNo.Text = tm.Code;
                        txtPartName.Text = tm.ItemName;
                        txtLotNo.Text = tm.LotNo;
                        txtQtyR.Text = tm.Quantity.ToString();
                        txtZone.Text = tm.Location;
                        txtZoneNo.Text = tm.ZoneNo.ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
           // txtQtyR.Text = "";
           // txtQtyR.Focus();
        }

        private void SaveData()
        {
            try
            {
                if (!idr.ToString().Equals("0"))
                {
                    decimal QtyR = 0;
                    decimal.TryParse(txtQtyR.Text, out QtyR);                  
                 
                    int ZoneNo = 0;
                    int.TryParse(txtZoneNo.Text, out ZoneNo);

                    if (QtyR > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            string CKNo = "";
                            tb_CheckStockTempCheck ck = db.tb_CheckStockTempChecks.Where(t => t.id==idr).FirstOrDefault();
                            if (ck != null)
                            {
                                CKNo = ck.CheckNo;
                                ck.Location = txtZone.Text;
                                ck.PKTAG = txtPKTAG.Text;
                                ck.Code2 = txtPKTAG.Text;
                                if (!txtPartNo.Text.Trim().Equals(""))
                                {
                                    ck.Code2 = txtPartNo.Text;
                                    ck.Code = "";// txtPartNo.Text;
                                   // ck.ItemName = txtPartName.Text;
                                }
                                ck.LotNo = txtLotNo.Text;
                                ck.Quantity = QtyR;
                                ck.ZoneNo = ZoneNo;
                                db.SubmitChanges();
                        
                            }
                            db.sp_E_003_Calculate(CKNo);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ข้อมูลไม่ถูกต้อง..");
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
