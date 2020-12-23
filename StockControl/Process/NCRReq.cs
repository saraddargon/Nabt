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
    public partial class NCRReq : Telerik.WinControls.UI.RadRibbonForm
    {
        public NCRReq()
        {
            InitializeComponent();
            this.Text = "NCR Req.";
        }
        public NCRReq(string QCNo,string Part,string LotNox)
        {
            InitializeComponent();
            txtQCNo.Text = QCNo;           
            PartCode = Part;
            PartLot = LotNox;
           
        }
        string PartCode = "";
        string PartLot = "";
        public NCRReq(string RCNox)
        {
            InitializeComponent();
            RCNo = RCNox;
            txtNCRNo.Text = RCNo;
        }
        public NCRReq(string RCNox,string PPX)
        {
            InitializeComponent();
            RCNo = RCNox;
            txtNCRNo.Text = RCNo;
            PPc = PPX;
        }
        string RCNo = "";
        string PPc = "";
        //private int RowView = 50;
        //private int ColView = 10;
        DataTable dt = new DataTable();
        string PathNCR = "";
        string PathNCRATT = "";
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
            dt.Columns.Add(new DataColumn("Qty", typeof(int)));
            dt.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("FromWH", typeof(string)));
            dt.Columns.Add(new DataColumn("ToWH", typeof(string)));
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("SS", typeof(int)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var ph = db.tb_Paths.Where(p => p.PathCode.Equals("NCR") || p.PathCode.Equals("NCRATT")).ToList();
                if (ph.Count > 0)
                {
                    foreach (var rd in ph)
                    {
                        if (rd.PathCode.Equals("NCR"))
                        {
                            PathNCR = rd.PathFile;
                        }
                        if (rd.PathCode.Equals("NCRATT"))
                        {
                            PathNCRATT = rd.PathFile;
                        }
                    }
                }

            }
            dtNCRDate.Value = DateTime.Now;

             GETDTRow();  
            if (!RCNo.Equals(""))
            {
                txtNCRNo.Text = RCNo;
               // LoadTemp(RCNo);
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
        private void SetValueTypeof(string TypeValue)
        {
            if (TypeValue.Equals(rdoP1Receive.Text))
            {
                rdoP1Receive.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP1NGByAssy.Text))
            {
                rdoP1NGByAssy.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP1NGFoundinLine.Text))
            {
                rdoP1NGFoundinLine.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP1FinishGoods.Text))
            {
                rdoP1FinishGoods.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP1Other.Text))
            {
                rdoP1Other.IsChecked = true;
            }


        }
        private void SetValueRootCause(string TypeValue)
        {
            if (TypeValue.Equals(rdoP4Man.Text))
            {
                rdoP4Man.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP4Material.Text))
            {
                rdoP4Material.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP4Method.Text))
            {
                rdoP4Method.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP4Machine.Text))
            {
                rdoP4Machine.IsChecked = true;
            }
            else if (TypeValue.Equals(rdoP4TherOther.Text))
            {
                rdoP4TherOther.IsChecked = true;
            }


        }
        private void DataLoad(string RRC)
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int ck = 0;
                    tb_QCNCR qn = db.tb_QCNCRs.Where(w => w.NCRNo.Equals(txtNCRNo.Text)).FirstOrDefault();
                    if (qn != null)
                    {
                        if (Convert.ToInt32(qn.SS).Equals(1))
                        {
                            txtStatus.ForeColor = Color.Red;
                            txtStatus.Text = "Waiting";
                            StatusAC = "Waiting";
                            btnSave.Enabled = true;
                            btnDelete.Enabled = true;
                            btnView.Enabled = true;
                        }
                        else if (Convert.ToInt32(qn.SS).Equals(2))
                        {
                            txtStatus.ForeColor = Color.Orange;
                            txtStatus.Text = "Process";
                            StatusAC = "Process";
                            btnSave.Enabled = false;
                            btnDelete.Enabled = false;
                            btnView.Enabled = true;
                        }
                        else if (Convert.ToInt32(qn.SS).Equals(7))
                        {
                            txtStatus.ForeColor = Color.Green;
                            txtStatus.Text = "Completed";
                            StatusAC = "Completed";
                            btnSave.Enabled = false;
                            btnDelete.Enabled = false;
                            btnView.Enabled = false;
                        }

                        ////Clear///
                        txtQCNo.Text = qn.QCNo;
                        txtWONo.Text = qn.WONo;
                        txtPartNo.Text = qn.PartNo;
                        txtPartName.Text = qn.PartName;
                        
                        txtLotNo.Text = qn.LotNo;
                        txtLotSize.Text = Convert.ToDecimal(qn.LotSize).ToString("###,###");
                        rdo1.IsChecked = Convert.ToBoolean(qn.IssueDebitNote);
                        rdo2.IsChecked = !Convert.ToBoolean(qn.IssueDebitNote);
                        txtCreateBy.Text = qn.CreateBy;
                        txtCreateDate.Text = Convert.ToDateTime(qn.CreateDate).ToString("dd/MM/yyyy");

                        txtReciveDate.Text = qn.ReceiveDate;
                        txtDept.Text = qn.Dept;
                        txtCustomer.Text = qn.Customer;
                        txtSupplier.Text = qn.Supplier;
                        dtNCRDate.Value = Convert.ToDateTime(qn.NCRDate);
                        txtNGPPM.Text = qn.NGPPM;
                        txtNGQty.Text = Convert.ToDecimal(qn.NGQty).ToString("###,###");
                        txtQuantity.Text = Convert.ToDecimal(qn.Qty).ToString("###,###");
                        txtRank.Text = qn.Rank;
                        txtSampling.Text = qn.Sampling;
                        if (qn.Occurrence.Equals("First Occurrence"))
                            rdoFirstOccurrence.IsChecked = true;
                        else
                            rdoRecurrence.IsChecked = true;
                        //Typeof//                        
                        SetValueTypeof(qn.TypeofProblem);
                        txtOther.Text = qn.TypeofRemark;
                        try
                        {
                            txtLinkPath.Text = "";
                            txtLinkPath2.Text = "";
                            txtFileName.Text = qn.ImageFile;
                            txtFileName2.Text = qn.ImageFile2;

                            pictureBox1.Image = System.Drawing.Image.FromFile(PathNCR + "" + qn.ImageFile.ToString());

                        }
                        catch { }


                        ////P2
                        txtP2Remark1.Text = qn.P2DefectReport1;
                        txtP2Remark2.Text = qn.P2DefectReport2;
                        txtP2Check.Text = qn.P2Checked;
                        txtP2Approve.Text = qn.P2Approve;
                        txtP2Detector.Text = qn.P2Detector;
                        txtP2Position.Text = qn.P2Position;
                        dtP2Date.Value = Convert.ToDateTime(qn.P2DetectorDate);
                        dtP2Checked.Value = Convert.ToDateTime(qn.P2CheckDate);
                        dtP2Approve.Value = Convert.ToDateTime(qn.P2ApproveDate);

                        ////P3
                        txtP3Approve.Text = qn.P3ApproveBy;
                        txtP3Incharge.Text = qn.P3InchargeBy;
                        txtP3LaborCost.Text = Convert.ToDecimal(qn.P3LaborCost).ToString("###,###,##0.00");
                        txtP3OtherCost.Text = Convert.ToDecimal(qn.P3Cost).ToString("###,###,##0.00");
                        txtP3Qtyofsort.Text = Convert.ToDecimal(qn.P3QtyofSort).ToString("###,###,##0");
                        txtP3Return.Text = qn.P3ReturnRemark;
                        txtP3Rework.Text = qn.P3ReworkRemark;
                        txtP3Scrap.Text = qn.P3ScrapRemark;
                        txtP3Sorting.Text = qn.P3SortingRemark;
                        txtP3Sorttime.Text = Convert.ToInt32(qn.P3SortTime).ToString();
                        txtP3Temporary.Text = qn.P3TemporaryAction;
                        txtP3TotalCharge.Text = Convert.ToDecimal(qn.P3TotalCost).ToString("###,###,##0.00");
                        rdoP3Scarp.IsChecked = Convert.ToBoolean(qn.P3Scrap);
                        rdoP3Return.IsChecked = Convert.ToBoolean(qn.P3Return);
                        rdoRework.IsChecked = Convert.ToBoolean(qn.P3Rework);
                        rdoSorting.IsChecked = Convert.ToBoolean(qn.P3Sorting);
                        dtP3Approve.Value = Convert.ToDateTime(qn.P3ApproveDate);
                        dtP3Incharge.Value = Convert.ToDateTime(qn.P3InchargeDate);
                        dtP3DueDate.Value = Convert.ToDateTime(qn.P3Duedate);

                        ////P4
                        SetValueRootCause(qn.P4RootCause);
                        txtP4CausingSection.Text = qn.CausingSection;
                        txtP4Remark.Text = qn.P4Remark;


                        ////P5
                        txtP5Incharge.Text = qn.P5Incharge;
                        dtP5Incharge.Value = Convert.ToDateTime(qn.P5DueDate);
                        txtP5Remark.Text = qn.P5Remark;

                        ////P6
                        txtP6Approved.Text = qn.P6Approveby;
                        txtP6Carpar.Text = qn.P6IssueCarNo;
                        dtP6CloseDate.Value = Convert.ToDateTime(qn.P6CodeDate);
                        rdoP6Accept.IsChecked = Convert.ToBoolean(qn.P6Accept);
                        rdoP6NotAccept.IsChecked= !Convert.ToBoolean(qn.P6Accept);
                        chkP6IssueCARPAR.Checked = Convert.ToBoolean(qn.P6IssueCar);
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถค้าหาได้!!");
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;


            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad(txtNCRNo.Text);
        }
        private void NewClick()
        {
            Clear();
            NewRecord();
        }
        string StatusAC = "New";
        private void Clear()
        {
            pictureBox1.Image = null;
            txtLinkPath.Text = "";
            txtLinkPath2.Text = "";
            txtFileName.Text = "";
            txtFileName2.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            btnView.Enabled = true;
            txtNCRNo.Text = dbClss.GetSeriesNo(77, 0);
            txtStatus.Text = "New";
            StatusAC = "New";
            ////Clear///
            txtReciveDate.Text = "";
            txtQCNo.Text = "";
            txtWONo.Text = "";
            txtPartNo.Text = "";
            txtPartName.Text = "";
            txtLinkPath.Text = "";
            txtLotNo.Text = "";
            txtLotSize.Text = "";
            rdo1.IsChecked = true;
            rdo2.IsChecked = false;
            txtCreateBy.Text = dbClss.UserID;
            txtCreateDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            txtDept.Text = "";
            txtCustomer.Text = "";
            txtSupplier.Text = "";
            dtNCRDate.Value = DateTime.Now;
            txtNGPPM.Text = "";
            txtNGQty.Text = "";
            txtQuantity.Text = "";
            txtRank.Text = "";
            txtSampling.Text = "";

            rdoFirstOccurrence.IsChecked = true;
            rdoRecurrence.IsChecked = false;
            rdoP1Receive.IsChecked = true;
            txtOther.Text = "";

            ////P2
            txtP2Remark1.Text = "";
            txtP2Remark2.Text = "";
            txtP2Check.Text = "";
            txtP2Approve.Text = "";
            txtP2Detector.Text = "";
            txtP2Position.Text = "";
            dtP2Date.Value = DateTime.Now;
            dtP2Checked.Value = DateTime.Now;
            dtP2Approve.Value = DateTime.Now;

            ////P3
            txtP3Approve.Text = "";
            txtP3Incharge.Text = "";
            txtP3LaborCost.Text = "";
            txtP3OtherCost.Text = "";
            txtP3Qtyofsort.Text = "";
            txtP3Return.Text = "";
            txtP3Rework.Text = "";
            txtP3Scrap.Text = "";
            txtP3Sorting.Text = "";
            txtP3Sorttime.Text = "";
            txtP3Temporary.Text = "";
            txtP3TotalCharge.Text = "";
            rdoP3Scarp.IsChecked = true;
            dtP3Approve.Value = DateTime.Now;
            dtP3Incharge.Value = DateTime.Now;
            dtP3DueDate.Value = DateTime.Now;

            ////P4
            txtP4CausingSection.Text = "";
            txtP4Remark.Text = "";
            rdoP4Man.IsChecked = true;

            ////P5
            txtP5Incharge.Text = "";
            dtP5Incharge.Value = DateTime.Now;
            txtP5Remark.Text = "";

            ////P6
            txtP6Approved.Text = "";
            txtP6Carpar.Text = "";
            dtP6CloseDate.Value = DateTime.Now;
            rdoP6Accept.IsChecked = true;
            chkP6IssueCARPAR.Checked = false;

        }
        private void NewRecord()
        {
            

        }
        private void EditClick()
        {

            StatusAC = "Edit";
        }
        private void ViewClick()
        {

            DataLoad(txtNCRNo.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            CloseDate();
        }
        private void CloseDate()
        {
            if(MessageBox.Show("ต้องการปิดเอกสารหรือไม่ ?","ปิดเอกสาร",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_QCNCR nck = db.tb_QCNCRs.Where(p => p.NCRNo.Equals(txtNCRNo.Text) && p.SS < 7).FirstOrDefault();
                        if (nck != null)
                        {
                            db.sp_46_QCSelectWO_08_NCRCloseDate(txtNCRNo.Text, dtP6CloseDate.Value);
                            MessageBox.Show("ปิดเอกสารเรียบร้อย!");
                            DataLoad(txtNCRNo.Text);
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถปิดได้ ");
                        }
                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
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
            if(txtPartNo.Text.Equals(""))
            {
                err += "Part No. ว่าง\n";
            }
            if(txtQuantity.Text.Equals(""))
            {
                err += "Quantity ว่าง\n";
            }
            if(txtNGQty.Text.Equals(""))
            {
                err += "NG Qty ว่าง\n";
            }
            if(txtLotNo.Text.Equals(""))
            {
                err += "Lot No. ว่าง\n";
            }
            if(txtCustomer.Text.Equals(""))
            {
                err += "Customer ว่าง\n";
            }
            if(txtSupplier.Text.Equals(""))
            {
                err += "Supplier ว่าง\n";
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
        private decimal ConvertDecimal(string Value,decimal Default)
        {
            decimal RT = Default;
            decimal.TryParse(Value, out RT);

            return RT;
        }
        private decimal ConvertInt(string Value, int Default)
        {
            int RT = Default;
            int.TryParse(Value, out RT);

            return RT;
        }
        private string TypeofProblem()
        {
            string RT = "";
            if (rdoP1Receive.IsChecked)
                RT = rdoP1Receive.Text;
            if (rdoP1NGByAssy.IsChecked)
                RT = rdoP1NGByAssy.Text;
            if (rdoP1NGFoundinLine.IsChecked)
                RT = rdoP1NGFoundinLine.Text;
            if (rdoP1FinishGoods.IsChecked)
                RT = rdoP1FinishGoods.Text;
            if (rdoP1Other.IsChecked)
                RT = rdoP1Other.Text;

            return RT;
        }
        private string RootCause()
        {
            string RT = "";
            if (rdoP4Man.IsChecked)
                RT = rdoP4Man.Text;
            if (rdoP4Machine.IsChecked)
                RT = rdoP4Machine.Text;
            if (rdoP4Material.IsChecked)
                RT = rdoP4Material.Text;
            if (rdoP4Method.IsChecked)
                RT = rdoP4Method.Text;
            if (rdoP4TherOther.IsChecked)
                RT = rdoP4TherOther.Text;

            return RT;
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkData())
                {
                    try
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (txtStatus.Text.Equals("New"))
                            {                             

                                txtNCRNo.Text = dbClss.GetSeriesNo(77, 2);
                                tb_QCNCR ne = new tb_QCNCR();
                                ne.SS = 1;
                                ne.CreateBy = txtCreateBy.Text;
                                ne.CreateDate = DateTime.Now;
                                ne.QCNo = txtQCNo.Text;
                                ne.Dept = txtDept.Text;
                                ne.NCRNo = txtNCRNo.Text.ToUpper();
                                ne.NCRBy = dbClss.UserID;
                                ne.NCRDate = dtNCRDate.Value;

                                ne.PartNo = txtPartNo.Text.ToUpper();
                                ne.PartName = txtPartName.Text;
                                ne.IssueDebitNote = rdo1.IsChecked;
                                ne.ImageFile = "";
                                ne.Customer = txtCustomer.Text;
                                ne.Supplier = txtSupplier.Text;
                                ne.ReceiveDate = txtReciveDate.Text;
                                ne.LotNo = txtLotNo.Text;
                                ne.LotSize = ConvertDecimal(txtLotSize.Text, 1);
                                ne.Qty = ConvertDecimal(txtQuantity.Text, 1);
                                ne.NGQty = ConvertDecimal(txtNGQty.Text, 1);
                                ne.NGPPM = txtNGPPM.Text;
                                ne.Sampling = txtSampling.Text;
                                ne.ImageFile2 = "";
                                ne.Rank = txtRank.Text;
                                ne.WONo = txtWONo.Text;
                                try
                                {
                                    if (!txtLinkPath.Text.Equals("") && !PathNCR.Equals(""))
                                    {
                                        txtFileName.Text = txtNCRNo.Text + "" + System.IO.Path.GetExtension(txtLinkPath.Text);
                                        ne.ImageFile = txtFileName.Text;
                                        System.IO.File.Copy(txtLinkPath.Text, PathNCR + txtFileName.Text,true);
                                    }
                                }
                                catch { }

                                //P1//
                                ne.TypeofProblem = TypeofProblem();
                                ne.TypeofRemark = txtOther.Text;
                                if (rdoFirstOccurrence.IsChecked)
                                    ne.Occurrence = rdoFirstOccurrence.Text;
                                else
                                    ne.Occurrence = rdoRecurrence.Text;

                                //P2//
                                ne.P2Approve = txtP2Approve.Text;
                                ne.P2ApproveDate = dtP2Approve.Value;
                                ne.P2CheckDate = dtP2Checked.Value;
                                ne.P2Checked = txtP2Check.Text;
                                ne.P2Detector = txtP2Detector.Text;
                                ne.P2DetectorDate = dtP2Date.Value;
                                ne.P2Position = txtP2Position.Text;
                                ne.P2DefectReport1 = txtP2Remark1.Text;
                                ne.P2DefectReport2 = txtP2Remark2.Text;

                                //P3//
                                ne.P3ApproveBy = txtP3Approve.Text;
                                ne.P3ApproveDate = dtP3Approve.Value;
                                ne.P3Duedate = dtP3DueDate.Value;
                                ne.P3InchargeBy = txtP3Incharge.Text;
                                ne.P3InchargeDate = dtP3Incharge.Value;
                                ne.P3TemporaryAction = txtP3Temporary.Text;
                                ne.P3Scrap = rdoP3Scarp.IsChecked;
                                ne.P3Return = rdoP3Return.IsChecked;
                                ne.P3Rework = rdoRework.IsChecked;
                                ne.P3Sorting = rdoSorting.IsChecked;
                                ne.P3ScrapRemark = txtP3Scrap.Text;
                                ne.P3ReturnRemark = txtP3Return.Text;
                                ne.P3ReworkRemark = txtP3Rework.Text;
                                ne.P3SortingRemark = txtP3Sorting.Text;
                                ne.P3SortTime = Convert.ToInt32(ConvertDecimal(txtP3Sorttime.Text,0));
                                ne.P3QtyofSort = ConvertDecimal(txtP3Qtyofsort.Text, 0);
                                ne.P3LaborCost = ConvertDecimal(txtP3LaborCost.Text, 0);
                                ne.P3Cost = ConvertDecimal(txtP3OtherCost.Text, 0);
                                ne.P3TotalCost = ConvertDecimal(txtP3TotalCharge.Text, 0);

                                //P4//
                                ne.CausingSection = txtP4CausingSection.Text;
                                ne.P4Remark = txtP4Remark.Text;
                                ne.P4RootCauseRemark = "";
                                ne.P4RootCause = RootCause();

                                //P5//
                                ne.P5DueDate = dtP5Incharge.Value;
                                ne.P5Incharge = txtP5Incharge.Text;
                                ne.P5Remark = txtP5Remark.Text;


                                //P6//
                                ne.P6Accept = rdoP6Accept.IsChecked;
                                ne.P6Approveby = txtP6Approved.Text;
                                ne.P6ApproveDate = null;
                                ne.P6CodeDate = dtP6CloseDate.Value;
                                ne.P6IssueCar = chkP6IssueCARPAR.Checked;
                                ne.P6IssueCarNo = txtP6Carpar.Text;
                                ne.refid = Convert.ToInt32(ConvertDecimal(txtRefid.Text,0));

                                //Insert//
                                db.tb_QCNCRs.InsertOnSubmit(ne);
                                db.SubmitChanges();
                                MessageBox.Show("บันทึกเรียบร้อย");


                            }
                            else
                            {
                                tb_QCNCR ne = db.tb_QCNCRs.Where(p => p.NCRNo.Equals(txtNCRNo.Text) && p.SS.Equals(1)).FirstOrDefault();
                                if(ne!=null)
                                {
                                    ne.Dept = txtDept.Text;
                                  //  ne.NCRNo = txtNCRNo.Text.ToUpper();
                                    //ne.NCRBy = dbClss.UserID;
                                  //  ne.NCRDate = dtNCRDate.Value;

                                    ne.PartNo = txtPartNo.Text.ToUpper();
                                    ne.PartName = txtPartName.Text;
                                    ne.IssueDebitNote = rdo1.IsChecked;
                                    
                                    ne.Customer = txtCustomer.Text;
                                    ne.Supplier = txtSupplier.Text;
                                    ne.ReceiveDate = txtReciveDate.Text;
                                    ne.LotNo = txtLotNo.Text;
                                    ne.LotSize = ConvertDecimal(txtLotSize.Text, 1);
                                    ne.Qty = ConvertDecimal(txtQuantity.Text, 1);
                                    ne.NGQty = ConvertDecimal(txtNGQty.Text, 1);
                                    ne.NGPPM = txtNGPPM.Text;
                                    ne.Sampling = txtSampling.Text;                                   
                                    ne.Rank = txtRank.Text;
                                    try
                                    {
                                        if (!txtLinkPath.Text.Equals("") && !PathNCR.Equals(""))
                                        {
                                            txtFileName.Text = txtNCRNo.Text + "" + System.IO.Path.GetExtension(txtLinkPath.Text);
                                            ne.ImageFile = txtFileName.Text;
                                            System.IO.File.Copy(txtLinkPath.Text, PathNCR + txtFileName.Text, true);
                                        }
                                    }
                                    catch { }

                                    //P1//
                                    ne.TypeofProblem = TypeofProblem();
                                    ne.TypeofRemark = txtOther.Text;
                                   
                                    if (rdoFirstOccurrence.IsChecked)
                                        ne.Occurrence = rdoFirstOccurrence.Text;
                                    else
                                        ne.Occurrence = rdoRecurrence.Text;

                                    //P2//
                                    ne.P2Approve = txtP2Approve.Text;
                                    ne.P2ApproveDate = dtP2Approve.Value;
                                    ne.P2CheckDate = dtP2Checked.Value;
                                    ne.P2Checked = txtP2Check.Text;
                                    ne.P2Detector = txtP2Detector.Text;
                                    ne.P2DetectorDate = dtP2Date.Value;
                                    ne.P2Position = txtP2Position.Text;
                                    ne.P2DefectReport1 = txtP2Remark1.Text;
                                    ne.P2DefectReport2 = txtP2Remark2.Text;

                                    //P3//
                                    ne.P3ApproveBy = txtP3Approve.Text;
                                    ne.P3ApproveDate = dtP3Approve.Value;
                                    ne.P3Duedate = dtP3DueDate.Value;
                                    ne.P3InchargeBy = txtP3Incharge.Text;
                                    ne.P3InchargeDate = dtP3Incharge.Value;
                                    ne.P3TemporaryAction = txtP3Temporary.Text;
                                    ne.P3Scrap = rdoP3Scarp.IsChecked;
                                    ne.P3Return = rdoP3Return.IsChecked;
                                    ne.P3Rework = rdoRework.IsChecked;
                                    ne.P3Sorting = rdoSorting.IsChecked;
                                    ne.P3ScrapRemark = txtP3Scrap.Text;
                                    ne.P3ReturnRemark = txtP3Return.Text;
                                    ne.P3ReworkRemark = txtP3Rework.Text;
                                    ne.P3SortingRemark = txtP3Sorting.Text;
                                    ne.P3SortTime = Convert.ToInt32(ConvertDecimal(txtP3Sorttime.Text, 0));
                                    ne.P3QtyofSort = ConvertDecimal(txtP3Qtyofsort.Text, 0);
                                    ne.P3LaborCost = ConvertDecimal(txtP3LaborCost.Text, 0);
                                    ne.P3Cost = ConvertDecimal(txtP3OtherCost.Text, 0);
                                    ne.P3TotalCost = ConvertDecimal(txtP3TotalCharge.Text, 0);

                                    //P4//
                                    ne.CausingSection = txtP4CausingSection.Text;
                                    ne.P4Remark = txtP4Remark.Text;
                                    ne.P4RootCauseRemark = "";
                                    ne.P4RootCause = RootCause();

                                    //P5//
                                    ne.P5DueDate = dtP5Incharge.Value;
                                    ne.P5Incharge = txtP5Incharge.Text;
                                    ne.P5Remark = txtP5Remark.Text;


                                    //P6//
                                    ne.P6Accept = rdoP6Accept.IsChecked;
                                    ne.P6Approveby = txtP6Approved.Text;
                                    ne.P6ApproveDate = null;
                                    ne.P6CodeDate = dtP6CloseDate.Value;
                                    ne.P6IssueCar = chkP6IssueCARPAR.Checked;
                                    ne.P6IssueCarNo = txtP6Carpar.Text;
                                    ne.refid = Convert.ToInt32(ConvertDecimal(txtRefid.Text, 0));

                                    db.SubmitChanges();
                                    MessageBox.Show("บันทึกเรียบร้อย");
                                }
                            }

                        }

                       // StatusAC = "Waiting";
                        DataLoad(txtNCRNo.Text);

                      //  txtStatus.Text = "Waiting";
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
           
                if (MessageBox.Show("ต้องการลบ ข้อมูลหรือไม่?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(AC==1)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            //Call Store//
                            tb_QCNCR nck = db.tb_QCNCRs.Where(p => p.NCRNo.Equals(txtNCRNo.Text) && p.SS.Equals(1)).FirstOrDefault();
                            if (nck != null)
                            {
                                db.sp_46_QCSelectWO_08_NCRDelete(txtNCRNo.Text.Trim());
                                MessageBox.Show("ลบเรียบร้อย");
                                NewClick();
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถลบได้!");
                            }
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
           // dbClss.ExportGridXlSX(radGridView1);
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

                    DataLoad(txtNCRNo.Text);
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
                if (!txtNCRNo.Text.Equals(""))
                {

                    DataLoad(txtNCRNo.Text);
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
           

         

            Report.Reportx1.WReport = "NCR";
            Report.Reportx1.Value = new string[1];
            Report.Reportx1.Value[0] = txtNCRNo.Text;
            Report.Reportx1 op = new Report.Reportx1("NCR.rpt");
            op.Show();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            dbClss.checkDigit(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void AddItem()
        {
            
        }
        private void ClearListAdd()
        {
          
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
          
        }
        private void InpurtItem(string ItemCode)
        {
            
        }

        private void AddItem(string ItemNo,string ItemName,int Qty,string LotNo,string MoveFrom,string MoveTo,string Remark,string RefDocument)
        {
           
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
            
        }

        private void rdo1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            LoadLotNo();
        }
        private void LoadLotNo()
        {
            
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
            
                 
           
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            tb_Link.Text = "";
            NCRList mv = new NCRList(tb_Link);
            mv.ShowDialog();

            if(!tb_Link.Text.Equals(""))
            {
                txtNCRNo.Text = tb_Link.Text;
                DataLoad(txtNCRNo.Text);
            }
            
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            dbClss.CheckDigitDecimal(e);
        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.png)|*.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLinkPath.Text = openFileDialog1.FileName;
                pictureBox1.Image = System.Drawing.Image.FromFile(txtLinkPath.Text);
            }
        }

        private void btnRefid_Click(object sender, EventArgs e)
        {
            tb_Link.Text = "";
            txtWONo.Text = "";
            QCList mv = new QCList(tb_Link);
            mv.ShowDialog();

            if (!tb_Link.Text.Equals(""))
            {
                txtQCNo.Text = tb_Link.Text;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_QCHD qh = db.tb_QCHDs.Where(p => p.QCNo.Equals(txtQCNo.Text)).FirstOrDefault();
                    if (qh != null)
                    {
                        txtWONo.Text = qh.WONo;
                    }
                }
            }
        }

        private void txtQuantity_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            dbClss.CheckDigitDecimal(e);
        }

        private void txtPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                InsertItem();
            }
        }
        private void InsertItem()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var ItemA = db.sp_46_QCSelectWO_08_NCRItem(txtPartNo.Text).FirstOrDefault();
                    if(ItemA != null)
                    {
                        txtPartName.Text = ItemA.Description.ToString();
                        txtLotSize.Text = Convert.ToInt32(ItemA.SNP).ToString();
                        
                    }
                }
            }
            catch { }
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            Report.Reportx1.WReport = "NCR";
            Report.Reportx1.Value = new string[2];
            Report.Reportx1.Value[0] = txtNCRNo.Text;
            Report.Reportx1 op = new Report.Reportx1("NCRSort.rpt");
            op.Show();
        }
    }
}
