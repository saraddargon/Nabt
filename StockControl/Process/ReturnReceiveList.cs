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
    public partial class ReturnReceiveList : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReturnReceiveList()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.RadTextBox RCNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        string Screen = "";
        public ReturnReceiveList(Telerik.WinControls.UI.RadTextBox RCNoxxx,string Screenxxx)
        {
            InitializeComponent();
            RCNo_tt = RCNoxxx;
            screen = 1;
            Screen = Screenxxx;
        }

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
        private void GETDTRow()
        {

            //dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            //dt.Columns.Add(new DataColumn("Order", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            //dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        }
       
        private void Unit_Load(object sender, EventArgs e)
        {
            if(Screen.Equals("ReturnReceive"))
            {
                radRibbonBar1.Text = "Receive List (รายการรับสินค้าแล้ว)";
                btnSave.Text = "ทำคืนรายการ";
                radRibbonBarGroup1.Text = "Order";
            }
            else //ChangeInvoice
            {
                radRibbonBar1.Text = "Receive List (รายการรับสินค้าแล้ว)";
                btnSave.Text = "เปลี่ยน";
                radRibbonBarGroup1.Text = "Invoice/DL No";
            }

            dtDate1.Value = DateTime.Now;
            dtDate2.Value = DateTime.Now;

            dgvData.AutoGenerateColumns = false;
            //GETDTRow();
            DefaultItem();
            //dgvData.ReadOnly = false;
            DataLoad();
            //txtVendorNo.Text = "";
            
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboVendorName.AutoCompleteMode = AutoCompleteMode.Append;
                cboVendorName.DisplayMember = "VendorName";
                cboVendorName.ValueMember = "VendorNo";
                cboVendorName.DataSource =(from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo,ix.VendorName}).ToList();
                cboVendorName.SelectedIndex = -1;
                cboVendorName.SelectedValue = "";
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
        private void DataLoad()
        {
            //dt.Rows.Clear();
            
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string VendorNo = "";
                    if (!cboVendorName.Text.Equals(""))
                        VendorNo = txtVendorNo.Text;

                    DateTime inclusiveStart = dtDate1.Value.Date;
                    // Include the *whole* of the day indicated by searchEndDate
                    DateTime exclusiveEnd = dtDate2.Value.Date.AddDays(1);


                    try
                    {
                        var d = (from ix in db.tb_ReceiveHs select ix)
                            .Where(a => a.InvoiceNo.Contains(txtInvoiceNo.Text.Trim())
                                    && a.VendorNo.Contains(VendorNo)
                                    && a.Status != "Cancel"
                                    && (a.RCDate >= inclusiveStart
                                        && a.RCDate < exclusiveEnd)
                                    )
                                    .ToList();
                        if (d.Count() > 0)
                        {
                            
                            dgvData.DataSource = d;

                            int rowcount = 0;
                            foreach (var x in dgvData.Rows)
                            {
                                rowcount += 1;
                                x.Cells["dgvNo"].Value = rowcount;
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        //private bool CheckDuplicate(string code, string Code2)
        //{
        //    bool ck = false;

        //    using (DataClasses1DataContext db = new DataClasses1DataContext())
        //    {
        //        int i = (from ix in db.tb_Models
        //                 where ix.ModelName == code

        //                 select ix).Count();
        //        if (i > 0)
        //            ck = false;
        //        else
        //            ck = true;
        //    }

        //    return ck;
        //}

        
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            return;
            dgvData.ReadOnly = false;
            dgvData.AllowAddNewRow = false;
            dgvData.Rows.AddNew();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            dgvData.ReadOnly = false;
           // btnEdit.Enabled = false;
            btnView.Enabled = true;
            dgvData.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    if (screen.Equals(1))
                    {
                        //DateTime? Date = DateTime.Today;
                        string yyyyMM = "";
                        yyyyMM = Convert.ToDateTime(dgvData.CurrentRow.Cells["CreateDate"].Value).ToString("yyyyMM");
                        if (Convert.ToInt32(yyyyMM) >= Convert.ToInt32(DateTime.Today.ToString("yyyyMM")))
                        {
                            RCNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["InvoiceNo"].Value);
                            this.Close();
                        }
                        else
                            MessageBox.Show("ไม่สามารถทำการคืนรายการรับเข้าย้อนหลังข้ามเดือนได้");
                    }
                }
                else
                { MessageBox.Show("ไม่สามารถทำรายการได้"); }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }


       

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
            dbClss.ExportGridXlSX(dgvData);
        }

        
       

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            DataLoad();
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
            DataLoad();
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboVendorName.Text.Equals(""))
                txtVendorNo.Text = cboVendorName.SelectedValue.ToString();
            else
                txtVendorNo.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void dgvData_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    if (screen.Equals(1))
                    {
                        //DateTime? Date = DateTime.Today;
                        string yyyyMM = "";
                        yyyyMM = Convert.ToDateTime(e.Row.Cells["CreateDate"].Value).ToString("yyyyMM");
                        if (Convert.ToInt32(yyyyMM) >= Convert.ToInt32(DateTime.Today.ToString("yyyyMM")))
                        {
                            RCNo_tt.Text = Convert.ToString(e.Row.Cells["InvoiceNo"].Value);
                            this.Close();
                        }
                        else
                            MessageBox.Show("ไม่สามารถทำการคืนรายการรับเข้าย้อนหลังข้ามเดือนได้");
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnView_Click_1(object sender, EventArgs e)
        {

        }
    }
}
