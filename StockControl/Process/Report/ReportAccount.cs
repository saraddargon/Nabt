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
    public partial class ReportAccount : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReportAccount()
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
            //firstOfNextMonth = Convert.ToDateTime(DateTime.Today.ToString("yyyy-mm-01"));
            string aa = DateTime.Today.ToString("yyyy-MM-01");
            dtDate1.Value = Convert.ToDateTime(aa);
            dtDate2.Value = lastOfThisMonth;
           // GETDTRow();
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
                }
        }
        private void DataLoad()
        {
            //dt.Rows.Clear();
            
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    try
                    {
                        // int year1 = 2017;

                        //var gd = (from ix in db.tb_ForcastCalculates
                        //          where ix.MMM == dbClss.getMonth(cboMonth.Text) && ix.YYYY == year1
                        //          select new { ix.YYYY, ix.MMM, Month = dbClss.getMonthRevest(ix.MMM)
                        //          , ix.CodeNo
                        //          , ItemDescription =db.tb_Items.Where(s => s.CodeNo == ix.CodeNo).Select(o => o.ItemDescription).FirstOrDefault()
                        //          ,ix.ForeCastQty,ix.Toolife_spc,ix.SumQty,ix.ExtendQty,ix.UsePerDay,ix.LeadTime,ix.KeepStock,ix.AddErrQty,ix.OrderQty}).ToList();
                        var gd = (from a in db.tb_Items

                                  select new {
                                      CodeNo = a.CodeNo,
                                      ItemDescription = a.ItemDescription,
                                      Order = 10,
                                      StockQty = 0,
                                      BackOrder = 0,
                                      UnitBuy = "PCS",
                                      PCSUnit = 1,
                                      LeadTime = a.Leadtime,
                                      MaxStock = a.MaximumStock,
                                      MinStock = a.MinimumStock,
                                      VendorNo = "V0001",
                                      VendorName = "HHL Interade Co.,LTD.",
                                      CreateDate = DateTime.Now,
                                      CreateBy = "Administrator",
                                      Status = "รับเข้าแล้ว",
                                      ItemName = a.ItemNo,
                                      Delivery = DateTime.Now,
                                      PRNo="PR201705-0001",
                                      ReceiveNo="RC201705-001",
                                      Cost=1000
                                   
                                  }).ToList();
                        //radGridView1.DataSource = gd;

                        //int rowcount = 0;
                        //foreach (var x in radGridView1.Rows)
                        //{
                        //    rowcount += 1;
                        //    x.Cells["dgvNo"].Value = rowcount;
                        //    x.Cells["dgvCodeTemp"].Value = x.Cells["CodeNo"].Value.ToString();
                        //    x.Cells["dgvCodeTemp2"].Value = x.Cells["VendorNo"].Value.ToString();
                        //    //x.Cells["dgvCodeTemp3"].Value = x.Cells["MMM"].Value.ToString();
                        //    //  MessageBox.Show("ss");
                        //    // x.Cells["ModelName"].ReadOnly = true;
                        //    //x.Cells["YYYY"].ReadOnly = true;
                        //    //x.Cells["MMM"].ReadOnly = true;
                        //}
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
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
            this.Cursor = Cursors.WaitCursor ;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                ////System.Diagnostics.Process.Start();
                //dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน "+dtDate1.Value.ToString("dd/MMM/yyyy")+"-"+dtDate2.Value.ToString("dd/MMM/yyyy"), "");
                //ck = true;

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");
                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_E008_ReportAccount(date1, date2, cboGroupType.Text);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน Report Account "+dtDate1.Value.ToString("dd/MMM/yyyy"), "");
                ck = true;


            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }
    }
}
