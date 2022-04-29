using HRIS.Controller;
using HRIS.Model;
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
using Type = HRIS.Model.Type;

namespace HRIS
{
    /// <summary>
    /// Interaction logic for AddClassDialog.xaml
    /// </summary>
    public partial class AddClassDialog : Window
    {
        private const string UNIT_LIST_KEY = "unitList";
        private const string STAFF_LIST_KEY = "staffList";
        private UnitController unitController;
        private StaffController staffController;
        private ClassController classController;
        public string unitCode="";
        public AddClassDialog()
        {
            InitializeComponent();
            InitializeComponent();
            unitController = (UnitController)(Application.Current.FindResource(UNIT_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            staffController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            classController = new ClassController();

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            
            Class c = new Class();
            c.Campus = (Campus)campusInput.SelectedItem;
            c.Type = (Type)campusInput.SelectedItem;
            c.Day = (Day)dayInput.SelectedItem;
            c.Room = roomInput.Text;
            c.StartTime = new TimeSpan(startHour.SelectedIndex + 9, startMin.SelectedIndex * 30, 0);
            c.EndTime = new TimeSpan(endHour.SelectedIndex + 9, endMin.SelectedIndex * 30, 0);
            c.Staff = staffController.GetStaffList()[coordInput.SelectedIndex].Id;
            c.UnitCode = unitCode;

            List<Class> classList = classController.GetClassList(unitCode);
            bool isTutorialSet = false;

            if (unitCode != "" && unitCode != null) {

                //Only on tutorial can be set for each unit
                foreach (Class cls in classList) {
                    if (cls.Type == Type.Tutorial) {
                        isTutorialSet = true;
                    }
                }

                if (isTutorialSet == true)
                {
                    string message = "The tutorial for this unit has already been set!";
                    MessageBox.Show(message);
                }
                else {

                    classController.AddNewClass(c);
                    DialogResult = true;
                    Close();
                }
               

            }


        }
    }
}
