using ATMDesigner.Common;
using ATMDesigner.UI.Helpers;
using ATMDesigner.UI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace ATMDesigner.UI.ViewModel
{
    public class ViewModelMainWindow:ViewModelBase
    {
        public RelayCommand PreviewDrop_Event {get; set; }
        //public RelayCommand EventedCommand2 { get; set; }       
        //public RelayCommand EventedCommand4 { get; set; }

        #region Propertyies       
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
                    //OnPropertyChanged("StateObj");
                    // Mediator.NotifyColleagues("ViewB", value);
                }
            }
        }
        #endregion


        public ViewModelMainWindow()
        {
          // PreviewDrop_Event = new RelayCommand(ViewPropertyGrid);
            //EventedCommand2 = new RelayCommand(DoStuff2);            
           // EventedCommand4 = new RelayCommand(ViewPropertyGrid);
           
        }

        void DoStuff2(object param)
        {
            var e = param as MouseButtonEventArgs;
            MessageBox.Show("Command executed. ButtonState: " + e.ButtonState);
        }

        //on Drag Drop event
        private void ViewPropertyGrid(object param)
        {           
            DragEventArgs e = param as DragEventArgs;          
            ViewModelDesignerCanvas dcanvas = new ViewModelDesignerCanvas();
            //dcanvas = (ViewModelDesignerCanvas)sender;
            //SelectedPgrid = new PropertyGrid();

            ModelDragObject dragObject = e.Data.GetData(typeof(ModelDragObject)) as ModelDragObject;
            ModelCanvasStateObject state = new ModelCanvasStateObject();

            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                ViewModelDesignerItem newItem = null;
                Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));
                if (content != null)
                {
                    newItem = new ViewModelDesignerItem();
                    DockPanel pnl = (DockPanel)content;
                    string frmName = "State" + pnl.Tag.ToString();
                    string Id = (dcanvas.avaliableStateNumberList[dcanvas.TransactionList.Count]).ToString();
                    state.Type = pnl.Tag.ToString();


                    Type CAType = Type.GetType("ATMDesigner.UI.States." + frmName);
                    Object myObj = Activator.CreateInstance(CAType);

                    PropertyInfo StateNo = CAType.GetProperty("StateNumber");
                    StateNo.SetValue(myObj, Id, null);

                    PropertyInfo stateType = CAType.GetProperty("StateType");
                    stateType.SetValue(myObj, pnl.Tag.ToString(), null);

                    PropertyInfo Desc = CAType.GetProperty("StateDescription");
                    Desc.SetValue(myObj, frmName, null);                   
                    StateObj = myObj;                
                   
                }
            }
        }

        public void ViewPropertyGrid(List<IModelSelectable> CurrentSelection)
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
            StateObj = myObj;
        
        }


    }



}
