using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Model
{
    class Class
    {
        //Create a new enum to define type of unitclass
    public enum Type { Lecture, Tutorial, Practical, Workshop };

    class UnitClass
    {
        public int Staff { get; set; }
        public string UnitCode { get; set; }
        public Campus Campus { get; set; }
        public Day Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Type Type { get; set; }
        public string Room { get; set; }

        public override string ToString()
        {
            return Campus + "\n" + Day + "\n" + StartTime + "-" + EndTime + "\n" + Type + "\n" + Room;
        }
    }
    }
}
