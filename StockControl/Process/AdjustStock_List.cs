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
    public partial class AdjustStock_List : Telerik.WinControls.UI.RadRibbonForm
    {
        public AdjustStock_List()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.RadTextBox ADNo_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public AdjustStock_List(Telerik.WinControls.UI.RadTextBox ADNoxxx
                    , Telerik.WinControls.UI.RadTextBox CodeNoxxx
                )
        {
            InitializeComponent();
            ADNo_tt = ADNoxxx;
            CodeNo_tt = CodeNoxxx;
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
           
            dgvData.AutoGenerateColumns = false;
            GETDTRow();
            //DefaultItem();
            //dgvData.ReadOnly = false;
            DataLoad();
            //txtVendorNo.Text = "";
            
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //cboVendorName.AutoCompleteMode = AutoCompleteMode.Append;
                //cboVendorName.DisplayMember = "VendorName";
                //cboVendorName.ValueMember = "VendorNo";
                //cboVendorName.DataSource =(from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo,ix.VendorName}).ToList();
                //cboVendorName.SelectedIndex = -1;
                //cboVendorName.SelectedValue = "";
                
            }
        }
        private void Load_Adjust()  
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
              
                int dgvNo = 0;
                bool S = false;
                DateTime inclusiveStart = dtDate1.Value.Date;
                // Include the *whole* of the day indicated by searchEndDate
                DateTime exclusiveEnd = dtDate2.Value.Date.AddDays(1);


                var r = (from h in db.tb_StockAdjusts
                             join d in db.tb_StockAdjustHs on h.AdjustNo equals d.ADNo
                             join i in db.tb_Items on h.CodeNo equals i.CodeNo

                             where //h.Status == "Waiting" //&& d.verticalID == VerticalID
                               
                                 h.AdjustNo.Contains(txtADNo.Text)
                                   && (h.CreateDate >= inclusiveStart
                                 && h.CreateDate < exclusiveEnd)

                             select new
                             {
                                 CodeNo = h.CodeNo,
                                 S = false,
                                 ItemNo = h.ItemNo,
                                 ItemDescription = h.ItemDescription,
                                
                                 QTY = h.Qty,
                                 Unit = h.Unit,
                                 PCSUnit = h.PCSUnit,
                                 VendorNo = i.VendorNo,
                                 VendorName = i.VendorItemName,
                                 CreateBy = h.CreateBy,
                                 CreateDate = h.CreateDate,
                                 LotNo =  h.LotNo,
                                 Reason = h.Reason,
                                 Status = i.Status,
                                 ADNo = d.ADNo
                             }
               ).ToList();
                    if (r.Count > 0)
                    {
                        dgvNo = dgvData.Rows.Count() + 1;

                    foreach (var vv in r)
                    {
                        dgvData.Rows.Add(dgvNo.ToString()
                                        , false,
                                        vv.ADNo,
                                        vv.CodeNo,
                                        vv.ItemNo,
                                        vv.ItemDescription,
                                        vv.QTY,
                                        vv.Unit,
                                        vv.PCSUnit,
                                        vv.VendorNo,
                                        vv.VendorName,
                                        vv.CreateBy,
                                        vv.CreateDate,
                                        vv.LotNo,
                                        vv.Reason,
                                        vv.Status);
                    }
                        

                    }
                    
                
            }
        }
       
        private void DataLoad()
        {
           dgvData.Rows.Clear();
            
            try
            {

                this.Cursor = Cursors.WaitCursor;
                dgvData.Rows.Clear();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    
                    try
                    {
                        Load_Adjust();
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
                    if (!Convert.ToString(dgvData.CurrentRow.Cells["ADNo"].Value).Equals(""))
                    {
                        ADNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["ADNo"].Value);
                        this.Close();
                    }
                    else
                    {
                        ADNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["ADNo"].Value);
                        CodeNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["CodeNo"].Value);
                        this.Close();
                    }
                }
                else
                {
                    AdjustStock a = new AdjustStock(Convert.ToString(dgvData.CurrentRow.Cells["ADNo"].Value),
                        Convert.ToString(dgvData.CurrentRow.Cells["CodeNo"].Value));
                    a.ShowDialog();
                    this.Close();
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
            //if (!cboVendorName.Text.Equals(""))
            //    txtADNo.Text = cboVendorName.SelectedValue.ToString();
            //else
            //    txtADNo.Text = "";
        }

        private void MasterTemplate_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (screen.Equals(1))
            {
                if (!Convert.ToString(e.Row.Cells["ADNo"].Value).Equals(""))
                {
                    ADNo_tt.Text = Convert.ToString(e.Row.Cells["ADNo"].Value);
                    this.Close();
                }
                else
                {
                    ADNo_tt.Text = Convert.ToString(e.Row.Cells["ADNo"].Value);
                    CodeNo_tt.Text = Convert.ToString(e.Row.Cells["CodeNo"].Value);
                    this.Close();
                }
            }
            else
            {
                AdjustStock a = new AdjustStock(Convert.ToString(e.Row.Cells["ADNo"].Value),
                    Convert.ToString(e.Row.Cells["CodeNo"].Value));
                a.ShowDialog();
                this.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string AdNo1 = "";
                string AdNo2 = "";

                if (dgvData.Rows.Count > 0)
                {
                    AdNo1 = Convert.ToString(dgvData.CurrentRow.Cells["ADNo"].Value);

                    AdNo2 = Convert.ToString(dgvData.CurrentRow.Cells["ADNo"].Value);
                }
                PrintPR a = new PrintPR(AdNo1, AdNo2, "AdjustStock");
                a.ShowDialog();

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    var g = (from ix in db.sp_R004_ReportShipping(txtSHNo.Text, DateTime.Now) select ix).ToList();
                //    if (g.Count() > 0)
                //    {

                //        Report.Reportx1.Value = new string[2];
                //        Report.Reportx1.Value[0] = txtSHNo.Text;
                //        Report.Reportx1.WReport = "ReportShipping";
                //        Report.Reportx1 op = new Report.Reportx1("ReportShipping.rpt");
                //        op.Show();

                //    }
                //    else
                //        MessageBox.Show("not found.");
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
