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
    public partial class ItemQCImage : Telerik.WinControls.UI.RadRibbonForm
    {
        public ItemQCImage()
        {
            InitializeComponent();
        }
        public ItemQCImage(string UserIDx)
        {
            InitializeComponent();
            Code = UserIDx;
        }
        string Code = "";
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
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Code.Equals(""))
                {
                    // txtItemNo.Enabled = false;
                    txtItemNo.ReadOnly = true;
                    txtItemNo.Text = Code;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        
                       tb_QCImageCheck  ur = db.tb_QCImageChecks.Where(u => u.PartNo.Equals(Code)).FirstOrDefault();
                        if (ur != null)
                        {
                            QC1.Checked = false;
                            QC2.Checked = false;
                            QC3.Checked = false;
                            QC4.Checked = false;

                            if(!ur.Image1.Equals(""))
                            {
                                QC1.Checked = true;
                                txtPathImage.Text = ur.Image1;
                            }
                            if(!ur.Image2.Equals(""))
                            {
                                QC2.Checked = true;
                                txtPathImage2.Text = ur.Image2;
                            }
                            if (!ur.Image3.Equals(""))
                            {
                                QC3.Checked = true;
                                txtPathImage3.Text = ur.Image3;
                            }
                            if (!ur.Image4.Equals(""))
                            {
                                QC4.Checked = true;
                                txtPathImage4.Text = ur.Image4;
                            }

                        }
                        else
                        {
                            //MessageBox.Show("Item No. Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //this.Close();
                        }
                        
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
           // DeleteUnit();
            //DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            //EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
           // ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
           // NewClick();

        }

        private void DataLoad()
        {
           
            
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            //using (DataClasses1DataContext db = new DataClasses1DataContext())
            //{
            //    int i = (from ix in db.tb_Units where ix.UnitCode == code select ix).Count();
            //    if (i > 0)
            //        ck = false;
            //    else
            //        ck = true;
            //}
            return ck;
        }

        private bool AddUnit()
        {
            bool ck = false;
            //int C = 0;
            //try
            //{


            //    radGridView1.EndEdit();
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
            //        foreach (var g in radGridView1.Rows)
            //        {
            //            if (!Convert.ToString(g.Cells["UnitCode"].Value).Equals(""))
            //            {
            //                if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
            //                {
                               
            //                    if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
            //                    {
            //                       // MessageBox.Show("11");
                                    
            //                        tb_Unit u = new tb_Unit();
            //                        u.UnitCode = Convert.ToString(g.Cells["UnitCode"].Value);
            //                        u.UnitActive = Convert.ToBoolean(g.Cells["UnitActive"].Value);
            //                        u.UnitDetail= Convert.ToString(g.Cells["UnitDetail"].Value);
            //                        db.tb_Units.InsertOnSubmit(u);
            //                        db.SubmitChanges();
            //                        C += 1;
            //                        dbClss.AddHistory(this.Name, "เพิ่ม", "Insert Unit Code [" + u.UnitCode+"]","");
            //                    }
            //                    else
            //                    {
                                   
            //                        var unit1 = (from ix in db.tb_Units
            //                                     where ix.UnitCode == Convert.ToString(g.Cells["dgvCodeTemp"].Value)
            //                                     select ix).First();
            //                           unit1.UnitDetail = Convert.ToString(g.Cells["UnitDetail"].Value);
            //                           unit1.UnitActive = Convert.ToBoolean(g.Cells["UnitActive"].Value);
                                    
            //                        C += 1;

            //                        db.SubmitChanges();
            //                        dbClss.AddHistory(this.Name, "แก้ไข", "Update Unit Code [" + unit1.UnitCode+"]","");

            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("AddUnit", ex.Message, this.Name);
            //}

            //if (C > 0)
            //    MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;
         
            //int C = 0;
            //try
            //{
                
            //    if (row >= 0)
            //    {
            //        string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["UnitCode"].Value);
            //        string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
            //        radGridView1.EndEdit();
            //        if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            using (DataClasses1DataContext db = new DataClasses1DataContext())
            //            {

            //                if (!CodeDelete.Equals(""))
            //                {
            //                    if (!CodeTemp.Equals(""))
            //                    {

            //                        var unit1 = (from ix in db.tb_Units
            //                                     where ix.UnitCode == CodeDelete
            //                                     select ix).ToList();
            //                        foreach (var d in unit1)
            //                        {
            //                            db.tb_Units.DeleteOnSubmit(d);
            //                            dbClss.AddHistory(this.Name, "ลบ Unit", "Delete Unit Code ["+d.UnitCode+"]","");
            //                        }
            //                        C += 1;



            //                        db.SubmitChanges();
            //                    }
            //                }

            //            }
            //        }
            //    }
            //}

            //catch (Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("DeleteUnit", ex.Message, this.Name);
            //}

            //if (C > 0)
            //{
            //        row = row - 1;
            //        MessageBox.Show("ลบรายการ สำเร็จ!");
            //}
              

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //DataLoad();
        }
        private void NewClick()
        {
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //btnEdit.Enabled = false;
           // btnView.Enabled = true;
           // radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
          //  radGridView1.ReadOnly = false;
            //btnEdit.Enabled = false;
            //btnView.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
           // radGridView1.ReadOnly = true;
           // btnView.Enabled = false;
            //btnEdit.Enabled = true;
           // radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //NewClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            //EditClick();
        }
        private void Saveclick()
        {
            if(!txtItemNo.Text.Equals("") && (!txtPathImage.Text.Equals("") || !txtPathImage2.Text.Equals("") || !txtPathImage3.Text.Equals("") || !txtPathImage4.Text.Equals("")))
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Path = "";
                        string Path2 = "";
                        string Path3 = "";
                        string Path4 = "";

                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_Path ph = db.tb_Paths.Where(p => p.PathCode == "QC1").FirstOrDefault();
                            if(ph!=null)
                            {
                                Path = ph.PathFile;
                            }
                            tb_Path ph2 = db.tb_Paths.Where(p => p.PathCode == "QC2").FirstOrDefault();
                            if (ph2 != null)
                            {
                                Path2 = ph2.PathFile;
                            }
                            tb_Path ph3 = db.tb_Paths.Where(p => p.PathCode == "QC3").FirstOrDefault();
                            if (ph3 != null)
                            {
                                Path3 = ph3.PathFile;
                            }
                            tb_Path ph4 = db.tb_Paths.Where(p => p.PathCode == "QC4").FirstOrDefault();
                            if (ph4 != null)
                            {
                                Path4 = ph4.PathFile;
                            }

                            //ImageQC1////      
                            if (!txtImage.Text.Equals(""))
                            {
                                tb_QCImageCheck uck = db.tb_QCImageChecks.Where(u => u.PartNo == txtItemNo.Text).FirstOrDefault();
                                if (uck != null)
                                {

                                    Path = Path + txtImage.Text;
                                    UploadImage(txtPathImage.Text, Path);
                                    uck.Image1 = txtImage.Text;
                                    uck.UdateBy = dbClss.UserID;
                                    uck.UpdateDate = DateTime.Now;
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "แก้ไขรูป", "แก้ไขรายละเอียด  [" + txtItemNo.Text + "] ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                                else
                                {
                                    Path = Path + txtImage.Text;
                                    UploadImage(txtPathImage.Text, Path);
                                    tb_QCImageCheck im = new tb_QCImageCheck();
                                    im.PartNo = txtItemNo.Text;
                                    im.LineName = "";
                                    im.Image1 = txtImage.Text;
                                    im.Image2 = "";
                                    im.Image3 = "";
                                    im.Image4 = "";
                                    im.CreateBy = dbClss.UserID;
                                    im.CreateDate = DateTime.Now;
                                    db.tb_QCImageChecks.InsertOnSubmit(im);
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "เพิ่มรูป", "ทำการเพิ่มรายการชื่อ  [" + txtItemNo.Text + "] เข้าระบบ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                            }
                            /////End Image QC 1///
                            //ImageQC2////   
                            if (!txtImage2.Text.Equals(""))
                            {
                                tb_QCImageCheck uck2 = db.tb_QCImageChecks.Where(u => u.PartNo == txtItemNo.Text).FirstOrDefault();
                                if (uck2 != null)
                                {

                                    Path2 = Path2 + txtImage2.Text;
                                    UploadImage(txtPathImage2.Text, Path2);
                                    uck2.Image2 = txtImage2.Text;
                                    uck2.UdateBy = dbClss.UserID;
                                    uck2.UpdateDate = DateTime.Now;
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "แก้ไขรูป", "แก้ไขรายละเอียด  [" + txtItemNo.Text + "] ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                                else
                                {
                                    Path2 = Path2 + txtImage2.Text;
                                    UploadImage(txtPathImage2.Text, Path2);
                                    tb_QCImageCheck im = new tb_QCImageCheck();
                                    im.PartNo = txtItemNo.Text;
                                    im.LineName = "";
                                    im.Image1 = "";
                                    im.Image2 = txtImage2.Text;
                                    im.Image3 = "";
                                    im.Image4 = "";
                                    im.CreateBy = dbClss.UserID;
                                    im.CreateDate = DateTime.Now;
                                    db.tb_QCImageChecks.InsertOnSubmit(im);
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "เพิ่มรูป", "ทำการเพิ่มรายการชื่อ  [" + txtItemNo.Text + "] เข้าระบบ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                            }
                            /////End Image QC2///

                            //ImageQC3////         
                            if (!txtImage3.Text.Equals(""))
                            {
                                tb_QCImageCheck uck3 = db.tb_QCImageChecks.Where(u => u.PartNo == txtItemNo.Text).FirstOrDefault();
                                if (uck3 != null)
                                {

                                    Path3 = Path3 + txtImage3.Text;
                                    UploadImage(txtPathImage3.Text, Path3);
                                    uck3.Image3 = txtImage3.Text;
                                    uck3.UdateBy = dbClss.UserID;
                                    uck3.UpdateDate = DateTime.Now;
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "แก้ไขรูป", "แก้ไขรายละเอียด  [" + txtItemNo.Text + "] ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                                else
                                {
                                    Path3 = Path3 + txtImage3.Text;
                                    UploadImage(txtPathImage3.Text, Path3);
                                    tb_QCImageCheck im = new tb_QCImageCheck();
                                    im.PartNo = txtItemNo.Text;
                                    im.LineName = "";
                                    im.Image1 = "";
                                    im.Image2 = "";
                                    im.Image3 = txtImage3.Text;
                                    im.Image4 = "";
                                    im.CreateBy = dbClss.UserID;
                                    im.CreateDate = DateTime.Now;
                                    db.tb_QCImageChecks.InsertOnSubmit(im);
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "เพิ่มรูป", "ทำการเพิ่มรายการชื่อ  [" + txtItemNo.Text + "] เข้าระบบ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                            }
                            /////End Image QC2///

                            //ImageQC4//// 
                            if (!txtImage4.Text.Equals(""))
                            {
                                tb_QCImageCheck uck4 = db.tb_QCImageChecks.Where(u => u.PartNo == txtItemNo.Text).FirstOrDefault();
                                if (uck4 != null)
                                {

                                    Path4 = Path4 + txtImage4.Text;
                                    UploadImage(txtPathImage4.Text, Path4);
                                    uck4.Image4 = txtImage4.Text;
                                    uck4.UdateBy = dbClss.UserID;
                                    uck4.UpdateDate = DateTime.Now;
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "แก้ไขรูป", "แก้ไขรายละเอียด  [" + txtItemNo.Text + "] ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                                else
                                {
                                    Path4 = Path4 + txtImage4.Text;
                                    UploadImage(txtPathImage4.Text, Path4);
                                    tb_QCImageCheck im = new tb_QCImageCheck();
                                    im.PartNo = txtItemNo.Text;
                                    im.LineName = "";
                                    im.Image1 = "";
                                    im.Image2 = "";
                                    im.Image3 = "";
                                    im.Image4 = txtImage4.Text;
                                    im.CreateBy = dbClss.UserID;
                                    im.CreateDate = DateTime.Now;
                                    db.tb_QCImageChecks.InsertOnSubmit(im);
                                    db.SubmitChanges();
                                    //dbClss.AddHistory("ListItem", "เพิ่มรูป", "ทำการเพิ่มรายการชื่อ  [" + txtItemNo.Text + "] เข้าระบบ", "จากเครื่อง " + System.Environment.MachineName);
                                }
                            }
                            /////End Image QC2//


                            MessageBox.Show("Save Completed!");
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

            }
            else
            {
                MessageBox.Show("Path Image is Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadImage(string Path,string Listpath)
        {
            try
            {

                //System.IO.File.Copy(txtPathImage.Text, Path + txtImage.Text, true);
                if (System.IO.File.Exists(Listpath))
                {
                    try
                    {
                        System.IO.File.Delete(Listpath);
                    }
                    catch { }
                    System.IO.File.Copy(Path, Listpath, true);
                }
                else
                {
                    System.IO.File.Copy(Path, Listpath, true);
                }

                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtItemNo.Text.Equals(""))
            {
                Saveclick();

            }else
            {
                MessageBox.Show("ต้องเลือกรูปใหม่!");
            }
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                //radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
                //string check1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["UnitCode"].Value);
                //string TM= Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                //if (!check1.Trim().Equals("") && TM.Equals(""))
                //{
                    
                //    if (!CheckDuplicate(check1.Trim()))
                //    {
                //        MessageBox.Show("ข้อมูล รหัสหน่วย ซ้ำ");
                //        radGridView1.Rows[e.RowIndex].Cells["UnitCode"].Value = "";
                //        radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                //    }
                //}
        

            }
            catch(Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyData == (Keys.Control | Keys.S))
            {
                //if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    //AddUnit();
                //    //DataLoad();
                //}
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                //if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    //NewClick();
                //}
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
          // dbClss.ExportGridXlSX(radGridView1);
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
                                    rd["UnitCode"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["UnitDetail"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["UnitActive"] = Convert.ToBoolean(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["UnitCode"] = "";
                                else if (c == 2)
                                    rd["UnitDetail"] = "";
                                else if (c == 3)
                                    rd["UnitActive"] = false;




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
            //try
            //{
            //    using (DataClasses1DataContext db = new DataClasses1DataContext())
            //    {
                   
            //        foreach (DataRow rd in dt.Rows)
            //        {
            //            if (!rd["UnitCode"].ToString().Equals(""))
            //            {

            //                var x = (from ix in db.tb_Units where ix.UnitCode.ToLower().Trim() == rd["UnitCode"].ToString().ToLower().Trim() select ix).FirstOrDefault();

            //                if(x==null)
            //                {
            //                    tb_Unit ts = new tb_Unit();
            //                    ts.UnitCode = Convert.ToString(rd["UnitCode"].ToString());
            //                    ts.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
            //                    ts.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
            //                    db.tb_Units.InsertOnSubmit(ts);
            //                    db.SubmitChanges();
            //                }
            //                else
            //                {
            //                    x.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
            //                    x.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
            //                    db.SubmitChanges();

            //                }

                       
            //            }
            //        }
                   
            //    }
            //}
            //catch(Exception ex) { MessageBox.Show(ex.Message);
            //    dbClss.AddError("InportData", ex.Message, this.Name);
            //}
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
           // radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files (*.PNG)|*.PNG|JPEG files (*.JPEG)|*.JPEG";
            if (op.ShowDialog() == DialogResult.OK)
            {
                txtPathImage.Text = op.FileName;
                txtPathInput.Text = op.FileName;
                string Ex = System.IO.Path.GetExtension(txtPathImage.Text);
                txtImage.Text = "QC1_"+txtItemNo.Text + "" + Ex;
                
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files (*.PNG)|*.PNG|JPEG files (*.JPEG)|*.JPEG";
            if (op.ShowDialog() == DialogResult.OK)
            {
                txtPathImage2.Text = op.FileName;
                txtPathInput2.Text = op.FileName;
                string Ex = System.IO.Path.GetExtension(txtPathImage2.Text);
                txtImage2.Text = "QC2_" + txtItemNo.Text + "" + Ex;

            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files (*.PNG)|*.PNG|JPEG files (*.JPEG)|*.JPEG";
            if (op.ShowDialog() == DialogResult.OK)
            {
                txtPathImage3.Text = op.FileName;
                txtPathInput3.Text = op.FileName;
                string Ex = System.IO.Path.GetExtension(txtPathImage3.Text);
                txtImage3.Text = "QC3_" + txtItemNo.Text + "" + Ex;

            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files (*.PNG)|*.PNG|JPEG files (*.JPEG)|*.JPEG";
            if (op.ShowDialog() == DialogResult.OK)
            {
                txtPathImage4.Text = op.FileName;
                txtPathInput4.Text = op.FileName;
                string Ex = System.IO.Path.GetExtension(txtPathImage4.Text);
                txtImage4.Text = "QC4_" + txtItemNo.Text + "" + Ex;

            }
        }
    }
}
