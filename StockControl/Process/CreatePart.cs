using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Telerik.WinControls.Data;
using System.IO;

namespace StockControl
{
    public partial class CreatePart : Telerik.WinControls.UI.RadRibbonForm
    {
        public CreatePart()
        {
            InitializeComponent();
          
        }
        string CodeNo = "";
        public CreatePart(string CodeNo)
        {
            InitializeComponent();
            this.CodeNo = CodeNo;
        }

        private int Cath01 = 9;
        DataTable dt_Import = new DataTable();
        DataTable dt = new DataTable();
        DataTable dt_Part = new DataTable();
        string Ac = "";
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name,txtCodeNo.Text);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }
        private void GETDTRow()
        {
            dt.Columns.Add(new DataColumn("DefaultNo", typeof(bool)));
            dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ContactName", typeof(string)));
            dt.Columns.Add(new DataColumn("Tel", typeof(string)));
            dt.Columns.Add(new DataColumn("Fax", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));

            dt_Part = new DataTable();

            dt_Part.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("ItemNo", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("GroupCode", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("TypeCode", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("UnitShip", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("ShelfNo", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("StandardCost", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("CostingMethod", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("ItemGroup", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("Replacement", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("VendorItemName", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("UseTacking", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("Critical", typeof(bool)));
            dt_Part.Columns.Add(new DataColumn("Leadtime", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("MaximumStock", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("MinimumStock", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("SafetyStock", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("ReOrderPoint", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("StopOrder", typeof(bool)));
            dt_Part.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("Size", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("DWGNo", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("DWG", typeof(bool)));
            dt_Part.Columns.Add(new DataColumn("Maker", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("Toollife", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("SD", typeof(decimal)));
            dt_Part.Columns.Add(new DataColumn("BarCode", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("CreateBy", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt_Part.Columns.Add(new DataColumn("UpdateBy", typeof(string)));
            dt_Part.Columns.Add(new DataColumn("UpdateDate", typeof(DateTime)));


            //dt_Import
            dt_Import.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("ItemNo", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("GroupCode", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("TypeCode", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("UnitShip", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("ShelfNo", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("StandardCost", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("CostingMethod", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("ItemGroup", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("Replacement", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("VendorItemName", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("UseTacking", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("Critical", typeof(bool)));
            dt_Import.Columns.Add(new DataColumn("Leadtime", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("MaximumStock", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("MinimumStock", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("SafetyStock", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("ReOrderPoint", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("StopOrder", typeof(bool)));
            dt_Import.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("Size", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("DWGNo", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("DWG", typeof(bool)));
            dt_Import.Columns.Add(new DataColumn("Maker", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("Toollife", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("SD", typeof(decimal)));
            dt_Import.Columns.Add(new DataColumn("BarCode", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("CreateBy", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            dt_Import.Columns.Add(new DataColumn("UpdateBy", typeof(string)));
            dt_Import.Columns.Add(new DataColumn("UpdateDate", typeof(DateTime)));

           

        }
        private void Unit_Load(object sender, EventArgs e)
        {

            LoadPath_Dwg();
                
            //radGridView1.ReadOnly = true;
            //radGridView1.AutoGenerateColumns = false;
            this.cboGroupType.AutoFilter = true;
            this.cboGroupType.DisplayMember = "GroupCode";
            FilterDescriptor filter = new FilterDescriptor();
            filter.PropertyName = this.cboGroupType.DisplayMember;
            filter.Operator = FilterOperator.Contains;
            this.cboGroupType.AutoCompleteMode = AutoCompleteMode.Append;
            this.cboGroupType.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);

           // this.cboVendor.AutoFilter = true;
            this.cboVendor.AutoCompleteMode = AutoCompleteMode.Append;
            //this.cboVendor.DisplayMember = "VendorNo";
            //this.cboVendor.ValueMember = "VendorName";
            //FilterDescriptor fi = new FilterDescriptor();
            //fi.PropertyName = this.cboVendor.ValueMember;
            //fi.Operator = FilterOperator.StartsWith;
            //this.cboVendor.EditorControl.MasterTemplate.FilterDescriptors.Add(fi);

            txtCreateby.Text = ClassLib.Classlib.User;
            txtCreateDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            
            GETDTRow();
            Set_dt_Print();  // load data print

            LoadDefault();
            //cboVendor.Text = VNDR;
            //txtVenderName.Text = VNDRName;
            Cath01 = 9;

            Cleardata();
            if (!CodeNo.Equals(""))
            {
                txtCodeNo.Text = CodeNo;
                DataLoad();
                //View
                Enable_Status(false, "View");
                btnView.Enabled = false;
            }
            else
            {
                btnNew_Click(null, null);
                //New
                //Enable_Status(false, "-");
            }
        }
        private void Enable_Status(bool ss,string Condition)
        {
            if (Condition.Equals("-") || Condition.Equals("New"))
            {
                txtCodeNo.Enabled = ss;
                txtPartName.Enabled = ss;
                txtDetailPart.Enabled = ss;
                cboGroupType.Enabled = ss;
                cboTypeCode.Enabled = ss;
                cboVendor.Enabled = ss;
                txtMaker.Enabled = ss;
                txtStandCost.Enabled = ss;
                cboUnitBuy.Enabled = ss;
                cboUnitShipping.Enabled = ss;
                txtPCSUnit.Enabled = ss;
                txtLeadTime.Enabled = ss;
                ddlUseTacking.Enabled = ss;
                cboReplacement.Enabled = ss;
                chkStopOrder.Enabled = ss;

                //lblStock.Text = "0.00";
                //lblTempStock.Text = "0.00";
                //lblOrder.Text = "0.00";
                txtShelfNo.Enabled = ss;
                txtMimimumStock.Enabled = ss;
                txtMaximumStock.Enabled = ss;
                txtErrorLeadtime.Enabled = ss;
                txtReOrderPoint.Enabled = ss;
                txtToolLife.Enabled = ss;
                txtSize.Enabled = ss;
                txtRemark.Enabled = ss;
                txtDwgfile.Enabled = ss;

                btnAddDWG.Enabled = ss;
                btnDeleteDWG.Enabled = ss;
                chkGET.Checked = true;
                //btnGET.Enabled = true;
            }
            else if (Condition.Equals("View"))
            {
                txtCodeNo.Enabled = ss;
                txtPartName.Enabled = ss;
                txtDetailPart.Enabled = ss;
                cboGroupType.Enabled = ss;
                cboTypeCode.Enabled = ss;
                cboVendor.Enabled = ss;
                txtMaker.Enabled = ss;
                txtStandCost.Enabled = ss;
                cboUnitBuy.Enabled = ss;
                cboUnitShipping.Enabled = ss;
                txtPCSUnit.Enabled = ss;
                txtLeadTime.Enabled = ss;
                ddlUseTacking.Enabled = ss;
                cboReplacement.Enabled = ss;
                chkStopOrder.Enabled = ss;

                //lblStock.Text = "0.00";
                //lblTempStock.Text = "0.00";
                //lblOrder.Text = "0.00";
                txtShelfNo.Enabled = ss;
                txtMimimumStock.Enabled = ss;
                txtMaximumStock.Enabled = ss;
                txtErrorLeadtime.Enabled = ss;
                txtReOrderPoint.Enabled = ss;
                txtToolLife.Enabled = ss;
                txtSize.Enabled = ss;
                txtRemark.Enabled = ss;
                txtDwgfile.Enabled = ss;

                btnAddDWG.Enabled = ss;
                btnDeleteDWG.Enabled = ss;
                btnGET.Enabled = false;
                chkGET.Checked = false;
            }
            else if (Condition.Equals("Edit"))
            {
                txtCodeNo.Enabled = false;
                txtPartName.Enabled = ss;
                txtDetailPart.Enabled = ss;
                cboGroupType.Enabled = false;
                cboTypeCode.Enabled = ss;
                cboVendor.Enabled = ss;
                txtMaker.Enabled = ss;
                txtStandCost.Enabled = ss;
                cboUnitBuy.Enabled = ss;
                cboUnitShipping.Enabled = ss;
                txtPCSUnit.Enabled = ss;
                txtLeadTime.Enabled = ss;
                ddlUseTacking.Enabled = ss;
                cboReplacement.Enabled = ss;
                chkStopOrder.Enabled = ss;

                //lblStock.Text = "0.00";
                //lblTempStock.Text = "0.00";
                //lblOrder.Text = "0.00";
                txtShelfNo.Enabled = ss;
                txtMimimumStock.Enabled = ss;
                txtMaximumStock.Enabled = ss;
                txtErrorLeadtime.Enabled = ss;
                txtReOrderPoint.Enabled = ss;
                txtToolLife.Enabled = ss;
                txtSize.Enabled = ss;
                txtRemark.Enabled = ss;
                txtDwgfile.Enabled = ss;

                btnAddDWG.Enabled = ss;
                btnDeleteDWG.Enabled = ss;
                btnGET.Enabled = false;
                chkGET.Checked = false;
            }
        }
        private void LoadDefault()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboVendor.DisplayMember = "VendorName";
                cboVendor.ValueMember = "VendorNo";
                cboVendor.DataSource = db.tb_Vendors.Where(s => s.Active == true).ToList();
                cboVendor.SelectedIndex = -1;

                cboUnitBuy.DisplayMember = "UnitCode";
                cboUnitBuy.ValueMember = "UnitCode";
                cboUnitBuy.DataSource = db.tb_Units.Where(s => s.UnitActive == true).ToList();

                cboUnitShipping.DataSource = null;
                cboUnitShipping.DisplayMember = "UnitCode";
                cboUnitShipping.ValueMember = "UnitCode";
                cboUnitShipping.DataSource = db.tb_Units.Where(w => w.UnitActive == true).ToList();

                cboGroupType.DisplayMember = "GroupCode";
                cboGroupType.ValueMember = "GroupCode";
                cboGroupType.DataSource = db.tb_GroupTypes.Where(s => s.GroupActive == true).ToList();
                try
                {

                    cboGroupType.SelectedIndex = 0;

                    if (!cboGroupType.Text.Equals(""))
                    {
                        DefaultType();
                    }
                }
                catch { }



                
            }
        }
       private void LoadPath_Dwg()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var g = (from ix in db.tb_Paths
                         where ix.PathCode == "Drawing"
                         select ix).ToList();
                if (g.Count > 0)
                    lblPath.Text = StockControl.dbClss.TSt(g.FirstOrDefault().PathFile);
            }
        }
        private void DefaultType()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    cboTypeCode.DataSource = null;
                    cboTypeCode.DisplayMember = "TypeCode";
                    cboTypeCode.ValueMember = "TypeCode";
                    cboTypeCode.DataSource = db.tb_Types.Where(t => t.TypeActive == true && t.GroupCode.Equals(cboGroupType.Text)).ToList();
                    cboTypeCode.SelectedIndex = 0;
                }
            }
            catch { }
        }
        private void DataLoad()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtCodeNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        txtPartName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().ItemNo);
                        txtDetailPart.Text = StockControl.dbClss.TSt(g.FirstOrDefault().ItemDescription);
                        cboGroupType.Text = StockControl.dbClss.TSt(g.FirstOrDefault().GroupCode);
                        cboTypeCode.Text = StockControl.dbClss.TSt(g.FirstOrDefault().TypeCode);
                        cboVendor.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorItemName);
                        txtVenderName.Text = StockControl.dbClss.TSt(g.FirstOrDefault().VendorNo);
                        txtMaker.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Maker);
                        txtStandCost.Text = StockControl.dbClss.TSt(g.FirstOrDefault().StandardCost);
                        cboUnitBuy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().UnitBuy);
                        cboUnitShipping.Text = StockControl.dbClss.TSt(g.FirstOrDefault().UnitShip);
                        txtPCSUnit.Text = StockControl.dbClss.TSt(g.FirstOrDefault().PCSUnit);
                        txtLeadTime.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Leadtime);
                        ddlUseTacking.Text = StockControl.dbClss.TSt(g.FirstOrDefault().UseTacking);
                        cboReplacement.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Replacement);
                        chkStopOrder.Checked = StockControl.dbClss.TBo(g.FirstOrDefault().StopOrder);

                        txtShelfNo.Text = StockControl.dbClss.TSt(g.FirstOrDefault().ShelfNo);
                        txtMaximumStock.Text = StockControl.dbClss.TSt(g.FirstOrDefault().MaximumStock);
                        txtMimimumStock.Text = StockControl.dbClss.TSt(g.FirstOrDefault().MinimumStock);
                        txtErrorLeadtime.Text = StockControl.dbClss.TSt(g.FirstOrDefault().SD);
                        txtReOrderPoint.Text = StockControl.dbClss.TSt(g.FirstOrDefault().ReOrderPoint);

                        txtToolLife.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Toollife);
                        txtSize.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Size);
                        txtRemark.Text = StockControl.dbClss.TSt(g.FirstOrDefault().Remark);
                        txtDwgfile.Text = StockControl.dbClss.TSt(g.FirstOrDefault().DWGNo);
                        chkDWG.Checked = StockControl.dbClss.TBo(g.FirstOrDefault().DWG);
                        txtCreateby.Text = StockControl.dbClss.TSt(g.FirstOrDefault().CreateBy);
                        DateTime temp = Convert.ToDateTime(g.FirstOrDefault().CreateDate);
                        txtCreateDate.Text = temp.ToString("dd/MMM/yyyy");
                        txtUpdateBy.Text = StockControl.dbClss.TSt(g.FirstOrDefault().UpdateBy);
                        if (!txtUpdateBy.Text.Equals(""))
                        {
                            DateTime temp2 = Convert.ToDateTime(g.FirstOrDefault().UpdateDate);
                            txtUpdateDate.Text = temp2.ToString("dd/MMM/yyyy");
                        }
                        else
                            txtUpdateDate.Text = "";

                        lblStock.Text = StockControl.dbClss.TDe(g.FirstOrDefault().StockInv).ToString("###,###,##0.00");
                        lblTempStock.Text = StockControl.dbClss.TDe(g.FirstOrDefault().StockDL).ToString("###,###,##0.00");
                        lblOrder.Text = StockControl.dbClss.TDe(g.FirstOrDefault().StockBackOrder).ToString("###,###,##0.00");

                        //lblStock.Text = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "Invoice", 0)).ToString("###,###,##0.00"));
                        //lblTempStock.Text = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "Temp", 0)).ToString("###,###,##0.00"));
                        //lblOrder.Text = (Convert.ToDecimal(db.Cal_QTY(txtCodeNo.Text, "BackOrder", 0)).ToString("###,###,##0.00"));

                        if (StockControl.dbClss.TSt(g.FirstOrDefault().Status).Equals("InActive"))
                        {
                            btnSave.Enabled = false;
                            btnDelete.Enabled = false;
                            btnView.Enabled = false;
                            btnEdit.Enabled = false;
                            lbStatus.Text = "InActive";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnDelete.Enabled = true;
                            btnView.Enabled = true;
                            btnEdit.Enabled = true;
                            lbStatus.Text = "Active";
                        }
                        dt_Part = StockControl.dbClss.LINQToDataTable(g);
                    }

                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
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

        
      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            Cleardata();
            lbStatus.Text = "New";
            btnView.Enabled = true;
            btnEdit.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            //radGridView1.ReadOnly = false;
            //radGridView1.AllowAddNewRow = false;
            //radGridView1.Rows.AddNew();
            Enable_Status(true, "New");
            //btnGET.Enabled = true;
            chkGET.Enabled = true;

            Ac = "New";
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Ac = "View";
            btnView.Enabled = false;
            btnEdit.Enabled = true;
            btnNew.Enabled = true;
            Enable_Status(false, "View");
            btnGET.Enabled = false;
            chkGET.Enabled = false;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCodeNo.Text.Equals(""))
            {
                MessageBox.Show("ไม่สามารถทำการแก้ไขรายการได้");
            }
            else
            {
                btnView.Enabled = true;
                btnEdit.Enabled = false;
                btnNew.Enabled = true;
                lbStatus.Text = "Edit";
                Enable_Status(true, "Edit");
                btnGET.Enabled = false;
                chkGET.Enabled = false;
                Ac = "Edit";
            }
        }
       
      
        private bool AddPart()
        {
            bool ck = false;
            int C = 0;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (Ac.Equals("New"))  //New
                    {
                        
                        string Temp_codeno = txtCodeNo.Text;
                        string temp_codeno2 = "";
                        if (chkGET.Checked.Equals(false))// ให้ระบบ Gen ให้
                        {
                            if (txtCodeNo.Text.Length > 5)
                            {
                                int c = txtCodeNo.Text.Length;

                                temp_codeno2 = Temp_codeno.Substring(5, c - 5);
                                txtCodeNo.Text = Get_CodeNo();
                                txtCodeNo.Text = txtCodeNo.Text + temp_codeno2;
                            }
                            else
                                txtCodeNo.Text = Get_CodeNo();
                        }
                        //byte[] barcode = StockControl.dbClss.SaveQRCode2D(txtCodeNo.Text);
                         byte[] barcode = null; 

                        decimal StandardCost = 0; 
                        decimal MaximumStock = 0;
                        decimal MinimumStock = 0;
                        decimal SafetyStock = 0;
                        decimal ReOrderPoint = 0;
                        decimal SD = 0;
                        decimal Toollife = 0;
                        decimal Leadtime = 0;
                        bool Critical = false;
                        decimal PCSUnit = 0;
                        string CostingMethod = "";
                        string ItemGroup = "";
                        string UpdateBy = ClassLib.Classlib.User;
                        DateTime CreateDate = DateTime.Now;
                        decimal.TryParse(txtStandCost.Text, out StandardCost);
                        decimal.TryParse(txtMaximumStock.Text, out MaximumStock);
                        decimal.TryParse(txtMimimumStock.Text, out MinimumStock);
                        decimal.TryParse(txtReOrderPoint.Text, out ReOrderPoint);
                        decimal.TryParse(txtPCSUnit.Text, out PCSUnit);
                        decimal.TryParse(txtLeadTime.Text, out Leadtime);
                        decimal.TryParse(txtToolLife.Text, out Toollife);
                        decimal.TryParse(txtErrorLeadtime.Text, out SD);

                        DateTime? UpdateDate =null;

                        tb_Item u = new tb_Item();
                        u.CodeNo = txtCodeNo.Text.Trim();
                        u.ItemNo = txtPartName.Text.Trim();
                        u.ItemDescription = txtDetailPart.Text.Trim();
                        u.GroupCode = cboGroupType.Text;
                        u.TypeCode = cboTypeCode.Text;
                        u.UnitBuy = cboUnitBuy.Text;
                        u.VendorNo = txtVenderName.Text;
                        u.VendorItemName = cboVendor.Text;
                        u.Maker = txtMaker.Text.Trim();
                        u.StandardCost = StandardCost;
                        u.UnitShip = cboUnitShipping.Text;
                        u.PCSUnit = PCSUnit;
                        u.Leadtime = Leadtime;
                        u.UseTacking = ddlUseTacking.Text;
                        u.Replacement = cboReplacement.Text;
                        u.StopOrder = StockControl.dbClss.TBo(chkStopOrder.Checked);
                        u.ShelfNo = txtShelfNo.Text;
                        u.MinimumStock = MinimumStock;
                        u.MaximumStock = MaximumStock;
                        u.SD = SD;
                        u.ReOrderPoint = ReOrderPoint;
                        u.Toollife = Toollife;
                        u.Size = txtSize.Text;
                        u.Remark = txtRemark.Text;
                        u.CreateBy = UpdateBy;
                        u.CreateDate = CreateDate;
                        u.UpdateDate = UpdateDate;
                        u.UpdateBy = "";
                        u.SafetyStock = SafetyStock;
                        u.Critical = Critical;
                        u.Status = "Active";
                        u.CostingMethod = CostingMethod;
                        u.ItemGroup = ItemGroup;
                        u.BarCode = barcode;
                        u.DWGNo = txtDwgfile.Text;
                        u.DWG = chkDWG.Checked;
                        u.StockDL = 0;//Convert.ToDecimal(lblTempStock.Text);
                        u.StockInv = 0;// Convert.ToDecimal(lblStock.Text);
                        u.StockBackOrder = 0;
                        ///Save Drawing
                        if (chkDWG.Checked)
                        {
                            string tagetpart = lblPath.Text;
                            string FileName = lblTempAddFile.Text;
                            if (!System.IO.Directory.Exists(tagetpart))  //เช็คว่ามี partไฟล์เก็บหรือไม่ถ้าไม่ให้สร้างใหม่
                            {
                                System.IO.Directory.CreateDirectory(tagetpart);
                            }
                            //System.IO.File.Copy()

                            string File_temp = txtCodeNo.Text + "_" + ".pdf";//Path.GetExtension(AttachFile);  // IMG_IT-0123.jpg
                            File.Copy(FileName, tagetpart + File_temp, true);//ต้องทำเสมอ เป็นการ ก็อปปี้ Path เพื่อให้รูป มาว่างไว้ที่ path นี้ 

                            dbClss.AddHistory(this.Name , "เพิ่ม Drawing", "เพิ่มไฟล์ Drawing [" + txtCodeNo.Text.Trim() + "]", txtCodeNo.Text);
                        }
                        
                        //


                        db.tb_Items.InsertOnSubmit(u);
                        db.SubmitChanges();
                        C += 1;
                        dbClss.AddHistory(this.Name,"เพิ่มทูล" , "Insert Part [" + u.CodeNo + "]", txtCodeNo.Text);
                    }
                    else  //Edit
                    {
                        foreach (DataRow row in dt_Part.Rows)
                        {
                            var g = (from ix in db.tb_Items
                                     where ix.CodeNo.Trim() == txtCodeNo.Text.Trim() && ix.Status == "Active"
                                     select ix).ToList();
                            if (g.Count > 0)  //มีรายการในระบบ
                            {
                                var gg = (from ix in db.tb_Items
                                          where ix.CodeNo.Trim() == txtCodeNo.Text.Trim()
                                          select ix).First();
                                //gg.Status = "Active";
                                
                                    gg.UpdateBy = ClassLib.Classlib.User;
                                    gg.UpdateDate = DateTime.Now;
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขโดย [" + ClassLib.Classlib.User +" วันที่ :" +DateTime.Now.ToString("dd/MMM/yyyy")+ "]", txtCodeNo.Text);

                                //if(StockControl.dbClss.TSt(gg.BarCode).Equals(""))
                                //    gg.BarCode = StockControl.dbClss.SaveQRCode2D(txtCodeNo.Text);


                                if (!txtPartName.Text.Trim().Equals(row["ItemNo"].ToString()))
                                {
                                    gg.ItemNo = txtPartName.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขชื่อพาร์ท [" + txtPartName.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtDetailPart.Text.Trim().Equals(row["ItemDescription"].ToString()))
                                {
                                    gg.ItemDescription = txtDetailPart.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขรายละเอียดพาร์ท [" + txtDetailPart.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!cboGroupType.Text.Trim().Equals(row["GroupCode"].ToString()))
                                {
                                    gg.GroupCode = cboGroupType.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขประเภทกลุ่มสินค้า [" + cboGroupType.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!cboTypeCode.Text.Trim().Equals(row["TypeCode"].ToString()))
                                {
                                    gg.TypeCode = cboTypeCode.Text.Trim();
                                    dbClss.AddHistory(this.Name, "แก้ไข ทูล", "แก้ไขประเภทสินค้า [" + cboTypeCode.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!cboUnitBuy.Text.Trim().Equals(row["UnitBuy"].ToString()))
                                {
                                    gg.UnitBuy = cboUnitBuy.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขหน่วยซื้อ [" + cboUnitBuy.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!cboUnitShipping.Text.Trim().Equals(row["UnitShip"].ToString()))
                                {
                                    gg.UnitShip = cboUnitShipping.Text.Trim();
                                    dbClss.AddHistory(this.Name, "แก้ไข ทูล", "แก้ไขหน่วยขาย [" + cboUnitShipping.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtPCSUnit.Text.Trim().Equals(row["PCSUnit"].ToString()))
                                {
                                    decimal PCSUnit = 0; decimal.TryParse(txtPCSUnit.Text, out PCSUnit);
                                    gg.PCSUnit = PCSUnit;
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไข ชิ้น/หน่วย [" + PCSUnit.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtShelfNo.Text.Trim().Equals(row["ShelfNo"].ToString()))
                                {
                                    gg.ShelfNo = txtShelfNo.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขหน่วยขาย [" + txtShelfNo.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtStandCost.Text.Trim().Equals(row["StandardCost"].ToString()))
                                {
                                    decimal StandardCost = 0; decimal.TryParse(txtStandCost.Text, out StandardCost);
                                    gg.StandardCost = StandardCost;
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไข ราคาซื้อ/หน่วย [" + StandardCost.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!cboReplacement.Text.Trim().Equals(row["Replacement"].ToString()))
                                {
                                    gg.Replacement = cboReplacement.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขทดแทนด้วย(Replecement) [" + cboReplacement.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtVenderName.Text.Trim().Equals(row["VendorNo"].ToString()))
                                {
                                    gg.VendorNo = txtVenderName.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขรหัสผู้ขาย [" + txtVenderName.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!cboVendor.Text.Trim().Equals(row["VendorItemName"].ToString()))
                                {
                                    gg.VendorItemName = cboVendor.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขชื่อผู้ขาย [" + cboVendor.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!ddlUseTacking.Text.Trim().Equals(row["UseTacking"].ToString()))
                                {
                                    gg.UseTacking = ddlUseTacking.Text.Trim();
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไขควบคุม Lot (Use Tracking) [" + ddlUseTacking.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtLeadTime.Text.Trim().Equals(row["Leadtime"].ToString()))
                                {
                                    decimal Leadtime = 0; decimal.TryParse(txtLeadTime.Text, out Leadtime);
                                    gg.Leadtime = Leadtime;
                                    dbClss.AddHistory(this.Name, "แก้ไข ทูล", "แก้ไข ระยะเวลาซื้อ [" + txtLeadTime.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtMaximumStock.Text.Trim().Equals(row["MaximumStock"].ToString()))
                                {
                                    decimal MaximumStock = 0; decimal.TryParse(txtMaximumStock.Text, out MaximumStock);
                                    gg.MaximumStock = MaximumStock;
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไข MaximumStock [" + txtMaximumStock.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtMimimumStock.Text.Trim().Equals(row["MinimumStock"].ToString()))
                                {
                                    decimal MinimumStock = 0; decimal.TryParse(txtMimimumStock.Text, out MinimumStock);
                                    gg.MinimumStock = MinimumStock;
                                    dbClss.AddHistory(this.Name, "แก้ไข ทูล", "แก้ไข MinimumStock [" + txtMimimumStock.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtReOrderPoint.Text.Trim().Equals(row["ReOrderPoint"].ToString()))
                                {
                                    decimal ReOrderPoint = 0; decimal.TryParse(txtReOrderPoint.Text, out ReOrderPoint);
                                    gg.ReOrderPoint = ReOrderPoint;
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไข ReOrder Point [" + txtReOrderPoint.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!chkStopOrder.Checked.ToString().Trim().Equals(row["StopOrder"].ToString()))
                                {
                                    bool StopOrder = chkStopOrder.Checked;
                                    gg.StopOrder = StopOrder;
                                    dbClss.AddHistory(this.Name, "แก้ไข ทูล", "แก้ไข หยุดสั่งซื้อ (Stop Order) [" + chkStopOrder.Checked.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtRemark.Text.Trim().Equals(row["Remark"].ToString()))
                                {
                                    gg.Remark = txtRemark.Text.Trim();
                                    dbClss.AddHistory(this.Name  , "แก้ไข ทูล", "แก้ไข หมายเหตุ [" + txtRemark.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtSize.Text.Trim().Equals(row["Size"].ToString()))
                                {
                                    gg.Size = txtSize.Text.Trim();
                                    dbClss.AddHistory(this.Name  , "แก้ไข ทูล", "แก้ไข ขนาด [" + txtSize.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtMaker.Text.Trim().Equals(row["Maker"].ToString()))
                                {
                                    gg.Maker = txtMaker.Text.Trim();
                                    dbClss.AddHistory(this.Name  , "แก้ไข ทูล", "แก้ไข ผู้ผลิต [" + txtMaker.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!txtToolLife.Text.Trim().Equals(row["Toollife"].ToString()))
                                {
                                    decimal Toollife = 0; decimal.TryParse(txtToolLife.Text, out Toollife);
                                    gg.Toollife = Toollife;
                                    dbClss.AddHistory(this.Name , "แก้ไข ทูล", "แก้ไข อายุการใช้งาน(Toollife)  [" + txtToolLife.Text.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtErrorLeadtime.Text.Trim().Equals(row["SD"].ToString()))
                                {
                                    decimal SD = 0; decimal.TryParse(txtErrorLeadtime.Text, out SD);
                                    gg.SD = SD;
                                    dbClss.AddHistory(this.Name  , "แก้ไข ทูล", "แก้ไข Error Lead time [" + txtErrorLeadtime.Text.ToString() + "]", txtCodeNo.Text);
                                }
                                if (!txtDwgfile.Text.Trim().Equals(row["DWGNo"].ToString()))
                                {
                                    gg.DWGNo = txtDwgfile.Text.Trim();
                                    dbClss.AddHistory(this.Name  , "แก้ไข ทูล", "แก้ไข Drawing No [" + txtDwgfile.Text.Trim() + "]", txtCodeNo.Text);
                                }
                                if (!chkDWG.Checked.ToString().Trim().Equals(row["DWG"].ToString()))
                                {
                                    bool DWG = chkDWG.Checked;
                                    gg.DWG = DWG;
                                    dbClss.AddHistory(this.Name  , "แก้ไข ทูล", "แก้ไข Drawing [" + chkDWG.Checked.ToString() + "]", txtCodeNo.Text);
                                }

                                //if (txtMimimumStock.Text.Trim().Equals(row["SafetyStock"].ToString()))
                                //{
                                //    decimal SafetyStock = 0; decimal.TryParse(txtMimimumStock.Text, out MinimumStock);
                                //    gg.SafetyStock = SafetyStock;
                                //    dbClss.AddHistory(this.Name + txtCodeNo.Text, "แก้ไข Part", "แก้ไข SafetyStock [" + txtMimimumStock.ToString() + "]", "");
                                //}
                                //if (Critical.Text.Trim().Equals(row["Critical"].ToString()))
                                //{
                                //    gg.UseTacking = Critical.Text.Trim();
                                //    dbClss.AddHistory(this.Name + txtCodeNo.Text, "แก้ไข Part", "แก้ไขควบคุม Lot (Use Tracking) [" + ddlUseTacking.Text.Trim() + "]", "");
                                //}
                                //if (CostingMethod.Text.Trim().Equals(row["CostingMethod"].ToString()))
                                //{
                                //    gg.CostingMethod = CostingMethod.Text.Trim();
                                //    dbClss.AddHistory(this.Name  , txtCodeNo.Text, "แก้ไขหน่วยขาย [" + txtShelfNo.Text.Trim() + "]", "");
                                //}
                                //if (txtItemGroup.Text.Trim().Equals(row["ItemGroup"].ToString()))
                                //{
                                //    gg.ItemGroup = ItemGroup.Text.Trim();
                                //    dbClss.AddHistory(this.Name  , txtCodeNo.Text, "แก้ไขหน่วยขาย [" + txtShelfNo.Text.Trim() + "]", "");
                                //}

                                C += 1;
                                db.SubmitChanges();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("CreatePart", ex.Message, this.Name);
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool Check_Save()
        {
            bool re = true;
            string err = "";
            try
            {
                //if (txtCodeNo.Text.Equals(""))
                //    err += " “รหัสพาร์ท:” เป็นค่าว่าง \n";
                if (txtPartName.Text.Equals(""))
                    err += " “ชื่อทูล:” เป็นค่าว่าง \n";
                if (txtDetailPart.Text.Equals(""))
                    err += "- “รายละเอียดทูล:” เป็นค่าว่าง \n";
                if (cboGroupType.Text.Equals(""))
                    err += "- “ประเภทกลุ่ม สินค้า:” เป็นค่าว่าง \n";
                if (cboTypeCode.Text.Equals(""))
                    err += "- “ประเภทสินค้า:” เป็นค่าว่าง \n";
                if (cboVendor.Text.Equals(""))
                    err += "- “ชื่อผู้ขาย:” เป็นค่าว่าง \n";
                if (txtVenderName.Text.Equals(""))
                    err += "- “รหัสผู้ขาย:” เป็นค่าว่าง \n";
                if (txtMaker.Text.Equals(""))
                    err += "- “ผู้ผลิต:” เป็นค่าว่าง \n";
                if (txtStandCost.Text.Equals(""))
                    err += "- “ราคาซื้อ/หน่วย:” เป็นค่าว่าง \n";
                if (cboUnitBuy.Text.Equals(""))
                    err += "- “หน่วยซื้อ:” เป็นค่าว่าง \n";
                if (cboUnitShipping.Text.Equals(""))
                    err += "- “หน่วยขาย:” เป็นค่าว่าง \n";
                if (txtPCSUnit.Text.Equals(""))
                    err += "- “ชิ้น/หน่วย:” เป็นค่าว่าง \n";
                if (txtLeadTime.Text.Equals(""))
                    err += "- “ระยะเวลาซื้อ:” เป็นค่าว่าง \n";
                if (txtMaximumStock.Text.Equals(""))
                    err += "- “Maximum Stock:” เป็นค่าว่าง \n";
                if (txtMimimumStock.Text.Equals(""))
                    err += "- “Minimum Stock:” เป็นค่าว่าง \n";
                if (txtErrorLeadtime.Text.Equals(""))
                    err += "- “Error Lead time:” เป็นค่าว่าง \n";
                if (txtToolLife.Text.Equals(""))
                    err += "- “อายุการใช้งาน:” เป็นค่าว่าง \n";

                //---------------check codeno -------------------//
                if (Ac.Equals("New"))  //New
                {
                    if (chkGET.Checked)
                    {
                        if (txtCodeNo.Text.Trim().Equals(""))
                        {
                            err += " “รหัสทูล:” เป็นค่าว่าง \n";
                        }
                        else //เช็คว่า เลข Gen ด้านหน้าเป็น เลข Group เดียวกันหรือไม่ ถ้าไม่ใช่จะขึ้น Error
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                string Temp_Running = "";
                                var I = (from ix in db.tb_GroupTypes select ix).Where(a => a.GroupCode == cboGroupType.Text).ToList();
                                if (I.Count > 0)
                                    Temp_Running = I.FirstOrDefault().Running;

                                if (!Temp_Running.Equals(""))
                                {
                                    string cut_string = "";
                                    cut_string = txtCodeNo.Text.Trim().Substring(0, 1);
                                    if (!cut_string.ToUpper().Equals(Temp_Running.ToUpper()))
                                        err += "- “รหัสทูล เริ่มต้นไม่ตรงกับประเภทกลุ่มสินค้า:”  \n";
                                    else//เช็คว่าเป็น CodeNo ที่มีในระบบหรือไม่ ถ้ามีแล้วจะ New เลขใหม่ไม่ได้ เพราะซ้ำ
                                    {
                                        var g1 = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtCodeNo.Text.Trim()).ToList();
                                        if (g1.Count() > 0)
                                        {
                                            err += "- “รหัสทูล ซ้ำ:”มีรหัสทูล ในระบบแล้ว  \n";
                                        }
                                    }
                                }
                                //err += "- “ประเภทกลุ่ม สินค้า:” เป็นค่าว่าง \n";
                            }
                        }
                    }
                }
                //-----------------------------------------------//



                if (!err.Equals(""))
                    MessageBox.Show(err);
                else
                    re = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbClss.AddError("CreatePart", ex.Message, this.Name);
            }

            return re;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Save())
                    return;
                else
                {
                    if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AddPart();
                        DataLoad();
                       
                        Ac = "View";
                        btnView.Enabled = false;
                        btnEdit.Enabled = true;
                        btnNew.Enabled = true;
                        Enable_Status(false, "View");
                        chkGET.Enabled = false;
                        btnGET.Enabled = false;
                    }
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                //radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
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
                    //AddUnit();
                    //DataLoad();
                }
            }
        }

        private void Cleardata()
        {
            chkGET.Checked = true;
            btnGET.Enabled = false;
            lblStock.Text = "0.00";
            lblTempStock.Text = "0.00";
            lblOrder.Text = "0.00";

            txtCodeNo.Text = "";
            txtPartName.Text = "";
            txtDetailPart.Text = "";
            cboGroupType.Text = "";
            cboTypeCode.Text = "";
            cboVendor.Text = "";
            txtVenderName.Text = "";
            txtMaker.Text = "";
            txtStandCost.Text = "0.00";
            cboUnitBuy.Text = "PCS";
            cboUnitShipping.Text = "PCS";
            txtPCSUnit.Text = "1.0";
            txtLeadTime.Text = "7";
            ddlUseTacking.Text = "";
            cboReplacement.Text = "";
            chkStopOrder.Checked = false;
            txtShelfNo.Text = "";
            txtMaximumStock.Text = "1.00";
            txtMimimumStock.Text = "0.00";
            txtErrorLeadtime.Text = "0.00";
            txtReOrderPoint.Text = "0.00";
            txtToolLife.Text = "1.00";
            txtSize.Text = "";
            txtRemark.Text = "";
            txtDwgfile.Text = "";
            lbStatus.Text = "-";
            txtUpdateBy.Text = "";
            txtUpdateDate.Text = "";

            txtCreateby.Text = ClassLib.Classlib.User;
            txtCreateDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            chkDWG.Checked = false;
            lblTempAddFile.Text = "";
            dt_Part.Rows.Clear();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Ac = "Del";
                if (MessageBox.Show("ต้องการลบรายการ ( " + txtCodeNo.Text + " ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var g = (from ix in db.tb_Items
                                 where ix.CodeNo.Trim() == txtCodeNo.Text.Trim() && ix.Status == "Active"
                                 select ix).ToList();
                        if (g.Count > 0)  //มีรายการในระบบ
                        {
                                     var gg = (from ix in db.tb_Items
                                             where ix.CodeNo.Trim() == txtCodeNo.Text.Trim()
                                          select ix).First();
                               gg.Status = "InActive";
                               gg.UpdateBy = ClassLib.Classlib.User;
                               gg.UpdateDate = DateTime.Now;
                            dbClss.AddHistory(this.Name  , "ลบทูล", "ลบทูล [" +txtCodeNo.Text.Trim() + "]", txtCodeNo.Text);                              
                            
                            db.SubmitChanges();
                            btnNew_Click(null, null);
                            btnSave.Enabled = true;
                            btnGET.Enabled = false;
                            chkGET.Checked = false;
                        }
                        else // ไม่มีในระบบ
                        {
                            //btnGET.Enabled = true;
                            chkGET.Checked = true;
                            Cleardata();
                            Enable_Status(true, "New");
                            btnSave.Enabled = true;
                        }
                    }

                    MessageBox.Show("ลบรายการ สำเร็จ!");
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
            //dbClss.ExportGridXlSX(radGridView1);
            try
            {
                string tagetpart= System.IO.Path.GetTempPath();
                string Name = "Excel_001_Part_Export";
                string FileName = AppDomain.CurrentDomain.BaseDirectory + "Report\\Excel_001_Part_Export.xlsx";
               //string  FileOpen = Path.GetTempPath() + "Excel_001_Part_Export.xlsx";

                if (!System.IO.Directory.Exists(tagetpart))  //เช็คว่ามี partไฟล์เก็บหรือไม่ถ้าไม่ให้สร้างใหม่
                {
                    System.IO.Directory.CreateDirectory(tagetpart);
                }


                string File_temp = Name + "" + Path.GetExtension(FileName);  // IMG_IT-0123.jpg
                File.Copy(FileName, tagetpart + File_temp, true);//ต้องทำเสมอ เป็นการ ก็อปปี้ Path เพื่อให้รูป มาว่างไว้ที่ path นี้ 
                MessageBox.Show("Export Finished");
                System.Diagnostics.Process.Start(tagetpart + File_temp);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Import_1()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            if (op.ShowDialog() == DialogResult.OK)
            {

                using (TextFieldParser parser = new TextFieldParser(op.FileName))
                {
                    dt_Import.Rows.Clear();
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int a = 0;
                    int c = 0;
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        a += 1;
                        DataRow rd = dt_Kanban.NewRow();
                        //// MessageBox.Show(a.ToString());
                        string[] fields = parser.ReadFields();
                        c = 0;
                        foreach (string field in fields)
                        {
                            c += 1;
                            ////TODO: Process field
                            //    // MessageBox.Show(field);
                            if (a > 7)
                            {
                                if (c == 2)
                                    rd["CodeNo"] = Convert.ToString(field);
                                else if (c == 3)
                                    rd["ItemNo"] = StockControl.dbClss.TSt(field);
                                else if (c == 4)
                                    rd["ItemDescription"] = Convert.ToString(field);
                                else if (c == 5)
                                    rd["GroupCode"] = Convert.ToString(field);
                                else if (c == 6)
                                    rd["TypeCode"] = Convert.ToString(field);
                                else if (c == 7)
                                    rd["VendorNo"] = Convert.ToString(field);
                                else if (c == 8)
                                    rd["VendorItemName"] = Convert.ToString(field);
                                else if (c == 9)
                                    rd["StandardCost"] = StockControl.dbClss.TDe(field);
                                else if (c == 10)
                                    rd["UnitBuy"] = Convert.ToString(field);
                                else if (c == 11)
                                    rd["UnitShip"] = Convert.ToString(field);
                                else if (c == 12)
                                    rd["PCSUnit"] = StockControl.dbClss.TDe(field);
                                else if (c == 13)
                                    rd["Leadtime"] = StockControl.dbClss.TDe(field);
                                else if (c == 14)
                                    rd["UseTacking"] = Convert.ToString(field);
                                else if (c == 15)
                                    rd["Replacement"] = Convert.ToString(field);
                                else if (c == 16)
                                    rd["StopOrder"] = StockControl.dbClss.TBo(field);
                                else if (c == 17)
                                    rd["ShelfNo"] = Convert.ToString(field);
                                else if (c == 18)
                                    rd["MaximumStock"] = StockControl.dbClss.TDe(field);
                                else if (c == 19)
                                    rd["MinimumStock"] = StockControl.dbClss.TDe(field);
                                else if (c == 20)
                                    rd["SD"] = StockControl.dbClss.TDe(field);
                                else if (c == 21)
                                    rd["ReOrderPoint"] = StockControl.dbClss.TDe(field);
                                else if (c == 22)
                                    rd["Toollife"] = StockControl.dbClss.TDe(field);
                                else if (c == 23)
                                    rd["Size"] = Convert.ToString(field);
                                else if (c == 24)
                                    rd["Remark"] = Convert.ToString(field);
                                else if (c == 25)
                                    rd["DWGNo"] = Convert.ToString(field);
                                else if (c == 26)
                                    rd["DWG"] = StockControl.dbClss.TBo(field);
                                else if (c == 27)
                                    rd["CostingMethod"] = Convert.ToString(field);
                                else if (c == 28)
                                    rd["ItemGroup"] = Convert.ToString(field);
                                else if (c == 29)
                                    rd["Critical"] = Convert.ToString(field);
                                else if (c == 30)
                                    rd["SafetyStock"] = StockControl.dbClss.TDe(field);
                                else if (c == 31)
                                    rd["Status"] = Convert.ToString(field);
                                else if (c == 32)
                                    rd["BarCode"] = Convert.ToString(field);
                                else if (c == 33)
                                    rd["CreateBy"] = Convert.ToString(field);
                                else if (c == 34)
                                    rd["CreateDate"] = StockControl.dbClss.TDa(field);
                                //else if (c == 35)
                                //    rd["UpdateBy"] = Convert.ToString(field);
                                //else if (c == 36)
                                //    rd["UpdateDate"] = StockControl.dbClss.TDa(field);

                            }

                        }
                        dt_Kanban.Rows.Add(rd);
                    }
                }
                if (dt_Import.Rows.Count > 0)
                {
                    dbClss.AddHistory(this.Name  , "Import ทูล", "Import file CSV in to System", "Import ทูล");
                    //ImportData();
                    MessageBox.Show("Import Completed.");

                    //DataLoad();
                }

            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            if (op.ShowDialog() == DialogResult.OK)
            {
                using (TextFieldParser parser = new TextFieldParser(op.FileName, Encoding.GetEncoding("windows-874")))
                {
                    this.Cursor = Cursors.WaitCursor;
                    //using (TextFieldParser parser = new TextFieldParser(op.FileName), Encoding.GetEncoding("windows-874")))
                    //{
                        dt_Import.Rows.Clear();
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");
                        int a = 0;
                        int c = 0;

                        string CodeNo = "";
                        string ItemNo = "";
                        string ItemDescription = "";
                        string GroupCode = "";
                        string TypeCode = "";
                        string VendorNo = "";
                        string VendorItemName = "";
                        string Maker = "";
                        decimal StandardCost = 0;
                        string UnitBuy = "";
                        string UnitShip = "";
                        decimal PCSUnit = 0;
                        decimal Leadtime = 0;
                        string UseTacking = "";
                        string Replacement = "";
                        bool StopOrder = false;
                        string ShelfNo = "";
                        decimal MaximumStock = 0;
                        decimal MinimumStock = 0;
                        decimal SD = 0;
                        decimal ReOrderPoint = 0;
                        decimal Toollife = 0;
                        string Size = "";
                        string Remark = "";
                        string DWGNo = "";
                        bool DWG = false;
                        string CostingMethod = "";
                        string ItemGroup = "";
                        bool Critical = false;
                        decimal SafetyStock = 0;
                        string Status = "";
                        string BarCode = "";
                        string CreateBy = "";
                        DateTime? CreateDate = DateTime.Now;
                        string UpdateBy = "";
                        DateTime? UpdateDate = DateTime.Now;

                        while (!parser.EndOfData)
                        {
                            //Processing row
                            a += 1;
                            //DataRow rd = dt_Kanban.NewRow();
                            //// MessageBox.Show(a.ToString());
                            CodeNo = "";
                            ItemNo = "";
                            ItemDescription = "";
                            GroupCode = "";
                            TypeCode = "";
                            VendorNo = "";
                            VendorItemName = "";
                            Maker = "";
                            StandardCost = 0;
                            UnitBuy = "";
                            UnitShip = "";
                            PCSUnit = 0;
                            Leadtime = 0;
                            UseTacking = "";
                            Replacement = "";
                            StopOrder = false;
                            ShelfNo = "";
                            MaximumStock = 0;
                            MinimumStock = 0;
                            SD = 0;
                            ReOrderPoint = 0;
                            Toollife = 0;
                            Size = "";
                            Remark = "";
                            DWGNo = "";
                            DWG = false;
                            CostingMethod = "";
                            ItemGroup = "";
                            Critical = false;
                            SafetyStock = 0;
                            Status = "";
                            BarCode = "";
                            CreateBy = "";
                            CreateDate = DateTime.Now;
                            UpdateBy = "";
                            UpdateDate = DateTime.Now;

                            string[] fields = parser.ReadFields();
                            c = 0;
                            foreach (string field in fields)
                            {
                                c += 1;
                                ////TODO: Process field
                                //    // MessageBox.Show(field);
                                if (a > 7)
                                {
                                    if (c == 3 && Convert.ToString(field).Equals(""))
                                    {
                                        break;
                                    }

                                    if (c == 2)
                                        CodeNo = Convert.ToString(field);
                                    else if (c == 3)
                                        ItemNo = StockControl.dbClss.TSt(field);
                                    else if (c == 4)
                                        ItemDescription = Convert.ToString(field);
                                    else if (c == 5)
                                        GroupCode = Convert.ToString(field);
                                    else if (c == 6)
                                        TypeCode = Convert.ToString(field);
                                    else if (c == 7)
                                        VendorNo = Convert.ToString(field);
                                    else if (c == 8)
                                        VendorItemName = Convert.ToString(field);
                                    else if (c == 9)
                                        Maker = Convert.ToString(field);
                                    else if (c == 10)
                                        decimal.TryParse(Convert.ToString(field), out StandardCost); //StockControl.dbClss.TDe(field);
                                    else if (c == 11)
                                        UnitBuy = Convert.ToString(field);
                                    else if (c == 12)
                                        UnitShip = Convert.ToString(field);
                                    else if (c == 13)
                                        decimal.TryParse(Convert.ToString(field), out PCSUnit); //StockControl.dbClss.TDe(field);
                                    else if (c == 14)
                                        decimal.TryParse(Convert.ToString(field), out Leadtime);//= StockControl.dbClss.TDe(field);
                                    else if (c == 15)
                                        UseTacking = Convert.ToString(field);
                                    else if (c == 16)
                                        Replacement = Convert.ToString(field);
                                    else if (c == 17)
                                        StopOrder = StockControl.dbClss.TBo(field);
                                    else if (c == 18)
                                        ShelfNo = Convert.ToString(field);
                                    else if (c == 19)
                                        decimal.TryParse(Convert.ToString(field), out MaximumStock);//= StockControl.dbClss.TDe(field);
                                    else if (c == 20)
                                        decimal.TryParse(Convert.ToString(field), out MinimumStock);// = StockControl.dbClss.TDe(field);
                                    else if (c == 21)
                                        decimal.TryParse(Convert.ToString(field), out SD);// = StockControl.dbClss.TDe(field);
                                    else if (c == 22)
                                        decimal.TryParse(Convert.ToString(field), out ReOrderPoint);// = StockControl.dbClss.TDe(field);
                                    else if (c == 23)
                                        decimal.TryParse(Convert.ToString(field), out Toollife);// = StockControl.dbClss.TDe(field);
                                    else if (c == 24)
                                        Size = Convert.ToString(field);
                                    else if (c == 25)
                                        Remark = Convert.ToString(field);
                                    else if (c == 26)
                                        DWGNo = Convert.ToString(field);
                                    else if (c == 27)
                                        DWG = StockControl.dbClss.TBo(field);
                                    else if (c == 28)
                                        CostingMethod = Convert.ToString(field);
                                    else if (c == 29)
                                        ItemGroup = Convert.ToString(field);
                                    else if (c == 30)
                                        Critical = StockControl.dbClss.TBo(field);
                                    else if (c == 31)
                                        decimal.TryParse(Convert.ToString(field), out SafetyStock);// = StockControl.dbClss.TDe(field);
                                    else if (c == 32)
                                        Status = Convert.ToString(field);
                                    else if (c == 33)
                                        BarCode = Convert.ToString(field);
                                    else if (c == 34)
                                        CreateBy = Convert.ToString(field);
                                    else if (c == 35 && !Convert.ToString(field).Equals(""))
                                        CreateDate = Convert.ToDateTime(StockControl.dbClss.TDa(field));
                                    //else if (c == 36)
                                    //    rd["UpdateBy"] = Convert.ToString(field);
                                    //else if (c == 37)
                                    //    rd["UpdateDate"] = StockControl.dbClss.TDa(field);

                                }

                            }

                        if (!GroupCode.Equals("")&& !TypeCode.Equals(""))
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                var g = (from ix in db.tb_Items
                                         where ix.CodeNo.Trim().ToUpper() == CodeNo.Trim().ToUpper() //&& ix.Status == "Active"
                                         select ix).ToList();
                                if (g.Count <= 0)
                                {
                                    //CodeNo = Get_CodeNo_GroupCode(GroupCode);
                                    string Temp_codeno = CodeNo;
                                    string temp_codeno2 = "";
                                    if (CodeNo.Length > 5)
                                    {
                                        int c1 = txtCodeNo.Text.Length;

                                        temp_codeno2 = Temp_codeno.Substring(5, c1 - 5);
                                        CodeNo = Get_CodeNo_GroupCode(GroupCode);
                                        CodeNo = CodeNo + temp_codeno2;
                                    }
                                    else
                                        CodeNo = Get_CodeNo_GroupCode(GroupCode);
           
                                }
                            }
                        }

                            //dt_Kanban.Rows.Add(rd);
                        if (CodeNo.ToString().Equals("") || ItemNo.ToString().Equals("")
                               || ItemDescription.ToString().Equals("") || GroupCode.ToString().Equals("")
                               || TypeCode.ToString().Equals("") || VendorNo.ToString().Equals("") || VendorItemName.ToString().Equals("")
                               || Maker.ToString().Equals("") || StandardCost.ToString().Equals("") || UnitBuy.ToString().Equals("")
                               || UnitShip.ToString().Equals("") || PCSUnit.ToString().ToString().Equals("") || Leadtime.ToString().Equals("")
                               || MaximumStock.ToString().Equals("") || MinimumStock.ToString().Equals("") || SD.ToString().Equals("")
                               )
                            {

                            }
                            else
                            {
                                //if (Status.Equals(""))
                                Status = "Active";
                                if (CreateBy.Equals(""))
                                    CreateBy = ClassLib.Classlib.User;

                                dt_Import.Rows.Add(CodeNo, ItemNo, ItemDescription, GroupCode
                                                               , TypeCode, UnitBuy, UnitShip, PCSUnit, ShelfNo, StandardCost,
                                                               CostingMethod, ItemGroup, Replacement, VendorNo, VendorItemName, UseTacking
                                                               , Critical, Leadtime, MaximumStock, MinimumStock
                                                               , SafetyStock, ReOrderPoint, Status, StopOrder, Remark
                                                               , Size, DWGNo, DWG, Maker, Toollife, SD
                                                               , BarCode, CreateBy, CreateDate, UpdateBy, UpdateDate);
                            }
                        }


                    }
                    if (dt_Import.Rows.Count > 0)
                    {
                        dbClss.AddHistory(this.Name, "Import ทูล", "Import file CSV in to System", "Import ทูล");
                        ImportData();
                        MessageBox.Show("Import Completed.");

                        //DataLoad();
                    }
                //}
            }
            this.Cursor = Cursors.Default;

        }

        private void ImportData()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   
                    foreach (DataRow rd in dt_Import.Rows)
                    {
                        if (!rd["CodeNo"].ToString().Equals(""))
                        {

                            var g = (from ix in db.tb_Items
                                     where ix.CodeNo.Trim() == rd["CodeNo"].ToString().Trim() //&& ix.Status == "Active"
                                     select ix).ToList();
                            if (g.Count > 0)  //มีรายการในระบบ อัพเดต
                            {

                                var gg = (from ix in db.tb_Items
                                          where ix.CodeNo.Trim() == rd["CodeNo"].ToString().Trim()
                                          select ix).First();

                                gg.UpdateBy = rd["CreateBy"].ToString().Trim();
                                gg.UpdateDate = Convert.ToDateTime(rd["CreateDate"].ToString()); //DateTime.Now;
                                dbClss.AddHistory(this.Name  , "แก้ไข ทูล", " แก้ไข ทูล โดย Import โดย [" + ClassLib.Classlib.User + " วันที่ :" + DateTime.Now.ToString("dd/MMM/yyyy") + "]", rd["CodeNo"].ToString());

                                //if (StockControl.dbClss.TSt(gg.BarCode).Equals(""))
                                //    gg.BarCode = StockControl.dbClss.SaveQRCode2D(rd["CodeNo"].ToString());

                                gg.ItemNo = rd["ItemNo"].ToString().Trim();
                                gg.ItemDescription = rd["ItemDescription"].ToString().Trim();
                                gg.GroupCode = rd["GroupCode"].ToString().Trim();
                                gg.TypeCode = rd["TypeCode"].ToString().Trim();
                                gg.UnitBuy = rd["UnitBuy"].ToString().Trim();
                                gg.UnitShip = rd["UnitShip"].ToString().Trim();
                                decimal PCSUnit = 0; decimal.TryParse(rd["PCSUnit"].ToString(), out PCSUnit);
                                gg.PCSUnit = PCSUnit;
                                gg.ShelfNo = rd["ShelfNo"].ToString().Trim();
                                decimal StandardCost = 0; decimal.TryParse(rd["StandardCost"].ToString(), out StandardCost);
                                gg.StandardCost = StandardCost;
                                gg.Replacement = rd["Replacement"].ToString().Trim();
                                gg.VendorNo = rd["VendorNo"].ToString().Trim();
                                gg.VendorItemName = rd["VendorItemName"].ToString().Trim();
                                gg.UseTacking = rd["UseTacking"].ToString().Trim();
                                decimal Leadtime = 0; decimal.TryParse(rd["Leadtime"].ToString(), out Leadtime);
                                gg.Leadtime = Leadtime;
                                decimal MaximumStock = 0; decimal.TryParse(rd["MaximumStock"].ToString(), out MaximumStock);
                                gg.MaximumStock = MaximumStock;
                                decimal MinimumStock = 0; decimal.TryParse(rd["MinimumStock"].ToString(), out MinimumStock);
                                gg.MinimumStock = MinimumStock;
                                decimal ReOrderPoint = 0; decimal.TryParse(rd["ReOrderPoint"].ToString(), out ReOrderPoint);
                                gg.ReOrderPoint = ReOrderPoint;
                                bool StopOrder = StockControl.dbClss.TBo(rd["StopOrder"]);
                                gg.StopOrder = StopOrder;
                                gg.Remark = rd["Remark"].ToString().Trim();
                                gg.Size = rd["Size"].ToString();
                                gg.Maker = rd["Maker"].ToString().Trim();
                                decimal Toollife = 0; decimal.TryParse(rd["Toollife"].ToString(), out Toollife);
                                gg.Toollife = Toollife;
                                decimal SD = 0; decimal.TryParse(rd["SD"].ToString(), out SD);
                                gg.SD = SD;
                                gg.DWGNo = rd["DWGNo"].ToString().Trim();
                                bool DWG = StockControl.dbClss.TBo(rd["DWG"]);
                                gg.DWG = DWG;

                                db.SubmitChanges();
                            }
                            else   // Add ใหม่
                            {
                                // byte[] barcode = StockControl.dbClss.SaveQRCode2D(rd["CodeNo"].ToString().Trim());
                                decimal StockDL = 0;
                                decimal StockInv = 0;
                                decimal StockBackOrder = 0;

                                decimal StandardCost = 0;
                                decimal MaximumStock = 0;
                                decimal MinimumStock = 0;
                                decimal SafetyStock = 0;
                                decimal ReOrderPoint = 0;
                                decimal SD = 0;
                                decimal Toollife = 0;
                                decimal Leadtime = 0;
                                bool Critical = false;
                                decimal PCSUnit = 0;
                                string CostingMethod = "";
                                string ItemGroup = "";
                                string UpdateBy = ClassLib.Classlib.User;
                                DateTime CreateDate = DateTime.Now;
                                UpdateBy = rd["CreateBy"].ToString().Trim();
                                CreateDate = Convert.ToDateTime(rd["CreateDate"].ToString()); //DateTime.Now;

                                decimal.TryParse(rd["StandardCost"].ToString(), out StandardCost);
                                decimal.TryParse(rd["MaximumStock"].ToString(), out MaximumStock);
                                decimal.TryParse(rd["MinimumStock"].ToString(), out MinimumStock);
                                decimal.TryParse(rd["ReOrderPoint"].ToString(), out ReOrderPoint);
                                decimal.TryParse(rd["PCSUnit"].ToString(), out PCSUnit);
                                decimal.TryParse(rd["Leadtime"].ToString(), out Leadtime);
                                decimal.TryParse(rd["Toollife"].ToString(), out Toollife);
                                if (Toollife < 0)
                                    Toollife = 1;

                                decimal.TryParse(rd["SD"].ToString(), out SD);

                                DateTime? UpdateDate = null;

                                tb_Item u = new tb_Item();
                                u.CodeNo = rd["CodeNo"].ToString().Trim();
                                u.ItemNo = rd["ItemNo"].ToString().Trim();
                                u.ItemDescription = rd["ItemDescription"].ToString().Trim();
                                u.GroupCode = rd["GroupCode"].ToString();
                                u.TypeCode = rd["TypeCode"].ToString();
                                u.UnitBuy = rd["UnitBuy"].ToString();
                                u.VendorNo = rd["VendorNo"].ToString();
                                u.VendorItemName = rd["VendorItemName"].ToString().Trim();
                                u.Maker = rd["Maker"].ToString().Trim();
                                u.StandardCost = StandardCost;
                                u.UnitShip = rd["UnitShip"].ToString();
                                u.PCSUnit = PCSUnit;
                                u.Leadtime = Leadtime;
                                u.UseTacking = rd["UseTacking"].ToString();
                                u.Replacement = rd["Replacement"].ToString();
                                u.StopOrder = StockControl.dbClss.TBo(rd["StopOrder"]);
                                u.ShelfNo = rd["ShelfNo"].ToString();
                                u.MinimumStock = MinimumStock;
                                u.MaximumStock = MaximumStock;
                                u.SD = SD;
                                u.ReOrderPoint = ReOrderPoint;
                                u.Toollife = Toollife;
                                u.Size = rd["Size"].ToString();
                                u.Remark = rd["Remark"].ToString();
                                u.CreateBy = UpdateBy;
                                u.CreateDate = CreateDate;
                                u.UpdateDate = UpdateDate;
                                u.UpdateBy = "";
                                u.SafetyStock = SafetyStock;
                                u.Critical = Critical;
                                u.Status = rd["Status"].ToString();
                                u.CostingMethod = CostingMethod;
                                u.ItemGroup = ItemGroup;
                                u.BarCode = null;// barcode;
                                u.DWGNo = rd["DWGNo"].ToString();
                                u.DWG = StockControl.dbClss.TBo(rd["DWG"]);
                                u.StockDL = StockDL;
                                u.StockInv = StockInv;
                                u.StockBackOrder = StockBackOrder;

                                db.tb_Items.InsertOnSubmit(u);
                                db.SubmitChanges();

                                dbClss.AddHistory(this.Name  ,"เพิ่ม ทูล", "เพิ่ม ทูล โดย Import [" + u.CodeNo + "]", u.CodeNo);

                            }
                        }
                    }
                   
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("ImportData Part", ex.Message, this.Name);
            }
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
           // radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
           // radGridView1.EnableFiltering = false;
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

                //VNDR = cboVendor.Text;
                //VNDRName = txtVenderName.Text;
                DataLoad();
            }
        }

        private void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if(Cath01==0)
                //txtVenderName.Text = cboVendor.SelectedValue.ToString();
                if (!cboVendor.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var I = (from ix in db.tb_Vendors select ix).Where(a => a.VendorName == cboVendor.Text).ToList();
                        if (I.Count > 0)
                            txtVenderName.Text = I.FirstOrDefault().VendorNo;
                    }
                }
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

        private void radLabel25_Click(object sender, EventArgs e)
        {

        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void radLabel27_Click(object sender, EventArgs e)
        {

        }

        private void radTextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (!txtDwgfile.Text.Trim().Equals(""))
            {

                //OpenFileDialog op = new OpenFileDialog();
                //op.Filter = "PDF files (*.pdf)|*.pdf";
                //op.FileName = txtDwgfile.Text;
                //op.ShowDialog();
                try
                {

                    OpenFileDialog op = new OpenFileDialog();
                    op.DefaultExt = "*.pdf";
                    op.AddExtension = true;
                    op.FileName = "";
                    op.Filter = "PDF files (*.pdf)|*.pdf";
                    this.Cursor = Cursors.WaitCursor;
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        string FileName = op.FileName;
                        string tagetpart = lblPath.Text;

                        if (!Ac.Equals("New")) // save ได้เรย
                        {
                            if (!System.IO.Directory.Exists(tagetpart))  //เช็คว่ามี partไฟล์เก็บหรือไม่ถ้าไม่ให้สร้างใหม่
                            {
                                System.IO.Directory.CreateDirectory(tagetpart);
                            }
                            //System.IO.File.Copy()

                            string File_temp = txtCodeNo.Text + "_" + ".pdf";//Path.GetExtension(AttachFile);  // IMG_IT-0123.jpg
                            File.Copy(FileName, tagetpart + File_temp, true);//ต้องทำเสมอ เป็นการ ก็อปปี้ Path เพื่อให้รูป มาว่างไว้ที่ path นี้ 

                            if(chkDWG.Checked)
                                dbClss.AddHistory(this.Name  , "แก้ไข Drawing", "แก้ไขไฟล์Drawing [" + txtCodeNo.Text.Trim() + "]", txtCodeNo.Text);
                            else
                                dbClss.AddHistory(this.Name , "แก้ไข Drawing", "เพิ่มไฟล์ Drawing [" + txtCodeNo.Text.Trim() + "]", txtCodeNo.Text);


                            chkDWG.Checked = true;
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                var g = (from ix in db.tb_Items
                                         where ix.CodeNo.Trim() == txtCodeNo.Text.Trim() && ix.Status == "Active"
                                         select ix).ToList();
                                if (g.Count > 0)  //มีรายการในระบบ
                                {
                                    var gg = (from ix in db.tb_Items
                                              where ix.CodeNo.Trim() == txtCodeNo.Text.Trim()
                                              select ix).First();
                                    gg.DWG = chkDWG.Checked;
                                    gg.UpdateBy = ClassLib.Classlib.User;
                                    gg.UpdateDate = DateTime.Now;
                                    db.SubmitChanges();
                                }
                            }

                        }
                        else
                        {
                            lblTempAddFile.Text = FileName;

                        }

                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally { this.Cursor = Cursors.Default; }
            }
            else { MessageBox.Show("ต้องใส่ Drawing No.!"); }
            
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (chkDWG.Checked.Equals(true))
            {
                System.IO.File.Delete(lblPath.Text + txtCodeNo.Text + "_.pdf");
                chkDWG.Checked = false;


                dbClss.AddHistory(this.Name  , "ลบไฟล์ Drawing", "ลบไฟล์ Drawing [" + txtCodeNo.Text.Trim() + "]", txtCodeNo.Text);

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items
                             where ix.CodeNo.Trim() == txtCodeNo.Text.Trim() && ix.Status == "Active"
                             select ix).ToList();
                    if (g.Count > 0)  //มีรายการในระบบ
                    {
                        var gg = (from ix in db.tb_Items
                                  where ix.CodeNo.Trim() == txtCodeNo.Text.Trim()
                                  select ix).First();
                        gg.DWG = chkDWG.Checked;
                        gg.UpdateBy = ClassLib.Classlib.User;
                        gg.UpdateDate = DateTime.Now;
                        db.SubmitChanges();
                    }
                }
            }
            else
            {
                lblTempAddFile.Text = "";
            }
            MessageBox.Show("ลบไฟล์ Drawing เรียบร้อย");
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Comming soon!");
            try
            {
                btnEdit.Enabled = true;
               
                btnNew.Enabled = true;
                
                Cleardata();
                Enable_Status(false, "View");
                

                this.Cursor = Cursors.WaitCursor;
                ListPart sc = new ListPart(txtCodeNo);
                this.Cursor = Cursors.Default;
                sc.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
                //LoadData
                DataLoad();
                btnGET.Enabled = false;
                btnView.Enabled = false;
                chkGET.Enabled = false;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message + " : radButtonElement1_Click", this.Name); }

        }

        private void btnGET_Click(object sender, EventArgs e)
        {
            try
            {
              if(!cboGroupType.Text.Trim().Equals(""))
                {
                    //txtCodeNo.Text = "I0001";
                    txtCodeNo.Text = Get_CodeNo();
                }
                else
                {
                    MessageBox.Show("ต้องเลือกประเภทกลุ่มก่อนเสมอ!!");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message + " : btnGET_Click", this.Name); }
        }
        private string Get_CodeNo()
        {
            string re = "";
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string Temp_Running = "";
                    var I = (from ix in db.tb_GroupTypes select ix).Where(a => a.GroupCode == cboGroupType.Text).ToList();
                    if (I.Count > 0)
                        Temp_Running = I.FirstOrDefault().Running;

                    if (!Temp_Running.Equals(""))
                    {
                        var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo.Contains(Temp_Running)).OrderByDescending(b => b.CodeNo).ToList();
                        if (g.Count > 0)
                        {
                            //string temp = g.FirstOrDefault().CodeNo;
                            int c =  Convert.ToInt32(g.FirstOrDefault().CodeNo.Substring(1, 4)) + 1;
                            if(c.ToString().Count().Equals(1))
                                re = Temp_Running + "000"+ c.ToString();
                            else if (c.ToString().Count().Equals(2))
                                re = Temp_Running + "00" + c.ToString();
                            else if (c.ToString().Count().Equals(3))
                                re = Temp_Running + "0" + c.ToString();
                            else 
                                re = Temp_Running  + c.ToString();
                        }
                        else
                            re = Temp_Running + "0001";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message+" : Get_CodeNo", this.Name); }
            this.Cursor = Cursors.Default;
            return re;
        }
        private string Get_CodeNo_GroupCode(string GroupCode)
        {
            string re = "";
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string Temp_Running = "";
                    var I = (from ix in db.tb_GroupTypes select ix).Where(a => a.GroupCode == GroupCode).ToList();
                    if (I.Count > 0)
                        Temp_Running = I.FirstOrDefault().Running;

                    if (!Temp_Running.Equals(""))
                    {
                        var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo.Contains(Temp_Running)).OrderByDescending(b => b.CodeNo).ToList();
                        if (g.Count > 0)
                        {
                            //string temp = g.FirstOrDefault().CodeNo;
                            int c = Convert.ToInt32(g.FirstOrDefault().CodeNo.Substring(1, 4)) + 1;
                            if (c.ToString().Count().Equals(1))
                                re = Temp_Running + "000" + c.ToString();
                            else if (c.ToString().Count().Equals(2))
                                re = Temp_Running + "00" + c.ToString();
                            else if (c.ToString().Count().Equals(3))
                                re = Temp_Running + "0" + c.ToString();
                            else
                                re = Temp_Running + c.ToString();
                        }
                        else
                            re = Temp_Running + "0001";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("CreatePart", ex.Message + " : Get_CodeNo", this.Name); }
            this.Cursor = Cursors.Default;
            return re;
        }
        private void cboGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultType();
            if (cboTypeCode.Text.Equals(""))
                cboTypeCode.Text = cboGroupType.Text;
        }

        private void txtStandCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtStandCost_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtStandCost.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtStandCost.Text = (temp).ToString();
        }

        private void txtPCSUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtPCSUnit_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtPCSUnit.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtPCSUnit.Text = (temp).ToString();
        }

        private void txtLeadTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtLeadTime_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtLeadTime.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtLeadTime.Text = (temp).ToString();
        }

        private void txtMaximumStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtMaximumStock_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtMaximumStock.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtMaximumStock.Text = (temp).ToString();
        }

        private void txtMimimumStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtMimimumStock_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtMimimumStock.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtMimimumStock.Text = (temp).ToString();
        }

        private void txtErrorLeadtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtErrorLeadtime_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtErrorLeadtime.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtErrorLeadtime.Text = (temp).ToString();
        }

        private void txtToolLife_KeyPress(object sender, KeyPressEventArgs e)
        {
            StockControl.dbClss.CheckDigitDecimal(e);
        }

        private void txtToolLife_Leave(object sender, EventArgs e)
        {
            decimal temp = 0;
            decimal.TryParse(txtToolLife.Text, out temp);
            temp = decimal.Round(temp, 2);
            txtToolLife.Text = (temp).ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string CodeNo = txtCodeNo.Text;
            Cleardata();
            Enable_Status(false, "View");
            txtCodeNo.Text = CodeNo;
            DataLoad();
            Ac = "View";
            btnGET.Enabled = false;
            chkGET.Enabled = false;
        }

        private void btnOpenDWG_Click(object sender, EventArgs e)
        {
            if (chkDWG.Checked.Equals(true))
            {
                System.Diagnostics.Process.Start(lblPath.Text+txtCodeNo.Text+"_.pdf");
            }
            else if (!lblTempAddFile.Text.Equals(""))  //กรณียังไม่ได้ save  
            {
                System.Diagnostics.Process.Start(lblTempAddFile.Text);
            }
            else
                MessageBox.Show("ไม่มีพบไฟล์ Drawing");
        }

        DataTable dt_ShelfTag = new DataTable();
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
            dt_ShelfTag.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_ShelfTag.Columns.Add(new DataColumn("PartDescription", typeof(string)));
            dt_ShelfTag.Columns.Add(new DataColumn("ShelfNo", typeof(string)));


            dt_Kanban.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("PartNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("PartDescription", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("ShelfNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("VendorName", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("GroupType", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("ToolLife", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("Max", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("Min", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("ReOrderPoint", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("BarCode", typeof(Image)));

        }
        private void btnPrintShelfTAG_Click(object sender, EventArgs e)
        {
            try
            {
                dt_ShelfTag.Rows.Clear();

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtCodeNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        //foreach(var gg in g)
                        //{
                        //    dt_ShelfTag.Rows.Add(gg.CodeNo, gg.ItemDescription, gg.ShelfNo);
                        //}
                        //DataTable DT =  StockControl.dbClss.LINQToDataTable(g);
                        //Reportx1 po = new Reportx1("Report_PurchaseRequest_Content1.rpt", DT, "FromDT");
                        //po.Show();
                        var deleteItem = (from ii in db.TempPrintShelfs where ii.UserName == Environment.UserName select ii);
                        foreach (var d in deleteItem)
                        {
                            db.TempPrintShelfs.DeleteOnSubmit(d);
                            db.SubmitChanges();
                        }
                        TempPrintShelf ps = new TempPrintShelf();
                        ps.UserName = Environment.UserName;
                        ps.CodeNo = g.FirstOrDefault().CodeNo;
                        ps.PartDescription = g.FirstOrDefault().ItemDescription;
                        ps.PartNo = g.FirstOrDefault().ItemNo;
                        ps.ShelfNo = g.FirstOrDefault().ShelfNo;
                        ps.Max = Convert.ToDecimal(g.FirstOrDefault().MaximumStock);
                        ps.Min = Convert.ToDecimal(g.FirstOrDefault().MinimumStock);
                        ps.OrderPoint = Convert.ToDecimal(g.FirstOrDefault().ReOrderPoint);
                        db.TempPrintShelfs.InsertOnSubmit(ps);
                        db.SubmitChanges();

                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = Environment.UserName;
                        Report.Reportx1.WReport = "002_BoxShelf_Part";
                        Report.Reportx1 op = new Report.Reportx1("002_BoxShelf_Part.rpt");
                        op.Show();
                       
                    }
                    else
                        MessageBox.Show("not found.");
                }

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnPrintBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                dt_Kanban.Rows.Clear();
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtCodeNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        // Step 1 delete UserName
                        var deleteItem = (from ii in db.TempPrintKanbans where ii.UserName == Environment.UserName select ii);
                        foreach(var d in deleteItem)
                        {
                            db.TempPrintKanbans.DeleteOnSubmit(d);
                            db.SubmitChanges();
                        }

                        // Step 2 Insert to Table
                        TempPrintKanban tm = new TempPrintKanban();
                        tm.UserName = Environment.UserName;
                        tm.CodeNo = g.FirstOrDefault().CodeNo;
                        tm.PartDescription = g.FirstOrDefault().ItemDescription;
                        tm.PartNo = g.FirstOrDefault().ItemNo;
                        tm.VendorName = g.FirstOrDefault().VendorItemName;
                        tm.ShelfNo = g.FirstOrDefault().ShelfNo;
                        tm.GroupType = g.FirstOrDefault().GroupCode;
                        tm.Max=Convert.ToDecimal(g.FirstOrDefault().MaximumStock);
                        tm.Min= Convert.ToDecimal(g.FirstOrDefault().MinimumStock);
                        tm.ReOrderPoint= Convert.ToDecimal(g.FirstOrDefault().ReOrderPoint);
                        tm.ToolLife= Convert.ToDecimal(g.FirstOrDefault().Toollife);
                        byte[] barcode = StockControl.dbClss.SaveQRCode2D(g.FirstOrDefault().CodeNo);
                        tm.BarCode = barcode;
                        db.TempPrintKanbans.InsertOnSubmit(tm);
                        db.SubmitChanges();
                        this.Cursor = Cursors.Default;
                        // Step 3 Call Report
                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = Environment.UserName;
                        Report.Reportx1.WReport = "001_Kanban_Part";
                        Report.Reportx1 op = new Report.Reportx1("001_Kanban_Part.rpt");
                        op.Show();

                        //foreach (var gg in g)
                        //{
                        //    dt_Kanban.Rows.Add(gg.CodeNo, gg.ItemNo, gg.ItemDescription, gg.ShelfNo, gg.Leadtime, gg.VendorItemName, gg.GroupCode, gg.Toollife, gg.MaximumStock, gg.MinimumStock,gg.ReOrderPoint, gg.BarCode);
                        //}
                        //DataTable DT =  StockControl.dbClss.LINQToDataTable(g);
                        //Reportx1 po = new Reportx1("Report_PurchaseRequest_Content1.rpt", DT, "FromDT");
                        //po.Show();


                    }
                    else
                        MessageBox.Show("not found.");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void txtMimimumStock_TextChanged(object sender, EventArgs e)
        {
            txtReOrderPoint.Text = "0.00";
        }

        private void cboVendor_Leave(object sender, EventArgs e)
        {
            cboVendor_SelectedIndexChanged(null, null);
        }

        private void cboGroupType_Leave(object sender, EventArgs e)
        {
            cboGroupType_SelectedIndexChanged(null, null);
        }

        private void lblStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Stock_List a = new Stock_List(txtCodeNo.Text, "Invoice");
                a.Show();
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void lblTempStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Stock_List a = new Stock_List(txtCodeNo.Text, "Temp");
                a.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void lblOrder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Stock_List a = new Stock_List(txtCodeNo.Text, "BackOrder");
                a.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void chkGET_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkGET.Checked.Equals(true))
            {
                btnGET.Enabled = false;
                txtCodeNo.Enabled = true;
            }
            else
            {
                btnGET.Enabled = true;
                txtCodeNo.Enabled = false;
            }
        }
    }
}
