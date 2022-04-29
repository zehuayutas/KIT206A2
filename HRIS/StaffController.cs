using HRIS.Database;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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

            if (category != Category.All)
            {

                var FilterByName = from Staff e in staffList
                                   where e.Category == category
                                   orderby e.GivenName
                                   select e;


                staffListObv.Clear();
                FilterByName.ToList().ForEach(staffListObv.Add);

            }
            else {
                staffListObv.Clear();
                staffList.ForEach(staffListObv.Add);
            }

        }


        //Get staff details
        public Staff GetStaffByID(int id)
        {
            return DBAdapter.GetStaffByID(id);
        }

        public int GetStaffIndex(int id) {
            int index = -1;

            for (int idx = 0; idx < staffListObv.Count; idx++) {
                if (staffListObv[idx].Id == id) {
                   
                    index = idx;
                }
            }

            return index;


        }

        //Update staff
        public void UpdateStaffDetails(Staff s)
        {
            DBAdapter.UpdateStaffDetails(s);
        }


        //Concert Byte Arry to BitMap Image 
        //Reference: "https://social.msdn.microsoft.com/Forums/sqlserver/en-US/dc44d5b1-4b3b-4bf7-befa-dc443321e8de/displaying-an-image-from-sqlite-database-to-wpf-datagrid?forum=wpf"
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            if (imageByteArray != null && imageByteArray.Length > 0)
            {
                BitmapImage img = new BitmapImage();
                using (MemoryStream memStream = new MemoryStream(imageByteArray))
                {
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.StreamSource = memStream;
                    img.EndInit();
                    img.Freeze();
                }

                return img;
            }
            else
            {
                return null;
            }

        }

        //Concert  BitMap Image to Byte Arry]
        //Reference: "https://stackoverflow.com/questions/6597676/bitmapimage-to-byte"
        public byte[] BitMapImageToByteArray(BitmapImage img)
        {

            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        
}



}
