using System;
using System.Collections.Generic;
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
    /// Interaction logic for WindowTest.xaml
    /// </summary>
    public partial class WindowTest : Window
    {
        LPAAllTest alltest;
        public WindowTest()
        {
            InitializeComponent();

            this.SetLanguageDictionary();

            alltest = new LPAAllTest();
            this.DataContext = alltest;
            alltest.startPrinterTest();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            alltest.stop();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Rect workArea = SystemParameters.WorkArea;
                this.Left = (workArea.Width - e.NewSize.Width) / 2 + workArea.Left;
                this.Top = (workArea.Height - e.NewSize.Height) / 2 + workArea.Top;

            }
            catch (Exception ex)
            {
                // ... Handel exception;
            }
        }

        private void btnPrinter2_Click(object sender, RoutedEventArgs e)
        {
            ZebraTestPrint zebraTestPrint = new ZebraTestPrint();
            if (zebraTestPrint.ChecknLoadZPLForTestPrint(2))
            {
                MessageBox.Show("Printer OK");
            }
            else
            {
                MessageBox.Show("Printer Failed");
            }
        }

        private void btnPrinter4_Click(object sender, RoutedEventArgs e)
        {
            ZebraTestPrint zebraTestPrint = new ZebraTestPrint();
           if ( zebraTestPrint.ChecknLoadZPLForTestPrint(4))
            {
                MessageBox.Show("Printer OK");
            }
           else
            {
                MessageBox.Show("Printer Failed");
            }
        }

        private void btnPrinter7_Click(object sender, RoutedEventArgs e)
        {
            ZebraTestPrint zebraTestPrint = new ZebraTestPrint();
            if (zebraTestPrint.ChecknLoadZPLForTestPrint(7))
            {
                MessageBox.Show("Printer OK");
            }
            else
            {
                MessageBox.Show("Printer Failed");
            }
        }

        private void SetLanguageDictionary()
        {
            this.Resources.MergedDictionaries.Add(Utils.GetResourceDict(Properties.Settings.Default.Culture));

 
            //CultureList.SelectedValue= Properties.Settings.Default.Culture;

            //Console.WriteLine("selected="+CultureList.SelectedValue);

            foreach (ComboBoxItem item in CultureList.Items)
            {
                if (item.Content.ToString()== Properties.Settings.Default.Culture)
                {
                    CultureList.SelectedItem = item;
                }
            }
        }
        private void CultureList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)CultureList.SelectedItem;
            if (typeItem.Content != null)
            {
                string selectedCul = typeItem.Content.ToString();
                this.Resources.MergedDictionaries.Add(Utils.GetResourceDict(selectedCul));
                Utils.SetCulture(selectedCul);
            }
            else
            {

            }
        }
    }
}
