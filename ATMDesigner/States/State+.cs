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
    
    //+ Begin ICC Initialisation State
    public class StatePlus : StateBase
    {

        public StatePlus(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StatePlus()
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

        private string _ICCInitStartedNextState;
        public string _ICCInitNotStartedNextState;
        private string _ICIInitRequirment;
        private string _AutomaticICCAppFlag;
        private string _ICCAppValidationFlag;
        private string _CardholderConfirmationFlag;
        private string _ScreenToClearScreenNumber;
        private string _Reserved;

        [CategoryAttribute("State Parameters"), PropertyOrder(1), ReadOnly(true), DescriptionAttribute("ICC Init Started NextState Number")]
        public string ICCInitStartedNextState
        {
            get
            {
                return _ICCInitStartedNextState;
            }

            set
            {
                _ICCInitStartedNextState = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("ICC Init Not Started Next State")]
        public string ICCInitNotStartedNextState
        {
            get
            {
                return _ICCInitNotStartedNextState;
            }

            set
            {
                _ICCInitNotStartedNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("ICI Init Requirment")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ICIInitRequirmentConverter))]
        public string ICIInitRequirment
        {
            get
            {
                return _ICIInitRequirment;
            }

            set
            {
                _ICIInitRequirment = value.PadLeft(3, '0');
            }
        }
        
        [Category("State Parameters"), PropertyOrder(4), DescriptionAttribute("Automatic ICC App Flag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(AutomaticICCAppFlagConverter))]
        public string AutomaticICCAppFlag
        {
            get
            {
                return _AutomaticICCAppFlag;
            }
            set 
            {
                _AutomaticICCAppFlag = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(5), DescriptionAttribute("ICC App Validation Flag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ICCAppValidationFlagConverter))]
        public string ICCAppValidationFlag
        {
            get
            {
                return _ICCAppValidationFlag;
            }
            set
            {
                _ICCAppValidationFlag = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(6), DescriptionAttribute("Card holder Confirmation Flag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(CardholderConfirmationFlagConverter))]
        public string CardholderConfirmationFlag
        {
            get
            {
                return _CardholderConfirmationFlag;
            }
            set
            {
                _CardholderConfirmationFlag = value.PadLeft(3, '0'); 
            }
        }

        [Category("State Parameters"), PropertyOrder(7), DescriptionAttribute("Screen To Clear Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ScreenToClearScreenNumber
        {
            get
            {
                return _ScreenToClearScreenNumber;
            }
            set
            {
                _ScreenToClearScreenNumber = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true),DescriptionAttribute("Reserved")]
        public string Reserved
        {
            get
            {
                return _Reserved;
            }

            set
            {
                _Reserved = value.PadLeft(3, '0');
            }
        }

        #endregion

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance,PropertyGrid SelectedPgrid)       
        {
            //FillStateFromPropertyGrid
            StatePlus Selectedstate = new StatePlus();
            StatePlus Dynamicstate = new StatePlus();
            Selectedstate = (StatePlus)SelectedPgrid.SelectedObject;
            Dynamicstate = (StatePlus)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ICCInitStartedNextState = Selectedstate.ICCInitStartedNextState;
            Dynamicstate._ICCInitNotStartedNextState = Selectedstate.ICCInitNotStartedNextState;
            Dynamicstate._ICIInitRequirment = Selectedstate.ICIInitRequirment;
            Dynamicstate._AutomaticICCAppFlag = Selectedstate.AutomaticICCAppFlag;
            Dynamicstate._ICCAppValidationFlag = Selectedstate.ICCAppValidationFlag;
            Dynamicstate._CardholderConfirmationFlag = Selectedstate.CardholderConfirmationFlag;
            Dynamicstate._ScreenToClearScreenNumber = Selectedstate.ScreenToClearScreenNumber;
            Dynamicstate._Reserved = Selectedstate.Reserved;      

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StatePlus Selectedstate = new StatePlus();
            StatePlus Dynamicstate = new StatePlus();
            Selectedstate = (StatePlus)SelectedPgrid.SelectedObject;
            Dynamicstate = (StatePlus)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;           
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            //Dynamicstate._ICCInitStartedNextState = Selectedstate._ICCInitStartedNextState;
            //Dynamicstate._ICCInitNotStartedNextState = Selectedstate._ICCInitNotStartedNextState;
            Dynamicstate._ICIInitRequirment = Selectedstate.ICIInitRequirment;
            Dynamicstate._AutomaticICCAppFlag = Selectedstate.AutomaticICCAppFlag;
            Dynamicstate._ICCAppValidationFlag = Selectedstate.ICCAppValidationFlag;
            Dynamicstate._CardholderConfirmationFlag = Selectedstate.CardholderConfirmationFlag;
            Dynamicstate._ScreenToClearScreenNumber = Selectedstate.ScreenToClearScreenNumber;
            Dynamicstate._Reserved = Selectedstate.Reserved;     

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StatePlus State = new StatePlus();
            State = (StatePlus)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ICCInitStartedNextState, State.ICCInitNotStartedNextState, State.ICIInitRequirment,
                State.AutomaticICCAppFlag,State.ICCAppValidationFlag, State.CardholderConfirmationFlag, State.ScreenToClearScreenNumber,
                State.Reserved,State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "+";
            StateDescription = "Begin ICC Initialisation State";          
            _ICCInitStartedNextState = "255";
            _ICCInitNotStartedNextState = "255";
            _ICIInitRequirment = "000";
            _AutomaticICCAppFlag = "000";
            _ICCAppValidationFlag = "000";
            _CardholderConfirmationFlag = "000";
            _ScreenToClearScreenNumber = "000";
            _Reserved = "000";

        }
 
       

        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StatePlus state = new StatePlus();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ICCInitStartedNextState = processRow[8].ToString();
            state._ICCInitNotStartedNextState = processRow[9].ToString();
            state._ICIInitRequirment = processRow[10].ToString();
            state._AutomaticICCAppFlag = processRow[11].ToString();
            state._ICCAppValidationFlag = processRow[12].ToString();
            state._CardholderConfirmationFlag = processRow[13].ToString();
            state._ScreenToClearScreenNumber = processRow[14].ToString();
            state._Reserved = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            if (state.ICCInitStartedNextState != "255")
                ChildobjList.Add(GetChildState("ICCInitStartedNextState", state.ICCInitStartedNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.ICCInitNotStartedNextState != "255")
                ChildobjList.Add(GetChildState("ICCInitNotStartedNextState", state.ICCInitNotStartedNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

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
}