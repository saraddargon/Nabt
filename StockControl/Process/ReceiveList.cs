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
    public partial class ReceiveList : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReceiveList()
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
            dt.Columns.Add(new DataColumn("edit", typeof(bool)));
            dt.Columns.Add(new DataColumn("code", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Active", typeof(bool)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateBy", typeof(string)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
           // RMenu3.Click += RMenu3_Click;
           // RMenu4.Click += RMenu4_Click;
           // RMenu5.Click += RMenu5_Click;
           // RMenu6.Click += RMenu6_Click;
           //// radGridView1.ReadOnly = true;
           // radGridView1.AutoGenerateColumns = false;
           // GETDTRow();

            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            dtDate1.Value = firstDayOfMonth;
            dtDate2.Value = lastDayOfMonth;
            DataLoad();
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
            DeleteUnit();
            DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
            NewClick();

        }

        private void DataLoad()
        {
            
            int ck = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                radGridView1.AutoGenerateColumns = false;

                //radGridView1.DataSource = db.tb_ReceiveHDs.Where(r => (txtRCNo.Text == string.Empty || r.RCNo.Contains(txtRCNo.Text))
                //&& (Convert.ToDecimal(Convert.ToDateTime(r.CreateDate).ToString("YYYYMMdd"))>=Convert.ToDecimal(dtDate1.Value.ToString("YYYYMMdd"))
                //   && Convert.ToDecimal(Convert.ToDateTime(r.CreateDate).ToString("YYYYMMdd")) <= Convert.ToDecimal(dtDate2.Value.ToString("YYYYMMdd")))
                //).ToList();
                radGridView1.DataSource = db.sp_006_SelectListReceive(txtRCNo.Text, dtDate1.Value, dtDate2.Value).ToList();
                foreach (var x in radGridView1.Rows)
                {                 
                    ck += 1;
                    x.Cells["No"].Value = ck;
                }

            }


            
        }
        private bool CheckDuplicate(string code)
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
            // DataLoad();
            DataLoad();
        }
        private void NewClick()
        {
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            ////btnEdit.Enabled = false;
            //btnView.Enabled = true;
            //radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            //radGridView1.ReadOnly = false;
            ////btnEdit.Enabled = false;
            //btnView.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
           //// radGridView1.ReadOnly = true;
           // btnView.Enabled = false;
           // //btnEdit.Enabled = true;
            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AddUnit();
                DataLoad();
            }
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
            //else if (e.KeyData == (Keys.Control | Keys.N))
            //{
            //    if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        NewClick();
            //    }
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                //DeleteUnit();
                //DataLoad();
            
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
           dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
          
        }

        private void ImportData()
        {
          
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                string RC = radGridView1.Rows[e.RowIndex].Cells["RCNo"].Value.ToString();
                Receive rc = new Receive(RC);
                rc.Show();

            }catch { }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_TPICS_CreateReceive2();
                    MessageBox.Show("Calculate Completed.");
                    DataLoad();
                }
                this.Cursor = Cursors.Default ;

            }
            catch { }
        }
    }
}
