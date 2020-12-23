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
    public partial class PrintHINO : Telerik.WinControls.UI.RadRibbonForm
    {
   
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        //public PrintHINO(Telerik.WinControls.UI.RadTextBox  CodeNox)
        //{
        //    InitializeComponent();
        //    CodeNo_tt = CodeNox;
        //    screen = 1;
        //}
        public PrintHINO()
        {
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
            dtShipDate.Value = DateTime.Now;
            dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
           // radDateTimePicker2.Value = DateTime.Now;
        }

        private void btn_PrintPR_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.sp_38_DeletePrintHeno();
            }
            PrintPR("3001","OEM", "BA02-a",@"2D/FZ");
            PrintPR("3001E","MSP", "BA02-a","OLT");
            PrintPR("3001S","Service", "BA02-a",@"S3/Non-FZ");

            PrintPR("300113V", "OEM", "BA02-a", @"2D/FZ");
            PrintPR("300153M", "MSP", "BA02-a", "OLT");            
            PrintPR("300113S", "Service", "BA02-a", @"S3/Non-FZ");
            PrintPR("300153S", "Service", "BA02-a", @"S3/Non-FZ");
            Report.Reportx1.Value = new string[2];
            //Report.Reportx1.Value[0] = radDateTimePicker1.Value.ToString();
            //Report.Reportx1.Value[1] = radDateTimePicker2.Value.ToString();
            Report.Reportx1.WReport = "PrintHiNO";
            Report.Reportx1 op = new Report.Reportx1("Report_Heno.rpt");
            op.Show();
        }

        private void PrintPR(string CSTM,string Plant,string Suppliera,string DocRc)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int countA = 0;
                    decimal Total = 0;
                    decimal Pack1 = 0;
                    decimal Pack2 = 0;
                    decimal Pack3 = 0;
                    decimal Pack4 = 0;
                    decimal Pack5 = 0;
                    string Order1 = "";
                    string Order2 = "";
                    string Order3 = "";
                    string Order4 = "";
                    string Order5 = "";
                    string GroupPP = "";
                    string GroupPP2 = "";
                    int GroupByPL = 0;
                    int SKID = 0;
                    var listGroup = db.sp_39_PrintHino_GroupPlant(dtShipDate.Value, dtShipDate.Value, txtPallet.Text,CSTM).ToList();
                    GroupByPL = listGroup.Count;
                    foreach (var lg in listGroup)
                    {
                        SKID += 1;
                        Total = 0;
                        //Create TAG//
                        var listP = db.sp_39_PrintHino_select_Dynamics(dtShipDate.Value, dtShipDate.Value, lg.PL, Plant, CSTM).ToList();
                        if (listP.Count > 0)
                        {
                            GroupPP=CSTM + "=" + lg.PL;
                            if (!GroupPP.Equals(GroupPP2))
                            {
                                countA = 0;
                                GroupPP2 = GroupPP;
                            }

                            Pack1 = 0;
                            Order1 = "";
                            Pack2 = 0;
                            Pack3 = 0;
                            Pack4 = 0;
                            Pack5 = 0;
                            Order2 = "";
                            Order3 = "";
                            Order4 = "";
                            Order5 = "";
                            countA = 0;
                            foreach (var rd in listP)
                            {
                                countA += 1;

                                if(Order1.Equals("") || Order1.Equals(rd.SaleOrder))
                                {
                                    Order1 = rd.SaleOrder;
                                    Pack1 += Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                }
                                else if(Order2.Equals("") || Order2.Equals(rd.SaleOrder))
                                {
                                    Order2 = rd.SaleOrder;
                                    Pack2 += Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                }
                                else if (Order3.Equals("") || Order3.Equals(rd.SaleOrder))
                                {
                                    Order3 = rd.SaleOrder;
                                    Pack3 += Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                }
                                else if (Order4.Equals("") || Order4.Equals(rd.SaleOrder))
                                {
                                    Order5 = rd.SaleOrder;
                                    Pack5 += Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                }
                                else if (Order5.Equals("") || Order5.Equals(rd.SaleOrder))
                                {
                                    Order5 = rd.SaleOrder;
                                    Pack5 += Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                }

                                /*
                                if (countA <= 5)
                                {
                                    
                                    if (countA.Equals(1))
                                    {
                                        Order1 = rd.SaleOrder;
                                        Pack1 = Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);

                                    }
                                    else if (countA.Equals(2))
                                    {
                                        Order2 = rd.SaleOrder;
                                        Pack2 = Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                    }
                                    else if (countA.Equals(3))
                                    {
                                        Order3 = rd.SaleOrder;
                                        Pack3 = Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                    }
                                    else if (countA.Equals(4))
                                    {
                                        Order4 = rd.SaleOrder;
                                        Pack4 = Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                    }
                                    else if (countA.Equals(5))
                                    {
                                        Order5 = rd.SaleOrder;
                                        Pack5 = Math.Round((Convert.ToDecimal(rd.qty) / Convert.ToDecimal(rd.LotSize)), MidpointRounding.AwayFromZero);
                                    }

                                    
                                }
                                */
                            }

                            Total += Pack1 + Pack2 + Pack3 + Pack4 + Pack5;
                            tb_PrintHeno hp = new tb_PrintHeno();
                            hp.Plant = Plant;
                            hp.DockReceive = DocRc;
                            hp.DepartureDate = dt1.Value;
                            hp.ArrivalDate = dt2.Value;
                            hp.Time1 = Time1.Text;
                            hp.Time2 = Time2.Text;
                            hp.Supplier = Suppliera;
                            hp.SKIDNo = SKID;
                            hp.SKIDOf = GroupByPL;
                            hp.TotalPackNo = Convert.ToInt32(Total);
                            hp.GroupA = GroupPP;

                            hp.OrderNo1 = Order1;
                            hp.PackNo1 = Convert.ToInt32(Pack1);

                            hp.OrderNo2 = Order2;
                            hp.PackNo2 = Convert.ToInt32(Pack2);

                            hp.OrderNo3 = Order3;
                            hp.PackNo3 = Convert.ToInt32(Pack3);

                            hp.OrderNo4 = Order4;
                            hp.PackNo4 = Convert.ToInt32(Pack4);

                            hp.OrderNo5 = Order5;
                            hp.PackNo5 = Convert.ToInt32(Pack5);

                            db.tb_PrintHenos.InsertOnSubmit(hp);
                            db.SubmitChanges();



                        }
                    }
                    //Create End TAG//

                    //End Create TAG//
                    //WP=PrintHiNO
                   
                }
            }
            catch { }
        }
        private void cboCSTM_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (cboCSTM.Text.Equals("3001"))
                {
                    txtDockRc.Text = @"2D/FZ";
                    txtSupplier.Text = "BA02-a";
                }
                else if (cboCSTM.Text.Equals("3001E"))
                {
                    txtDockRc.Text = @"OLT";
                    txtSupplier.Text = "BA02-a";
                }
                else if(cboCSTM.Text.Equals("3001S"))
                {
                    txtDockRc.Text = @"S3/Non-FZ";
                    txtSupplier.Text = "BA02-a";
                }
            }
            catch { }
        }
    }
}
