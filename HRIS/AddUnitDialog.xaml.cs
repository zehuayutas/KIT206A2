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
    /// Interaction logic for AddUnitDialog.xaml
    /// </summary>
    public partial class AddUnitDialog : Window
    {
        private const string UNIT_LIST_KEY = "unitList";
        private const string STAFF_LIST_KEY = "staffList";
        private UnitController unitController;
        private StaffController staffController;
        private bool isAdded = false;


        public AddUnitDialog()
        {
            InitializeComponent();
            unitController = (UnitController)(Application.Current.FindResource(UNIT_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            staffController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            Unit u = new Unit();
            u.Code = codeInput.Text;
            u.Title = nameInput.Text;
            u.Coordinator = staffController.GetStaffList()[(coordInput.SelectedIndex)].Id;

            if (u.Code != "") {
                Debug.WriteLine("Code is" + u.Code);
                unitController.AddNewUnit(u);
                unitController.AddToObvList(u);

            }

            
            Close();
        }
    }
}
