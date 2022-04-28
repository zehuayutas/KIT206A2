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
        private ClassController classController;
        private List<int> insertedindices= new List<int>();

        public UnitTabWindow()
        {
            InitializeComponent();

            unitController = (UnitController)(Application.Current.FindResource(UNIT_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            staffController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            classController = new ClassController();
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

                LoadClassTimetable(unit, staffController.GetStaffList()[unitCoordinator.SelectedIndex]);
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



        //Set the time table
        public static int WORK_START_TIME = 9;   //define the start time displayed in the timetable
        public static int WORK_END_TIME = 17;    //define the end time displayed in the timetable
        public static int WORK_DAYS = 5;
        private void LoadClassTimetable(Unit u, Staff s)
        {
            //remove classes from previously selected class
            if (insertedindices != null && insertedindices.Count > 0) {
                foreach (int idx in insertedindices) {
                    timeTable.Children.RemoveAt(idx);
               
                }
            }

            insertedindices.Clear();

            BrushConverter bgColour = new BrushConverter();

            List<Class> classList = classController.GetClassList(u.Code);

            string displayContent="";//the content displayed on a cell

            foreach (Class c in classList){

                Label label = new Label();
                if ((c.StartTime.Hours - WORK_START_TIME) * 2 + 1 > 0)
                {
                    label.SetValue(Grid.RowProperty, (c.StartTime.Hours - WORK_START_TIME) * 2 + 1);
                }
                else { label.SetValue(Grid.RowProperty, 1); }

                if ((c.EndTime.Hours - c.StartTime.Hours) * 2 + (c.EndTime.Minutes - c.StartTime.Minutes) / 30 > 0) {
                    label.SetValue(Grid.RowSpanProperty, (c.EndTime.Hours - c.StartTime.Hours) * 2 + (c.EndTime.Minutes - c.StartTime.Minutes) / 30);
                }

                label.SetValue(Grid.ColumnProperty, (int)c.Day);
                label.Background = (Brush)bgColour.ConvertFrom("#FF00FFFF");

                label.Content = String.Format("Unit Code: {0}\nType: {1}\nLocation: {2}\nCampus: {3}\nStaff: {4}", u.Code, c.Type, c.Room, c.Campus, s.ToString());
                timeTable.Children.Add(label);

                insertedindices.Add(timeTable.Children.IndexOf(label));
                Debug.WriteLine("index added" + timeTable.Children.IndexOf(label));
            }
        }
    }
}
