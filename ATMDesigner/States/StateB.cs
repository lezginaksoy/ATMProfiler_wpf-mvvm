using System;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Collections.Generic;
using ATMDesigner.Common;
using System.Collections;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
  
    public class StateB : StateBase
    {
        public StateB(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public StateB()
        {
            SetDefaultData();
        }

        #region Converter

        public class MaxRetriesConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"None");
                strings.Add("001");
                strings.Add("002");
                strings.Add("003");
                strings.Add("004");
                strings.Add("005");
                strings.Add("006");
                strings.Add("007");
                strings.Add("008");
                strings.Add("009");              
                return strings;
            }
        }

        #endregion
        
        #region State Parameters
        private string _screen;
        public string _TimeoutNextState;
        public string _CancelNextState;
        public string _LocalPINCheckGoodPINNextState;
        public string _LocalPINCheckMaximumBadPINsNextState;
        private string _localPINErrorScreen;
        public string _RemotePINCheckNextState;
        private string _localMaxPINRetries;
        
        [CategoryAttribute("State Parameters"), PropertyOrder(1), DescriptionAttribute("State Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ScreenNumber
        {
            get
            {
                return _screen;
            }

            set
            {
                _screen = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), ReadOnly(true), PropertyOrder(2), DescriptionAttribute("Timeout Next State")]
        public string TimeoutNextState
        {
            get
            {
                return _TimeoutNextState;
            }

            set
            {
                _TimeoutNextState = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), ReadOnly(true), PropertyOrder(3), DescriptionAttribute("Cancel Next State")]
        public string CancelNextState
        {
            get
            {
                return _CancelNextState;
            }

            set
            {
                _CancelNextState = value.PadLeft(3, '0');                
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Local PIN Check Good PIN Next State")]
        public string LocalPINCheckGoodPINNextState
        {
            get
            {
                return _LocalPINCheckGoodPINNextState;
            }

            set
            {
                _LocalPINCheckGoodPINNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("Local PIN Check Maximum Bad PINs Next State")]
        public string LocalPINCheckMaximumBadPINsNextState
        {
            get
            {
                return _LocalPINCheckMaximumBadPINsNextState;
            }

            set
            {
                _LocalPINCheckMaximumBadPINsNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Local PIN Check Error Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string LocalPINCheckErrorScreen
        {
            get
            {
                return _localPINErrorScreen;
            }

            set
            {
                _localPINErrorScreen = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(7), ReadOnly(true), DescriptionAttribute("Remote PIN Check Next State")]
        public string RemotePINCheckNextState
        {
            get
            {
                return _RemotePINCheckNextState;
            }

            set
            {
                _RemotePINCheckNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), DescriptionAttribute("Local PIN Check Maximum PIN Retries")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(MaxRetriesConverter))]
        public string LocalPINCheckMaximumPINRetries
        {
            get
            {
                return _localMaxPINRetries;
            }

            set
            {
                _localMaxPINRetries = value.PadLeft(3, '0');
            }
        }

        #endregion

        #region Events and Methods
  
        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
           //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateB Selectedstate = new StateB();
            StateB Dynamicstate = new StateB();

            Selectedstate = (StateB)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateB)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._CancelNextState = Selectedstate.CancelNextState;
            Dynamicstate._LocalPINCheckGoodPINNextState = Selectedstate.LocalPINCheckGoodPINNextState;
            Dynamicstate._LocalPINCheckMaximumBadPINsNextState = Selectedstate.LocalPINCheckMaximumBadPINsNextState;
            Dynamicstate._localMaxPINRetries = Selectedstate.LocalPINCheckMaximumPINRetries;
            Dynamicstate._localPINErrorScreen = Selectedstate.LocalPINCheckErrorScreen;
            Dynamicstate._RemotePINCheckNextState = Selectedstate.RemotePINCheckNextState;
            Dynamicstate._screen = Selectedstate.ScreenNumber;
            Dynamicstate._TimeoutNextState = Selectedstate.TimeoutNextState;
            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateB Selectedstate = new StateB();
            StateB Dynamicstate = new StateB();

            Selectedstate = (StateB)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateB)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            //Dynamicstate._cancelnextState = Selectedstate._cancelnextState;
            //Dynamicstate._localGoodPINState = Selectedstate._localGoodPINState;
            //Dynamicstate._localMaxBadPINState = Selectedstate._localMaxBadPINState;
            Dynamicstate._localMaxPINRetries = Selectedstate.LocalPINCheckMaximumPINRetries;
            Dynamicstate._localPINErrorScreen = Selectedstate.LocalPINCheckErrorScreen;
            //Dynamicstate._remotePINState = Selectedstate._remotePINState;
            Dynamicstate._screen = Selectedstate.ScreenNumber;
            //Dynamicstate._timeoutnextState = Selectedstate._timeoutnextState;
            return Dynamicstate;
        }
 
        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateB State = new StateB();
            State = (StateB)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ScreenNumber, State.TimeoutNextState, State.CancelNextState,
                State.LocalPINCheckGoodPINNextState, State.LocalPINCheckMaximumBadPINsNextState, State.LocalPINCheckErrorScreen,
                State.RemotePINCheckNextState, State.LocalPINCheckMaximumPINRetries, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);
            
            return SqlStringList;

        }
    
        private void SetDefaultData()
        {
       
            StateType = "B";
            StateDescription = "PIN Entry State";
            
            _screen = "000";
            _TimeoutNextState = "255";
            _CancelNextState = "255";
            _LocalPINCheckGoodPINNextState = "255";
            _LocalPINCheckMaximumBadPINsNextState = "255";
            _RemotePINCheckNextState = "255";
            _localPINErrorScreen = "000";
            _TimeoutNextState = "255";
            _localMaxPINRetries = "000";
        }



        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateB state = new StateB();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._screen = processRow[8].ToString();
            state._TimeoutNextState = processRow[9].ToString();
            state._CancelNextState = processRow[10].ToString();
            state._LocalPINCheckGoodPINNextState = processRow[11].ToString();
            state._LocalPINCheckMaximumBadPINsNextState = processRow[12].ToString();
            state._localPINErrorScreen = processRow[13].ToString();
            state._RemotePINCheckNextState = processRow[14].ToString();
            state._localMaxPINRetries = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            #region nextState arrange-parent and pointer

            if (state.TimeoutNextState != "255")
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.CancelNextState != "255")
                ChildobjList.Add(GetChildState("CancelNextState", state.CancelNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.LocalPINCheckGoodPINNextState != "255")
                ChildobjList.Add(GetChildState("LocalPINCheckGoodPINNextState", state.LocalPINCheckGoodPINNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.LocalPINCheckMaximumBadPINsNextState != "255")
                ChildobjList.Add(GetChildState("LocalPINCheckMaximumBadPINsNextState", state.LocalPINCheckMaximumBadPINsNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.RemotePINCheckNextState != "255")
                ChildobjList.Add(GetChildState("RemotePINCheckNextState", state.RemotePINCheckNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
           
            #endregion
            
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