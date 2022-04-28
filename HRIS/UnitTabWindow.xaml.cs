using HRIS.Controller;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const string UNIT_LIST_KEY = "unitList";
        private const string STAFF_LIST_KEY = "staffList";
        private UnitController unitController;
        private StaffController staffController;

        public UnitTabWindow()
        {
            InitializeComponent();

            unitController = (UnitController)(Application.Current.FindResource(UNIT_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            staffController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;
        }

        //Name filter
        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = unitFilter.Text;
            unitController.FilterByCodeOrTitle(name);
        }

        private void UnitList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {

                Unit unit = (Unit)e.AddedItems[0];
                unitDetails.DataContext = unit;
                unitCoordinator.SelectedIndex = staffController.GetStaffIndex(unit.Coordinator);

            }

        }

        //Name filter
        private void FilterName(object sender, RoutedEventArgs e)
        {
            string filter = unitFilter.Text;
            unitController.FilterByCodeOrTitle(filter);

        }


        private void AddNewUnit(object sender, RoutedEventArgs e)
        {
            AddUnitDialog addUnitDialog = new AddUnitDialog();
            addUnitDialog.ShowDialog();
        }
    }
}
