using LPATest.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LPATest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TblInOutTableAdapter tblInOutTableAdapter = new TblInOutTableAdapter();
            DataSet1 dataSet1 = new DataSet1();
            ReportQuery rq = new ReportQuery();

            tblInOutTableAdapter.Fill(dataSet1.TblInOut);

            DataTable dt = rq.GettAllDb();

            if (dataSet1.TblInOut.Rows.Count == 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataSet1.TblInOutRow newRow = (DataSet1.TblInOutRow)dataSet1.TblInOut.NewRow();

                    //                   newRow.Id =(int) row["Id"];
                    newRow.FL = (String)row["FL"];
                    newRow.DTIn = (DateTime)row["DTIn"];

                    try
                    {
                        if (row["DTOut"] != DBNull.Value) newRow.DTOut = (DateTime)row["DTOut"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DTOut: " + row["DTOut"] + "," + ex.Message);
                    }
                    try
                    {
                        if (row["STnNo"] != DBNull.Value)
                        {
                            newRow.STnNo = (int)row["STnNo"];
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(row["STnNo"] + "," + ex.Message);
                    }
                    if (row["Stat"] != null) newRow.Stat = (byte)row["Stat"];
                    if (row["Reason"] != null) newRow.Reason = (short)row["Reason"];


                    dataSet1.TblInOut.Rows.Add(newRow);
                    //       dataSet1.TblInOut.AddTblInOutRow(newRow);

                }
            }


            try
            {

                if (dataSet1.HasChanges())
                {
                    tblInOutTableAdapter.Update(dataSet1);
                }

                dataSet1.TblInOut.AcceptChanges();
                dataSet1.AcceptChanges(); //can't insert into database after that
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //        DataGrid1.ItemsSource = dataSet1.TblInOut;

            DataGrid1.ItemsSource = new DataView(rq.GettAllInOut().Tables[0]);


        }
    }
}
