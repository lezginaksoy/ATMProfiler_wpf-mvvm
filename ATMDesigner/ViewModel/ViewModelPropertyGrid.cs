using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace ATMDesigner.UI.ViewModel
{
    public class ViewModelPropertyGrid :ViewModelBase 
    {

        Object _stateObj;
        public Object StateObj
        {
            get
            {
                return _stateObj;
            }
            set
            {
                if (_stateObj != value)
                {
                    _stateObj = value;

                     OnPropertyChanged("StateObj");
                    // Mediator.NotifyColleagues("ViewB", value);
                }
            }
        }

        public ViewModelPropertyGrid(Object obj)
        {
            StateObj =obj;
        }


        void DoAction(object param)
        {
            StateObj = param as Object;
            //OnPropertyChanged("StateObj");
        }

      

        public ViewModelPropertyGrid(List<IModelSelectable> CurrentSelection)
        {
            
            var Selection = CurrentSelection;
            ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
            foreach (var slc in Selection)
            {
                Ditem = (ViewModelDesignerItem)slc;
            }

            DockPanel Sourcepnl = (DockPanel)Ditem.Content;
            string frmName = "State" + Sourcepnl.Tag.ToString();

            Type CAType = Type.GetType("ATMDesigner.UI.States." + frmName);
            Object myObj = Activator.CreateInstance(CAType);

            PropertyInfo StateNo = CAType.GetProperty("StateNumber");
            StateNo.SetValue(myObj, Sourcepnl.Uid, null);

            PropertyInfo stateType = CAType.GetProperty("StateType");
            stateType.SetValue(myObj, Sourcepnl.Tag.ToString(), null);

            PropertyInfo Desc = CAType.GetProperty("StateDescription");
            Desc.SetValue(myObj, frmName, null);

            List<Object> obj = new List<object>();
            obj.Add(myObj);
            var peopleList = obj;

         

        }

    }
}
