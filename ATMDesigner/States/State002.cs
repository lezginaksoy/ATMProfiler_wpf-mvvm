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
using System.Reflection;
using ATMDesigner.UI.Popups;


namespace ATMDesigner.UI.States
{

    //  e002  Start/Restart EMV Application- EMV Chip Card State
    public class Statee002 : StateBase
    {

        public Statee002(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public Statee002()
        {
            SetDefaultValues();
        }
                
        #region state parameters

        private string _ChipCardOperation;
        public string  _AppStartedNextstate;
        private string _AppNotStartedNextState;
        private string _EMVAppErrorNextState;
        private string _EMVHardwareErrorNextState;
        private string _StartAppScreenNumber;      
        private string _EMVAppScreenNumber;
        private string _EMVAppStoringScreenNumber;
   
     
        [CategoryAttribute("State Parameters"), PropertyOrder(1), ReadOnly(true), DescriptionAttribute("Chip Card Operation to perform")]      
        public string ChipCardOperation
        {
            get
            {
                return _ChipCardOperation;
            }

            set
            {
                _ChipCardOperation = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Next state if application is (re)started.")]
        public string AppStartedNextstate
        {
            get
            {
                return _AppStartedNextstate;
            }

            set
            {
                _AppStartedNextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Next state if the app. could not be (re)started")]
        public string AppNotStartedNextState
        {
            get
            {
                return _AppNotStartedNextState;
            }
            set
            {
                _AppNotStartedNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Next state number if an EMV App. level error occurred")]
        public string EMVAppErrorNextState
        {
            get
            {
                return _EMVAppErrorNextState;
            }

            set
            {
                _EMVAppErrorNextState = value.PadLeft(3, '0');
            }
        }
        
        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("Next state number if an EMV hardware level error occurred")]
        public string EMVHardwareErrorNextState
        {
            get
            {
                return _EMVHardwareErrorNextState;
            }

            set
            {
                _EMVHardwareErrorNextState = value.PadLeft(3, '0');
            }
        }
        
        [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Starting app. Screen number.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string StartAppScreenNumber
        {
            get
            {
                return _StartAppScreenNumber;
            }

            set
            {
                _StartAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(7),  DescriptionAttribute("Screen template number for the EMV app. label")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string EMVAppScreenNumber
        {
            get
            {
                return _EMVAppScreenNumber;
            }

            set
            {
                _EMVAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), DescriptionAttribute("Screen number for storing the EMV application label")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string EMVAppStoringScreenNumber
        {
            get
            {
                return _EMVAppStoringScreenNumber;
            }

            set
            {
                _EMVAppStoringScreenNumber = value.PadLeft(3, '0');
            }
        }

        #endregion
        
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            Statee002 Selectedstate = new Statee002();
            Statee002 Dynamicstate = new Statee002();
            Selectedstate = (Statee002)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee002)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            Dynamicstate._AppStartedNextstate = Selectedstate.AppStartedNextstate;
            Dynamicstate._AppNotStartedNextState = Selectedstate.AppNotStartedNextState;
            Dynamicstate._EMVAppErrorNextState = Selectedstate.EMVAppErrorNextState;
            Dynamicstate._EMVHardwareErrorNextState = Selectedstate.EMVHardwareErrorNextState;
            Dynamicstate._StartAppScreenNumber = Selectedstate.StartAppScreenNumber;
            Dynamicstate._EMVAppScreenNumber = Selectedstate.EMVAppScreenNumber;
            Dynamicstate._EMVAppStoringScreenNumber = Selectedstate.EMVAppStoringScreenNumber;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Statee002 Selectedstate = new Statee002();
            Statee002 Dynamicstate = new Statee002();
            Selectedstate = (Statee002)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee002)ClassInstance;

            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._Description=Selectedstate.StateDescription;
            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            //Dynamicstate._AppStartedNextstate = Selectedstate._AppStartedNextstate;
            //Dynamicstate._AppNotStartedNextState = Selectedstate._AppNotStartedNextState;
            //Dynamicstate._EMVAppErrorNextState = Selectedstate._EMVAppErrorNextState;
            //Dynamicstate._EMVHardwareErrorNextState = Selectedstate._EMVHardwareErrorNextState;
            Dynamicstate._StartAppScreenNumber = Selectedstate.StartAppScreenNumber;
            Dynamicstate._EMVAppScreenNumber = Selectedstate.EMVAppScreenNumber;
            Dynamicstate._EMVAppStoringScreenNumber = Selectedstate.EMVAppStoringScreenNumber;

            return Dynamicstate;
        }
        
        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Statee002 State = new Statee002();
            State = (Statee002)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;
            
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ChipCardOperation, State.AppStartedNextstate, State.AppNotStartedNextState,
                State.EMVAppErrorNextState, State.EMVHardwareErrorNextState, State.StartAppScreenNumber,
                State.EMVAppScreenNumber,State.EMVAppStoringScreenNumber , State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "e";
            StateDescription = "Start/Restart EMV Application";
            _ChipCardOperation = "002";
            _AppStartedNextstate = "255";
            _AppNotStartedNextState = "255";
            _EMVAppErrorNextState = "255";
            _EMVHardwareErrorNextState = "255";
            _StartAppScreenNumber = "000";
            _EMVAppScreenNumber = "000";
            _EMVAppStoringScreenNumber = "000";
        }



        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Statee002 state = new Statee002();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ChipCardOperation = processRow[8].ToString();
            state._AppStartedNextstate = processRow[9].ToString();
            state._AppNotStartedNextState = processRow[10].ToString();
            state._EMVAppErrorNextState = processRow[11].ToString();
            state._EMVHardwareErrorNextState = processRow[12].ToString();
            state._StartAppScreenNumber = processRow[13].ToString();
            state._EMVAppScreenNumber = processRow[14].ToString();
            state._EMVAppStoringScreenNumber = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            ////NextState Kontrolu
            if (state.AppStartedNextstate != "255")
                ChildobjList.Add(GetChildState("AppStartedNextstate", state.AppStartedNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.AppNotStartedNextState != "255")
                ChildobjList.Add(GetChildState("AppNotStartedNextState", state.AppNotStartedNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.EMVAppErrorNextState != "255")
                ChildobjList.Add(GetChildState("EMVAppErrorNextState", state._EMVAppErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.EMVHardwareErrorNextState != "255")
                ChildobjList.Add(GetChildState("EMVHardwareErrorNextState", state.EMVHardwareErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            

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