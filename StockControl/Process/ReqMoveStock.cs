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
    public partial class ReqMoveStock : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReqMoveStock()
        {
            InitializeComponent();
        }
        public ReqMoveStock(string QCNo,string Part,string LotNox)
        {
            InitializeComponent();
            txtQCNo.Text = QCNo;
            txtItemCode.Text = Part;
            PartCode = Part;
            PartLot = LotNox;
           
        }
        string PartCode = "";
        string PartLot = "";
        public ReqMoveStock(string RCNox)
        {
            InitializeComponent();
            RCNo = RCNox;
            txtReceiveNo.Text = RCNo;
        }
        public ReqMoveStock(string RCNox,string PPX)
        {
            InitializeComponent();
            RCNo = RCNox;
            txtReceiveNo.Text = RCNo;
            PPc = PPX;
        }
        string RCNo = "";
        string PPc = "";
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
            //dt.Columns.Add(new DataColumn("RCNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("PONo", typeof(string)));
            //dt.Columns.Add(new DataColumn("PONo", typeof(string)));
            //dt.Columns.Add(new DataColumn("PONo", typeof(string)));

            dt.Columns.Add(new DataColumn("Seq", typeof(int)));
            dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));            
            dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("FromWH", typeof(string)));
            dt.Columns.Add(new DataColumn("ToWH", typeof(string)));
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("SS", typeof(int)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            //RMenu3.Click += RMenu3_Click;
            //RMenu4.Click += RMenu4_Click;
            //RMenu5.Click += RMenu5_Click;
            //RMenu6.Click += RMenu6_Click;
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
             GETDTRow();  
            if (!RCNo.Equals(""))
            {
                txtReceiveNo.Text = RCNo;
               // LoadTemp(RCNo);
                DataLoad(RCNo);
            }
            else
            {
                NewClick();
            }
            if(!PPc.Equals(""))
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_QCHD qh = db.tb_QCHDs.Where(q => q.QCNo.Equals(PPc)).FirstOrDefault();
                    if(qh!=null)
                    {
                        txtDept.Text = "QC";
                        cboMoveTo.Text = "NG01";

                        //LoadLotNo//
                        txtItemCode.Text = qh.PartNo;
                        LoadItem();
                        LoadLotNo();
                        cboLotNo2.Text = qh.LotNo.ToUpper();
                        txtQuantity.Text = Convert.ToDecimal(qh.NGQty).ToString("######");
                        txtRemark.Text = "Auto from QC No." + qh.QCNo;
                        txtReqBy.Text = dbClss.UserName;
                        AddItem();
                        txtOther.Text = "งาน NG "+qh.WONo;
                        rdoD4Other.IsChecked = true;
                    }
                }
            }

            if(!PartCode.Equals(""))
            {
                txtItemCode.Text = PartCode;
                LoadItem();
                LoadLotNo();
                cboLotNo2.Text = PartLot;
                txtRemark.Text = "Ref. from QC "+txtQCNo.Text;
                rdoD2QC.IsChecked = true;
                txtQuantity.Text = "1";
                txtDept.Text = "QC";
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
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView1.DataSource = null;
                    dt.Rows.Clear();
                    tb_RequisitionHD rh = db.tb_RequisitionHDs.Where(r => r.ReqNo == txtReceiveNo.Text).FirstOrDefault();
                    if(rh!=null)
                    {
                        if(rh.ReqType.Equals("A"))
                        {
                            rdo1.IsChecked = true;
                        }else
                        {
                            rdo2.IsChecked = true;
                        }

                        txtCreateBy.Text = rh.IssueBy;
                        txtCreateDate.Text = Convert.ToDateTime(rh.IssueDate).ToString("dd/MM/yyyy");
                        txtReqBy.Text = rh.ReqBy;
                        dtReqDate.Value = Convert.ToDateTime(rh.ReqDate);
                        txtStatus.Text = rh.Status;
                        txtOther.Text = rh.OtherRemark.ToString();
                        rdoD1NG.IsChecked = Convert.ToBoolean(rh.Remark1);
                        rdoD2QC.IsChecked = Convert.ToBoolean(rh.Remark2);
                        rdoD3Supplier.IsChecked = Convert.ToBoolean(rh.Remark3);
                        rdoD4Other.IsChecked = Convert.ToBoolean(rh.Remark4);
                        rdoD6Nam.IsChecked = Convert.ToBoolean(rh.Remark7);
                        
                        txtDept.Text = rh.Section.ToString();
                        txtRTime.Text = rh.RTime.ToString();

                        if(rh.Remark5.ToString().Equals("True"))
                        {
                            rdoD5Free.IsChecked = true;
                        }
                        if(rh.Remark6.ToString().Equals("True"))
                        {
                            rdoD5Short.IsChecked = true;
                        }


                        var listdt = db.tb_RequisitionDTs.Where(d => d.ReqNo.Equals(txtReceiveNo.Text) && !d.SS.Equals(0)).ToList();
                        if(listdt.Count>0)
                        {                            
                            radGridView1.DataSource = listdt;                            
                        }
                    }

                    int ck = 0;
                    //radGridView1.DataSource = dt;
                    foreach (var x in radGridView1.Rows)
                    {
                        x.Cells["dgvNo"].Value = ck + 1;
                        ck += 1;
                    }
                }
            }
            catch { }
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
            StatusAC = "New";
            txtStatus.Text = "New";
            radGridView1.DataSource = null;
           
            txtReceiveNo.Text = "";

            // txtRemark.Text = "";
            txtQCNo.Text = "";
            txtStatus.Text = "";
            txtCreateBy.Text = dbClss.UserID;
            txtCreateDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDept.Text = "";
            txtRefDoc.Text = "";
            radGridView1.DataSource = null;
            dt.Rows.Clear();
            txtOther.Text = "";
            rdo1.IsChecked = true;
            rdo2.IsChecked = false;
            rdoD1NG.IsChecked = false;
            rdoD2QC.IsChecked = false;
            rdoD3Supplier.IsChecked = false;
            rdoD4Other.IsChecked = false;
            rdoD5Free.IsChecked = false;
            rdoD5Short.IsChecked = false;
            rdoD6Nam.IsChecked = false;
            txtItemCode.Text = "";
            txtItemName.Text = "";
            txtAmount.Text = "";
            txtReqBy.Text = dbClss.UserID;
            dtReqDate.Value = DateTime.Now;
            

            cboLotNo2.DataSource = null;

            //cboMoveTo.Items.Clear();
            //cboMoveTo.Text = "";

            txtRemark.Text = "";
            txtQuantity.Text = "0";
            StatusAC = "New";
            txtStatus.Text = "New";
            txtReceiveNo.Text = dbClss.GetSeriesNo(5, 0);


        }
        private void NewRecord()
        {
            //try
            //{
               
            //    StatusAC = "New";
            //    txtStatus.Text = "New";
            //    txtReceiveNo.Text = dbClss.GetSeriesNo(3, 0);
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        var rctemp = db.tb_ReceiveLineTemps.Where(t => t.RCNo == txtReceiveNo.Text).ToList();
            //        if (rctemp != null)
            //        {
            //            foreach (var rd in rctemp)
            //            {
            //                db.tb_ReceiveLineTemps.DeleteOnSubmit(rd);
            //                db.SubmitChanges();
            //            }
            //        }
            //    }
            //    txtDept.Text = "";
            //    txtDept.Focus();
            //}
            //catch { }

        }
        private void EditClick()
        {
            //if(row>=0)
            //{
            //    string Status = radGridView1.Rows[row].Cells["StatusTranfer"].Value.ToString();
            //    if (!Status.Equals("Completed"))
            //    {
            //        string PO = radGridView1.Rows[row].Cells["PONo"].Value.ToString();
            //        ReceiveCheck rc = new ReceiveCheck(txtReceiveNo.Text, PO, txtDept.Text);
            //        rc.ShowDialog();
            //        DataLoad(txtReceiveNo.Text);
            //    }
            //    else
            //    {
            //        MessageBox.Show("แก้ไขไม่ได้เนื่องจากรับเข้า TPICS เรียบร้อยแล้ว!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

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
        private bool checkData()
        {
            bool ck = false;
            string err = "";
            if(txtDept.Text.Equals(""))
            {
                err += "โปรดระบุแผนก เพื่อ ออกเอกสาร\n";
            }

            if (rdoD1NG.IsChecked || rdoD2QC.IsChecked || rdoD3Supplier.IsChecked || rdoD4Other.IsChecked || rdoD5Free.IsChecked || rdoD5Short.IsChecked || rdoD6Nam.IsChecked)
            {
                if (rdoD4Other.IsChecked && txtOther.Text.Equals(""))
                {
                    err += "โปรดระบุ เหตุผลอื่นๆ \n";
                }
                
            }
            else
            {
                err += "โปรดเลือดรายละเอียดอื่นๆ \n";
            }



            if(txtRTime.Text.Trim().Equals(""))
            {
                txtRTime.Text = DateTime.Now.ToString("HH:mm");
              //  err += "โปรดใส่เวลา \n";
            }

            if (err.Equals(""))
            {
                ck = true;
            }
            else
            {
                ck = false;
                MessageBox.Show(err);
            }

            return ck;
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkData())
                {
                    try
                    {
                        txtRTime.Text = DateTime.Now.ToString("HH:mm");
                        radGridView1.EndEdit();
                        if (radGridView1.Rows.Count > 0)
                        {
                            int Check = 0;
                            foreach (GridViewRowInfo rd in radGridView1.Rows)
                            {
                                if (rd.Cells["SS"].Value.ToString().Equals(3))
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

                                    if(txtStatus.Text.Equals("New"))
                                    {
                                        txtReceiveNo.Text = dbClss.GetSeriesNo(5, 0);
                                    }

                                  
                                    tb_RequisitionHD rc = db.tb_RequisitionHDs.Where(r => r.ReqNo == txtReceiveNo.Text).FirstOrDefault();
                                    if (rc != null)
                                    {
                                        rc.Section = txtDept.Text;                                        
                                        db.SubmitChanges();
                                    }
                                    else
                                    {

                                        txtReceiveNo.Text = dbClss.GetSeriesNo(5, 2);
                                        byte[] barcode = dbClss.SaveQRCode2D(txtReceiveNo.Text);
                                        tb_RequisitionHD rh = new tb_RequisitionHD();
                                        rh.IssueBy = dbClss.UserID;
                                        rh.IssueDate = DateTime.Now;
                                        rh.ApproveBy = "";
                                        rh.ApproveDate = null;
                                        rh.OtherRemark = txtOther.Text;
                                        rh.ReceiveBy = "";
                                        rh.ReceiveDate = null;
                                        rh.Remark1 = rdoD1NG.IsChecked;
                                        rh.Remark2 = rdoD2QC.IsChecked;
                                        rh.Remark3 = rdoD3Supplier.IsChecked;
                                        rh.Remark4 = rdoD4Other.IsChecked;
                                        rh.Remark5 = rdoD5Free.IsChecked;
                                        rh.Remark6 = rdoD5Short.IsChecked;
                                        rh.Remark7 = rdoD6Nam.IsChecked;
                                        rh.ReqBy = txtReqBy.Text;
                                        rh.ReqDate = dtReqDate.Value;
                                        rh.Status = "Waiting";
                                        rh.ReqNo = txtReceiveNo.Text;
                                        rh.RTime = txtRTime.Text;
                                        rh.QRCode = barcode;
                                        rh.QCNo = txtQCNo.Text;

                                        if (rdo1.IsChecked)
                                            rh.ReqType = "A";
                                        else
                                            rh.ReqType = "B";
                                        rh.Section = txtDept.Text;
                                      

                                        db.tb_RequisitionHDs.InsertOnSubmit(rh);
                                        db.SubmitChanges();

                                    }

                                   // radGridView1.EndEdit();
                                    int id = 0;
                                    int Row1 = 0;
                                    decimal qty = 0;
                                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                                    {
                                        if (rd.Cells["SS"].Value.ToString().Equals("1"))
                                        {
                                            int.TryParse(Convert.ToString(rd.Cells["id"].Value), out id);
                                            tb_RequisitionDT tr = db.tb_RequisitionDTs.Where(r => r.id == id).FirstOrDefault();
                                            if (tr != null)
                                            {
                                                //แก้ไข                                        
                                            }
                                            else
                                            {
                                                //Use Store Procedure
                                                //เพิ่มใหม่//     
                                                qty = 0;   
                                                decimal.TryParse(Convert.ToString(rd.Cells["dgvQty"].Value), out qty);
                                               
                                                if (qty > 0)
                                                {
                                                    Row1 += 1;
                                                    tb_RequisitionDT dtr = new tb_RequisitionDT();
                                                    dtr.SS = 1;
                                                    dtr.Seq = Row1;
                                                    dtr.ReqNo = txtReceiveNo.Text;
                                                    dtr.Remark = Convert.ToString(rd.Cells["dgvRemark"].Value);
                                                    dtr.Qty = qty;
                                                    dtr.LotNo = Convert.ToString(rd.Cells["dgvLotNo"].Value);
                                                    dtr.ItemName = Convert.ToString(rd.Cells["dgvItemName"].Value);
                                                    dtr.CodeNo = Convert.ToString(rd.Cells["dgvItemNo"].Value);
                                                    dtr.FromWH = Convert.ToString(rd.Cells["FromWH"].Value);
                                                    dtr.ToWH = Convert.ToString(rd.Cells["ToWH"].Value);
                                                    dtr.RefDocument= Convert.ToString(rd.Cells["RefDocument"].Value);
                                                    dtr.Amount = Convert.ToDecimal(rd.Cells["Amount"].Value);
                                                    dtr.QCNo = txtQCNo.Text;
                                                    db.tb_RequisitionDTs.InsertOnSubmit(dtr);

                                                    db.SubmitChanges();
                                                    CountAdd += 1;
                                                }


                                            }
                                        }
                                    }
                                }
                                if (CountAdd > 0)
                                {
                                   // LoadTemp(txtReceiveNo.Text);
                                    MessageBox.Show("บันทึกเรียบร้อย!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("มีการ Transfer เข้า TPICS แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        StatusAC = "Waiting";
                        DataLoad(txtReceiveNo.Text);

                        txtStatus.Text = "Waiting";
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                   
                }
                
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
                    if(AC==1)
                    {
                        int countA = 0;
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            string PONo = "";// radGridView1.Rows[row].Cells["PONo"].Value.ToString();
                            string RCNo = "";// radGridView1.Rows[row].Cells["RCNo"].Value.ToString();
                            int SS = 0;
                            foreach (GridViewRowInfo rd in radGridView1.Rows)
                            {
                                if (Convert.ToInt32(rd.Cells["SS"].Value).Equals(3))
                                {
                                    SS += Convert.ToInt32(rd.Cells["SS"].Value);
                                }
                                
                        
                            }
                            if(SS.Equals(0))
                            {
                                tb_RequisitionHD dh = db.tb_RequisitionHDs.Where(h => h.ReqNo.Equals(txtReceiveNo.Text) && h.Status.Equals("Waiting")).FirstOrDefault();
                                if(dh!=null)
                                {
                                    dh.Status = "Deleted";
                                    db.SubmitChanges();
                                    var listDL = db.tb_RequisitionDTs.Where(d => d.ReqNo.Equals(dh.ReqNo)).ToList();
                                    foreach(var rl in listDL)
                                    {
                                                                   
                                        tb_RequisitionDT dd = db.tb_RequisitionDTs.Where(s => s.id == rl.id).FirstOrDefault();
                                        if (dd != null)
                                        {
                                            countA += 1;
                                            dd.SS = 0;
                                            db.SubmitChanges();
                                        }
                                    }

                                }
                                if(countA>0)
                                {
                                    MessageBox.Show("Deleted Completed.");
                                }
                            }

                            db.sp_41_MoveStockSelecItem05_UpdateStatus(txtReceiveNo.Text);
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
            //if (e.KeyChar == 13)
            //{
            //    if (!txtDept.Text.Equals(""))
            //    {
                   
            //    }
            //}
        }

        private void txtReceiveNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtReceiveNo.Text.Equals(""))
                {

                    DataLoad(txtReceiveNo.Text);
                }
            }
        }

        private void txtScanPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    if (!txtStatus.Text.Equals("Completed"))
            //    {
                   
            //    }else
            //    {
            //        MessageBox.Show("สถานะนี้ไม่สามารถ เพิ่มได้");
            //    }
            //}
        }
        private void InsertReceive(string PO)
        {
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        var gPO = db.sp_007_TPIC_SelectPO(PO).ToList();
            //        if (gPO.Count > 0)
            //        {
            //            //ReceiveCheck ckp = new ReceiveCheck(txtReceiveNo.Text, txtScanPO.Text,txtInvoiceNo.Text);
            //            //ckp.ShowDialog();
            //            //LoadTemp();
            //            DataLoad(txtReceiveNo.Text);
            //        }
            //        else
            //        {
            //            MessageBox.Show("หา PO ไม่พบ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
            /*
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                DeleteUnit(2, row);
                dbClss.AddHistory("Receive", "Delete", "ลบทั้งหมดของ " + txtReceiveNo.Text, "");

            }
            catch { }
            this.Cursor = Cursors.Default;
            */
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show("ต้องการส่งข้อมูลเข้า TPICS ข้อมูลหรือไม่?", "ส่งข้อมูล TPICS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Threading.Thread.Sleep(2000);
                MessageBox.Show("Completed.");
            }
            */

            Report.Reportx1.Value = new string[2];
            Report.Reportx1.Value[0] = txtReceiveNo.Text.ToString();
            //Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "MoveStock";
            Report.Reportx1 op = new Report.Reportx1("MoveStockR1.rpt");
            op.Show();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            dbClss.CheckDigitDecimal(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (rdoD1NG.IsChecked || rdoD2QC.IsChecked || rdoD3Supplier.IsChecked || rdoD4Other.IsChecked || rdoD5Free.IsChecked || rdoD5Short.IsChecked || rdoD6Nam.IsChecked)
            {
                AddItem();

            }
            else
            {
                MessageBox.Show("ต้องติ๊กเลือก รายละเอียดอื่นๆ ก่อน!!!");
            }
        }

        private void AddItem()
        {
            try
            {
                bool check = false;
                decimal Qty1 = 0;
                string err = "";
                string RefDocument = "";
                string FWH = "WH01";
                string TWH = "NG01";

                decimal CostA = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {


                    if (txtItemCode.Text.Equals(""))
                    {
                        err += "Item No. Empty! \n";
                    }
                    else
                    {
                        txtItemName.Text = db.getItemNoTPICS_Dynamics(txtItemCode.Text.Trim());
                        if (txtItemName.Text.Equals(""))
                        {
                            err += "Item No. Invalid!!\n";
                        }
                    }

                    decimal.TryParse(txtQuantity.Text, out Qty1);
                    if (Qty1 <= 0)
                    {
                        err += "Qty is Empty! \n";
                    }

                    if(rdoD4Other.IsChecked && rdo2.IsChecked && (txtAmount.Text.Equals("") || txtAmount.Text.Equals("0")))
                    {
                        err += "Cost Amount is Empty! \n";
                    }
                    if (rdoD5Short.IsChecked && rdo2.IsChecked && (txtAmount.Text.Equals("") || txtAmount.Text.Equals("0")))
                    {
                        err += "Cost Amount is Empty! \n";
                    }
                    if (rdoD6Nam.IsChecked && rdo2.IsChecked && (txtAmount.Text.Equals("") || txtAmount.Text.Equals("0")))
                    {
                        err += "Cost Amount is Empty! \n";
                    }
                    if (rdoD5Free.IsChecked && rdo1.IsChecked)
                    {
                        err += "Free of Charge ส่งคืนได้เท่านั้น !";
                    }

                    if (rdo1.IsChecked)
                    {
                        FWH = "WH01";
                        if (rdoD1NG.IsChecked)
                        {
                            TWH = "Adjust";
                        }
                        else if (rdoD2QC.IsChecked)
                        {
                            TWH = "NG01";
                        }
                        else if (rdoD3Supplier.IsChecked)
                        {
                            TWH = "NG01";
                        }
                        else if (rdoD4Other.IsChecked)
                        {
                            TWH = "Adjust";
                        }
                        else if (rdoD5Free.IsChecked)
                        {
                            TWH = "Adjust";
                        }
                        else if (rdoD5Short.IsChecked)
                        {
                            TWH = "Adjust";
                        }
                        else if (rdoD6Nam.IsChecked)
                        {
                            TWH = "Adjust";
                        }

                    }
                    else
                    {
                        TWH = "WH01";
                        if (rdoD1NG.IsChecked)
                        {
                            FWH = "Adjust";
                        }
                        else if (rdoD2QC.IsChecked)
                        {
                            FWH = "NG01";
                        }
                        else if (rdoD3Supplier.IsChecked)
                        {
                            FWH = "NG01";
                        }
                        else if (rdoD4Other.IsChecked)
                        {
                            FWH = "Adjust";
                        }
                        else if (rdoD5Free.IsChecked)
                        {
                            FWH = "Adjust";
                        }
                        else if (rdoD5Short.IsChecked)
                        {
                            FWH = "Adjust";
                        }
                        else if (rdoD6Nam.IsChecked)
                        {
                            FWH = "Adjust";
                        }

                    }

                    decimal Am = 0;
                    decimal.TryParse(txtAmount.Text, out Am);
                    txtAmount.Text = Am.ToString();

                    if (cboLotNo2.Text.Equals("") && err.Equals(""))
                    {

                        //Check Control Lot//
                        bool cl = Convert.ToBoolean(db.getControlLot_Dynamics(txtItemCode.Text));
                        if (cl)
                        {
                            err += "Lot No. Empty! \n";

                            //Find // Ref Document No.
                            //มี ควบคุม Lot แสดงว่าตอนย้ายจะต้องมี Lot ในการย้าย
                            decimal Qty43 = 0;
                            decimal.TryParse(txtQuantity.Text, out Qty43);
                            string WH43 = "";
                            if (rdo1.IsChecked)
                            {
                                WH43 = "WH01";
                            }
                            else
                            {
                                WH43 = TWH;
                            }

                            bool ChKb = true;
                            if(rdo2.IsChecked && TWH.Equals("Adjust"))
                            {
                                ChKb = false;
                            }

                            if (ChKb)
                            {

                                var cck = db.sp_42_CheckLotLocationAndQty_Dynamics(txtItemCode.Text, Qty43, cboLotNo2.Text, WH43).First();
                                if (cck.Checkdata.Equals(0))
                                {
                                    err += "Quantity in Lot No. Not enough! \n";
                                }
                            }


                        }
                        else
                        {

                        }
                    }


                    if (txtRemark.Text.Equals(""))
                    {
                        err += "Remark Is Empty! \n";
                    }                   

                    if (!FWH.Equals("Adjust"))
                    {
                        if (Convert.ToInt32(db.MoveStock_CheckLot_Dynamics(txtItemCode.Text, FWH, cboLotNo2.Text, Qty1)).Equals(1))
                        {
                            err += "LotName and Qty Not enough! \n";
                        }


                        if (Convert.ToInt32(db.MoveStock_CheckQty_Dynamics(txtItemCode.Text, FWH, cboLotNo2.Text, Qty1)).Equals(1))
                        {
                            err += "Qty Not enough! \n";
                        }

                    }


                    if (TWH.Equals("NG01"))
                    {


                        if (rdo2.IsChecked)
                        {
                            if (txtRefDoc.Text.Trim().Equals(""))
                            {
                                err += "Ref. Document Empty!\n";
                            }
                            //Check Return Qty//
                            if (Convert.ToInt32(db.MoveStock_CheckQtyRef_Dynamics(txtItemCode.Text, FWH, cboLotNo2.Text, txtRefDoc.Text.Trim(), Qty1)).Equals(1))
                            {
                                err += "Qty Return in Ref. Document Invalid!!\n";
                            }
                        }
                    }


                }

                if (err.Equals(""))
                {
                    if (rdo1.IsChecked)
                    {
                        AddItem(txtItemCode.Text, txtItemName.Text, Qty1, cboLotNo2.Text, FWH, TWH, txtRemark.Text, txtRefDoc.Text);
                    }
                    else
                    {
                        AddItem(txtItemCode.Text, txtItemName.Text, Qty1, cboLotNo2.Text, FWH, TWH, txtRemark.Text, txtRefDoc.Text);
                    }
                    ClearListAdd();
                }
                else
                {
                    MessageBox.Show(err, "error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void ClearListAdd()
        {
            txtItemCode.Text = "";
          //  cboMoveTo.Items.Clear();
           
            cboLotNo2.DataSource = null;
            txtRemark.Text = "";
            txtQuantity.Text = "0";
            txtRefDoc.Text = "";
            cboLotNo2.Text = "";
            txtItemCode.Focus();
        }

        private void txtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                LoadItem();
            }
        }
        private void LoadItem()
        {
            lblItemName.Text = "";
            InpurtItem(txtItemCode.Text.Trim());
            cboLotNo2.Focus();
        }
        private void InpurtItem(string ItemCode)
        {
            try
            {//Call Item//
             // DataLoad();
             //cboMoveTo.Items.Clear();
                cboLotNo2.DataSource = null;
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    if (!txtItemCode.Text.Equals(""))
                //    {
                //        var listMove = db.sp_41_MoveStockSelecItem01(txtItemCode.Text.Trim()).ToList();
                //        foreach (var rx in listMove)
                //        {
                //            if (!rx.HOKAN.Equals("WH01"))
                //            {
                //                cboMoveTo.Items.Add(rx.HOKAN);

                //            }
                //        }


                //    }
                //}

                cboLotNo2.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    lblItemName.Text=  db.getItemNoTPICS_Dynamics(ItemCode);
                    if (!txtItemCode.Text.Equals(""))
                    {
                        string LC = "WH01";
                        if (rdo1.IsChecked)
                            LC = "WH01";
                        else
                            LC = cboMoveTo.Text;

                        var listMove = db.sp_41_MoveStockSelecItem02_Lot_Dynamics(txtItemCode.Text.Trim(), LC).ToList();
                        cboLotNo2.DataSource = listMove;
                        cboLotNo2.DisplayMember = "LOTNAME";
                        cboLotNo2.ValueMember = "LOTNAME";
                        if (listMove.Count > 0)
                            cboLotNo2.SelectedIndex = 0;



                    }



                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AddItem(string ItemNo,string ItemName,decimal Qty,string LotNo,string MoveFrom,string MoveTo,string Remark,string RefDocument)
        {
            try
            {
                
                int rowindex = -1;
                GridViewRowInfo ee;
                decimal AM = 0;
                decimal.TryParse(txtAmount.Text, out AM);
                int rowsss = radGridView1.Rows.Count;
                if (rowindex == -1)
                {
                    ee = radGridView1.Rows.AddNew();
                }
                else
                    ee = radGridView1.Rows[rowindex];

                ee.Cells["dgvNo"].Value = rowsss + 1;
                ee.Cells["dgvItemNo"].Value = ItemNo;
                ee.Cells["dgvItemName"].Value = ItemName;
                ee.Cells["dgvQty"].Value = Qty;
                ee.Cells["dgvLotNo"].Value = LotNo;
                ee.Cells["dgvRemark"].Value = Remark;
                ee.Cells["id"].Value = 0;
                ee.Cells["FromWH"].Value = MoveFrom;
                ee.Cells["ToWH"].Value = MoveTo;
                ee.Cells["SS"].Value = 1;
                ee.Cells["RefDocument"].Value = RefDocument;
                ee.Cells["Amount"].Value = AM;



            }
            catch { }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            try
            {
                //Cal Item//
                //InpurtItem(txtItemCode.Text);
            }
            catch { }
        }

        private void rdo2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            lblMove.Text = "Move From";
        }

        private void rdo1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            lblMove.Text = "Move To";
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            LoadLotNo();
        }
        private void LoadLotNo()
        {
            try
            {
                //Call Item//
                // DataLoad();

                cboLotNo2.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (!txtItemCode.Text.Equals(""))
                    {
                        string LC = "WH01";
                        if (rdo1.IsChecked)
                            LC = "WH01";
                        else
                            LC = cboMoveTo.Text;

                        var listMove = db.sp_41_MoveStockSelecItem02_Lot_Dynamics(txtItemCode.Text.Trim(), LC).ToList();
                        cboLotNo2.DataSource = listMove;
                        cboLotNo2.DisplayMember = "LOTNAME";
                        cboLotNo2.ValueMember = "LOTNAME";
                        if (listMove.Count > 0)
                            cboLotNo2.SelectedIndex = 0;

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radGridView1_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {

        }

        private void radGridView1_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
           //if(MessageBox.Show("ต้องการลบรายการ ?","ลยรายการ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
           // {
                
           //    // MessageBox.Show(radGridView1.Rows[e.Rows].Cells["dgvNo"].Value.ToString());
           // }
        }

        private void ลบ1รายการToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการลบรายการ ?", "ลยรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = 0;
                    if (row >= 0)
                    {
                        GridViewRowInfo ee = radGridView1.Rows[row];
                        int.TryParse(ee.Cells["id"].Value.ToString(), out id);
                        if (id > 0)
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                tb_RequisitionDT rdt = db.tb_RequisitionDTs.Where(r => r.id == id && r.SS == 1).FirstOrDefault();
                                if (rdt != null)
                                {
                                    db.tb_RequisitionDTs.DeleteOnSubmit(rdt);
                                    db.SubmitChanges();
                                    radGridView1.Rows.Remove(ee);
                                    MessageBox.Show("ลบรายการจากระบบ เรียบร้อยแล้ว!");
                                    

                                }
                                else
                                {
                                    MessageBox.Show("ไม่สามารถ ลบรายการจากระบบได้! (สถานะไม่ถูกต้อง)");
                                }

                                

                            }
                        }
                        else
                        {
                            radGridView1.Rows.Remove(ee);
                        }
                    }
                }catch { }
                 
            }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            tb_Link.Text = "";
            MoveStockList mv = new MoveStockList(tb_Link);
            mv.ShowDialog();

            if(!tb_Link.Text.Equals(""))
            {
                txtReceiveNo.Text = tb_Link.Text;
                DataLoad(txtReceiveNo.Text);
            }
            
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            dbClss.CheckDigitDecimal(e);
        }
    }
}
