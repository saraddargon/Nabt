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
namespace StockControl
{
    public partial class Receive : Telerik.WinControls.UI.RadRibbonForm
    {
        public Receive()
        {
            InitializeComponent();
        }

        public Receive(string RCNox)
        {
            InitializeComponent();
            RCNo = RCNox;
            txtReceiveNo.Text = RCNo;
        }
        string RCNo = "";
        //private int RowView = 50;
        //private int ColView = 10;
        DataTable dt = new DataTable();
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
            dt.Columns.Add(new DataColumn("RCNo", typeof(string)));
            dt.Columns.Add(new DataColumn("PONo", typeof(string)));
            dt.Columns.Add(new DataColumn("PONo", typeof(string)));
            dt.Columns.Add(new DataColumn("PONo", typeof(string)));

            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Active", typeof(bool)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateBy", typeof(string)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            //RMenu3.Click += RMenu3_Click;
            //RMenu4.Click += RMenu4_Click;
            //RMenu5.Click += RMenu5_Click;
            //RMenu6.Click += RMenu6_Click;
            dtReceive.Value = DateTime.Now;
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            // GETDTRow();  
            if (!RCNo.Equals(""))
            {
                txtReceiveNo.Text = RCNo;
                LoadTemp(RCNo);
                DataLoad(RCNo);
            }
            else
            {
                NewClick();
            }
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {

            //DeleteUnit();
           // DataLoad();
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

        private void DataLoad(string RRC)
        {

            int ck = 0;
        
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                tb_ReceiveLine rc = db.tb_ReceiveLines.Where(r => r.RCNo == txtReceiveNo.Text).FirstOrDefault();
                if (rc != null)
                {
                    StatusAC = "Waiting";
                }
                else
                {
                    StatusAC = "New";
                }

                var gpd= db.tb_ReceiveLineTemps.Where(r => r.RCNo == RRC && r.CreateBy.Equals(dbClss.UserID)).ToList();
                if (gpd.Count > 0)
                {
                    var tn = gpd.FirstOrDefault();
                    txtReceiveNo.Text = RRC;
                    txtInvoiceNo.Text = tn.InvoiceNo;
                    txtCreateBy.Text = tn.CreateBy;
                    txtCreateDate.Text =  Convert.ToDateTime(tn.CreateDate).ToString("dd/MM/yyyy");
                    txtScanPO.Text = "";
                   
                    radGridView1.AutoGenerateColumns = false;
                    radGridView1.DataSource = gpd;
                    if (!StatusAC.Equals("New"))
                    {
                        txtStatus.Text = tn.StatusTranfer;
                        if (!tn.StatusTranfer.Equals("Completed"))
                        {
                            txtScanPO.Enabled = true;
                        }
                        else
                        {
                            txtScanPO.Enabled = false;
                        }
                    }
                    else
                    {
                        txtStatus.Text = "New";
                        txtScanPO.Enabled = true;
                    }
                    foreach (var x in radGridView1.Rows)
                    {
                        x.Cells["No"].Value = ck + 1;
                        ck += 1;
                    }

                    if (txtStatus.Text.Equals("New"))
                    {
                        txtScanPO.Text = "";
                        txtScanPO.Enabled = true;
                        txtStatus.Focus();
                    }
                    else
                    {
                        txtScanPO.Text = "";
                        txtScanPO.Enabled = false;
                    }
                }
                else
                {
                    //txtReceiveNo.Text = "";
                    // txtInvoiceNo.Text = "";
                    txtScanPO.Text = "";

                }

                }
           


        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;


            return ck;
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad(txtReceiveNo.Text);
        }
        private void NewClick()
        {
            Clear();
            NewRecord();
        }
        string StatusAC = "New";
        private void Clear()
        {
            dtReceive.Value = DateTime.Now;
            StatusAC = "New";
            txtStatus.Text = "New";
            radGridView1.DataSource = null;
            txtScanPO.Text = "";
            txtReceiveNo.Text = "";
           // txtRemark.Text = "";
            txtStatus.Text = "";
            txtCreateBy.Text = dbClss.UserID;
            txtCreateDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtInvoiceNo.Text = "";
            radGridView1.DataSource = null;
        }
        private void NewRecord()
        {
            try
            {
               
                StatusAC = "New";
                txtStatus.Text = "New";
                txtReceiveNo.Text = dbClss.GetSeriesNo(3, 0);
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var rctemp = db.tb_ReceiveLineTemps.Where(t => t.RCNo == txtReceiveNo.Text).ToList();
                    if (rctemp != null)
                    {
                        foreach (var rd in rctemp)
                        {
                            db.tb_ReceiveLineTemps.DeleteOnSubmit(rd);
                            db.SubmitChanges();
                        }
                    }
                }
                txtInvoiceNo.Text = "";
                txtInvoiceNo.Focus();
            }
            catch { }

        }
        private void EditClick()
        {
            if(row>=0)
            {
                string Status = radGridView1.Rows[row].Cells["StatusTranfer"].Value.ToString();
                if (!Status.Equals("Completed"))
                {
                    string PO = radGridView1.Rows[row].Cells["PONo"].Value.ToString();
                    ReceiveCheck rc = new ReceiveCheck(txtReceiveNo.Text, PO, txtInvoiceNo.Text);
                    rc.ShowDialog();
                    DataLoad(txtReceiveNo.Text);
                }
                else
                {
                    MessageBox.Show("แก้ไขไม่ได้เนื่องจากรับเข้า TPICS เรียบร้อยแล้ว!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void ViewClick()
        {

            DataLoad(txtReceiveNo.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if(radGridView1.Rows.Count>0)
                    {
                        int Check = 0;
                        foreach(GridViewRowInfo rd in radGridView1.Rows)
                        {
                            if(rd.Cells["StatusTranfer"].Value.ToString().Equals("Completed"))
                            {
                                Check += 1;
                            }
                        }

                        if (Check == 0)
                        {
                            int CountAdd = 0;
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                //Add to Data
                                tb_ReceiveLine rc = db.tb_ReceiveLines.Where(r => r.RCNo == txtReceiveNo.Text).FirstOrDefault();
                                if (rc != null)
                                {
                                   
                                }
                                else
                                {
                                    txtReceiveNo.Text = dbClss.GetSeriesNo(3, 2);
                                }
                             
                                foreach (GridViewRowInfo rd in radGridView1.Rows)
                                {
                                    if (rd.Cells["StatusTranfer"].Value.ToString().Equals("Waiting"))
                                    {
                                        tb_ReceiveLine tr = db.tb_ReceiveLines.Where(r => r.RCNo == txtReceiveNo.Text
                                     && r.PONo == rd.Cells["PONo"].Value.ToString() && r.BarcodeText.Equals(rd.Cells["BarcodeText"].Value.ToString())).FirstOrDefault();
                                        if (tr != null)
                                        {
                                            //แก้ไข
                                            tr.Qty = Convert.ToDecimal(rd.Cells["Qty"].Value);
                                            tr.Remark = rd.Cells["Remark"].Value.ToString();
                                           // tr.BeforeRemain = Convert.ToDecimal(rd.Cells[""].Value);
                                            tr.CreateBy = dbClss.UserID;
                                            tr.CreateDate = dtReceive.Value;
                                            tr.InvoiceNo =txtInvoiceNo.Text ;// rd.Cells["InvoiceNo"].Value.ToString();
                                            tr.LocalLotNo = DateTime.Now.ToString("yyyyMMdd")+"T";
                                            tr.LotNo = rd.Cells["LotNo"].Value.ToString();
                                            tr.Status = rd.Cells["Status"].Value.ToString();
                                            db.SubmitChanges();
                                            CountAdd += 1;
                                        }
                                        else
                                        {
                                            //Use Store Procedure
                                            //เพิ่มใหม่//
                                            db.sp_009_InsertReceiveLine_Dynamics(txtReceiveNo.Text, rd.Cells["PONo"].Value.ToString(), rd.Cells["BarcodeText"].Value.ToString(), dtReceive.Value);
                                            CountAdd += 1;
                                        }
                                    }
                                }
                            }
                            if(CountAdd>0)
                            {
                                LoadTemp(txtReceiveNo.Text);
                                MessageBox.Show("บันทึกเรียบร้อย!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("มีการ Transfer เข้า TPICS แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                StatusAC = "Waiting";
                DataLoad(txtReceiveNo.Text);
                
               // txtStatus.Text = "Waiting";
            }
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyData == (Keys.Control | Keys.S))
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    //DataLoad();
                    Saveclick();
                }
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewClick();
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DeleteUnit(1, row);
                
            }
            catch { }
            this.Cursor = Cursors.Default;
            //DataLoad(txtReceiveNo.Text);

        }
        private void DeleteUnit(int AC,int Ros)
        {
            if (row >= 0)
            {
                if (MessageBox.Show("ต้องการลบ ข้อมูลหรือไม่?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (AC == 1)
                    {
                        string PONo = radGridView1.Rows[row].Cells["PONo"].Value.ToString();
                        string RCNo = radGridView1.Rows[row].Cells["RCNo"].Value.ToString();
                        //string Status = radGridView1.Rows[row].Cells["RCNo"].Value.ToString();
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_ReceiveLine rc = db.tb_ReceiveLines.Where(r => r.RCNo == RCNo && r.PONo == PONo && !r.StatusTranfer.Equals("Completed")).FirstOrDefault();
                            if(rc!=null)
                            {
                                db.tb_ReceiveLines.DeleteOnSubmit(rc);
                                db.SubmitChanges();
                                dbClss.AddHistory("Receive", "Delete", "ลบรายการ ของ " + txtReceiveNo.Text, PONo);
                            }
                            tb_ReceiveLineTemp rm = db.tb_ReceiveLineTemps.Where(r => r.RCNo == RCNo && r.PONo == PONo && !r.StatusTranfer.Equals("Completed")).FirstOrDefault();
                            if(rm!=null)
                            {
                                db.tb_ReceiveLineTemps.DeleteOnSubmit(rm);
                                db.SubmitChanges();
                            }
                            DataLoad(RCNo);
                        }
                    }
                    else if(AC==2)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            string PONo = "";// radGridView1.Rows[row].Cells["PONo"].Value.ToString();
                            string RCNo = "";// radGridView1.Rows[row].Cells["RCNo"].Value.ToString();
                            foreach (GridViewRowInfo rd in radGridView1.Rows)
                            {
                                PONo = rd.Cells["PONo"].Value.ToString();
                                RCNo = rd.Cells["RCNo"].Value.ToString();
                                tb_ReceiveLine rc = db.tb_ReceiveLines.Where(r => r.RCNo == RCNo && r.PONo == PONo && !r.StatusTranfer.Equals("Completed")).FirstOrDefault();
                                if (rc != null)
                                {
                                    db.tb_ReceiveLines.DeleteOnSubmit(rc);
                                    db.SubmitChanges();
                                }
                                tb_ReceiveLineTemp rm = db.tb_ReceiveLineTemps.Where(r => r.RCNo == RCNo && r.PONo == PONo && !r.StatusTranfer.Equals("Completed")).FirstOrDefault();
                                if (rm != null)
                                {
                                    db.tb_ReceiveLineTemps.DeleteOnSubmit(rm);
                                    db.SubmitChanges();
                                }
                            }
                        }
                        DataLoad(txtReceiveNo.Text);
                    }
                }
            }
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
            dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            if (op.ShowDialog() == DialogResult.OK)
            {


                using (TextFieldParser parser = new TextFieldParser(op.FileName))
                {
                    dt.Rows.Clear();
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int a = 0;
                    int c = 0;
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        a += 1;
                        DataRow rd = dt.NewRow();
                        // MessageBox.Show(a.ToString());
                        string[] fields = parser.ReadFields();
                        c = 0;
                        foreach (string field in fields)
                        {
                            c += 1;
                            //TODO: Process field
                            //MessageBox.Show(field);
                            if (a > 1)
                            {
                                if (c == 1)
                                    rd["UnitCode"] = Convert.ToString(field);
                                else if (c == 2)
                                    rd["UnitDetail"] = Convert.ToString(field);
                                else if (c == 3)
                                    rd["UnitActive"] = Convert.ToBoolean(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["UnitCode"] = "";
                                else if (c == 2)
                                    rd["UnitDetail"] = "";
                                else if (c == 3)
                                    rd["UnitActive"] = false;




                            }

                            //
                            //rd[""] = "";
                            //rd[""]
                        }
                        dt.Rows.Add(rd);

                    }
                }
                if (dt.Rows.Count > 0)
                {
                    dbClss.AddHistory(this.Name, "Import", "Import file CSV in to System", "");
                    ImportData();
                    MessageBox.Show("Import Completed.");

                    DataLoad(txtReceiveNo.Text);
                }

            }
        }

        private void ImportData()
        {

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

        private void radButton1_Click(object sender, EventArgs e)
        {
            //ReceiveCheck rc = new ReceiveCheck(txtReceiveNo.Text, txtScanPO.Text);
            //rc.ShowDialog();
        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtInvoiceNo.Text.Equals(""))
                {
                    txtScanPO.Enabled = true;
                    txtScanPO.Text = "";
                    txtScanPO.Focus();
                }
            }
        }

        private void txtReceiveNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (!txtInvoiceNo.Text.Equals(""))
                {
                    txtScanPO.Enabled = true;
                    txtScanPO.Text = "";
                    txtScanPO.Focus();
                }
                else
                {
                    txtInvoiceNo.Text = "";
                    txtInvoiceNo.Focus();
                }
                DataLoad(txtReceiveNo.Text);
            }
        }

        private void txtScanPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtStatus.Text.Equals("Completed"))
                {
                    if (!txtScanPO.Text.Equals("") && !txtReceiveNo.Text.Equals(""))
                    {
                        InsertReceive(txtScanPO.Text);
                        txtScanPO.Enabled = true;
                        txtScanPO.Text = "";
                        txtScanPO.Focus();

                    }
                }else
                {
                    MessageBox.Show("สถานะนี้ไม่สามารถ เพิ่มได้");
                }
            }
        }
        private void InsertReceive(string PO)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var gPO = db.sp_007_TPIC_SelectPO_Dynamics(PO).ToList();
                    if (gPO.Count > 0)
                    {
                        ReceiveCheck ckp = new ReceiveCheck(txtReceiveNo.Text, txtScanPO.Text,txtInvoiceNo.Text);
                        ckp.ShowDialog();
                        //LoadTemp();
                        DataLoad(txtReceiveNo.Text);
                    }
                    else
                    {
                        MessageBox.Show("หา PO ไม่พบ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTemp(string RC)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_008_InsertTempReceive_Dynamics(RC,dbClss.UserID);
                    
                }
            }
            catch { }
        }

        private void radGridView1_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                DeleteUnit(2, row);
                dbClss.AddHistory("Receive", "Delete", "ลบทั้งหมดของ " + txtReceiveNo.Text, "");

            }
            catch { }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการส่งข้อมูลเข้า TPICS ข้อมูลหรือไม่?", "ส่งข้อมูล TPICS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Threading.Thread.Sleep(2000);
                MessageBox.Show("Completed.");
            }
        }
    }
}
