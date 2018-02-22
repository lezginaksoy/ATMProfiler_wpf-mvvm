using System;
using System.ComponentModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Collections;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows.Data;
using System.Windows.Input;
using ATMDesigner.Common;


namespace ATMDesigner.UI.States
{
    
    //  / Complete ICC Application Selection & Initialisation State
    public class StateSlash : StateBase
    {

        public StateSlash(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateSlash()
        {
            SetDefaultValues();
        }    


        #region Converters

        public class ICIInitRequirmentConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000, "Always begin ICC initialisation");
                strings.Add(001,"Only begin ICC initialisation");
                return strings;
            }
        }

        public class AutomaticICCAppFlagConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"Cardholder ICC app. selec. or confirm. to be performed.");
                strings.Add(001,"Automatic ICC app. selec. to be performed.");
                return strings;
            }
        }
        
        public class ICCAppValidationFlagConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"Check if the ICC app. name is provided.");
                strings.Add(001,"Do not check if the ICC app. name is provided");
                strings.Add(002,"Check if the ICC app. name is provided.");                
                return strings;
            }
        }

        public class CardholderConfirmationFlagConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000," Do not check if the ICC app. requires cardholder confirm.");
                strings.Add(001,"Check if the ICC application requires cardholder confirm.");
               return strings;
            }
        }
        
        #endregion

        #region state parameters

        private string _PleaseWaitScreenNumber;
        private string _ICCAppTemplateScreenNumber;
        private string _ICCAppNameScreenNumber;
        private string _Reserved;

        [CategoryAttribute("State Parameters"), PropertyOrder(1), DescriptionAttribute("Please Wait Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string PleaseWaitScreenNumber
        {
            get
            {
                return _PleaseWaitScreenNumber;
            }

            set
            {
                _PleaseWaitScreenNumber = value.PadLeft(3, '0');
            }
        }


        [CategoryAttribute("State Parameters"), PropertyOrder(2), DescriptionAttribute("ICC App. Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ICCAppTemplateScreenNumber
        {
            get
            {
                return _ICCAppTemplateScreenNumber;
            }

            set
            {
                _ICCAppTemplateScreenNumber = value.PadLeft(3, '0');
            }
        }
        
        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("ICC App. Name Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]  
        public string ICCAppNameScreenNumber
        {
            get
            {
                return _ICCAppNameScreenNumber;
            }

            set
            {
                _ICCAppNameScreenNumber = value.PadLeft(3, '0');
            }
        }
           
        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true),Browsable(false), DescriptionAttribute("Reserved")]
        public string Reserved
        {
            get
            {
                return _Reserved;
            }

            set
            {
                _Reserved = value;
            }
        }

        #endregion

        #region Extension States

        private string _ExitPathsExtStateNumber;
        private string _ExtensionType;
        private string _ExtensionDescription;
        private string _ICCAppSuccessfulNextState;
        private string _CardNotSmartNextState;
        private string _ICCAppNotUsableNextState;
        private string _NoSuitableICCAppsNextState;
        private string _ICCAppLevelErrorNextState;
        private string _ICCHardwareLevelErrorNextState;
        private string _ProcessNotPerformNextState;
        private string _ExtensionReserved;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("Extension States"), PropertyOrder(2), DescriptionAttribute("Extension State Number")]       
        public string ExitPathsExtStateNumber
        {
            get
            {
                return _ExitPathsExtStateNumber;
            }
            set
            {
                _ExitPathsExtStateNumber = value.PadLeft(3, '0');
            }

        }

        [CategoryAttribute("Extension States"), PropertyOrder(3), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExtensionType
        {
            get { return _ExtensionType; }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(4), DescriptionAttribute("Extension Description")]
        public string ExtensionDescription
        {
            get
            {
                return _ExtensionDescription;
            }
            set
            {
                _ExtensionDescription = value;
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("ICC App. Successful Next State Number")]
        public string ICCAppSuccessfulNextState
        {
            get
            {
                return _ICCAppSuccessfulNextState;
            }
            set
            {
                _ICCAppSuccessfulNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("Card Not Smart Next State Number")]
        public string CardNotSmartNextState
        {
            get
            {
                return _CardNotSmartNextState;
            }
            set
            {
                _CardNotSmartNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(7), ReadOnly(true), DescriptionAttribute("ICC App. Not Usable Next State Number")]
        public string ICCAppNotUsableNextState
        {
            get
            {
                return _ICCAppNotUsableNextState;
            }
            set
            {
                _ICCAppNotUsableNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(8), ReadOnly(true), DescriptionAttribute("No Suitable ICC Apps. Next State Number")]
        public string NoSuitableICCAppsNextState
        {
            get
            {
                return _NoSuitableICCAppsNextState;
            }
            set
            {
                _NoSuitableICCAppsNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(9), ReadOnly(true), DescriptionAttribute("ICC App. Level Error Next State Number")]
        public string ICCAppLevelErrorNextState
        {
            get
            {
                return _ICCAppLevelErrorNextState;
            }
            set
            {
                _ICCAppLevelErrorNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(10), ReadOnly(true), DescriptionAttribute("ICC Hardware Level Error Next State Number")]
        public string ICCHardwareLevelErrorNextState
        {
            get
            {
                return _ICCHardwareLevelErrorNextState;
            }
            set
            {
                _ICCHardwareLevelErrorNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(11), ReadOnly(true), DescriptionAttribute("Processing Not Performed Next State Number")]
        public string ProcessNotPerformNextState
        {
            get
            {
                return _ProcessNotPerformNextState;
            }
            set
            {
                _ProcessNotPerformNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(12), ReadOnly(true), Browsable(false), DescriptionAttribute("Reserved")]
        public string ExtensionReserved
        {
            get
            {
                return _ExtensionReserved;
            }

            set
            {
                _ExtensionReserved = value;
            }
        }


        #endregion

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            StateSlash Selectedstate = new StateSlash();
            StateSlash Dynamicstate = new StateSlash();
            Selectedstate = (StateSlash)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateSlash)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._PleaseWaitScreenNumber = Selectedstate._PleaseWaitScreenNumber;
            Dynamicstate._ICCAppTemplateScreenNumber = Selectedstate._ICCAppTemplateScreenNumber;
            Dynamicstate._ICCAppNameScreenNumber = Selectedstate._ICCAppNameScreenNumber;
            Dynamicstate._Reserved = Selectedstate._Reserved;

            Dynamicstate._ExitPathsExtStateNumber = Selectedstate._ExitPathsExtStateNumber;
            Dynamicstate._ExtensionDescription = Selectedstate._ExtensionDescription;
            Dynamicstate._ICCAppSuccessfulNextState = Selectedstate._ICCAppSuccessfulNextState;
            Dynamicstate._CardNotSmartNextState = Selectedstate._CardNotSmartNextState;
            Dynamicstate._ICCAppNotUsableNextState = Selectedstate._ICCAppNotUsableNextState;
            Dynamicstate._NoSuitableICCAppsNextState = Selectedstate._NoSuitableICCAppsNextState;
            Dynamicstate._ICCAppLevelErrorNextState = Selectedstate._ICCAppLevelErrorNextState;
            Dynamicstate._ICCHardwareLevelErrorNextState = Selectedstate._ICCHardwareLevelErrorNextState;
            Dynamicstate._ProcessNotPerformNextState = Selectedstate._ProcessNotPerformNextState;
            Dynamicstate._ExtensionReserved = Selectedstate._ExtensionReserved;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateSlash Selectedstate = new StateSlash();
            StateSlash Dynamicstate = new StateSlash();
            Selectedstate = (StateSlash)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateSlash)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;          
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._PleaseWaitScreenNumber = Selectedstate._PleaseWaitScreenNumber;
            Dynamicstate._ICCAppTemplateScreenNumber = Selectedstate._ICCAppTemplateScreenNumber;
            Dynamicstate._ICCAppNameScreenNumber = Selectedstate._ICCAppNameScreenNumber;
            Dynamicstate._Reserved = Selectedstate._Reserved;

            Dynamicstate._ExitPathsExtStateNumber = Selectedstate._ExitPathsExtStateNumber;
            Dynamicstate._ExtensionDescription = Selectedstate._ExtensionDescription;
            //Dynamicstate._ICCAppSuccessfulNextState = Selectedstate._ICCAppSuccessfulNextState;
            //Dynamicstate._CardNotSmartNextState = Selectedstate._CardNotSmartNextState;
            //Dynamicstate._ICCAppNotUsableNextState = Selectedstate._ICCAppNotUsableNextState;
            //Dynamicstate._NoSuitableICCAppsNextState = Selectedstate._NoSuitableICCAppsNextState;
            //Dynamicstate._ICCAppLevelErrorNextState = Selectedstate._ICCAppLevelErrorNextState;
            //Dynamicstate._ICCHardwareLevelErrorNextState = Selectedstate._ICCHardwareLevelErrorNextState;
            //Dynamicstate._ProcessNotPerformNextState = Selectedstate._ProcessNotPerformNextState;
            Dynamicstate._ExtensionReserved = Selectedstate._ExtensionReserved;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateSlash State = new StateSlash();
            State = (StateSlash)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;

            //Extension
            string ex1sql = sql;
            if (State.ExitPathsExtStateNumber != "255")
            {
                ex1sql = string.Format(ex1sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExitPathsExtStateNumber,
                    State.ExtensionDescription, State.ExtensionType, ProjectName, TransactionName, State.ICCAppSuccessfulNextState,
                    State.CardNotSmartNextState, State.ICCAppNotUsableNextState, State.NoSuitableICCAppsNextState, State.ICCAppLevelErrorNextState,
                    State.ICCHardwareLevelErrorNextState, State.ProcessNotPerformNextState, State.ExtensionReserved,
                 State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex1sql);
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.PleaseWaitScreenNumber, State.ICCAppTemplateScreenNumber,
                State.ICCAppNameScreenNumber, State.ExitPathsExtStateNumber, State.Reserved, State.Reserved, State.Reserved,
                State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "/";
            StateDescription = "Complete ICC App. Select. & Init. State";
            _PleaseWaitScreenNumber = "000";
            _ICCAppTemplateScreenNumber = "000";
            _ICCAppNameScreenNumber = "000";
            _Reserved = "000";

            _ExitPathsExtStateNumber = "255";
            _ExtensionType = "Z";
            _ExtensionDescription = "Exit Paths Extension State";
            _ICCAppSuccessfulNextState = "255";
            _CardNotSmartNextState = "255";
            _ICCAppNotUsableNextState = "255";
            _NoSuitableICCAppsNextState = "255";
            _ICCAppLevelErrorNextState = "255";
            _ICCHardwareLevelErrorNextState = "255";
            _ProcessNotPerformNextState = "255";
            _ExtensionReserved = "000";
        }
                
       

        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateSlash state = new StateSlash();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._PleaseWaitScreenNumber = processRow[8].ToString();
            state._ICCAppTemplateScreenNumber = processRow[9].ToString();
            state._ICCAppNameScreenNumber = processRow[10].ToString();
            state._ExitPathsExtStateNumber = processRow[11].ToString();
            state._Reserved = processRow[12].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu Screen
            if (state.ExitPathsExtStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExitPathsExtStateNumber);
                state._ExtensionDescription = ExtensionState[4].ToString();
                state._ICCAppSuccessfulNextState = ExtensionState[8].ToString();
                state._CardNotSmartNextState = ExtensionState[9].ToString();
                state._ICCAppNotUsableNextState = ExtensionState[10].ToString();
                state._NoSuitableICCAppsNextState = ExtensionState[11].ToString();
                state._ICCAppLevelErrorNextState = ExtensionState[12].ToString();
                state._ICCHardwareLevelErrorNextState = ExtensionState[13].ToString();
                state._ProcessNotPerformNextState = ExtensionState[14].ToString();
                state._ExtensionReserved = ExtensionState[15].ToString();

                //NextState Kontrolu
                if (state.ICCAppSuccessfulNextState != "255")
                    ChildobjList.Add(GetChildState("ICCAppSuccessfulNextState", state.ICCAppSuccessfulNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                if (state.CardNotSmartNextState != "255")
                    ChildobjList.Add(GetChildState("CardNotSmartNextState", state.CardNotSmartNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                if (state.ICCAppNotUsableNextState != "255")
                    ChildobjList.Add(GetChildState("ICCAppNotUsableNextState", state.ICCAppNotUsableNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                if (state.NoSuitableICCAppsNextState != "255")
                    ChildobjList.Add(GetChildState("NoSuitableICCAppsNextState", state.NoSuitableICCAppsNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                if (state.ICCAppLevelErrorNextState != "255")
                    ChildobjList.Add(GetChildState("ICCAppLevelErrorNextState", state.ICCAppLevelErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                if (state.ICCHardwareLevelErrorNextState != "255")
                    ChildobjList.Add(GetChildState("ICCHardwareLevelErrorNextState", state.ICCHardwareLevelErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                if (state.ProcessNotPerformNextState != "255")
                    ChildobjList.Add(GetChildState("ProcessNotPerformNextState", state.ProcessNotPerformNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            
            
            

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

        private object[] GetExtensionState(ref ArrayList StateList, string ExtensionStateNumber)
        {
            object[] ExtensionState = null;
            foreach (object[] StatedRow in StateList)
            {
                //Extension state sorgusu
                if (ExtensionStateNumber == StatedRow[3].ToString().PadLeft(3, '0'))
                {
                    ExtensionState = StatedRow;
                    //StateList.Remove(StatedRow);
                    return ExtensionState;
                }
            }

            return ExtensionState;
        }
    

        #endregion
                
    }
}