using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;

namespace StockControl
{
    class dbClss
    {
        public string versioin = "ss2";
        public static Telerik.WinControls.UI.RadRibbonForm CreateForm(string form)
        {
            try
            {
                //StockControl.CreatePart
                Type t = Type.GetType("StockControl." + form);
                return (Telerik.WinControls.UI.RadRibbonForm)Activator.CreateInstance(t);
            }
            // catch (Exception ex) { ErrorAdd("Open CreateForm" + "FMS." + form, ex.ToString(), "BaseClass.cs"); return null; }
            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + "ไม่มีไฟล์ link"); return null; }

        }
        // ฟังก์ชั่น Update DatagridView
        public static void DGVCOMMIT(object sender, EventArgs e) //Commit
        {
            DataGridView obj = null;
            obj = (DataGridView)sender;
            if (obj.CurrentCell is DataGridViewCheckBoxCell || obj.CurrentCell is DataGridViewComboBoxCell)
            {
                obj.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        static SaveFileDialog sv = new SaveFileDialog();
        public static void ExportGridCSV(RadGridView rv)
        {

           //sv.fi
            sv.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            sv.Title = "Save an CSV File";
            sv.ShowDialog();
            if (sv.FileName != "")
            {


                ExportToCSV exporter = new ExportToCSV(rv);
                exporter.FileExtension = "csv";
                exporter.ColumnDelimiter = ",";
                exporter.HiddenColumnOption = HiddenOption.DoNotExport;
                exporter.HiddenRowOption = HiddenOption.DoNotExport;
                exporter.SummariesExportOption = SummariesOption.DoNotExport;
                exporter.RunExport(sv.FileName);
                MessageBox.Show("Export Completed");

            }
            
        }
        public static void ExportGridXlSX(RadGridView rv)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel File (*.xls)|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                ExportToExcelML exporter = new ExportToExcelML(rv);
               
                exporter.HiddenRowOption = HiddenOption.DoNotExport;
                exporter.HiddenColumnOption = HiddenOption.DoNotExport;
                exporter.RunExport(dialog.FileName);
                MessageBox.Show("Export Finished");
            }
        }
        public static void ExportGridXlSX2(RadGridView rv,string FileName)
        {
            //SaveFileDialog dialog = new SaveFileDialog();
            //dialog.Filter = "Excel File (*.xls)|*.xls";
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{

                ExportToExcelML exporter = new ExportToExcelML(rv);

                exporter.HiddenRowOption = HiddenOption.DoNotExport;
                exporter.HiddenColumnOption = HiddenOption.DoNotExport;
                exporter.RunExport(FileName);
            //    MessageBox.Show("Export Finished");
            //}
        }

        public static void AddError(string Mathod,string Error,string Screen)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {


                    ErrorLog lg = new ErrorLog();
                    lg.ErrorLogNo = 0;
                    lg.ErrorMethod = Mathod;
                    lg.ErrorLogMessage = Error;
                    lg.ErrorLogScreen = Screen;
                    lg.ErrorLogBy = System.Environment.UserName;
                    lg.ErrorLoginMachineName = System.Environment.MachineName;
                    lg.ErrorLogDateTime = DateTime.Now;
                    db.ErrorLogs.InsertOnSubmit(lg);
                    db.SubmitChanges();
                }
                catch { }
            }
        }
        public static void AddHistory(string Screen,string App,string Detail,string Ref)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //MessageBox.Show(Screen);
                    tb_History hy = new tb_History();
                    hy.id = 0;
                    hy.ScreenName = Screen;
                    hy.ApplicationNme = App;
                    hy.Detail = Detail;
                    hy.RefNo = Ref;
                    hy.CreateBy = System.Environment.UserName;
                    hy.CreateDate = DateTime.Now;
                    db.tb_Histories.InsertOnSubmit(hy);
                    db.SubmitChanges();
                }

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        public static string GetNo(int ControlNo,int Ac)
        {
            string No = "";

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.Sp_GetNameControl_001(ControlNo, Ac) select ix).ToList();
                if(g.Count>0)
                {
                    No = g.FirstOrDefault().GetNo;
                }
            }

                return No;
        }
        public static decimal Insert_Stock(string CodeNo, decimal Qty,string Screen,string Type)
        {
            decimal re = 0;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                decimal qty_can = 0;
                string tt = "";
                if (Qty < 0)
                {
                    tt = "ลบ";
                    qty_can = -Qty;   //-(-10)   =>>> 10   เพื่อไปเปรียบเทียบ
                }
                else
                {
                    tt = "เพิ่ม";
                }
                decimal Remain_Inv = 0;
                decimal Remain_Dl = 0;
                decimal Remain_Temp = 0;
                



                var g = (from ix in db.tb_Items
                         where ix.CodeNo.Trim() == CodeNo.Trim() //&& ix.Status == "Active"
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {
                    var gg = (from ix in db.tb_Items
                              where ix.CodeNo.Trim() == CodeNo.Trim()
                              select ix).First();

                    decimal.TryParse(StockControl.dbClss.TSt(gg.StockInv), out Remain_Inv);
                    decimal.TryParse(StockControl.dbClss.TSt(gg.StockDL), out Remain_Dl);

                    if(Type.Equals("Inv"))
                    {
                        if(Qty<0) //เบิกของออก  Shipping,Cancel Receive
                        {
                            if (qty_can > Remain_Inv) //เบิกของออกจะตัดที่ stock ปกติก่อน แต่ถ้าไม่พอจะไปเอาที่ Temp (DL)
                            {
                                Remain_Temp = qty_can - Remain_Inv;
                                gg.StockInv = 0;            //ตัด Stock ปกติให้เป็น 0
                                gg.StockDL = Remain_Dl - Remain_Temp;   //แล้วมาตัดที่ temp 
                            }
                            else//ใน Stock ปกติมีของพอสำหรับการเบิกก็จะตัดเพียง Stock inv เดียว
                            {
                                gg.StockInv = Remain_Inv + Qty;
                            }
                        }
                        else //ของเข้า Receive,Cancel Shipping
                        {
                            gg.StockInv = Remain_Inv + Qty;
                        }
                    }
                    else //DL
                    {
                        //if (Qty < 0) //เบิกของออก  Shipping,Cancel Receive
                        //{

                        //}
                        //else //ของเข้า Receive,Cancel Shipping
                        //{
                            gg.StockDL = Remain_Dl + Qty;
                        //}
                    }

                    dbClss.AddHistory(Screen, CodeNo, tt + " Stock [" + CodeNo +  " จำนวน "+ Qty.ToString()+"]", "");

                    db.SubmitChanges();
                   
                }
            }

            return re;
        }
        public static decimal Insert_StockTemp(string CodeNo, decimal Qty, string Screen, string Type)
        {
            decimal re = 0;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                decimal qty_can = 0;
                string tt = "";
                if (Qty < 0)
                {
                    tt = "ลบ";
                    qty_can = -Qty;   //-(-10)   =>>> 10   เพื่อไปเปรียบเทียบ
                }
                else
                {
                    tt = "เพิ่ม";
                }
                decimal Remain_BackOrder = 0;
                
                var g = (from ix in db.tb_Items
                         where ix.CodeNo.Trim() == CodeNo.Trim() //&& ix.Status == "Active"
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {
                    var gg = (from ix in db.tb_Items
                              where ix.CodeNo.Trim() == CodeNo.Trim()
                              select ix).First();

                    decimal.TryParse(StockControl.dbClss.TSt(gg.StockBackOrder), out Remain_BackOrder);
                   
                   gg.StockBackOrder = Remain_BackOrder + Qty;
                     

                    dbClss.AddHistory(Screen, CodeNo, tt + " Stock BackOrder [" + CodeNo + " จำนวน " + Qty.ToString() + "]", "");

                    db.SubmitChanges();

                }
            }

            return re;
        }
        public static DateTime ChangeFormat(string ds)
        {
            CultureInfo c = new CultureInfo("en-us", true);
            c.DateTimeFormat.DateSeparator = ".";
            //c.DateTimeFormat.TimeSeparator= ".";//this will fail
            c.DateTimeFormat.TimeSeparator = ":";//this will work since TimeSeparator and DateSeparator  are different.
            Thread.CurrentThread.CurrentCulture = c;
            DateTime dt;
            DateTime.TryParse(ds, out dt);

            //Console.WriteLine(s + "\n");
            //Console.WriteLine(DateTime.Now + "\n");
            //Console.WriteLine(dt.ToString() + "\n");

            DateTime.TryParse(ds,
                              CultureInfo.CurrentCulture.DateTimeFormat,
                              DateTimeStyles.None,
                              out dt);
            return dt;
        }
        public static int getMonth(string MMM)
        {
            int cal = 0;

            switch(MMM.ToUpper())
            {
                case "JAN" : { cal = 1; }break;
                case "FEB": { cal = 2; } break;
                case "MAR": { cal = 3; } break;
                case "APR": { cal = 4; } break;
                case "MAY": { cal = 5; } break;
                case "JUN": { cal = 6; } break;
                case "JUL": { cal = 7; } break;
                case "AUG": { cal = 8; } break;
                case "SEP": { cal = 9; } break;
                case "OCT": { cal = 10; } break;
                case "NOV": { cal = 11; } break;
                case "DEC": { cal = 12; } break;

            }

            return cal;
        }
        public static string getMonthRevest(int MMM)
        {
            string cal = "";

            switch (MMM)
            {
                case 1: { cal = "JAN"; } break;
                case 2: { cal = "FEB"; } break;
                case 3: { cal = "MAR"; } break;
                case 4: { cal = "APR"; } break;
                case 5: { cal = "MAY"; } break;
                case 6: { cal = "JUN"; } break;
                case 7: { cal = "JUL"; } break;
                case 8: { cal = "AUG"; } break;
                case 9: { cal = "SEP"; } break;
                case 10: { cal = "OCT"; } break;
                case 11: { cal = "NOV"; } break;
                case 12: { cal = "DEC"; } break;

            }

            return cal;
        }
        public static string TryString_isNull(object Val)
        {
            try
            {
                if (Val == null)
                    return "";
                else
                    return Convert.ToString(Val);
            }
            catch { return ""; }
        }
       
        public static decimal TDe(object Val)
        {
            try
            {
                decimal Retval = 0.00m;
                if (Val == null)
                    return Retval;
                else
                {
                    Retval = Convert.ToDecimal(Val);
                    return Retval;
                }
            }
            catch { return 0.00m; }
        }
        public static string TSt(object Val)
        {
            try
            {
                if (Val == null)
                    return "";
                else return Convert.ToString(Val);
            }
            catch { return ""; }
        }
        public static int TInt(object Val)
        {
            try
            {
                if (Val == null)
                    return 0;
                else return Convert.ToInt32(Val);
            }
            catch { return 0; }
        }
        public static bool TBo(object Val)
        {
            try
            {
                if (Val == null)
                    return false;
                else return Convert.ToBoolean(Val);
            }
            catch { return false; }
        }
        public static double TDo(object Val)
        {
            try
            {
                if (Val == null)
                    return 0;
                else return Convert.ToDouble(Val);
            }
            catch { return 0; }
        }
        public static DateTime? TDa(object Val)
        {
            try
            {
                if (Val == null)
                    return null;
                else
                    return Convert.ToDateTime(Val, new CultureInfo("en-US"));
            }
            catch { return null; }
        }
        public static void SetRowNo(RadGridView Grid)//เลขลำดับ
        {
            int i = 1;
            Grid.Rows.Where(o => o.IsVisible).ToList().ForEach(o =>
            {
                o.Cells["No"].Value = i;
                i++;
            });
        }
        public static void SetRowNo1(RadGridView Grid)//เลขลำดับ
        {
            int i = 1;
            Grid.Rows.Where(o => o.IsVisible).ToList().ForEach(o =>
            {
                o.Cells["dgvNo"].Value = i;
                i++;
            });
        }
        public static void checkDigit(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

        }
        public static void CheckDigitDecimal(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
        public static void CheckDigitDecimailKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue > 57 && e.KeyValue < 48 && e.KeyValue != 8 && e.KeyValue != 46)
            {
                e.Handled = true;
            }
        }
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others         will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        private static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            catch (Exception ex) { return null; }
        }
        //Barcode 2D
        public static byte[] SaveQRCode2D(string Condition)
        {
            try
            {
                //string Data2D = "ReqNo-";
                ////-----------ทำ บาร์โค้ด 2D
                //if (Condition.Equals("Kanban"))
                //    Data2D = "";

                //Data2D = txtReqNo.Text;
                //// สร้าง Image 2D    
                Image image2D = QRBarcode2D(Condition);
                //// แปลง Image เป็น Byte เพิ่อนำเข้า SQL                    
                //bye_2D = ImageToByteArray(image2D);
                //-----------------------

                return ImageToByteArray(image2D);
            }
            catch (Exception ex) { return null; }
        }
        private static Image QRBarcode2D(string SystemNo)
        {
            // System.Threading.Thread.Sleep(5000);
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            try
            {
                //SystemNo = SystemNo.Substring(0, 35);
                String encoding = "Byte";
                if (encoding == "Byte")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                }
                else if (encoding == "AlphaNumeric")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                }
                else if (encoding == "Numeric")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                }

                try
                {
                    int scale = Convert.ToInt32(3);
                    qrCodeEncoder.QRCodeScale = scale;
                }
                catch
                {
                    //MessageBox.Show("Invalid size!" + ex.Message);
                    // return;
                }

                try
                {
                    int version = 3;
                    qrCodeEncoder.QRCodeVersion = version;
                }
                catch
                {
                    // MessageBox.Show("Invalid version !" + ex.Message);
                }


                string errorCorrect = "M";
                if (errorCorrect == "L")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                else if (errorCorrect == "M")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                else if (errorCorrect == "Q")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                else if (errorCorrect == "H")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;


            }
            catch (Exception ex) { }//ErrorAdd("INV EXEx", ex.ToString(), "BaseClass TAXWin.cs"); }
            String data = SystemNo;
            return qrCodeEncoder.Encode(data);
        }
    }
}
