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
    public partial class ClearTempList : Telerik.WinControls.UI.RadRibbonForm
    {
        public ClearTempList()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.RadTextBox RCNo_tt = new Telerik.WinControls.UI.RadTextBox();
       // Telerik.WinControls.UI.RadTextBox PRNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ClearTempList(Telerik.WinControls.UI.RadTextBox RCNoxxx
                 
                )
        {
            InitializeComponent();
            RCNo_tt = RCNoxxx;
           
            screen = 1;
        }

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
            cboStatus.Text = "ทั้งหมด";
            dgvData.AutoGenerateColumns = false;
            GETDTRow();
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
        private void Load_WaitingReceive()  //รอรับเข้า (รอ Receive)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string VendorNo_ss = "";
                if (!cboVendorName.Text.Equals(""))
                    VendorNo_ss = txtVendorNo.Text;

                int dgvNo = 0;
                bool S = false; 
                //string RCNo = "";
                //string PRNo = "";
                //string CodeNo = "";
                //string ItemName = "";
                //string ItemNo = "";
                //string ItemDescription = "";
                //DateTime? DeliveryDate = null;
                //decimal QTY = 0;
                //decimal BackOrder = 0;
                //decimal RemainQty = 0;
                //string Unit = "";
                //decimal PCSUnit = 0;
                //decimal Leadtime = 0;
                //decimal MaxStock = 0;
                //decimal MinStock = 0;
                //string VendorNo = "";
                //string VendorName = "";
                //DateTime? CreateDate = null;
                //string CreateBy = "";
                //string Status = "รอรับเข้า";


                //var g = (from ix in db.tb_PurchaseRequests select ix).Where(a => a.VendorNo.Contains(VendorNo_ss)
                //    //&& a.Status != "Cancel"
                //    && a.Status == "Waiting"
                //    )
                //    .ToList();
                //if (g.Count() > 0)
                //{

                    var r = (from h in db.tb_PurchaseRequests
                             join d in db.tb_PurchaseRequestLines on h.PRNo equals d.PRNo
                             join i in db.tb_Items on d.CodeNo equals i.CodeNo

                             where //h.Status == "Waiting" //&& d.verticalID == VerticalID
                                Convert.ToDecimal(d.OrderQty ) == Convert.ToDecimal(d.RemainQty)
                                && h.VendorNo.Contains(VendorNo_ss)
                             select new
                             {
                                 CodeNo = d.CodeNo,
                                 S = false,
                                 ItemNo = d.ItemName,
                                 ItemDescription = d.ItemDesc,
                                 RCNo = "",
                                 PRNo = d.PRNo,
                                 DeliveryDate = d.DeliveryDate,
                                 QTY = d.OrderQty,
                                 BackOrder = d.RemainQty,
                                 RemainQty = d.RemainQty,
                                 Unit = d.UnitCode,
                                 PCSUnit = d.PCSUnit,
                                 MaxStock = i.MaximumStock,
                                 MinStock = i.MinimumStock,
                                 VendorNo = h.VendorNo,
                                 VendorName = h.VendorName,
                                 CreateBy = h.CreateBy,
                                 CreateDate = h.CreateDate,
                                 Status = "รอรับเข้า"
                             }
               ).ToList();
                    if (r.Count > 0)
                    {
                        dgvNo = dgvData.Rows.Count() + 1;

                        foreach (var vv in r)
                        {
                            dgvData.Rows.Add(dgvNo.ToString(), S,"", vv.RCNo, vv.PRNo, vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                        , vv.DeliveryDate, vv.QTY, vv.BackOrder, vv.RemainQty, vv.Unit, vv.PCSUnit, vv.MaxStock,
                                        vv.MinStock, vv.VendorNo, vv.VendorName, vv.CreateBy, vv.CreateDate, vv.Status
                                        );
                        }

                    }
                    //var gg = (from ix in db.tb_PurchaseRequestLines select ix)
                    //    .Where(a => a.SS.Equals(true) && (a.PRNo==(StockControl.dbClss.TSt(g.FirstOrDefault().PRNo)))
                    //   && a.OrderQty == a.RemainQty
                    //   && a.OrderQty >0
                    //).ToList();
                    //if (gg.Count() > 0)
                    //{
                    //    foreach (var vv in gg)
                    //    {
                    //        if (!StockControl.dbClss.TSt(vv.DeliveryDate).Equals(""))
                    //            DeliveryDate = Convert.ToDateTime(vv.DeliveryDate);

                    //        decimal.TryParse(StockControl.dbClss.TSt(vv.OrderQty), out QTY);
                    //        decimal.TryParse(StockControl.dbClss.TSt(vv.RemainQty), out BackOrder);
                    //        decimal.TryParse(StockControl.dbClss.TSt(vv.RemainQty), out RemainQty);

                    //        dgvNo = dgvData.Rows.Count() + 1;
                    //        dgvData.Rows.Add(dgvNo.ToString(), S, RCNo,vv.PRNo,vv.CodeNo,vv.ItemName,vv.ItemDesc
                    //            , DeliveryDate, QTY, BackOrder, RemainQty);
                    //    }
                    //}
                //}
            }
        }
        private void Load_PratitalReceive() //รับเข้าบางส่วน
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string VendorNo_ss = "";
                if (!cboVendorName.Text.Equals(""))
                    VendorNo_ss = txtVendorNo.Text;

                int dgvNo = 0;
                bool S = false;
                string RCNo = "";
                //string PRNo = "";
                //string CodeNo = "";
                //string ItemName = "";
                //string ItemNo = "";
                //string ItemDescription = "";
                //DateTime? DeliveryDate = null;
                //decimal QTY = 0;
                //decimal BackOrder = 0;
                //decimal RemainQty = 0;
                //string Unit = "";
                //decimal PCSUnit = 0;
                //decimal Leadtime = 0;
                //decimal MaxStock = 0;
                //decimal MinStock = 0;
                //string VendorNo = "";
                //string VendorName = "";
                //DateTime? CreateDate = null;
                //string CreateBy = "";
                //string Status = "รอรับเข้า";

                var r = (from d in db.tb_Receives
                         join c in db.tb_ReceiveHs on d.RCNo equals c.RCNo
                         join p in db.tb_PurchaseRequestLines on d.PRID equals p.id
                         join i in db.tb_Items on d.CodeNo equals i.CodeNo

                         where d.Status == "Partial" && c.VendorNo.Contains(VendorNo_ss)
                         select new
                         {
                             CodeNo = d.CodeNo,
                             S = false,
                             ItemNo = d.ItemNo,
                             ItemDescription = d.ItemDescription,
                             RCNo = d.RCNo,
                             PRNo = d.PRNo,
                             DeliveryDate = p.DeliveryDate,
                             QTY = d.QTY,
                             BackOrder = d.RemainQty,
                             RemainQty = d.RemainQty,
                             Unit = d.Unit,
                             PCSUnit = d.PCSUnit,
                             MaxStock = i.MaximumStock
                             ,MinStock = i.MinimumStock
                            , VendorNo = c.VendorNo
                            ,VendorName = c.VendorName
                            ,CreateBy = d.CreateBy
                            ,CreateDate = d.RCDate
                            ,Status = "รับเข้าบางส่วน"//d.Status
                         }
                ).ToList();
                //dgvData.DataSource = StockControl.dbClss.LINQToDataTable(r);
                if(r.Count > 0)
                {
                    dgvNo = dgvData.Rows.Count() + 1;

                    foreach (var vv in r)
                    {
                        dgvData.Rows.Add(dgvNo.ToString(), S,"", vv.RCNo, vv.PRNo, vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                    , vv.DeliveryDate, vv.QTY, vv.BackOrder, vv.RemainQty,vv.Unit,vv.PCSUnit,vv.MaxStock,
                                    vv.MinStock,vv.VendorNo,vv.VendorName,vv.CreateBy,vv.CreateDate,vv.Status
                                    );
                    }

                }

                //int rowcount = 0;
                //foreach (var x in dgvData.Rows)
                //{
                //    rowcount += 1;
                //    x.Cells["dgvNo"].Value = rowcount;
                //}


            }
        }
        private void Load_CompletedReceive()//รับเข้าแล้ว
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string VendorNo_ss = "";
                if (!cboVendorName.Text.Equals(""))
                    VendorNo_ss = txtVendorNo.Text;

                int dgvNo = 0;
                bool S = false;
                string RCNo = "";
                //string PRNo = "";
                //string CodeNo = "";
                //string ItemName = "";
                //string ItemNo = "";
                //string ItemDescription = "";
                //DateTime? DeliveryDate = null;
                //decimal QTY = 0;
                //decimal BackOrder = 0;
                //decimal RemainQty = 0;
                //string Unit = "";
                //decimal PCSUnit = 0;
                //decimal Leadtime = 0;
                //decimal MaxStock = 0;
                //decimal MinStock = 0;
                //string VendorNo = "";
                //string VendorName = "";
                //DateTime? CreateDate = null;
                //string CreateBy = "";
                //string Status = "รับเข้าแล้ว";

                var r = (from d in db.tb_Receives
                         join c in db.tb_ReceiveHs on d.RCNo equals c.RCNo
                         join p in db.tb_PurchaseRequestLines on d.PRID equals p.id
                         join i in db.tb_Items on d.CodeNo equals i.CodeNo

                         where d.Status == "Completed" && c.VendorNo.Contains(VendorNo_ss)
                         select new
                         {
                             CodeNo = d.CodeNo,
                             S = false,
                             ItemNo = d.ItemNo,
                             ItemDescription = d.ItemDescription,
                             RCNo = d.RCNo,
                             PRNo = d.PRNo,
                             DeliveryDate = p.DeliveryDate,
                             QTY = d.QTY,
                             BackOrder = d.RemainQty,
                             RemainQty = d.RemainQty,
                             Unit = d.Unit,
                             PCSUnit = d.PCSUnit,
                             MaxStock = i.MaximumStock
                             ,
                             MinStock = i.MinimumStock
                            ,
                             VendorNo = c.VendorNo
                            ,
                             VendorName = c.VendorName
                            ,
                             CreateBy = d.CreateBy
                            ,
                             CreateDate = d.RCDate
                            ,
                             Status = "รับเข้าแล้ว"//d.Status
                         }
                ).ToList();
                //dgvData.DataSource = StockControl.dbClss.LINQToDataTable(r);
                if (r.Count > 0)
                {
                    dgvNo = dgvData.Rows.Count() + 1;

                    foreach (var vv in r)
                    {
                        dgvData.Rows.Add(dgvNo.ToString(), S,"", vv.RCNo, vv.PRNo, vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                    , vv.DeliveryDate, vv.QTY, vv.BackOrder, vv.RemainQty, vv.Unit, vv.PCSUnit, vv.MaxStock,
                                    vv.MinStock, vv.VendorNo, vv.VendorName, vv.CreateBy, vv.CreateDate, vv.Status
                                    );
                    }

                }

                //int rowcount = 0;
                //foreach (var x in dgvData.Rows)
                //{
                //    rowcount += 1;
                //    x.Cells["dgvNo"].Value = rowcount;
                //}

            }
        }
        private void DataLoad()
        {
            //dt.Rows.Clear();
            
            try
            {

                this.Cursor = Cursors.WaitCursor;
                dgvData.Rows.Clear();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    
                    try
                    {
                        //if (cboStatus.Text.Equals("รอรับเข้า"))
                        //    Load_WaitingReceive();
                        // if (cboStatus.Text.Equals("รับเข้าบางส่วน"))
                        //    Load_PratitalReceive();
                        //else if (cboStatus.Text.Equals("รับเข้าแล้ว"))
                        //    Load_CompletedReceive();
                        //else
                        //{
                        //Load_WaitingReceive();
                        //Load_PratitalReceive();
                        //    Load_CompletedReceive();
                        //}
                        string VendorNo_ss = "";
                        if (!cboVendorName.Text.Equals(""))
                            VendorNo_ss = txtVendorNo.Text;
                        int dgvNo = 0;
                        bool S = false;
                        DateTime inclusiveStart = dtDate1.Value.Date;
                        // Include the *whole* of the day indicated by searchEndDate
                        DateTime exclusiveEnd = dtDate2.Value.Date.AddDays(1);


                        var r = (from d in db.tb_ReceiveHs
                                     //join c in db.tb_ReceiveHs on d.RCNo equals c.RCNo
                                     //join p in db.tb_PurchaseRequestLines on d.PRID equals p.id
                                     //join i in db.tb_Items on d.CodeNo equals i.CodeNo

                                 where d.VendorNo.Contains(VendorNo_ss)
                                    && d.Flag_Temp == true && d.TempNo.Contains(txtDLNo.Text)
                                    && d.Status != "Cancel"
                                    && (d.CreateDate >= inclusiveStart
                                        && d.CreateDate < exclusiveEnd)
                                 select new
                                 {
                                    
                                     RCNo = d.RCNo,
                                     VendorNo = d.VendorNo
                                    ,
                                     VendorName = d.VendorName
                                     ,RemarkHD = d.RemarkHD
                                     ,
                                     TempNo = d.TempNo
                                     ,
                                     RCDate = d.RCDate
                                    ,
                                     CreateBy = d.CreateBy
                                    ,
                                     CreateDate = d.CreateDate
                                    ,
                                     Status = d.Status
                                 }
                ).ToList();
                        //dgvData.DataSource = StockControl.dbClss.LINQToDataTable(r);
                        if (r.Count > 0)
                        {
                            dgvNo = dgvData.Rows.Count() + 1;
                            string status = "";
                            foreach (var vv in r)
                            {
                                if (vv.Status.Equals("Partial"))
                                    status = "รับเข้าบางส่วน";
                                else
                                    status = "รับเข้าครบแล้ว";

                                dgvData.Rows.Add(dgvNo.ToString(),vv.RCNo,vv.TempNo,vv.VendorNo,vv.VendorName,vv.RemarkHD
                                    ,vv.CreateBy,vv.RCDate,vv.CreateDate, status
                                            );
                            }

                        }

                        //int rowcount = 0;
                        //foreach (var x in dgvData.Rows)
                        //{
                        //    rowcount += 1;
                        //    x.Cells["dgvNo"].Value = rowcount;
                            
                        //}
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
            btnPrint.Enabled = true;
            dgvData.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (screen.Equals(1))
                {
                    if (!Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value).Equals(""))
                    {
                        RCNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value);
                        this.Close();
                    }
                }
                else
                {
                    ClearTemp a = new ClearTemp(Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value));
                    a.ShowDialog();

                }

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

    

        private void dgvData_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try

            {
                if (screen.Equals(1))
                {
                    RCNo_tt.Text = Convert.ToString(e.Row.Cells["RCNo"].Value);
                    this.Close();
                }
                else

                {
                    ClearTemp a = new ClearTemp(Convert.ToString(e.Row.Cells["RCNo"].Value));
                    a.ShowDialog();
                }
                
            }
            catch { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

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


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
