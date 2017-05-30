using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
namespace StockControl
{
    public partial class Contact : Telerik.WinControls.UI.RadRibbonForm
    {
        public Contact(string VNDRNo,string VendorName)
        {
            InitializeComponent();
            VNDR = VNDRNo;
            VNDRName = VendorName;
        }

        //private int RowView = 50;
        //private int ColView = 10;
        private string VNDR = "";
        private string VNDRName = "";
        private int Cath01 = 9;
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
            dt.Columns.Add(new DataColumn("DefaultNo", typeof(bool)));
            dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ContactName", typeof(string)));
            dt.Columns.Add(new DataColumn("Tel", typeof(string)));
            dt.Columns.Add(new DataColumn("Fax", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));

        }
        private void Unit_Load(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            GETDTRow();
          
            LoadDefault();
            cboVendor.Text = VNDR;
            txtVenderName.Text = VNDRName;
            Cath01 = 9;
            DataLoad();
        }

        private void LoadDefault()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboVendor.DisplayMember = "VendorNo";
                cboVendor.ValueMember = "VendorName";
                cboVendor.DataSource = db.tb_Vendors.Where(s => s.Active == true).ToList();
            }
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
              
                radGridView1.DataSource = db.tb_VendorContacts.Where(s=>s.VendorNo==VNDR).ToList();
                foreach(var x in radGridView1.Rows)
                {
                   // x.Cells["dgvCodeTemp"].Value = x.Cells["VendorNo"].Value.ToString();
                    x.Cells["VendorNo"].ReadOnly = true;
                }
               
            }
            Cath01 = 0;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_GroupTypes where ix.GroupCode == code select ix).Count();
                if (i > 0)
                    ck = false;
                else
                    ck = true;
            }
            return ck;
        }

        private bool AddUnit()
        {
            bool ck = false;
            int C = 0;
            try
            {


                radGridView1.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in radGridView1.Rows)
                    {
                        if (!Convert.ToString(cboVendor.Text.Trim()).Equals(""))
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                               
                                if (Convert.ToString(g.Cells["VendorNo"].Value).Equals(""))
                                {
                                    // MessageBox.Show("11");


                                    tb_VendorContact gy = new tb_VendorContact();
                                    gy.id = 0;
                                    gy.VendorNo = cboVendor.Text.Trim();
                                    gy.ContactName = Convert.ToString(g.Cells["ContactName"].Value);
                                    gy.DefaultNo = Convert.ToBoolean(g.Cells["DefaultNo"].Value);
                                    gy.Tel= Convert.ToString(g.Cells["Tel"].Value);
                                    gy.Fax = Convert.ToString(g.Cells["Fax"].Value);
                                    gy.Mobile = Convert.ToString(g.Cells["Tel"].Value);
                                    gy.Email = Convert.ToString(g.Cells["Email"].Value);
                                    db.tb_VendorContacts.InsertOnSubmit(gy);
                                    db.SubmitChanges();
                                    C += 1;
                                    dbClss.AddHistory(this.Name, "เพิ่มผู้ติดต่อ", "เพิ่มรายชื่อ [" +cboVendor.Text+ ","+ gy.ContactName+ "]","");
                                }
                                else
                                {
                                   
                                    var unit1 = (from ix in db.tb_VendorContacts
                                                 where ix.id == Convert.ToInt32(g.Cells["id"].Value)
                                                 select ix).First();
                                       unit1.ContactName = Convert.ToString(g.Cells["ContactName"].Value);
                                    unit1.Tel = Convert.ToString(g.Cells["Tel"].Value);
                                    unit1.Mobile = Convert.ToString(g.Cells["Tel"].Value);
                                    unit1.Fax = Convert.ToString(g.Cells["Fax"].Value);
                                    unit1.Email = Convert.ToString(g.Cells["Email"].Value);
                                    unit1.DefaultNo = Convert.ToBoolean(g.Cells["DefaultNo"].Value);
                                    
                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไขผู้ติดต่อ", "รายชื่อผู้ติดต่อ [" + cboVendor.Text + "," + unit1.ContactName+ "]","");

                                }
                            }
                        }else
                        {
                            MessageBox.Show("กรุณาเลือก รหัสผู้ขายก่อน !");
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("เพิ่มแก้ไข ผู้ติดต่อ", ex.Message, this.Name);
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;
         
            int C = 0;
            try
            {

                if (row >= 0)
                {
                    string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["ContactName"].Value);
                    string id = Convert.ToString(radGridView1.Rows[row].Cells["id"].Value);
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals(""))
                            {
                                if (!id.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_VendorContacts
                                                 where ix.id == Convert.ToInt32(id)
                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_VendorContacts.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบรายการ", "ลบผุ้ติดต่อ [" + cboVendor.Text + "," + d.ContactName+"]","");
                                    }
                                    C += 1;



                                    db.SubmitChanges();
                                }
                            }

                        }
                    }
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("ลบรายการผู้ติดต่อ", ex.Message, this.Name);
            }

            if (C > 0)
            {
                    MessageBox.Show("ลบรายการ สำเร็จ!");
            }
              

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;
            radGridView1.AllowAddNewRow = false;
            radGridView1.Rows.AddNew();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = true;
            btnView.Enabled = false;
            btnEdit.Enabled = true;
            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;
            btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการบันทึก ?","บันทึก",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                AddUnit();
                DataLoad();
            }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
                //string check1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["VendorNo"].Value);
                //string TM= Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                //if (!check1.Trim().Equals("") && TM.Equals(""))
                //{
                    
                //    if (!CheckDuplicate(check1.Trim()))
                //    {
                //        MessageBox.Show("ข้อมูล รหัสกลุ่มปรเภท ซ้ำ");
                //        radGridView1.Rows[e.RowIndex].Cells["GroupCode"].Value = "";
                //        radGridView1.Rows[e.RowIndex].Cells["GroupCode"].IsSelected = true;

                //    }
                //}
        

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());

            if(e.KeyData==(Keys.Control|Keys.S))
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AddUnit();
                    DataLoad();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                DeleteUnit();
                DataLoad();
            
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            if (op.ShowDialog() == DialogResult.OK)
            {


                using (TextFieldParser parser = new TextFieldParser(op.FileName))
                {
                    dt.Rows.Clear();
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int a = 0;
                    int c = 0;
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        a += 1;
                        DataRow rd = dt.NewRow();
                        // MessageBox.Show(a.ToString());
                        string[] fields = parser.ReadFields();
                        c = 0;
                        foreach (string field in fields)
                        {
                            c += 1;
                            //TODO: Process field
                            //MessageBox.Show(field);
                            if (a>1)
                            {
                                if(c==1)
                                    rd["DefalutNo"] = Convert.ToBoolean(field);
                                else if(c==2)
                                    rd["ContactName"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["Tel"] = Convert.ToString(field);
                                else if (c == 4)
                                    rd["Fax"] = Convert.ToString(field);
                                else if (c == 5)
                                    rd["Email"] = Convert.ToString(field);
                                else if (c == 6)
                                    rd["VendorNo"] = Convert.ToString(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["DefalutNo"] = false;
                                else if (c == 2)
                                    rd["ContactName"] = "";
                                else if (c == 3)
                                    rd["Tel"] = "";
                                else if (c == 4)
                                    rd["Fax"] = "";
                                else if (c == 5)
                                    rd["Email"] = "";
                                else if (c == 6)
                                    rd["VendorNo"] = "";




                            }

                     
                        }
                        dt.Rows.Add(rd);

                    }
                }
                if(dt.Rows.Count>0)
                {
                    dbClss.AddHistory(this.Name, "Import", "Import file CSV in to System", "");
                    ImportData();
                    MessageBox.Show("Import Completed.");

                    DataLoad();
                }
               
            }
        }

        private void ImportData()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   
                    foreach (DataRow rd in dt.Rows)
                    {
                        if (!rd["VendorNo"].ToString().Equals("") && !rd["ContactName"].ToString().Equals(""))
                        {

                            var x = (from ix in db.tb_VendorContacts where ix.VendorNo.ToLower().Trim() == rd["VendorNo"].ToString().ToLower().Trim()
                                     && ix.ContactName.Trim().ToLower()==rd["ContactName"].ToString().Trim().ToLower()
                                     select ix).FirstOrDefault();

                            if(x==null)
                            {
                                
                                tb_VendorContact ts = new tb_VendorContact();
                                ts.VendorNo= Convert.ToString(rd["VendorNo"].ToString());
                                ts.ContactName = Convert.ToString(rd["ContactName"].ToString());
                                try
                                {

                                    ts.DefaultNo = Convert.ToBoolean(rd["DefaultNo"].ToString());
                                }
                                catch { ts.DefaultNo = false; }
                                ts.Tel= Convert.ToString(rd["Tel"].ToString());
                                ts.Mobile = Convert.ToString(rd["Tel"].ToString());
                                ts.Fax = Convert.ToString(rd["Fax"].ToString());
                                ts.Email = Convert.ToString(rd["Email"].ToString());
                                db.tb_VendorContacts.InsertOnSubmit(ts);
                                db.SubmitChanges();
                            }
                            else
                            {
                                x.Tel = Convert.ToString(rd["Tel"].ToString());
                                x.Mobile = Convert.ToString(rd["Tel"].ToString());
                                x.Fax = Convert.ToString(rd["Fax"].ToString());
                                x.Email = Convert.ToString(rd["Email"].ToString());
                                try
                                {

                                    x.DefaultNo = Convert.ToBoolean(rd["DefaultNo"].ToString());
                                }
                                catch {x.DefaultNo = false; }
                                db.SubmitChanges();

                            }

                       
                        }
                    }
                   
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("InportData", ex.Message, this.Name);
            }
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

        private void cboVendor_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtVenderName_TextChanged(object sender, EventArgs e)
        {
            if (Cath01 == 0)
            {

                VNDR = cboVendor.Text;
                VNDRName = txtVenderName.Text;
                DataLoad();
            }
        }

        private void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(Cath01==0)
                txtVenderName.Text = cboVendor.SelectedValue.ToString();

            }
            catch { }
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.HeaderText == "รหัสผู้ขาย")
            {
                if (e.CellElement.RowInfo.Cells["VendorNo"].Value != null)
                {
                    if (!e.CellElement.RowInfo.Cells["VendorNo"].Value.Equals(""))
                    {
                        e.CellElement.DrawFill = true;
                        // e.CellElement.ForeColor = Color.Blue;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.WhiteSmoke;
                    }
                    
                }
            }
        }
    }
}
