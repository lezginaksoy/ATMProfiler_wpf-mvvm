using ATMDesigner.Common;
using ATMDesigner.UI.Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace ATMDesigner.UI.States
{
    [DefaultPropertyAttribute("Name")]
    public class StateP : StateBase
    {

        public StateP(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            StateType = "P";
            StateDescription = "StatePointer";
            _NextStateNumber = "255";
        }

        public StateP()
        {
            StateType = "P";
            StateDescription = "StatePointer";
            _NextStateNumber = "255";
           // _SourceStateNumber = "255";
            SourceStatesList = new List<PointerStateElements>();
        }

        #region Converters

        public class SetNextStateNumber : ITypeEditor
        {
            Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem property;
            public FrameworkElement ResolveEditor(PropertyItem propertyItem)
            {
                property = propertyItem;
                Button button = new Button();
                button.Name = property.DisplayName;
                button.Content = property.Value;
                button.Click += button_Click;
                return button;
            }
            void button_Click(object sender, RoutedEventArgs e)
            {
                Button btn = (Button)sender;
                NextStateSelection StateSelectionPopup = new NextStateSelection(designerCanvas.TransactionList, btn.Content.ToString(),designerCanvas.CurrentBrandId,designerCanvas.CurrentTransactionName);
                StateSelectionPopup.ShowDialog();               

                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }

                DockPanel pointerpnl = (DockPanel)Ditem.Content;
                string PointerTransactionName = designerCanvas.TransactionList.Find(x => x.Id == pointerpnl.Uid).TransactionName;
                StateP statepointer = (StateP)designerCanvas.TransactionList.Find(x => x.Id == pointerpnl.Uid).PropertyGrid.SelectedObject;

                if (statepointer.SourceStatesList.Count>0)
                {
                    btn.Content = StateSelectionPopup.NextStateNumber;
                    string SelectedProperty = btn.Name.ToString();
                    string SelectedPropertyValue = btn.Content.ToString();

                    //Pointer state güncellemesi
                    Type ClassType = statepointer.GetType();
                    PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                    property.SetValue(statepointer, SelectedPropertyValue, null);
                    designerCanvas.TransactionList.Find(x => x.Id == pointerpnl.Uid).PropertyGrid.SelectedObject = statepointer;
                    List<PointerStateElements> elementlist = new List<PointerStateElements>();

                    for (int i = 0; i < statepointer.SourceStatesList.Count; i++)
                    {
                        //Source State Güncellemesi             
                        Object SourceStateobj = designerCanvas.TransactionList.FindAll(x => x.Id == statepointer.SourceStatesList[i].SourceStateNumber).
                            Find(x=>x.TransactionName==PointerTransactionName).PropertyGrid.SelectedObject;
                        Type SourceClassType = SourceStateobj.GetType();
                        PropertyInfo SourcePropertyName = SourceClassType.GetProperty(statepointer.SourceStatesList[i].SourcePropertyName);
                        SourcePropertyName.SetValue(SourceStateobj, statepointer.NextStateNumber);
                        designerCanvas.TransactionList.Find(x => x.Id == statepointer.SourceStatesList[i].SourceStateNumber).PropertyGrid.SelectedObject = SourceStateobj;
                    } 

                  
                }
                else
                {
                    MessageBox.Show("Lütfen Öncellikle Pointer State bir bağlantı oluşturun..!");
                }


            }

        }

        
        #endregion

        #region state parameters

        private string _NextStateNumber;
        
        [CategoryAttribute("State Parameters"), DescriptionAttribute("Next State Number")]
        [Editor(typeof(SetNextStateNumber), typeof(SetNextStateNumber))]
        public string NextStateNumber
        {
            get
            {
                return _NextStateNumber;
            }

            set
            {
                _NextStateNumber = value.PadLeft(3, '0');
            }
        }

        public List<PointerStateElements> SourceStatesList=new List<PointerStateElements>();



        #endregion

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, object ClassInstance, Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid SelectedPgrid)
        {
            return null;
        }

        public  Object FillPropertyGridFromPointerState(object ClassInstance,string SourceStateNumber,string SourceStateType,string SourcePropertyName, Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid SelectedPgrid)
        {
            StateP Selectedstate = new StateP();
            StateP Dynamicstate = new StateP();
            Selectedstate = (StateP)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateP)ClassInstance;
            
            //Dynamicstate._SourcePropertyName = SourcePropertyName;
            //Dynamicstate._SourceStateNumber = SourceStateNumber;
            //Dynamicstate._SourceStateType = SourceStateType;
           //Dynamicstate._NextStateNumber = Selectedstate._NextStateNumber;
            return Dynamicstate;
        }
        
        public override object FillPropertyGridFromState(object ClassInstance, Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid SelectedPgrid)
        {
            return null;

        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateP State = new StateP();
            State = (StateP)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string SourceStateType="",SourceStateNumber="",SourcePropertyName="";

            for (int i = 0; i < State.SourceStatesList.Count; i++)
            {
                SourceStateType = SourceStateType + ";" + State.SourceStatesList[i].SourceStateType;
                SourceStateNumber = SourceStateNumber + ";" + State.SourceStatesList[i].SourceStateNumber;
                SourcePropertyName = SourcePropertyName + ";" + State.SourceStatesList[i].SourcePropertyName;
               // SourceTransactionName = SourceTransactionName + ";" + State.SourceStatesList[i].SourceTransactionName;
            }


            string sql = SqlStr;
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.NextStateNumber, SourceStateType,SourceStateNumber,
                SourcePropertyName, "255", "255", "255", "255",
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }


        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateP state = new StateP();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            PointerStateElements elements;
         

            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._NextStateNumber = processRow[8].ToString();
            
            string[] sourceStateType =processRow[9].ToString().Split(';');
            string[] sourceStateNumber = processRow[10].ToString().Split(';');
            string[] sourcePropertyName = processRow[11].ToString().Split(';');
           // string[] sourceTransactionName = processRow[12].ToString().Split(';');

            if (sourceStateType.Count() == sourceStateNumber.Count() && sourceStateType.Count() == sourcePropertyName.Count())
            {
                for (int i = 1; i < sourceStateType.Count(); i++)
                {
                    elements = new PointerStateElements();
                    elements.SourcePropertyName = sourcePropertyName[i];
                    elements.SourceStateNumber = sourceStateNumber[i];
                    elements.SourceStateType = sourceStateType[i];
                   // elements.SourceTransactionName = sourceTransactionName[i];
                    state.SourceStatesList.Add(elements);
                }
                
            }
          

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            TransStateObj.BrandId = state.BrandId;
            TransStateObj.ConfigId = state.ConfigId;
            TransStateObj.Id = state.StateNumber;
            TransStateObj.StateDescription = state.StateDescription;
            TransStateObj.Type = state.StateType;
            TransStateObj.TransactionName = processRow[7].ToString();

            TransStateObj.PropertyGrid.SelectedObject = state;
            TransStateObj.ParentStateList = ParentobjList;
            TransStateObj.ChildStateList = ChildobjList;
            designerCanvas.TransactionList.Add(TransStateObj);

            return StateList;
        }
           
        #endregion
        

    }


  public class PointerStateElements
   {     
       public string SourceStateNumber;
       public string SourcePropertyName;
       public string SourceStateType;
       //public string SourceTransactionName;
   }


}
