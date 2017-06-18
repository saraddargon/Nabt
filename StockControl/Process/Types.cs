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
    public partial class Types : Telerik.WinControls.UI.RadRibbonForm
    {
        public Types()
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

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            dt.Columns.Add(new DataColumn("GroupCode", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeCode", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeDetail", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            RMenu3.Click += RMenu3_Click;
            RMenu4.Click += RMenu4_Click;
            RMenu5.Click += RMenu5_Click;
            RMenu6.Click += RMenu6_Click;
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            GETDTRow();
           
            DataLoad();
            LoadDefualt();
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
            DeleteUnit();
            DataLoad();
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

        private void LoadDefualt()
        {
            try
            {


                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var gt = (from ix in db.tb_GroupTypes where ix.GroupActive == true select ix).ToList();
                    GridViewComboBoxColumn comboBoxColumn = this.radGridView1.Columns["GroupCode"] as GridViewComboBoxColumn;
                    comboBoxColumn.DisplayMember = "GroupCode";
                    comboBoxColumn.ValueMember = "GroupCode";
                    comboBoxColumn.DataSource = gt;
                    //comboBoxColumn.DataSource= new string[] { "INSERT","Mr.", "Mrs.", "Dr.", "Ms." };

                    //GridViewComboBoxColumn lookUpColumn = new GridViewComboBoxColumn();
                    //lookUpColumn.FieldName = "GroupCode1";
                    //lookUpColumn.Name = "GroupCode1";
                    //lookUpColumn.HeaderText = "GroupCode";
                    //lookUpColumn.DataSource = new string[] { "Mr.", "Mrs.", "Dr.", "Ms." };
                    //lookUpColumn.Width = 100;
                    //lookUpColumn.IsVisible = true;
                    //this.radGridView1.Columns.Add(lookUpColumn);

                }
            }catch(Exception ex) {
                MessageBox.Show(ex.Message);
                dbClss.AddError("เพิ่มปรเภท", ex.Message, this.Name);
            }
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                radGridView1.DataSource = db.tb_Types.ToList();
                int ck = 0;
                foreach(var x in radGridView1.Rows)
                {
                    x.Cells["dgvCodeTemp"].Value = x.Cells["GroupCode"].Value.ToString();
                    x.Cells["dgvCodeTemp2"].Value = x.Cells["TypeCode"].Value.ToString();
                    x.Cells["dgvOldDetail"].Value= x.Cells["TypeDetail"].Value.ToString();
                    x.Cells["GroupCode"].ReadOnly = true;
                    x.Cells["TypeCode"].ReadOnly = true;
                    x.Cells["GroupCode"].Style.ForeColor = Color.MidnightBlue;
                    x.Cells["TypeCode"].Style.ForeColor = Color.MidnightBlue;
                    if (row >= 0 && row == ck)
                    {

                        x.ViewInfo.CurrentRow = x;

                    }
                    ck += 1;
                }


               
            }


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code,string Typecode)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Types where ix.GroupCode == code && ix.TypeCode==Typecode select ix).Count();
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
                        if (!Convert.ToString(g.Cells["GroupCode"].Value).Equals("")
                            && !Convert.ToString(g.Cells["TypeCode"].Value).Equals(""))
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                               
                                if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals("")
                                    && Convert.ToString(g.Cells["dgvCodeTemp2"].Value).Equals("")
                                    )
                                {
                                    // MessageBox.Show("11");


                                    tb_Type gy = new tb_Type();
                                    gy.GroupCode = Convert.ToString(g.Cells["GroupCode"].Value);
                                    gy.TypeCode= Convert.ToString(g.Cells["TypeCode"].Value);
                                    gy.TypeActive = Convert.ToBoolean(g.Cells["TypeActive"].Value);
                                    gy.TypeDetail= Convert.ToString(g.Cells["TypeDetail"].Value);
                                    db.tb_Types.InsertOnSubmit(gy);
                                    db.SubmitChanges();
                                    C += 1;
                                    dbClss.AddHistory(this.Name, "เพิ่มประเภท", "Insert Type Code [" + gy.TypeCode+ "]","");
                                }
                                else
                                {
                                   
                                    var unit1 = (from ix in db.tb_Types
                                                 where ix.GroupCode == Convert.ToString(g.Cells["dgvCodeTemp"].Value)
                                                 && ix.TypeCode == Convert.ToString(g.Cells["dgvCodeTemp2"].Value)
                                                 select ix).First();
                                       unit1.TypeDetail = Convert.ToString(g.Cells["TypeDetail"].Value);
                                       unit1.TypeActive = Convert.ToBoolean(g.Cells["TypeActive"].Value);
                                    
                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Type Code [" + unit1.TypeCode+ "]","");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("เพิ่มปรเภท", ex.Message, this.Name);
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
                    string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["GroupCode"].Value);
                    string CodeDelete2 = Convert.ToString(radGridView1.Rows[row].Cells["TypeCode"].Value);
                    string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
                    string CodeTemp2 = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp2"].Value);
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals("") && !CodeDelete2.Equals(""))
                            {
                                if (!CodeTemp.Equals("") && !CodeTemp2.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_Types
                                                 where ix.GroupCode == CodeDelete
                                                 && ix.TypeCode == CodeDelete2
                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_Types.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบประเภท", "Delete Type Code ["+d.TypeCode+"]","");
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
                dbClss.AddError("ลบประเภท", ex.Message, this.Name);
            }

            if (C > 0)
            {
                row = row - 1;
                if (radGridView1.Rows.Count == 1)
                    row = 0;
                MessageBox.Show("ลบรายการ สำเร็จ!");
            }
              

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
        private void NewClick()
        {
            radGridView1.ReadOnly = false;
            radGridView1.AllowAddNewRow = false;
            btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            radGridView1.ReadOnly = false;
            btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
            radGridView1.ReadOnly = true;
            btnView.Enabled = false;
            btnEdit.Enabled = true;
            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditClick();
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
                string check1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["GroupCode"].Value);
                string check2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["TypeCode"].Value);
                string TM= Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                if (!check1.Trim().Equals("") && !check2.Trim().Equals("") && TM.Equals(""))
                {

                    if (!CheckDuplicate(check1.Trim(), check2.Trim()))
                    {
                        MessageBox.Show("ข้อมูล รหัสปรเภท ซ้ำ");
                        radGridView1.Rows[e.RowIndex].Cells["TypeCode"].Value = "";
                        radGridView1.Rows[e.RowIndex].Cells["GroupCode"].IsSelected = true;

                    }
                }
        

            }
            catch { }
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
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewClick();
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


                using (TextFieldParser parser = new TextFieldParser(op.FileName, Encoding.GetEncoding("windows-874")))
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
                                    rd["GroupCode"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["Typecode"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["TypeDetail"] = Convert.ToString(field);
                                else if (c == 4)
                                    rd["TypeActive"] = Convert.ToBoolean(field);

                            }
                            else
                            {

                                if (c == 1)
                                    rd["GroupCode"] = "";
                                else if (c == 2)
                                    rd["Typecode"] = "";
                                else if (c == 3)
                                    rd["TypeDetail"] = "";
                                else if (c == 4)
                                    rd["TypeActive"] = false;




                            }

                            //
                            //rd[""] = "";
                            //rd[""]
                        }
                        dt.Rows.Add(rd);

                    }
                }
                if(dt.Rows.Count>0)
                {
                    dbClss.AddHistory(this.Name, "Import [Type]", "Import file CSV in to System", "");
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
                        if (!rd["GroupCode"].ToString().Equals("") && !rd["TypeCode"].ToString().Equals(""))
                        {

                            var x = (from ix in db.tb_Types
                                     where ix.GroupCode.ToLower().Trim() == rd["GroupCode"].ToString().ToLower().Trim()
                                     && ix.TypeCode.ToLower().Trim() == rd["TypeCode"].ToString().ToLower().Trim()
                                     select ix).FirstOrDefault();

                            if(x==null)
                            {

                                tb_Type ts = new tb_Type();
                                ts.GroupCode= Convert.ToString(rd["GroupCode"].ToString());
                                ts.TypeCode = Convert.ToString(rd["TypeCode"].ToString());
                                ts.TypeDetail= Convert.ToString(rd["TypeDetail"].ToString());
                                ts.TypeActive = Convert.ToBoolean(rd["TypeActive"].ToString());
                                db.tb_Types.InsertOnSubmit(ts);
                                db.SubmitChanges();
                            }
                            else
                            {
                                x.TypeDetail= Convert.ToString(rd["TypeDetail"].ToString());
                                x.TypeActive = Convert.ToBoolean(rd["TypeActive"].ToString());
                               
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

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.HeaderText == "รหัสประเภทกลุ่ม" )
            {
                if (e.CellElement.RowInfo.Cells["GroupCode"].Value != null)
                {
                    if (!e.CellElement.RowInfo.Cells["GroupCode"].Value.Equals(""))
                    {
                        e.CellElement.DrawFill = true;
                        // e.CellElement.ForeColor = Color.Blue;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.WhiteSmoke;
                    }
                    //else
                    //{
                    //    e.CellElement.DrawFill = true;
                    //    //e.CellElement.ForeColor = Color.Yellow;
                    //    e.CellElement.NumberOfColors = 1;
                    //    e.CellElement.BackColor = Color.WhiteSmoke;
                    //}
                }
            }
        }
    }
}
