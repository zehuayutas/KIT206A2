using HRIS.Database;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Controller
{
    class ClassController
    {
        private List<Class> classList;

        public ClassController()
        {

            //consultationList = DBAdapter.GetConsultationsByStaff();
            //consultationListObv = new ObservableCollection<Consultation>(consultationList);
        }

        public List<Class> GetClassList(string unitCode)
        {
            classList = DBAdapter.GetUnitClasses(unitCode);
            return classList;
        }

        public void AddNewClass(Class c) 
        {
            DBAdapter.AddNewClass(c);
        }
    }
}
