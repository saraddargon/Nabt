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
    public partial class ListPart : Telerik.WinControls.UI.RadRibbonForm
    {
        public ListPart(string CodeNox)
        {
            InitializeComponent();
            CodeNo = CodeNox;
            //this.Text = "ประวัติ "+ Screen;
        }
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ListPart(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public ListPart()
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

                    //var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo.Contains(txtCodeNo.Text)
                    //    && a.ItemNo.Contains(txtPartName.Text)
                    //    && a.ItemDescription.Contains(txtDescription.Text)
                    //    && a.VendorItemName.Contains(txtVendorName.Text))
                    //    .ToList();
                    var g=(from ix in db.sp_SelectItem() select ix).ToList();
                    if (g.Count > 0)
                    {

                        radGridView1.DataSource = g;
                        foreach (var x in radGridView1.Rows)
                        {
                            c += 1;
                            x.Cells["No"].Value = c;

                           // x.Cells["StockInv"].Value = (Convert.ToDecimal(db.Cal_QTY(Convert.ToString(x.Cells["CodeNo"].Value), "Invoice", 0)));
                           // x.Cells["StockDL"].Value = (Convert.ToDecimal(db.Cal_QTY(Convert.ToString(x.Cells["CodeNo"].Value), "Temp", 0)));
                           // x.Cells["StockBackOrder"].Value = (Convert.ToDecimal(db.Cal_QTY(Convert.ToString(x.Cells["CodeNo"].Value), "BackOrder", 0)));

                        }
                    }
                    
                        


                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Units where ix.UnitCode == code select ix).Count();
                if (i > 0)
                    ck = false;
                else
                    ck = true;
            }
            return ck;
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
            CreatePart sc = new CreatePart();
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
                    CodeNo_tt.Text = Convert.ToString(e.Row.Cells["CodeNo"].Value);
                    this.Close();
                }
                else
                {
                    CreatePart sc = new CreatePart(Convert.ToString(e.Row.Cells["CodeNo"].Value));
                    this.Cursor = Cursors.Default;
                    sc.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        DataTable dt_ShelfTag = new DataTable();
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
            dt_ShelfTag.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_ShelfTag.Columns.Add(new DataColumn("PartDescription", typeof(string)));
            dt_ShelfTag.Columns.Add(new DataColumn("ShelfNo", typeof(string)));


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
        private void Print_Shelftag_datatable()
        {
            try
            {
                dt_ShelfTag.Rows.Clear();

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtCodeNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        foreach (var gg in g)
                        {
                            dt_ShelfTag.Rows.Add(gg.CodeNo, gg.ItemDescription, gg.ShelfNo);
                        }
                        //DataTable DT =  StockControl.dbClss.LINQToDataTable(g);
                        //Reportx1 po = new Reportx1("Report_PurchaseRequest_Content1.rpt", DT, "FromDT");
                        //po.Show();

                        Report.Reportx1 op = new Report.Reportx1("002_BoxShelf_Part.rpt", dt_ShelfTag, "FromDL");
                        op.Show();
                    }
                    else
                        MessageBox.Show("not found.");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_PrintShelfTag_Click(object sender, EventArgs e)
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //delete ทิ้งก่อน
                    var deleteItem = (from ii in db.TempPrintShelfs where ii.UserName == Environment.UserName select ii);
                    foreach (var d in deleteItem)
                    {
                        db.TempPrintShelfs.DeleteOnSubmit(d);
                        db.SubmitChanges();
                    }

                    int c = 0;
                    string CodeNo = "";
                    radGridView1.EndEdit();
                    //insert
                    foreach (var Rowinfo in radGridView1.Rows)
                    {
                        if (StockControl.dbClss.TBo(Rowinfo.Cells["S"].Value).Equals(true))
                        {
                            CodeNo = StockControl.dbClss.TSt(Rowinfo.Cells["CodeNo"].Value);
                            var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == CodeNo).ToList();
                            if (g.Count() > 0)
                            {
                                
                                c += 1;
                                TempPrintShelf ps = new TempPrintShelf();
                                ps.UserName = Environment.UserName;
                                ps.CodeNo = g.FirstOrDefault().CodeNo;
                                ps.PartDescription = g.FirstOrDefault().ItemDescription;
                                ps.PartNo = g.FirstOrDefault().ItemNo;
                                ps.ShelfNo = g.FirstOrDefault().ShelfNo;
                                ps.Max = Convert.ToDecimal(g.FirstOrDefault().MaximumStock);
                                ps.Min = Convert.ToDecimal(g.FirstOrDefault().MinimumStock);
                                ps.OrderPoint = Convert.ToDecimal(g.FirstOrDefault().ReOrderPoint);
                                db.TempPrintShelfs.InsertOnSubmit(ps);
                                db.SubmitChanges();
                            }
                        }

                    }
                    if (c > 0)
                    {
                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = Environment.UserName;
                        Report.Reportx1.WReport = "002_BoxShelf_Part";
                        Report.Reportx1 op = new Report.Reportx1("002_BoxShelf_Part.rpt");
                        op.Show();
                    }
                    else
                        MessageBox.Show("not found.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_Print_Barcode_Click(object sender, EventArgs e)
        {
            try
            {
                dt_Kanban.Rows.Clear();
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                   
                        // Step 1 delete UserName
                        var deleteItem = (from ii in db.TempPrintKanbans where ii.UserName == Environment.UserName select ii);
                        foreach (var d in deleteItem)
                        {
                            db.TempPrintKanbans.DeleteOnSubmit(d);
                            db.SubmitChanges();
                        }

                        // Step 2 Insert to Table

                        int c = 0;
                        string CodeNo = "";
                        radGridView1.EndEdit();
                        //insert
                        foreach (var Rowinfo in radGridView1.Rows)
                        {
                            if (StockControl.dbClss.TBo(Rowinfo.Cells["S"].Value).Equals(true))
                            {
                                CodeNo = StockControl.dbClss.TSt(Rowinfo.Cells["CodeNo"].Value);
                                var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == CodeNo).ToList();
                                if (g.Count() > 0)
                                {
                                    c += 1;
                                    TempPrintKanban tm = new TempPrintKanban();
                                    tm.UserName = Environment.UserName;
                                    tm.CodeNo = g.FirstOrDefault().CodeNo;
                                    tm.PartDescription = g.FirstOrDefault().ItemDescription;
                                    tm.PartNo = g.FirstOrDefault().ItemNo;
                                    tm.VendorName = g.FirstOrDefault().VendorItemName;
                                    tm.ShelfNo = g.FirstOrDefault().ShelfNo;
                                    tm.GroupType = g.FirstOrDefault().GroupCode;
                                    tm.Max = Convert.ToDecimal(g.FirstOrDefault().MaximumStock);
                                    tm.Min = Convert.ToDecimal(g.FirstOrDefault().MinimumStock);
                                    tm.ReOrderPoint = Convert.ToDecimal(g.FirstOrDefault().ReOrderPoint);
                                    tm.ToolLife = Convert.ToDecimal(g.FirstOrDefault().Toollife);
                                    byte[] barcode = StockControl.dbClss.SaveQRCode2D(g.FirstOrDefault().CodeNo);
                                    tm.BarCode = barcode;
                                    db.TempPrintKanbans.InsertOnSubmit(tm);
                                    db.SubmitChanges();
                                    this.Cursor = Cursors.Default;
                                  
                                }
                            }
                        }
                        if (c > 0)
                        {
                            Report.Reportx1.Value = new string[2];
                            Report.Reportx1.Value[0] = Environment.UserName;
                            Report.Reportx1.WReport = "001_Kanban_Part";
                            Report.Reportx1 op = new Report.Reportx1("001_Kanban_Part.rpt");
                            op.Show();
                        }
                        else
                            MessageBox.Show("not found.");
                   
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
    }
}
