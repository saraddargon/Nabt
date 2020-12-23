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
    public partial class QCPicture : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCPicture()
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
         
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            
            
           
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
            
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
           
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
            
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
          
        }

        private void DataLoad()
        {
           


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code,string Code2)
        {
            bool ck = false;

         

            return ck;
        }

        private bool AddUnit()
        {
            bool ck = false;
          

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;
         
           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void NewClick()
        {
          
        }
        private void EditClick()
        {
            
        }
        private void ViewClick()
        {
           
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
           
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
               
            
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
           
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
           
        }

        private void ImportData()
        {
            
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
          
        }
    }
}
