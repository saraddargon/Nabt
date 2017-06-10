using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace StockControl
{
    public partial class ServerConfig : Telerik.WinControls.UI.RadForm
    {
        public ServerConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ServerConfig_Load(object sender, EventArgs e)
        {
            string apc = Properties.Settings.Default.dbStockControlConnectionString3;
            try
            {
                if (!apc.Equals(""))
                {
                    //Data Source=XTH-TOO\SQLEXPRESS;Initial Catalog=dbStockControl;User ID=sa;Password=;

                    string[] a = apc.Split(';');
                    string[] b = a[0].Split('=');
                    string[] c = a[1].Split('=');
                    string[] d = a[2].Split('=');
                    string[] f = a[3].Split('=');
                    txtServer.Text = b[1];
                    txtDatabase.Text = c[1];
                    tbUser.Text = d[1];
                    tbPass.Text = f[1];
                }

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            //MessageBox.Show(apc);
        }
    }
}
