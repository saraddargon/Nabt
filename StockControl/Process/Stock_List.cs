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
    public partial class Stock_List : Telerik.WinControls.UI.RadRibbonForm
    {
        public Stock_List(string DocNoxx)
        {
            InitializeComponent();
            DocNo = DocNoxx;
            
        }
        public Stock_List(string DocNoxx,string Typexx)
        {
            InitializeComponent();
            DocNo = DocNoxx;

            Type = Typexx;
        }
        string DocNo = "";
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
            dgvData.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;

            DataLoad();
        }

        private void DataLoad()
        {
            dgvData.Rows.Clear();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int dgvNo = 0;
                    if (!Type.Equals("") && Type.Equals("Invoice") && !DocNo.Equals(""))
                    {
                        //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                        dgvData.DataSource = db.tb_Stocks.Where(s => s.CodeNo == DocNo && s.Category == "Invoice"
                        ).OrderBy(o => o.CreateDate).ToList();
                    }
                    else if (!Type.Equals("") && Type.Equals("Temp") && !DocNo.Equals(""))
                    {
                        dgvData.DataSource = db.tb_Stocks.Where(s => s.CodeNo == DocNo && s.Category == "Temp"
                        ).OrderBy(o => o.CreateDate).ToList();
                    }
                    else if (!Type.Equals("") && Type.Equals("BackOrder") && !DocNo.Equals(""))
                    {
                        //dgvData.DataSource = db.tb_PurchaseRequestLines.Where(s => s.CodeNo == DocNo && s.SS ==1 && s.RemainQty >0
                        //).ToList();
                        var r = (from d in db.tb_PurchaseRequestLines
                                 join p in db.tb_PurchaseRequests on d.PRNo equals p.PRNo
                                 where d.CodeNo == DocNo && d.SS ==1
                                      && d.RemainQty > 0
                                 select new
                                 {
                                     CodeNo = d.CodeNo,
                                     Qty = d.RemainQty,
                                     AmountCost = d.Amount,
                                     UnitCost = d.StandardCost,
                                     CreateDate = p.CreateDate,
                                     App = "รอสินค้า"//d.Status
                                     ,
                                     Type = "-"
                                      ,
                                     Category = "BackOrder"
                                    ,
                                     DocNo = d.PRNo
                                    ,
                                     RefNo = "-"
                                     ,id = d.id
                                 }
                           ).ToList();
                        //dgvData.DataSource = StockControl.dbClss.LINQToDataTable(r);
                        if (r.Count > 0)
                        {
                             dgvNo = dgvData.Rows.Count() + 1;

                            foreach (var vv in r)
                            {
                                dgvData.Rows.Add(dgvNo.ToString(), vv.CodeNo, vv.App, vv.Type, vv.Category
                                    , vv.DocNo, vv.RefNo, vv.Qty, vv.UnitCost, vv.AmountCost, vv.CreateDate,"",0,0,0);
                            }

                        }

                    }

                    int c = 0;
                    foreach (var x in dgvData.Rows)
                    {
                        c += 1;
                        x.Cells["dgvNo"].Value = c;
                        
                        if(Convert.ToString(x.Cells["App"].Value).Equals("Cancel RC")) //หัวข้อ
                            x.Cells["App"].Value = "ยกเลิกการรับสินค้า";
                        else if (Convert.ToString(x.Cells["App"].Value).Equals("Receive")) //หัวข้อ
                            x.Cells["App"].Value = "รับสินค้า";
                        else if (Convert.ToString(x.Cells["App"].Value).Equals("Shipping")) //หัวข้อ
                            x.Cells["App"].Value = "เบิกสินค้า";
                        else if (Convert.ToString(x.Cells["App"].Value).Equals("Cancel SH")) //หัวข้อ                        
                            x.Cells["App"].Value = "ยกเลิกเบิกสินค้า";

                        //if (Convert.ToString(x.Cells["Type"].Value).Equals("ClearTemp")) //หัวข้อ
                        //    x.Cells["Type"].Value = "ยกเลิกการรับสินค้า";
                    }

                }
            }
            catch { }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Units where ix.UnitCode == code select ix).Count();
                if (i > 0)
                    ck = false;
                else
                    ck = true;
            }
            return ck;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
            //radGridView1.ReadOnly = true;
           
            //radGridView1.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //radGridView1.ReadOnly = false;
          
            //radGridView1.AllowAddNewRow = false;
            ////DataLoad();
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
           dbClss.ExportGridXlSX(dgvData);
        }



        private void btnFilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
