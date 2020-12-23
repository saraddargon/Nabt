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
    public partial class QCSetFormFM_PD_035_02_1 : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCSetFormFM_PD_035_02_1()
        {
            InitializeComponent();
        }
        public QCSetFormFM_PD_035_02_1(string Wox,string FormISOx)
        {
            InitializeComponent();
            WOs = Wox;
            FormISO = FormISOx;
            //this.Text = "Detail Production Order -> " + FormISOx;
        }
        string WOs = "";
        string FormISO = "";
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
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {

            try
            {
                LoadData();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void LoadData()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var woList = db.sp_46_QCSelectWO_01(WOs).FirstOrDefault();

                    if(woList!=null)
                    {
                        radGridView2.DataSource = null;
                        txtPartNo.Text = woList.CODE.ToString();
                        txtProdNo.Text = woList.PORDER.ToString().ToUpper();
                        txtLineNo.Text = woList.BUMO.ToString();
                        txtLotNo.Text = woList.LotNo.ToString();
                        txtQty.Text = Convert.ToDecimal(woList.OrderQty).ToString("###,###.##");
                        txtDayNight.Text = woList.DayNight.ToString();
                        string Tx = db.get_QC_FromISOGet01(FormISO, 0);
                        //this.Text = "Detail Production Order -> " + FormISO + " " + Tx;
                        groupBox1.Text = "Detail Production Order -> " + FormISO + " " + Tx;
                       
                        db.sp_46_QCSelectWO_03_Copy(FormISO, txtProdNo.Text);
                        var RList = db.sp_46_QCSelectWO_03(txtProdNo.Text.ToUpper(), FormISO).ToList();
                        if(RList.Count>0)
                        {
                            radGridView2.DataSource = RList;
                        }

                        //Lot No.
                        //Day / Night
                        string PRN = "";
                        foreach(GridViewRowInfo rd in radGridView2.Rows)
                        {
                            PRN = "";
                            if(Convert.ToString(rd.Cells["Description"].Value).Equals("Lot No.") && Convert.ToString(rd.Cells["Value1"].Value).Equals(""))
                            {
                                rd.Cells["Value1"].Value = txtLotNo.Text.ToUpper();
                            }
                            if (Convert.ToString(rd.Cells["Description"].Value).Equals("Day/Night") && Convert.ToString(rd.Cells["Value1"].Value).Equals(""))
                            {
                                rd.Cells["Value1"].Value = txtDayNight.Text.ToUpper();
                            }
                            if(Convert.ToString(rd.Cells["Description"].Value).Equals("Part No.") && Convert.ToString(rd.Cells["Value1"].Value).Equals(""))
                            {
                                PRN = txtPartNo.Text;
                                if (FormISO.Equals("FM-PD-033_00_1"))
                                {
                                    PRN = dbClss.Right(txtPartNo.Text, 5);
                                   // PRN = PRN.Replace("4412", "");
                                   // PRN = PRN.Replace("4120", "");
                                }

                                rd.Cells["Value1"].Value = PRN;
                            }
                            if (Convert.ToString(rd.Cells["Description"].Value).Equals("Stamp No.") && Convert.ToString(rd.Cells["Value1"].Value).Equals(""))
                            {                                
                                    PRN = dbClss.Right(txtPartNo.Text, 5); 
                                    rd.Cells["Value1"].Value = PRN;
                            }
                        }


                    }
                }
            }
            catch { }
          
           
        }
        private void RMenu6_Click(object sender, EventArgs e)
        {
           
           // DeleteUnit();
            //DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            //EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
           // ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
           // NewClick();

        }

        private void DataLoad()
        {
           
            
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
            LoadData();
            //DataLoad();
            //radGridView2.DataSource = null;
            //txtPartNo.Text = "";
            //txtProdNo.Text = "";
            //txtLineNo.Text = "";

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
            //NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            //EditClick();
        }
        private void Saveclick()
        {
           
        }
        private void UploadImage(string Path,string Listpath)
        {
          
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("ต้องการบันทึกหรือไม่ ?","บันทึก",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_QCHD qch = db.tb_QCHDs.Where(q => q.WONo.Equals(txtProdNo.Text) && q.FormISO.Equals(FormISO)).FirstOrDefault();
                        if(qch == null)
                        {
                            tb_QCHD qcN = new tb_QCHD();
                            qcN.QCNo = dbClss.GetSeriesNo(6, 2);
                            qcN.WONo = txtProdNo.Text.ToUpper();
                            qcN.PartNo = txtPartNo.Text;
                            qcN.OrderQty = Convert.ToDecimal(txtQty.Text);
                            qcN.OKQty = 0;
                            qcN.NGQty = 0;
                            qcN.LotNo = txtLotNo.Text;
                            qcN.LineName = txtLineNo.Text;
                            qcN.CreateBy = dbClss.UserID;
                            qcN.CreateDate = DateTime.Now;
                            qcN.SS = 1;
                            qcN.Status = "Checking";
                            qcN.FormISO = FormISO;
                            qcN.DocRef1 = "";
                            qcN.DocRef2 = "";
                            qcN.ApproveBy = "";
                            qcN.ApproveBy2 = "";
                            qcN.CheckBy1 = "";
                            qcN.CheckBy2 = "";
                            qcN.IssueBy = dbClss.UserID;
                            qcN.IssueDate = DateTime.Now;
                            db.tb_QCHDs.InsertOnSubmit(qcN);
                            db.SubmitChanges();                            
                        }
                        int idr = 0;
                        foreach (GridViewRowInfo rd in radGridView2.Rows)
                        {
                            idr = Convert.ToInt32(rd.Cells["id"].Value);
                            tb_QCSetupPoint sp = db.tb_QCSetupPoints.Where(q => q.id.Equals(idr)).FirstOrDefault();
                            if(sp!=null)
                            {
                                sp.Value1 = Convert.ToString(rd.Cells["Value1"].Value);
                                db.SubmitChanges();
                            }

                        }
                    }

                    MessageBox.Show("บันทึกสำเร็จ");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                //if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    //AddUnit();
                //    //DataLoad();
                //}
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                //if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    //NewClick();
                //}
            }

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
                            if (a>1)
                            {
                                if(c==1)
                                    rd["UnitCode"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["UnitDetail"] = Convert.ToString(field);
                                else if(c==3)
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
                if(dt.Rows.Count>0)
                {
                    dbClss.AddHistory(this.Name, "Import", "Import file CSV in to System", "");
                    ImportData();
                    MessageBox.Show("Import Completed.");

                    DataLoad();
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
           // radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void txtScanID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                getWO();
            }
        }
        private void getWO()
        {
            try
            {

              
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                       
                        string WO = "";                

                        var woList = db.sp_46_QCSelectWO_01(WO.ToUpper()).FirstOrDefault();
                        

                        txtPartNo.Text = woList.CODE.ToString();
                        txtProdNo.Text = woList.PORDER.ToString();
                        txtLineNo.Text = woList.BUMO.ToString();
                        txtQty.Text = Convert.ToDecimal(woList.OrderQty).ToString("###,###.##");
                        txtLotNo.Text = woList.LotNo.ToString();
                        var FormList = db.sp_46_QCSelectWO_02(txtProdNo.Text.ToUpper(),txtLineNo.Text,txtPartNo.Text,"PD").ToList();
                        radGridView2.DataSource = FormList;
                       
                        ////Load Datagridview///
                    }
                
            }
            catch { }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                Report.Reportx1.WReport = "QCReport01";
                Report.Reportx1.Value = new string[3];
                Report.Reportx1.Value[0] = "";
                Report.Reportx1.Value[1] = "";
                Report.Reportx1 op = new Report.Reportx1(FormISO+".rpt");
                op.Show();
               
            }
            catch { }
        }
    }
}
