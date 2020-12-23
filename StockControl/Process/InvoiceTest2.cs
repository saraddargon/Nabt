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
    public partial class InvoiceTest2 : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public InvoiceTest2(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public InvoiceTest2()
        {
            InitializeComponent();
        }
        public InvoiceTest2(string INvx,int Steel,int PL)
        {
            InitializeComponent();
           
        }

       
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
           
        }

        private void LoadData()
        {
           
        }
        private void upDateCode()
        {
            
        }

       

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //Save Click
            /*
            if(MessageBox.Show("ต้องการบันทึก หรือไม่!","บันทึก",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_ExportList ev = db.tb_ExportLists.Where(w => w.InvoiceNo.Equals(txtInvNo.Text)).FirstOrDefault();
                        if(ev!=null)
                        {
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
                           
                            db.SubmitChanges();
                            LoadData();
                        }
                    }

                    MessageBox.Show("Insert Completed.");

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            */
        }

        private void txtCurrency_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnUseFromCode2_Click(object sender, EventArgs e)
        {
            
        }

        private void txtUseFromCode1_Click(object sender, EventArgs e)
        {
            
        }

        private void PackingList_Click(object sender, EventArgs e)
        {
            //Print Packing List
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            //Print FOB
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            //Print CIF

        }
    }
}
