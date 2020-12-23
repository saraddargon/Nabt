using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
namespace Report
{
    public static class CRRReport
    {
        public static string ServerName="";
        public static string DbName = "";
        public static string dbUser = "";
        public static string dbPass = "";
        public static string dbPartReport = "";
        public static string PrinterName = "EPSON L360 Series_too";


        public static CrystalDecisions.CrystalReports.Engine.ReportDocument rptSourceX = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    }
}
