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
    public partial class ShippingList2 : Telerik.WinControls.UI.RadRibbonForm
    {
        public ShippingList2()
        {
            InitializeComponent();
        }
        public ShippingList2(Telerik.WinControls.UI.RadTextBox SHNoxxx
                    , Telerik.WinControls.UI.RadTextBox CodeNoxxx
                 , Telerik.WinControls.UI.RadTextBox txtItemDescriptionxxx
            , Telerik.WinControls.UI.RadTextBox  txtQTYxxx
            , Telerik.WinControls.UI.RadTextBox txtidxxx
            )
        {
            InitializeComponent();
            SHNo_tt = SHNoxxx;
            CodeNo_tt = CodeNoxxx;
            txtItemDescription_tt = txtItemDescriptionxxx;
            txtQTY_tt = txtQTYxxx;
            txtid_tt = txtidxxx;
            screen = 1;

        }
        Telerik.WinControls.UI.RadTextBox SHNo_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox txtItemDescription_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox txtQTY_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox txtid_tt = new Telerik.WinControls.UI.RadTextBox();


        int screen = 0;
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
            dtDate1.Value = DateTime.Now;
            dtDate2.Value = DateTime.Now;
            //radGridView1.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            //GETDTRow();
           // DefaultItem();
            
            DataLoad();

         
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
        private void DataLoad()
        {
            dgvData.Rows.Clear();
            
            try
            {
                
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DateTime inclusiveStart = dtDate1.Value.Date;
                    // Include the *whole* of the day indicated by searchEndDate
                    DateTime exclusiveEnd = dtDate2.Value.Date.AddDays(1);
                    

                    var r = (from d in db.tb_Shippings
                             join h in db.tb_ShippingHs on d.ShippingNo equals h.ShippingNo
                             join i in db.tb_Items on d.CodeNo equals i.CodeNo

                             where h.Status != "Cancel" //&& d.verticalID == VerticalID
                                    && d.Status != "Cancel"
                                 && d.ShippingNo.Contains(txtSHNo.Text.Trim())
                                 && d.CodeNo.Contains(txtCodeNo.Text.Trim())

                                  && (h.ShipDate >= inclusiveStart
                                        && h.ShipDate < exclusiveEnd)
                             select new
                             {
                                 ShippingNo = d.ShippingNo,
                                 CodeNo = d.CodeNo,
                                 ItemNo = d.ItemNo,
                                 ItemDescription = d.ItemDescription,
                                 QTY = d.QTY,
                                 UnitShip = d.UnitShip,
                                 PCSUnit = d.PCSUnit,
                                 LeadTime = i.Leadtime,
                                 MaxStock = i.MaximumStock,
                                 MinStock = i.MinimumStock,
                                 ShipName = h.ShipName,
                                 CreateDate = h.CreateDate,

                                 CreateBy = h.CreateBy,
                                 Remark = d.Remark,
                                 Status = d.Status,
                                 id = d.id,


                             }).ToList();
                    dgvData.DataSource = r;

                    int rowcount = 0;
                    foreach (var x in dgvData.Rows)
                    {
                        rowcount += 1;
                        x.Cells["dgvNo"].Value = rowcount;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
      
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            dgvData.ReadOnly = false;
           // btnEdit.Enabled = false;
            btnPrint.Enabled = true;
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
                        SHNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["ShippingNo"].Value);
                        CodeNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["CodeNo"].Value);
                        txtItemDescription_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["ItemDescription"].Value);
                        txtQTY_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["QTY"].Value);
                        txtid_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["id"].Value);
                        this.Close();
                    }
                }
                else
                { MessageBox.Show("ไม่สามารถทำรายการได้"); }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //try
            //{
            //    //radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
            //    //string TM1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value);
            //    ////string TM2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["MMM"].Value);
            //    //string Chk = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
            //    //if (Chk.Equals("") && !TM1.Equals(""))
            //    //{

            //    //    if (!CheckDuplicate(TM1, Chk))
            //    //    {
            //    //        MessageBox.Show("ข้อมูล รายการซ้า");
            //    //        radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value = "";
            //    //        //  radGridView1.Rows[e.RowIndex].Cells["MMM"].Value = "";
            //    //        //  radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

            //    //    }
            //    //}


            //}
            //catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void MasterTemplate_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    if (screen.Equals(1))
                    {
                        SHNo_tt.Text = Convert.ToString(e.Row.Cells["ShippingNo"].Value);
                        CodeNo_tt.Text = Convert.ToString(e.Row.Cells["CodeNo"].Value);
                        txtItemDescription_tt.Text = Convert.ToString(e.Row.Cells["ItemDescription"].Value);
                        txtQTY_tt.Text = Convert.ToString(e.Row.Cells["QTY"].Value);
                        txtid_tt.Text = Convert.ToString(e.Row.Cells["id"].Value);
                       
                        this.Close();
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
