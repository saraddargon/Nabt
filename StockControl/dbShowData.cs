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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace StockControl
{
    public static class dbShowData
    {
        public static void Setcolor(TextBox tx,int AC)
        {
            if (AC.Equals(1))
            {
                tx.BackColor = Color.Yellow;
            }
            else
            {
                tx.BackColor = Color.White;
            }

        }
        public static void SetColor2(RadTextBox tx,int AC)
        {
            if (AC.Equals(1))
            {
                tx.BackColor = Color.Yellow;
            }
            else
            {
                tx.BackColor = Color.White;
            }
        }
        public static void CreateListQC(string WO,string ISO)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string QCNo = "";
                    tb_QCHD qch = db.tb_QCHDs.Where(q => q.WONo.Equals(WO) && q.FormISO.Equals(ISO)).FirstOrDefault();
                    if (qch == null)
                    {
                        var lWo = db.sp_46_QCSelectWO_01(WO).FirstOrDefault();
                        if (lWo != null)
                        {
                            QCNo = dbClss.GetSeriesNo(6, 2);
                            tb_QCHD qcN = new tb_QCHD();
                            qcN.CheckBy1 = "";
                            qcN.CheckBy2 = "";
                            qcN.CheckBy3 = "";
                            qcN.IssueBy = "";
                            qcN.IssueBy2 = "";
                            qcN.ApproveBy = "";
                            qcN.ApproveBy2 = "";                           
                            qcN.QCNo = QCNo;
                            qcN.WONo = WO;
                            qcN.PartNo = lWo.CODE;
                            qcN.OrderQty = lWo.OrderQty;
                            qcN.OKQty = 0;
                            qcN.NGQty = 0;
                            qcN.LotNo = lWo.LotNo;
                            qcN.LineName = lWo.BUMO;
                            qcN.CreateBy = dbClss.UserID;
                            qcN.CreateDate = DateTime.Now;
                            qcN.SS = 1;
                            qcN.Status = "Checking";
                            qcN.SendApprove = false;
                            qcN.FormISO = ISO;
                            qcN.DocRef1 = "";
                            qcN.DocRef2 = "";
                            qcN.ApproveBy = "";
                            qcN.ApproveBy2 = "";
                            qcN.CheckBy1 = "";
                            qcN.CheckBy2 = "";
                            qcN.CheckBy3 = "";
                            
                            qcN.IssueBy = "";
                            qcN.IssueBy2 = "";
                            qcN.ChangeModel = lWo.ChangeModel;
                            qcN.DayNight = lWo.DayNight;
                            qcN.QCPoint = "";
                            qcN.RefValue1 = "";
                            qcN.RefValue2 = "";
                            qcN.RefValue3 = "";
                            db.tb_QCHDs.InsertOnSubmit(qcN);
                            db.SubmitChanges();
                        }
                    }
                }
            }
            catch { }
        }
        public static void InsertTAG(string PTAG,string WO,string QCNo,decimal Qty,string ofTAG,string Type,string LineNo,string GType,string TAG)
        {
            try
            {
                int OK = 0;
                int NG = 0;               
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    //SumQty Inspection / NG / OK 
                    decimal SumALL = Convert.ToDecimal(db.get_QCSumQty(QCNo, PTAG, 5));
                    decimal SumOK = 0;// Convert.ToDecimal(db.get_QCSumQty(Upd.QCNo, 1));
                    decimal SumNG = Convert.ToDecimal(db.get_QCSumQty(QCNo, PTAG, 4));
                    SumOK = SumALL - SumNG;


                    tb_QCTAG qctag = db.tb_QCTAGs.Where(t => t.BarcodeTag.Equals(PTAG) && t.QCNo.Equals(QCNo)).FirstOrDefault();
                    if (qctag == null)
                    {
                        tb_QCTAG qct = new tb_QCTAG();
                        qct.QCNo = QCNo;
                        qct.BarcodeTag = PTAG;
                        qct.SS = 1;
                        qct.QtyofTag = Qty;
                        qct.OKQty = (Qty - NG);
                        qct.NGQty = NG;
                        qct.ofTAG = ofTAG;
                        qct.Dept = Type;
                        qct.CheckDate = DateTime.Now;
                        qct.CheckBy = dbClss.UserID;
                        qct.DType = LineNo;
                        qct.NGofTAG = 0;
                        qct.Seq = 1;
                        qct.CheckTAG = false;
                        qct.GTAG = TAG;
                        qct.GType = GType;
                        db.tb_QCTAGs.InsertOnSubmit(qct);
                        db.SubmitChanges();
                    }
                    //else
                    //{

                    //    qctag.QtyofTag = Qty;
                    //    qctag.OKQty = (OK - NG);
                    //    qctag.NGQty = NG;
                    //    qctag.ofTAG = ofTAG;
                    //    db.SubmitChanges();
                    //}
                }
            }
            catch { }
        }
        public static void InsertScanMachine(string WO,string TAG,string FormISO,string PartNo)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string[] DATA = TAG.Split(',');
                    if(DATA.Length==1)
                    {
                        int Seqa = 0;
                        string DN = dbShowData.CheckDayN(DateTime.Now);
                        int.TryParse(DATA[0], out Seqa);
                        if (Seqa > 0)
                        {
                            db.sp_46_QCMachine_Copy(FormISO, PartNo, WO);
                            tb_QCCheckMachine qc = db.tb_QCCheckMachines.Where(q => q.WONo.Equals(WO) && q.FormISO.Equals(FormISO) && q.Seq.Equals(Seqa)).FirstOrDefault();
                            if (qc != null)
                            {
                                qc.SC = "OK";
                                qc.CreateBy = dbClss.UserID;
                                qc.CreateDate = DateTime.Now;
                                qc.TAGScan = TAG;
                                qc.DayN = DN;
                                db.SubmitChanges();
                            }
                            else
                            {
                                //tb_QCCheckMachine qn = new tb_QCCheckMachine();
                                //qn.WONo = WO;
                                //qn.FormISO = FormISO;
                                //qn.Seq = Seq;
                                //qn.TAGScan = TAG;
                                //qn.PartNo = PartNo;
                                //qn.CreateBy = dbClss.UserID;
                                //qn.CreateDate = DateTime.Now;
                                //qn.DayN = DN;
                                //qn.SC = "OK";
                                //db.tb_QCCheckMachines.InsertOnSubmit(qn);
                                //db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            catch { }
        }
        public static string GETQCNo(string WO,string ISO)
        {
            string QCNo = "";
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                tb_QCHD qh = db.tb_QCHDs.Where(q => q.FormISO.Equals(ISO) && q.WONo.Equals(WO)).FirstOrDefault();
                if (qh != null)
                {
                    QCNo = qh.QCNo;
                }
            }

                return QCNo;
        }
        public static string CheckDayN(DateTime date1)
        {
            string DayN = "D";
            
            try
            {
               // date1 = Convert.ToDateTime("08:30:00");
                TimeSpan ts = new TimeSpan(date1.Hour, date1.Minute, date1.Second);
                if (ts.TotalMinutes >= 510 && ts.TotalMinutes < 1200)
                {
                    DayN = "D";
                }
                else
                {
                    DayN = "N";
                }              
            }
            catch { }
            return DayN;
        }
        public static int CheckColorDayN(string PartNo,string WONo)
        {
            int AC = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int A1 = 0;
                    int A2 = 0;
                    var cklist = db.tb_QCCheckParts.Where(c => c.PartNo.Equals(PartNo) && c.OrderNo.Equals(WONo)).ToList();
                    foreach(var rd in cklist)
                    {
                        if(rd.DayN.Equals("D"))
                        {
                            A1 += 1;
                        }
                        if(rd.DayN.Equals("N"))
                        {
                            A2 += 1;
                        }
                    }

                    if (A1 > 0 && A2 > 0)
                    {
                        AC = 3;
                    }
                    else if (A1 > 0 && A2 == 0)
                    {
                        AC = 1;
                    }
                    else if (A1 == 0 && A2 > 0)
                    {
                        AC = 2;
                    }


                }
            }
            catch { }
            return AC;
        }
        public static void Print026()
        {

        }
        public static void PrintData(string WO, string PartNo,string QCNo1)
        {
            try
            {
                // MessageBox.Show(QCNo1+","+PartNo+","+WO);
                /*
                string FormISOx = "FM-PD-026_00_1.rpt";
                Report.Reportx1.WReport = "QCReport01";
                Report.Reportx1.Value = new string[2];
                Report.Reportx1.Value[0] = FormISOx;
                Report.Reportx1.Value[1] = txtProdNo.Text;
                Report.Reportx1 op = new Report.Reportx1(FormISOx);
                op.Show();
                */
                //      ：　  ～　　：
                string DATA = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = System.IO.Path.GetTempPath();
                string FileName = "FM-PD-026.xlsx";
                string tempfile = tempPath + FileName;
                DATA = DATA + @"QC\" + FileName;

                if (File.Exists(tempfile))
                {
                    try
                    {
                        File.Delete(tempfile);
                    }
                    catch { }
                }

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  DATA, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                // progressBar1.Maximum = 51;
                // progressBar1.Minimum = 1;
                //int row1 = 22;
                //int row2 = 22;
                //int Seq = 0;
                //int seq2 = 22;
                //int CountRow = 0;
                string cIssueBy1 = "";
                string cIssueBy2 = "";

                string cCheckBy1 = "";
                string cCheckBy2 = "";
                string cCheckBy3 = "";

                string cCheckByF1 = "";
                string cCheckByF2 = "";
                string cCheckByF3 = "";

                string PV = "P";
                string QHNo = QCNo1;
                string FormISO = "";
                string DN = "";
                string SymBo = "～";
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //string Value1 = "";
                    //string Value2 = "";
                    //string LotNo = "";

                   
                   
                    ///////////////SETValue/////////////////
                    var DValue = db.sp_46_QCSelectWO_01(WO).FirstOrDefault();
                    if (DValue != null)
                    {
                        DN = DValue.DayNight;
                        Excel.Range CPart = worksheet.get_Range("P3");
                        CPart.Value2 = DValue.CODE;
                        
                        Excel.Range CStamp = worksheet.get_Range("X5");
                        if (PartNo.Length > 0)
                        {
                            if (dbClss.Right(PartNo, 1).Equals("W"))
                            {
                                CStamp.Value2 = "'" + dbClss.Right(PartNo, 8).Substring(0, 2) + "  " + dbClss.Right(PartNo, 6).Substring(0, 5);
                            }
                            else
                            {
                                CStamp.Value2 = "'" + dbClss.Right(PartNo, 7).Substring(0, 2) + "  " + dbClss.Right(PartNo, 5);
                            }
                        }

                       
                        Excel.Range CName = worksheet.get_Range("I5");
                        CName.Value2 = DValue.NAME;

                        Excel.Range WOs = worksheet.get_Range("D5");
                        WOs.Value2 = DValue.PORDER;

                        Excel.Range CDate = worksheet.get_Range("D7");
                        CDate.Value2 = DValue.DeliveryDate;

                        Excel.Range CLot = worksheet.get_Range("D9");
                        CLot.Value2 = DValue.LotNo;

                        Excel.Range CQty = worksheet.get_Range("D11");
                        CQty.Value2 = DValue.OrderQty.ToString();


                        try
                        {
                            tb_QCHD qh = db.tb_QCHDs.Where(w => w.QCNo.Equals(QCNo1)).FirstOrDefault();
                            if (qh != null)
                            {


                                //////////Find UserName////////////
                                var uc = db.tb_QCCheckUsers.Where(u => u.QCNo.Equals(QCNo1)).ToList();
                                int r1 = 0;
                                int r2 = 0;
                                int r3 = 0;
                                int rr1 = 0;
                                int rr2 = 0;
                                int rr3 = 0;

                                foreach (var rd in uc)
                                {
                                    DN = rd.DayN;// dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));
                                    
                                    if (rd.UDesc.Equals("ผู้จัดทำเอกสาร"))
                                        cIssueBy1 = rd.UserName;
                                    if (rd.UDesc.Equals("ผู้ตรวจสอบก่อนผลิต"))
                                        cIssueBy2 = rd.UserName;

                                    if (DN.Equals("D"))
                                    {
                                        if (rd.UDesc.Equals("พนักงานประกอบ SUB LINE"))
                                        {
                                            r1 += 1;
                                            if (r1 > 1)
                                                cCheckBy1 += ",";
                                            cCheckBy1 += rd.UserName;
                                            
                                            
                                        }
                                        else if (rd.UDesc.Equals("พนักงานประกอบ MAIN LINE"))
                                        {
                                            r2 += 1;
                                            if (r2 > 1)
                                                cCheckBy2 += ",";
                                            cCheckBy2 += rd.UserName;
                                            
                                        }
                                        else if (rd.UDesc.Equals("พนักงานประกอบ FINAL LINE"))
                                        {
                                            r3 += 1;
                                            if (r3 > 1)
                                                cCheckBy3 += ",";
                                            cCheckBy3 += rd.UserName;
                                           
                                        }
                                    }
                                    else //N
                                    {
                                        if (rd.UDesc.Equals("พนักงานประกอบ SUB LINE"))
                                        {
                                            rr1 += 1;
                                            if (rr1 > 1)
                                                cCheckByF1 += ",";
                                            cCheckByF1 += rd.UserName;


                                        }
                                        else if (rd.UDesc.Equals("พนักงานประกอบ MAIN LINE"))
                                        {
                                            rr2 += 1;
                                            if (rr2 > 1)
                                                cCheckByF2 += ",";
                                            cCheckByF2 += rd.UserName;

                                        }
                                        else if (rd.UDesc.Equals("พนักงานประกอบ FINAL LINE"))
                                        {
                                            rr3 += 1;
                                            if (rr3 > 1)
                                                cCheckByF3 += ",";
                                            cCheckByF3 += rd.UserName;

                                        }
                                    }
                                }


                                FormISO = qh.FormISO;
                                QHNo = qh.QCNo;
                                Excel.Range Ap = worksheet.get_Range("AE10");
                                Ap.Value2 = Convert.ToString(qh.ApproveBy);


                                Excel.Range CheckBy1 = worksheet.get_Range("E23");
                                CheckBy1.Value2 = cCheckBy1;
                                Excel.Range CheckBy2 = worksheet.get_Range("E32");
                                CheckBy2.Value2 = cCheckBy2;
                                Excel.Range CheckBy3 = worksheet.get_Range("E40");
                                CheckBy3.Value2 = cCheckBy3;

                                Excel.Range CheckByF1 = worksheet.get_Range("F23");
                                CheckByF1.Value2 = cCheckByF1;
                                Excel.Range CheckByF2 = worksheet.get_Range("F32");
                                CheckByF2.Value2 = cCheckByF2;
                                Excel.Range CheckByF3 = worksheet.get_Range("F40");
                                CheckByF3.Value2 = cCheckByF3;


                                if (DN.Equals("D"))
                                {
                                    Excel.Range IssueBy = worksheet.get_Range("AE5");
                                    IssueBy.Value2 = "1. " + cIssueBy1;
                                    Excel.Range IssueBy2 = worksheet.get_Range("AE7");
                                    IssueBy2.Value2 = "2. " + cIssueBy2;
                                }
                                else
                                {
                                    Excel.Range IssueBy = worksheet.get_Range("AF5");
                                    IssueBy.Value2 = "1. " + cIssueBy1;
                                    Excel.Range IssueBy2 = worksheet.get_Range("AF7");
                                    IssueBy2.Value2 = "2. " + cIssueBy2;
                                }

                                QHNo = qh.QCNo;

                               ////Set Topic//

                                Excel.Range AF1 = worksheet.get_Range("AF1");
                                AF1.Value2 = "'"+db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 51);

                                //Excel.Range AA40 = worksheet.get_Range("AA40");
                                //AA40.Value2 = db.get_QC_SetDataMasterTpic(qh.FormISO, qh.PartNo, 38);

                                //Excel.Range AA32 = worksheet.get_Range("AA32");
                                //AA32.Value2 = db.get_QC_SetDataMasterTpic(qh.FormISO, qh.PartNo, 30);

                                //Step 1
                                int cRow = 22;
                                string Ppart = "";    
                                for (int II = 1; II <= 22; II++)
                                {
                                    cRow += 1;
                                    
                                    ////Line 1 //
                                    Ppart= db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, II);

                                    Excel.Range Line1 = worksheet.get_Range("L"+cRow.ToString());
                                    Line1.Value2 = Ppart;

                                    var rds = db.sp_46_QCGetValue2601(qh.WONo, Ppart).FirstOrDefault();
                                    if (rds != null)
                                    {

                                        Excel.Range Line2 = worksheet.get_Range("Q" + cRow.ToString());
                                        Line2.Value2 = rds.DayN;

                                        Excel.Range Line3 = worksheet.get_Range("R" + cRow.ToString());
                                        Line3.Value2 = rds.NightN;

                                        Excel.Range Line4 = worksheet.get_Range("S" + cRow.ToString());
                                        Line4.Value2 ="'"+ rds.Lot;
                                    }


                                    //if (cRow == 23)
                                   //     cRow += 1;
                                }

                                //Step 2
                                int crow2 = 22;
                                cRow = 22;
                                int NewR = 0;
                                int NewR2 = 0;
                                for (int II = 23; II <= 55; II++)
                                {
                                    cRow += 1;
                                    crow2 += 1;
                                    ////Line 1 //
                                    if (II != 29 && II != 51)
                                    {
                                        //การเซ็ต
                                        NewR2 = II;
                                        NewR = cRow;
                                        if (II >= 47)
                                        {
                                            NewR2 = II + 5;
                                            if (II >= 52)
                                            {
                                                NewR2 = II - 5;
                                                NewR = NewR - 1;
                                            }

                                        }

                                        Excel.Range Line1 = worksheet.get_Range("AE" + NewR.ToString());
                                        Line1.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, NewR2);

                                    }

                                   

                                    if (II != 42)
                                    {

                                        NewR2 = II;
                                        NewR = crow2;
                                        if (II >= 47)
                                        {
                                            NewR2 = II + 5;
                                            if (II >= 52)
                                            {
                                                NewR2 = II - 5;
                                                NewR = NewR - 1;
                                            }

                                        }

                                        var rss = db.sp_46_QCGetValue2601_20(qh.WONo, NewR2).FirstOrDefault();
                                        if (rss != null)
                                        {
                                            Excel.Range Line2 = worksheet.get_Range("AF" + NewR.ToString());
                                            Line2.Value2 = rss.DayN;
                                            Excel.Range Line3 = worksheet.get_Range("AG" + NewR.ToString());
                                            Line3.Value2 = rss.NightN;
                                        }
                                    }
                                    else
                                    {
                                        Excel.Range Line2 = worksheet.get_Range("AF42");
                                        Line2.Value2 = db.get_QC_DATAPoint(qh.QCNo, "", 42);
                                    }

                                 
                                    
                                }

                                Excel.Range Loctite1 = worksheet.get_Range("S42");
                                Loctite1.Value2 = "'" + db.get_QC_ValueRM(WO, "GREASE G-30M", 20);
                                Excel.Range Loctite2 = worksheet.get_Range("S43");
                                Loctite2.Value2 = "'" + db.get_QC_ValueRM(WO, "LOCTITE 277", 21);
                                Excel.Range Loctite3 = worksheet.get_Range("S44");
                                Loctite3.Value2 = "'" + db.get_QC_ValueRM(WO, "LOCTITE 414", 22);

                                string DDN1 = db.get_QC_ValueRM22(WO, "GREASE G-30M", 20);
                                string DDN2 = db.get_QC_ValueRM22(WO, "LOCTITE 277", 21);
                                string DDN3 = db.get_QC_ValueRM22(WO, "LOCTITE 414", 22);

                                //Step 1
                                if (DDN1.Equals("D"))
                                {
                                    Excel.Range LoctiteQ1 = worksheet.get_Range("Q42");
                                    LoctiteQ1.Value2 = "P";
                                }
                                else if (DDN1.Equals("N"))
                                {
                                    Excel.Range LoctiteR1 = worksheet.get_Range("R42");
                                    LoctiteR1.Value2 = "P";
                                }

                                //Step 2
                                if (DDN2.Equals("D"))
                                {
                                    Excel.Range LoctiteQ2 = worksheet.get_Range("Q43");
                                    LoctiteQ2.Value2 = "P";
                                }
                                else if (DDN2.Equals("N"))
                                {
                                    Excel.Range LoctiteR2 = worksheet.get_Range("R43");
                                    LoctiteR2.Value2 = "P";
                                }

                                //Step 3
                                if (DDN3.Equals("D"))
                                {
                                    Excel.Range LoctiteQ3 = worksheet.get_Range("Q44");
                                    LoctiteQ3.Value2 = "P";
                                }
                                else if (DDN3.Equals("N"))
                                {
                                    Excel.Range LoctiteR3 = worksheet.get_Range("R44");
                                    LoctiteR3.Value2 = "P";
                                }







                            }
                            var gTime = db.sp_46_QCGetValue2601_Time(WO).ToList();
                            if (gTime.Count > 0)
                            {
                                var g = gTime.FirstOrDefault();
                                Excel.Range AB = worksheet.get_Range("AB9");
                                AB.Value2 = Convert.ToDecimal(DValue.ChangeModel).ToString("####") + " นาที";

                                if (!g.StartTime.Equals(""))
                                {
                                    Excel.Range StartT = worksheet.get_Range("N7");
                                    StartT.Value2 = Convert.ToDateTime(g.StartTime).ToString("HH:mm");

                                    Excel.Range EndT = worksheet.get_Range("AA7");
                                    EndT.Value2 = Convert.ToDateTime(g.EndTime).ToString("HH:mm");

                                    int ChanP = 0;
                                    int.TryParse(Convert.ToInt32(DValue.ChangeModel).ToString(), out ChanP);
                                    if (ChanP > 0)
                                    {
                                        DateTime Chtime = Convert.ToDateTime(g.StartTime).AddMinutes(ChanP * -1);
                                        Excel.Range O9 = worksheet.get_Range("O9");
                                        O9.Value2 = "'" + Convert.ToDateTime(Chtime).ToString("HH:mm") + "-" + Convert.ToDateTime(g.StartTime).ToString("HH:mm");

                                    }

                                }
                            }

                            //Find Problem//

                            tb_QCProblem pb = db.tb_QCProblems.Where(p => p.QCNo.Equals(QHNo)).FirstOrDefault();
                            if(pb!=null)
                            {
                                if (pb.TypeProblem.Equals("Man"))
                                {
                                    Excel.Range PBA = worksheet.get_Range("F13");
                                    PBA.Value2 = "P";

                                }
                                else if (pb.TypeProblem.Equals("Machine"))
                                {
                                    Excel.Range PBA = worksheet.get_Range("I13");
                                    PBA.Value2 = "P";
                                }else if(pb.TypeProblem.Equals("Method"))
                                {
                                    Excel.Range PBA = worksheet.get_Range("M13");
                                    PBA.Value2 = "P";
                                }
                                else if (pb.TypeProblem.Equals("Material"))
                                {
                                    Excel.Range PBA = worksheet.get_Range("P13");
                                    PBA.Value2 = "P";
                                }
                                else if (pb.TypeProblem.Equals("Other"))
                                {
                                    Excel.Range PBA = worksheet.get_Range("S13");
                                    PBA.Value2 = "P";
                                    Excel.Range PBA2 = worksheet.get_Range("X13");
                                    PBA2.Value2 = pb.TypeRemark;
                                }

                                Excel.Range PC1 = worksheet.get_Range("F14");
                                PC1.Value2 = pb.ProblemSeeBy;
                                Excel.Range PC2 = worksheet.get_Range("N14");
                                PC2.Value2 = pb.ProblemName;

                                Excel.Range PC3 = worksheet.get_Range("AC14");
                                PC3.Value2 = pb.ProblemWare;
                                Excel.Range PC4 = worksheet.get_Range("E15");
                                PC4.Value2 = pb.ProblemTime;

                                Excel.Range PC5 = worksheet.get_Range("N15");
                                PC5.Value2 = pb.ProblemWhy;

                                Excel.Range PC6 = worksheet.get_Range("G17");
                                PC6.Value2 = pb.ProblemFix;
                                Excel.Range PC7 = worksheet.get_Range("S18");
                                PC7.Value2 = pb.FixBy;
                                Excel.Range PC8 = worksheet.get_Range("AE18");
                                PC8.Value2 = pb.CheckBy;



                            }
                        }
                        catch { }




                    }

                    ////////////////////////////////////////


                    //var listPart = db.tb_QCGroupParts.Where(q => q.FormISO.Equals(FormISO) && q.PartNo.Equals(PartNo)).OrderBy(o => o.Seq).ToList();
                    //foreach (var rd in listPart)
                    //{

                    //    if (CountRow == 0)
                    //    {
                    //        //if (rd.Seq.Equals(48))
                    //        //{
                    //        //    Excel.Range CRemark = worksheet.get_Range("A13");
                    //        //    CRemark.Value2 = "Remark  " + rd.SetData;
                    //        //    CountRow += 1;
                    //        //}
                    //    }

                    //    if (rd.Seq < 22)
                    //    {
                    //        row1 += 1;
                    //        Seq += 1;
                    //        if (row1 <= 38)
                    //        {

                    //            Excel.Range Col0 = worksheet.get_Range("G" + row1.ToString(), "G" + row1.ToString());
                    //            Excel.Range Col1 = worksheet.get_Range("L" + row1.ToString(), "L" + row1.ToString());
                    //            if (Seq.Equals(rd.Seq))
                    //            {
                    //                Col0.Value2 = rd.TopPic;
                    //                Col1.Value2 = rd.SetData;
                    //                if (!rd.SetData.Equals(""))
                    //                {
                    //                    try
                    //                    {
                    //                        var gValue = db.sp_46_QCGetValue2601(WO, rd.SetData).FirstOrDefault();

                    //                        LotNo = "";
                    //                        LotNo = Convert.ToString(gValue.Lot);
                    //                        if (gValue.CountA > 0)
                    //                        {
                    //                            if (DN.Equals("D"))
                    //                            {
                    //                                Excel.Range Check1 = worksheet.get_Range("Q" + row1.ToString(), "Q" + row1.ToString());
                    //                                Check1.Value2 = "P";
                    //                            }
                    //                            else
                    //                            {
                    //                                Excel.Range Check2 = worksheet.get_Range("R" + row1.ToString(), "R" + row1.ToString());
                    //                                Check2.Value2 = "P";
                    //                            }

                    //                            if (!LotNo.Equals(""))
                    //                            {
                    //                                Excel.Range Check3 = worksheet.get_Range("S" + row1.ToString(), "S" + row1.ToString());
                    //                                Check3.Value2 = LotNo;
                    //                            }
                    //                        }
                    //                    }
                    //                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    //                }

                    //            }
                    //            if (row1 == 18)
                    //                row1 += 1;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        row2 += 1;
                    //        seq2 += 1;
                    //        PV = "P";
                    //        if (row2 == 25 || row2 == 43)
                    //            row2 += 1;
                    //        if (seq2.Equals(rd.Seq) && rd.Seq != 48)
                    //        {
                    //            if (row2 != 31 || row2 != 42)
                    //            {
                    //                Excel.Range Col2 = worksheet.get_Range("AA" + row2.ToString(), "AA" + row2.ToString());
                    //                Col2.Value2 = rd.TopPic;
                    //            }
                    //            if (row2 != 24 || row2 != 42)
                    //            {
                    //                Excel.Range Col3 = worksheet.get_Range("AE" + row2.ToString(), "AE" + row2.ToString());
                    //                Col3.Value2 = rd.SetData;

                    //            }

                    //            if (row2 != 42 && row2 != 43)
                    //            {
                    //                tb_QCNGPoint ngq = db.tb_QCNGPoints.Where(w => w.QCNo.Equals(QHNo) && w.SeqNo.Equals(rd.Seq)).FirstOrDefault();
                    //                if (ngq != null)
                    //                {
                    //                    PV = "O";
                    //                }

                    //                if (DN.Equals("D"))
                    //                {
                    //                    Excel.Range Check2 = worksheet.get_Range("AF" + row2.ToString(), "AF" + row2.ToString());
                    //                    Check2.Value2 = PV;
                    //                }
                    //                else
                    //                {
                    //                    Excel.Range Check2 = worksheet.get_Range("AG" + row2.ToString(), "AG" + row2.ToString());
                    //                    Check2.Value2 = PV;
                    //                }

                    //                if (row2 == 35)
                    //                {
                    //                    Excel.Range Check2 = worksheet.get_Range("AG" + row2.ToString(), "AG" + row2.ToString());
                    //                    Check2.Value2 = rd.SetData;
                    //                }
                    //            }



                    //        }
                    //    }



                    //}

                    /*
                    for (int j = 0; j <= 50; j++)
                    {
                        row1 += 1;
                        Excel.Range Col0 = worksheet.get_Range("B" + row1.ToString(), "B" + row1.ToString());
                        // Excel.Range Col1 = worksheet.get_Range("E" + row1.ToString(), "E" + row1.ToString());
                        Excel.Range Col2 = worksheet.get_Range("F" + row1.ToString(), "F" + row1.ToString());
                        Excel.Range Col3 = worksheet.get_Range("C" + row1.ToString(), "C" + row1.ToString());
                        string Value1 = Convert.ToString(Col0.Value2);
                        if (Value1 == null)
                        {
                            Value1 = "";
                        }
                        if (!Convert.ToString(Value1).Equals(""))
                        {
                            Seq = 0;
                            int.TryParse(Value1, out Seq);
                            Col2.Value = db.QC_GetTemplate(FormISO, txtPartNo.Text, Seq);
                            Col3.Value = txtPartNo.Text.ToUpper();

                        }

                    }
                    */
                }

                excelBook.SaveAs(tempfile);
                excelBook.Close(false);
                excelApp.Quit();

                releaseObject(worksheet);
                releaseObject(excelBook);
                releaseObject(excelApp);
                Marshal.FinalReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
                System.Diagnostics.Process.Start(tempfile);

            }
            catch { }
            
        }
        public static void PrintData5601(string WO, string PartNo, string QCNo1)
        {
            try
            {

               // MessageBox.Show(QCNo1);
                string DATA = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = System.IO.Path.GetTempPath();
                string FileName = "FM-QA-056.xlsx";
               // FileName = "FM-QA-056_02_1.xlsx";
                string tempfile = tempPath + FileName;
                DATA = DATA + @"QC\" + FileName;

                if (File.Exists(tempfile))
                {
                    try
                    {
                        File.Delete(tempfile);
                    }
                    catch { }
                }

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  DATA, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                // progressBar1.Maximum = 51;
                // progressBar1.Minimum = 1;
                int row1 = 8;
                int row2 = 9;
                int Seq = 0;
                int seq2 = 21;
                int CountRow = 0;
                string PV = "P";
                string QHNo = QCNo1;
                string FormISO = "";
                string DN = "";
                string cIssueBy1 = "";                
                string cCheckBy1 = "";

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string Value1 = "";
                    string Value2 = "";
                    string LotNo = "";
                    string RefValue1 = "";
                    ///////////////SETValue/////////////////
                    var DValue = db.sp_46_QCSelectWO_01(WO).FirstOrDefault();
                    if (DValue != null)
                    {
                        DN = DValue.DayNight;
                        Excel.Range CPart = worksheet.get_Range("C3");
                        CPart.Value2 = DValue.NAME;
                        Excel.Range CStamp = worksheet.get_Range("C2");
                        CStamp.Value2 = DValue.CODE;
                        Excel.Range CName = worksheet.get_Range("C4");
                        CName.Value2 = DValue.OrderQty;

                        Excel.Range CDate = worksheet.get_Range("C5");
                        CDate.Value2 = DValue.LotNo;

                    

                     


                        try
                        {
                            tb_QCHD qh = db.tb_QCHDs.Where(w => w.QCNo.Equals(QCNo1)).FirstOrDefault();
                            if (qh != null)
                            {
                              
                                Excel.Range App = worksheet.get_Range("I3");
                                App.Value2 = qh.ApproveBy;                               
                                if (!qh.ApproveBy.Equals(""))
                                {
                                    Excel.Range Appdate = worksheet.get_Range("I5");
                                    Appdate.Value2 = qh.ApproveDate;
                                }

                                QHNo = qh.QCNo;
                                RefValue1 = qh.RefValue1;
                                FormISO = qh.FormISO;
                                //////////Find UserName////////////
                                var uc = db.tb_QCCheckUsers.Where(u => u.QCNo.Equals(QHNo)).ToList();
                               

                                foreach (var rd in uc)
                                {
                                    DN = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));

                                    if (rd.UDesc.Equals("Inspector"))
                                    {
                                        cIssueBy1 = rd.UserName;
                                        Excel.Range K3 = worksheet.get_Range("M3");
                                        K3.Value2 = cIssueBy1;
                                        Excel.Range K5 = worksheet.get_Range("M5");
                                        K5.Value2 = rd.ScanDate;
                                    }
                                    if (rd.UDesc.Equals("Check By"))
                                    {
                                        cCheckBy1 = rd.UserName;
                                        Excel.Range I3 = worksheet.get_Range("K3");
                                        I3.Value2 = cCheckBy1;
                                        Excel.Range I5 = worksheet.get_Range("K5");
                                        I5.Value2 = rd.ScanDate;

                                    }
                                }
                                   
                            }
                            
                        }
                        catch (Exception ex) { MessageBox.Show("1." + ex.Message); }




                    }

                    ////////////////////////////////////////
                    int countA = 0;
                    string col = "";
                    string col2x = "";
                    var listPoint = db.sp_46_QCSelectWO_09_QCTAGSelect(QHNo).ToList();
                    if (listPoint.Count > 0)
                    {
                        foreach (var rs in listPoint)
                        {
                            countA += 1;
                            // MessageBox.Show(countA.ToString());
                            if (countA <= 2)
                            {
                                row1 = 9;
                                col = "I";
                                col2x = "G";
                                if (countA == 2)
                                {
                                    col = "L";
                                    col2x = "H";
                                }



                                var listPart = db.tb_QCGroupParts.Where(q => q.FormISO.Equals(FormISO) && q.PartNo.Equals(DValue.CODE)).OrderBy(o => o.Seq).ToList();
                                foreach (var rd in listPart)
                                {
                                    //Start Insert Checkmark                            
                                    
                                    //if (rd.Seq <= 14)
                                    //{
                                    row1 += 1;
                                    
                                    //Start G=7,H=
                                    if (!rd.SetData.Equals(""))
                                    {
                                        try
                                        {
                                            var gValue = db.sp_46_QCGetValue5601(rs.BarcodeTag, QHNo, rd.Seq).FirstOrDefault();

                                            PV = "P";
                                            if (gValue.CountA > 0)
                                            {
                                                PV = "O";
                                                if (gValue.CountA == 99)
                                                    PV = "";                                   
                                            }
                                            if (countA == 2 && row1==10)
                                                   PV = "";

                                            if (rd.Seq>7)
                                            {
                                                PV = "";
                                                if (rd.Seq < 16)
                                                {
                                                    string SValue = db.get_QC_DATAPoint(QHNo, rs.BarcodeTag, rd.Seq);
                                                    Excel.Range Col0 = worksheet.get_Range(col2x + row1.ToString());
                                                    Col0.Value2 = SValue;
                                                    Excel.Range Col02 = worksheet.get_Range(col + row1.ToString());
                                                    Col02.Value2 = db.get_QC_DATAPointValue4(QHNo, rs.BarcodeTag, rd.Seq);
                                                }
                                                if(rd.Seq==16)
                                                {
                                                    Excel.Range Col02 = worksheet.get_Range(col + row1.ToString());
                                                    Col02.Value2 = db.get_QC_DATAPoint(QHNo, rs.BarcodeTag, rd.Seq);
                                                }
                                                
                                            }
                                            else
                                            {
                                                Excel.Range Col0 = worksheet.get_Range(col + row1.ToString());
                                                Col0.Value2 = PV;
                                            }
                                           




                                        }
                                        catch (Exception ex) { MessageBox.Show(ex.Message); }                                
                                        

                                    }





                                }//foreach
                            }//cunt A
                        }//for
                    }

                    
               
                }

                excelBook.SaveAs(tempfile);
                excelBook.Close(false);
                excelApp.Quit();

                releaseObject(worksheet);
                releaseObject(excelBook);
                releaseObject(excelApp);
                Marshal.FinalReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
                System.Diagnostics.Process.Start(tempfile);

            }
            catch(Exception ex) { MessageBox.Show("2."+ex.Message); }

        }
        public static void PrintData5501(string WO, string PartNo, string QCNo1)
        {
            try
            {
                //Step Report 055

                string DATA = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = System.IO.Path.GetTempPath();
                string FileName = "FM-QA-055.xlsx";
                string tempfile = tempPath + FileName;
                DATA = DATA + @"QC\" + FileName;

                if (File.Exists(tempfile))
                {
                    try
                    {
                        File.Delete(tempfile);
                    }
                    catch { }
                }

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  DATA, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                // progressBar1.Maximum = 51;
                // progressBar1.Minimum = 1;
                int row1 = 6;
                int row2 = 9;
                int Seq = 0;
                int seq2 = 21;
                int CountRow = 0;
                string PV = "P";
                string QHNo = QCNo1;
                string FormISO = "";
                int NGQ = 0;
                string DN = "";
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string Value1 = "";
                    string Value2 = "";
                    string LotNo = "";
                    string RefValue1 = "";
                    string PartName = "";
                    string Remark = "";
                    bool chek24 = true;
                    ///////////////SETValue/////////////////
                    var DValue = db.sp_46_QCSelectWO_01(WO).FirstOrDefault();
                    if (DValue != null)
                    {
                        DN = DValue.DayNight;
                        PartName = DValue.NAME;
                        Excel.Range CStamp = worksheet.get_Range("A4");
                        CStamp.Value2 = DValue.CODE;
                        Excel.Range CName = worksheet.get_Range("C4");
                        CName.Value2 = DValue.NAME;
                        Excel.Range QD = worksheet.get_Range("D4");
                        QD.Value2 = DValue.OrderQty;
                        Excel.Range CDate = worksheet.get_Range("I4");
                        CDate.Value2 = DValue.LotNo;
                        chek24 = false;
                        string GP5 = "";
                        string GP6 = "";

                        //if(PartName.Contains("24"))
                        //{
                        //    chek24 = true;
                        //}
                        //if(PartName.Contains("20"))
                        //{
                        //    chek24 = true;
                        //}

                        if (PartName.Contains("30-") || PartName.Contains("-30"))
                        {
                            chek24 = false;
                            GP5 = "30-24";
                            GP6 = "D";
                            Excel.Range G19 = worksheet.get_Range("G19");
                            G19.Value2 = "P";
                        }
                        else
                        {
                            if (PartName.Contains("16-24"))
                            {
                                GP5 = "16-24";
                                GP6 = "A";
                                Excel.Range G16 = worksheet.get_Range("G16");
                                G16.Value2 = "P";
                            }
                            else if (PartName.Contains("20-24"))
                            {
                                GP5 = "20-24";
                                GP6 = "B";
                                Excel.Range G17 = worksheet.get_Range("G17");
                                G17.Value2 = "P";
                            }
                            else if (PartName.Contains("24-24"))
                            {
                                GP5 = "24-24";
                                GP6 = "C";
                                Excel.Range G18 = worksheet.get_Range("G18");
                                G18.Value2 = "P";
                            }
                        }





                        try
                        {
                            tb_QCHD qh = db.tb_QCHDs.Where(w => w.QCNo.Equals(QCNo1)).FirstOrDefault();
                            if (qh != null)
                            {
                                FormISO = qh.FormISO;
                                Excel.Range T2 = worksheet.get_Range("T2");
                                T2.Value2 = qh.ApproveBy;

                                if(!Convert.ToString(qh.ApproveBy).Equals(""))
                                {
                                    Excel.Range APD = worksheet.get_Range("T3");
                                    APD.Value2 = qh.ApproveDate;
                                }
                                var uc = db.tb_QCCheckUsers.Where(u => u.QCNo.Equals(QHNo)).ToList();
                                int CRow = 0;
                                foreach (var rd in uc)
                                {
                                    DN = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));
                                    CRow += 1;
                                    if (rd.UDesc.Equals("Inspector"))
                                    {
                                        if (CRow == 1)
                                        {
                                            Excel.Range AH2 = worksheet.get_Range("AH2");
                                            AH2.Value2 = rd.UserName;
                                            Excel.Range AH3 = worksheet.get_Range("AH3");
                                            AH3.Value2 = rd.ScanDate;
                                        }
                                        else if (CRow == 2)
                                        {
                                            Excel.Range AE2 = worksheet.get_Range("AE2");
                                            AE2.Value2 = rd.UserName;
                                            Excel.Range AE3 = worksheet.get_Range("AE3");
                                            AE3.Value2 = rd.ScanDate;
                                        }
                                        else if (CRow==3)
                                        {
                                            Excel.Range AB2 = worksheet.get_Range("AB2");
                                            AB2.Value2 = rd.UserName;
                                            Excel.Range AB3 = worksheet.get_Range("AB3");
                                            AB3.Value2 = rd.ScanDate;
                                        }
                                    }

                                    if (rd.UDesc.Equals("Check By"))
                                    {
                                        if(CRow==1)
                                        {
                                            Excel.Range X2 = worksheet.get_Range("X2");
                                            X2.Value2 = rd.UserName;
                                            Excel.Range X3 = worksheet.get_Range("X3");
                                            X3.Value2 = rd.ScanDate;
                                        }

                                    }
                                }

                                QHNo = qh.QCNo;
                                RefValue1 = qh.RefValue1;
                            }

                        }
                        catch { }

                    }

                    ////////////////////////////////////////
                    int SOK = 0;
                    int SNG = 0;
                    int countA = 0;
                    var listPoint = db.sp_46_QCSelectWO_09_QCTAGSelect(QHNo).ToList();
                    if (listPoint.Count > 0)
                    {
                        foreach (var rs in listPoint)
                        {
                            SOK = 0;
                            SNG = 0;
                            countA += 1;
                            // MessageBox.Show(countA.ToString());
                            if (countA <= 25)
                            {
                                row1 = 6;
                                var listPart = db.tb_QCGroupParts.Where(q => q.FormISO.Equals(FormISO) && q.PartNo.Equals(DValue.CODE)).OrderBy(o => o.Seq).ToList();
                                foreach (var rd in listPart)
                                {
                                    //Start Insert Checkmark  
                                    row1 += 1;
                                    //Start G=7,H=
                                    if (!rd.TopPic.Equals(""))
                                    {
                                        try
                                        {
                                            Remark = "";
                                            var gValue = db.sp_46_QCGetValue5601(rs.BarcodeTag, QHNo, rd.Seq).FirstOrDefault();
                                            PV = "P";
                                            
                                            if (gValue.CountA > 0)
                                            {
                                               
                                                PV = "O";
                                                if (gValue.CountA == 99)
                                                    PV = "";
                                            }
                                            var NValue = db.sp_46_QCGetValue55501(rs.BarcodeTag, QHNo, rd.Seq).FirstOrDefault();
                                            Remark = NValue.Remark;

                                         

                                            //if (countA >1 && row1 == 9)
                                            //{
                                            //    if (!Remark.Equals(""))
                                            //    {
                                            //        Excel.Range Col1 = worksheet.get_Range("AF" + Convert.ToString(row1));
                                            //        Col1.Value2 = Remark;
                                            //    }
                                            //}
                                            //else
                                            //{
                                                Excel.Range Col0 = worksheet.get_Range(Getcolumn(countA + 6) + row1.ToString(), Getcolumn(countA + 6) + row1.ToString());
                                                Col0.Value2 = PV;
                                                if (!Remark.Equals(""))
                                                {
                                                    Excel.Range Col1 = worksheet.get_Range("AF" + Convert.ToString(row1));
                                                    Col1.Value2 = Remark;
                                                }
                                            //}




                                        }
                                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                                        //}




                                    }
                                    //SumNG//

                                 



                                }//foreach
                            }//cunt A
                         
                             Excel.Range GNG = worksheet.get_Range(Getcolumn(countA + 6) + "21");
                            GNG.Value2 = db.get_QCSumQtyTAGNG(QHNo, rs.BarcodeTag, 3);
                            Excel.Range GOK = worksheet.get_Range(Getcolumn(countA + 6) + "20");
                            GOK.Value2 = db.get_QCSumQtyTAGNG(QHNo, rs.BarcodeTag, 2);
                        }//for
                    }



                }

                excelBook.SaveAs(tempfile);
                excelBook.Close(false);
                excelApp.Quit();

                releaseObject(worksheet);
                releaseObject(excelBook);
                releaseObject(excelApp);
                Marshal.FinalReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
                System.Diagnostics.Process.Start(tempfile);

            }
            catch { }

        }
        public static void PrintData035(string WO, string PartNo, string QCNo1)
        {
            try
            {


                string DATA = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = System.IO.Path.GetTempPath();
                string FileName = "FM-PD-035.xls";
                string tempfile = tempPath + FileName;
                DATA = DATA + @"QC\" + FileName;

                if (File.Exists(tempfile))
                {
                    try
                    {
                        File.Delete(tempfile);
                    }
                    catch { }
                }

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  DATA, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                // progressBar1.Maximum = 51;
                // progressBar1.Minimum = 1;
                int row1 = 6;               
                int Seq = 0;               
                string PV = "P";
                string QHNo = QCNo1;
                string FormISO = "";
                string cIssueBy1 = "";
                string cIssueBy2 = "";
                string cCheckBy1 = "";
                string cCheckBy2 = "";
                string cCheckBy3 = "";
                string DN1 = "";
                string DN2 = "";
                string DN3 = "";
                string P30 = "บริเวณพ่นสีต้องไม่มีคราบกาวจากการติดกาวที่ Diaphragm (No.30)";

                bool chek24 = true;
                string DN = "";
                string LotMark = "";// "Lot ที่ตอกสามารถอ่านได้อย่างชัดเจน ( " +")";
                string Line1Part = "";
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string Value1 = "";
                    string Value2 = "";
                    string LotNo = "";
                    string RefValue1 = "";
                    string RefValue2 = "";
                    string RefValue3 = "";
                    string PartName = "";
                    string Remark = "";
                    string C9 = "";
                   // string ConnerElbo = "มุมการประกอบ Elbow กับ Cace อยู่ในค่าที่กำหนด";
                   
                    string GP5 = "";
                   
                    ///////////////SETValue/////////////////
                    var DValue = db.sp_46_QCSelectWO_01(WO).FirstOrDefault();
                    if (DValue != null)
                    {
                        DN = DValue.DayNight;
                        PartName = DValue.NAME;
                        Excel.Range CStamp = worksheet.get_Range("N3");
                        CStamp.Value2 = DValue.CODE;
                        Excel.Range CName = worksheet.get_Range("N4");
                        CName.Value2 = DValue.NAME;

                        Excel.Range W5 = worksheet.get_Range("W5");
                        W5.Value2 = DValue.PORDER;

                        Excel.Range AE5 = worksheet.get_Range("AE5");
                        AE5.Value2 = DValue.LotNo;

                        LotMark = "Lot ที่ตอกสามารถอ่านได้อย่างชัดเจน (  "+ DValue.LotNo+"   )";
                        if (DValue.CODE.Length > 0)
                        {
                            if (dbClss.Right(DValue.CODE, 1).Equals("W"))
                            {
                                Line1Part = "Part No.ที่ Stamp ที่ CASE สามารถอ่านได้ชัดเจน  \n (   " + dbClss.Right(DValue.CODE, 6).Substring(0, 2) + " " + dbClss.Right(DValue.CODE, 6) + "  )";
                            }
                            else
                            {
                                Line1Part = "Part No.ที่ Stamp ที่ CASE สามารถอ่านได้ชัดเจน  \n (   " + dbClss.Right(DValue.CODE, 7).Substring(0, 2) + " " + dbClss.Right(DValue.CODE, 5) + "  )";
                            }
                        }

                      

                        chek24 = true;
                        if (PartName.Contains("30-") || PartName.Contains("-30"))
                        {
                            chek24 = false;
                            GP5 = "30-24";
                        }
                        else
                        {
                            if (PartName.Contains("16-24"))
                            {
                                GP5 = "16-24";
                            }
                            else if (PartName.Contains("20-24"))
                            {
                                GP5 = "20-24";
                            }
                            else if (PartName.Contains("24-24"))
                            {
                                GP5 = "24-24";
                            }
                        }

                        





                        try
                        {
                            tb_QCHD qh = db.tb_QCHDs.Where(w => w.QCNo.Equals(QCNo1)).FirstOrDefault();
                            if (qh != null)
                            {

                                //////////Find UserName////////////
                                var uc = db.tb_QCCheckUsers.Where(u => u.QCNo.Equals(QCNo1)).ToList();                               
                                foreach (var rd in uc)
                                {
                                    DN = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));
                                    if (rd.UDesc.Equals("ผู้ตรวจสอบ"))
                                    {
                                        cCheckBy1 = rd.UserName;
                                        DN1 = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));
                                    }
                                    if (rd.UDesc.Equals("พนักงานตรวจ ก่อนผลิต"))
                                    {
                                        if (cCheckBy2.Equals(""))
                                            cCheckBy2 = rd.UserName;
                                        else
                                            cCheckBy2 = cCheckBy2 + "/" + rd.UserName;

                                        DN2 = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));
                                    }
                                    if (rd.UDesc.Equals("พนักงานตรวจ หลังผลิต"))
                                    {
                                        if (cCheckBy3.Equals(""))
                                            cCheckBy3 = rd.UserName;
                                        else
                                            cCheckBy3 = cCheckBy3 + "/" + rd.UserName;

                                        DN3 = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));
                                    }
                                }

                                FormISO = qh.FormISO;
                                QHNo = qh.QCNo;
                                RefValue1 = qh.RefValue1;
                                RefValue2 = qh.RefValue2;
                                RefValue3 = qh.RefValue3;

                                Excel.Range app = worksheet.get_Range("AJ4");
                                app.Value2 = qh.ApproveBy;
                                
                                if (DN1.Equals("D"))
                                {
                                    Excel.Range check = worksheet.get_Range("AT5");
                                    check.Value2 = cCheckBy1;
                                }
                                else
                                {
                                    Excel.Range check = worksheet.get_Range("AW5");
                                    check.Value2 = cCheckBy1;
                                }

                                if(DN2.Equals("D"))
                                {
                                    Excel.Range check = worksheet.get_Range("AO20");
                                    check.Value2 = cCheckBy2;
                                }
                                else
                                {
                                    Excel.Range check = worksheet.get_Range("AT20");
                                    check.Value2 = cCheckBy2;
                                }

                                if (DN3.Equals("D"))
                                {
                                    Excel.Range check = worksheet.get_Range("AO25");
                                    check.Value2 = cCheckBy3;
                                }
                                else
                                {
                                    Excel.Range check = worksheet.get_Range("AT25");
                                    check.Value2 = cCheckBy3;
                                }


                                Excel.Range QD1 = worksheet.get_Range("K5");
                                QD1.Value2 = Convert.ToDateTime(qh.CreateDate).ToString("dd") + " วัน " + Convert.ToDateTime(qh.CreateDate).ToString("MM") + " เดือน  " + Convert.ToDateTime(qh.CreateDate).ToString("yyyy") + " ปี";




                                Excel.Range order = worksheet.get_Range("J4");
                                order.Value2 = qh.OrderQty;//db.get_QCSumQtyTAGNG(qh.QCNo, "", 99);
                                Excel.Range J16 = worksheet.get_Range("J16");
                                J16.Value2 = GP5;

                                Excel.Range B7 = worksheet.get_Range("B7");
                                B7.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 1);

                                Excel.Range B8 = worksheet.get_Range("B8");
                                B8.Value2 = Line1Part;// db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 2);

                                Excel.Range B9 = worksheet.get_Range("B9");
                                B9.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 3);

                                Excel.Range B10 = worksheet.get_Range("B10");
                                B10.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 4);

                                Excel.Range B11 = worksheet.get_Range("B11");
                                B11.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 5) + " \n " + LotMark;

                                Excel.Range B12 = worksheet.get_Range("B12");
                                B12.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 6);

                                Excel.Range B13 = worksheet.get_Range("B13");
                                B13.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 7);

                                Excel.Range B14 = worksheet.get_Range("B14");
                                B14.Value2 = db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 8);

                                C9= db.get_QC_SetDataMaster(qh.FormISO, qh.PartNo, 9);
                                Excel.Range B15 = worksheet.get_Range("B15");
                                B15.Value2 = C9;





                            }

                        }
                        catch { }




                    }

                    ////////////////////////////////////////

                    int countA = 0;
                    int TAG2 = 0;
                    var listPoint = db.sp_46_QCSelectWO_09_QCTAGSelect(QHNo).ToList();
                    if (listPoint.Count > 0)
                    {
                        foreach (var rs in listPoint)
                        {
                            countA += 1;
                            if (chek24)
                            {
                                TAG2 += 4;
                            }
                            else
                            {
                                TAG2 += 3;
                            }
                            // MessageBox.Show(countA.ToString());
                            if (countA <= 24)
                            {
                                row1 = 6;
                                var listPart = db.tb_QCGroupParts.Where(q => q.FormISO.Equals(FormISO)).OrderBy(o => o.Seq).ToList();
                                foreach (var rd in listPart)
                                {
                                    //Start Insert Checkmark  
                                    row1 += 1;
                                    Seq += 1;
                                    //Start G=7,H=
                                    if (!rd.SetData.Equals("") && row1<=15)
                                    {
                                        try
                                        {
                                            Remark = "";
                                            var gValue = db.sp_46_QCGetValue5601(rs.BarcodeTag, QHNo, rd.Seq).FirstOrDefault();
                                            PV = "P";

                                            if (gValue.CountA > 0)
                                            {
                                                PV = "O";
                                                if(gValue.CountA==99)
                                                {
                                                    PV = "";
                                                }
                                            }
                                            if(rd.Seq.Equals(9) && C9.Equals(""))
                                            {
                                                PV = "";
                                            }
                                            //var NValue = db.sp_46_QCGetValue55501(rs.BarcodeTag, QHNo, rd.Seq).FirstOrDefault();
                                            //Remark = NValue.Remark;
                                            
                                            Excel.Range Col0 = worksheet.get_Range(Getcolumn(countA + 10) + row1.ToString(), Getcolumn(countA + 10) + row1.ToString());
                                            Col0.Value2 = PV;                                              
                                           

                                        }
                                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                                        //}




                                    }
                                    //SumNG//

                                    //  NGQ = db.get_QCSumQtyTAGNG(QHNo,rs.BarcodeTag,
                                    Excel.Range CSum = worksheet.get_Range(Getcolumn(countA + 10) + "16");
                                    CSum.Value2 = TAG2;



                                }//foreach
                            }//cunt A
                        }//for
                    }



                }

                excelBook.SaveAs(tempfile);
                excelBook.Close(false);
                excelApp.Quit();

                releaseObject(worksheet);
                releaseObject(excelBook);
                releaseObject(excelApp);
                Marshal.FinalReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
                System.Diagnostics.Process.Start(tempfile);

            }
            catch { }

        }
        public static void PrintData033(string WO, string PartNo, string QCNo1)
        {
            try
            {
                string DATA = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = System.IO.Path.GetTempPath();
                string FileName = "FM-PD-033.xls";
                string tempfile = tempPath + FileName;
                DATA = DATA + @"QC\" + FileName;

                if (File.Exists(tempfile))
                {
                    try
                    {
                        File.Delete(tempfile);
                    }
                    catch { }
                }

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  DATA, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                // progressBar1.Maximum = 51;
                // progressBar1.Minimum = 1;
                int row1 = 14;
               
                string PV = "P";
                string QHNo = QCNo1;
                string FormISO = "";
               
                string DN = "";
                string cIssueBy1 = "";
                string cIssueBy2 = "";
                string cCheckBy1 = "";
                string cCheckBy2 = "";
                string cCheckBy3 = "";
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   
                    string LotNo = "";
                    
                    string RefValue2 = "";
                    string RefValue3 = "";
                    string PartName = "";
                   
                    bool chek24 = true;
                    string HeaderValue1 = "Piggy Back Checksheet การตรวจสอบด้วยตนเอง　（Size 24）";
                    string HeaderValue2 = "Piggy Back Checksheet การตรวจสอบด้วยตนเอง　（Size 30）";
                    ///////////////SETValue/////////////////
                    var DValue = db.sp_46_QCSelectWO_01(WO).FirstOrDefault();
                    if (DValue != null)
                    {
                        DN = DValue.DayNight;
                        PartName = DValue.NAME;
                        Excel.Range CStamp = worksheet.get_Range("O6");
                        CStamp.Value2 = DValue.CODE;

                        Excel.Range WWo = worksheet.get_Range("U5");
                        WWo.Value2 = DValue.PORDER;

                        //Excel.Range CName = worksheet.get_Range("C3");
                        //CName.Value2 = DValue.DeliveryDate;
                        //Excel.Range QD = worksheet.get_Range("D3");
                        //QD.Value2 = DValue.OrderQty;
                        //Excel.Range CDate = worksheet.get_Range("I3");
                        //CDate.Value2 = DValue.LotNo;
                        chek24 = true;
                        RefValue2 = "6300～7300　N";
                        RefValue3 = "5320～6200　N";
                        if (PartName.Contains("-30") || PartName.Contains("30-"))
                        {
                            chek24 = false;
                            RefValue2 = "８２００～９７２０　N";
                            RefValue3 = "７０６０～８４２０　N";
                        }

                        Excel.Range Header1 = worksheet.get_Range("I1");
                        Excel.Range Line19 = worksheet.get_Range("I19");
                        Excel.Range LIne20 = worksheet.get_Range("I20");
                        if (chek24)
                        {
                            Header1.Value2 = HeaderValue1;
                            Line19.Value2 = RefValue2;
                            LIne20.Value2 = RefValue3;
                        }
                        else
                        {
                            Header1.Value2 = HeaderValue2;
                            Line19.Value2 = RefValue2;
                            LIne20.Value2 = RefValue3;
                        }




                        try
                        {
                            tb_QCHD qh = db.tb_QCHDs.Where(w => w.QCNo.Equals(QCNo1)).FirstOrDefault();
                            if (qh != null)
                            {
                                FormISO = qh.FormISO;
                                LotNo = qh.LotNo;

                                //////////Find UserName////////////
                                var uc = db.tb_QCCheckUsers.Where(u => u.QCNo.Equals(QCNo1)).ToList();
                                int r2 = 0;
                                int r3 = 0;
                                foreach (var rd in uc)
                                {
                                    DN = dbShowData.CheckDayN(Convert.ToDateTime(rd.ScanDate));                                   

                                    if (rd.UDesc.Equals("ผู้ตรวจสอบ หัว"))
                                        cCheckBy1 = rd.UserName;
                                    if (rd.UDesc.Equals("ผู้ตรวจสอบ กลาง"))
                                        cCheckBy2 = rd.UserName;
                                    if (rd.UDesc.Equals("ผู้ตรวจสอบ ท้าย"))
                                        cCheckBy3 = rd.UserName;


                                }

                                //Excel.Range CDate1 = worksheet.get_Range("AH2");
                                //CDate1.Value2 = Convert.ToDateTime(qh.CreateDate).ToString("yyyy");
                                //Excel.Range CDate2 = worksheet.get_Range("AK2");
                                //CDate2.Value2 = Convert.ToDateTime(qh.CreateDate).ToString("MM");
                                //Excel.Range CDate3 = worksheet.get_Range("AN2");
                                //CDate3.Value2 = Convert.ToDateTime(qh.CreateDate).ToString("dd");

                                Excel.Range AD2 = worksheet.get_Range("AD2");
                                AD2.Value2 = Convert.ToDateTime(qh.CreateDate).ToString("yyyy")+"　　ปี     "+ Convert.ToDateTime(qh.CreateDate).ToString("MM") + "　 เดือน     "+ Convert.ToDateTime(qh.CreateDate).ToString("dd") + "　　วัน";
                                
                                Excel.Range Ap = worksheet.get_Range("AP4");
                                Ap.Value2 = qh.ApproveBy;

                                Excel.Range O9 = worksheet.get_Range("O9");
                                O9.Value2 = cCheckBy1;

                                Excel.Range Q9 = worksheet.get_Range("Q9");
                                Q9.Value2 = cCheckBy2;

                                Excel.Range S9 = worksheet.get_Range("S9");
                                S9.Value2 = cCheckBy3;
                                
                                QHNo = qh.QCNo;
                                // RefValue1 = qh.RefValue1;
                                //   RefValue2 = qh.RefValue2;
                                //  RefValue3 = qh.RefValue3;
                                

                                Excel.Range O19 = worksheet.get_Range("O19");
                                O19.Value2 = db.get_QC_DATAPoint(qh.QCNo, "", 8);

                                Excel.Range O20 = worksheet.get_Range("O20");
                                O20.Value2 = db.get_QC_DATAPoint(qh.QCNo, "", 9);
                            }

                        }
                        catch { }

                    }

                    ////////////////////////////////////////
                    

                    int countA = 0;
                    string Colm = "";
                    string ValuePoint = "";
                    var listPoint = db.sp_46_QCSelectWO_09_QCTAGSelect(QHNo).ToList();
                    if (listPoint.Count > 0)
                    {
                        foreach (var rs in listPoint)
                        {
                            countA += 1;
                            // MessageBox.Show(countA.ToString());
                            if (rs.Seq == 1)
                            {

                                ValuePoint=db.get_QC_DATAPoint(rs.QCNo, rs.BarcodeTag, 1);
                            }
                            if (countA <= 3)
                            {
                                row1 = 11;
                                var listPart = db.tb_QCGroupParts.Where(q => q.FormISO.Equals(FormISO)).OrderBy(o => o.Seq).ToList();
                                foreach (var rd in listPart)
                                {
                                    //Start Insert Checkmark  
                                    row1 += 1;
                                    //Start G=7,H=
                                    if (!rd.SetData.Equals("") && row1 <= 21)
                                    {
                                        try
                                        {
                                            
                                            var gValue = db.sp_46_QCGetValue5601(rs.BarcodeTag, QHNo, rd.Seq).FirstOrDefault();
                                            PV = "P";

                                            if (gValue.CountA > 0)
                                            {
                                                PV = "O";
                                                if (gValue.CountA == 99)
                                                    PV = "";
                                            }
                                            if(rd.Seq.Equals(6))
                                            {
                                                PV = "";
                                            }

                                          //  if (row1 == 19)
                                          //      row1 += 5;

                                            if (countA == 1)
                                                Colm = "O";

                                            else if (countA == 2)
                                                Colm = "Q";
                                            else
                                                Colm = "S";
                                            if (row1 == 19 || row1 == 20 || row1==17)
                                            {

                                            }
                                            else if (row1 == 12)
                                            {
                                                Excel.Range Col0 = worksheet.get_Range(Colm + "" + row1.ToString());
                                                Col0.Value2 = ValuePoint;
                                            }
                                            else if (row1 == 15)
                                            {
                                                if (gValue.CountA < 99)
                                                {
                                                    Excel.Range Col0 = worksheet.get_Range(Colm + "" + row1.ToString());
                                                    Col0.Value2 = LotNo;
                                                }
                                            }
                                            else
                                            {
                                                if (row1 != 9 && row1 != 8)
                                                {
                                                    Excel.Range Col0 = worksheet.get_Range(Colm + "" + row1.ToString());
                                                    Col0.Value2 = PV;
                                                }
                                            }

                                            




                                        }
                                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                                    }
                                    //SumNG//

                                    //  NGQ = db.get_QCSumQtyTAGNG(QHNo,rs.BarcodeTag,
                                    //Excel.Range CSum = worksheet.get_Range(Getcolumn(countA + 6) + "26");
                                    //CSum.Value2 = rs.NGQty;



                                }//foreach
                            }//cunt A
                        }//for
                    }



                }

                excelBook.SaveAs(tempfile);
                excelBook.Close(false);
                excelApp.Quit();

                releaseObject(worksheet);
                releaseObject(excelBook);
                releaseObject(excelApp);
                Marshal.FinalReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
                System.Diagnostics.Process.Start(tempfile);

            }
            catch { }

        }
        public static void InsertQCChecker(string Uid,string QCNo,string TypeS,string Desc)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!Desc.Equals(""))
                    {
                        string DN= dbShowData.CheckDayN(DateTime.Now);
                        tb_QCCheckUser cu = db.tb_QCCheckUsers.Where(u => u.UserID.Equals(Uid) && u.QCNo.Equals(QCNo) && u.UType.Equals(TypeS)
                        && u.UDesc.Equals(Desc)
                        && u.DayN.Equals(DN)
                        ).FirstOrDefault();
                        if (cu != null)
                        {

                        }
                        else
                        {
                            tb_User us = db.tb_Users.Where(u => u.UserID.Equals(Uid)).FirstOrDefault();
                            if (us != null)
                            {

                                tb_QCCheckUser cn = new tb_QCCheckUser();
                                cn.QCNo = QCNo;
                                cn.ScanDate = DateTime.Now;
                                cn.UserID = Uid;
                                cn.UserName = us.NameApp;
                                cn.UType = TypeS;
                                cn.DayN = DN;
                                cn.UDesc = Desc;
                                db.tb_QCCheckUsers.InsertOnSubmit(cn);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            catch { }
        }
        public static void DeleteQCChecker(int id)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_QCCheckUser cx = db.tb_QCCheckUsers.Where(w => w.id.Equals(id)).FirstOrDefault();
                    if (cx != null)
                    {
                        db.tb_QCCheckUsers.DeleteOnSubmit(cx);
                        db.SubmitChanges();
                    }
                }

            }
            catch { }
        }

        private static string[] ConvertToStringArray(System.Array values)
        {

            // create a new string array
            string[] theArray = new string[values.Length];

            // loop through the 2-D System.Array and populate the 1-D String Array
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }

            return theArray;
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                // MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private static string Getcolumn(int Col)
        {
            string RT = "";

            if (Col.Equals(1))
                RT = "A";
            else if (Col.Equals(2))
                RT = "B";
            else if (Col.Equals(3))
                RT = "C";
            else if (Col.Equals(4))
                RT = "D";
            else if (Col.Equals(5))
                RT = "E";
            else if (Col.Equals(6))
                RT = "F";
            else if (Col.Equals(7))
                RT = "G";
            else if (Col.Equals(8))
                RT = "H";
            else if (Col.Equals(9))
                RT = "I";
            else if (Col.Equals(10))
                RT = "J";
            else if (Col.Equals(11))
                RT = "K";
            else if (Col.Equals(12))
                RT = "L";
            else if (Col.Equals(13))
                RT = "M";
            else if (Col.Equals(14))
                RT = "N";
            else if (Col.Equals(15))
                RT = "O";
            else if (Col.Equals(16))
                RT = "P";
            else if (Col.Equals(17))
                RT = "Q";
            else if (Col.Equals(18))
                RT = "R";
            else if (Col.Equals(19))
                RT = "S";
            else if (Col.Equals(20))
                RT = "T";
            else if (Col.Equals(21))
                RT = "U";
            else if (Col.Equals(22))
                RT = "V";
            else if (Col.Equals(23))
                RT = "W";
            else if (Col.Equals(24))
                RT = "X";
            else if (Col.Equals(25))
                RT = "Y";
            else if (Col.Equals(26))
                RT = "Z";

           else if (Col.Equals(27))
                RT = "AA";
            else if (Col.Equals(28))
                RT = "AB";
            else if (Col.Equals(29))
                RT = "AC";
            else if (Col.Equals(30))
                RT = "AD";
            else if (Col.Equals(31))
                RT = "AE";
            else if (Col.Equals(32))
                RT = "AF";
            else if (Col.Equals(33))
                RT = "AG";
            else if (Col.Equals(34))
                RT = "AH";
            else if (Col.Equals(35))
                RT = "AI";
            else if (Col.Equals(36))
                RT = "AJ";
   


                return RT;
        }
    }
}
