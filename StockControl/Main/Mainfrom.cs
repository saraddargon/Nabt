using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using ClassLib;
using System.Security.Permissions;
using System.Linq;
namespace StockControl
{
    public partial class Mainfrom : Telerik.WinControls.UI.RadForm
    {
        public Mainfrom()
        {
            InitializeComponent();
            lblUser.Text= ClassLib.Classlib.User;
            lblDomain.Text = Classlib.DomainUser;
            lblresolution.Text = Classlib.ScreenWidth.ToString("#,###") + " x " + Classlib.ScreenHight.ToString("#,###");
        }
   

        string SqlGetName= "";
        display formshow;
        Home1600x900 homeshow;
        private void Mainfrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch { }
        }

        private void radMenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Mainfrom_Load(object sender, EventArgs e)
        {
            TreeManu.ExpandAll();
            //SqlGetName = "PartSetting";
            SqlGetName = "home";
            txtposition.Text = "x0:y0";
            CallDisplayHome();
            this.Text = "Barcode System " + dbClss.versioin ;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_QCConfig cf = db.tb_QCConfigs.FirstOrDefault();
                    if(cf !=null)
                    {
                        dbClss.UseQC = Convert.ToBoolean(cf.UsedQC);
                        dbClss.UseQCDept = Convert.ToBoolean(cf.UsePDDept);
                        dbClss.UseQCDept = Convert.ToBoolean(cf.UseQCDept);
                    }
                }
            }
            catch { }
        }
        private void CallDisplayHome()
        {
            if (CountDisplay == 0 && !SqlGetName.Equals(""))
            {
                homeshow = new Home1600x900(ref txtposition);
                ShowTreeForm(homeshow);
                GC.Collect();
                GC.WaitForFullGCComplete();
                CountDisplay = 1;
            }

        }
        private void TreeManu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CountDisplay = 1;
            TreeManu.SelectedNode.Expand();
            SqlGetName = TreeManu.SelectedNode.Name.ToString();
            formshow = new display(ref SqlGetName);
            //formshow.lblModule.Text = TreeManu.SelectedNode.Text.ToString();
            //formshow.lblDatabase.Text = ConnectDB.Db.ToUpper();
            //formshow.lblServer.Text = ConnectDB.Server.ToUpper();
            //formshow.lblVersion.Text = "1.0";
            //formshow.lblUser.Text = ConnectDB.UserName.ToUpper();
            ShowTreeForm(formshow);
           
            GC.Collect();
            GC.WaitForFullGCComplete();
        }
        public void ShowTreeForm(Form Show1)
        {
            Show1.TopLevel = false;
            Show1.Dock = DockStyle.Fill;
            Show1.WindowState = FormWindowState.Maximized;
            Show1.FormBorderStyle = FormBorderStyle.None;
            Show1.ShowInTaskbar = false;
            // set panal1 show
            
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(Show1);
            Show1.Show();

        }

        private void radMenuItem15_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //Unit unit = new Unit();
            //this.Cursor = Cursors.Default;
            //unit.ShowDialog();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            //ClassLib.Memory.Heap();
        }

        private void radMenuItem17_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //Types tb = new Types();
            //this.Cursor = Cursors.Default;
            //tb.ShowDialog();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            //ClassLib.Memory.Heap();
        }

        private void Mainfrom_MaximumSizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("xx");
            //formshow = new display(ref SqlGetName);
            //ShowTreeForm(formshow);
            //GC.Collect();
            //GC.WaitForFullGCComplete();
        }

        private void Mainfrom_ResizeEnd(object sender, EventArgs e)
        {
            //MessageBox.Show("resize");
        }

        private void Mainfrom_MinimumSizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("2xx");
        }

        int CountDisplay = 0;
        private void CallDisplay()
        {
            if (CountDisplay == 0 && SqlGetName.Equals("home"))
            {
                CallDisplayHome();
                return;
            }
            else if(CountDisplay==0 && !SqlGetName.Equals(""))
            {
                formshow = new display(ref SqlGetName);
                ShowTreeForm(formshow);
                GC.Collect();
                GC.WaitForFullGCComplete();
                CountDisplay = 1;
            }

        }
        private void Mainfrom_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //MessageBox.Show("Minimize");
               
            }
            else if(WindowState==FormWindowState.Normal)
            {
                // MessageBox.Show("restore down");
                CountDisplay = 0;
                CallDisplay();
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                CountDisplay = 0;
                CallDisplay();
                // MessageBox.Show("Maximize");
            }
        }

        private void radMenuItem4_Click(object sender, EventArgs e)
        {
            CountDisplay = 0;
            SqlGetName = "home";
            CallDisplayHome();
        }

        private void radMenuItem16_Click(object sender, EventArgs e)
        {

            //this.Cursor = Cursors.WaitCursor;
            //GroupType gy = new GroupType();
            //this.Cursor = Cursors.Default;
            //gy.ShowDialog();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            //ClassLib.Memory.Heap();
        }

        private void radMenuItem22_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //HistoryScreen gy = new HistoryScreen("");
            //this.Cursor = Cursors.Default;
            //gy.ShowDialog();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            //ClassLib.Memory.Heap();
        }

        private void radMenuItem21_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PathConfig pf = new PathConfig();
            this.Cursor = Cursors.Default;
            pf.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void radMenuItem8_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("ต้องการที่จะ Run Job Query หรือไม่ ?", "Run Job", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    db.sp_RunJOB();
                //    db.sp_SelectItemUpdate();
                //}
                 MessageBox.Show("Script Run StoreProcedure Agent Completed.");
            }
        }

        private void radMenuItem7_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการที่จะ Backup ฐานข้อมูล","Backup",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {

                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_BackupDatabase();
                    }
                    MessageBox.Show("Backup Completed.");
                }
                catch (Exception ex) { MessageBox.Show("ไม่สามารถ Backup ได้โปรดเช็คสถานที่เก็บไฟล์!"); }
            }
        }

        private void radMenuItem19_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการที่จะ Update หรือไม่ ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("AutoUpdate.exe");
                Application.Exit();
            }
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
           
          

            //this.Cursor = Cursors.WaitCursor;
            //ServerConfig sc = new ServerConfig();
            //this.Cursor = Cursors.Default;
            //sc.ShowDialog();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            //ClassLib.Memory.Heap();
        }

        private void radMenuItem12_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            About sc = new About();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void radMenuItem11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reset Layout Completed.");
        }

        private void radMenuItem18_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //Vendor sc = new Vendor();
            //this.Cursor = Cursors.Default;
            //sc.ShowDialog();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            //ClassLib.Memory.Heap();


            
        }

        private void radMenuItem20_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            WorkDays sc = new WorkDays();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            //  MessageBox.Show("aa");
            CountDisplay = 0;
            SqlGetName.Equals("home");
            CallDisplayHome();
        }

        private void radMenuItem10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Product is activated.");
        }

        private void radMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\Manual.pdf");
            }
            catch { }
        }

        private void radMenuItem15_Click_1(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\Manual.pdf");
            }
            catch { }
        }

        private void radMenuItem16_Click_1(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\Production Order.pdf");
            }
            catch { }
        }

        private void radMenuItem17_Click_1(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\Receive.pdf");
            }
            catch { }
        }

        private void radMenuItem18_Click_1(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\Manual_Export_LocalDelivery.pdf");
            }
            catch { }
        }

        private void radMenuItem23_Click(object sender, EventArgs e)
        {
            //Move Stock.pdf

            try
            {
                System.Diagnostics.Process.Start(@"Report\Move Stock.pdf");
            }
            catch { }
        }

        private void radMenuItem24_Click(object sender, EventArgs e)
        {


            try
            {
                System.Diagnostics.Process.Start(@"Report\Manual System Barcode Box.pdf");
            }
            catch { }
        }

        private void radMenuItem27_Click(object sender, EventArgs e)
        {
            
                try
            {
                System.Diagnostics.Process.Start(@"Report\LocalInvoice.pdf");
            }
            catch { }
        }

        private void radMenuItem28_Click(object sender, EventArgs e)
        {

            try
            {
                System.Diagnostics.Process.Start(@"Report\Export Invoice.pdf");
            }
            catch { }
        }

        private void radMenuItem29_Click(object sender, EventArgs e)
        {
            //QC Setup
            QCSetup qcs = new QCSetup();
            qcs.ShowDialog();
        }

        private void radMenuItem30_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\ManualQC.pdf");
            }
            catch { }
        }
    }
}
