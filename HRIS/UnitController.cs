using HRIS.Database;
using HRIS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRIS.Controller
{
    class UnitController
    {
        private List<Unit> unitList;
        private ObservableCollection<Unit> unitListObv;

        public UnitController()
        {
            Debug.WriteLine("Controller initialised loaded");

            unitList = DBAdapter.GetAllUnits();
            unitListObv = new ObservableCollection<Unit>(unitList);
        }

        public ObservableCollection<Unit> GetUnitList()
        {
            return unitListObv;
        }

        //Code and Title filter
        public void FilterByCodeOrTitle(string key)
        {
            //Use to make sure whether the key contains number
            Regex r = new Regex(@"\d");

            if (r.IsMatch(key))
            {
                var Filter = from Unit u in unitList
                             where u.Code.ToLower().Contains(key.ToLower())
                             select u;
                unitListObv.Clear();
                Filter.ToList().ForEach(unitListObv.Add);
            }
            else
            {
                var Filter = from Unit u in unitList
                             where u.Title.ToLower().Contains(key.ToLower())
                             select u;
                unitListObv.Clear();
                Filter.ToList().ForEach(unitListObv.Add);
            }

        }

    }
}
