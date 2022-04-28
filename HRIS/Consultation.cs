using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Model
{
    public enum Day { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };
    class Consultation
    {
        public int StaffID { get; set; }
        public Day Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public override string ToString()
        {
            return "Consultation: " + Day + " " + StartTime + "-" + EndTime;
        }
    }
}
