using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using FMS_Base;
using Microsoft.VisualBasic;
using System.IO;
using System.Diagnostics;
namespace StockControl
{
    public partial class display : Form
    {
        public List<Process> Process_List = new List<Process>();
        public ListViewItem ListItem;    
        private string sqlname;
        //private BaseClassAllDataContext GetData = null;
        private DataClasses1DataContext GetData = new DataClasses1DataContext();
        public display(ref string sqlname1)
        {
           
            InitializeComponent();
            listView1.Dock = DockStyle.Fill;
            picBackgroup.Visible = false;
            this.sqlname = sqlname1;
            string ShowName = "";
            switch(sqlname1)
            {
                case "Production": { ShowName = "Production"; pictureBox1.Image = imageList4.Images[2]; }break;
                case "TPIC": { ShowName = "TPIC List"; pictureBox1.Image = imageList4.Images[1]; } break;
                case "MasterList": { ShowName = "Master List"; pictureBox1.Image = imageList4.Images[0]; } break;
                case "Receive": { ShowName = "Receive"; pictureBox1.Image = imageList4.Images[3]; } break;
                case "Export": { ShowName = "Export"; pictureBox1.Image = imageList4.Images[4]; } break;
                case "Local": { ShowName = "Local"; pictureBox1.Image = imageList4.Images[5]; } break;
                case "CheckStock": { ShowName = "CheckStock"; pictureBox1.Image = imageList4.Images[7]; } break;

            }
            lblModule.Text = ShowName;

           
             // int c1 = 0;
             //c1 = Convert.ToInt32(GetData.Check_Screen(this.sqlname, ConnectDB.User));
             //if (c1 > 0)
             //{
             //    MessageBox.Show("xxx");
             //    return;
             //}
        }
        private void GETIM()
        {
           // AddCenteredImage(imageList1, Image.FromFile(@"C:\Users\Administrator\Desktop\Project\Ogusu\picture\report 64X64\report account (Custom).png"));
            //foreach(Image im in imageList3.Images)
            //{
            //    AddCenteredImage(imageList3, im);
            //}
        }
        private void display_Load(object sender, EventArgs e)
        {
            // lblVersion.Text = ConnectDB.VersionFms;

            //GETIM();
            try
            {
                
                var Data = (from OpenFormdata in GetData.Sp_ADM03_OpenFormSelect()
                            where OpenFormdata.NodeName == this.sqlname
                            select OpenFormdata).ToList();

                this.panel1.Size = new System.Drawing.Size(769, 70);
                ////////////////////
                listView1.Items.Clear();
                listView1.BeginUpdate();
                listView1.LargeImageList = imageList3;
                listView1.View = View.Tile;
                ListViewGroup group1 = listView1.Groups[0];
                ListViewGroup group2 = listView1.Groups[1];

                foreach (var c in Data)
                {

                    ListItem = new ListViewItem(c.TextNode.ToString());
                    if (c.TypeNode.Equals("Report"))
                    {
                        ListItem.Group = group2;
                        ListItem.ImageIndex = 1;

                    }
                    else
                    {
                        ListItem.Group = group1;
                        ListItem.ImageIndex = 0;
                        if(c.LinkNode.ToString().Equals("ShippingReport"))
                        {
                            ListItem.ImageIndex = 1;
                           
                        }
                        else if (c.LinkNode.ToString().Equals("ReportAccount"))
                        {
                            ListItem.ImageIndex = 2;
                        }
                        else if (c.LinkNode.ToString().Equals("StockBalance"))
                        {
                            ListItem.ImageIndex = 3;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportPart"))
                        {
                            ListItem.ImageIndex = 4;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportReceive"))
                        {
                            ListItem.ImageIndex = 5;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportPR"))
                        {
                            ListItem.ImageIndex = 6;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportStock"))
                        {
                            ListItem.ImageIndex = 7;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportRC")
                            || c.LinkNode.ToString().Equals("ReportExport")
                             || c.LinkNode.ToString().Equals("Report Local")
                              || c.LinkNode.ToString().Equals("ReportPD")
                            )
                        {
                            ListItem.ImageIndex = 8;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportWaitingOrder"))
                        {
                            ListItem.ImageIndex = 9;
                        }
                        else if (c.LinkNode.ToString().Equals("ReportAdjustStock"))
                        {
                            ListItem.ImageIndex = 10;
                        }
                        else if (c.LinkNode.ToString().Equals("MasterModel"))
                        {
                            ListItem.ImageIndex = 11;
                        }
                        else if (c.LinkNode.ToString().Equals("ModelMapping"))
                        {
                            ListItem.ImageIndex = 12;
                        }
                        else if (c.LinkNode.ToString().Equals("Forecast"))
                        {
                            ListItem.ImageIndex = 13;
                        }
                        else if (c.LinkNode.ToString().Equals("CalculatePlanning"))
                        {
                            ListItem.ImageIndex = 14;
                        }
                        else if (c.LinkNode.ToString().Equals("CreatePart"))
                        {
                            ListItem.ImageIndex = 15;
                        }
                        else if (c.LinkNode.ToString().Equals("UserList") ||
                            c.LinkNode.ToString().Equals("UserSetup") ||
                            c.LinkNode.ToString().Equals("LocationWH") ||
                            c.LinkNode.ToString().Equals("ListItem") ||
                            c.LinkNode.ToString().Equals("ListPO") ||
                            c.LinkNode.ToString().Equals("ListQueue") ||
                            c.LinkNode.ToString().Equals("ReceiveList") ||
                            c.LinkNode.ToString().Equals("Receive") ||
                            c.LinkNode.ToString().Equals("UploadExList") ||
                            c.LinkNode.ToString().Equals("ExportList") ||
                            c.LinkNode.ToString().Equals("ExShipment") ||
                            c.LinkNode.ToString().Equals("GuidelineLot") ||
                            c.LinkNode.ToString().Equals("LocalList")  ||
                            c.LinkNode.ToString().Equals("LocalShipment") ||
                                c.LinkNode.ToString().Equals("UploadLocal") ||
                                c.LinkNode.ToString().Equals("CheckStockList") ||
                                    c.LinkNode.ToString().Equals("CheckStock") ||
                                        c.LinkNode.ToString().Equals("ProductionBom") ||
                                            c.LinkNode.ToString().Equals("PrintRW") 
                            )
                        {
                            ListItem.ImageIndex = 16;
                        }
                        else if (c.LinkNode.ToString().Equals("ExportImportPart"))
                        {
                            ListItem.ImageIndex = 17;
                        }
                        else if (c.LinkNode.ToString().Equals("WaitingPR"))
                        {
                            ListItem.ImageIndex = 18;
                        }
                        else if (c.LinkNode.ToString().Equals("CreatePR"))
                        {
                            ListItem.ImageIndex = 19;
                        }
                        else if (c.LinkNode.ToString().Equals("CreatePR_List"))
                        {
                            ListItem.ImageIndex = 20;
                        }
                        else if (c.LinkNode.ToString().Equals("Receive"))
                        {
                            ListItem.ImageIndex = 21;
                        }
                        else if (c.LinkNode.ToString().Equals("ReceiveList"))
                        {
                            ListItem.ImageIndex = 22;
                        }
                        else if (c.LinkNode.ToString().Equals("ReturnReceive"))
                        {
                            ListItem.ImageIndex = 23;
                        }
                        else if (c.LinkNode.ToString().Equals("ChangeInvoice"))
                        {
                            ListItem.ImageIndex = 24;
                        }
                        else if (c.LinkNode.ToString().Equals("Shipping"))
                        {
                            ListItem.ImageIndex = 25;
                        }
                        else if (c.LinkNode.ToString().Equals("ShippingList"))
                        {
                            ListItem.ImageIndex = 26;
                        }
                        else if (c.LinkNode.ToString().Equals("ShippingCancel"))
                        {
                            ListItem.ImageIndex = 27;
                        }
                        else if (c.LinkNode.ToString().Equals("StockList"))
                        {
                            ListItem.ImageIndex = 28;
                        }
                        else if (c.LinkNode.ToString().Equals("AdjustStock"))
                        {
                            ListItem.ImageIndex = 29;
                        }
                        else if (c.LinkNode.ToString().Equals("MovementStock"))
                        {
                            ListItem.ImageIndex = 30;
                        }else if(c.LinkNode.ToString().Equals("ClearTempList"))
                        {
                            ListItem.ImageIndex = 18;
                        }
                        else
                        {
                            ListItem.ImageIndex = 0;
                        }
                        //if (c.LinkNode.ToString().Equals("SaleOrderList") || c.LinkNode.ToString().Equals("ToolsList") || c.LinkNode.ToString().Equals("KanbanListTx"))
                        //    ListItem.ForeColor = Color.Blue;
                        //else if (c.LinkNode.ToString().Equals("ToolsListShelf") || c.LinkNode.ToString().Equals("ReqOrderList"))
                        //    ListItem.ForeColor = Color.Green;
                    }
                    ListItem.Name = c.TypeNode.ToString();
                    ListItem.Tag = c.DialogFlag.ToString();
                    ListItem.ToolTipText = c.LinkNode.ToString();
                    listView1.Items.Add(ListItem);
                }
                listView1.EndUpdate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //if (this.sqlname.Equals("Tools"))
                //    listView1.Sorting = SortOrder.Ascending;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();

            }

                ///////////////////////////////////////////////////////

          


            //**darky
            //if (ConnectDB.User.ToLower().Equals("admin") || ConnectDB.User.ToLower().Equals("pongsakorn_s"))
            //if(true)
            //{
            //    if (sqlname.Equals("Approve"))
            //    {
            //        foreach (ListViewItem item in listView1.Items)
            //        {
            //            if ((new List<string> { "P/O View", "Drawing AP", "Approve Order" }).Contains(item.Text))
            //                listView1.Items.Remove(item);
            //        }
            //    }
            //}
        }
        public static void AddCenteredImage(ImageList list, Image image)
        {
            using (var bmp = new Bitmap(list.ImageSize.Width, list.ImageSize.Height))
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.Transparent);   // Change background if necessary
                var size = image.Size;
                if (size.Width > list.ImageSize.Width || size.Height > list.ImageSize.Height)
                {
                    // Image too large, rescale to fit the image list
                    double wratio = list.ImageSize.Width / size.Width;
                    double hratio = list.ImageSize.Height / size.Height;
                    double ratio = Math.Min(wratio, hratio);
                    size = new Size((int)(ratio * size.Width), (int)(ratio * size.Height));
                }
                var rc = new Rectangle(
                    (list.ImageSize.Width - size.Width) / 2,
                    (list.ImageSize.Height - size.Height) / 2,
                    size.Width, size.Height);
                gr.DrawImage(image, rc);
                list.Images.Add(bmp);
            }
        }
        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;

        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {                    
            listView1.View = View.SmallIcon;
            
           
        }

        private void SetShowForm(Telerik.WinControls.UI.RadRibbonForm GetForm,bool dialog = true)
        {
            /// เช็คสิทธิ์จากตรงนี้ 
            try
            {
                if (!dialog)
                    GetForm.Show();
                else
                    GetForm.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {

                if (listView1.FocusedItem.ToolTipText.ToString().Equals("ScreenItem"))
                {
                    
                    string GetMarkup = Interaction.InputBox("PASSWORD INPUT", "PASSWORD FOR Developer","0", 300, 150);
                    try
                    {
                        if (Convert.ToInt32(GetMarkup) == 22)
                        {
                            //LinQForm openform = new LinQForm();
                            //openform.ShowDialog();
                            //openform = null;
                        }
                        else MessageBox.Show("Developer Only");
                    }
                    catch { }

                }
                else if (listView1.FocusedItem.ToolTipText.ToString().Equals("UserSetting"))
                {
                    try
                    {
                        //if (ConnectDB.User.ToUpper().Equals("ADMIN"))
                        //{
                        //    System.Diagnostics.Process.Start(@"C:\Program Files\FMS\Admin\ITSS_Admin.exe");
                        //}
                        //else MessageBox.Show("For ADMIN Only");
                    }
                    catch { }
                }
                else if (listView1.FocusedItem.Name.ToString().Equals("Report"))
                {
                    //Report.Report openRpt = new Report.Report(listView1.FocusedItem.ToolTipText.ToString(), listView1.FocusedItem.Text.ToString());
                    //openRpt.ShowDialog();
                }
                else
                {
                    if (listView1.FocusedItem.ToolTipText.ToUpper().Contains(".EXE"))
                    {
                        try
                        {
                            //// check version screen กรณีเป็น .exe เช็คจาก Modify date
                            //// By TU

                            string ToolTipText = listView1.FocusedItem.ToolTipText;
                            string[] FileName = Path.GetFileName(ToolTipText).Split('.');
                            string ServerPath = ToolTipText.Replace(@"C:\Program Files\FMS",@"\\192.168.0.3\FMS\Update FMS");

                            DateTime ComDate = File.GetLastWriteTime(ToolTipText);
                            DateTime ServerDate = File.GetLastWriteTime(ServerPath);

                            if (ComDate.Equals(ServerDate))
                            {
                                string LText = listView1.FocusedItem.Text;
                                if (LText.Equals("Safety & Training"))
                                   // GetData.sp_211_DC004_OpenProgram_SAVE(FMSClass.ConnectDB.User, 2);

                                System.Diagnostics.Process.Start(ToolTipText);
                                Process_List.Add(Process.GetProcessesByName(FileName[0]).FirstOrDefault());
                            }
                            else
                            {
                                if (MessageBox.Show("มีการแก้ไขโปรแกรม \n คุณต้องการ update program หรือไม่", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    UpdateProgram();
                                }
                            }
                        }
                        catch{}
                    }
                    else
                    {
                        try
                        {
                            //// check Version Screen จาก Form.Tag 
                            //// By TU

                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                            //var G = db.Sp_ADM03_OpenFormSelect().ToList().Where(a => a.LinkNode.ToString().Equals(listView1.FocusedItem.ToolTipText.ToString())).ToList();
                            //string Version = "";
                            //if (G.Count > 0)
                            //{
                            //    Version = Convert.ToString(G.FirstOrDefault().Version);
                            //}

                            // MessageBox.Show(listView1.FocusedItem.ToolTipText.ToString());
                            this.Cursor = Cursors.WaitCursor;
                            Telerik.WinControls.UI.RadRibbonForm showf = dbClss.CreateForm(listView1.FocusedItem.ToolTipText.ToString());
                           
                            SetShowForm(showf, Convert.ToBoolean(listView1.FocusedItem.Tag));
                            showf = null;
                            this.Cursor = Cursors.Default;
                            //if (Version.Equals("") || (!Version.Equals("") && Version.Equals(showf.Tag.ToString())))
                            //    {

                            //        SetShowForm(showf, Convert.ToBoolean(listView1.FocusedItem.Tag));
                            //        showf = null;
                            //    }
                            //    else
                            //    {
                            //        if (MessageBox.Show("มีการแก้ไขโปรแกรม \n คุณต้องการ update program หรือไม่", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            //        {
                            //            UpdateProgram();
                            //        }
                            //    }
                        }
                        }
                        catch { }
                    }
                   
                }
            this.Cursor = Cursors.Default;
            GC.Collect();
                GC.WaitForPendingFinalizers();
                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
           
        }
        //DARKY

        private void UpdateProgram()
        {
            try
            {
                Application.Exit();
                Application.ExitThread();
                //System.Diagnostics.Process.Start(@"C:\Program Files\FMS\AutoUpdate\AutoUpdate.exe");
                try
                {
                    //ss
                    // System.Diagnostics.Process.Start(@"\\192.168.0.3\FMS\System\AutoUpdate\AutoUpdate.exe");
                    bool osInfo = System.Environment.Is64BitOperatingSystem;
                    if (!osInfo)
                        System.Diagnostics.Process.Start(@"\\srv2k3\fms\System\AutoUpdate\AutoUpdate.exe");
                    else
                        System.Diagnostics.Process.Start(@"\\srv2k3\fms\System\AutoUpdate\AutoUpdate.exe");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            catch { }
        }
        private void display_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Process_List.Count > 0)
                {
                    Process_List.ToList().ForEach(o =>
                    {
                        o.CloseMainWindow();
                        o.WaitForExit();
                    });
                }
            }
            catch { }
        }
        
        private void listIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Sorting = SortOrder.Ascending;
        }

        private void dESCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Sorting = SortOrder.Descending;
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.ShowGroups = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
