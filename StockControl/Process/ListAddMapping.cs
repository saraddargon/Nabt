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
    public partial class ListAddMapping : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;

        public ListAddMapping(string Code)
        {
            this.Name = "ListAddMapping";
            InitializeComponent();
           
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

           

        }
        private void LoadBomNo()
        {
           
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (MessageBox.Show("ต้องการบันทึกหรือไม่ ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int pk = 0;
                        int.TryParse(txtPackagePallet.Text, out pk);
                        tb_MapItemTPIC tm = new tb_MapItemTPIC();
                        tm.Code = txtPartNo.Text;
                        tm.CustItemNo = txtCustomerItemNo.Text;
                        tm.CustItemName = txtCustomerItemName.Text;
                        tm.CustomerName = txtCustomerName.Text;                       
                        tm.CustomerNo = txtCustomerShortName.Text;
                        tm.PackagePallet = pk;
                        tm.Ac = 1;
                        tm.DWG = "-";
                       
                        db.tb_MapItemTPICs.InsertOnSubmit(tm);
                        db.SubmitChanges();
                        MessageBox.Show("Completed.");
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void txtBomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                LoadBomNo();
            }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
           
        }

        
    }
}
