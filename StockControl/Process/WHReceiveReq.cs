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
    public partial class WHReceiveReq : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public WHReceiveReq(string Code)
        {
            this.Name = "WHReceiveReq";
            InitializeComponent();          
        }
        public WHReceiveReq()
        {
            this.Name = "WHReceiveReq";
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
            txtBarcodeTAG.Text = "";
            txtBarcodeTAG2.Text = "";
           
            txtInvoiceNo.Text = "";
            txtHOrderNo.Text = "";
            //txtOrder22.Text = "";
            //txtPartNo22.Text = "";
            //txtCustomerItemNo22.Text = "";
            txtPartNo2.Text = "";
            txtPartNapt.Text = "";
            txtStatus.Text = "";
            txtItemCheck.Text = "";
            txtInvoiceNo.Text = "";
            txtCodeNo.Text = "";
            txtBarcodeTAG.Text = "";
            txtBarcodeTAG.Focus();
        }
        private void Clear2()
        {
            //txtStatus.Text = "";
            //txtHOrderNo.Text = "";
            //txtBarcodeTAG2.Text = txtBarcodeTAG.Text;
            //txtInvoiceNo.Text = "";
            //txtItemCheck.Text = "";
            //txtPartNapt.Text = "";
            //txtInvoiceNo.Text = "";
            //txtPartNo2.Text = "";
            //txtBarcodeTAG2.Text = "";
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
            //txtCustomerTAG.Text = "";
           // txtNaptTAG.Text = "";            
            txtBarcodeTAG.Text = "";
            txtCodeNo.Text = "";
            txtBarcodeTAG.Focus();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtNaptTAG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //if (!txtNaptTAG.Text.Equals(""))
                //{
                //    CheckItemMap();
                //    //txtItemCheck.Text = "";
                //    //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //    //{
                //    //    tb_SkipItemCheck li = db.tb_SkipItemChecks.Where(rc => rc.ItemNaptMap.Equals(txtNaptTAG.Text) && rc.CheckItem == true).FirstOrDefault();
                //    //    if(li!=null)
                //    //    {
                //    //        txtItemCheck.Text = li.ItemCust.ToString();
                //    //        txtPartNapt.Text = li.ItemNapt;
                //    //        txtNaptTAG0.Text = li.ItemNapt;
                //    //    }
                //    //}

                //    //    txtCustomerTAG.Text = "";
                //    //    txtCustomerTAG.Focus();

                //}
            }
        }
        private void CheckItemMap()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string ItemForCheck = txtBarcodeTAG.Text.Trim();
                    //if (!txtItemCheck.Text.Equals(""))
                    //    ItemForCheck = txtItemCheck.Text;
                  
                }
            }
            catch { }
        }

        private void txtCustomerTAG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //if (!txtCustomerTAG.Text.Equals(""))
                //{
                //    using (DataClasses1DataContext db = new DataClasses1DataContext())
                //    {
                //        var ck = db.sp_021_LocalDeliverySaleOrder(txtBarcodeTAG.Text, txtCustomerTAG.Text).ToList();
                //        if (ck.Count > 0)
                //        {
                //            txtItemCheck.Text = "";
                //            txtCodeNo.Text = ck.FirstOrDefault().CCODE;
                //            txtNaptTAG0.Text = ck.FirstOrDefault().CODE;
                //            tb_SkipItemCheck li = db.tb_SkipItemChecks.Where(rc => rc.ItemNapt.Equals(txtNaptTAG0.Text) && rc.CheckItem == true).FirstOrDefault();
                //            if (li != null)
                //            {

                //                // txtItemCheck.Text = li.ItemNaptMap;
                //                txtCustomerTAG.Text = li.ItemNaptMap;
                //            }
                //            tb_SkipItemCheck li2 = db.tb_SkipItemChecks.Where(rr => rr.ItemNapt == txtNaptTAG0.Text && rr.FixItem == true).FirstOrDefault();
                //            if (li2 != null)
                //            {
                //                txtNaptTAG.Text = "";
                //                txtNaptTAG.Focus();
                //            }
                //            else
                //            {
                //                CheckItemMap();
                //            }

                //        }
                //        else
                //        {
                //            MessageBox.Show("Customer Part ไม่ตรงกับ Sale Order No.!");
                //            txtStatus.Text = "Item Not Match!!!";
                //            txtStatus.ForeColor = Color.Red;
                //            System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                //            player2.Play();
                //            Clear2();
                //        }
                //    }

                //    /*
                //    //string ItemForCheck = txtNaptTAG.Text;
                //    //if (!txtItemCheck.Text.Equals(""))
                //    //    ItemForCheck = txtItemCheck.Text;

                //    //if (txtCustomerTAG.Text.Contains(ItemForCheck))
                //    //{

                //    using (DataClasses1DataContext db = new DataClasses1DataContext())
                //        {
                //            tb_LocalListDeliverly01 rc = db.tb_LocalListDeliverly01s.Where(c => c.SaleOrder == txtOrderNo.Text && c.PartNo== txtNaptTAG0.Text).FirstOrDefault();
                //            if (rc != null)
                //            {
                //                rc.DocumentFlag = true;
                //                rc.DocumentDate = DateTime.Now;
                //                rc.DocumentBy = dbClss.UserID;
                //                db.SubmitChanges();

                //                txtStatus.Text = "OK";
                //                txtStatus.ForeColor = Color.Green;
                //                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                //                player.Play();
                //            }
                //            else
                //            {
                //                var ck = db.sp_020_LocalDeliverySaleOrder(txtOrderNo.Text, txtNaptTAG0.Text).ToList();
                //                if (ck.Count > 0)
                //                {
                //                    var rd = ck.FirstOrDefault();

                //                    tb_LocalListDeliverly01 ne = new tb_LocalListDeliverly01();
                //                    ne.SaleOrder = rd.SORDER.ToString();
                //                    ne.DocumentDate = DateTime.Now;
                //                    ne.DocumentFlag = true;
                //                    ne.DocumentBy = dbClss.UserID;
                //                    ne.ShipFlag = false;
                //                    ne.PackingFlag = false;
                //                    ne.PrintFlag = false;
                //                    ne.PartNo = rd.CODE;
                //                    ne.CustomerNo = rd.CustomerNo;
                //                    ne.ShippingDate = Convert.ToDateTime(rd.ShippingDate);

                //                    ne.ShipBy = "";
                //                    ne.PrintBy = "";
                //                    ne.PackingBy = "";


                //                    ne.SS = 1;
                //                    db.tb_LocalListDeliverly01s.InsertOnSubmit(ne);
                //                    db.SubmitChanges();
                //                    txtStatus.Text = "OK";
                //                    txtStatus.ForeColor = Color.Green;
                //                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-07.wav");
                //                    player.Play();
                //                }
                //                else
                //                {
                //                    MessageBox.Show("PartNo ไม่ตรงกับ Sale Order No.!");
                //                    txtStatus.Text = "Item Not Match!!!";
                //                    txtStatus.ForeColor = Color.Red;
                //                    System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                //                    player2.Play();
                //                    Clear2();
                //                }

                //            }
                //        }
                //        //Clear2();
                //    //}
                //    //else
                //    //{

                //    //    txtStatus.Text = "Not Match!!!";
                //    //    txtStatus.ForeColor = Color.Red;
                //    //    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
                //    //    player.Play();
                //    //    Clear2();

                //    //}
                //    */

                //}
                //else
                //{
                //    //txtCustomerTAG.Text = "";
                //    //txtCustomerTAG.Focus();
                //}
            }
        }

        private bool PVCheck()
        {
            bool ck = false;
            //using (DataClasses1DataContext db = new DataClasses1DataContext())
            //{
            //    var sl = db.sp_021_LocalDeliverySaleOrder(txtBarcodeTAG.Text, txtCustomerTAG.Text).ToList();
            //    if (sl.Count > 0)
            //    {
            //        if (txtNaptTAG.Text.Equals(""))
            //        {
            //            txtNaptTAG.Text = sl.FirstOrDefault().CODE;
            //            txtNaptTAG0.Text = sl.FirstOrDefault().CODE;
            //        }
            //        else
            //        {
            //            txtNaptTAG0.Text = sl.FirstOrDefault().CODE;
            //        }
            //    }
            //    else
            //    {
            //        //TAG ไม่ตรงกับ SaleOrder//
            //        MessageBox.Show("Customer Part ไม่ตรงกับ Sale Order No.!");
            //        txtStatus.Text = "Item Not Match!!!";
            //        txtStatus.ForeColor = Color.Red;
            //        System.Media.SoundPlayer player2 = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\beep-05.wav");
            //        player2.Play();
            //        Clear2();
            //    }
            //}

            return ck;
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                txtBarcodeTAG2.Text = "";
                txtHOrderNo.Text = "";
                txtStatus.Text = "";
                txtPartNo2.Text = "";
                txtInvoiceNo.Text = "";

                if (!txtBarcodeTAG.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        //Order,PalletNo,Invoice,PartCode,Qty,ofTAG,TotalTAG,LotNo
                        // //ListNo,ExportNo,OrderNo,PartNo,CustItem,Qty,id
                        //id,ReqNo
                        string[]BK = txtBarcodeTAG.Text.Split(',');
                        
                        if(BK.Length==1)
                        {

                           
                            string ReqNo = BK[0];
                            
                           
                            tb_RequisitionHD ex = db.tb_RequisitionHDs.Where(ep => ep.ReqNo.Equals(ReqNo) && (ep.Status.Equals("Waiting") || ep.Status.Equals("SKIP"))).FirstOrDefault();
                            if(ex!=null)
                            {
                                txtBarcodeTAG2.Text = txtBarcodeTAG.Text;
                                txtHOrderNo.Text = ReqNo;
                                txtStatus.Text = "Move Stock OK!!";
                                txtStatus.ForeColor = Color.Green;
                                ex.ReceiveBy = dbClss.UserID;
                                ex.ReceiveDate = DateTime.Now;
                                ex.Status = "Process";
                                db.SubmitChanges();
                                var listUpdate = db.tb_RequisitionDTs.Where(d => d.ReqNo.Equals(ReqNo) && (d.SS == 1 || d.SS==4)).ToList();
                                foreach(var rd in listUpdate)
                                {
                                    rd.SS = 2;
                                    db.SubmitChanges();
                                }

                            }

                            
                            txtBarcodeTAG.Text = "";
                            txtBarcodeTAG.Focus();
                        }
                        else
                        {
                            txtBarcodeTAG.Text = "";
                            txtBarcodeTAG.Focus();
                        }

                    }

                }
            }
        }

        private void txtOrder2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                //if(!txtOrder2.Text.Equals(""))
                //{
                //    txtStatus.Text = "";
                //    UpdateCheckDocument();
                //}
            }
        }
        private void UpdateCheckDocument()
        {
            
        }
        private void Clear3()
        {
            //txtOrder2.Text = "";
            //txtOrder2.Focus();
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
    }
}
