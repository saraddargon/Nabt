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
using System.Runtime.InteropServices;
namespace StockControl
{
    public partial class LocationTAGCheck : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public LocationTAGCheck(string Code)
        {
            this.Name = "LocationTAGCheck";
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
            else if (keyData == (Keys.Escape))
            {
               
                return false;
            }
            else if (keyData == (Keys.F5))
            {
                Clear();
                return false;
            }
            else if (keyData == (Keys.Control | Keys.D))
            {

                // Deletetrans();
                return false;
            }
            else if (keyData == (Keys.Control | Keys.P))
            {
                
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Clear()
        {
            txtNaptTAG.Text = "";
            txtCustomerTAG.Text = "";
            txtHCustomerTAG.Text = "";
            txtOrder2.Text = "";
            txtStatus2.Text = "";
            //txtOrder22.Text = "";
            //txtPartNo22.Text = "";
            //txtCustomerItemNo22.Text = "";
            txtNaptTAG0.Text = "";
            txtPartNapt.Text = "";
            txtStatus.Text = "";
            txtItemCheck.Text = "";
            txtHNaptTAG.Text = "";
            txtCodeNo.Text = "";
            txtOrderNo.Text = "";
            txtOrderNo.Focus();
        }
        private void Clear2()
        {
            txtHOrderNo.Text = txtOrderNo.Text;
            txtHCustomerTAG.Text = txtCustomerTAG.Text;
            txtHNaptTAG.Text = txtNaptTAG.Text;
            txtItemCheck.Text = "";
            txtPartNapt.Text = "";
            txtNaptTAG0.Text = "";
            txtCodeNo.Text = "";
            txtNaptTAG.Text = "";
            txtCustomerTAG.Text = "";
            txtCustomerTAG.Focus();
            //txtOrderNo.Text = "";
           // txtOrderNo.Focus();
        }
        private void Check()
        {

        }
        // TextBox Lot//
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
            txtCustomerTAG.Text = "";
            txtNaptTAG.Text = "";            
            txtOrderNo.Text = "";
            txtCodeNo.Text = "";
            txtOrderNo.Focus();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtNaptTAG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtNaptTAG.Text.Equals(""))
                {
                    CheckItemMap();
                    //txtItemCheck.Text = "";
                    //using (DataClasses1DataContext db = new DataClasses1DataContext())
                    //{
                    //    tb_SkipItemCheck li = db.tb_SkipItemChecks.Where(rc => rc.ItemNaptMap.Equals(txtNaptTAG.Text) && rc.CheckItem == true).FirstOrDefault();
                    //    if(li!=null)
                    //    {
                    //        txtItemCheck.Text = li.ItemCust.ToString();
                    //        txtPartNapt.Text = li.ItemNapt;
                    //        txtNaptTAG0.Text = li.ItemNapt;
                    //    }
                    //}

                    //    txtCustomerTAG.Text = "";
                    //    txtCustomerTAG.Focus();

                }
            }
        }
        private void CheckItemMap()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string ItemForCheck = txtNaptTAG.Text;
                    if (!txtItemCheck.Text.Equals(""))
                        ItemForCheck = txtItemCheck.Text;
                    if (txtCustomerTAG.Text.Contains(ItemForCheck))
                    {
                        tb_LocalListDeliverly01 rc = db.tb_LocalListDeliverly01s.Where(c => c.SaleOrder == txtOrderNo.Text 
                        && c.PartNo == txtNaptTAG0.Text
                        && c.Plant.Trim().ToUpper().Equals(txtPlant.Text.Trim().ToUpper())
                        ).FirstOrDefault();


                        if (!CSTMNo.Equals("300113S") && !CSTMNo.Equals("300113V") && !CSTMNo.Equals("300153M") && !CSTMNo.Equals("300153S") && !CSTMNo.Equals("3006"))
                        {
                            rc = db.tb_LocalListDeliverly01s.Where(c => c.SaleOrder == txtOrderNo.Text
                                   && c.PartNo == txtNaptTAG0.Text
                                 //  && c.Plant.Trim().ToUpper().Equals(txtPlant.Text.Trim().ToUpper())
                                   ).FirstOrDefault();
                        }



                        if (rc != null)
                        {
                            rc.DocumentFlag = true;
                            rc.DocumentDate = DateTime.Now;
                            rc.DocumentBy = dbClss.UserID;
                            db.SubmitChanges();

                            txtStatus.Text = "OK";
                            txtStatus.ForeColor = Color.Green;
                            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                            player.Play();
                        }
                        else
                        {

                            int cck = 0;
                            var ck = db.sp_020_LocalDeliverySaleOrder_Dynamics(txtOrderNo.Text, txtNaptTAG0.Text).ToList();
                            if (ck.Count > 0)
                            {
                                if(txtPlant.Text.Equals(""))
                                {
                                    txtPlant.Text = ck.FirstOrDefault().BR_PLANTD.ToString();
                                }

                                var listSO = db.sp_020_LocalDeliverySaleOrder_Plant_Dynamics(txtOrderNo.Text, txtNaptTAG0.Text, txtPlant.Text.Trim().ToUpper()).ToList();
                                foreach (var rss in listSO)
                                {
                                    cck += 1;
                                    var rd = ck.FirstOrDefault();

                                    tb_LocalListDeliverly01 ne = new tb_LocalListDeliverly01();
                                    ne.SaleOrder = rd.SORDER.ToString();
                                    ne.DocumentDate = DateTime.Now;
                                    ne.DocumentFlag = true;
                                    ne.DocumentBy = dbClss.UserID;
                                    ne.ShipFlag = false;
                                    ne.PackingFlag = false;
                                    ne.PrintFlag = false;
                                    ne.PartNo = rd.CODE;
                                    ne.CustomerNo = rd.CustomerNo;
                                    ne.ShippingDate = Convert.ToDateTime(rd.ShippingDate);

                                    ne.ShipBy = "";
                                    ne.PrintBy = "";
                                    ne.PackingBy = "";
                                    ne.Plant = Convert.ToString(rss.BR_PLANTD);

                                    ne.SS = 1;
                                    db.tb_LocalListDeliverly01s.InsertOnSubmit(ne);
                                    db.SubmitChanges();
                                }
                                if (cck > 0)
                                {
                                    txtStatus.Text = "OK";
                                    txtStatus.ForeColor = Color.Green;
                                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                                    player.Play();
                                }
                            }
                            Clear2();
                        }

                    }
                    else
                    {
                        txtStatus.Text = "Not Match!!!";
                        txtStatus.ForeColor = Color.Red;
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                        player.Play();
                        Clear2();
                    }
                }
            }
            catch { }
        }
        int skp = 0;
        private void txtCustomerTAG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtCustomerTAG.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var ck = db.sp_021_LocalDeliverySaleOrder_Dynamics(txtOrderNo.Text, txtCustomerTAG.Text).ToList();
                        if (ck.Count > 0)
                        {
                            txtItemCheck.Text = "";
                            txtCodeNo.Text = ck.FirstOrDefault().CCODE;
                            txtNaptTAG0.Text = ck.FirstOrDefault().CODE;
                            tb_SkipItemCheck li = db.tb_SkipItemChecks.Where(rc => rc.ItemNapt.Equals(txtNaptTAG0.Text) && rc.CheckItem == true).FirstOrDefault();
                            if (li != null)
                            {

                                // txtItemCheck.Text = li.ItemNaptMap;
                                txtCustomerTAG.Text = li.ItemNaptMap;
                            }
                            tb_SkipItemCheck li2 = db.tb_SkipItemChecks.Where(rr => rr.ItemNapt == txtNaptTAG0.Text && rr.FixItem == true).FirstOrDefault();
                            if (li2 != null)
                            {
                                skp = 0;
                                txtNaptTAG.Text = "";
                                txtNaptTAG.Focus();
                                txtPlant.Text = "";
                                txtPlant.Focus();
                                if (!CSTMNo.Equals("300113S") && !CSTMNo.Equals("300113V") && !CSTMNo.Equals("300153M") && !CSTMNo.Equals("300153S") && !CSTMNo.Equals("3006"))
                                {
                                    txtNaptTAG.Text = "";
                                    txtNaptTAG.Focus();
                                }
                            }
                            else
                            {

                                skp = 1;
                                txtPlant.Text = "";
                                txtPlant.Focus();
                                if (!CSTMNo.Equals("300113S") && !CSTMNo.Equals("300113V") && !CSTMNo.Equals("300153M") && !CSTMNo.Equals("300153S") && !CSTMNo.Equals("3006"))
                                {
                                    CheckItemMap();
                                }
                              //  CheckItemMap();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Customer Part ไม่ตรงกับ Sale Order No.!");
                            txtStatus.Text = "Item Not Match!!!";
                            txtStatus.ForeColor = Color.Red;
                            System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                            player2.Play();
                            Clear2();
                        }
                    }

                    /*
                    //string ItemForCheck = txtNaptTAG.Text;
                    //if (!txtItemCheck.Text.Equals(""))
                    //    ItemForCheck = txtItemCheck.Text;

                    //if (txtCustomerTAG.Text.Contains(ItemForCheck))
                    //{

                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_LocalListDeliverly01 rc = db.tb_LocalListDeliverly01s.Where(c => c.SaleOrder == txtOrderNo.Text && c.PartNo== txtNaptTAG0.Text).FirstOrDefault();
                            if (rc != null)
                            {
                                rc.DocumentFlag = true;
                                rc.DocumentDate = DateTime.Now;
                                rc.DocumentBy = dbClss.UserID;
                                db.SubmitChanges();

                                txtStatus.Text = "OK";
                                txtStatus.ForeColor = Color.Green;
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                                player.Play();
                            }
                            else
                            {
                                var ck = db.sp_020_LocalDeliverySaleOrder(txtOrderNo.Text, txtNaptTAG0.Text).ToList();
                                if (ck.Count > 0)
                                {
                                    var rd = ck.FirstOrDefault();

                                    tb_LocalListDeliverly01 ne = new tb_LocalListDeliverly01();
                                    ne.SaleOrder = rd.SORDER.ToString();
                                    ne.DocumentDate = DateTime.Now;
                                    ne.DocumentFlag = true;
                                    ne.DocumentBy = dbClss.UserID;
                                    ne.ShipFlag = false;
                                    ne.PackingFlag = false;
                                    ne.PrintFlag = false;
                                    ne.PartNo = rd.CODE;
                                    ne.CustomerNo = rd.CustomerNo;
                                    ne.ShippingDate = Convert.ToDateTime(rd.ShippingDate);

                                    ne.ShipBy = "";
                                    ne.PrintBy = "";
                                    ne.PackingBy = "";


                                    ne.SS = 1;
                                    db.tb_LocalListDeliverly01s.InsertOnSubmit(ne);
                                    db.SubmitChanges();
                                    txtStatus.Text = "OK";
                                    txtStatus.ForeColor = Color.Green;
                                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                                    player.Play();
                                }
                                else
                                {
                                    MessageBox.Show("PartNo ไม่ตรงกับ Sale Order No.!");
                                    txtStatus.Text = "Item Not Match!!!";
                                    txtStatus.ForeColor = Color.Red;
                                    System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                                    player2.Play();
                                    Clear2();
                                }

                            }
                        }
                        //Clear2();
                    //}
                    //else
                    //{

                    //    txtStatus.Text = "Not Match!!!";
                    //    txtStatus.ForeColor = Color.Red;
                    //    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                    //    player.Play();
                    //    Clear2();

                    //}
                    */

                }
                else
                {
                    txtCustomerTAG.Text = "";
                    txtCustomerTAG.Focus();
                }
            }
        }

        private bool PVCheck()
        {
            bool ck = false;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var sl = db.sp_021_LocalDeliverySaleOrder_Dynamics(txtOrderNo.Text, txtCustomerTAG.Text).ToList();
                if (sl.Count > 0)
                {
                    if (txtNaptTAG.Text.Equals(""))
                    {
                        txtNaptTAG.Text = sl.FirstOrDefault().CODE;
                        txtNaptTAG0.Text = sl.FirstOrDefault().CODE;
                    }
                    else
                    {
                        txtNaptTAG0.Text = sl.FirstOrDefault().CODE;
                    }
                }
                else
                {
                    //TAG ไม่ตรงกับ SaleOrder//
                    MessageBox.Show("Customer Part ไม่ตรงกับ Sale Order No.!");
                    txtStatus.Text = "Item Not Match!!!";
                    txtStatus.ForeColor = Color.Red;
                    System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                    player2.Play();
                    Clear2();
                }
            }

            return ck;
        }
        string CSTMNo = "";
        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                if(!txtOrderNo.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int Check1 = 0;
                        var ck = db.sp_020_LocalDeliverySaleOrder_Dynamics(txtOrderNo.Text,"").ToList();
                        if (ck.Count > 0)
                        {
                            CSTMNo = ck.FirstOrDefault().CustomerNo.ToString();
                            Check1 = 1;
                            //txtCodeNo.Text = ck.FirstOrDefault().CCODE;                            
                            // txtNaptTAG0.Text = "";
                            // txtNaptTAG0.Focus();
                            // txtNaptTAG.Text = "";
                            // txtNaptTAG.Focus();                            
                        }
                        //แบบที่ 1
                        if(Check1==0)
                        {

                            if (txtOrderNo.Text.Length > 10)
                            {
                                //51000517570
                                string RN = txtOrderNo.Text.Substring(0, 11);
                                var ck2 = db.sp_020_LocalDeliverySaleOrder_Dynamics(RN, "").ToList();
                                if (ck2.Count > 0)
                                {
                                    CSTMNo = ck2.FirstOrDefault().CustomerNo.ToString();
                                    Check1 = 1;
                                    txtOrderNo.Text = RN;
                                }
                            }
                        }
                        //แบบที่ 2
                        if (Check1 == 0)
                        {
                            if (txtOrderNo.Text.Length > 7)
                            {

                                string RN = "V" + txtOrderNo.Text;
                                var ck2 = db.sp_020_LocalDeliverySaleOrder_Dynamics(RN, "").ToList();
                                if (ck2.Count > 0)
                                {
                                    CSTMNo = ck2.FirstOrDefault().CustomerNo.ToString();
                                    txtOrderNo.Text = RN;
                                    Check1 = 1;
                                }

                                if (Check1 == 0)
                                {
                                    RN = "M" + txtOrderNo.Text;
                                    var ck3 = db.sp_020_LocalDeliverySaleOrder_Dynamics(RN, "").ToList();
                                    if (ck3.Count > 0)
                                    {
                                        CSTMNo = ck3.FirstOrDefault().CustomerNo.ToString();
                                        Check1 = 1;
                                        txtOrderNo.Text = RN;
                                    }
                                }
                                if (Check1 == 0)
                                {
                                    RN = "S" + txtOrderNo.Text;
                                    var ck4 = db.sp_020_LocalDeliverySaleOrder_Dynamics(RN, "").ToList();
                                    if (ck4.Count > 0)
                                    {
                                        CSTMNo = ck4.FirstOrDefault().CustomerNo.ToString();
                                        Check1 = 1;
                                        txtOrderNo.Text = RN;
                                    }
                                }
                            }

                        }

                        //แบบที่ 3
                        if(Check1==0)
                        {
                            string RN = txtOrderNo.Text.Trim();
                            var ck5 = db.sp_022_LocalDeliverySaleOrder_Dynamics(RN).ToList();
                            if(ck5.Count>0)
                            {
                                Check1 = 1;
                                txtOrderNo.Text = ck5.FirstOrDefault().SORDER;
                                CSTMNo = ck5.FirstOrDefault().CustomerNo.ToString();
                            }
                        }

                        /////////////////Next Step///////////
                        if (Check1 == 1)
                        {
                            txtCSTM.Text = CSTMNo;
                            txtCustomerTAG.Text = "";
                            txtCustomerTAG.Focus();
                        }
                        else if (Check1 == 0)
                        {
                            MessageBox.Show("ไม่มีเลขที่ SaleOrder นี้ในระบบ!");
                            txtOrderNo.Text = "";
                            txtOrderNo.Focus();
                        }
                    }

                }
            }
        }

        private void txtOrder2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                if(!txtOrder2.Text.Equals(""))
                {
                    txtStatus.Text = "";
                    UpdateCheckDocument();
                }
            }
        }
        private void UpdateCheckDocument()
        {
            try
            {
                int cc1 = 0;
                int CheckRow = 0;
                int ErrorRow = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var ck = db.sp_020_LocalDeliverySaleOrder_Dynamics(txtOrder2.Text, "").ToList();
                    if (ck.Count > 0)
                    {
                        foreach (var rd1 in ck)
                        {
                            // txtPartNo22.Text = rd1.CODE;
                            //  txtCustomerItemNo22.Text = rd1.CCODE;
                            //  txtOrder22.Text = ck.FirstOrDefault().SORDER;
                            cc1 = 0;
                            tb_SkipItemCheck li = db.tb_SkipItemChecks.Where(rc => rc.ItemNapt.Equals(rd1.CODE)
                        && rc.FixItem == true).FirstOrDefault();
                            if (li != null)
                            {

                                tb_LocalListDeliverly01 rc2 = db.tb_LocalListDeliverly01s.Where(c => c.SaleOrder == txtOrder2.Text &&
                                c.PartNo==rd1.CODE && c.DocumentFlag==true).FirstOrDefault();

                                if(rc2!=null)
                                {
                                    cc1 = 0;
                                }
                                else
                                {
                                    cc1 = 1;
                                }

                                
                            }

                            if (cc1 == 0)
                            {
                                CheckRow = 0;

                                tb_LocalListDeliverly01 rc = db.tb_LocalListDeliverly01s.Where(c => c.SaleOrder == txtOrder2.Text
                                && c.PartNo==rd1.CODE).FirstOrDefault();
                                if (rc != null)
                                {
                                    rc.DocumentFlag = true;
                                    rc.DocumentDate = DateTime.Now;
                                    rc.DocumentBy = dbClss.UserID;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    //var ck3 = db.sp_020_LocalDeliverySaleOrder(txtOrder2.Text, rd1.CODE).ToList();
                                    //if (ck3.Count > 0)
                                    //{
                                       // var rd = ck3.FirstOrDefault();

                                        tb_LocalListDeliverly01 ne = new tb_LocalListDeliverly01();
                                        ne.SaleOrder = rd1.SORDER.ToString();
                                        ne.DocumentDate = DateTime.Now;
                                        ne.DocumentFlag = true;
                                        ne.DocumentBy = dbClss.UserID;
                                        ne.ShipFlag = false;
                                        ne.PackingFlag = false;
                                        ne.PrintFlag = false;
                                        ne.PartNo = rd1.CODE;
                                        ne.CustomerNo = rd1.CustomerNo;
                                        ne.ShippingDate = Convert.ToDateTime(rd1.ShippingDate);

                                        ne.ShipBy = "";
                                        ne.PrintBy = "";
                                        ne.PackingBy = "";

                                        ne.SS = 1;
                                        db.tb_LocalListDeliverly01s.InsertOnSubmit(ne);
                                        db.SubmitChanges();
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("Sale Order No.! ไม่พบในระบบ");
                                    //}

                                }
                                txtStatus2.Text = "OK";
                                txtStatus2.ForeColor = Color.Green;
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                                player.Play();
                                Clear3();

                            }
                            else
                            {
                                ErrorRow += 1;
                            }
                            

                            ////////////////for////////////////////
                        }

                        if (ErrorRow > 0)
                        {
                            txtStatus2.Text = " มีรายการพาร์ท ที่จะต้องเช็ค TAG!!! ";
                            txtStatus2.ForeColor = Color.Red;
                            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                            player.Play();
                            Clear3();
                        }

                    }
                    else
                    {
                        Clear3();
                    }
                }
            }
            catch { }
        }
        private void Clear3()
        {
            txtOrder2.Text = "";
            txtOrder2.Focus();
        }

        private void txtNaptTAG0_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if(e.KeyChar==13)
            //    {
            //        if(!txtNaptTAG0.Text.Equals(""))
            //        {
            //            txtNaptTAG.Text = "";
            //            txtNaptTAG.Focus();

            //        }else
            //        {
            //            MessageBox.Show("Empty! Napt Item(L)");
            //        }
            //    }
            //}
            //catch { }
        }

        private void txtPlant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (skp == 0)
                {
                    txtNaptTAG.Text = "";
                    txtNaptTAG.Focus();
                }
                else
                {
                    CheckItemMap();
                }
            }
        }
    }
}
