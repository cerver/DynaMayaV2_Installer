using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DynaMaya
{
    /// <summary>
    /// Interaction logic for scrWelcome.xaml
    /// </summary>
    public partial class scrWelcome : UserControl
    {
        public bool v19checked = false;
        public bool v18checked = true;

        public scrWelcome()
        {
            InitializeComponent();
        }

        private void v2018_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox) sender;
            v18checked = (bool)cb.IsChecked;
        }

        private void v2019_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            v19checked = (bool)cb.IsChecked;
        }
    }
}
