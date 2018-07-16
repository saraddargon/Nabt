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
    public partial class HistoryView : Telerik.WinControls.UI.RadRibbonForm
    {
        public HistoryView(string Screen)
        {
            InitializeComponent();
            ScreenSearch = Screen;
            this.Text = "ประวัติ "+ Screen;
        }
        public HistoryView(string Screen,string Refno)
        {
            InitializeComponent();
            ScreenSearch = Screen;
            this.Text = "ประวัติ " + Screen;
            RefNo = Refno;
        }
        string ScreenSearch = "";
        string RefNo = "";
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
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;

            DataLoad();
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (RefNo.Equals(""))
                    {
                        //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                        radGridView1.DataSource = db.tb_Histories.Where(s => s.ScreenName == ScreenSearch
                        ).OrderBy(o => o.CreateDate).ToList();
                    }
                    else
                    {
                        radGridView1.DataSource = db.tb_Histories.Where(s => s.ScreenName == ScreenSearch
                           && (s.RefNo) == RefNo

                        ).OrderBy(o => o.CreateDate).ToList();
                    }
                    int c = 0;
                    foreach (var x in radGridView1.Rows)
                    {
                        c += 1;
                        x.Cells["No"].Value = c;
                    }

                }
            }
            catch { }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
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
           
            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;
          
            radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
        
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
          
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
       
                DataLoad();
            
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
          //  dbClss.ExportGridCSV(radGridView1);
           dbClss.ExportGridXlSX(radGridView1);
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

        private void frezzRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (radGridView1.Rows.Count > 0)
                {

                    int Row = 0;
                    Row = radGridView1.CurrentRow.Index;
                    dbClss.Set_Freeze_Row(radGridView1, Row);


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


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
