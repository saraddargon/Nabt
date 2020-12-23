using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Telerik.WinControls.UI;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;
namespace StockControl
{
    public partial class QCSetMasterSelect : Telerik.WinControls.UI.RadRibbonForm
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           // MessageBox.Show(keyData.ToString());
            if (keyData == (Keys.Control | Keys.S))
            {
                // Alt+F pressed
                //  ClearData();

                return false;
                //txtSeriesNo.Focus();
            }
            else if ((keyData == Keys.NumPad1 ) || (keyData== Keys.D1))
            {
                radioButton1.Checked = true;
            }
            else if ((keyData == Keys.NumPad2) || (keyData == Keys.D2))
            {
                radioButton2.Checked = true;
            }
            else if ((keyData == Keys.NumPad3) || (keyData == Keys.D3))
            {
                radioButton3.Checked = true;
            }
            else if ((keyData == Keys.NumPad4) || (keyData == Keys.D4))
            {
                radioButton4.Checked = true;
            }
            else if ((keyData == Keys.NumPad5) || (keyData == Keys.D5))
            {
                radioButton5.Checked = true;
            }
            else if (keyData == (Keys.F9))
            {
                SelectLoad();
            }
            else if (keyData == (Keys.Escape))
            {
                this.Close();
            }
           

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public QCSetMasterSelect()
        {
            InitializeComponent();
        }
        public QCSetMasterSelect(string OrderNo,string Linex,string PartNo,RadTextBox tx,string Ty)
        {
            InitializeComponent();
            WONo = OrderNo;
            LineName = Linex;
            Code = PartNo;
            ISO = tx;
            PType = Ty;
        }
        string Code = "";
        string PType = "";
        string LineName = "";
        string WONo = "";
        RadTextBox ISO = new RadTextBox();
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
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   var listQ= db.sp_46_QCSelectWO_02(WONo, LineName, Code, PType).ToList();
                    int CountA = 0;
                    foreach(var rd in listQ)
                    {
                        CountA += 1;
                        if (CountA == 1)
                        {
                            radioButton1.Text = rd.FormISO + " " + rd.FormName;
                            radTextBox1.Text = rd.FormISO;
                            radioButton1.Visible = true;
                        }
                        else if (CountA == 2)
                        {
                            radioButton2.Text = rd.FormISO + " " + rd.FormName;
                            radTextBox2.Text = rd.FormISO;
                            radioButton2.Visible = true;
                        }
                        else if (CountA == 3)
                        {
                            radioButton3.Text = rd.FormISO + " " + rd.FormName;
                            radTextBox3.Text = rd.FormISO;
                            radioButton3.Visible = true;
                        }
                        else if (CountA == 4)
                        {
                            radioButton4.Text = rd.FormISO + " " + rd.FormName;
                            radTextBox4.Text = rd.FormISO;
                            radioButton4.Visible = true;
                        }
                        else if (CountA == 5)
                        {
                            radioButton5.Text = rd.FormISO + " " + rd.FormName;
                            radTextBox5.Text = rd.FormISO;
                            radioButton5.Visible = true;
                        }
                    }
                }



            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void SetFocus()
        {
           
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
            SelectLoad();
        }
        private void SelectLoad()
        {
            if (radioButton1.Checked)
            {
                ISO.Text = radTextBox1.Text;
            }
            else if (radioButton2.Checked)
            {
                ISO.Text = radTextBox2.Text;
            }
            else if (radioButton3.Checked)
            {
                ISO.Text = radTextBox3.Text;
            }
            else if (radioButton4.Checked)
            {
                ISO.Text = radTextBox4.Text;
            }
            else if (radioButton5.Checked)
            {
                ISO.Text = radTextBox5.Text;
            }
            this.Close();
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
                      
        }

        private void radGridView2_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
           
        }
    }
}
