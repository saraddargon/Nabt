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
    public partial class ReportLocal : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ReportLocal(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public ReportLocal()
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
            LoadDefault1();
        }
        private void LoadDefault1()
        {

            cboCustomer.DataSource = null;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var cp = db.sp_034_SelectCustomer_Dynamics().ToList();
                if (cp.Count > 0)
                {
                    cboCustomer.DataSource = cp;
                    cboCustomer.DisplayMember = "CustNo";
                    cboCustomer.ValueMember = "CustNo";
                }
            }
        }

        private void btn_PrintPD1_Click(object sender, EventArgs e)
        {
            Report.Reportx1.Value = new string[3];
            Report.Reportx1.Value[0] = radDateTimePicker1.Value.ToString();
            Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.Value[2] = cboCustomer.Text;
            Report.Reportx1.WReport = "ExportTAG1";
            Report.Reportx1 op = new Report.Reportx1("Report_LocalShipping.rpt");
            op.Show();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            Report.Reportx1.Value = new string[2];
            Report.Reportx1.Value[0] = radDateTimePicker1.Value.ToString();
            Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "ExportTAG1";
            Report.Reportx1 op = new Report.Reportx1("Report_LocalConfirm.rpt");
            op.Show();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            Report.Reportx1.Value = new string[2];
            Report.Reportx1.Value[0] = radDateTimePicker1.Value.ToString();
            Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "ExportTAG1";
            Report.Reportx1 op = new Report.Reportx1("Report_NotLocalConfirm.rpt");
            op.Show();
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                radGridView1.DataSource = null;
                radGridView1.DataSource = db.RP_ConfirmLocal(radDateTimePicker1.Value, radDateTimePicker2.Value);
                dbClss.ExportGridXlSX(radGridView1);


            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                radGridView1.DataSource = null;
                radGridView1.DataSource = db.RP_ShippingLocal_dynamics(radDateTimePicker1.Value, radDateTimePicker2.Value,cboCustomer.Text);
                dbClss.ExportGridXlSX(radGridView1);


            }
        }
    }
}
