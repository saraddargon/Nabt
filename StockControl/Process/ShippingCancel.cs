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
    public partial class ShippingCancel : Telerik.WinControls.UI.RadRibbonForm
    {
        public ShippingCancel()
        {
            InitializeComponent();
        }

        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
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
        //private void GETDTRow()
        //{

        //    dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
        //    dt.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Order", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
        //    dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        //}
        
        private void Unit_Load(object sender, EventArgs e)
        {
            ddlType.Text = "ทั้งใบ";
            //txtCNNo.Text = StockControl.dbClss.GetNo(6, 0);
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //cboVendor.AutoCompleteMode = AutoCompleteMode.Append;
                //cboVendor.DisplayMember = "VendorName";
                //cboVendor.ValueMember = "VendorNo";
                //cboVendor.DataSource =(from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo,ix.VendorName}).ToList();
                //cboVendor.SelectedIndex = -1;

                try
                {

               

                    //GridViewMultiComboBoxColumn col = (GridViewMultiComboBoxColumn)radGridView1.Columns["CodeNo"];
                    //col.DataSource = (from ix in db.tb_Items.Where(s => s.Status.Equals("Active")) select new { ix.CodeNo, ix.ItemDescription }).ToList();
                    //col.DisplayMember = "CodeNo";
                    //col.ValueMember = "CodeNo";

                    //col.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                    //col.FilteringMode = GridViewFilteringMode.DisplayMember;

                    // col.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;
                }
                catch { }

                //col.TextAlignment = ContentAlignment.MiddleCenter;
                //col.Name = "CodeNo";
                //this.radGridView1.Columns.Add(col);

                //this.radGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                //this.radGridView1.CellEditorInitialized += radGridView1_CellEditorInitialized;
            }
        }
     
        private bool CheckDuplicate(string code, string Code2)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Models
                         where ix.ModelName == code

                         select ix).Count();
                if (i > 0)
                    ck = false;
                else
                    ck = true;
            }

            return ck;
        }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            return;
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //radGridView1.Rows.AddNew();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           // radGridView1.ReadOnly = false;
           //// btnEdit.Enabled = false;
           // btnView.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ddlType.Text.Equals(""))
            {
                MessageBox.Show("กรุณาเลือกประเภทการคืนรายการ");
                return;
            }

            else if (ddlType.Text.Equals("ตามรายการ") && ((txtid.Text.Equals(""))) && txtid.Text.Equals("0"))
            {
                MessageBox.Show("ไม่สามารถทำการคืนรายการได้");
                return;
            }
            else if (ddlType.Text.Equals("ทั้งใบ") && txtSHNo.Text.Equals(""))
            {
                MessageBox.Show("ไม่สามารถทำการคืนรายการได้");
                return;
            }

            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtCNNo.Text = StockControl.dbClss.GetNo(6, 2);

                if(ddlType.Text.Equals("ตามรายการ"))
                    Save_detail();
                else if(ddlType.Text.Equals("ทั้งใบ"))
                    Save_herder();

                MessageBox.Show("บันทึกสำเร็จ!");

                ClearData();
            }
        }
        private void Save_detail()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var g = (from ix in db.tb_Shippings
                //         where ix.ShippingNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                //         //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                //         select ix).ToList();
                //if (g.Count > 0)  //มีรายการในระบบ
                //{
                //    //Herder 
                //    string RCNo = StockControl.dbClss.TSt(g.FirstOrDefault().RCNo);

                //}
            }
        }
        private void Save_herder()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var g = (from ix in db.tb_Shippings
                //         where ix.InvoiceNo.Trim() == txtInvoiceNo.Text.Trim() && ix.Status != "Cancel"
                //         //&& ix.TEMPNo.Trim() == txtTempNo.Text.Trim()
                //         select ix).ToList();
                //if (g.Count > 0)  //มีรายการในระบบ
                //{
                //    //Herder 
                //    string RCNo = StockControl.dbClss.TSt(g.FirstOrDefault().RCNo);

                //}
            }
        }
        private void ClearData()
        {
            ddlType.Text = "ทั้งใบ";
            txtSHNo.Text = "";
            txtid.Text = "";
            txtCodeNo.Text = "";
            txtItemDescription.Text = "";
            txtQTY.Text = "";
            txtCNNo.Text = "";
        }
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
               // radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
                //string TM1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value);
                ////string TM2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["MMM"].Value);
                //string Chk = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                //if (Chk.Equals("") && !TM1.Equals(""))
                //{

                //    if (!CheckDuplicate(TM1, Chk))
                //    {
                //        MessageBox.Show("ข้อมูล รายการซ้า");
                //        radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value = "";
                //        //  radGridView1.Rows[e.RowIndex].Cells["MMM"].Value = "";
                //        //  radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                //    }
                //}


            }
            catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        return;
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        
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

       
        private void btnFilter1_Click(object sender, EventArgs e)
        {
          //  radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
           // radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
         
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            if(!ddlType.Text.Equals(""))
            {
                try
                {


                    this.Cursor = Cursors.WaitCursor;
                    ShippingList2 sc = new ShippingList2(txtSHNo,txtCodeNo,txtItemDescription,txtQTY,txtid);
                    this.Cursor = Cursors.Default;
                    sc.ShowDialog();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                    ClassLib.Memory.Heap();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("ReturnReceive", ex.Message + " : radButton1_Click_1", this.Name); }
                finally { this.Cursor = Cursors.Default; }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกประเภทการคืนก่อน");
            }
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //if (e.CellElement.ColumnInfo.Name == "ModelName")
            //{
            //    if (e.CellElement.RowInfo.Cells["ModelName"].Value != null)
            //    {
            //        if (!e.CellElement.RowInfo.Cells["ModelName"].Value.Equals(""))
            //        {
            //            e.CellElement.DrawFill = true;
            //            // e.CellElement.ForeColor = Color.Blue;
            //            e.CellElement.NumberOfColors = 1;
            //            e.CellElement.BackColor = Color.WhiteSmoke;
            //        }

            //    }
            //}
        }

        private void txtModelName_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (crow == 0)
            //    DataLoad();
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
           
        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if(radCheckBox1.Checked)
            //{
            //    foreach(var rd in radGridView1.Rows)
            //    {
            //        rd.Cells["S"].Value = true;
            //    }
            //}else
            //{
            //    foreach (var rd in radGridView1.Rows)
            //    {
            //        rd.Cells["S"].Value = false;
            //    }
            //}
        }

        private void radTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
