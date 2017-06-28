namespace StockControl
{
    partial class Form2
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem8 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem9 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem10 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem11 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem12 = new Telerik.WinControls.UI.RadListDataItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.cboMonth = new Telerik.WinControls.UI.RadDropDownList();
            this.cboYear = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.cboMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ใช้เวลาในการคำนวณ 2-3 นาที";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(123, 121);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(184, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 38);
            this.button1.TabIndex = 2;
            this.button1.Text = "กดเพื่อคำนวณ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboMonth
            // 
            radListDataItem1.Text = "JAN";
            radListDataItem2.Text = "FEB";
            radListDataItem3.Text = "MAR";
            radListDataItem4.Text = "APR";
            radListDataItem5.Text = "MAY";
            radListDataItem6.Text = "JUN";
            radListDataItem7.Text = "JUL";
            radListDataItem8.Text = "AUG";
            radListDataItem9.Text = "SEP";
            radListDataItem10.Text = "OCT";
            radListDataItem11.Text = "NOV";
            radListDataItem12.Text = "DEC";
            this.cboMonth.Items.Add(radListDataItem1);
            this.cboMonth.Items.Add(radListDataItem2);
            this.cboMonth.Items.Add(radListDataItem3);
            this.cboMonth.Items.Add(radListDataItem4);
            this.cboMonth.Items.Add(radListDataItem5);
            this.cboMonth.Items.Add(radListDataItem6);
            this.cboMonth.Items.Add(radListDataItem7);
            this.cboMonth.Items.Add(radListDataItem8);
            this.cboMonth.Items.Add(radListDataItem9);
            this.cboMonth.Items.Add(radListDataItem10);
            this.cboMonth.Items.Add(radListDataItem11);
            this.cboMonth.Items.Add(radListDataItem12);
            this.cboMonth.Location = new System.Drawing.Point(166, 72);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(88, 20);
            this.cboMonth.TabIndex = 7;
            this.cboMonth.Text = "JAN";
            this.cboMonth.ThemeName = "Office2010Blue";
            // 
            // cboYear
            // 
            this.cboYear.Location = new System.Drawing.Point(167, 46);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(87, 20);
            this.cboYear.TabIndex = 8;
            this.cboYear.Text = "2017";
            this.cboYear.ThemeName = "Office2010Blue";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(106, 72);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(54, 18);
            this.radLabel3.TabIndex = 5;
            this.radLabel3.Text = "เลือกเดือน";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(123, 47);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(36, 18);
            this.radLabel1.TabIndex = 6;
            this.radLabel1.Text = "เลือกปี";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(415, 255);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculate";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private Telerik.WinControls.UI.RadDropDownList cboMonth;
        private Telerik.WinControls.UI.RadDropDownList cboYear;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}