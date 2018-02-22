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

    //, Complete ICC Initialisation State 
    public class StateComma : StateBase
    {

        public StateComma(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateComma()
        {
            SetDefaultValues();
        }    


        #region Converters

       
        #endregion

        #region state parameters

        private string _PleaseWaitScreenNumber;
        public string _ICCInitSuccessfulNextState;
        private string _CardNotSmartNextState;
        private string _NoUsableAppNextState;
        private string _ICCAppLevelErrorNextState;
        private string _ICCHardLevelErrorNextState;
        private string _Reserved;

        [Category("State Parameters"), PropertyOrder(1), DescriptionAttribute("Please Wait Screen Number")]
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

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("ICC Init Successful Next State")]
        public string ICCInitSuccessfulNextState
        {
            get
            {
                return _ICCInitSuccessfulNextState;
            }

            set
            {
                _ICCInitSuccessfulNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3),ReadOnly(true), DescriptionAttribute("Card Not Smart Next State")]
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

        [Category("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("No Usable App Next State")]
        public string NoUsableAppNextState
        {
            get
            {
                return _NoUsableAppNextState;
            }
            set
            {
                _NoUsableAppNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("ICC App Level Error Next State")]
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

        [Category("State Parameters"), PropertyOrder(6),ReadOnly(true), DescriptionAttribute("ICC Hardware Level Error Next State")]
        public string ICCHardLevelErrorNextState
        {
            get
            {
                return _ICCHardLevelErrorNextState;
            }
            set
            {
                _ICCHardLevelErrorNextState = value.PadLeft(3, '0');
            }
        }
     
        [CategoryAttribute("State Parameters"), PropertyOrder(7), ReadOnly(true),DescriptionAttribute("Reserved")]
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
            StateComma Selectedstate = new StateComma();
            StateComma Dynamicstate = new StateComma();
            Selectedstate = (StateComma)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateComma)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._PleaseWaitScreenNumber = Selectedstate.PleaseWaitScreenNumber;
            Dynamicstate._ICCInitSuccessfulNextState = Selectedstate.ICCInitSuccessfulNextState;
            Dynamicstate._CardNotSmartNextState = Selectedstate.CardNotSmartNextState;
            Dynamicstate._NoUsableAppNextState = Selectedstate.NoUsableAppNextState;
            Dynamicstate._ICCAppLevelErrorNextState = Selectedstate.ICCAppLevelErrorNextState;
            Dynamicstate._ICCHardLevelErrorNextState = Selectedstate.ICCHardLevelErrorNextState;
            Dynamicstate._Reserved = Selectedstate.Reserved;      

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateComma Selectedstate = new StateComma();
            StateComma Dynamicstate = new StateComma();
            Selectedstate = (StateComma)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateComma)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._PleaseWaitScreenNumber = Selectedstate.PleaseWaitScreenNumber;
            //Dynamicstate._ICCInitSuccessfulNextState = Selectedstate._ICCInitSuccessfulNextState;
            //Dynamicstate._CardNotSmartNextState = Selectedstate._CardNotSmartNextState;
            //Dynamicstate._NoUsableAppNextState = Selectedstate._NoUsableAppNextState;
            //Dynamicstate._ICCAppLevelErrorNextState = Selectedstate._ICCAppLevelErrorNextState;
            //Dynamicstate._ICCHardLevelErrorNextState = Selectedstate._ICCHardLevelErrorNextState;
            Dynamicstate._Reserved = Selectedstate.Reserved;    

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateComma State = new StateComma();
            State = (StateComma)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.PleaseWaitScreenNumber, State.ICCInitSuccessfulNextState, 
                State.CardNotSmartNextState,State.NoUsableAppNextState,State.ICCAppLevelErrorNextState, State.ICCHardLevelErrorNextState,
                State.Reserved,State.Reserved,State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = ",";
            StateDescription = "Complete ICC Initialisation State";
            _PleaseWaitScreenNumber = "000";
            _ICCInitSuccessfulNextState = "255";
            _CardNotSmartNextState = "255";
            _NoUsableAppNextState = "255";
            _ICCAppLevelErrorNextState = "255";
            _ICCHardLevelErrorNextState = "255";         
            _Reserved = "000";

        }
 

        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateComma state = new StateComma();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._PleaseWaitScreenNumber = processRow[8].ToString();
            state._ICCInitSuccessfulNextState = processRow[9].ToString();
            state._CardNotSmartNextState = processRow[10].ToString();
            state._NoUsableAppNextState = processRow[11].ToString();
            state._ICCAppLevelErrorNextState = processRow[12].ToString();
            state._ICCHardLevelErrorNextState = processRow[13].ToString();
            state._Reserved = processRow[14].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            if (state.ICCInitSuccessfulNextState != "255")
                ChildobjList.Add(GetChildState("ICCInitSuccessfulNextState", state.ICCInitSuccessfulNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.CardNotSmartNextState != "255")
                ChildobjList.Add(GetChildState("CardNotSmartNextState", state.CardNotSmartNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.NoUsableAppNextState != "255")
                ChildobjList.Add(GetChildState("NoUsableAppNextState", state.NoUsableAppNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.ICCAppLevelErrorNextState != "255")
                ChildobjList.Add(GetChildState("ICCAppLevelErrorNextState", state.ICCAppLevelErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.ICCHardLevelErrorNextState != "255")
                ChildobjList.Add(GetChildState("ICCHardLevelErrorNextState", state.ICCHardLevelErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
         
            
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