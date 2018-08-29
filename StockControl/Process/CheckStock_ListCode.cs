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
    public partial class CheckStock_ListCode : Telerik.WinControls.UI.RadRibbonForm
    {

        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public CheckStock_ListCode(string CodeNox)
        {
            InitializeComponent();
            //CodeNo_tt = CodeNox;
            txtCheckNo.Text = CodeNox;
            screen = 1;
        }
        public CheckStock_ListCode()
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
           
            ddlLocation.Text = "";
            txtItemCount.Text = "0";
            txtCheckBy.Text = dbClss.UserID;
            //txtScanCode.Enabled = false;
            //txtCheckNo.Enabled = true;

            if(txtCheckNo.Text !="")
            {
                LoadData(txtCheckNo.Text);
            }

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
                txtCheckNo.Text = txtCheckNo.Text.ToUpper();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //var h = (from ix in db.tb_CheckStocks
                    //         where ix.CheckNo == txtCheckNo.Text.Trim()
                    //         && ix.Status == "Waiting Upload"
                    //         select ix).ToList();
                    //if (h.Count > 0)
                    //{
                    //    txtScanCode.Enabled = true;
                    //    txtCheckNo.Enabled = false;
                    //}
                    ////else
                    ////    MessageBox.Show("เลข Check No สถานะไม่ถูกต้อง !");

                    LoadData(txtCheckNo.Text);
                }
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(txtCheckNo.Text);
        }

        private void txtScanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if(e.KeyChar==13)
                {                    
                    string CodeNo = txtScanCode.Text;
                    //SP/PONo/Quantity/SPN/LotNo/TAGof/ItemNo
                    if (CodeNo != "" && txtCheckNo.Text !="" && ddlLocation.Text !="")
                    {
                        LoadCode(CodeNo);
                        
                    }
                    txtScanCode.Text = "";
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void LoadCode(string Code)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //SP/PONo/Quantity/SPN/LotNo/TAGof/ItemNo

                    string Status = "";
                    string CodeNo = Code;

                    //int index = Code.IndexOf('/');
                    //if(index>0)
                    //{
                    //    CodeNo = Code.Substring(0, index);
                    //}
                    //else
                    // CodeNo = Code;
                    string SP = "";
                    string PONo = "";
                    int SPN = 0;
                    string LotNo = "";
                    string TAGof = "";
                    string ItemName = "";
                    string Type = "" ;
                    decimal Quantity = 0;
                    int id = 0;
                    //int i = Code.IndexOf('/');
                    //if (i > 0)
                    //{
                    //    string d = Code.Substring(i + 1);
                    //    Quantity = dbClss.TDe(d);
                    //}
                    string phrase = Code;//"SP/PONo/Quantity/SPN/LotNo/TAGof/ItemNo";
                    string[] words = phrase.Split('/');
                    SP = dbClss.TSt(words[0]);
                    PONo = dbClss.TSt(words[1]);
                    Quantity = dbClss.TDe(words[2]);
                    SPN = dbClss.TInt(words[3]);
                    LotNo = dbClss.TSt(words[4]);
                    TAGof = dbClss.TSt(words[5]);
                    CodeNo = dbClss.TSt(words[6]);

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

                        InsertData(txtCheckNo.Text.ToUpper(), CodeNo.ToUpper(), ItemName, PONo, SP, Quantity, SPN, LotNo, TAGof, Type);
                        LoadData(txtCheckNo.Text);
                        //if (!duplicate(CodeNo))
                        //{
                        //    Add_Item((dgvData.Rows.Count() + 1).ToString(), Status, CodeNo, ItemName, Type, Quantity, id);
                        //    Count_Item();
                        //}
                    }
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void InsertData(string CheckNo,string Code,string ItemName,string PONo,string SP,decimal Quantity,int SPN,string LotNo,string TAGof,string Type)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_CheckStockTempChecks
                         where ix.CheckNo.Trim() == CheckNo
                         && ix.Status != "Cancel"
                         //&& ix.Status != "Completed"
                         && ix.Code == Code
                         select ix).ToList();
                if (g.Count > 0)  //มีรายการในระบบ
                {

                    var g1 = (from ix in db.tb_CheckStockTempChecks
                              where ix.CheckNo.Trim() == CheckNo
                             && ix.Status != "Cancel"
                             && ix.Status != "Completed"
                             && ix.Code == Code
                              select ix).ToList();
                    if (g1.Count > 0)
                    {
                        var gg = (from ix in db.tb_CheckStockTempChecks
                                  where ix.CheckNo.Trim() == CheckNo
                                 && ix.Status != "Cancel"
                                 && ix.Status != "Completed"
                                 && ix.Code == Code
                                 && ix.id == Convert.ToInt16(g1.FirstOrDefault().id)
                                  select ix).First();

                        gg.CreateBy = dbClss.UserID;
                        gg.CreateDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                        gg.CheckMachine = "";
                        gg.Location = ddlLocation.Text;
                        gg.CheckBy = txtCheckBy.Text;
                        gg.Code = Code;
                        gg.ItemName = ItemName;
                        gg.Quantity = Quantity;
                        gg.Status = "Waiting";
                        gg.Remark = "";
                        gg.Package = "";
                        gg.Type = Type;
                        gg.LotNo = LotNo;
                        gg.SNP = SPN;
                        gg.ofTAG = TAGof;
                        gg.RefNo = PONo;
                        gg.SP = SP;
                        db.SubmitChanges();

                    }
                }
                else
                {
                    tb_CheckStockTempCheck u = new tb_CheckStockTempCheck();
                    u.CheckNo = CheckNo;
                    u.CreateBy = dbClss.UserID;
                    u.CreateDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                    u.CheckMachine = "";
                    u.Location = ddlLocation.Text;
                    u.CheckBy = txtCheckBy.Text;
                    u.Code = Code;
                    u.ItemName = ItemName;
                    u.Quantity = Quantity;
                    u.Status = "Waiting";
                    u.Remark = "";
                    u.Package = "";
                    u.Type = Type;
                    u.LotNo = LotNo;
                    u.SNP = SPN;
                    u.ofTAG = TAGof;
                    u.RefNo = PONo;
                    u.SP = SP;
                    db.tb_CheckStockTempChecks.InsertOnSubmit(u);
                    db.SubmitChanges();
                }
            }
        }
        private void LoadData(string checkNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dgvData.Rows.Clear();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_CheckStockTempChecks
                             where //ix.CheckNo.Trim() == checkNo
                             ix.Code == checkNo
                             && ix.Status != "Cancel"                             
                             select ix).ToList();
                    if(g.Count>0)
                    {
                        txtCheckNo.Text = txtCheckNo.Text.ToUpper();
                        dgvData.DataSource = g;
                        int c = 0;
                        foreach (var x in dgvData.Rows)
                        {
                            c += 1;
                            x.Cells["No"].Value = c;
                        }
                            txtItemCount.Text = dgvData.Rows.Count().ToString();
                    }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
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
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            var g = (from ix in db.tb_CheckStockTempChecks
                                     where ix.CheckNo.Trim() == txtCheckNo.Text
                                     && ix.Status != "Cancel"
                                     && ix.Status != "Completed"
                                     && ix.Code == Code
                                     select ix).ToList();
                            if (g.Count > 0)  //มีรายการในระบบ
                            {
                                var gg = (from ix in db.tb_CheckStockTempChecks
                                          where ix.CheckNo.Trim() == txtCheckNo.Text
                                         && ix.Status != "Cancel"
                                         && ix.Status != "Completed"
                                         && ix.Code == Code
                                         && ix.id == Convert.ToInt16(g.FirstOrDefault().id)
                                          select ix).First();

                                gg.CreateBy = dbClss.UserID;
                                gg.CreateDate = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US"));
                                gg.Status = "Cancel";
                                db.SubmitChanges();

                                LoadData(txtCheckNo.Text);
                            }
                        }                            
                        //dgvData.CurrentRow.Delete();
                        // Count_Item();
                    }
                   
                    //int ck = 0;
                    //foreach (var x in dgvData.Rows)
                    //{
                    //    ck += 1;
                    //    x.Cells["No"].Value = ck;
                    //}
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

        private void txtCheckNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                btnLoad_Click(null, null);
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            txtCheckNo.Text = "";
            ddlLocation.Text = "";
            txtCheckBy.Text = dbClss.UserID;
            txtItemCount.Text = "";
            txtScanCode.Text = "";
            txtScanCode.Enabled = false;
            txtCheckNo.Enabled = true;
            dgvData.Rows.Clear();

        }
    }
}
