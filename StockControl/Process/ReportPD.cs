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
                    radGridView1.DataSource = db.sp_011_PD_ReportDialy_Dynamics(txtWorkNo.Text,txtLineNo.Text,radDateTimePicker1.Value, radDateTimePicker2.Value);
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
            CalculatePD01();
            Report.Reportx1.Value = new string[4];
            Report.Reportx1.Value[0] = txtWorkNo.Text;
            Report.Reportx1.Value[1] = txtLineNo.Text;
            Report.Reportx1.Value[2] = radDateTimePicker1.Value.ToString();
            Report.Reportx1.Value[3] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "ReportPD01";
            Report.Reportx1 op = new Report.Reportx1("Report_Production01.rpt");
            op.Show();
        }
        private void CalculatePD01()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                progressBar1.Visible = true;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_34_Cal_ReportProuction01_Dynamics(radDateTimePicker1.Value, radDateTimePicker2.Value);

                    var listQ = db.sp_35_Cal_ReportProuction01(radDateTimePicker1.Value, radDateTimePicker2.Value).ToList();
                    if(listQ.Count>0)
                    {
                        progressBar1.Minimum = 1;
                        progressBar1.Maximum = listQ.Count + 1;
                        int count1 = 0;
                        int Qty10 = 0;
                        TimeSpan ck1 = new TimeSpan(8, 30, 0);
                        TimeSpan ck2 = new TimeSpan(20, 30, 0);
                        
                        foreach(var rd in listQ)
                        {
                            Qty10 = 0;
                            count1 += 1;
                            progressBar1.Value = count1;
                            progressBar1.PerformStep();
                            //2018-10-25 16:37:07.947
                            string CRDate = Convert.ToDateTime(rd.HDate).ToString("yyyy-MM-dd");
                            DateTime CRDateNext = Convert.ToDateTime(rd.HDate).AddDays(1);
                            string CRDateNext2= Convert.ToDateTime(CRDateNext).ToString("yyyy-MM-dd");

                            string CRDate1 = CRDateNext2 + " " + "08:30:00.000";
                            string CRDate2 = CRDate + " " + "20:30:00.000";

                            DateTime DateCheck1 = Convert.ToDateTime(CRDate1);
                            DateTime DateCheck2 = Convert.ToDateTime(CRDate2);
                            ////////////////////////////
                            if (rd.DayNight.Trim().ToUpper().Equals("D"))
                            {
                                var rp = db.tb_ProductionReceives.Where(p => p.OrderNo == rd.OrderNo).OrderBy(o => o.CreateDate).ToList();
                                foreach (var rs in rp)
                                {
                                    if(Convert.ToDateTime(rs.CreateDate)>DateCheck2)
                                    {
                                        Qty10 += Convert.ToInt32(rs.Qty);
                                    }
                                }
                            }
                            else if (rd.DayNight.Trim().ToUpper().Equals("N"))
                            {
                                var rp = db.tb_ProductionReceives.Where(p => p.OrderNo == rd.OrderNo).OrderBy(o => o.CreateDate).ToList();
                                foreach (var rs in rp)
                                {
                                    if (Convert.ToDateTime(rs.CreateDate) > DateCheck1)
                                    {
                                        Qty10 += Convert.ToInt32(rs.Qty);
                                    }
                                }
                            }
                            ////////////UPDATE/////////////////
                            if(Qty10>0)
                            {
                                tb_ProductionHD rh = db.tb_ProductionHDs.Where(r => r.id == rd.id).FirstOrDefault();
                                if(rh!=null)
                                {
                                    rh.Delays = Qty10;
                                    db.SubmitChanges();
                                }
                                Qty10 = 0;
                            }else
                            {
                                tb_ProductionHD rh = db.tb_ProductionHDs.Where(r => r.id == rd.id).FirstOrDefault();
                                if (rh != null)
                                {
                                    rh.Delays = 0;
                                    db.SubmitChanges();
                                }
                            }


                            ///////////////////////////
                        }
                    }
                }
            }
            catch { }
            this.Cursor = Cursors.Default;
            progressBar1.Visible = false;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            LoadData2();
            dbClss.ExportGridXlSX(radGridView2);
        }
        private void LoadData2()
        {
            try
            {
                radGridView2.AutoGenerateColumns = true;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_012_PD_ReportDialyItemRM_Dynamics(radDateTimePicker1.Value, radDateTimePicker2.Value);
                    radGridView2.DataSource = db.TempCustRMs.Where(r => r.id != 0).ToList();
                }
            }
            catch { }
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            try
            {
                radGridView3.AutoGenerateColumns = true;
                radGridView3.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView3.DataSource = db.sp_037_ReportProduction(radDateTimePicker1.Value, radDateTimePicker2.Value, txtWorkNo.Text, txtLineNo.Text).ToList();
                    dbClss.ExportGridXlSX(radGridView3);
                }

            }
            catch { }
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            try
            {
                radGridView3.AutoGenerateColumns = true;
                radGridView3.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView3.DataSource = db.RP_Production03_Dynamics(txtWorkNo.Text, txtLineNo.Text,radDateTimePicker1.Value, radDateTimePicker2.Value).ToList();
                    dbClss.ExportGridXlSX(radGridView3);
                }

            }
            catch { }
        }
    }
}
