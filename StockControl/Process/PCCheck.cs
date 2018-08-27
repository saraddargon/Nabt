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
using System.Globalization;

namespace StockControl
{
    public partial class PCCheck : Telerik.WinControls.UI.RadRibbonForm
    {

        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public PCCheck(Telerik.WinControls.UI.RadTextBox CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public PCCheck()
        {
            InitializeComponent();
        }

        string PR1 = "";
        string PR2 = "";
        string Type = "";
        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

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
            //RMenu4.Click += RMenu4_Click;

            LoadDefault();
            txtCheckNo.Text = "";
            ddlLocation.Text = "";
            txtItemCount.Text = "0";
            txtCheckBy.Text = dbClss.UserID;
            txtScanCode.Enabled = false;
            txtCheckNo.Enabled = true;
        }
        private void LoadDefault()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                ddlLocation.DisplayMember = "Code";
                ddlLocation.ValueMember = "Code";
                ddlLocation.DataSource = db.tb_LocationlWHs.Where(s => s.Active == true).ToList();
                ddlLocation.Text = "";
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (txtCheckNo.Text != "")
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var h = (from ix in db.tb_CheckStocks
                             where ix.CheckNo == txtCheckNo.Text.Trim()
                             && ix.Status == "Waiting Upload"
                             select ix).ToList();
                    if (h.Count > 0)
                    {
                        txtScanCode.Enabled = true;
                        txtCheckNo.Enabled = false;
                    }
                    else
                        MessageBox.Show("เลข Check No สถานะไม่ถูกต้อง !");
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void txtScanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if(e.KeyChar==13)
                {                    
                    string CodeNo = txtScanCode.Text;                   
                    //0180100021001N/10
                    if (CodeNo != "")
                    {
                        LoadCode(CodeNo);
                        

                    }
                    txtScanCode.Text = "";
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void LoadCode(string Code)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //0180100021001N/10

                string Status = "";
                string CodeNo = Code;

                int index = Code.IndexOf('/');
                if(index>0)
                {
                    CodeNo = Code.Substring(0, index);
                }
                else
                 CodeNo = Code;

                string ItemName = "";
                string Type = "";
                decimal Quantity = 0;
                int id = 0;
                string Location = "";

                int i = Code.IndexOf('/');
                if (i > 0)
                {
                    string d = Code.Substring(i + 1);
                    Quantity = dbClss.TDe(d);
                }

                var h = (from ix in db.tb_CheckStockLists
                         where ix.CheckNo == txtCheckNo.Text.Trim()
                         //&& ix.Location == ddlLocation.Text
                         && ix.Code == CodeNo
                         //&& ix.Status == "Waiting"
                         select ix).ToList();
                if (h.Count > 0)
                {
                    ItemName = dbClss.TSt(h.FirstOrDefault().PartName);
                    Type = dbClss.TSt(h.FirstOrDefault().Type);
                    id = dbClss.TInt(h.FirstOrDefault().id);
                    Location = dbClss.TSt(h.FirstOrDefault().Location);
                    if (!duplicate(CodeNo))
                    {
                        Add_Item((dgvData.Rows.Count() + 1).ToString(), Status, CodeNo, ItemName, Type, Quantity, id, Location);
                        Count_Item();
                    }
                }
            }
        }
        private bool duplicate(string Code)
        {
            bool re = false;
            foreach (var rd1 in dgvData.Rows)
            {
                if (StockControl.dbClss.TSt(rd1.Cells["Code"].Value).Equals(Code))
                    re = true;
            }
            
            return re;
        }
        private void Add_Item(string No, string Status, string CodeNo, string ItemName
           , string Type, decimal Quantity,int id,string Location)
        {
            try
            {
                int rowindex = -1;
                GridViewRowInfo ee;
                if (rowindex == -1)
                {
                    ee = dgvData.Rows.AddNew();
                }
                else
                    ee = dgvData.Rows[rowindex];

                ee.Cells["No"].Value = No.ToString();
                ee.Cells["Status"].Value = Status;
                ee.Cells["Code"].Value = CodeNo;
                ee.Cells["ItemName"].Value = ItemName;
                ee.Cells["Type"].Value = Type;
                ee.Cells["Quantity"].Value = Quantity;
                ee.Cells["id"].Value = id;
                 ee.Cells["Location"].Value = Location;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError(this.Name, ex.Message + " : Add_Item", this.Name); }

        }

        private void deletePartNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvData.Rows.Count > 0)
                {
                    string Code = dbClss.TSt(dgvData.CurrentRow.Cells["Code"].Value);
                    if (MessageBox.Show("ต้องการลบรายการ "+ Code +" ?", "ลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvData.CurrentRow.Delete();
                         Count_Item();
                    }

                    int ck = 0;
                    foreach (var x in dgvData.Rows)
                    {
                        ck += 1;
                        x.Cells["No"].Value = ck;
                    }
                }

            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void Count_Item()
        {
             txtItemCount.Text =  dgvData.Rows.Count().ToString();
            
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCheckNo.Text == "")
                {
                    MessageBox.Show("Check No. is null !");
                    return;
                }
                if (ddlLocation.Text =="")
                {
                    MessageBox.Show("Location is null !");
                    return;
                }
                if (txtCheckBy.Text == "")
                {
                    MessageBox.Show("Check By is null !");
                    return;
                }

                if (MessageBox.Show("ต้องการบันทึกรายการ ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    int c = 0;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        foreach (var g in dgvData.Rows)
                        {
                            if (dbClss.TSt(g.Cells["Code"].Value) != "")
                            {
                                c += 1;
                                var h = (from ix in db.tb_CheckStockTempChecks
                                         where ix.CheckNo == txtCheckNo.Text.Trim()
                                         && ix.Code == dbClss.TSt(g.Cells["Code"].Value)
                                         && ix.Location == ddlLocation.Text
                                         //&& ix.CheckMachine == ""
                                         && ix.Status != "Cancel"
                                         select ix).ToList();
                                if (h.Count > 0)
                                {
                                    var hh = (from ix in db.tb_CheckStockTempChecks
                                              where ix.CheckNo == txtCheckNo.Text.Trim()
                                              && ix.Location == ddlLocation.Text
                                              && ix.Code == dbClss.TSt(g.Cells["Code"].Value)
                                               //&& ix.CheckMachine == ""
                                               && ix.Status != "Cancel"
                                              select ix).First();
                                    //unit1.Status = "";
                                    hh.CreateDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                                    hh.CheckBy = dbClss.UserID;
                                    //hh.Status = "Waiting Check";
                                    hh.Location = ddlLocation.Text;
                                    hh.CheckBy = txtCheckBy.Text;
                                    hh.ItemName = dbClss.TSt(g.Cells["ItemName"].Value);
                                    hh.Quantity = dbClss.TDe(g.Cells["Quantity"].Value);
                                    hh.Remark = "";
                                    hh.Package = "";
                                    hh.Type = dbClss.TSt(g.Cells["Type"].Value);

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "PCCheck [" + hh.Code + "]", hh.CheckNo);
                                }
                                else
                                {

                                    tb_CheckStockTempCheck u = new tb_CheckStockTempCheck();
                                    u.CheckNo = txtCheckNo.Text.Trim();
                                    u.Status = "Waiting";
                                    u.CheckMachine = "";
                                    u.Location = ddlLocation.Text;
                                    u.CheckBy = txtCheckBy.Text;
                                    u.CreateDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                                    u.CreateBy = dbClss.UserID;
                                    u.Code = dbClss.TSt(g.Cells["Code"].Value);
                                    u.ItemName = dbClss.TSt(g.Cells["ItemName"].Value);
                                    u.Quantity = dbClss.TDe(g.Cells["Quantity"].Value);
                                    u.Remark = "";
                                    u.Package = "";
                                    u.Type = dbClss.TSt(g.Cells["Type"].Value);

                                    db.tb_CheckStockTempChecks.InsertOnSubmit(u);
                                    db.SubmitChanges();

                                    dbClss.AddHistory(this.Name, "เพิ่ม", "PCCheck [" + u.Code + "]", u.CheckNo);
                                }
                            }
                        }
                    }
                    if (c > 0)
                        MessageBox.Show("Import data Complete.");
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
            }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            dbClss.ExportGridXlSX(dgvData);
        }
    }
}
