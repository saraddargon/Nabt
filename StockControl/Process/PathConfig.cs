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
    public partial class PathConfig : Telerik.WinControls.UI.RadRibbonForm
    {
        public PathConfig()
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
            dt.Columns.Add(new DataColumn("PathCode", typeof(string)));
            dt.Columns.Add(new DataColumn("PathDetail", typeof(string)));
            dt.Columns.Add(new DataColumn("PathFile", typeof(string)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            RMenu4.Click += RMenu4_Click;
            RMenu5.Click += RMenu5_Click;
            
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

        private void RMenu6_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                radGridView1.DataSource = db.tb_Paths.ToList();// dt;
                foreach(var x in radGridView1.Rows)
                {
                    x.Cells["dgvCodeTemp"].Value = x.Cells["PathCode"].Value.ToString();
                    x.Cells["PathCode"].ReadOnly = true;
                }
               
            }


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
                        if (!Convert.ToString(g.Cells["PathCode"].Value).Equals(""))
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                               
                                if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
                                {
                                    // MessageBox.Show("11");


                                    //tb_Path gy = new tb_Path();
                                    //gy.PathCode = Convert.ToString(g.Cells["PathCode"].Value);
                                    //gy.GroupActive = Convert.ToBoolean(g.Cells["GroupActive"].Value);
                                    //gy.GroupName= Convert.ToString(g.Cells["GroupName"].Value);
                                    //db.tb_GroupTypes.InsertOnSubmit(gy);
                                    //db.SubmitChanges();
                                    //dbClss.AddHistory(this.Name, "เพิ่มประเภทกลุ่ม", "Insert Group Code [" + gy.GroupName+ "]","");
                                }
                                else
                                {
                                   
                                    var unit1 = (from ix in db.tb_Paths
                                                 where ix.PathCode == Convert.ToString(g.Cells["dgvCodeTemp"].Value)
                                                 select ix).First();
                                       unit1.PathDetail = Convert.ToString(g.Cells["PathDetail"].Value);
                                       unit1.PathFile = Convert.ToString(g.Cells["PathFile"].Value);
                                    
                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Path Code [" + unit1.PathCode + "]","");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("แก้ไข Path", ex.Message, this.Name);
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool DeleteUnit()
        {
            return false;
            bool ck = false;
         
            int C = 0;
            try
            {

                if (row >= 0)
                {
                    string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["PathCode"].Value);
                    string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals(""))
                            {
                                if (!CodeTemp.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_GroupTypes
                                                 where ix.GroupCode == CodeDelete
                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_GroupTypes.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบประเภทกลุ่ม", "Delete Group Code ["+d.GroupName+"]","");
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
                dbClss.AddError("ลบประเภทกลุ่ม", ex.Message, this.Name);
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
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //radGridView1.Rows.AddNew();
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
                string TM= Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                if (!check1.Trim().Equals("") && TM.Equals(""))
                {
                    
                    if (!CheckDuplicate(check1.Trim()))
                    {
                        MessageBox.Show("ข้อมูล รหัสกลุ่มปรเภท ซ้ำ");
                        radGridView1.Rows[e.RowIndex].Cells["GroupCode"].Value = "";
                        radGridView1.Rows[e.RowIndex].Cells["GroupCode"].IsSelected = true;

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

            MessageBox.Show("ไม่สามารถลบได้!");   
               // DeleteUnit();
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
                                    rd["GroupCode"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["GroupName"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["GroupActive"] = Convert.ToBoolean(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["GroupCode"] = "";
                                else if (c == 2)
                                    rd["GroupName"] = "";
                                else if (c == 3)
                                    rd["GroupActive"] = false;




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
                        if (!rd["GroupCode"].ToString().Equals(""))
                        {

                            var x = (from ix in db.tb_GroupTypes where ix.GroupCode.ToLower().Trim() == rd["GroupCode"].ToString().ToLower().Trim() select ix).FirstOrDefault();

                            if(x==null)
                            {
                                
                                tb_GroupType ts = new tb_GroupType();
                                ts.GroupCode= Convert.ToString(rd["GroupCode"].ToString());
                                ts.GroupName = Convert.ToString(rd["GroupName"].ToString());
                                ts.GroupActive = Convert.ToBoolean(rd["GroupActive"].ToString());
                                db.tb_GroupTypes.InsertOnSubmit(ts);
                                db.SubmitChanges();
                            }
                            else
                            {
                                x.GroupName = Convert.ToString(rd["GroupName"].ToString());
                                x.GroupActive = Convert.ToBoolean(rd["GroupActive"].ToString());
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

        private void MasterTemplate_Click(object sender, EventArgs e)
        {

        }
    }
}
