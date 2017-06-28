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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            Calculate();
        }
        private void Calculate()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                    if (MessageBox.Show("ต้องการคำนวณ หรือไม่?", "Calculate", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        int value1 = 0;
                            int yyyy = DateTime.Now.Year;
                            int month = DateTime.Now.Month;
                            yyyy = Convert.ToInt32(cboYear.Text);
                            month = dbClss.getMonth(cboMonth.Text);
                        var db1 = (from ix in db.sp_SelectProduction_Year(yyyy, month) select ix).ToList();
                        if(db1.Count>0)
                        {
                            progressBar1.Maximum = db1.Count + 1;
                            foreach (var d in db1)
                            {
                                //////////////////
                                db.sp_SelectProduction_Cal(d.ModelName, yyyy, month, Convert.ToDecimal(d.Consump));
                                //////////////////
                                value1 += 1;
                                progressBar1.Value = value1;
                                progressBar1.PerformStep();
                            }
                           // dbClss.AddHistory("CalculatePlanning", "Calculate", "คำนวณจุดสั่งซื้อ โดย " + Environment.UserName + "Year=" + yyyy.ToString() + ",Month=" + month.ToString(), "");
                            MessageBox.Show("Apply เรียบร้อยแล้ว!");
                        }

                        
                    }
                }
                catch { }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 2017; i < DateTime.Now.Year+10; i++)
            {
                cboYear.Items.Add(i.ToString());

            }
        }
    }
}
