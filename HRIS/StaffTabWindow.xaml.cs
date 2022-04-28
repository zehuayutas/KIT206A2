using HRIS.Controller;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for StaffTabWindow.xaml
    /// </summary>
    public partial class StaffTabWindow : UserControl
    {
        private StaffController staffController;
        private const string STAFF_LIST_KEY = "staffList";


        public StaffTabWindow()
        {
            InitializeComponent();
            staffController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

       
            
        }

        //Name filter
        private void FilterName(object sender, RoutedEventArgs e)
        {
            string name = filterbyname.Text;

            staffController.FilterByName(name, filter.SelectedIndex);

        }

        //Category filter
        private void FilterbyCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (staffController != null)
            {
                staffController.FilterByCategory((Category)e.AddedItems[0]);
            }

        }


        private void StaffList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Staff staff = (Staff)e.AddedItems[0];
                StaffDetails.DataContext = staff;

            }

        }


        
    }
}
