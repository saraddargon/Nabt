using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockControl
{
    public partial class Home1600x900 : Form
    {
        public Home1600x900(ref Telerik.WinControls.UI.RadLabelElement lb)
        {
            InitializeComponent();
            lb2 = lb;
        }
        Telerik.WinControls.UI.RadLabelElement lb2;
        private void Home1600x900_Load(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void Home1600x900_MouseMove(object sender, MouseEventArgs e)
        {
            //txtxy.Text = Screen.PrimaryScreen.Bounds.Width.ToString("#,###") + "x" + Screen.PrimaryScreen.Bounds.Width.ToString("#,###");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            //MessageBox.Show(MousePosition.X.ToString("#,###") + ":" + MousePosition.Y.ToString("#,###"));
            //txtxy.Text = MousePosition.X.ToString("#,###") + ":" + MousePosition.Y.ToString("#,###");
            
            callscreen();
        }
        private void callscreen()
        {

            string Screen1 = "";
            if(ClassLib.Classlib.ScreenWidth == 1600 && ClassLib.Classlib.ScreenHight==900)
            {
                Screen1=getX("1600x900",ClassLib.Classlib.ScreenWidth, ClassLib.Classlib.ScreenHight);

            }
            else if(ClassLib.Classlib.ScreenHight == 1600 && ClassLib.Classlib.ScreenHight == 900)
            {

            }
            //MessageBox.Show(Screen1);
            if (!Screen1.Equals(""))
            {

                this.Cursor = Cursors.WaitCursor;
                Telerik.WinControls.UI.RadRibbonForm showf = dbClss.CreateForm(Screen1);
                SetShowForm(showf, false);
                showf = null;
                this.Cursor = Cursors.Default;
            }
        }
        private string getX(string ty,int x,int y)
        {
            string Scrren1 = "";
            if(ty.Equals("1600x900"))
            {
                
                if (MousePosition.X>=514 && MousePosition.X<=642 && MousePosition.Y>=304 && MousePosition.Y<=327)
                {
                 
                    Scrren1 = "Shipping";
                }
            }
                
            return Scrren1;
        }
           
        private void SetShowForm(Telerik.WinControls.UI.RadRibbonForm GetForm, bool dialog = true)
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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            lb2.Text = "x" + MousePosition.X.ToString("#,###") + ":" + "y" + MousePosition.Y.ToString("#,###");
        }
    }
}
