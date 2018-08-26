using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
namespace StockControl
{
    public partial class Login : Telerik.WinControls.UI.RadForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Text = "Login " + dbClss.versioin ;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_UserMachine mc = db.tb_UserMachines.Where(m => m.MachineName == Environment.MachineName).FirstOrDefault();
                    if(mc!=null)
                    {
                        txtUserID.Text = mc.UserID.ToString();
                        txtPassword.Focus();

                    }else
                    {
                        txtUserID.Focus();

                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtUserID.Text.Equals("") && !txtPassword.Text.Equals(""))
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        
                        tb_User ur = db.tb_Users.Where(u => u.UserID == txtUserID.Text && u.Password == txtPassword.Text && u.Active==true).FirstOrDefault();
                        if(ur!=null)
                        {
                            dbClss.UserID = txtUserID.Text;
                            this.Hide();
                            tb_UserMachine um = db.tb_UserMachines.Where(m => m.MachineName == Environment.MachineName).FirstOrDefault();
                            if(um!=null)
                            {
                                db.tb_UserMachines.DeleteOnSubmit(um);
                                db.SubmitChanges();
                            }
                            tb_UserMachine nw = new tb_UserMachine();
                            nw.MachineName = Environment.MachineName;
                            nw.UserID = txtUserID.Text.Trim();
                            db.tb_UserMachines.InsertOnSubmit(nw);
                            db.SubmitChanges();
                            Mainfrom main = new Mainfrom();
                            main.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("User or Password Invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("User or Password Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                radButton1_Click(sender, e);
            }
        }
    }
}
