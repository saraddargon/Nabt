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
    public partial class UploadMapping : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public UploadMapping(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            this.Name = "UploadMapping";
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public UploadMapping()
        {
            this.Name = "UploadMapping";
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
           
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}
