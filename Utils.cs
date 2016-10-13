using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LPATest
{
    class Utils
    {
        public static ResourceDictionary GetResourceDict(string cult)
        {

            ResourceDictionary dict = new ResourceDictionary();

            //   switch (Thread.CurrentThread.CurrentCulture.ToString())
            if ((cult == null) || (cult == String.Empty))
            {
                cult = Properties.Settings.Default.Culture;
            }
            switch (cult)
            {
                case "zh-CN":
                    dict.Source = new Uri("..\\Resources\\StringResources.zh-CN.xaml", UriKind.Relative);
                    break;
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative);
                    break;
                case "fr-CA":
                    dict.Source = new Uri("..\\Resources\\StringResources.fr-CA.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            return dict;
        }
        public static void SetCulture(string cult)
        {

            Properties.Settings.Default.Culture = cult;

            Properties.Settings.Default.Save();
        }
    }
}
