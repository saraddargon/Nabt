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
    public partial class ReportAdjustStock : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReportAdjustStock()
        {
            InitializeComponent();
        }

        //private int RowView = 50;
        //private int ColView = 10;
        DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {

            //dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            //dt.Columns.Add(new DataColumn("Order", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            //dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        }
        int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {
            DateTime firstOfNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
            dtDate1.Value = firstOfNextMonth;
            dtDate2.Value = lastOfThisMonth;
            GETDTRow();
            DefaultItem();

            crow = 0;
        }
        private void DefaultItem()
        {
            
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var gt = (from ix in db.tb_GroupTypes where ix.GroupActive == true select ix).ToList();
                //GridViewComboBoxColumn comboBoxColumn = this.radGridView1.Columns["GroupCode"] as GridViewComboBoxColumn;
                 cboGroupType.DisplayMember = "GroupCode";
                 cboGroupType.ValueMember = "GroupCode";
                 cboGroupType.DataSource = gt;
                cboGroupType.SelectedIndex = -1;

                //cboVendor.AutoCompleteMode = AutoCompleteMode.Append;
                //cboVendor.DisplayMember = "VendorName";
                //cboVendor.ValueMember = "VendorNo";
                //cboVendor.DataSource = (from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo, ix.VendorName }).ToList();
                //cboVendor.SelectedIndex = -1;
            }
        }
       

        private void btnView_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการออกรายงาน หรือไม่ ?","ออกรายงาน",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if(saveFileDialog1.FileName!="")
                {
                    if (GetData(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");
                    
                }
                
            }
        }
        private bool GetData(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_E007_ReportAdjust(txtADNo.Text,txtADNo.Text,date1,date2,"","",cboStatus.Text,txtCodeNo.Text,txtItemNo.Text,cboGroupType.Text);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (!cboVendor.Text.Equals(""))
            //    {
            //        txtVendorNo.Text = Convert.ToString(cboVendor.SelectedValue);
            //        //var I = (from ix in db.tb_Vendors select ix).Where(a => a.Active == true 
            //        //                && a.VendorName.Equals(cboVendor.Text)).ToList();
            //        //if (I.Count > 0)

            //    }
            //    else
            //        txtVendorNo.Text = "";
        }
}
}
