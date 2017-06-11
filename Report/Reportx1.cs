using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using System.Runtime.InteropServices;

namespace Report
{
    public partial class Reportx1 : Form
    {
        public Reportx1()
        {
            InitializeComponent();
        }
        public Reportx1(string Rptx)
        {
            InitializeComponent();
            Rpt = Rptx;
        }
        public Reportx1(string Rptx, DataTable Dt, string Fromd)
        {
            InitializeComponent();
            Rpt = Rptx;
            _Dt = Dt;
            fromdt = Fromd;
        }
        private string Rpt = "";
        private DataTable _Dt = null;
        private string fromdt = "";
        public static string WReport { get; set; }
        public static ReportDocument rptNull { get; set; }
        public static string[] Value { get; set; }
        static string SERVERName = Report.CRRReport.ServerName;
        static string DBName = Report.CRRReport.DbName;
        static string Userdb = Report.CRRReport.dbUser;
        static string PassDb = Report.CRRReport.dbPass;
        static string DATA = "";
        
        private void Report_Load(object sender, EventArgs e)
        {
            try
            {
                SERVERName = Report.CRRReport.ServerName;
                DBName = Report.CRRReport.DbName;
                Userdb = Report.CRRReport.dbUser;
                PassDb = Report.CRRReport.dbPass;

               // MessageBox.Show(SERVERName + "," + DBName + "," + Userdb + "," + PassDb);
                // this.Cursor = Cursors.WaitCursor;
                // dbClass.rptSourceX.Refresh();
                // crystalReportViewer1.ReportSource = null;
                DATA = "";
                DATA = AppDomain.CurrentDomain.BaseDirectory;
                DATA = DATA+@"Report\" + Rpt;
              
                if (fromdt.Equals(""))
                {

                    CRRReport.rptSourceX.Load(DATA, OpenReportMethod.OpenReportByDefault);
                    SetDataSourceConnection(CRRReport.rptSourceX);
                    SetParameter(CRRReport.rptSourceX);
                    crystalReportViewer1.ReportSource = CRRReport.rptSourceX;
                    crystalReportViewer1.Zoom(100);
                }
                else
                {
                    //FromData Table
                    CRRReport.rptSourceX.Load(DATA, OpenReportMethod.OpenReportByDefault);
                    SetDataSourceConnection(CRRReport.rptSourceX);
                    //SetDataSourceConnection(JcControl.rptSourceX);
                    //JcControl.SetParameter(JcControl.rptSourceX);          
                    crystalReportViewer1.ReportSource = CryStal_pdf(_Dt, DATA);
                    crystalReportViewer1.Zoom(100);
                }
                //this.Cursor = Cursors.Default;
            }
            catch { }
            finally { }
           // this.reportViewer1.RefreshReport();
            
        }
        public static void SetDataSourceConnection(CrystalDecisions.CrystalReports.Engine.ReportDocument rpt)
        {
            try
            {

                for (int i = 0; i < rpt.Subreports.Count; i++)
                {

                    rpt.Subreports[i].DataSourceConnections[0].SetConnection(SERVERName, DBName, Userdb, PassDb);
                    rpt.Subreports[i].DataSourceConnections[0].IntegratedSecurity = false;
                    rpt.Subreports[i].DataSourceConnections[SERVERName, DBName].SetLogon(Userdb, PassDb);
                }
                rpt.DataSourceConnections[0].SetConnection(SERVERName, DBName, Userdb, PassDb);
                rpt.DataSourceConnections[0].IntegratedSecurity = false;
                rpt.DataSourceConnections[SERVERName, DBName].SetLogon(Userdb, PassDb);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public static CrystalDecisions.CrystalReports.Engine.ReportDocument CryStal_pdf(DataTable dt, string Rpt)
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("Search data not found");
                }
                else
                {
                    rpt.Load(Rpt);
                    //SetDataSourceConnection(rpt);
                    rpt.SetDataSource(dt);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return rpt;
        }
        public static CrystalDecisions.CrystalReports.Engine.ReportDocument CryStal_pdf2(DataTable dt, string Rpt)
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("Search data not found");
                }
                else
                {

                    //  rpt.Load(Path.Combine(@"C:\Program Files\GBarcode", Rpt));
                    // SetDataSourceConnection(rpt);
                    rpt.SetDataSource(dt);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Report;01" + ex.Message); }
            return rpt;
        }
        public static void SetParameter(ReportDocument rptDc)
        {

            switch (WReport)
            {
                case "001_Kanban_Part":
                    {

                        rptDc.SetParameterValue("@User", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);

                    }
                    break;
                case "002_BoxShelf_Part":
                    {

                        rptDc.SetParameterValue("@User", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        
                    } break;
                case "ReportPR":
                    {

                        rptDc.SetParameterValue("@PRNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportPR2":
                    {

                        rptDc.SetParameterValue("@PRNoFrom", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@PRNoTo", Convert.ToString(Value[1].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    }
                    break;
                case "ReportPO":
                    {

                        rptDc.SetParameterValue("@PONo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportReceive":
                    {

                        rptDc.SetParameterValue("@RCNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    }
                    break;
                case "ReportReceive2":
                    {
                        rptDc.SetParameterValue("@RCNo1", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@RCNo2", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    }
                    break;
                case "ReportShipping":
                    {

                        rptDc.SetParameterValue("@ShippingNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    }
                    break;
                case "ReportShipping2":
                    {

                        rptDc.SetParameterValue("@ShippingNo1", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@ShippingNo2", Convert.ToString(Value[1].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    }
                    break;



            }
        }

        private void printForXPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.UseEXDialog = false;
            DialogResult dr = printDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //CRRReport.rptSourceX.VerifyDatabase();
                //CRRReport.rptSourceX.Refresh();
                if (fromdt.Equals(""))
                {
                    //CRRReport.rptSourceX = CryStal_pdf(_Dt, DATA);
                }
                else
                {
                    CRRReport.rptSourceX = CryStal_pdf(_Dt, DATA);
                }
                CRRReport.rptSourceX.PrintToPrinter(1, false, 0, 0);
                //System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
                ////Get the Copy times
                //int nCopy = printDocument1.PrinterSettings.Copies;
                ////Get the number of Start Page
                //int sPage = printDocument1.PrinterSettings.FromPage;
                ////Get the number of End Page
                //int ePage = printDocument1.PrinterSettings.ToPage;
                //rptSourceX.PrintOptions.PrinterName = printDocument1.PrinterSettings.PrinterName;
                ////Start the printing process.  Provide details of the print job
                //rptSourceX.PrintToPrinter(nCopy, false, sPage, ePage);
 
            }
        }
    }
}
