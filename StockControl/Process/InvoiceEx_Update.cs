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
    public partial class InvoiceEx_Update : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public InvoiceEx_Update(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public InvoiceEx_Update()
        {
            InitializeComponent();
        }
        public InvoiceEx_Update(string INvx,int PL,int Steel)
        {
            InitializeComponent();
            InvNo = INvx;
            txtInvNo.Text = INvx;
            PalletCount = PL;
           // StellCase = Steel;
          //  PalletCount = PL-Steel;
           // txtSteelcase.Text = Steel.ToString();
           // txtPallet.Text = (PL-Steel).ToString();
        }

        //string PR1 = "";
        //string PR2 = "";
        //string Type = "";
        string InvNo = "";
        int StellCase = 0;
        int PalletCount = 0;
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
            //dtLoadDate.Value = DateTime.Now;
            //radDateTimePicker2.Value = DateTime.Now;
            upDateCode();
            LoadData();
            LoadFOBRate();
        }

        private void LoadData()
        {
            try
            {
                if (!txtInvNo.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ExportList ex = db.tb_ExportLists.Where(ea => ea.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                        if (ex != null)
                        {
                            DateTime ETD = DateTime.Now;
                            DateTime ETA = DateTime.Now;
                            int CheckNum = 0;
                            dtLoadDate.Value = Convert.ToDateTime(ex.LoadDate);
                            txtCarrier.Text = Convert.ToString(ex.ShippingBy);
                            txtCurrency.Text = Convert.ToString(ex.Country);
                            txtCRRNCY.Text = Convert.ToString(ex.CRR);
                            if (Convert.ToString(ex.CRR).Equals(""))
                            {
                                if (Convert.ToString(ex.Country).ToLower().Contains("japan"))
                                {
                                    txtCRRNCY.Text = "Baht.";
                                }
                                else

                                {
                                    txtCRRNCY.Text = "USD.";
                                }
                            }
                            


                            txtPaymentterm.Text = Convert.ToString(ex.paymentTerm);
                            if(txtPaymentterm.Text.Equals(""))
                            {
                                txtPaymentterm.Text = "60 Days";
                            }
                            txtFreight.Text = Convert.ToString(ex.Frieght);
                            txtCountry.Text = Convert.ToString(ex.CountryOriginal);
                            if(txtCountry.Text.Equals(""))
                            {
                                txtCountry.Text = "THAILAND";
                            }
                            CheckNum = 0;
                            if(!Convert.ToString(ex.ETDDatex).Equals(""))
                            {
                                ETD = Convert.ToDateTime(ex.ETDDatex);
                            }
                            if (!Convert.ToString(ex.ETADatex).Equals(""))
                            {
                                ETA = Convert.ToDateTime(ex.ETADatex);
                            }
                            dtETADate.Value = ETA;
                            dtETDDate.Value = ETD;
                            txtShippedVia.Text = Convert.ToString(ex.ShipVia);
                            txtShippingMark.Text = Convert.ToString(ex.ShippingMark);
                            txtAirType.Text = Convert.ToString(ex.AirType);
                            int FG = 0;
                            int RM = 0;
                            string InvoiceOrder = "";
                            string TypeF = "";
                            var ItemList = db.tb_ExportDetails.Where(w => w.InvoiceNo.Equals(txtInvNo.Text)).ToList();
                            foreach(var rd in ItemList)
                            {
                                TypeF = db.getItem_TypeFG_Dynamics(rd.PartNo);
                                if(TypeF.ToUpper().Equals("MATERIAL") || TypeF.ToUpper().Equals("RM"))
                                {
                                    RM += 1;
                                }
                                if(TypeF.ToUpper().Equals("UNIT") || TypeF.ToUpper().Equals("FM") || TypeF.ToUpper().Equals("WIP"))
                                {
                                    FG += 1;
                                }
                            }
                           // InvoiceOrder = txtInvNo.Text;
                           
                            if (FG > 0)
                            {
                                InvoiceOrder = txtInvNo.Text + "-2";
                                
                            }
                            if (RM > 0)
                            {
                                
                                if (!InvoiceOrder.Equals(""))
                                {
                                    InvoiceOrder = InvoiceOrder + "," + txtInvNo.Text + "-1";
                                }
                                else
                                {                                   
                                    InvoiceOrder = txtInvNo.Text + "-1";
                                }

                            }

                            txtInvoiceOrder.Text = InvoiceOrder;
                            txtCode.Text = Convert.ToString(ex.Code);
                           
                            string CustomerSale = "";
                            string CustomerShip = "";
                            string AddressSale = "";
                            string AddressSale2 = "";
                            string AddressShip = "";
                            string AddressShip2 = "";
                            string TelSale = "";
                            string FaxSale = "";
                            string TelShip = "";
                            string FaxShip = "";
                            string AttnSale = "";
                            string AttnShip = "";


                            
                                CustomerSale = Convert.ToString(ex.CustomerSale);
                                AddressSale = Convert.ToString(ex.AddressSale);
                                AddressSale2 = Convert.ToString(ex.AddressSale2);
                                TelSale = Convert.ToString(ex.TelSale);
                                FaxSale = Convert.ToString(ex.FaxSale);
                                AttnSale = Convert.ToString(ex.AttnSale);

                                CustomerShip = Convert.ToString(ex.CustomerShip);
                                AddressShip = Convert.ToString(ex.AddressShip);
                                AddressShip2 = Convert.ToString(ex.AddressShip2);
                                TelShip = Convert.ToString(ex.TelShip);
                                FaxShip = Convert.ToString(ex.FaxShip);
                                AttnShip = Convert.ToString(ex.AttnShip);

                            

                            txtCustomerSale.Text = CustomerSale;
                            txtAddressSale.Text = AddressSale;
                            txtAddressSales2.Text = AddressSale2;
                            txtTelSale.Text = TelSale;
                            txtFaxSale.Text = FaxSale;
                            txtAttnSale.Text = AttnSale;

                            txtCustomerShip.Text = CustomerShip;
                            txtAddressShip.Text = AddressShip;
                            txtAddressShip2.Text = AddressShip2;
                            txtTelShip.Text = TelShip;
                            txtFaxShip.Text = FaxShip;
                            txtAttnShip.Text = AttnShip;





                        }
                    }
                }
            }
            catch { }
        }
        private void upDateCode()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (!txtInvNo.Text.Equals(""))
                {
                    //Update K,L,M,forweight,UnitPrice,Unit
                    db.sp_044_Inv_Export_UPDATEKLM(txtInvNo.Text);
                    int Gr = 0;
                   
                    ////
                    bool Gril = false;
                    string GLi = "";
                    string GG = "";

                    var ListD = db.tb_ExportDetails.Where(d => d.InvoiceNo.Equals(txtInvNo.Text)).OrderBy(o=>o.GroupPDA1).ToList();
                    foreach (var rd in ListD)
                    {
                        Gril = false;
                        GG = "";
                        
                        if (Convert.ToBoolean(rd.Grille) )
                        {
                            if (Convert.ToString(rd.GroupPDA1).Equals(""))
                            {
                                GG = "1";
                            }
                            if(!Convert.ToString(rd.GroupPDA1).Equals(""))
                            {
                                if (!Convert.ToString(rd.GroupPDA1).Equals(GLi))
                                {
                                    GG = "1";
                                    GLi = Convert.ToString(rd.GroupPDA1);
                                }
                                else
                                {
                                    GG = "";
                                }
                                //GLi = Convert.ToString(rd.GroupPDA1);
                            }

                        }


                        tb_ExportDetail dt = db.tb_ExportDetails.Where(t => t.id.Equals(rd.id)).FirstOrDefault();
                        if(dt!=null)
                        {
                            if(GG.Equals("1"))
                            {
                                Gr += 1;
                            }
                            dt.K = GG;
                            db.SubmitChanges();
                        }

                    }
                    //Stc//
                    
                    int PL = PalletCount;
                    PalletCount = PL - Gr;
                    txtSteelcase.Text = Gr.ToString();
                     txtPallet.Text = (PL - Gr).ToString();


                    tb_ExportList ev = db.tb_ExportLists.Where(s => s.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                    {
                        if(ev!=null)
                        {
                            txtCode.Text = ev.Code;
                            if(!txtCode.Text.Equals("") && Convert.ToString(ev.CustomerSale).Equals("") && Convert.ToString(ev.CustomerShip).Equals(""))
                            {
                                tb_InvoiceEx_Master ms = db.tb_InvoiceEx_Masters.Where(m => m.Code.Equals(txtCode.Text)).FirstOrDefault();
                                if(ms!=null)
                                {
                                    ev.CustomerSale = Convert.ToString(ms.Customer);
                                    ev.CustomerShip = Convert.ToString(ms.Customer);
                                    ev.AddressSale = Convert.ToString(ms.Address);
                                    ev.AddressShip = Convert.ToString(ms.Address);
                                    ev.AddressSale2 = Convert.ToString(ms.Address2);
                                    ev.AddressShip2 = Convert.ToString(ms.Address2);
                                    ev.TelSale = Convert.ToString(ms.Tel);
                                    ev.TelShip = Convert.ToString(ms.Tel);
                                    ev.FaxShip = Convert.ToString(ms.Fax);
                                    ev.FaxSale = Convert.ToString(ms.Fax);

                                    ev.AttnSale = Convert.ToString(ms.Attn);
                                    ev.AttnShip = Convert.ToString(ms.Attn);
                                    if(!Convert.ToString(ev.Attn).Equals(""))
                                    {
                                        ev.AttnShip = ev.Attn;
                                    }
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }

                    ////Update Find Stell Case//
                    //StellCase = 0;
                    //PalletCount = 0;
                    //var ListD = db.tb_ExportDetails.Where(d => d.InvoiceNo.Equals(txtInvNo.Text)).ToList();
                    //foreach(var rd in ListD)
                    //{

                    //}
                }
            }
        }

       

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Refresh Click
            LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //Save Click

            if(MessageBox.Show("ต้องการบันทึก หรือไม่!","บันทึก",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {
                    CalculateAmount("");
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ExportList ev = db.tb_ExportLists.Where(w => w.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                        if(ev!=null)
                        {
                            if(txtCRRNCY.Text.Equals(""))
                            {
                                if (txtCurrency.Text.ToLower().Contains("japan"))
                                {
                                    txtCRRNCY.Text = "Baht.";
                                }
                                else
                                {
                                    txtCRRNCY.Text = "USD.";
                                }
                            }
                            ev.LoadDate = dtLoadDate.Value;
                            ev.ETDDatex = dtETDDate.Value;
                            ev.ETADatex = dtETADate.Value;
                            ev.ShipVia = txtShippedVia.Text;
                            ev.ShippingMark = txtShippingMark.Text;
                            ev.AirType = txtAirType.Text;
                            ev.Code = txtCode.Text;
                            ev.InvoiceOrder = txtInvoiceOrder.Text;
                            ev.CountryOriginal = txtCountry.Text;
                            ev.Frieght = txtFreight.Text;
                            ev.ShippingBy = txtCarrier.Text;
                            ev.Country = txtCurrency.Text;
                            ev.paymentTerm = txtPaymentterm.Text;
                            ev.Attn = txtAttnShip.Text;
                            if(!Convert.ToBoolean(ev.InvoiceFlag))
                            {
                                ev.InvoiceFlag = true;
                                ev.InvoiceDate = DateTime.Now;
                            }

                            ////////////Address/////////////////////
                            ev.CustomerSale = txtCustomerSale.Text;
                            ev.CustomerShip = txtCustomerShip.Text;
                            ev.AddressSale = txtAddressSale.Text;
                            ev.AddressShip = txtAddressShip.Text;
                            ev.AddressSale2 = txtAddressSales2.Text;
                            ev.AddressShip2 = txtAddressShip2.Text;
                            ev.TelSale = txtTelSale.Text;
                            ev.TelShip = txtTelShip.Text;
                            ev.FaxShip = txtFaxShip.Text;
                            ev.FaxSale = txtFaxSale.Text;

                            ev.AttnSale = txtAttnSale.Text;
                            ev.AttnShip = txtAttnShip.Text;
                            ev.CRR = txtCRRNCY.Text;

                            //Find Order No//
                            int CRow = 0;
                            string OrderH = "";
                            string OLD = "";
                            var ListDetail = db.tb_ExportDetails.Where(dd => dd.InvoiceNo.Equals(txtInvNo.Text)
                            && !dd.OrderNo.Equals("")
                            ).OrderBy(o=>o.OrderNo).ToList();
                            foreach (var ld in ListDetail)
                            {
                               
                                CRow += 1;
                               
                                if (CRow == 1)
                                {
                                    OrderH = ld.OrderNo;
                                    if (ld.OrderNo.Length >= 3)
                                    {
                                        OLD = dbClss.Right(ld.OrderNo, 3);
                                    }
                                }
                                else
                                {
                                    if (ld.OrderNo.Length >= 3)
                                    {
                                        if (!dbClss.Right(ld.OrderNo, 3).Equals(OLD))
                                        {
                                            OLD = dbClss.Right(ld.OrderNo, 3);
                                            OrderH = OrderH + "," + OLD;
                                        }
                                    }
                                }
                            }
                            ev.OrderNoH = OrderH;
                           
                            db.SubmitChanges();
                            LoadData();
                        }
                    }

                    MessageBox.Show("Insert Completed.");

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void txtCurrency_TextChanged(object sender, EventArgs e)
        {
            if (txtCurrency.Text.ToLower().Contains("japan"))
            {
                txtCRRNCY.Text = "Baht.";
            }
            else
            {
                txtCRRNCY.Text = "USD.";
            }
        }

        private void btnUseFromCode2_Click(object sender, EventArgs e)
        {
            //SHIPTO
            if (MessageBox.Show("ต้องการดึงรายละเอียด ?", "ดึงข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!txtInvNo.Text.Equals("") && !txtCode.Text.Equals(""))
                    {


                        tb_InvoiceEx_Master ms = db.tb_InvoiceEx_Masters.Where(m => m.Code.Equals(txtCode.Text)).FirstOrDefault();
                        if (ms != null)
                        {

                            txtCustomerShip.Text = Convert.ToString(ms.Customer);
                            txtAddressShip.Text = Convert.ToString(ms.Address);
                            txtAddressShip2.Text = Convert.ToString(ms.Address2);
                            txtTelShip.Text = Convert.ToString(ms.Tel);
                            txtFaxShip.Text = Convert.ToString(ms.Fax);
                            txtAttnShip.Text = Convert.ToString(ms.Attn);


                        }


                    }
                }
            }
        }

        private void txtUseFromCode1_Click(object sender, EventArgs e)
        {
            //SOLD
            if (MessageBox.Show("ต้องการดึงรายละเอียด ?", "ดึงข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!txtInvNo.Text.Equals("") && !txtCode.Text.Equals(""))
                    {


                        tb_InvoiceEx_Master ms = db.tb_InvoiceEx_Masters.Where(m => m.Code.Equals(txtCode.Text)).FirstOrDefault();
                        if (ms != null)
                        {

                            txtCustomerSale.Text = Convert.ToString(ms.Customer);
                            txtAddressSale.Text = Convert.ToString(ms.Address);
                            txtAddressSales2.Text = Convert.ToString(ms.Address2);
                            txtTelSale.Text = Convert.ToString(ms.Tel);
                            txtFaxSale.Text = Convert.ToString(ms.Fax);
                            txtAttnSale.Text = Convert.ToString(ms.Attn);

                        }


                    }
                }
            }
        }

        private void PackingList_Click(object sender, EventArgs e)
        {
            //Print Packing List
            try
            {
                if (!txtInvNo.Text.Equals(""))
                {
                    if (MessageBox.Show("ต้องการเปิด Packing List.", "พิมพ์เอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_044_Inv_Export_Delete();
                           
                            string PLNo = "";
                            string PLNo2 = "";
                            string GroupA = "";
                            decimal CNetwet = 0;
                            decimal TotalNet = 0;
                            decimal TotalGross = 0;
                            decimal Grossw = 0;
                            decimal TotalQty = 0;
                            int SortR = 0;
                            var Listdetail = db.sp_044_Inv_Export_ListDetail(txtInvNo.Text).ToList();

                            int CC = 0;
                            bool Grilled = false;
                            

                            foreach (var rd in Listdetail)
                            {
                                /////////////////////////CLEAR
                                Grossw = 0;
                                SortR += 1;
                                Grilled = false;
                                PLNo2 = "";
                                /////////////////////////
                                if (PLNo.Equals(rd.PalletNo))
                                {
                                    // PLNo = "";
                                    PLNo = rd.PalletNo;
                                    PLNo2 = "";
                                   
                                }
                                else
                                {
                                    PLNo = rd.PalletNo;
                                    PLNo2 = rd.PalletNo;
                                }
                                if (!Convert.ToString(rd.K).Equals(""))
                                        Grilled = true;

                                CC += 1;
                                tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                em.SortN = SortR;
                                em.InoviceNo = txtInvNo.Text.ToUpper();
                                em.PalletNo = PLNo2;
                                em.SteelCase = Grilled;
                                em.Description = rd.PartName;
                                string PartNo = rd.CustItem.ToUpper().Trim();
                                if (!Convert.ToString(rd.M).Equals(""))
                                {
                                    PartNo = rd.PartNo.ToString().ToUpper();
                                }
                                em.ProductionCode = rd.CustItem.ToUpper().Trim();
                                em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                em.Pallet = Convert.ToInt32(txtPallet.Text) ;
                                em.Qty = Convert.ToDecimal(rd.Qty);

                                //Net Weight//
                                decimal.TryParse(db.getI_ShelfNetweight_Dynamics(rd.PartNo.ToString()), out CNetwet);
                                if (CNetwet == 0)
                                    CNetwet = Convert.ToDecimal(rd.ForNetWet);

                                em.NetWeight = Convert.ToDecimal(rd.Qty)*CNetwet;
                                TotalNet+= Convert.ToDecimal(rd.Qty) * CNetwet;
                                TotalQty += Convert.ToDecimal(rd.Qty);

                                if ((Convert.ToDecimal(rd.Qty) * CNetwet) > 0)
                                {
                                    if (PLNo2.Equals(""))
                                    {
                                        em.GrossWeight = Convert.ToDecimal(rd.Qty) * CNetwet;
                                        Grossw = Convert.ToDecimal(rd.Qty) * CNetwet;
                                    }
                                    else if (Convert.ToBoolean(rd.Grille))
                                    {
                                        em.GrossWeight = (Convert.ToDecimal(rd.Qty) * CNetwet) + 80;
                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 80;
                                    }
                                    else
                                    {
                                        em.GrossWeight = (Convert.ToDecimal(rd.Qty) * CNetwet) + 30;
                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 30;
                                    }

                                    TotalGross += Grossw;
                                }
                                else
                                {
                                    em.GrossWeight = 0;
                                }


                                em.GroupA = GroupA;
                                em.TotalGrossWeight = 0;
                                em.TotalNetWeigth = 0;
                                em.CodeNo = rd.PartNo.ToString();
                                

                                db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                db.SubmitChanges();



                            }

                            if(CC>0)
                            {
                               // MessageBox.Show("=>"+TotalNet.ToString()+","+TotalGross.ToString());
                                //Print//
                                Report.Reportx1.Value = new string[1];
                                Report.Reportx1.Value[0] = txtInvNo.Text;
                                Report.Reportx1.WReport = "InvoiceEx";
                                Report.Reportx1 op = new Report.Reportx1("InvoiceEx01.rpt");
                                op.Show();
                            }


                        }
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default;  MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            PrintFOB(0);

        }
        private void PrintFOB(int ACC)
        {
            //Print FOB
            //แยก Comercial//
            try
            {
                if (!txtInvNo.Text.Equals(""))
                {
                    if (MessageBox.Show("ต้องการพิมพ์รายงาน FOB ?.", "พิมพ์เอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_044_Inv_Export_Delete();

                            string PLNo = "";
                            string GroupA = "";
                            decimal CNetwet = 0;
                            decimal TotalNet = 0;
                            decimal TotalGross = 0;
                            decimal Grossw = 0;
                            decimal TotalQty = 0;
                            string PartNo = "";
                            int SortR = 0;
                            int STC = 0;

                            //For Gross,NetWeight//
                            var ListDetail01 = db.sp_044_Inv_Export_ListDetail(txtInvNo.Text).ToList();
                            foreach (var rd in ListDetail01)
                            {
                                //Net Weight//
                                if (PLNo.Equals(rd.PalletNo))
                                {
                                    PLNo = "";
                                }
                                else
                                {
                                    PLNo = rd.PalletNo;
                                }

                                decimal.TryParse(db.getI_ShelfNetweight_Dynamics(rd.PartNo.ToString()), out CNetwet);
                                if (CNetwet == 0)
                                    CNetwet = Convert.ToDecimal(rd.ForNetWet);

                                TotalNet += Convert.ToDecimal(rd.Qty) * CNetwet;
                                TotalQty += Convert.ToDecimal(rd.Qty);

                                if ((Convert.ToDecimal(rd.Qty) * CNetwet) > 0)
                                {

                                    if (PLNo.Equals(""))
                                    {

                                        Grossw = Convert.ToDecimal(rd.Qty) * CNetwet;
                                    }
                                    else if (Convert.ToBoolean(rd.Grille))
                                    {

                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 80;
                                    }
                                    else
                                    {

                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 30;
                                    }

                                    TotalGross += Grossw;

                                }
                            }

                            var Listdetail = db.sp_044_Inv_Export_ListDetailFOB(txtInvNo.Text).ToList();
                            int CC = 0;
                            bool Grilled = false;
                            foreach (var rd in Listdetail)
                            {
                                /////////////////////////CLEAR
                                Grossw = 0;
                                Grilled = false;
                                /////////////////////////
                                GroupA = "Commercial";

                                if (!Convert.ToString(rd.K).Equals(""))
                                {
                                    STC += 1;
                                }
                                if (!Convert.ToString(rd.M).Equals(""))
                                {
                                    GroupA = "NoCommercial";
                                }
                                CC += 1;

                                if (!Convert.ToString(rd.K).Equals(""))
                                    Grilled = true;

                                tb_InvoiceExTemp ckc = db.tb_InvoiceExTemps.Where(kc => kc.InoviceNo.Equals(txtInvNo.Text) && kc.ProductionCode.Equals(rd.CustItem)).FirstOrDefault();
                                if (ckc != null && !Convert.ToString(rd.CustItem).Trim().Equals("-"))
                                {
                                    //if Dupclicate Update Qty
                                    decimal CQty = Convert.ToDecimal(ckc.Qty);
                                    CQty += Convert.ToDecimal(rd.Qty);
                                    ckc.Qty = CQty;
                                    ckc.Amount = ckc.UnitCost * CQty;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    SortR += 1;
                                    tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                    em.SortN = SortR;
                                    em.InoviceNo = txtInvNo.Text.ToUpper();
                                    em.PalletNo = PLNo;
                                    em.SteelCase = Grilled;
                                    em.Description = rd.PartName;

                                    string PartNo1 = rd.CustItem.ToUpper().Trim();                                    
                                    em.ProductionCode = PartNo1;                                   
                                    em.CodeNo = rd.PartNo.ToString();
                                    em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                    em.Pallet = Convert.ToInt32(txtPallet.Text);
                                    em.Qty = Convert.ToDecimal(rd.Qty);
                                    em.UnitCost = Convert.ToDecimal(rd.UnitCost);
                                    em.Amount = Convert.ToDecimal(rd.Qty) * Convert.ToDecimal(rd.UnitCost);
                                    em.GroupA = GroupA;
                                    em.TotalGrossWeight = 0;
                                    em.TotalNetWeigth = 0;
                                    em.Unit = "PCS";
                                    db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                    db.SubmitChanges();
                                }

                            }

                            if (CC > 0)
                            {
                                //Add Steel Case 1 Row//
                                if ((STC > 0))
                                {
                                    tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                    em.SortN = (SortR + 1);
                                    em.InoviceNo = txtInvNo.Text.ToUpper();
                                    em.PalletNo = PLNo;
                                    em.SteelCase = true;
                                    em.Description = "STEEL CASE";
                                    em.ProductionCode = "-";
                                    em.CodeNo = "-";
                                    em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                    em.Pallet = Convert.ToInt32(txtPallet.Text);
                                    em.Qty = Convert.ToDecimal(txtSteelcase.Text);
                                    em.UnitCost = 2800;
                                    em.Amount = 2800 * Convert.ToDecimal(txtSteelcase.Text);
                                    em.GroupA = "NoCommercial";
                                    em.TotalGrossWeight = 0;
                                    em.TotalNetWeigth = 0;
                                    em.Unit = "CASE";
                                    db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                    db.SubmitChanges();
                                }


                                //Update Netweight,Grossweight,PL,STC
                                tb_InvoiceExFob fb = db.tb_InvoiceExFobs.Where(f => f.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                                if (fb != null)
                                {
                                    fb.PL = Convert.ToInt32(txtPallet.Text);
                                    fb.STC = Convert.ToInt32(txtSteelcase.Text);

                                    fb.NetWeight = TotalNet;
                                    fb.GrossWeight = TotalGross;
                                    db.SubmitChanges();
                                }

                                //Print//
                                if (ACC.Equals(0))
                                {
                                    string FileFob = "InvoiceExFOB.rpt";

                                    if (txtShippedVia.Text.ToLower().Contains("air"))
                                    {
                                        //FileFob = "InvoiceExFOB_Collect.rpt";
                                        FileFob = "InvoiceExFOB_AIR.rpt";
                                    }
                                    else
                                    {
                                        if(txtFreight.Text.ToLower().Equals("collect"))
                                        {
                                            FileFob = "InvoiceExFOB_SEA_COLLECT.rpt";
                                        }
                                    }


                                    Report.Reportx1.Value = new string[1];
                                    Report.Reportx1.Value[0] = txtInvNo.Text;
                                    Report.Reportx1.WReport = "InvoiceEx";
                                    Report.Reportx1 op = new Report.Reportx1(FileFob);
                                    op.Show();
                                }
                                else
                                {
                                    string FileFob = "InvoiceExFOBExcel.rpt";

                                    if (txtShippedVia.Text.ToLower().Contains("air"))
                                    {                                      
                                        FileFob = "InvoiceExFOBExcel_Air.rpt";
                                    }
                                    else
                                    {
                                        if (txtFreight.Text.ToLower().Equals("collect"))
                                        {
                                            FileFob = "InvoiceExFOBExcel_Collect.rpt";
                                        }
                                    }


                                    Report.Reportx1.Value = new string[1];
                                    Report.Reportx1.Value[0] = txtInvNo.Text;
                                    Report.Reportx1.WReport = "InvoiceEx";
                                    Report.Reportx1 op = new Report.Reportx1("InvoiceExFOBExcel.rpt");
                                    op.Show();
                                }
                               
                            }


                        }
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
       

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            //Print CIF
            //Print FOB
            //แยก Comercial//
            try
            {
                if (!txtInvNo.Text.Equals(""))
                {
                    if (MessageBox.Show("ต้องการพิมพ์รายงาน CIF ?.", "พิมพ์เอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_044_Inv_Export_Delete();

                            string PLNo = "";
                            string PLNo2 = "";
                            string GroupA = "";
                            decimal CNetwet = 0;
                            decimal TotalNet = 0;
                            decimal TotalGross = 0;
                            decimal Grossw = 0;
                            decimal TotalQty = 0;
                            string PartNo = "";
                            int SortR = 0;
                            int SortC = 0;
                            int countNo = 0;
                            int STC = 0;

                            //For Gross,NetWeight//
                            var ListDetail01 = db.sp_044_Inv_Export_ListDetail(txtInvNo.Text).ToList();
                            foreach (var rd in ListDetail01)
                            {
                                //Net Weight//
                                PLNo2 = "";
                                if (PLNo.Equals(rd.PalletNo))
                                {
                                    PLNo = rd.PalletNo;
                                    PLNo2 = "";
                                }
                                else
                                {
                                    PLNo = rd.PalletNo;
                                    PLNo2 = rd.PalletNo;
                                }

                                decimal.TryParse(db.getI_ShelfNetweight_Dynamics(rd.PartNo.ToString()), out CNetwet);
                                if (CNetwet == 0)
                                    CNetwet = Convert.ToDecimal(rd.ForNetWet);

                                TotalNet += Convert.ToDecimal(rd.Qty) * CNetwet;
                                TotalQty += Convert.ToDecimal(rd.Qty);

                                if ((Convert.ToDecimal(rd.Qty) * CNetwet) > 0)
                                {

                                    if (PLNo.Equals(""))
                                    {

                                        Grossw = Convert.ToDecimal(rd.Qty) * CNetwet;
                                    }
                                    else if (Convert.ToBoolean(rd.Grille))
                                    {

                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 80;
                                    }
                                    else
                                    {

                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 30;
                                    }

                                    TotalGross += Grossw;

                                }
                            }

                            var Listdetail = db.sp_044_Inv_Export_ListDetailFOB(txtInvNo.Text).ToList();
                            int CC = 0;
                            bool Grilled = false;
                            foreach (var rd in Listdetail)
                            {
                                /////////////////////////CLEAR
                                Grossw = 0;
                                Grilled = false;
                                CC += 1;
                                /////////////////////////
                                GroupA = "Commercial";

                                if (!Convert.ToString(rd.K).Equals(""))
                                {
                                    STC += 1;
                                }
                                if (!Convert.ToString(rd.M).Equals(""))
                                {
                                    GroupA = "NoCommercial";
                                }
                                

                                if (!Convert.ToString(rd.K).Equals(""))
                                    Grilled = true;

                                //if (GroupA.Equals("Commercial"))
                                //{
                                //    SortR += 1;
                                //    countNo = SortR;
                                //}
                                //else
                                //{
                                //    SortC += 1;
                                //    countNo = SortC;
                                //}

                                tb_InvoiceExTemp ckc = db.tb_InvoiceExTemps.Where(kc => kc.InoviceNo.Equals(txtInvNo.Text) && kc.ProductionCode.Equals(rd.CustItem)).FirstOrDefault();
                                if (ckc != null && !Convert.ToString(rd.CustItem).Trim().Equals("-"))
                                {
                                    //if Dupclicate Update Qty
                                    decimal CQty = Convert.ToDecimal(ckc.Qty);
                                    CQty += Convert.ToDecimal(rd.Qty);
                                    ckc.Qty = CQty;
                                    ckc.Amount = ckc.UnitCost * CQty;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    SortR += 1;
                                    tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                    em.SortN = SortR;
                                    em.InoviceNo = txtInvNo.Text.ToUpper();
                                    em.PalletNo = PLNo2;
                                    em.SteelCase = Grilled;
                                    em.Description = rd.PartName;
                                    em.ProductionCode = rd.CustItem.ToUpper().Trim();//rd.PartNo.ToString().ToUpper();
                                    em.CodeNo = rd.PartNo.ToString();
                                    em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                    em.Pallet = Convert.ToInt32(txtPallet.Text);
                                    em.Qty = Convert.ToDecimal(rd.Qty);
                                    em.UnitCost = Convert.ToDecimal(rd.UnitCost);
                                    em.Amount = Convert.ToDecimal(rd.Qty) * Convert.ToDecimal(rd.UnitCost);
                                    em.GroupA = GroupA;
                                    em.TotalGrossWeight = 0;
                                    em.TotalNetWeigth = 0;
                                    em.Unit = "PCS";
                                    db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                    db.SubmitChanges();
                                }

                            }

                            if (CC > 0)
                            {
                                //Add Steel Case 1 Row//
                                if (STC > 0)
                                {
                                    //tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                    //em.SortN = (SortR + 1);
                                    //em.InoviceNo = txtInvNo.Text.ToUpper();
                                    //em.PalletNo = PLNo;
                                    //em.SteelCase = true;
                                    //em.Description = "STEEL CASE";
                                    //em.ProductionCode = "-";
                                    //em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                    //em.Pallet = Convert.ToInt32(txtPallet.Text);
                                    //em.Qty = Convert.ToDecimal(txtSteelcase.Text);
                                    //em.UnitCost = 2800;
                                    //em.Amount = 2800 * Convert.ToDecimal(txtSteelcase.Text);
                                    //em.GroupA = "NoCommercial";
                                    //em.TotalGrossWeight = 0;
                                    //em.TotalNetWeigth = 0;
                                    //em.Unit = "CASE";
                                    //db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                    //db.SubmitChanges();
                                }


                                //Update Netweight,Grossweight,PL,STC
                                tb_InvoiceExFob fb = db.tb_InvoiceExFobs.Where(f => f.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                                if (fb != null)
                                {
                                    fb.PL = Convert.ToInt32(txtPallet.Text);
                                    fb.STC = Convert.ToInt32(txtSteelcase.Text);

                                    fb.NetWeight = TotalNet;
                                    fb.GrossWeight = TotalGross;
                                    db.SubmitChanges();
                                }

                                //Print//
                                Report.Reportx1.Value = new string[1];
                                Report.Reportx1.Value[0] = txtInvNo.Text;
                                Report.Reportx1.WReport = "InvoiceEx";
                                Report.Reportx1 op = new Report.Reportx1("InvoiceExCIF.rpt");
                                op.Show();
                            }


                        }
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการอัพเดตราคา ?","อัพเดตราคา",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                CalculateAmount("");

            }
        }

        private void LoadFOBRate()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_InvoiceExFob rs = db.tb_InvoiceExFobs.Where(s => s.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                    if (rs != null)
                    {
                        txtFOBTotal1.Text = Convert.ToDecimal(rs.TotalAmount).ToString("###,###,##0.00");
                        txtFobRate1.Text = Convert.ToDecimal(rs.TotalFobRate1).ToString("###,###,##0.00");
                        txtFOBRate2.Text = Convert.ToDecimal(rs.TotalFobRate2).ToString("###,###,##0.00");
                        txtFobStam.Text = Convert.ToDecimal(rs.FobStam).ToString("###,###,##0.00");
                        txtFobStameRate.Text = Convert.ToDecimal(rs.FobStamRate).ToString("###,###,##0.00");
                        txtFOBCC1.Text = Convert.ToDecimal(rs.FobCC).ToString("###,###,##0.00");
                        txtCalPallet.Text = Convert.ToDecimal(rs.FobPallet).ToString("###,###,##0.00");
                        txtUseRate.Text = Convert.ToDecimal(rs.FobUseRate).ToString("###,###,##0.00");


                        txtInsurance.Text = Convert.ToDecimal(rs.TotalInsurance).ToString("###,###,##0.00");
                        txtFreightAmount.Text = Convert.ToDecimal(rs.TotalFreight).ToString("###,###,##0.00");
                        txtTotalFOB.Text = Convert.ToDecimal(rs.TotalAmountFOB).ToString("###,###,##0.00");
                        txtNoComercial.Text = Convert.ToDecimal(rs.NoComercial).ToString("###,###,##0.00");
                        txtAmountTextFOB.Text = Convert.ToString(rs.AmountText);

                        txtPaymentText.Text = Convert.ToString(rs.PaymentText);
                        txtTotalText.Text = Convert.ToString(rs.TotalText);
                        txtFreight5C.Text = Convert.ToDecimal(rs.Freight5C).ToString("###,###,##0.##");
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void CalculateAmount(string PType)
        {
            //LoadFOBRate();
            if (!txtInvNo.Text.Equals(""))
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        if (txtAmountTextFOB.Text.Equals(""))
                        {
                            if (txtFreight.Text.ToLower().Contains("prepaid"))
                            {
                                txtAmountTextFOB.Text = "(CIF. JAPAN)";
                            }
                            else if (txtFreight.Text.ToLower().Contains("ddp"))
                            {
                                txtAmountTextFOB.Text = "(CIF. JAPAN)";

                            }
                            else if (txtFreight.Text.ToLower().Contains("correct"))
                            {
                                txtAmountTextFOB.Text = "(FOB. BKK)";
                            }
                            else if (txtFreight.Text.ToLower().Contains("exw"))
                            {
                                txtAmountTextFOB.Text = "(Exw-Thailand)";
                            }
                            else
                            {
                                txtAmountTextFOB.Text = "(CIF. JAPAN)";
                            }
                        }
                        if(txtTotalText.Text.Equals(""))
                        {
                            if (txtFreight.Text.ToLower().Contains("prepaid"))
                            {
                                txtTotalText.Text = "(FOB LCB)";
                            }
                            else if (txtFreight.Text.ToLower().Contains("ddp"))
                            {
                                txtTotalText.Text = "(FOB BKK)";
                            }                           
                            else
                            {
                                txtTotalText.Text = "(FOB LCB)";
                            }
                        }

                        txtFreight5C.Text = "0";

                        if (txtFreight5C.Text.Equals("") || txtFreight5C.Text.Equals("0"))
                        {
                            if (txtShippedVia.Text.ToLower().Contains("air"))
                            {
                                if (txtAirType.Text.ToLower().Contains("cargo"))
                                {
                                    if(txtFreight.Text.ToLower().Contains("ddp") || txtFreight.Text.ToLower().Contains("prepaid"))
                                            txtFreight5C.Text = "5";
                                }
                            }
                        }

                        if(txtFreight.Text.ToLower().Contains("ddp") && txtShippedVia.Text.ToLower().Contains("air"))
                        {
                            txtAmountTextFOB.Text = "(DDP JAPAN.)";
                            //if(txtAirType.Text.ToLower().Contains("cargo"))
                            //{
                            //    txtAmountTextFOB.Text = "(FOB BKK)";
                            //}
                        }
                        if (txtFreight.Text.ToLower().Contains("exw") && txtShippedVia.Text.ToLower().Contains("air"))
                        {
                            txtAmountTextFOB.Text = "(EXW Thailand.)";
                        }
                        if (txtFreight.Text.ToLower().Contains("prepaid") && txtShippedVia.Text.ToLower().Contains("air"))
                        {
                            txtAmountTextFOB.Text = "(CIF. JAPAN)";
                        }
                        if (txtFreight.Text.ToLower().Contains("prepaid") && !txtShippedVia.Text.ToLower().Contains("air"))
                        {
                            txtAmountTextFOB.Text = "(CIF. JAPAN)";
                        }
                        
                        
                        /////////////////////////
                        //if ( txtShippedVia.Text.ToLower().Contains("air"))
                        //{
                        //    txtAmountTextFOB2.Text = "(FOB BKK)";
                        //}


                        decimal TotalAmount = 0;
                        decimal FobRate1 = Convert.ToDecimal(txtFobRate1.Text);
                        decimal FobRate2 = Convert.ToDecimal(txtFOBRate2.Text);
                        decimal FobRate3 = 0;
                        decimal FobStam = 0;
                        decimal FobStemRate = Convert.ToDecimal(txtFobStameRate.Text);
                        decimal FobCC = 1;
                        decimal FobPallet = 370;
                        decimal FobUseExRate = Convert.ToDecimal(txtUseRate.Text);
                        decimal Insurance = 0;
                        decimal Freight = 0;
                        decimal TotalFob = 0;
                        decimal UnitCost = 0;
                        decimal NoComer = 0;
                        int PL = 0;
                        int Stl = 0;
                        int.TryParse(txtPallet.Text, out PL);
                        int.TryParse(txtSteelcase.Text, out Stl);
                        decimal Freight5c = 0;
                        decimal.TryParse(txtFreight5C.Text, out Freight5c);
                        //Update Cost in ExList//
                        var ListDetail = db.tb_ExportDetails.Where(s => s.InvoiceNo.Equals(txtInvNo.Text)).ToList();
                        foreach (var rd in ListDetail)
                        {
                            
                            
                            if (true)
                            {
                                // UnitCost = 0;
                                UnitCost = Convert.ToDecimal(db.get_CustDynamicsSaleOrderPrice(rd.CustNo, rd.OrderNo, rd.PartNo, ""));
                                tb_ExportDetail ed = db.tb_ExportDetails.Where(sd => sd.id == rd.id).FirstOrDefault();
                                if (ed != null)
                                {
                                    ed.UnitA = "PCS";
                                    ed.UnitCost = UnitCost;
                                    db.SubmitChanges();
                                }
                            }
                            else
                            {
                               
                               // UnitCost = Convert.ToDecimal(rd.UnitCost);
                            }



                            TotalAmount += (UnitCost * Convert.ToDecimal(rd.Qty));

                            if (!Convert.ToString(rd.K).Equals(""))
                            {
                                NoComer += 2800;
                                TotalAmount += 2800;
                            }

                            if (!Convert.ToString(rd.M).Equals(""))
                            {
                                //if (Convert.ToString(rd.PartName).ToLower().Contains("tray"))
                                //{
                                //    NoComer += 5 * Convert.ToDecimal(rd.Qty);
                                //}
                                //else
                                //{
                                    NoComer += Convert.ToDecimal(rd.UnitCost) * Convert.ToDecimal(rd.Qty);
                                //}
                            }
                        }
                        // MessageBox.Show(""+ TotalAmount.ToString());
                        TotalAmount = Math.Round(TotalAmount, 2);
                        FobRate3 = Math.Round((((TotalAmount * FobRate1) / 100) * FobRate2), 2);
                        FobStam = Math.Ceiling((FobRate3 * Convert.ToDecimal(txtFobStameRate.Text))/100);
                       
                        if (FobStam < 6)
                            FobCC = 1;
                        else
                            FobCC = 5;
                        if((PL+Stl)>20)
                        {
                            FobPallet = 700;
                        }


                        Insurance = Math.Round((FobRate3 + FobCC + FobStam), 0);
                        Freight = Math.Round((FobPallet * FobUseExRate),1);
                        TotalFob = (TotalAmount - Insurance) - Freight;




                        tb_InvoiceExFob fb = db.tb_InvoiceExFobs.Where(f => f.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                        if (fb != null)
                        {
                            //Update//

                            fb.TotalAmount = TotalAmount;
                            fb.TotalFobRate1 = FobRate1;
                            fb.TotalFobRate2 = FobRate2;
                            fb.TotalInsurance = Insurance;
                            fb.FobStam = FobStam;
                            fb.FobStamRate = FobStemRate;
                            fb.FobUseRate = FobUseExRate;
                            fb.TotalFreight = Freight;
                            fb.TotalAmountFOB = TotalFob;
                            fb.FobCC = FobCC;
                            fb.NoComercial = NoComer;
                            fb.FobPallet = FobPallet;
                            fb.AmountText = txtAmountTextFOB.Text.ToUpper();
                            fb.Freight5C = Freight5c;
                            if (TotalFob > 0)
                            {
                                txtPaymentText.Text = "T.T.REMITTANCE AT 60 DAYS AFTER B/L DATE";
                            }
                            else
                            {
                                txtPaymentText.Text = "No Commercial Value.";
                            }

                            if (txtPaymentText.Text.Equals(""))
                            {
                                fb.PaymentText = "T.T.REMITTANCE AT 60 DAYS AFTER B/L DATE";
                            }
                            else
                                fb.PaymentText = txtPaymentText.Text;


                            if (txtTotalText.Text.Equals(""))
                                fb.TotalText = "(FOB LCB)";
                            else
                                fb.TotalText = txtTotalText.Text;


                            db.SubmitChanges();

                        }
                        else
                        {
                            //Insert
                            tb_InvoiceExFob fb1 = new tb_InvoiceExFob();
                            fb1.InvoiceNo = txtInvNo.Text;
                            fb1.TotalAmount = TotalAmount;
                            fb1.TotalFobRate1 = FobRate1;
                            fb1.TotalFobRate2 = FobRate2;
                            fb1.TotalInsurance = Insurance;
                            fb1.FobStam = FobStam;
                            fb1.FobStamRate = FobStemRate;
                            fb1.FobUseRate = FobUseExRate;
                            fb1.TotalFreight = Freight;
                            fb1.TotalAmountFOB = TotalFob;
                            fb1.FobCC = FobCC;
                            fb1.NoComercial = NoComer;
                            fb1.FobPallet = FobPallet;
                            fb1.Freight5C = Freight5c;

                            if (TotalFob > 0)
                            {
                                txtPaymentText.Text = "T.T.REMITTANCE AT 60 DAYS AFTER B/L DATE";
                            }
                            else
                            {
                                txtPaymentText.Text = "No Commercial Value.";
                            }
                            if (txtAmountTextFOB.Text.Equals(""))
                                txtAmountTextFOB.Text = "(CIF. JAPAN)";
                            fb1.AmountText = txtAmountTextFOB.Text;
                            if (txtPaymentText.Text.Equals(""))
                            {
                                fb1.PaymentText = "T.T.REMITTANCE AT 60 DAYS AFTER B/L DATE";
                            }
                            else
                                fb1.PaymentText = txtPaymentText.Text;
                            if (txtTotalText.Text.Equals(""))
                                fb1.TotalText = "FOB LCB";
                            else
                                fb1.TotalText = txtTotalText.Text;

                            db.tb_InvoiceExFobs.InsertOnSubmit(fb1);
                            db.SubmitChanges();
                        }


                       
                        //Display//
                        // txtTotalFOB.Text = TotalAmount.ToString("###,###,##0.00#");
                    }
                    LoadFOBRate();

                }
                catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }

                this.Cursor = Cursors.Default;
            }
        }

        private void txtFobRate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            dbClss.CheckDigitDecimal(e);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                PrintFOB(1);
            }
            catch { }
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            //Print DDP/EXW
           
            //แยก Comercial//
            try
            {
                if (!txtInvNo.Text.Equals(""))
                {
                    if (MessageBox.Show("ต้องการพิมพ์รายงาน DDP/EXW ?.", "พิมพ์เอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_044_Inv_Export_Delete();

                            string PLNo = "";
                            string PLNo2 = "";
                            string GroupA = "";
                            decimal CNetwet = 0;
                            decimal TotalNet = 0;
                            decimal TotalGross = 0;
                            decimal Grossw = 0;
                            decimal TotalQty = 0;
                            string PartNo = "";
                            int SortR = 0;
                            int SortC = 0;
                            int countNo = 0;
                            int STC = 0;

                            //For Gross,NetWeight//
                            var ListDetail01 = db.sp_044_Inv_Export_ListDetail(txtInvNo.Text).ToList();
                            foreach (var rd in ListDetail01)
                            {
                                //Net Weight//
                                PLNo2 = "";
                                if (PLNo.Equals(rd.PalletNo))
                                {
                                    PLNo = rd.PalletNo;
                                    PLNo2 = "";
                                }
                                else
                                {
                                    PLNo = rd.PalletNo;
                                    PLNo2 = rd.PalletNo;
                                }

                                decimal.TryParse(db.getI_ShelfNetweight_Dynamics(rd.PartNo.ToString()), out CNetwet);
                                if (CNetwet == 0)
                                    CNetwet = Convert.ToDecimal(rd.ForNetWet);

                                TotalNet += Convert.ToDecimal(rd.Qty) * CNetwet;
                                TotalQty += Convert.ToDecimal(rd.Qty);

                                if ((Convert.ToDecimal(rd.Qty) * CNetwet) > 0)
                                {

                                    if (PLNo.Equals(""))
                                    {

                                        Grossw = Convert.ToDecimal(rd.Qty) * CNetwet;
                                    }
                                    else if (Convert.ToBoolean(rd.Grille))
                                    {

                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 80;
                                    }
                                    else
                                    {

                                        Grossw = (Convert.ToDecimal(rd.Qty) * CNetwet) + 30;
                                    }

                                    TotalGross += Grossw;

                                }
                            }

                            var Listdetail = db.sp_044_Inv_Export_ListDetailFOB(txtInvNo.Text).ToList();
                            int CC = 0;
                            bool Grilled = false;
                            foreach (var rd in Listdetail)
                            {
                                /////////////////////////CLEAR
                                Grossw = 0;
                                Grilled = false;
                                CC += 1;
                                /////////////////////////
                                GroupA = "Commercial";

                                if (!Convert.ToString(rd.K).Equals(""))
                                {
                                    STC += 1;
                                }
                                if (!Convert.ToString(rd.M).Equals(""))
                                {
                                    GroupA = "NoCommercial";
                                }


                                if (!Convert.ToString(rd.K).Equals(""))
                                    Grilled = true;

                                //if (GroupA.Equals("Commercial"))
                                //{
                                //    SortR += 1;
                                //    countNo = SortR;
                                //}
                                //else
                                //{
                                //    SortC += 1;
                                //    countNo = SortC;
                                //}

                                tb_InvoiceExTemp ckc = db.tb_InvoiceExTemps.Where(kc => kc.InoviceNo.Equals(txtInvNo.Text) && kc.ProductionCode.Equals(rd.CustItem)).FirstOrDefault();
                                if (ckc != null && !Convert.ToString(rd.CustItem).Trim().Equals("-"))
                                {
                                    //if Dupclicate Update Qty
                                    decimal CQty = Convert.ToDecimal(ckc.Qty);
                                    CQty += Convert.ToDecimal(rd.Qty);
                                    ckc.Qty = CQty;
                                    ckc.Amount = ckc.UnitCost * CQty;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    SortR += 1;
                                    tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                    em.SortN = SortR;
                                    em.InoviceNo = txtInvNo.Text.ToUpper();
                                    em.PalletNo = PLNo2;
                                    em.SteelCase = Grilled;
                                    em.Description = rd.PartName;
                                    em.ProductionCode = rd.CustItem.ToUpper().Trim();//rd.PartNo.ToString().ToUpper();
                                    em.CodeNo = rd.PartNo.ToString();
                                    em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                    em.Pallet = Convert.ToInt32(txtPallet.Text);
                                    em.Qty = Convert.ToDecimal(rd.Qty);
                                    em.UnitCost = Convert.ToDecimal(rd.UnitCost);
                                    em.Amount = Convert.ToDecimal(rd.Qty) * Convert.ToDecimal(rd.UnitCost);
                                    em.GroupA = GroupA;
                                    em.TotalGrossWeight = 0;
                                    em.TotalNetWeigth = 0;
                                    em.Unit = "PCS";
                                    db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                    db.SubmitChanges();
                                }

                            }

                            if (CC > 0)
                            {
                                //Add Steel Case 1 Row//
                                if (STC > 0)
                                {
                                    //tb_InvoiceExTemp em = new tb_InvoiceExTemp();
                                    //em.SortN = (SortR + 1);
                                    //em.InoviceNo = txtInvNo.Text.ToUpper();
                                    //em.PalletNo = PLNo;
                                    //em.SteelCase = true;
                                    //em.Description = "STEEL CASE";
                                    //em.ProductionCode = "-";
                                    //em.SteelCaseCount = Convert.ToInt32(txtSteelcase.Text);
                                    //em.Pallet = Convert.ToInt32(txtPallet.Text);
                                    //em.Qty = Convert.ToDecimal(txtSteelcase.Text);
                                    //em.UnitCost = 2800;
                                    //em.Amount = 2800 * Convert.ToDecimal(txtSteelcase.Text);
                                    //em.GroupA = "NoCommercial";
                                    //em.TotalGrossWeight = 0;
                                    //em.TotalNetWeigth = 0;
                                    //em.Unit = "CASE";
                                    //db.tb_InvoiceExTemps.InsertOnSubmit(em);
                                    //db.SubmitChanges();
                                }


                                //Update Netweight,Grossweight,PL,STC
                                tb_InvoiceExFob fb = db.tb_InvoiceExFobs.Where(f => f.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                                if (fb != null)
                                {
                                    fb.PL = Convert.ToInt32(txtPallet.Text);
                                    fb.STC = Convert.ToInt32(txtSteelcase.Text);

                                    fb.NetWeight = TotalNet;
                                    fb.GrossWeight = TotalGross;
                                    db.SubmitChanges();
                                }

                                //Print//
                                Report.Reportx1.Value = new string[1];
                                Report.Reportx1.Value[0] = txtInvNo.Text;
                                Report.Reportx1.WReport = "InvoiceEx";
                                Report.Reportx1 op = new Report.Reportx1("InvoiceExCIF.rpt");
                                op.Show();
                            }


                        }
                    }
                }
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radRibbonBarGroup3_Click(object sender, EventArgs e)
        {

        }
    }
}
