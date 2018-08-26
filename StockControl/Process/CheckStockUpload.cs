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
    public partial class CheckStockUpload : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public CheckStockUpload(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            this.Name = "CheckStockUpload";
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public CheckStockUpload()
        {
            this.Name = "CheckStockUpload";
            InitializeComponent();
        }

        string PR1 = "";
        string PR2 = "";
        string Type = "";    
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            //Select File//
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //Upload
            try
            {               
               // tb_CheckStockList <= Insert to this table
               //Update Status tb_CheckStock to "Waiting Check"
               //สามารถอัพโหลดใหม่ได้ โดยการ ให้ลบ ข้อมูลเก่าทั้งหมดออกก่อน


            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
