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
            }catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

                    if (!duplicate(CodeNo))
                    {
                        Add_Item((dgvData.Rows.Count() + 1).ToString(), Status, CodeNo, ItemName, Type, Quantity, id);
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
           , string Type, decimal Quantity,int id)
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

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            dbClss.ExportGridXlSX(dgvData);
        }
    }
}
