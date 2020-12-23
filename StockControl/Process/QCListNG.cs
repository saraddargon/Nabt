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
    public partial class QCListNG : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCListNG()
        {
            InitializeComponent();
        }
        public QCListNG(TextBox tx)
        {
            InitializeComponent();
            LinkPage = tx;
            Link = "Link";
        }
        public QCListNG(string QCNox)
        {
            InitializeComponent();
            QCNo = QCNox;
        }
        public QCListNG(RadTextBox rx,int AAx)
        {
            InitializeComponent();
            AA = AAx;
            NGidList = rx;
        }
        TextBox LinkPage = new TextBox();
        string Link = "";
        //private int RowView = 50;
        //private int ColView = 10;
        int AA = 0;
        RadTextBox NGidList = new RadTextBox();
        string QCNo = "";
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
            //dtDate1.Value = firstDayOfMonth;
            //dtDate2.Value = lastDayOfMonth;
            DataLoad();
            btnView.Enabled = false;
            if(AA>0)
            {
                btnView.Enabled = true;
            }

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

                radGridView1.DataSource = db.sp_46_QCSelectWO_06(QCNo).ToList();
          
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
            //Select NCR List No."//
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
                //string RC = radGridView1.Rows[e.RowIndex].Cells["RCNo"].Value.ToString();
                //Receive rc = new Receive(RC);
                //rc.ShowDialog();
                DataLoad();
            }
            catch { }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
               

            }
            catch { }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            ReqMoveStock rq = new ReqMoveStock("", QCNo);
            rq.Show();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Create NCR No.");
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            if(rowx>=0)
            {
                try
                {
                    int iid = 0;
                    int.TryParse(radGridView1.Rows[rowx].Cells["id"].Value.ToString(), out iid);
                    if (iid > 0)
                    {
                        QCProblemFix2 pp = new QCProblemFix2(iid);
                        pp.ShowDialog();
                        DataLoad();
                    }
                }
                catch { }
            }
        }
        int rowx = 0;
        private void radGridView1_CellClick_1(object sender, GridViewCellEventArgs e)
        {
            rowx = e.RowIndex;
        }
    }
}
