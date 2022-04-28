using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Model
{
    //The enum of campus for staff
    public enum Campus { All, Hobart, Launceston };
    //The enum of category for staff
    public enum Category { All, academic, technical, admin, casual };


    class Staff
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public Campus Campus { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public Category Category { get; set; }


        public override string ToString()
        {
            return GivenName + " " + FamilyName + "(" + Title + ")";
        }
    }
}
