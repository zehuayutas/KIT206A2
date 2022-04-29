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
    /// Interaction logic for AddConsultationDialog.xaml
    /// </summary>
    /// 
    public partial class AddConsultation : Window
    {
        public int staffID = -1;
        ConsultationController consultationController;
        ClassController classController;
        public AddConsultation()
        {
            InitializeComponent();
            consultationController = new ConsultationController();
            classController = new ClassController();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {


            Consultation c = new Consultation();
            c.Day = (Day)dayInput.SelectedItem;
            c.StartTime = new TimeSpan(startHour.SelectedIndex + 9, startMin.SelectedIndex * 30, 0);
            c.EndTime = new TimeSpan(endHour.SelectedIndex + 9, endMin.SelectedIndex * 30, 0);
            c.StaffID = staffID;

            if (staffID != -1 && staffID != null)
            {


                List<Class> classList = classController.GetClassListByStaff(staffID);
                List<Consultation> consultList = consultationController.GetConsultationList(staffID);
                bool isClashed = false;

                foreach (Class cls in classList) {
                    Debug.WriteLine("how mnay classes they have" + classList[0].Staff);
                    //Check if it clashes with this staff's classes
                    if (c.Day == cls.Day && ((c.StartTime <= cls.StartTime && c.EndTime >= cls.StartTime) || (c.EndTime >= cls.EndTime && c.StartTime <= cls.EndTime)))
                    {
                        isClashed = true;

                    }
                }


                foreach (Consultation cons in consultList)
                {

                    //Check if it clashes with this staff's consultations
                    if (c.Day == cons.Day && ((c.StartTime <= cons.StartTime && c.EndTime >= cons.StartTime) || (c.EndTime >= cons.EndTime && c.StartTime <= cons.EndTime)))
                    {
                        isClashed = true;
                    }
                }

                if (isClashed == true)
                {
                    string message = "This staff already has class or consultation during this period!";
                    MessageBox.Show(message);
                }
                else {
                    consultationController.AddNewConsultation(c);
                    DialogResult = true;

                    Close();
                }
                
            }


        }
    }
}
