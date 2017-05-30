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
        static string SERVERName = ConnectDB.ConnectDB.server;//mainClass.mainClass.DB;
        static string DBName = ConnectDB.ConnectDB.dbname;//mainClass.mainClass.database;
        static string DATA = "";
        
        private void Report_Load(object sender, EventArgs e)
        {
            try
            {
                // this.Cursor = Cursors.WaitCursor;
                // dbClass.rptSourceX.Refresh();
                // crystalReportViewer1.ReportSource = null;
                DATA = "";
                DATA = AppDomain.CurrentDomain.BaseDirectory;
                DATA = DATA+@"Report\" + Rpt;
              //  MessageBox.Show(DATA);
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

                    rpt.Subreports[i].DataSourceConnections[0].SetConnection(SERVERName, DBName, ConnectDB.ConnectDB.Userdb, ConnectDB.ConnectDB.PassDb);
                    rpt.Subreports[i].DataSourceConnections[0].IntegratedSecurity = false;
                    rpt.Subreports[i].DataSourceConnections[SERVERName, DBName].SetLogon(ConnectDB.ConnectDB.Userdb, ConnectDB.ConnectDB.PassDb);
                }
                rpt.DataSourceConnections[0].SetConnection(SERVERName, DBName, ConnectDB.ConnectDB.Userdb, ConnectDB.ConnectDB.PassDb);
                rpt.DataSourceConnections[0].IntegratedSecurity = false;
                rpt.DataSourceConnections[SERVERName, DBName].SetLogon(ConnectDB.ConnectDB.Userdb, ConnectDB.ConnectDB.PassDb);

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
                case "FromDL":
                    {

                        rptDc.SetParameterValue("@DocNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@DateTime", DateTime.Now);
                        
                    } break;
                case "ReportPR":
                    {

                        rptDc.SetParameterValue("@PRNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportPO":
                    {

                        rptDc.SetParameterValue("@PONo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportReceiveList":
                    {
                        rptDc.SetParameterValue("@RCNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportReceiveTAG":
                    {
                        rptDc.SetParameterValue("@RCNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "Kanban":
                    {
                        rptDc.SetParameterValue("@USERID", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;

                case "ReportBOx":
                    {
                        rptDc.SetParameterValue("@CodeNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "Shipping":
                    {
                        rptDc.SetParameterValue("@SPNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportReturn":
                    {
                        rptDc.SetParameterValue("@RTNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "Report01":
                    {
                        rptDc.SetParameterValue("@JobNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportCost":
                    {
                        rptDc.SetParameterValue("@JobNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
                case "ReportBox":
                    {
                        rptDc.SetParameterValue("@CodeNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;

                case "ReportCalJob":
                    {
                        rptDc.SetParameterValue("@JobNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@User", Convert.ToString(Value[1].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                    }break;
                case "ReportCalTotal":
                    {                       
                        rptDc.SetParameterValue("@User", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                    } break;
                case "ReportToolALL":
                    {                       
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                    } break;
                case "ReportJobs":
                    {
                        rptDc.SetParameterValue("@User", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                    } break;
                case "BoxLabel":
                    {
                        rptDc.SetParameterValue("@CodeNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                    } break;

                case "CheckStock":
                    {
                        rptDc.SetParameterValue("@ListNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Nowx", DateTime.Now);
                        rptDc.SetParameterValue("@Ac", Convert.ToInt32(Value[1].ToString()));

                    } break;
                case "ShipWork":
                    {
                        rptDc.SetParameterValue("@RNo", Convert.ToString(Value[0].ToString()));
                        rptDc.SetParameterValue("@Data", Convert.ToString(Value[1].ToString()));
                        rptDc.SetParameterValue("@TY", Convert.ToString(Value[2].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                       

                    } break;
                case "ReportReceiveTAG_INS":
                    {
                        rptDc.SetParameterValue("@id", Convert.ToInt32(Value[0].ToString()));
                        rptDc.SetParameterValue("@Datex", DateTime.Now);
                        // rptDc.SetParameterValue("@Action", Convert.ToInt32(ClassReport.Value[1]));
                    } break;
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
