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

namespace HRIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        private UnitTabWindow unitTabWindow;
        private StaffTabWindow staffTabWindow;

        public MainWindow()
        {
            InitializeComponent();
            unitTabWindow = new UnitTabWindow();
            staffTabWindow = new StaffTabWindow();


        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {

                staffTab.Content = staffTabWindow;
            }
            else if (tabControl.SelectedIndex == 1)
            {
                unitTab.Content = unitTabWindow;
            }
        }
    }
}
