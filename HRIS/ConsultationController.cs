using HRIS.Database;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Controller
{
    class ConsultationController
    {

        private List<Consultation> consultationList;

        public ConsultationController()
        {

            //consultationList = DBAdapter.GetConsultationsByStaff();
            //consultationListObv = new ObservableCollection<Consultation>(consultationList);
        }

        public List<Consultation> GetConsultationList(int id)
        {
            consultationList = DBAdapter.GetConsultationsByStaff(id);
            return consultationList;
        }

        public void AddNewConsultation(Consultation c) {
            DBAdapter.AddNewConsultation(c);
        }

        public void DeleteConsultation(Consultation c)
        {
            DBAdapter.DeleteConsultation(c);
        }
    }
}
