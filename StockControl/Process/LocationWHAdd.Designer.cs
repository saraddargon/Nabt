namespace StockControl
{
    partial class LocationWHAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationWHAdd));
            this.radRibbonBar1 = new Telerik.WinControls.UI.RadRibbonBar();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonBarGroup1 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnSave = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroup3 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnRefresh = new Telerik.WinControls.UI.RadButtonElement();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuSeparatorItem1 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radLabelElement1 = new Telerik.WinControls.UI.RadLabelElement();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtCode = new Telerik.WinControls.UI.RadTextBox();
            this.txtName = new Telerik.WinControls.UI.RadTextBox();
            this.chkActive = new Telerik.WinControls.UI.RadCheckBox();
            this.office2010BlueTheme1 = new Telerik.WinControls.Themes.Office2010BlueTheme();
            this.radRibbonBarButtonGroup2 = new Telerik.WinControls.UI.RadRibbonBarButtonGroup();
            this.btnFilter = new Telerik.WinControls.UI.RadButtonElement();
            this.Unfilter = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarButtonGroup5 = new Telerik.WinControls.UI.RadRibbonBarButtonGroup();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radRibbonBar1
            // 
            this.radRibbonBar1.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab1});
            // 
            // 
            // 
            this.radRibbonBar1.ExitButton.Text = "Exit";
            this.radRibbonBar1.ExitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radRibbonBar1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.radRibbonBar1.Name = "radRibbonBar1";
            // 
            // 
            // 
            this.radRibbonBar1.OptionsButton.Text = "Options";
            this.radRibbonBar1.OptionsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radRibbonBar1.OptionsButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // 
            // 
            this.radRibbonBar1.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.radRibbonBar1.Size = new System.Drawing.Size(483, 160);
            this.radRibbonBar1.StartButtonImage = ((System.Drawing.Image)(resources.GetObject("radRibbonBar1.StartButtonImage")));
            this.radRibbonBar1.StartMenuItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem1,
            this.radMenuSeparatorItem1});
            this.radRibbonBar1.TabIndex = 0;
            this.radRibbonBar1.Text = "Location Add/Edit";
            this.radRibbonBar1.ThemeName = "Office2010Blue";
            this.radRibbonBar1.Click += new System.EventHandler(this.radRibbonBar1_Click);
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.IsSelected = true;
            this.ribbonTab1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radRibbonBarGroup1,
            this.radRibbonBarGroup3});
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "Action";
            this.ribbonTab1.UseMnemonic = false;
            // 
            // radRibbonBarGroup1
            // 
            this.radRibbonBarGroup1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnSave});
            this.radRibbonBarGroup1.Name = "radRibbonBarGroup1";
            this.radRibbonBarGroup1.Text = "Manage";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "บันทึกรายการ";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radRibbonBarGroup3
            // 
            this.radRibbonBarGroup3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.radRibbonBarGroup3.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnRefresh});
            this.radRibbonBarGroup3.Name = "radRibbonBarGroup3";
            this.radRibbonBarGroup3.Text = "Page";
            // 
            // btnRefresh
            // 
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(6)))), ((int)(((byte)(197)))));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "รีเฟรช";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefresh.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("radMenuItem1.Image")));
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "Exit";
            this.radMenuItem1.Click += new System.EventHandler(this.radMenuItem1_Click);
            // 
            // radMenuSeparatorItem1
            // 
            this.radMenuSeparatorItem1.Name = "radMenuSeparatorItem1";
            this.radMenuSeparatorItem1.Text = "radMenuSeparatorItem1";
            this.radMenuSeparatorItem1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radLabelElement1});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 376);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(483, 26);
            this.radStatusStrip1.SizingGrip = false;
            this.radStatusStrip1.TabIndex = 1;
            // 
            // radLabelElement1
            // 
            this.radLabelElement1.Name = "radLabelElement1";
            this.radStatusStrip1.SetSpring(this.radLabelElement1, false);
            this.radLabelElement1.Text = "Status : Location Add/Edit";
            this.radLabelElement1.TextWrap = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 216);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Honeydew;
            this.panel2.Controls.Add(this.radLabel2);
            this.panel2.Controls.Add(this.radLabel1);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.txtName);
            this.panel2.Controls.Add(this.chkActive);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 216);
            this.panel2.TabIndex = 0;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(79, 49);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(42, 18);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Name :";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(79, 24);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(38, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Code :";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(157, 24);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(140, 20);
            this.txtCode.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(157, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 20);
            this.txtName.TabIndex = 3;
            // 
            // chkActive
            // 
            this.chkActive.Location = new System.Drawing.Point(157, 75);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(51, 18);
            this.chkActive.TabIndex = 6;
            this.chkActive.Text = "Active";
            // 
            // radRibbonBarButtonGroup2
            // 
            this.radRibbonBarButtonGroup2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnFilter,
            this.Unfilter});
            this.radRibbonBarButtonGroup2.Name = "radRibbonBarButtonGroup2";
            this.radRibbonBarButtonGroup2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.radRibbonBarButtonGroup2.Padding = new System.Windows.Forms.Padding(1);
            this.radRibbonBarButtonGroup2.ShowBackColor = false;
            this.radRibbonBarButtonGroup2.Text = "radRibbonBarButtonGroup2";
            // 
            // btnFilter
            // 
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Text = "Filter";
            this.btnFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // Unfilter
            // 
            this.Unfilter.Image = ((System.Drawing.Image)(resources.GetObject("Unfilter.Image")));
            this.Unfilter.Name = "Unfilter";
            this.Unfilter.Text = "Un Filter";
            this.Unfilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // radRibbonBarButtonGroup5
            // 
            this.radRibbonBarButtonGroup5.Name = "radRibbonBarButtonGroup5";
            this.radRibbonBarButtonGroup5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.radRibbonBarButtonGroup5.Padding = new System.Windows.Forms.Padding(1);
            this.radRibbonBarButtonGroup5.ShowBackColor = false;
            this.radRibbonBarButtonGroup5.ShowBorder = false;
            this.radRibbonBarButtonGroup5.Text = "radRibbonBarButtonGroup4";
            // 
            // LocationWHAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 402);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radStatusStrip1);
            this.Controls.Add(this.radRibbonBar1);
            this.KeyPreview = true;
            this.Name = "LocationWHAdd";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Setup";
            this.Load += new System.EventHandler(this.Unit_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Unit_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadRibbonBar radRibbonBar1;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadLabelElement radLabelElement1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RibbonTab ribbonTab1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup3;
        private Telerik.WinControls.UI.RadButtonElement btnRefresh;
        private Telerik.WinControls.Themes.Office2010BlueTheme office2010BlueTheme1;
        private Telerik.WinControls.UI.RadButtonElement btnSave;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem1;
        private Telerik.WinControls.UI.RadRibbonBarButtonGroup radRibbonBarButtonGroup2;
        private Telerik.WinControls.UI.RadButtonElement btnFilter;
        private Telerik.WinControls.UI.RadButtonElement Unfilter;
        private Telerik.WinControls.UI.RadRibbonBarButtonGroup radRibbonBarButtonGroup5;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtName;
        private Telerik.WinControls.UI.RadCheckBox chkActive;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtCode;
    }
}
