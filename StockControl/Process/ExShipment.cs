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
using Telerik.WinControls;

namespace StockControl
{
    public partial class ExShipment : Telerik.WinControls.UI.RadRibbonForm
    {
        public ExShipment()
        {
            InitializeComponent();
        }
        public ExShipment(string Invvoicex)
        {
            InitializeComponent();
            Inv = Invvoicex;
            txtExportNo.Text = Inv;
        }
        string Inv = "";
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
            dt.Columns.Add(new DataColumn("edit", typeof(bool)));
            dt.Columns.Add(new DataColumn("code", typeof(string)));
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
            //radGridView1.ReadOnly = true;
            //radGridView1.AutoGenerateColumns = false;
            //GETDTRow();

            LoadData();



        }
        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_ExportList ex = db.tb_ExportLists.Where(es => es.InvoiceNo == Inv).FirstOrDefault();
                    if(ex!=null)
                    {
                        txtLoadDate.Text = Convert.ToDateTime(ex.LoadDate).ToString("dd/MM/yyyy");


                        //LoadDetail//
                        db.sp_013_selectExportList_DetailUpdatecustItem(Inv);
                        radGridView1.DataSource = null;
                       
                        var pl = (from ix in db.sp_013_selectExportList_Detail(Inv, cboStatus.Text) select ix).ToList();
                        if(pl.Count>0)
                        {
                            txtTotalPallet.Text = pl.FirstOrDefault().TotalPallet.ToString();
                            radGridView1.DataSource = pl;
                            int[] ts = new int[] { 8 };
                            MergeVertically(radGridView1, ts);

                        }

                        foreach(GridViewRowInfo rs in radGridView1.Rows)
                        {
                            if(!Convert.ToBoolean(rs.Cells["LConfirm"].Value))
                            {
                                rs.Cells["S"].ReadOnly = true;
                            }

                            if(Convert.ToString(rs.Cells["PartNo"].Value).Trim().Equals("-"))
                            {
                                rs.Cells["S"].ReadOnly = false;
                            }

                            if(dbClss.UserID.Equals("0203")|| dbClss.UserID.Equals("0240"))
                            {
                                rs.Cells["S"].ReadOnly = false;
                            }

                        }
                        
                        //int ck = 0;
                        // foreach()
                       // MergeVertical2();
                    }
                }
            }
            catch(Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
        private void MergeVertical2()
        {
            
            ColumnGroupsViewDefinition view = new ColumnGroupsViewDefinition();
            //view.ColumnGroups.Add(new GridViewColumnGroup("Pallet"));

            //view.ColumnGroups[0].Rows.Add(new GridViewColumnGroupRow());
            //view.ColumnGroups[0].Rows[0].ColumnNames.Add(this.radGridView1.Columns["UnitsInStock"]);
            //view.ColumnGroups[0].Rows[0].Columns.Add(this.radGridView1.Columns["UnitsOnOrder"]);
            //view.ColumnGroups[0].Rows.Add(new GridViewColumnGroupRow());
            //view.ColumnGroups[0].Rows[1].Columns.Add(this.radGridView1.Columns["QuantityPerUnit"]);

            view.ColumnGroups.Add(new GridViewColumnGroup("Pallet"));
            view.ColumnGroups[0].Rows.Add(new GridViewColumnGroupRow());
            view.ColumnGroups[0].Rows[0].ColumnNames.Add("GroupP");
            view.ColumnGroups[0].ShowHeader = false;

            radGridView1.ViewDefinition = view;

        }
        private void setCellBorders(GridViewCellInfo cell, Color color)
        {
            cell.Style.CustomizeBorder = true;
            cell.Style.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            cell.Style.BorderLeftColor = color;
            cell.Style.BorderRightColor = color;
            cell.Style.BorderBottomColor = color;
            if (cell.Style.BorderTopColor != Color.Transparent)
            {
                cell.Style.BorderTopColor = color;
            }
        }

        private void MergeVertically(RadGridView radGridView, int[] columnIndexes)
        {
            GridViewRowInfo Prev = null;
            foreach (GridViewRowInfo item in radGridView.Rows)
            {
                if (Prev != null)
                {
                    string firstCellText = string.Empty;
                    string secondCellText = string.Empty;

                    foreach (int i in columnIndexes)
                    {
                        GridViewCellInfo firstCell = Prev.Cells[i];
                        GridViewCellInfo secondCell = item.Cells[i];

                        firstCellText = (firstCell != null && firstCell.Value != null ? firstCell.Value.ToString() : string.Empty);
                        secondCellText = (secondCell != null && secondCell.Value != null ? secondCell.Value.ToString() : string.Empty);

                        setCellBorders(firstCell, Color.FromArgb(209, 225, 245));
                        setCellBorders(secondCell, Color.FromArgb(209, 225, 245));
                        if(secondCellText.Equals(0) || secondCellText.Equals("0") || secondCellText.Equals(""))
                        {
                            firstCell.Style.BorderBottomColor = Color.Transparent;
                            secondCell.Style.BorderTopColor = Color.Transparent;
                            secondCell.Style.ForeColor = Color.Transparent;
                            Prev = item;
                        }else
                        {
                            secondCell.Style.ForeColor = Color.Black;
                            Prev = item;
                            break;
                        }

                        //if (firstCellText == secondCellText)
                        //{
                        //    firstCell.Style.BorderBottomColor = Color.Transparent;
                        //    secondCell.Style.BorderTopColor = Color.Transparent;
                        //    secondCell.Style.ForeColor = Color.Transparent;
                        //    Prev = item;
                        //}
                        //else
                        //{
                        //    secondCell.Style.ForeColor = Color.Black;
                        //    Prev = item;
                        //    break;
                        //}
                    }
                }
                else
                {
                    Prev = item;
                }
            }
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
           
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


        private void NewClick()
        {
           // radGridView1.ReadOnly = false;
           // radGridView1.AllowAddNewRow = false;
           // btnEdit.Enabled = false;
           //// btnView.Enabled = true;
           // radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            //radGridView1.ReadOnly = false;
            //btnEdit.Enabled = false;
            ////btnView.Enabled = true;
            //radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
          //  radGridView1.ReadOnly = true;
          ////  btnView.Enabled = false;
          //  btnEdit.Enabled = true;
          //  radGridView1.AllowAddNewRow = false;
           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการเพิ่มรายการ?", "เพิ่ม", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                    if (row >= 0)
                    {
                        int id = -1;
                        int.TryParse(radGridView1.Rows[row].Cells["id"].Value.ToString(), out id);
                        string ListNos = radGridView1.Rows[row].Cells["ListNo"].Value.ToString();
                        if (id >= 0)
                        {
                            ExportEditAdd ed = new ExportEditAdd(id, "Add", ListNos);
                                ed.ShowDialog();
                            LoadData();
                        }

                    }
                }
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการแก้ไข ?", "แก้ไข", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               if(row>=0)
                {
                    int id = -1;
                    int.TryParse(radGridView1.Rows[row].Cells["id"].Value.ToString(), out id);
                    string ListNos = radGridView1.Rows[row].Cells["ListNo"].Value.ToString();
                    if(id>=0)
                    {
                        ExportEditAdd ed = new ExportEditAdd(id,"Edit", ListNos);
                        ed.ShowDialog();
                        LoadData();
                    }

                }
            }

        }
        private void Saveclick()
        {
            //if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    AddUnit();
            //    DataLoad();
            //}
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex>=0 && radGridView1.Columns["PalletNo"].Index==e.ColumnIndex)
                {
                   // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    int PalletNo = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString(), out PalletNo);
                    if(id>0 && PalletNo>0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList(id, PalletNo.ToString(), 0,0, 0);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["ListNo"].Index == e.ColumnIndex)
                {
                    // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    int ListNo = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["ListNo"].Value.ToString(), out ListNo);
                    if (id > 0 && ListNo > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList(id, "", ListNo,0, 1);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["QtyOfPL"].Index == e.ColumnIndex)
                {
                    // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    int QtyOfPL = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["QtyOfPL"].Value.ToString(), out QtyOfPL);
                    if (id > 0 && QtyOfPL >= 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList(id, "",0, QtyOfPL, 2);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["GroupPDA"].Index == e.ColumnIndex)
                {
                    // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    int QtyOfPL = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    // int.TryParse(radGridView1.Rows[e.RowIndex].Cells["QtyOfPL"].Value.ToString(), out QtyOfPL);

                    string GroupPDA = radGridView1.Rows[e.RowIndex].Cells["GroupPDA"].Value.ToString();
                    if(GroupPDA.Equals("0"))
                    {
                        GroupPDA = "";
                        radGridView1.Rows[e.RowIndex].Cells["GroupPDA"].Value = "";
                    }

                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList(id, GroupPDA, 0, QtyOfPL, 999);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["GroupP"].Index == e.ColumnIndex)
                {
                    // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    int QtyOfPL = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    string Groupp = radGridView1.Rows[e.RowIndex].Cells["GroupP"].Value.ToString();
                    if(Groupp.Equals(""))
                    {
                        Groupp = "0";
                    }
                    // int.TryParse(radGridView1.Rows[e.RowIndex].Cells["QtyOfPL"].Value.ToString(), out QtyOfPL);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList(id, radGridView1.Rows[e.RowIndex].Cells["GroupP"].Value.ToString(), 0, QtyOfPL, 998);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["PrintType"].Index == e.ColumnIndex)
                {
                    // MessageBox.Show(radGridView1.Rows[e.RowIndex].Cells["PalletNo"].Value.ToString()+","+radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                    int id = 0;
                    int QtyOfPL = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    string PT = radGridView1.Rows[e.RowIndex].Cells["PrintType"].Value.ToString();
                    
                    // int.TryParse(radGridView1.Rows[e.RowIndex].Cells["QtyOfPL"].Value.ToString(), out QtyOfPL);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList(id, PT, 0, QtyOfPL, 997);
                        }
                    }
                }

                if (e.RowIndex >= 0 && radGridView1.Columns["ForNetWet"].Index == e.ColumnIndex)
                {
                  

                    int id = 0;                  
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    decimal PT = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["ForNetWet"].Value);                    
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {                            
                            db.sp_036_UpdateExportList_KLMP(id, "", PT, 984);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["K"].Index == e.ColumnIndex)
                {


                    int id = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);                   
                    string PT = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["K"].Value);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                          
                            db.sp_036_UpdateExportList_KLMP(id, PT, 0, 981);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["L"].Index == e.ColumnIndex)
                {


                    int id = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                   
                    string PT = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["L"].Value);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                           
                            db.sp_036_UpdateExportList_KLMP(id, PT, 0, 982);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["M"].Index == e.ColumnIndex)
                {


                    int id = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    string PT = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["M"].Value);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList_KLMP(id, PT, 0, 983);
                        }
                    }
                }
                if (e.RowIndex >= 0 && radGridView1.Columns["UnitCost"].Index == e.ColumnIndex)
                {


                    int id = 0;
                    int.TryParse(radGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    decimal UnitCost = Convert.ToDecimal(radGridView1.Rows[e.RowIndex].Cells["UnitCost"].Value);
                    if (id > 0)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            db.sp_036_UpdateExportList_KLMP(id, "", UnitCost, 986);
                        }
                    }
                }




            }
            catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //if (e.KeyData == (Keys.Control | Keys.S))
            //{
            //    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        AddUnit();
            //        DataLoad();
            //    }
            //}
            //else if (e.KeyData == (Keys.Control | Keys.N))
            //{
            //    if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        NewClick();
            //    }
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ck = 0;
            try
            {
                
                if (row >= 0)
                {
                    decimal ListNo = 0;
                    int id = -1;
                    decimal.TryParse(radGridView1.Rows[row].Cells["ListNo"].Value.ToString(), out ListNo);
                    int.TryParse(radGridView1.Rows[row].Cells["id"].Value.ToString(), out id);
                    if (MessageBox.Show("ต้องการลบรายการ List No. "+ ListNo +" หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                       
                        radGridView1.EndEdit();
                        //foreach (GridViewRowInfo rd in radGridView1.Rows)
                        //{
                        //if (Convert.ToBoolean(rd.Cells["S"].Value))
                        //{
                        
                                

                                this.Cursor = Cursors.WaitCursor;
                                using (DataClasses1DataContext db = new DataClasses1DataContext())
                                {
                                    tb_ExportDetail ed = db.tb_ExportDetails.Where(ex => ex.id == id && ex.SS != 3).FirstOrDefault();
                                    if (ed != null)
                                    {
                                        ck += 1;
                                        db.tb_ExportDetails.DeleteOnSubmit(ed);
                                        db.SubmitChanges();
                                        
                                    }
                                    dbClss.AddHistory("ExShipment", "ลบรายการ", "ลบรายการ  [" + ListNo + "] เข้าระบบ", "จากเลขที่ INV No. " + txtExportNo.Text);
                                }                              
                                
                            //}

                        //}

                        if(ck>0)
                            UpdateListNo();
                        LoadData();
                    }
                }
            }
            catch(Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;

            if(ck>0)
                 MessageBox.Show("Delete Successfully.");

        }
        private void UpdateListNo()
        {
            try
            {
               
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if(!txtExportNo.Text.Equals(""))
                    {
                        var ListA = db.tb_ExportDetails.Where(ee => ee.InvoiceNo == txtExportNo.Text).OrderBy(o=>o.ListNo).ToList();
                        int countA = 0;
                        foreach(var rd in ListA)
                        {
                            countA += 1;
                            tb_ExportDetail ed = db.tb_ExportDetails.Where(es => es.id == rd.id).FirstOrDefault();
                            if(ed!=null)
                            {
                                ed.ListNo = countA;
                                db.SubmitChanges();
                            }
                        }

                    }
                }
            }
            catch { }
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




        private void btnFilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExportNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                Inv = txtExportNo.Text;
                LoadData();
            }
        }

        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(e.RowElement.RowInfo.Cells["LConfirm"].Value))
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.GreenYellow;
                }
                else
                {
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                }
            }
            catch { }
        }

        private void chkALL_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                if (chkALL.Checked)
                {
                    foreach (var rd in radGridView1.Rows)
                    {
                        rd.Cells["S"].Value = true;

                    }
                }
                else
                {
                    foreach (var rd in radGridView1.Rows)
                    {
                        rd.Cells["S"].Value = false;

                    }
                }

                foreach (GridViewRowInfo rs in radGridView1.Rows)
                {
                    if (!Convert.ToBoolean(rs.Cells["LConfirm"].Value))
                    {
                        rs.Cells["S"].Value = false;
                    }
                }
            }
            catch { }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            try
            {
                radGridView1.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_014_CreateGroup_Dynamics(txtExportNo.Text);
                    db.sp_014_DeletePrintTAG(txtExportNo.Text);
                    string QRCode = "";
                    string Country = "";
                    string CountrySize = "";
                    tb_ExportList el = db.tb_ExportLists.Where(w => w.InvoiceNo == txtExportNo.Text).FirstOrDefault();
                    if (el != null)
                    {
                        CountrySize = el.CountrySize;
                        Country = el.Country;
                    }
                  

                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                       
                        if (Convert.ToBoolean(rd.Cells["S"].Value))
                        {
                           
                            tb_ExportDetail ed = db.tb_ExportDetails.Where(ee => ee.id == Convert.ToInt32(rd.Cells["id"].Value) && ee.PrintType=="A").FirstOrDefault();
                            if (ed != null)
                            {
                                DateTime SH = Convert.ToDateTime(ed.ShippingDate);
                                tb_ExportList exx = db.tb_ExportLists.Where(es => es.InvoiceNo == ed.InvoiceNo).FirstOrDefault();
                                if(exx!=null)
                                {
                                    SH = Convert.ToDateTime(exx.LoadDate);
                                }

                                //Order,PalletNo,Invoice,PartCode,Qty,ofTAG,TotalTAG,LotNo
                                QRCode = "";
                                QRCode = ed.OrderNo + "," + ed.PalletNo + "," + ed.InvoiceNo + ",";
                                QRCode = QRCode+ed.PartNo + "," + ed.Qty + "," + ed.ofPL.ToString() + "of" + ed.TotalPL.ToString() + "," + ed.LotNo.ToString();
                                byte[] barcode = dbClss.SaveQRCode2D(QRCode);
                                
                                tb_ExportPrintTAG ep = new tb_ExportPrintTAG();
                                ep.CustomerAddress = "5-1 Kanaya, Murayama, Yamagata, 995-0004 Japan";// ed.CustomerAddress.ToString();
                                //MessageBox.Show("OK");
                                ep.CustomerItemName = ""; //// ed.CustItem.ToString();                               
                                ep.CustomerItemNo = Convert.ToString(db.getItemCSTM_Dynamics(ed.PartNo,""));
                                ep.CustomerName = "Nabtesco Autmotive Corporation";// Convert.ToString(db.getItemCSTMName(ed.Customer));
                              
                                if (Country.ToUpper().Equals("INDIA"))
                                {                                   
                                    ep.CustomerName = "MINDA NABTESCO AUTOMOTIVE PVT LTD";
                                    ep.CustomerAddress = "Plot no-191 sector-8 IMT  Manesar ,distt- Gurgaon- 122050 State Haryana.";
                                }
                                ep.InvoiceNo = txtExportNo.Text;
                                ep.LOTNo = ed.LotNo;
                                ep.QRCode = barcode;
                                
                                ep.Qty = ed.Qty;
                                ep.GroupP = ed.GroupP;
                                ep.ShippingDate = SH;// ed.ShippingDate;
                                ep.TotalPLOf = Convert.ToInt32(ed.PalletNo);
                                ep.TotalPLOfQty = Convert.ToInt32(txtTotalPallet.Text);
                                ep.PLOfQty = Convert.ToInt32(ed.TotalPL);
                                ep.PLOf = Convert.ToInt32(ed.ofPL);
                                ep.PartCode = ed.PartNo;
                                ep.PartName = ed.PartName;
                                ep.Country = Country;
                                ep.CountrySize = CountrySize;
                              

                                db.tb_ExportPrintTAGs.InsertOnSubmit(ep);
                                db.SubmitChanges();
                            }

                        }
                    }

                    Report.Reportx1.WReport = "PrintEXTAG";
                    Report.Reportx1.Value = new string[1];
                    Report.Reportx1.Value[0] = txtExportNo.Text;
                    Report.Reportx1 op = new Report.Reportx1("ExInvoiceTAX.rpt");
                    op.Show();
                   
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                radGridView1.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_014_CreateGroup_Dynamics(txtExportNo.Text);
                    db.sp_014_DeletePrintTAG(txtExportNo.Text);
                    string QRCode = "";
                    string Country = "";
                    string CountrySize = "";
                    DateTime SH = DateTime.Now;// Convert.ToDateTime(ed.ShippingDate);
                   
                    
                    tb_ExportList el = db.tb_ExportLists.Where(w => w.InvoiceNo == txtExportNo.Text).FirstOrDefault();
                    if (el != null)
                    {
                        CountrySize = el.CountrySize;
                        Country = el.Country;
                        SH = Convert.ToDateTime(el.LoadDate);
                    }
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rd.Cells["S"].Value))
                        {
                            tb_ExportDetail ed = db.tb_ExportDetails.Where(ee => ee.id == Convert.ToInt32(rd.Cells["id"].Value) && ee.PrintType == "B").FirstOrDefault();
                            if (ed != null)
                            {
                                //Order,PalletNo,Invoice,PartCode,Qty,ofTAG,TotalTAG,LotNo

                                QRCode = "";
                                QRCode = ed.OrderNo+"," + ed.PalletNo + "," + ed.InvoiceNo + "," + ed.PartNo + "," + ed.Qty + "," + ed.ofPL.ToString() + "of" + ed.TotalPL.ToString() + "," + ed.LotNo.ToString();
                                byte[] barcode = dbClss.SaveQRCode2D(QRCode);
                                tb_ExportPrintTAG ep = new tb_ExportPrintTAG();
                                ep.CustomerAddress = "5-1 Kanaya, Murayama, Yamagata, 995-0004 Japan";
                                //ep.CustomerAddress = ed.CustomerAddress;//"5-1 Kanaya,Murayama,Yamagata,995-0004 Japan.";
                                ep.CustomerItemName = "";// ep.CustomerItemName;
                                ep.CustomerItemNo = Convert.ToString(db.getItemCSTM_Dynamics(ed.PartNo, "")); // ed.CustItem;                             
                               // ep.CustomerName = ed.CustomerName;// Convert.ToString(db.getItemCSTMName(ed.Customer));
                                ep.CustomerName = "Nabtesco Autmotive Corporation";// Convert.ToString(db.getItemCSTMName(ed.Customer));
                                ep.InvoiceNo = txtExportNo.Text;
                                ep.LOTNo = ed.LotNo;
                                ep.QRCode = barcode;
                                ep.Qty = ed.Qty;
                                ep.GroupP = ed.GroupP;
                                ep.ShippingDate = SH;// ed.ShippingDate;
                                ep.TotalPLOf = Convert.ToInt32(ed.PalletNo);
                                ep.TotalPLOfQty = Convert.ToInt32(txtTotalPallet.Text);
                                ep.PLOfQty = Convert.ToInt32(ed.TotalPL);
                                ep.PLOf = Convert.ToInt32(ed.ofPL);
                                ep.PartCode = ed.PartNo;
                                ep.PartName = ed.PartName;
                                ep.Country = Country;
                                ep.CountrySize = CountrySize;
                                if (Country.ToUpper().Equals("INDIA"))
                                {
                                    ep.CustomerName = "MINDA NABTESCO AUTOMOTIVE PVT LTD";
                                    ep.CustomerAddress = "Plot no-191 sector-8 IMT  Manesar ,distt- Gurgaon- 122050 State Haryana.";
                                }

                                db.tb_ExportPrintTAGs.InsertOnSubmit(ep);
                                db.SubmitChanges();
                            }

                        }
                    }

                    Report.Reportx1.WReport = "PrintEXTAG";
                    Report.Reportx1.Value = new string[1];
                    Report.Reportx1.Value[0] = txtExportNo.Text;
                    Report.Reportx1 op = new Report.Reportx1("ExInvoiceTAX2.rpt");
                    op.Show();
                }
            }
            catch { }
            this.Cursor = Cursors.Default ;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.sp_014_CreateGroup_Dynamics(txtExportNo.Text);
                MessageBox.Show("คำนวณเรียบร้อย!");
                LoadData();
            }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            //
            this.Cursor = Cursors.WaitCursor;
            try
            {
                radGridView1.EndEdit();
                //Print List
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_028_ExportPrintListTAG();
                    string QRCode = "";
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {
                        //if (Convert.ToBoolean(rd.Cells["S"].Value))
                        //{
                        //SaleOrder,PartNo,Invoice,LoadDate,PartName,Quantity,Shipdate
                        tb_ExportPrintListTAG Tag = new tb_ExportPrintListTAG();
                        Tag.InviceNo = txtExportNo.Text;
                        Tag.LoadDate = Convert.ToDateTime(rd.Cells["ShippingDate"].Value);
                        Tag.SaleOrder = Convert.ToString(rd.Cells["OrderNo"].Value);
                        Tag.PartNo = Convert.ToString(rd.Cells["PartNo"].Value);
                        Tag.PartName = Convert.ToString(rd.Cells["PartName"].Value);
                        Tag.Quantity = Convert.ToInt32(rd.Cells["Qty"].Value);
                        Tag.Period = Convert.ToString(rd.Cells["Period"].Value);
                        Tag.CSTM = Convert.ToString(rd.Cells["Customer"].Value);
                        Tag.ListNo = Convert.ToInt32(rd.Cells["ListNo"].Value);
                        Tag.PalletNo = Convert.ToInt32(rd.Cells["PalletNo"].Value);
                        Tag.ShippingDate = Convert.ToDateTime(rd.Cells["ShippingDate"].Value);
                        Tag.Status = Convert.ToString(rd.Cells["Status"].Value);
                        Tag.LotNo = Convert.ToString(rd.Cells["LotNo"].Value);
                        Tag.Packing = Convert.ToBoolean(rd.Cells["LConfirm"].Value);
                        //Create QR//
                        //ListNo,ExportNo,OrderNo,PartNo,CustItem,Qty,id
                        QRCode = "0" + "," + txtExportNo.Text + ",";
                        QRCode += Convert.ToString(rd.Cells["OrderNo"].Value) + "," + Convert.ToString(rd.Cells["PartNo"].Value) + ",";
                        QRCode += Convert.ToString(rd.Cells["CustItem"].Value) + "," + Convert.ToInt32(rd.Cells["Qty"].Value).ToString() + "," + Convert.ToInt32(rd.Cells["id"].Value).ToString();
                        byte[] barcode = dbClss.SaveQRCode2D(QRCode);
                        byte[] QrNo = dbClss.SaveQRCode2D(txtExportNo.Text);
                        Tag.QRNo = QrNo;
                        Tag.QRCode = barcode;
                        db.tb_ExportPrintListTAGs.InsertOnSubmit(Tag);
                        db.SubmitChanges();

                        //}
                    }

                }

                /////Print//////////
                //ExportForConfirm.rpt
                Report.Reportx1.Value = new string[1];
                Report.Reportx1.Value[0] = txtExportNo.Text;               
                Report.Reportx1.WReport = "ExportListRP";
                Report.Reportx1 op = new Report.Reportx1("ExportForConfirm.rpt");
                op.Show();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement7_Click(object sender, EventArgs e)
        {
            try
            {
                ExportTAGCheck et = new ExportTAGCheck("");
                et.Show();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButtonElement8_Click(object sender, EventArgs e)
        {
            ScanPDAList spl = new ScanPDAList("Export");
            spl.Show();
        }

        private void radButtonElement9_Click(object sender, EventArgs e)
        {
            int CASE = 0;
            try
            {
                foreach (GridViewRowInfo rd in radGridView1.Rows)
                {
                    if (!Convert.ToString(rd.Cells["K"].Value).Equals(""))
                    {
                        CASE += 1;
                    }
                }
                InvoiceEx_Update ivu = new InvoiceEx_Update(txtExportNo.Text,Convert.ToInt32(txtTotalPallet.Text),CASE);
                ivu.ShowDialog();
            }
            catch { }
        }

        private void radButtonElement11_Click(object sender, EventArgs e)
        {
            ExDN_localdelivery dn = new ExDN_localdelivery();
            dn.Show();
        }
    }
}
