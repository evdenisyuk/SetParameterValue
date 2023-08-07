using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SetpValue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string parameterText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetValue(object sender, RoutedEventArgs e)
        {
            parameterText = parValue.Text;
            DialogResult = true;
        }

        private void CancelCommand(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
