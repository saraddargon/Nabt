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
    public partial class Forecast : Telerik.WinControls.UI.RadRibbonForm
    {
        public Forecast()
        {
            InitializeComponent();
        }


        //private int RowView = 50;
        //private int ColView = 10;


        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
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

            dt.Columns.Add(new DataColumn("YYYY", typeof(int)));
            dt.Columns.Add(new DataColumn("ModelName", typeof(string)));
            dt.Columns.Add(new DataColumn("PartName", typeof(string)));
            dt.Columns.Add(new DataColumn("PartNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Process", typeof(string)));
            dt.Columns.Add(new DataColumn("JAN", typeof(decimal)));
            dt.Columns.Add(new DataColumn("FEB", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MAR", typeof(decimal)));
            dt.Columns.Add(new DataColumn("APR", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MAY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("JUN", typeof(decimal)));
            dt.Columns.Add(new DataColumn("JUL", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AUG", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SEP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("OCT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("NOV", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DEC", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Active", typeof(bool)));
            dt.Columns.Add(new DataColumn("id", typeof(int)));

            dt2.Columns.Add(new DataColumn("YYYY", typeof(int)));
            dt2.Columns.Add(new DataColumn("ModelName", typeof(string)));
            dt2.Columns.Add(new DataColumn("PartName", typeof(string)));
            dt2.Columns.Add(new DataColumn("PartNo", typeof(string)));
            dt2.Columns.Add(new DataColumn("Process", typeof(string)));
            dt2.Columns.Add(new DataColumn("JAN", typeof(string)));
            dt2.Columns.Add(new DataColumn("FEB", typeof(string)));
            dt2.Columns.Add(new DataColumn("MAR", typeof(string)));
            dt2.Columns.Add(new DataColumn("APR", typeof(string)));
            dt2.Columns.Add(new DataColumn("MAY", typeof(string)));
            dt2.Columns.Add(new DataColumn("JUN", typeof(string)));
            dt2.Columns.Add(new DataColumn("JUL", typeof(string)));
            dt2.Columns.Add(new DataColumn("AUG", typeof(string)));
            dt2.Columns.Add(new DataColumn("SEP", typeof(string)));
            dt2.Columns.Add(new DataColumn("OCT", typeof(string)));
            dt2.Columns.Add(new DataColumn("NOV", typeof(string)));
            dt2.Columns.Add(new DataColumn("DEC", typeof(string)));
            dt2.Columns.Add(new DataColumn("Active", typeof(bool)));
            dt2.Columns.Add(new DataColumn("id", typeof(int)));


        }
        int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {
            RMenu3.Click += RMenu3_Click;
            RMenu4.Click += RMenu4_Click;
            RMenu5.Click += RMenu5_Click;
            RMenu6.Click += RMenu6_Click;
            RFrezzRow.Click += RFrezzRow_Click;
            RFrezzColumn.Click += RFrezzColumn_Click;
            RUnFrezz.Click += RUnFrezz_Click;

            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            GETDTRow();
   
            DefaultItem();
            cboYear.Text = DateTime.Now.Year.ToString();
            DataLoad();
            crow = 0;

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

        private void DefaultItem()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboModelName.AutoCompleteMode = AutoCompleteMode.Append;
                cboModelName.DisplayMember = "ModelName";
                cboModelName.ValueMember = "ModelName";
                cboModelName.DataSource = (from ix in db.tb_Models.Where(s => s.ModelActive == true)select new {ix.ModelName,ix.ModelDescription }).ToList();
                cboModelName.SelectedIndex = -1;


                try
                {

                    for(int i=2017;i<2030;i++)
                    {
                        cboYear.Items.Add(i.ToString());

                    }

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
            //dt.Rows.Clear();
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    try
                    {
                        int year1 = 2017;
                        int.TryParse(cboYear.Text, out year1);
                        radGridView1.DataSource = db.tb_ProductionForecasts.Where(s => s.ModelName.Contains(cboModelName.Text.Trim()) 
                        && s.YYYY== year1).ToList();

                        int ck = 0;
                        foreach (var x in radGridView1.Rows)
                        {
                            x.Cells["dgvCodeTemp"].Value = x.Cells["ModelName"].Value.ToString();
                            x.Cells["dgvCodeTemp2"].Value = x.Cells["YYYY"].Value.ToString();
                           
                            x.Cells["ModelName"].ReadOnly = true;
                            x.Cells["YYYY"].ReadOnly = true;
                            //x.Cells["MMM"].ReadOnly = true;
                            if (row >= 0 && row == ck && radGridView1.Rows.Count>0)
                            {

                                x.ViewInfo.CurrentRow = x;

                            }
                            ck += 1;
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

                                    /*
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
                                    */


                                }
                                else
                                {
                                    int id = 0;
                                    int.TryParse(Convert.ToString(g.Cells["id"].Value), out id);
                                    var u = (from ix in db.tb_ProductionForecasts
                                             where ix.ModelName == Convert.ToString(g.Cells["ModelName"].Value)
                                             && ix.YYYY == Convert.ToInt32(g.Cells["YYYY"].Value)
                                            // && ix.PartNo== Convert.ToString(g.Cells["PartNo"].Value)
                                             && ix.id == id
                                             select ix).First();

                                    decimal a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, a9 = 0, a10 = 0, a11 = 0, a12 = 0;
                                   
                                    decimal.TryParse(Convert.ToString(g.Cells["JAN"].Value), out a1);
                                    decimal.TryParse(Convert.ToString(g.Cells["FEB"].Value), out a2);
                                    decimal.TryParse(Convert.ToString(g.Cells["MAR"].Value), out a3);
                                    decimal.TryParse(Convert.ToString(g.Cells["APR"].Value), out a4);
                                    decimal.TryParse(Convert.ToString(g.Cells["MAY"].Value), out a5);
                                    decimal.TryParse(Convert.ToString(g.Cells["JUN"].Value), out a6);
                                    decimal.TryParse(Convert.ToString(g.Cells["JUL"].Value), out a7);
                                    decimal.TryParse(Convert.ToString(g.Cells["AUG"].Value), out a8);
                                    decimal.TryParse(Convert.ToString(g.Cells["SEP"].Value), out a9);
                                    decimal.TryParse(Convert.ToString(g.Cells["OCT"].Value), out a10);
                                    decimal.TryParse(Convert.ToString(g.Cells["NOV"].Value), out a11);
                                    decimal.TryParse(Convert.ToString(g.Cells["DEC"].Value), out a12);

                                    u.JAN = a1;
                                    u.FEB = a2;
                                    u.MAR = a3;
                                    u.APR = a4;
                                    u.MAY = a5;
                                    u.JUN = a6;
                                    u.JUL = a7;
                                    u.AUG = a8;
                                    u.SEP = a9;
                                    u.OCT = a10;
                                    u.NOV = a11;
                                    u.DEC = a12;
                                   

                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Model [" + u.ModelName +","+u.YYYY+ "]", "");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("Edit", ex.Message, this.Name);
            }

            if (C > 0)
            {
                
                if (radGridView1.Rows.Count == 1)
                    row = 0;
                MessageBox.Show("บันทึกสำเร็จ!");
            }
               

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
                                    int id = 0;
                                    int.TryParse(Convert.ToString(radGridView1.Rows[row].Cells["id"].Value), out id);
                                    var unit1 = (from ix in db.tb_ProductionForecasts
                                                 where ix.id == id

                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_ProductionForecasts.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบรายการ ModelName", "Model [" + d.ModelName + "]", "");
                                    }
                                    C += 1;



                                    db.SubmitChanges();
                                }
                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
                dbClss.AddError("DeleteUnit", ex.Message, this.Name);
            }

            if (C > 0)
            {
                row = row - 1;
                if (radGridView1.Rows.Count == 1)
                    row = 0;
                else if (row < 0 && radGridView1.Rows.Count > 1)
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
            ForecastConsumption md = new ForecastConsumption(cboYear.Text,cboModelName.Text);
            md.ShowDialog();
            row = radGridView1.Rows.Count - 1;
            if (row < 0)
                row = 0;
            DataLoad();
            //  radGridView1.Rows.AddNew();
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
                //string TM1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value);
                ////string TM2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["MMM"].Value);
                //string Chk = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                //if (Chk.Equals("") && !TM1.Equals(""))
                //{

                //    if (!CheckDuplicate(TM1, Chk))
                //    {
                //        MessageBox.Show("ข้อมูล รายการซ้า");
                //        radGridView1.Rows[e.RowIndex].Cells["ModelName"].Value = "";
                //        //  radGridView1.Rows[e.RowIndex].Cells["MMM"].Value = "";
                //        //  radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                //    }
                //}


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
                    return;
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
                
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
                if (op.ShowDialog() == DialogResult.OK)
                {


                    using (TextFieldParser parser = new TextFieldParser(op.FileName))
                    {
                        dt2.Rows.Clear();
                        DateTime? d = null;
                        DateTime d1 = DateTime.Now;
                        int id = 0;
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");
                        int a = 0;
                        int c = 0;
                        while (!parser.EndOfData)
                        {
                            //Processing row
                            a += 1;
                            DataRow rd = dt2.NewRow();
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
                                        rd["YYYY"] = Convert.ToString(field).Trim();
                                    else if (c == 2)
                                        rd["ModelName"] = Convert.ToString(field);
                                    else if (c == 3)
                                        rd["PartName"] = Convert.ToString(field);
                                    else if (c == 4)
                                        rd["PartNo"] = Convert.ToString(field);
                                    else if (c == 5)
                                        rd["Process"] = Convert.ToString(field);
                                    else if (c == 6)
                                        rd["JAN"] = Convert.ToString(field);
                                    else if (c == 7)
                                        rd["FEB"] = Convert.ToString(field).Trim();
                                    else if (c == 8)
                                        rd["MAR"] = Convert.ToString(field);
                                    else if (c == 9)
                                        rd["APR"] = Convert.ToString(field);
                                    else if (c == 10)
                                        rd["MAY"] = Convert.ToString(field);
                                    else if (c == 11)
                                        rd["JUN"] = Convert.ToString(field);
                                    else if (c == 12)
                                        rd["JUL"] = Convert.ToString(field);
                                    else if (c == 13)
                                        rd["AUG"] = Convert.ToString(field);
                                    else if (c == 14)
                                        rd["SEP"] = Convert.ToString(field);
                                    else if (c == 15)
                                        rd["OCT"] = Convert.ToString(field);
                                    else if (c == 16)
                                        rd["NOV"] = Convert.ToString(field);
                                    else if (c == 17)
                                        rd["DEC"] = Convert.ToString(field);
                                    else if (c == 18)
                                        rd["Active"] = Convert.ToBoolean(field);
                                    else if (c == 19)
                                    {
                                        id = 0;
                                        int.TryParse(Convert.ToString(field), out id);
                                        rd["id"] = id;
                                        
                                    }

                                }
                                else
                                {
                                    if (c == 1)
                                        rd["YYYY"] = 0;
                                    else if (c == 2)
                                        rd["ModelName"] = "";
                                    else if (c == 3)
                                        rd["PartName"] = "";
                                    else if (c == 4)
                                        rd["PartNo"] = "";
                                    else if (c == 5)
                                        rd["Process"] = "";
                                    else if (c == 6)
                                        rd["JAN"] = "";
                                    else if (c == 7)
                                        rd["FEB"] = "";
                                    else if (c == 8)
                                        rd["MAR"] = "";
                                    else if (c == 9)
                                        rd["APR"] = "";
                                    else if (c == 10)
                                        rd["MAY"] = "";
                                    else if (c == 11)
                                        rd["JUN"] = "";
                                    else if (c == 12)
                                        rd["JUL"] = "";
                                    else if (c == 13)
                                        rd["AUG"] = "";
                                    else if (c == 14)
                                        rd["SEP"] = "";
                                    else if (c == 15)
                                        rd["OCT"] = "";
                                    else if (c == 16)
                                        rd["NOV"] = "";
                                    else if (c == 17)
                                        rd["DEC"] = "";
                                    else if (c == 18)
                                        rd["Active"] = false;
                                    else if (c == 19)
                                        rd["id"] = 0;


                                }


                            }
                            dt2.Rows.Add(rd);

                        }
                    }
                    if (dt2.Rows.Count > 0)
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

                    foreach (DataRow rd in dt2.Rows)
                    {
                        if (!rd["ModelName"].ToString().Equals("") && !rd["YYYY"].ToString().Equals("0"))
                        {

                            int id = 0;
                            int.TryParse(rd["id"].ToString(), out id);
                            var mp1 = (from ix in db.tb_ProductionForecasts
                                       where ix.YYYY==Convert.ToInt32(rd["YYYY"].ToString())
                                             && ix.ModelName==Convert.ToString(rd["ModelName"])
                                             //&& ix.PartNo==Convert.ToString(rd["PartNo"])
                                       select ix).FirstOrDefault();
                            DateTime? d = null;
                            DateTime d1 = DateTime.Now;
                            decimal a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, a9 = 0, a10 = 0, a11 = 0, a12 = 0;
                            decimal.TryParse(rd["JAN"].ToString(), out a1);
                            decimal.TryParse(rd["FEB"].ToString(), out a2);
                            decimal.TryParse(rd["MAR"].ToString(), out a3);
                            decimal.TryParse(rd["APR"].ToString(), out a4);
                            decimal.TryParse(rd["MAY"].ToString(), out a5);
                            decimal.TryParse(rd["JUN"].ToString(), out a6);
                            decimal.TryParse(rd["JUL"].ToString(), out a7);
                            decimal.TryParse(rd["AUG"].ToString(), out a8);
                            decimal.TryParse(rd["SEP"].ToString(), out a9);
                            decimal.TryParse(rd["OCT"].ToString(), out a10);
                            decimal.TryParse(rd["NOV"].ToString(), out a11);
                            decimal.TryParse(rd["DEC"].ToString(), out a12);
                            int yyyy = 0;
                            int.TryParse(rd["YYYY"].ToString(), out yyyy);
                            if (mp1 == null)
                            {


                                tb_ProductionForecast mp = new tb_ProductionForecast();
                                mp.YYYY = yyyy;
                                mp.ModelName = rd["ModelName"].ToString();
                                mp.JAN = a1;
                                mp.FEB = a2;
                                mp.MAR = a3;
                                mp.APR = a4;
                                mp.MAY = a5;
                                mp.JUN = a6;
                                mp.JUL = a7;
                                mp.AUG = a8;
                                mp.SEP = a9;
                                mp.OCT = a10;
                                mp.NOV = a11;
                                mp.DEC = a12;
                                mp.PartName = rd["PartName"].ToString();
                                mp.PartNo = rd["PartNo"].ToString();
                                mp.Process = rd["Process"].ToString();
                                mp.Active = true;
                                db.tb_ProductionForecasts.InsertOnSubmit(mp);
                                db.SubmitChanges();
                            }
                            else
                            {

                                //mp1.YYYY = yyyy;
                                //mp1.ModelName = rd["ModelName"].ToString();
                                mp1.JAN = a1;
                                mp1.FEB = a2;
                                mp1.MAR = a3;
                                mp1.APR = a4;
                                mp1.MAY = a5;
                                mp1.JUN = a6;
                                mp1.JUL = a7;
                                mp1.AUG = a8;
                                mp1.SEP = a9;
                                mp1.OCT = a10;
                                mp1.NOV = a11;
                                mp1.DEC = a12;
                               
                                mp1.Active = Convert.ToBoolean(rd["Active"].ToString()) ;
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
            DataLoad();
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (crow == 0)
                DataLoad();
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (crow == 0)
                DataLoad();
        }

        private void frezzRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (radGridView1.Rows.Count > 0)
                {

                    int Row = 0;
                    Row = radGridView1.CurrentRow.Index;
                    dbClss.Set_Freeze_Row(radGridView1, Row);

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
                if (radGridView1.Columns.Count > 0)
                {
                    int Col = 0;
                    Col = radGridView1.CurrentColumn.Index;
                    dbClss.Set_Freeze_Column(radGridView1, Col);

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

                dbClss.Set_Freeze_UnColumn(radGridView1);
                dbClss.Set_Freeze_UnRows(radGridView1);
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
        private void RFrezzRow_Click(object sender, EventArgs e)
        {
            frezzRowToolStripMenuItem_Click(null, null);
        }
        private void RFrezzColumn_Click(object sender, EventArgs e)
        {
            frezzColumnToolStripMenuItem_Click(null, null);
        }
        private void RUnFrezz_Click(object sender, EventArgs e)
        {
            unFrezzToolStripMenuItem_Click(null, null);
        }
    }
}
