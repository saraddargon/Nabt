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
    public partial class QCList : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCList()
        {
            InitializeComponent();
        }
        public QCList(TextBox tx)
        {
            InitializeComponent();
            LinkPage = tx;
            LinkPage1 = "Link";
        }
        //private int RowView = 50;
        //private int ColView = 10;
        TextBox LinkPage = new TextBox();
        string LinkPage1 = "";
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
                radGridView1.DataSource = db.sp_46_QCSelectWO_04_Select(txtQCNo.Text, txtWONo.Text, txtPartNo.Text, ckDate.Checked, dtDate1.Value, dtDate2.Value).ToList();
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
                //string RC = radGridView1.Rows[e.RowIndex].Cells["RCNo"].Value.ToString();
                //Receive rc = new Receive(RC);
                //rc.ShowDialog();
               // DataLoad();
               if(LinkPage1.Equals("Link"))
                {
                    LinkPage.Text = radGridView1.Rows[e.RowIndex].Cells["QCNo"].Value.ToString();
                    this.Close();
                }
               
            }
            catch { }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (rowx >= 0)
                {
                    if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-PD-026_1"))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        dbShowData.PrintData(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString());
                        this.Cursor = Cursors.Default;
                    }
                    if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-QA-056_02_1"))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        dbShowData.PrintData5601(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString());
                        this.Cursor = Cursors.Default;
                    }
                    if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-QA-055_02_1"))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        dbShowData.PrintData5501(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString());
                        this.Cursor = Cursors.Default;
                    }
                    if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-PD-035_1"))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        dbShowData.PrintData035(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString());
                        this.Cursor = Cursors.Default;
                    }
                    if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-PD-033_1"))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        dbShowData.PrintData033(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString()
                            , radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString());
                        this.Cursor = Cursors.Default;
                    }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            if (rowx >= 0)
            {
                ReqMoveStock req = new ReqMoveStock(radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString(), radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString()
                    , radGridView1.Rows[rowx].Cells["LotNo"].Value.ToString());
                req.Show();
            }
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            NCRList mv = new NCRList();
            mv.Show();
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            string QCNo = "";
            if(rowx>=0)
            {
                QCNo = radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString();
                QCListNG lng = new QCListNG(QCNo);
                lng.Show();
            }
        }
        int rowx = 0;
        private void radGridView1_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            rowx = e.RowIndex;
        }

        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowx >= 0)
                {
                    string FormISO = radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString();
                    string TOPTAG = "";
                    string TOPTAG2 = "";
                    string Types = radGridView1.Rows[rowx].Cells["Dept"].Value.ToString();
                    string LineName = radGridView1.Rows[rowx].Cells["LineName"].Value.ToString();

                    if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-PD-026_1"))
                    {


                        
                        TOPTAG = "PQC," + radGridView1.Rows[rowx].Cells["WONo"].Value.ToString() + ",1,1," + radGridView1.Rows[rowx].Cells["LotNo"].Value.ToString() + ",No 1," +
                            radGridView1.Rows[rowx].Cells["PartNo"].Value.ToString() + ",026_1";
                        TOPTAG2 = "None";

                    }
                    else if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-QA-056_02_1"))
                    {

                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            var g = db.sp_46_QCSelectWO_09_QCTAGTopTAG(radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString()).FirstOrDefault();
                            if (g != null)
                            {
                                TOPTAG = g.BarcodeTag;
                                TOPTAG2 = g.GTAG;
                            }
                        }

                        //QCFormQC5601 pd026 = new QCFormQC5601(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString(), "FM-QA-056_02_1", TOPTAG, "");
                        //pd026.ShowDialog();
                    }
                    else if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-QA-055_02_1"))
                    {

                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            var g = db.sp_46_QCSelectWO_09_QCTAGTopTAG(radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString()).FirstOrDefault();
                            if (g != null)
                            {
                                TOPTAG = g.BarcodeTag;
                                TOPTAG2 = g.GTAG;
                            }
                        }

                        //QCFormQC5501 pd026 = new QCFormQC5501(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString(), "FM-QA-055_02_1", TOPTAG, "");
                        //pd026.ShowDialog();
                    }
                    else if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-PD-035_1"))
                    {

                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            var g = db.sp_46_QCSelectWO_09_QCTAGTopTAG(radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString()).FirstOrDefault();
                            if (g != null)
                            {
                                TOPTAG = g.BarcodeTag;
                                TOPTAG2 = g.GTAG;
                            }
                        }

                        //QCFormPD035 pd026 = new QCFormPD035(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString(), "FM-PD-035_1", TOPTAG, "");
                        //pd026.ShowDialog();
                    }
                    else if (radGridView1.Rows[rowx].Cells["FormISO"].Value.ToString().Equals("FM-PD-033_1"))
                    {

                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            var g = db.sp_46_QCSelectWO_09_QCTAGTopTAG(radGridView1.Rows[rowx].Cells["QCNo"].Value.ToString()).FirstOrDefault();
                            if (g != null)
                            {
                                TOPTAG = g.BarcodeTag;
                                TOPTAG2 = g.GTAG;
                            }
                        }

                        //QCFormPD033 pd026 = new QCFormPD033(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString(), "FM-PD-033_1", TOPTAG, "");
                        //pd026.ShowDialog();
                    }
                    QCFormPD026 pd026 = new QCFormPD026(radGridView1.Rows[rowx].Cells["WONo"].Value.ToString(), FormISO, TOPTAG, LineName, Types, TOPTAG2);
                    pd026.ShowDialog();
                }

            }
            catch { }
        }
    }
}
