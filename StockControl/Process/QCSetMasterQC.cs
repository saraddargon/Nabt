using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
namespace StockControl
{
    public partial class QCSetMasterQC : Telerik.WinControls.UI.RadRibbonForm
    {
        public QCSetMasterQC()
        {
            InitializeComponent();
        }
        public QCSetMasterQC(string UserIDx)
        {
            InitializeComponent();
            PType = UserIDx;
            
        }
        string Code = "";
        string PType = "";
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
                SetFocus();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void SetFocus()
        {
            radGridView2.DataSource = null;
            txtPartNo.Text = "";
            txtProdNo.Text = "";
            txtLineNo.Text = "";
            txtScanID.Text = "";
            
            txtScanID.Focus();
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
            //DataLoad();
          //  txtScanID.Text = txtProdNo.Text;
            string SC = txtProdNo.Text;
            radGridView2.DataSource = null;
            txtPartNo.Text = "";
            txtProdNo.Text = "";
            txtLineNo.Text = "";
            txtScanID.Text = "";
           txtScanID.Focus();
           // getWO();

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
        string PDTAG = "";
        private void getWO()
        {
            try
            {

                if(!txtScanID.Text.Trim().Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        string[] Data = txtScanID.Text.Split(',');
                        PDTAG = txtScanID.Text.ToUpper();
                        if (Data.Length == 8)
                        {
                            string WO = "";
                            decimal Qty = 0;      
                            WO = Data[1].ToString().ToUpper();
                            Qty = Convert.ToDecimal(Data[2]);

                            var woList = db.sp_46_QCSelectWO_01(WO.ToUpper()).FirstOrDefault();


                            txtPartNo.Text = woList.CODE.ToString();
                            txtProdNo.Text = woList.PORDER.ToString();
                            txtLineNo.Text = woList.BUMO.ToString();
                            txtQty.Text = Convert.ToDecimal(Qty).ToString("###,###.##");
                            txtLotNo.Text = woList.LotNo.ToString();
                            var FormList = db.sp_46_QCSelectWO_02(txtProdNo.Text.ToUpper(), txtLineNo.Text, txtPartNo.Text, "QC").ToList();
                            radGridView2.DataSource = FormList;
                            txtScanID.Text = "";
                            txtScanID.Focus();
                        }
                        else
                        {
                            txtScanID.Text = "";
                            txtScanID.Focus();
                        }
                        ////Load Datagridview///
                    }
                }
            }
            catch { }
        }

        private void radGridView2_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                //    string FormType = radGridView2.Rows[e.RowIndex].Cells["FormISO"].Value.ToString();
                //    if(FormType.Equals("FM-QA-055_02_1"))
                //    {

                //        QCForm5501 qcf = new QCForm5501(txtProdNo.Text, "FM-QA-055_02_1", PDTAG);
                //        qcf.ShowDialog();

                //    }
                //    else if(FormType.Equals("FM-QA-056_02_1"))
                //    {
                //        QCForm5601 qcf = new QCForm5601(txtProdNo.Text, "FM-QA-056_02_1", PDTAG);
                //        qcf.ShowDialog();
                //             //   QCSetFormFM_PD_035_02_1 FMD = new QCSetFormFM_PD_035_02_1(txtProdNo.Text.ToUpper(), "FM-QA-056_02_1",PDTAG);
                //             //   FMD.Show();
                //    }

            }
        }
    }
}
