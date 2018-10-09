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
    public partial class scrLegal : UserControl
    {
        public bool hasAccepted = false;

        public scrLegal()
        {
            InitializeComponent();
        }

        private void cbTerms_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox) sender;
            hasAccepted =  (bool)cb.IsChecked;
        }
    }
}
