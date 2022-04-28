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

namespace HRIS
{
    /// <summary>
    /// Interaction logic for UnitTabWindow.xaml
    /// </summary>
    public partial class UnitTabWindow : UserControl
    {
        public UnitTabWindow()
        {
            InitializeComponent();
        }

        private void AddNewUnit(object sender, RoutedEventArgs e)
        {
            AddUnitDialog addUnitDialog = new AddUnitDialog();
            addUnitDialog.ShowDialog();
        }
    }
}
