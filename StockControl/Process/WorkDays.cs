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
    public partial class WorkDays : Telerik.WinControls.UI.RadRibbonForm
    {
        public WorkDays()
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
            dt.Columns.Add(new DataColumn("YYYY", typeof(int)));
            dt.Columns.Add(new DataColumn("MMM", typeof(int)));
            dt.Columns.Add(new DataColumn("WorkDays", typeof(decimal)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            GETDTRow();
            //for (int i = 0; i <= RowView; i++)
            //{
            //    DataRow rd = dt.NewRow();
            //    rd["UnitCode"] = "";
            //    rd["UnitDetail"] = "";
            //    rd["UnitActive"] = false;
            //    dt.Rows.Add(rd);
            //}
            
            
            DataLoad();
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                try
                {


                    radGridView1.DataSource = db.tb_WorkDays.Where(s => s.YYYY == Convert.ToInt32(cboYear.Text)).ToList();
                    foreach (var x in radGridView1.Rows)
                    {
                        x.Cells["dgvCodeTemp"].Value = x.Cells["YYYY"].Value.ToString();
                        x.Cells["dgvCodeTemp2"].Value = x.Cells["MMM"].Value.ToString();
                        x.Cells["YYYY"].ReadOnly = true;
                        x.Cells["MMM"].ReadOnly = true;
                    }
                }catch(Exception ex) { MessageBox.Show(ex.Message); }
               
            }


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code,string Code2)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_WorkDays where ix.YYYY == Convert.ToInt32(code)
                         && ix.MMM == Convert.ToInt32(Code2) 
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
                        if (!Convert.ToString(g.Cells["YYYY"].Value).Equals("") 
                            && !Convert.ToString(g.Cells["MMM"].Value).Equals(""))
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                                int yyyy = 0;
                                int mmm = 0;
                                decimal wk = 0;
                                int.TryParse(Convert.ToString(g.Cells["YYYY"].Value), out yyyy);
                                int.TryParse(Convert.ToString(g.Cells["MMM"].Value), out mmm);
                                decimal.TryParse(Convert.ToString(g.Cells["WorkDays"].Value), out wk);
                                if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
                                {
                                    // MessageBox.Show("11");
                                    if (yyyy > 0 && mmm > 0 && wk > 0)
                                    {

                                        tb_WorkDay u = new tb_WorkDay();

                                        u.YYYY = yyyy;
                                        u.MMM = mmm;
                                        u.WorkDays = wk;
                                        db.tb_WorkDays.InsertOnSubmit(u);
                                        db.SubmitChanges();
                                        C += 1;
                                        dbClss.AddHistory(this.Name, "เพิ่ม", "Working Days [" + u.YYYY+","+u.MMM+ "]", "");
                                    }
                                }
                                else
                                {
                                   
                                    var unit1 = (from ix in db.tb_WorkDays
                                                 where ix.YYYY == Convert.ToInt32(g.Cells["dgvCodeTemp"].Value)
                                                 && ix.MMM== Convert.ToInt32(g.Cells["dgvCodeTemp2"].Value)
                                                 select ix).First();
                                    unit1.WorkDays = wk;
                                      
                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Working Days[" + unit1.YYYY+", "+ unit1.MMM+ "]","");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
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

                if (row >= 0)
                {
                    string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["YYYY"].Value);
                    string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
                    string CodeTemp2 = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp2"].Value);
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete +","+CodeTemp2+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals(""))
                            {
                                if (!CodeTemp.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_WorkDays
                                                 where ix.YYYY == Convert.ToInt32(CodeTemp)
                                                 && ix.MMM == Convert.ToInt32(CodeTemp2)
                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_WorkDays.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบรายการวันทำงาน", "Delete Working Days ["+d.YYYY+","+d.MMM+"]","");
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
                string TM1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["YYYY"].Value);
                string TM2 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["MMM"].Value);
                string Chk = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                if (!TM2.Trim().Equals("") && Chk.Equals(""))
                {

                    if (!CheckDuplicate(TM1,TM2))
                    {
                        MessageBox.Show("ข้อมูล รายการซ้า");
                        radGridView1.Rows[e.RowIndex].Cells["YYYY"].Value = "";
                        radGridView1.Rows[e.RowIndex].Cells["MMM"].Value = "";
                      //  radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                    }
                }


            }
            catch(Exception ex) { }
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
                                    rd["YYYY"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["MMM"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["WorkDays"] = Convert.ToString(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["YYYY"] = 0;
                                else if (c == 2)
                                    rd["MMM"] = 0;
                                else if (c == 3)
                                    rd["WorkDays"] = 0;




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
                        if (!rd["YYYY"].ToString().Equals("0") && !rd["MMM"].ToString().Equals("0"))
                        {
                            int yyyy = 0;
                            int mmm = 0;
                            decimal wk = 0;
                            int.TryParse(Convert.ToString(rd["YYYY"].ToString()), out yyyy);
                            int.TryParse(Convert.ToString(rd["MMM"].ToString()), out mmm);
                            decimal.TryParse(Convert.ToString(rd["WorkDays"].ToString()), out wk);

                            var x = (from ix in db.tb_WorkDays where ix.YYYY== yyyy && ix.MMM==mmm select ix).FirstOrDefault();
                            if (yyyy > 0 && mmm>0 && wk>0)
                            {

                                if (x == null)
                                {
                                    tb_WorkDay ts = new tb_WorkDay();
                                    ts.YYYY = yyyy;
                                    ts.MMM = mmm;
                                    ts.WorkDays = wk;
                                    db.tb_WorkDays.InsertOnSubmit(ts);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    x.WorkDays = wk;

                                    db.SubmitChanges();

                                }
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}
