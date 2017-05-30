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
using Telerik.WinControls.Data;

namespace StockControl
{
    public partial class ModelMapping : Telerik.WinControls.UI.RadRibbonForm
    {
        public ModelMapping()
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
            dt.Columns.Add(new DataColumn("ModelName", typeof(string)));
            dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDescription", typeof(bool)));
            dt.Columns.Add(new DataColumn("QtyPerPCS", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ToolLife", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            //.Columns.Add(new DataColumn("Limit", typeof(bool)));
            //dt.Columns.Add(new DataColumn("ExpireDate", typeof(DateTime)));
        }
        int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            GETDTRow();
            DefaultItem();

            DataLoad();
            crow = 0;
        }
        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboModelName.AutoCompleteMode = AutoCompleteMode.Append;
                cboModelName.DisplayMember = "ModelName";
                cboModelName.ValueMember = "ModelName";
                cboModelName.DataSource = db.tb_Models.Where(s => s.ModelActive == true).ToList();
                cboModelName.SelectedIndex = 0;


                try
                {

                    GridViewMultiComboBoxColumn col = (GridViewMultiComboBoxColumn)radGridView1.Columns["CodeNo"];
                    col.DataSource = (from ix in db.tb_Items.Where(s => s.Status.Equals("Active")) select new { ix.CodeNo, ix.ItemDescription }).ToList();
                    col.DisplayMember = "CodeNo";
                    col.ValueMember = "CodeNo";
                 
                    col.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                    col.FilteringMode = GridViewFilteringMode.DisplayMember;

                    // col.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;
                }
                catch { }

                //col.TextAlignment = ContentAlignment.MiddleCenter;
                //col.Name = "CodeNo";
                //this.radGridView1.Columns.Add(col);

                this.radGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                this.radGridView1.CellEditorInitialized += radGridView1_CellEditorInitialized;
            }
        }

        private void radGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            RadMultiColumnComboBoxElement mccbEl = e.ActiveEditor as RadMultiColumnComboBoxElement;
            if (mccbEl != null)
            {
                mccbEl.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                mccbEl.DropDownMinSize = new Size(350, 200);
                mccbEl.DropDownMaxSize = new Size(350, 200);

                mccbEl.AutoSizeDropDownToBestFit = true;
                mccbEl.DropDownAnimationEnabled = false;
                mccbEl.AutoFilter = true;
                FilterDescriptor filterDescriptor = new FilterDescriptor(mccbEl.ValueMember, FilterOperator.Contains, string.Empty);
                mccbEl.EditorControl.MasterTemplate.FilterDescriptors.Add(filterDescriptor);
            }
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    try
                    {
                       // radGridView1.DataSource = null;
                        if (!cboModelName.Text.Trim().Equals(""))
                        {

                            var gd = (from a in db.tb_Mappings
                                      where a.ModelName.Equals(cboModelName.Text)
                                      select new {
                                          a.ModelName,
                                          a.CodeNo,
                                          a.Remark,
                                          a.QtyPerPCS,
                                          a.ToolLife,
                                          ItemDescription = (from b in db.tb_Items.Where(s=>s.CodeNo.Trim().Equals(a.CodeNo)) select b.ItemDescription).FirstOrDefault()
                                      }).ToList();

                          

                            radGridView1.DataSource = gd;

                            foreach (var x in radGridView1.Rows)
                            {
                                //x.Cells["dgvCodeTemp"].Value = x.Cells["ModelName"].Value.ToString();
                                // x.Cells["dgvCodeTemp2"].Value = x.Cells["CodeNo"].Value.ToString();
                                x.Cells["ModelName"].ReadOnly = true;
                                //x.Cells["MMM"].ReadOnly = true;
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch { }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code, string Code2)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Models
                         where ix.ModelName == code

                         select ix).Count();
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

                /*
                radGridView1.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in radGridView1.Rows)
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

                */
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
        private bool DeleteUnit()
        {
            bool ck = false;

            int C = 0;
            try
            {
                /*

                if (row >= 0)
                {
                    string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["ModelName"].Value);
                    string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
                    string CodeTemp2 = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp2"].Value);
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( " + CodeDelete + " ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals(""))
                            {
                                if (!CodeTemp.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_Models
                                                 where ix.ModelName == Convert.ToString(CodeTemp)

                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_Models.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบรายการ ModelName", "Delete Model [" + d.ModelName + "]", "");
                                    }
                                    C += 1;



                                    db.SubmitChanges();
                                }
                            }

                        }
                    }
                }
                */
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("DeleteUnit", ex.Message, this.Name);
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
           // radGridView1.Rows.AddNew();
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
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                /*
                string TM1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value);
                //string TM2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["MMM"].Value);
                string Chk = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                if (Chk.Equals("") && !TM1.Equals(""))
                {

                    if (!CheckDuplicate(TM1, Chk))
                    {
                        MessageBox.Show("ข้อมูล รายการซ้า");
                        radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value = "";
                        //  radGridView1.Rows[e.RowIndex].Cells["MMM"].Value = "";
                        //  radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                    }
                }
                */

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
            try
            {
                return;
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
                if (op.ShowDialog() == DialogResult.OK)
                {


                    using (TextFieldParser parser = new TextFieldParser(op.FileName))
                    {
                        dt.Rows.Clear();
                        DateTime? d = null;
                        DateTime d1 = DateTime.Now;
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

                                if (a > 1)
                                {
                                    if (c == 1)
                                        rd["ModelName"] = Convert.ToString(field).Trim();
                                    else if (c == 2)
                                        rd["ModelDescription"] = Convert.ToString(field);
                                    else if (c == 3)
                                        rd["ModelActive"] = Convert.ToBoolean(field);
                                    else if (c == 4)
                                        rd["LineName"] = Convert.ToString(field).Trim();
                                    else if (c == 5)
                                        rd["MCName"] = Convert.ToString(field);
                                    else if (c == 6)
                                        rd["Limit"] = Convert.ToBoolean(field);
                                    else if (c == 7)
                                    {
                                        if (DateTime.TryParse(Convert.ToString(field), out d1))
                                        {
                                            rd["ExpireDate"] = Convert.ToDateTime(field);

                                        }
                                        else
                                        {
                                            rd["ExpireDate"] = d;
                                        }
                                    }

                                }
                                else
                                {
                                    if (c == 1)
                                        rd["ModelName"] = "";
                                    else if (c == 2)
                                        rd["ModelDescription"] = "";
                                    else if (c == 3)
                                        rd["ModelActive"] = false;
                                    else if (c == 4)
                                        rd["LineName"] = "";
                                    else if (c == 5)
                                        rd["MCName"] = "";
                                    else if (c == 6)
                                        rd["Limit"] = false;
                                    else if (c == 7)
                                        rd["ExpireDate"] = d;




                                }


                            }
                            dt.Rows.Add(rd);

                        }
                    }
                    if (dt.Rows.Count > 0)
                    {

                        dbClss.AddHistory(this.Name, "Import", "Import file CSV in to System", "");
                        ImportData();
                        MessageBox.Show("Import Completed.");

                        DataLoad();
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dt.Rows.Clear(); }
        }

        private void ImportData()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    foreach (DataRow rd in dt.Rows)
                    {
                        if (!rd["ModelName"].ToString().Equals(""))
                        {


                            var x = (from ix in db.tb_Models where ix.ModelName == rd["ModelName"].ToString().Trim() select ix).FirstOrDefault();


                            DateTime? d = null;
                            DateTime d1 = DateTime.Now;
                            if (x == null)
                            {
                                tb_Model u = new tb_Model();
                                u.ModelName = rd["ModelName"].ToString().Trim();
                                u.ModelDescription = rd["ModelDescription"].ToString().Trim();
                                u.ModelActive = Convert.ToBoolean(rd["ModelActive"].ToString());
                                u.LineName = rd["LineName"].ToString().Trim();
                                u.MCName = rd["MCName"].ToString().Trim();
                                u.Limit = Convert.ToBoolean(rd["Limit"].ToString());
                                if (DateTime.TryParse(rd["ExpireDate"].ToString(), out d1))
                                {

                                    u.ExpireDate = Convert.ToDateTime(rd["ExpireDate"].ToString());
                                }
                                else
                                {
                                    u.ExpireDate = d;
                                }
                                db.tb_Models.InsertOnSubmit(u);
                                db.SubmitChanges();
                            }
                            else
                            {
                                x.ModelName = rd["ModelName"].ToString().Trim();
                                x.ModelDescription = rd["ModelDescription"].ToString().Trim();
                                x.ModelActive = Convert.ToBoolean(rd["ModelActive"].ToString());
                                x.LineName = rd["LineName"].ToString().Trim();
                                x.MCName = rd["MCName"].ToString().Trim();
                                x.Limit = Convert.ToBoolean(rd["Limit"].ToString());
                                if (DateTime.TryParse(rd["ExpireDate"].ToString(), out d1))
                                {

                                    x.ExpireDate = Convert.ToDateTime(rd["ExpireDate"].ToString());
                                }
                                else
                                {
                                    x.ExpireDate = d;
                                }


                                db.SubmitChanges();

                            }



                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (e.CellElement.ColumnInfo.Name == "ModelName")
            {
                if (e.CellElement.RowInfo.Cells["ModelName"].Value != null)
                {
                    if (!e.CellElement.RowInfo.Cells["ModelName"].Value.Equals(""))
                    {
                        e.CellElement.DrawFill = true;
                        // e.CellElement.ForeColor = Color.Blue;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.WhiteSmoke;
                    }

                }
            }
        }

        private void txtModelName_TextChanged(object sender, EventArgs e)
        {
           // DataLoad();
        }

        private void cboModelName_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void cboModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(crow==0)
                    DataLoad();
        }
    }
}
