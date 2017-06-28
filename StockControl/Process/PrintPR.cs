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
    public partial class PrintPR : Telerik.WinControls.UI.RadRibbonForm
    {
        public PrintPR(string PR1xx,string PR2xx,string Typexx)
        {
            InitializeComponent();
            PR1 = PR1xx;
            PR2 = PR2xx;
            txtPRNo1.Text = PR1xx;
            txtPRNo2.Text = PR2xx;
            Type = Typexx;
        }
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public PrintPR(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public PrintPR()
        {
            InitializeComponent();
        }

        string PR1 = "";
        string PR2 = "";
        string Type = "";
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
            if (Type.Equals("PR"))
            {
                lblName.Text = "เลขที่ใบสั่งซื้อ";
            }
            else if (Type.Equals("Receive"))
                lblName.Text = "เลขที่รับสินค้า";
            else if (Type.Equals("Shipping"))
                lblName.Text = "เลขที่เบิกสินค้า";
            else if (Type.Equals("AdjustStock"))
                lblName.Text = "เลขที่ปรับปรุงสินค้า";
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            try
            {
               
                this.Cursor = Cursors.WaitCursor;
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                   
                    
                       
                //}
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
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

            DataLoad();

        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ////  dbClss.ExportGridCSV(radGridView1);
            //dbClss.ExportGridXlSX(radGridView1);
        }



        private void btnFilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            
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
                    this.Close();
                }
               
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
          

        }
       
        private void btn_Print_Barcode_Click(object sender, EventArgs e)
        {
            try
            {
                dt_Kanban.Rows.Clear();
               
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtPRNo2.Text).ToList();
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
                string PRNo1 = txtPRNo1.Text;
                string PRNo2 = txtPRNo2.Text;


                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (Type.Equals("PR"))
                    {
                       
                        var g = (from ix in db.sp_R005_ReportPR(PRNo1, PRNo2, DateTime.Now) select ix).ToList();
                        if (g.Count() > 0)
                        {
                            Report.Reportx1.Value = new string[2];
                            Report.Reportx1.Value[0] = PRNo1;
                            Report.Reportx1.Value[1] = PRNo2;
                            Report.Reportx1.WReport = "ReportPR3";
                            Report.Reportx1 op = new Report.Reportx1("ReportPR3.rpt");
                            op.Show();
                        }
                        else
                            MessageBox.Show("not found.");
                    }
                    else if (Type.Equals("Receive"))
                    {
                        var g = (from ix in db.sp_R006_ReportReceive(PRNo1, PRNo2, DateTime.Now) select ix).ToList();
                        if (g.Count() > 0)
                        {
                            Report.Reportx1.Value = new string[2];
                            Report.Reportx1.Value[0] = PRNo1;
                            Report.Reportx1.Value[1] = PRNo2;
                            Report.Reportx1.WReport = "ReportReceive2";
                            Report.Reportx1 op = new Report.Reportx1("ReportReceive2.rpt");
                            op.Show();
                        }
                        else
                            MessageBox.Show("not found.");
                    }
                    else if (Type.Equals("Shipping"))
                    {
                        var g = (from ix in db.sp_R007_ReportShipping(PRNo1, PRNo2, DateTime.Now) select ix).ToList();
                        if (g.Count() > 0)
                        {
                            Report.Reportx1.Value = new string[2];
                            Report.Reportx1.Value[0] = PRNo1;
                            Report.Reportx1.Value[1] = PRNo2;
                            Report.Reportx1.WReport = "ReportShipping2";
                            Report.Reportx1 op = new Report.Reportx1("ReportShipping2.rpt");
                            op.Show();
                        }
                        else
                            MessageBox.Show("not found.");
                    }
                    else if (Type.Equals("AdjustStock"))
                    {
                        var g = (from ix in db.sp_R008_ReportAdjustStock(PRNo1, PRNo2, DateTime.Now) select ix).ToList();
                        if (g.Count() > 0)
                        {
                            Report.Reportx1.Value = new string[2];
                            Report.Reportx1.Value[0] = PRNo1;
                            Report.Reportx1.Value[1] = PRNo2;
                            Report.Reportx1.WReport = "ReportAdjustStock";
                            Report.Reportx1 op = new Report.Reportx1("ReportAdjustStock.rpt");
                            op.Show();
                        }
                        else
                            MessageBox.Show("not found.");
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
