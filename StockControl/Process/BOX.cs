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
using Telerik.WinControls;

namespace StockControl
{
    public partial class BOX : Telerik.WinControls.UI.RadRibbonForm
    {
        public BOX()
        {            
            
            InitializeComponent();
            lblCount.Text = "Count 0";
            LinkPage = "";
        }

        public BOX(TextBox tx)
        {
            
            InitializeComponent();
            lblCount.Text = "Count 0";
            textRQ = tx;
            LinkPage = "Link";
        }

        //private int RowView = 50;
        //private int ColView = 10;
        string LinkPage = "";
        TextBox textRQ;
        DataTable dt3 = new DataTable();
        DataTable dt = new DataTable();
        string PathFile = "";
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            
            //dt3.Columns.Add(new DataColumn("Code", typeof(string)));
            //dt3.Columns.Add(new DataColumn("NAME", typeof(string)));
            //dt3.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            //dt3.Columns.Add(new DataColumn("SHELVES", typeof(string)));

        }
        private void Unit_Load(object sender, EventArgs e)
        {
            // RMenu3.Click += RMenu3_Click;
            //  RMenu4.Click += RMenu4_Click;
            // RMenu5.Click += RMenu5_Click;
            //  RMenu6.Click += RMenu6_Click;
            // radGridView1.ReadOnly = true;
            dtStartDate.Value = DateTime.Now;
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            dtDate1.Value = firstDayOfMonth;
            dtDate2.Value = lastDayOfMonth;
            radGridView1.AutoGenerateColumns = false;
            //  GETDTRow();
            this.radGridView1.TableElement.RowHeight = 65;
            this.radGridView1.EnableAlternatingRowColor = true;

            // LoadDataDefault();
            DataLoad();
        }
        private void LoadDataDefault()
        {
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        tb_Path ph = db.tb_Paths.Where(p => p.PathCode == "Image").FirstOrDefault();
            //        if(ph!=null)
            //        {
            //            PathFile = ph.PathFile;
            //        }
            //    }
            //}
            //catch { }
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
            DeleteUnit();
            DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
            NewClick();

        }
        
        private void DataLoad()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int ck = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    // radGridView1.DataSource = db.sp_44_BOX_ListSelect_01().ToList();
                    var ListQ = db.tb_BOXes.ToList();//.Where(r => r.Status.Equals("Active")).ToList();
                    radGridView1.DataSource = ListQ;
                    foreach (var x in radGridView1.Rows)
                    {                       
                        ck += 1;
                        x.Cells["No"].Value = ck;
                    }

                    int countA = 0;
                    foreach(var rx in ListQ)
                    {
                        //if (rx.Picture.Length>0)
                        //{
                        try
                        {
                            countA += 1;
                            radGridView1.Rows[countA-1].Cells["Pic"].Value = dbClss.BinaryToImage(rx.Picture);
                            
                        }
                        catch { }
                        //}
                    }

                }
            }
            catch { }
            this.Cursor = Cursors.Default;

            
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

         
            return ck;
        }

        private bool AddUnit()
        {
            bool ck = false;
         

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;
         
      

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void NewClick()
        {
           
        }
        private void EditClick()
        {
           
        }
        private void ViewClick()
        {
          
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();

            //genLOt();
        }

        private void genLOt()
        {
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
        
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {            

                if (e.RowIndex >= 0)
                {
                    string ItemNo = radGridView1.Rows[e.RowIndex].Cells["ItemNo"].Value.ToString();
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        if (e.ColumnIndex == radGridView1.Columns["Status"].Index && e.RowIndex >= 0)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.Status = radGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                                db.SubmitChanges();
                            }
                        }
                        else if(e.ColumnIndex == radGridView1.Columns["W"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.W = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["W"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["H"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.H = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["H"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["L"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.L = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["L"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["QTY"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.QTY = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["QTY"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["SNP"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.SNP = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["SNP"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["Customer"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.Customer = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["Customer"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["PackageFG"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.PackageFG = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["PackageFG"].Value);
                                db.SubmitChanges();
                            }
                        }
                        else if (e.ColumnIndex == radGridView1.Columns["PackageType"].Index)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo == ItemNo).FirstOrDefault();
                            if (bx != null)
                            {
                                bx.PackageType = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["PackageType"].Value);
                                db.SubmitChanges();
                            }
                        }

                    }

                }
            }
            catch(Exception ex) { }


        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
            //else if (e.KeyData == (Keys.Control | Keys.N))
            //{
            //    if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        NewClick();
            //    }
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                DeleteUnit();
                DataLoad();
            
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
            try
            {
                if (chkTool.Checked)
                {
                    txtItemNo.Text = radGridView1.Rows[e.RowIndex].Cells["ItemNo"].Value.ToString();
                }
            }
            catch { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
           dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
          
        }

        private void ImportData()
        {

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

        private void radButtonElement2_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //DataLoad();
            radButtonElement1_Click(sender,e);
        }

        private void radButtonElement2_Click_1(object sender, EventArgs e)
        {
            if (row >= 0)
            {
                string code = radGridView1.Rows[row].Cells["ItemNo"].Value.ToString();
                BoxItemListImage im = new BoxItemListImage(code);
                im.ShowDialog();
            }

        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
           
        }

        private void txtItemNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                DataLoad();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการปริ้น Barcode ?", "Barcode TAG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PrintTAG();
            }
           

        }
        private void PrintTAG()
        {
            //Temp TAG//
            try
            {
                string ItemNo2 = "";
                string ItemName = "";
                this.Cursor = Cursors.WaitCursor;
                int row12 = -1;
                radGridView1.EndEdit();
                foreach(GridViewRowInfo rd in radGridView1.Rows)
                {
                    if(Convert.ToBoolean(rd.Cells["S"].Value) && row12.Equals(-1))
                    {
                        row12 = Convert.ToInt32(rd.Cells["No"].Value)-1;
                    }
                }
                if (row12 >= 0)
                {
                    ItemNo2 = radGridView1.Rows[row12].Cells["ItemNo"].Value.ToString();
                    ItemName = radGridView1.Rows[row12].Cells["Description"].Value.ToString();
                    int Qty = Convert.ToInt32(radGridView1.Rows[row12].Cells["QTY"].Value);
                   
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_44_BOX_CreateList(ItemNo2);
                        for (int i = 1; i <= Qty; i++)
                        {
                            tb_BoxList bs = new tb_BoxList();
                            byte[] Barcode = dbClss.SaveQRCode2D(ItemNo2 + "," + i.ToString());

                            bs.Active = true;
                            bs.Barcode = Barcode;
                            bs.ItemNo = ItemNo2;
                            bs.Remark = ItemNo2 + "," + i.ToString();
                            bs.Running = i;
                            bs.CreateBy = dbClss.UserID;
                            bs.CreateDate = DateTime.Now;
                            
                            db.tb_BoxLists.InsertOnSubmit(bs);
                            db.SubmitChanges();
                        }
                    }
                }


                Report.Reportx1.WReport = "BOXList";
                Report.Reportx1.Value = new string[3];
                Report.Reportx1.Value[0] = ItemNo2;
                Report.Reportx1.Value[1] = ItemName;
                
                Report.Reportx1 op = new Report.Reportx1("BOXBarcode01.rpt");
                op.Show();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
        private void PrintTAG2()
        {
            //Temp TAG//
            try
            {
                string ItemNo2 = "";
                string ItemName = "";
                this.Cursor = Cursors.WaitCursor;
                int row12 = -1;
                radGridView1.EndEdit();
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    if (Convert.ToBoolean(rd.Cells["S"].Value) && row12.Equals(-1))
                    {
                        row12 = Convert.ToInt32(rd.Cells["No"].Value) - 1;
                    }
                }
                if (row12 >= 0)
                {
                    ItemNo2 = radGridView1.Rows[row12].Cells["ItemNo"].Value.ToString();
                    ItemName = radGridView1.Rows[row12].Cells["Description"].Value.ToString();
                    int Qty = Convert.ToInt32(radGridView1.Rows[row12].Cells["QTY"].Value);

                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_44_BOX_CreateList(ItemNo2);
                        for (int i = 1; i <= Qty; i++)
                        {
                            tb_BoxList bs = new tb_BoxList();
                            byte[] Barcode = dbClss.SaveQRCode2D(ItemNo2 + "," + i.ToString());

                            bs.Active = true;
                            bs.Barcode = Barcode;
                            bs.ItemNo = ItemNo2;
                            bs.Remark = ItemNo2 + "," + i.ToString();
                            bs.Running = i;
                            bs.CreateBy = dbClss.UserID;
                            bs.CreateDate = DateTime.Now;

                            db.tb_BoxLists.InsertOnSubmit(bs);
                            db.SubmitChanges();
                        }
                    }
                }


                Report.Reportx1.WReport = "BOXList";
                Report.Reportx1.Value = new string[3];
                Report.Reportx1.Value[0] = ItemNo2;
                Report.Reportx1.Value[1] = ItemName;

                Report.Reportx1 op = new Report.Reportx1("BOXBarcode02.rpt");
                op.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            
            try
            {
                if (MessageBox.Show("ออกรายงาน \n ใช้เวลาคำนวณสักครู่..", "ออกรายงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    progressBar1.Visible = true;
                    lblCalculate.Visible = true;
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = 100;
                    progressBar1.Step = 1;
                    int CountA = 0;
                    int ValueA = 1;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.CommandTimeout = 0;

                        db.sp_44_BOX_Calculate01();
                        db.sp_44_BOXJObQ_StartBoxDelete();

                        var bl = db.tb_BOXes.Where(b => b.Status.Equals("Active")).ToList();
                        if(!txtItemNo.Text.Equals(""))
                        {
                            bl= db.tb_BOXes.Where(b => b.Status.Equals("Active") && b.ItemNo.Contains(txtItemNo.Text)).ToList();
                            
                        }

                        progressBar1.Maximum = bl.Count;
                        foreach(var rd in bl)
                        {
                            //MessageBox.Show(rd.ItemNo);
                            db.sp_44_BOXJObQ_StartBoxRunFirst_Dynamics(rd.ItemNo);
                            db.sp_44_BOXJObQ_StartBoxRun(dtStartDate.Value,rd.ItemNo);
                         
                            progressBar1.Value = ValueA;
                            progressBar1.PerformStep();
                           
                            ValueA += 1;
                            /////////////////////////////
                            CountA += 1;
                        }
                        
                    }

                    if (CountA > 0)
                    {
                        Report.Reportx1.WReport = "BoxReport";
                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = "";
                        Report.Reportx1.Value[1] = dbClss.UserID;
                        Report.Reportx1 op = new Report.Reportx1("BoxReport.rpt");
                        op.Show();
                    }
                }
                
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
            lblCalculate.Visible = false;
            progressBar1.Visible = false;
            dtStartDate.Value.ToString();
        }
        
        string Date1 = DateTime.Now.ToString("dd/MMM/yyyy");
        string Date2 = DateTime.Now.AddDays(1).ToString("dd/MMM/yyyy");
        string Date3 = DateTime.Now.AddDays(2).ToString("dd/MMM/yyyy");
        string Date4 = DateTime.Now.AddDays(3).ToString("dd/MMM/yyyy");
        string Date5 = DateTime.Now.AddDays(4).ToString("dd/MMM/yyyy");
        string Date6 = DateTime.Now.AddDays(5).ToString("dd/MMM/yyyy");
        string Date7 = DateTime.Now.AddDays(6).ToString("dd/MMM/yyyy");
        string DateTPIcs1 = DateTime.Now.ToString("yyyyMMdd") + "1";
        string DateTPIcs2 = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
        string DateTPIcs3 = DateTime.Now.AddDays(2).ToString("yyyyMMdd") + "1";
        string DateTPIcs4 = DateTime.Now.AddDays(3).ToString("yyyyMMdd") + "1";
        string DateTPIcs5 = DateTime.Now.AddDays(4).ToString("yyyyMMdd") + "1";
        string DateTPIcs6 = DateTime.Now.AddDays(5).ToString("yyyyMMdd") + "1";
        string DateTPIcs7 = DateTime.Now.AddDays(6).ToString("yyyyMMdd") + "1";
        string Date1c = DateTime.Now.ToString("yyyy-MM-dd");
        string Date2c = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        string Date3c = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
        string Date4c = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
        string Date5c = DateTime.Now.AddDays(4).ToString("yyyy-MM-dd");
        string Date6c = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");
        string Date7c = DateTime.Now.AddDays(6).ToString("yyyy-MM-dd");

        private void AddPrintTemp(string ItemNo,string ItemName,decimal Qty,string Location,int No)
        {
            try
            {
                

                using (DataClasses1DataContext db2 = new DataClasses1DataContext())
                {
                    tb_Boxtemp bm = new tb_Boxtemp();
                    bm.AllQty = Qty;
                    bm.ItemNo = ItemNo;
                    bm.ItemName = ItemName;
                    bm.Location = Location;
                    bm.Day1 = GetQtyLO(Location, DateTPIcs1, ItemNo, Date1c, Qty, "Day1");
                    bm.Day2 = GetQtyLO(Location, DateTPIcs2, ItemNo, Date2c, Qty, "Day2");
                    bm.Day3 = GetQtyLO(Location, DateTPIcs3, ItemNo, Date3c, Qty, "Day3");
                    bm.Day4 = GetQtyLO(Location, DateTPIcs4, ItemNo, Date4c, Qty, "Day4");
                    bm.Day5 = GetQtyLO(Location, DateTPIcs5, ItemNo, Date5c, Qty, "Day5");
                    bm.Day6 = GetQtyLO(Location, DateTPIcs6, ItemNo, Date6c, Qty, "Day6");
                    bm.Day7 = GetQtyLO(Location, DateTPIcs7, ItemNo, Date7c, Qty, "Day7");
                    bm.DayCaption1 = Date1;
                    bm.DayCaption2 = Date2;
                    bm.DayCaption3 = Date3;
                    bm.DayCaption4 = Date4;
                    bm.DayCaption5 = Date5;
                    bm.DayCaption6 = Date6;
                    bm.DayCaption7 = Date7;
                    bm.Datex = Date1c;
                    
                    bm.No = No;
                    db2.tb_Boxtemps.InsertOnSubmit(bm);
                    db2.SubmitChanges();
                    
                }
            }
            catch { }
        }
        private decimal GetQtyLO (string Location,string DateTpics, string ItemNo, string Datec,decimal Qtyx,string Days)
        {
            decimal RT = 0;
           
            if(Location.Equals("Blank Box Napt"))
            {
                RT = BlankBox(DateTpics, ItemNo, Datec);
                //ตั้งต้น+Return Cust-MoveTo
            }
            else if (Location.Equals("Blank Box UT"))
            {               
                RT = BlankBoxUni(DateTpics, ItemNo, Datec);
                //ตั่งต้น+รับเข้า-Return Uni

            }
            else if (Location.Equals("MOVE NAPT->UT"))
            {
                RT = BlankBoxUniM(DateTpics, ItemNo, Datec);
                //ตั่งต้น+รับเข้า-Return Uni

            }
            else if (Location.Equals("Dummy BOX"))
            {
                RT = 0;// BlankBoxUniM(DateTpics, ItemNo, Datec);
                //ตั่งต้น+รับเข้า-Return Uni

            }
            else if(Location.Equals("PD (Plan)"))
            {
                RT = PDPlan(DateTpics, ItemNo, Datec);
            }
            else if (Location.Equals("Delivery"))
            {
                RT = Delivery(DateTpics, ItemNo, Datec);
            }
            else if (Location.Equals("FG Stock"))
            {
                RT = FGStock(DateTpics, ItemNo, Datec);
            }
            else if (Location.Equals("Return Cust."))
            {
                RT = Returnx(DateTpics, ItemNo, Datec);
            }
            else if (Location.Equals("Return UT"))
            {
                RT = ReturnxUni(DateTpics, ItemNo, Datec);
            }
            else if (Location.Equals("Transfer"))
            {
                RT = Transfer(DateTpics, ItemNo, Datec);
            }
            else if (Location.Equals("Customer"))
            {
                RT = Customer(DateTpics, ItemNo, Datec, Qtyx , Days);
            }
            else if (Location.Equals("Balance Box"))
            {
                RT = BalanceBox(DateTpics, ItemNo, Datec, Qtyx);
            }

            

            return RT;
        }
        private decimal BlankBox(string DateTpics,string ItemNo,string Datec)
        {
            decimal RT = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   // RT = Convert.ToDecimal(db.g_getQty_BlankBox(ItemNo,Datec));
                }
            }
            catch { RT = 0; }
            return RT;
        }
        private decimal BlankBoxUni(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //RT = Convert.ToDecimal(db.g_getQty_BlankBoxUni(ItemNo, Datec));
                }
            }
            catch { RT = 0; }
            return RT;
        }
        private decimal BlankBoxUniM(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;
            try
            {
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    RT = Convert.ToDecimal(db.g_getQty_MoveTo(ItemNo, Datec, "Uni"));
                //}
            }
            catch { RT = 0; }

            return RT;
        }
        private decimal PDPlan(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;

            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //    var ListPD = db.sp_44_PDPlan_Qty(ItemNo, DateTpics).ToList();
                    //    decimal SumQty = 0;

                    //    foreach(var rd in ListPD)
                    //    {
                    //        SumQty += Math.Ceiling((Convert.ToDecimal(rd.Qty) / Convert.ToDecimal(rd.LotSize)));
                    //    }
                  //  RT = Math.Round(SumQty, 0);
                }
            }
            catch { RT = 0; }

            return RT;
        }
        private decimal Delivery(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //var ListPD = db.sp_44_Delivery_Qty(ItemNo, DateTpics).ToList();
                    //decimal SumQty = 0;

                    //foreach (var rd in ListPD)
                    //{
                    //    SumQty += Math.Ceiling((Convert.ToDecimal(rd.Qty) / Convert.ToDecimal(rd.LotSize)));
                    //}
                    //RT = Math.Round(SumQty, 0);
                }
            }
            catch { RT = 0; }
            return RT;
        }
        private decimal FGStock(string DateTpics, string ItemNo, string Datec)
        {
            /*
            decimal RT = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var ListPD = db.sp_44_FGStock_Qty(ItemNo, DateTpics).ToList();
                    decimal SumQty = 0;

                    foreach (var rd in ListPD)
                    {
                        SumQty += Math.Floor((Convert.ToDecimal(rd.Qty) / Convert.ToDecimal(rd.LotSize)));
                    }
                    RT = Math.Round(SumQty, 0);
                    
                }
            }
            catch { RT = 0; }
            return RT;
            */

            decimal RT = 0;
            try
            {
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    RT = Convert.ToDecimal(db.g_getQty_BlankBoxFG(ItemNo, Datec));
                //}
            }
            catch { RT = 0; }
            return RT;

        }
        private decimal Returnx(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;
            try
            {
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    RT = Convert.ToDecimal(db.g_getQty_ReturnBox(ItemNo, Datec,"Cust"));
                //}
            }
            catch { RT = 0; }

            return RT;
        }
        private decimal ReturnxUni(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        RT = Convert.ToDecimal(db.g_getQty_ReturnBox(ItemNo, Datec,"Uni"));
            //    }
            //}
            //catch { RT = 0; }

            return RT;
        }
        private decimal Transfer(string DateTpics, string ItemNo, string Datec)
        {
            decimal RT = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //var ListPD = db.sp_44_Transfer_Qty(ItemNo, DateTpics).ToList();
                    //decimal SumQty = 0;

                    //foreach (var rd in ListPD)
                    //{
                    //    SumQty += Math.Ceiling((Convert.ToDecimal(rd.Qty) / Convert.ToDecimal(rd.LotSize)));
                    //}
                    //RT = Math.Round(SumQty, 0);
                }
            }
            catch { RT = 0; }
            return RT;
        }
        private decimal Customer(string DateTpics, string ItemNo, string Datec,decimal Qty,string Days)
        {
            decimal RT = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                  //  RT = Convert.ToDecimal(db.g_getQty_BlankBoxCust(ItemNo, Datec));
                }
            }
            catch { RT = 0; }
            return RT;
        }

        private decimal BalanceBox(string DateTpics, string ItemNo, string Datec, decimal Qty)
        {
            decimal RT = Qty;

            return RT;
        }
        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            ////FG_TAG
            ////Report.Reportx1.WReport = "PDTAG";
            ////Report.Reportx1.Value = new string[2];
            ////Report.Reportx1.Value[0] = "BomNo";
            ////Report.Reportx1.Value[1] = dbClss.UserID;
            ////Report.Reportx1 op = new Report.Reportx1("FG_TAG.rpt");
            ////op.Show();
            //PrintPDTAG pd = new PrintPDTAG("");
            //pd.Show();
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        var tp = db.TempPrints.Where(t => !t.CodeNo.Equals("")).ToList();
            //        if(tp!=null)
            //        {
            //            foreach(var rd in tp)
            //            {
            //                db.TempPrints.DeleteOnSubmit(rd);
            //                db.SubmitChanges();
            //            }

            //        }

            //        radGridView1.EndEdit();
            //        radGridView1.EndUpdate();
            //        int ck = 1;
            //        int Gp = 1;
            //        foreach(DataRow rd in dt3.Rows)
            //        {
                       

            //                if (ck == 8)
            //                {
            //                    ck = 1;
            //                    Gp += 1;
            //                }
            //                TempPrint tm = new TempPrint();
            //                tm.CodeNo = rd["Code"].ToString();
            //                tm.Name = rd["NAME"].ToString();
            //                tm.PLANTID = rd["PLANTID"].ToString();
            //                tm.SHELVES = rd["SHELVES"].ToString();
            //                tm.No = ck;
            //                tm.GP = Gp;
            //                db.TempPrints.InsertOnSubmit(tm);
            //                db.SubmitChanges();
            //                ck += 1;
                        
            //        }

            //        this.Cursor = Cursors.WaitCursor;
            //        try
            //        {
            //            Report.Reportx1.WReport = "TAGITEM";
            //            Report.Reportx1.Value = new string[1];
            //            Report.Reportx1.Value[0] = Gp.ToString();

            //            Report.Reportx1 op = new Report.Reportx1("TAGItem.rpt");
            //            op.Show();
            //        }
            //        catch { }
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch(Exception ex) { MessageBox.Show(ex.Message); }
            //this.Cursor = Cursors.Default;
        }

        private void radCheckBox1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radCheckBox1.Checked)
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    rd.Cells["chk"].Value = true;
                }

            }
            else
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    rd.Cells["chk"].Value = false;
                }
            }
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    radGridView1.EndEdit();
            //    radGridView1.EndUpdate();
            //    foreach (GridViewRowInfo rd in radGridView1.Rows)
            //    {
            //        if(Convert.ToBoolean(rd.Cells["chk"].Value))
            //        {
            //            DataRow nr = dt3.NewRow();
            //            nr["Code"] = rd.Cells["Code"].Value.ToString();
            //            nr["NAME"]= rd.Cells["NAME"].Value.ToString();
            //            nr["PLANTID"] = rd.Cells["PLANTID"].Value.ToString();
            //            nr["SHELVES"] = rd.Cells["SHELVES"].Value.ToString();

            //            dt3.Rows.Add(nr);

            //            rd.Cells["chk"].Value = false;

            //        }
            //    }
            //    lblCount.Text = "Count " + dt3.Rows.Count.ToString();
            //    this.Cursor = Cursors.Default;
            //}
            //catch { }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            dt3.Rows.Clear();
            lblCount.Text = "Count 0";
        }

        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            if(row>=0)
            {
                if(MessageBox.Show("คุณต้องการลบ BOX","ลบรายการ",MessageBoxButtons.YesNo,MessageBoxIcon .Question)==DialogResult.Yes)
                {

                }
            }
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการ อัพเดตจาก TPICS หรือไม่ ?", "อัพเดตรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var ListQ = db.sp_44_BOX_TPICSUpdate_Dynamics().ToList();
                        foreach (var rd in ListQ)
                        {
                            tb_BOX bx = db.tb_BOXes.Where(b => b.ItemNo.Equals(rd.CODE)).FirstOrDefault();
                            if (bx == null)
                            {
                                tb_BOX bn = new tb_BOX();
                                bn.Customer = "";
                                bn.Description = rd.NAME;
                                bn.ItemNo = rd.CODE;
                                bn.L = 0;
                                bn.H = 0;
                                bn.W = 0;
                                bn.QTY = 0;
                                bn.SNP = Convert.ToDecimal(rd.LOTS2);
                                bn.Shelf = rd.SHELVES;
                                bn.Picture = null;
                                bn.PackageFG = rd.NAME;
                                bn.PackageType = rd.NAME;
                                bn.Status = "Active";
                                db.tb_BOXes.InsertOnSubmit(bn);
                                db.SubmitChanges();

                            }
                            else
                            {
                                if (Convert.ToDecimal(rd.LOTS2) > 0)
                                {
                                    bx.SNP = Convert.ToDecimal(rd.LOTS2);
                                }
                                bx.Shelf = rd.SHELVES;
                                bx.Description = rd.NAME;
                                db.SubmitChanges();
                            }
                        }
                        MessageBox.Show("อัพเดตเสร็จสิ้น!!");
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (!e.RowElement.RowInfo.Cells["Status"].Value.ToString().Equals("Active"))
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.LightGray;
            }
            else
            {
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            }
        }

        private void radButtonElement7_Click(object sender, EventArgs e)
        {
            BOXReceive rx = new BOXReceive();
            rx.Show();
        }

        private void radButtonElement6_Click(object sender, EventArgs e)
        {
            BOXReceiveList brl = new BOXReceiveList();
            brl.Show();
        }

        private void radButtonElement8_Click(object sender, EventArgs e)
        {
            BOXStart bs = new BOXStart();
            bs.Show();
        }

        private void radButtonElement9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการปริ้น Barcode ?", "Barcode TAG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PrintTAG2();
            }
        }

        private void radButtonElement10_Click(object sender, EventArgs e)
        {
            BOXReceiveUni rx = new BOXReceiveUni();
            rx.Show();
        }

        private void radButtonElement11_Click(object sender, EventArgs e)
        {
            BOXMoveToUni mv = new BOXMoveToUni();
            mv.Show();
        }

        private void radButtonElement12_Click(object sender, EventArgs e)
        {
            BOXReceiveListUni bu = new BOXReceiveListUni();
            bu.Show();
        }

        private void radButtonElement13_Click(object sender, EventArgs e)
        {
            BOXReceiveListUniDumy bu = new BOXReceiveListUniDumy();
            bu.Show();
        }

        private void radButtonElement14_Click(object sender, EventArgs e)
        {

        }
    }
}
