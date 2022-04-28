using HRIS.Database;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HRIS.Controller
{
    class StaffController
    {
        private List<Staff> staffList;
        private ObservableCollection<Staff> staffListObv;
        public StaffController()
        {
            staffList = DBAdapter.GetAllStaff();
            staffListObv = new ObservableCollection<Staff>(staffList);

        }


        public ObservableCollection<Staff> GetStaffList()
        {
            return staffListObv;
        }


        //Name filter
        public void FilterByName(string name, int category)
        {

            if ((Category)category == Category.All)
            {
                var FilterByName = from Staff e in staffList
                                   where e.GivenName.ToLower().Contains(name.ToLower()) || e.FamilyName.ToLower().Contains(name.ToLower())
                                   select e;
                staffListObv.Clear();
                FilterByName.ToList().ForEach(staffListObv.Add);
            }
            else
            {
                var FilterByName = from Staff e in staffList
                                   where (e.GivenName.ToLower().Contains(name.ToLower()) || e.FamilyName.ToLower().Contains(name.ToLower())) && e.Category == (Category)category
                                   select e;
                staffListObv.Clear();
                FilterByName.ToList().ForEach(staffListObv.Add);
            }
        }

        //Category filter
        public void FilterByCategory(Category category)
        {
            Debug.WriteLine("Category selected is" + category);
            if (category != Category.All)
            {

                var FilterByName = from Staff e in staffList
                                   where e.Category == category
                                   orderby e.GivenName
                                   select e;

                Debug.WriteLine("Filtered Name" + FilterByName.ToList());

                staffListObv.Clear();
                FilterByName.ToList().ForEach(staffListObv.Add);
                Debug.WriteLine("Obv list" + FilterByName.ToList().Count());
            }
            else {
                staffListObv.Clear();
                staffList.ForEach(staffListObv.Add);
            }
     
        }


        //Get staff details
        public Staff GetStaffDetails(Staff staff)
        {
            staff = DBAdapter.GetStaffDetails(staff);
            return staff;
        }

    }



}
