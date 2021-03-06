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
using System.Drawing;

namespace HRIS
{

    public partial class StaffTabWindow : UserControl
    {
        private StaffController staffController;
        private ConsultationController consultationController;
        private const string STAFF_LIST_KEY = "staffList";
        private Staff current_Staff = new Staff();

        public StaffTabWindow()
        {
            InitializeComponent();
            staffController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            consultationController = new ConsultationController();



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
                current_Staff = staff;


                consultationList.ItemsSource = consultationController.GetConsultationList(staff.Id);


                staffPhoto.Source = staffController.ConvertByteArrayToBitMapImage(staff.Photo);

                //The following info can only be changed if they are empty
                if (staff.Phone == "" || staff.Phone == null) { phoneInput.IsEnabled = true; } else { phoneInput.IsEnabled = false; }
                if (staff.Email == "" || staff.Phone == null) { emailInput.IsEnabled = true; } else { emailInput.IsEnabled = false; }
                if (staff.Room == "" || staff.Phone == null) { roomInput.IsEnabled = true; } else { roomInput.IsEnabled = false; }

                saveChangeBtn.IsEnabled = true;
                changeImgBtn.IsEnabled = true;
                addConsulBtn.IsEnabled = true;
            }

        }

        private void UpdateStaff(object sender, RoutedEventArgs e)
        {
            Staff s = current_Staff;
            s.Phone = phoneInput.Text;
            s.Email = emailInput.Text;
            s.Room = roomInput.Text;
            s.Title = titleInput.Text;
            s.Photo = staffController.BitMapImageToByteArray((BitmapImage)staffPhoto.Source);

            //Debug.WriteLine("Phtto is" + s.Photo.Length);

            if ((Category)cateInput.SelectedIndex != 0)
            {
                s.Category = (Category)cateInput.SelectedIndex;
            }

            s.Campus = (Campus)campusInput.SelectedIndex;
      
            
            staffController.UpdateStaffDetails(current_Staff);
        }

        private void AddNewConsultation(object sender, RoutedEventArgs e)
        {
            AddConsultation addConsultationDialog = new AddConsultation();
            addConsultationDialog.staffID = current_Staff.Id;
            addConsultationDialog.ShowDialog();
            
            if(addConsultationDialog.DialogResult == true)
            {
                //reload data
                consultationList.ItemsSource = consultationController.GetConsultationList(current_Staff.Id);
            }
        }

        private void ChangePhoto(object sender, RoutedEventArgs e)
        {
           

            BitmapImage image = new BitmapImage(new Uri("/Pictures/profilephoto.png", UriKind.Relative));
            staffPhoto.Source = image;
        }

        private void DeleteConsultation(object sender, RoutedEventArgs e)
        {

            if (consultationList.SelectedItem != null)
            {
                int consultationIdx = consultationList.SelectedIndex;
                Consultation c = consultationController.GetConsultationList(current_Staff.Id)[consultationIdx];
                consultationController.DeleteConsultation(c);

                //reload data
                consultationList.ItemsSource = consultationController.GetConsultationList(current_Staff.Id);
            }
        }
    }
}
