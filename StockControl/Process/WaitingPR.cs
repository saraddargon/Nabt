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
    public partial class WaitingPR : Telerik.WinControls.UI.RadRibbonForm
    {
        public WaitingPR()
        {
            InitializeComponent();
        }

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
        private void GETDTRow()
        {
         
            dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("Order", typeof(decimal)));
            dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
            dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        }
        int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {
            //radGridView1.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            GETDTRow();
            DefaultItem();
            
            DataLoad();

            crow = 0;
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboVendor.AutoCompleteMode = AutoCompleteMode.Append;
                cboVendor.DisplayMember = "VendorName";
                cboVendor.ValueMember = "VendorNo";
                cboVendor.DataSource =(from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo,ix.VendorName}).ToList();
                cboVendor.SelectedIndex = -1;

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
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    try
                    {
                        string Vendorno = "";
                        //if (!cboVendor.Text.Equals(""))
                        Vendorno = txtVendorNo.Text;
                        

                        var gd = (from a in db.tb_Items
                                  join b in db.tb_Vendors on a.VendorNo equals b.VendorNo
                                  where a.Status == "Active" 
                                  && a.StopOrder == false
                                  && (a.VendorNo.Contains(Vendorno))
                                  && (b.VendorName.Contains(cboVendor.Text))
                                  && (( a.StockInv+a.StockDL+a.StockBackOrder
                                  
                                       // (Convert.ToDecimal(db.Cal_QTY(a.CodeNo, "Invoice", 0)))
                                                  //+ (Convert.ToDecimal(db.Cal_QTY(a.CodeNo, "Temp", 0)))
                                                   //+ (Convert.ToDecimal(db.Cal_QTY(a.CodeNo, "BackOrder", 0)))
                                    ) <= Convert.ToDecimal(a.MinimumStock))
                                  //&& (Convert.ToDecimal(a.StockInv) + Convert.ToDecimal(a.StockDL)
                                  //  + Convert.ToDecimal(a.StockBackOrder) <= Convert.ToDecimal(a.MinimumStock))
                                 
                                  select new {
                                      CodeNo = a.CodeNo,
                                      ItemDescription = a.ItemDescription,
                                      Order = Convert.ToDecimal(a.MaximumStock),

                                      //StockQty = (Convert.ToDecimal(db.Cal_QTY(a.CodeNo, "Invoice", 0)))
                                      //            + (Convert.ToDecimal(db.Cal_QTY(a.CodeNo, "Temp", 0))) ,
                                      StockQty = Convert.ToDecimal(a.StockInv) + Convert.ToDecimal(a.StockDL),
                                      //BackOrder = (Convert.ToDecimal(db.Cal_QTY(a.CodeNo, "BackOrder", 0))),
                                      BackOrder = StockControl.dbClss.TSt(a.StockBackOrder),


                                      UnitBuy = a.UnitBuy,
                                      PCSUnit = StockControl.dbClss.TSt(a.PCSUnit),
                                      LeadTime = StockControl.dbClss.TSt(a.Leadtime),
                                      MaxStock = StockControl.dbClss.TSt(a.MaximumStock),
                                      MinStock = StockControl.dbClss.TSt(a.MinimumStock),
                                      VendorNo = a.VendorNo,
                                      VendorName = b.VendorName,
                                      
                                  })//.Where(ab => ab.VendorNo.Contains(Vendorno))
                                  .ToList();
                        //dgvData.DataSource = gd;
                        if (gd.Count > 0)
                        {
                            foreach(var gg in gd)
                            {
                                dgvData.Rows.Add(false, "", gg.CodeNo,
                                                gg.ItemDescription,
                                                gg.Order,
                                                gg.StockQty,
                                                gg.BackOrder,
                                                gg.UnitBuy,
                                                gg.PCSUnit,
                                                gg.LeadTime,
                                                gg.MaxStock,
                                                gg.MinStock,
                                                gg.VendorNo,
                                                gg.VendorName);
                            }
                        }
                        int rowcount = 0;
                        foreach (var x in dgvData.Rows)
                        {
                            rowcount += 1;
                            x.Cells["dgvNo"].Value = rowcount;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string CodeNo)
        {
            bool ck = false;
            string CodeNo_temp = "";
            string CodeNo_temp2 = "";
            dgvData.EndEdit();

            foreach(var rowinfo in dgvData.Rows)
            {
                if (StockControl.dbClss.TBo(rowinfo.Cells["S"].Value))
                {
                    if (CodeNo_temp.Equals(""))
                        CodeNo_temp = StockControl.dbClss.TSt(rowinfo.Cells["VendorNo"].Value);
                    CodeNo_temp2 = StockControl.dbClss.TSt(rowinfo.Cells["VendorNo"].Value);

                    if (!CodeNo_temp.Equals(CodeNo_temp2))
                    {
                        ck = true;
                        break;
                    }
                    else
                        ck = false;
                }
                
                
            }
                return ck;
        }

        private bool AddUnit()
        {
          
            bool ck = false;
            int C = 0;
            try
            {


                dgvData.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in dgvData.Rows)
                    {
                        if (!Convert.ToString(g.Cells["ModelName"].Value).Equals("")
                            )
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                                //int yyyy = 0;
                                //int mmm = 0;
                                //decimal wk = 0;
                                //int.TryParse(Convert.ToString(g.Cells["YYYY"].Value), out yyyy);
                                //int.TryParse(Convert.ToString(g.Cells["MMM"].Value), out mmm);
                                //decimal.TryParse(Convert.ToString(g.Cells["WorkDays"].Value), out wk);
                                DateTime? d = null;
                                DateTime d1 = DateTime.Now;
                                if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
                                {

                                    tb_Model u = new tb_Model();
                                    u.ModelName = Convert.ToString(g.Cells["ModelName"].Value);
                                    u.ModelDescription = Convert.ToString(g.Cells["ModelDescription"].Value);
                                    u.ModelActive = Convert.ToBoolean(Convert.ToString(g.Cells["ModelActive"].Value));
                                    u.LineName = Convert.ToString(g.Cells["LineName"].Value);
                                    u.MCName = Convert.ToString(g.Cells["MCName"].Value);
                                    u.Limit = Convert.ToBoolean(g.Cells["Limit"].Value);
                                    if (DateTime.TryParse(Convert.ToString(g.Cells["ExpireDate"].Value), out d1))
                                    {
                                        d = dbClss.ChangeFormat(Convert.ToString(g.Cells["ExpireDate"].Value));
                                        //Convert.ToDateTime(Convert.ToString(g.Cells["ExpireDate"].Value));

                                    }
                                    u.ExpireDate = d;


                                    db.tb_Models.InsertOnSubmit(u);
                                    db.SubmitChanges();
                                    C += 1;
                                    dbClss.AddHistory(this.Name, "เพิ่ม", "Insert Model [" + u.ModelName + "]", "");

                                }
                                else
                                {

                                    var u = (from ix in db.tb_Models
                                             where ix.ModelName == Convert.ToString(g.Cells["dgvCodeTemp"].Value)

                                             select ix).First();

                                    u.ModelDescription = Convert.ToString(g.Cells["ModelDescription"].Value);
                                    u.ModelActive = Convert.ToBoolean(Convert.ToString(g.Cells["ModelActive"].Value));
                                    u.LineName = Convert.ToString(g.Cells["LineName"].Value);
                                    u.MCName = Convert.ToString(g.Cells["MCName"].Value);
                                    u.Limit = Convert.ToBoolean(g.Cells["Limit"].Value);

                                    if (DateTime.TryParse(Convert.ToString(g.Cells["ExpireDate"].Value), out d1))
                                    {
                                        d = dbClss.ChangeFormat(Convert.ToString(g.Cells["ExpireDate"].Value));
                                        //Convert.ToDateTime(Convert.ToString(g.Cells["ExpireDate"].Value));

                                    }
                                    u.ExpireDate = d;

                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Model [" + u.ModelName + "]", "");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("AddUnit", ex.Message, this.Name);
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
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
            btnCal.Enabled = true;
            dgvData.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("สร้างรายการสั่งซื้อ ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvData.EndEdit();
                //dt_createPR
                List<GridViewRowInfo> dgvRow_List = new List<GridViewRowInfo>();
                string CodeNo = "";
                foreach (GridViewRowInfo rowinfo in dgvData.Rows.Where(o => Convert.ToBoolean(o.Cells["S"].Value)))
                {
                    CodeNo = Convert.ToString(rowinfo.Cells["CodeNo"].Value);

                    if (CheckDuplicate(CodeNo))
                    {
                        MessageBox.Show("ไม่สามารถสั่งซื้อต่างผู้ขายได้");
                        break;
                    }
                    else
                    {
                        dgvRow_List.Add(rowinfo);
                    }
                }

                if (dgvRow_List.Count() > 0)
                {
                    CreatePR MS = new CreatePR(dgvRow_List);
                    MS.ShowDialog();
                }
                else
                    MessageBox.Show("กรุณาเลือกรายการ");
            }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                //int a = Convert.ToInt32(radGridView1.Rows[e.RowIndex].Cells["Order"].Value);

                Boolean ss = StockControl.dbClss.TBo(dgvData.Rows[e.RowIndex].Cells["S"].Value);
                if (ss.Equals(true))
                {
                    string CodeNo = ""; 
                    CodeNo = StockControl.dbClss.TSt(dgvData.Rows[e.RowIndex].Cells["CodeNo"].Value);

                    if (CheckDuplicate(CodeNo))
                    {
                        MessageBox.Show("ไม่สามารถสั่งซื้อต่างผู้ขายได้");
                        dgvData.Rows[e.RowIndex].Cells["S"].Value = false;

                    }
                }
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

            if (e.KeyData == (Keys.Control | Keys.S))
            {
                btnSave_Click(null, null);
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

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            DataLoad();
        }

       
        

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //using (DataClasses1DataContext db = new DataClasses1DataContext())
            //{
            if (!cboVendor.Text.Equals(""))
            {
                txtVendorNo.Text = Convert.ToString(cboVendor.SelectedValue);
                //var I = (from ix in db.tb_Vendors select ix).Where(a => a.Active == true 
                //                && a.VendorName.Equals(cboVendor.Text)).ToList();
                //if (I.Count > 0)

            }
            else
                txtVendorNo.Text = "";

            //}

        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (crow == 0)
                DataLoad();
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

        private void radGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (MessageBox.Show("สร้างรายการสั่งซื้อ ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvData.EndEdit();
                    //dt_createPR
                    List<GridViewRowInfo> dgvRow_List = new List<GridViewRowInfo>();
                    
                    dgvRow_List.Add(dgvData.CurrentRow);
                
                    if (dgvRow_List.Count() > 0)
                    {
                        CreatePR MS = new CreatePR(dgvRow_List);
                        MS.ShowDialog();
                    }
                    else
                        MessageBox.Show("กรุณาเลือกรายการ");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataLoad();
                MessageBox.Show("บันทึกสำเร็จ!");
            }
            catch (Exception ex) { }
            this.Cursor = Cursors.Default;
        }

        private void frezzRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {

                    int Row = 0;
                    Row = dgvData.CurrentRow.Index;
                    dbClss.Set_Freeze_Row(dgvData, Row);

                    //foreach (var rd in radGridView1.Rows)
                    //{
                    //    if (rd.Index <= Row)
                    //    {
                    //        radGridView1.Rows[rd.Index].PinPosition = PinnedRowPosition.Top;
                    //    }
                    //    else
                    //        break;
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frezzColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Columns.Count > 0)
                {
                    int Col = 0;
                    Col = dgvData.CurrentColumn.Index;
                    dbClss.Set_Freeze_Column(dgvData, Col);

                    //foreach (var rd in radGridView1.Columns)
                    //{
                    //    if (rd.Index <= Col)
                    //    {
                    //        radGridView1.Columns[rd.Index].PinPosition = PinnedColumnPosition.Left;
                    //    }
                    //    else
                    //        break;
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void unFrezzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dbClss.Set_Freeze_UnColumn(dgvData);
                dbClss.Set_Freeze_UnRows(dgvData);
                //foreach (var rd in radGridView1.Rows)
                //{
                //    radGridView1.Rows[rd.Index].IsPinned = false;
                //}
                //foreach (var rd in radGridView1.Columns)
                //{
                //    radGridView1.Columns[rd.Index].IsPinned = false;                   
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
