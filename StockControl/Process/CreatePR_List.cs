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
    public partial class CreatePR_List : Telerik.WinControls.UI.RadRibbonForm
    {
        public CreatePR_List(string CodeNox)
        {
            InitializeComponent();
            CodeNo = CodeNox;
            //this.Text = "ประวัติ "+ Screen;
        }
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public CreatePR_List(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public CreatePR_List()
        {
            InitializeComponent();
        }

        string CodeNo = "";
        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
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
            dtDateFrom.Value = DateTime.Now;
            dtDateTo.Value = DateTime.Now;
            Set_dt_Print();
            //radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            DataLoad();
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            try
            {
                radGridView1.DataSource = null;
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    //radGridView1.DataSource = db.tb_Histories.Where(s => s.ScreenName == ScreenSearch).OrderBy(o => o.CreateDate).ToList();
                    int c = 0;
                  
                    DateTime inclusiveStart = dtDateFrom.Value.Date;
                    // Include the *whole* of the day indicated by searchEndDate
                    DateTime exclusiveEnd = dtDateTo.Value.Date.AddDays(1);

                    var g = (from ix in db.tb_PurchaseRequests select ix)
                        .Where(a => a.VendorNo.Contains(txtVendorNo.Text)
                        && a.TEMPNo.Contains(txtTempNo.Text)
                        && a.PRNo.Contains(txtPRNo.Text)
                        && a.VendorName.Contains(txtVendorName.Text)
                        && (a.CreateDate >= inclusiveStart
                                && a.CreateDate < exclusiveEnd)
                         )
                        .ToList();
                    if (g.Count > 0)
                    {

                        radGridView1.DataSource = g;
                        foreach (var x in radGridView1.Rows)
                        {
                            c += 1;
                            x.Cells["No"].Value = c;
                        }
                    }
                    
                       
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private int compart_date(DateTime date1, DateTime date2)
        {
            int result = 0;
            //DateTime date1 = new DateTime(2009, 8, 1, 0, 0, 0);
            //DateTime date2 = new DateTime(2009, 8, 1, 12, 0, 0);
            result = DateTime.Compare(date1, date2);

            

            return result;
        }
       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;
            radGridView1.AllowAddNewRow = false;
            radGridView1.Rows.AddNew();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = true;

            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;

            radGridView1.AllowAddNewRow = false;
            //DataLoad();
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

            DataLoad();

        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //  dbClss.ExportGridCSV(radGridView1);
            dbClss.ExportGridXlSX(radGridView1);
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

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CreatePR sc = new CreatePR();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (screen.Equals(1))
                {
                    CodeNo_tt.Text = Convert.ToString(e.Row.Cells["TempNo"].Value);
                    this.Close();
                }
                else
                {
                    CreatePR a = new CreatePR(Convert.ToString(e.Row.Cells["TempNo"].Value));
                    a.ShowDialog();
                    //this.Close();
                }
               
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
          
            dt_Kanban.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("PartNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("PartDescription", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("ShelfNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("VendorName", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("GroupType", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("ToolLife", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("Max", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("Min", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("ReOrderPoint", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("BarCode", typeof(Image)));

        }
       
        private void btn_Print_Barcode_Click(object sender, EventArgs e)
        {
            try
            {
                dt_Kanban.Rows.Clear();

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtVendorNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        foreach (var gg in g)
                        {
                            dt_Kanban.Rows.Add(gg.CodeNo, gg.ItemNo, gg.ItemDescription, gg.ShelfNo, gg.Leadtime, gg.VendorItemName, gg.GroupCode, gg.Toollife, gg.MaximumStock, gg.MinimumStock, gg.ReOrderPoint, gg.BarCode);
                        }
                        //DataTable DT =  StockControl.dbClss.LINQToDataTable(g);
                        //Reportx1 po = new Reportx1("Report_PurchaseRequest_Content1.rpt", DT, "FromDT");
                        //po.Show();

                        Report.Reportx1 op = new Report.Reportx1("001_Kanban_Part.rpt", dt_Kanban, "FromDL");
                        op.Show();
                    }
                    else
                        MessageBox.Show("not found.");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_PrintPR_Click(object sender, EventArgs e)
        {
            try
            {
                
                //dt_ShelfTag.Rows.Clear();
                string PRNo = "";
                if(radGridView1.Rows.Count > 0)
                    PRNo = StockControl.dbClss.TSt(radGridView1.CurrentRow.Cells["PRNo"].Value);

                PrintPR a = new PrintPR(PRNo, PRNo,"PR");
                a.ShowDialog();

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    var g = (from ix in db.sp_R002_ReportPR(PRNo, DateTime.Now) select ix).ToList();
                //    if (g.Count() > 0)
                //    {

                //        Report.Reportx1.Value = new string[2];
                //        Report.Reportx1.Value[0] = PRNo;
                //        Report.Reportx1.WReport = "ReportPR";
                //        Report.Reportx1 op = new Report.Reportx1("ReportPR.rpt");
                //        op.Show();

                //    }
                //    else
                //        MessageBox.Show("not found.");
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
