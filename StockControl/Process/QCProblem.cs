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
    public partial class QCProblem : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCProblem()
        {
            InitializeComponent();
        }
     
        public QCProblem(string QCNox,string WOx)
        {
            InitializeComponent();
            QCNo = QCNox;
            WOs = WOx;
            lblQCNo.Text = "QCNo : " + QCNo;
        }
     
        TextBox LinkPage = new TextBox();
        string Link = "";
        string WOs = "";
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
            try
            {
                int ck = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    tb_QCProblem qcp = db.tb_QCProblems.Where(q => q.QCNo.Equals(QCNo)).FirstOrDefault();
                    if (qcp != null)
                    {
                        txtrdoOther.Text = qcp.TypeRemark;
                        txtProblemName.Text = qcp.ProblemName;
                        txtProblemFix.Text = qcp.ProblemFix;
                        txtProblemSeeby.Text = qcp.ProblemSeeBy;
                        txtProblemTime.Text = qcp.ProblemTime;
                        txtProblemWhere.Text = qcp.ProblemWare;
                        txtProblemWhy.Text = qcp.ProblemWhy;
                        txtFixby.Text = qcp.FixBy;
                        txtCheckBy.Text = qcp.CheckBy;
                        if (qcp.TypeProblem.Equals(rdo1.Text))
                        {
                            rdo1.IsChecked = true;
                        }
                        else if (qcp.TypeProblem.Equals(rdo2.Text))
                        {
                            rdo2.IsChecked = true;
                        }
                        else if (qcp.TypeProblem.Equals(rdo3.Text))
                        {
                            rdo3.IsChecked = true;
                        }
                        else if (qcp.TypeProblem.Equals(rdo4.Text))
                        {
                            rdo4.IsChecked = true;
                        }
                        else if (qcp.TypeProblem.Equals(rdo5.Text))
                        {
                            rdo5.IsChecked = true;

                        }
                    }
                }
            }
            catch { }



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
           
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //Select NCR List No."//
            try
            {
                Saveclick();
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //AddUnit();
                //DataLoad();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_QCProblem qp = db.tb_QCProblems.Where(w => w.QCNo.Equals(QCNo)).FirstOrDefault();
                    if (qp != null)
                    {
                        if (rdo1.IsChecked)
                            qp.TypeProblem = rdo1.Text;
                        else if (rdo2.IsChecked)
                            qp.TypeProblem = rdo2.Text;
                        else if (rdo3.IsChecked)
                            qp.TypeProblem = rdo3.Text;
                        else if (rdo4.IsChecked)
                            qp.TypeProblem = rdo4.Text;
                        else if (rdo5.IsChecked)
                            qp.TypeProblem = rdo5.Text;
                        qp.TypeRemark = txtrdoOther.Text;
                        qp.QCNo = QCNo;
                        qp.WONo = WOs;
                        qp.ProblemSeeBy = txtProblemSeeby.Text;
                        qp.ProblemName = txtProblemName.Text;
                        qp.ProblemTime = txtProblemTime.Text;
                        qp.ProblemWare = txtProblemWhere.Text;
                        qp.ProblemWhy = txtProblemWhy.Text;
                        qp.ProblemFix = txtProblemFix.Text;
                        qp.FixBy = txtFixby.Text;
                        qp.CheckBy = txtCheckBy.Text;
                        db.SubmitChanges();


                    }
                    else
                    {
                        tb_QCProblem qp2 = new tb_QCProblem();
                        if (rdo1.IsChecked)
                            qp2.TypeProblem = rdo1.Text;
                        else if (rdo2.IsChecked)
                            qp2.TypeProblem = rdo2.Text;
                        else if (rdo3.IsChecked)
                            qp2.TypeProblem = rdo3.Text;
                        else if (rdo4.IsChecked)
                            qp2.TypeProblem = rdo4.Text;
                        else if (rdo5.IsChecked)
                            qp2.TypeProblem = rdo5.Text;
                        qp2.TypeRemark = txtrdoOther.Text;
                        qp2.QCNo = QCNo;
                        qp2.WONo = WOs;
                        qp2.ProblemSeeBy = txtProblemSeeby.Text;
                        qp2.ProblemName = txtProblemName.Text;
                        qp2.ProblemTime = txtProblemTime.Text;
                        qp2.ProblemWare = txtProblemWhere.Text;
                        qp2.ProblemWhy = txtProblemWhy.Text;
                        qp2.ProblemFix = txtProblemFix.Text;
                        qp2.FixBy = txtFixby.Text;
                        qp2.CheckBy = txtCheckBy.Text;
                        qp2.CreateBy = dbClss.UserID;
                        qp2.CreateDate = DateTime.Now;
                        db.tb_QCProblems.InsertOnSubmit(qp2);
                        db.SubmitChanges();

                    }
                    MessageBox.Show("บันทึกสำเร็จ");
                    DataLoad();
                }
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
                // DataLoad();
                row = e.RowIndex;
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

        }

        private void radGridView1_CellClick_1(object sender, GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }
    }
}
