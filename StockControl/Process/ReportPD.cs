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
    public partial class ReportPD : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ReportPD(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public ReportPD()
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
            radDateTimePicker1.Value = DateTime.Now;
            radDateTimePicker2.Value = DateTime.Now;


        }
        private void LoadData()
        {
            try
            {
                radGridView1.AutoGenerateColumns = true;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView1.DataSource = db.sp_011_PD_ReportDialy(radDateTimePicker1.Value, radDateTimePicker2.Value);
                }
            }
            catch { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            LoadData();
            dbClss.ExportGridXlSX(radGridView1);
        }

        private void btn_PrintPD1_Click(object sender, EventArgs e)
        {
            Report.Reportx1.Value = new string[2];
            Report.Reportx1.Value[0] = radDateTimePicker1.Value.ToString();
            Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "ReportPD02";
            Report.Reportx1 op = new Report.Reportx1("Report_Production02.rpt");
            op.Show();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            Report.Reportx1.Value = new string[2];
            Report.Reportx1.Value[0] = radDateTimePicker1.Value.ToString();
            Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "ReportPD01";
            Report.Reportx1 op = new Report.Reportx1("Report_Production01.rpt");
            op.Show();
        }
    }
}
