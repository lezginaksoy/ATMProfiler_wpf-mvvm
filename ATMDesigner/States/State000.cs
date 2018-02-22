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

    //  e000  Contact Chip and Candidate List creation- EMV Chip Card State
    public class Statee000 : StateBase
    {

        public Statee000(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public Statee000()
        {
            SetDefaultValues();
        }

        #region Converters

        public class FlagServiceCodecheckingConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(255, "None");
                strings.Add(000, "Always start EMV processing.");
                strings.Add(001,"Start EMV processing only if the magnetic track 2 data has been read and contains a service code indicating that this card has a chip");
               
                return strings;
            }
        }

        public class FlagConfirmCandidatesCheckingConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(255, "None");
                strings.Add(000, "candidates which have to be confirmed.should NOT be removed ");
                strings.Add(001, "candidates which have to be confirmed.should be removed ");
                return strings;
            }
        }
                   
        #endregion

        #region state parameters

        private string _ChipCardOperation;
        public string  _SuccessfulNextstate;
        private string _FailedContactingNextState;
        private string _NoAppAvaliableNextState;
        private string _EMVAppLevelErrorNextState;
        private string _EMVHardwareErrorNextState;      
        private string _Reserved;

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

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Next state if processing was successful")]
        public string SuccessfulNextstate
        {
            get
            {
                return _SuccessfulNextstate;
            }

            set
            {
                _SuccessfulNextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Next state number if the chip was not contacted or failed contacting")]
        public string FailedContactingNextState
        {
            get
            {
                return _FailedContactingNextState;
            }
            set
            {
                _FailedContactingNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Next state if no app. candidate is available")]
        public string NoAppAvaliableNextState
        {
            get
            {
                return _NoAppAvaliableNextState;
            }

            set
            {
                _NoAppAvaliableNextState = value.PadLeft(3, '0');
            }
        }
        
        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("Next state if an EMV app. level error occurred")]
        public string EMVAppLevelErrorNextState
        {
            get
            {
                return _EMVAppLevelErrorNextState;
            }

            set
            {
                _EMVAppLevelErrorNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("Next state if an EMV hardware level error occurred")]
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

        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true),DescriptionAttribute("Reserved")]
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

        private string _ExtensionStateNumber;
        private string _ExtensionType;
        private string _ExtensionDescription;
        private string _PleaseWaitScreenNumber;
        private string _ProcessNotStartNextstate;
        private string _FlagServiceCodechecking;
        private string _FlagConfirmCandidatesChecking;
        private string _ExtensionReserved;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("Extension States"), PropertyOrder(1), DescriptionAttribute("Extension Number")]
        public string ExtensionStateNumber
        {
            get
            {
                return _ExtensionStateNumber;
            }
            set
            {
                _ExtensionStateNumber = value.PadLeft(3, '0');
            }

        }

        [CategoryAttribute("Extension States"), PropertyOrder(2), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExtensionType
        {
            get { return _ExtensionType; }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(3), DescriptionAttribute("Extension Description")]
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

        [CategoryAttribute("Extension States"), PropertyOrder(4),DescriptionAttribute("Please Wait Screen Number")]
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

        [CategoryAttribute("Extension States"), PropertyOrder(5), DescriptionAttribute("Process Not Started Next State Number")]
        public string ProcessNotStartNextstate
        {
            get
            {
                return _ProcessNotStartNextstate;
            }
            set
            {
                _ProcessNotStartNextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(6),DescriptionAttribute("Flag for Service Code checking")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(FlagServiceCodecheckingConverter))]      
        public string FlagServiceCodechecking
        {
            get
            {
                return _FlagServiceCodechecking;
            }
            set
            {
                _FlagServiceCodechecking = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(7), DescriptionAttribute("Flag for confirm candidates checking")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(FlagConfirmCandidatesCheckingConverter))]      
        public string FlagConfirmCandidatesChecking
        {
            get
            {
                return _FlagConfirmCandidatesChecking;
            }
            set
            {
                _FlagConfirmCandidatesChecking = value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true), DescriptionAttribute("Reserved")]
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
            Statee000 Selectedstate = new Statee000();
            Statee000 Dynamicstate = new Statee000();
            Selectedstate = (Statee000)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee000)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;          
            Dynamicstate._SuccessfulNextstate = Selectedstate.SuccessfulNextstate;
            Dynamicstate._FailedContactingNextState = Selectedstate.FailedContactingNextState;
            Dynamicstate._NoAppAvaliableNextState = Selectedstate.NoAppAvaliableNextState;
            Dynamicstate._EMVAppLevelErrorNextState = Selectedstate.EMVAppLevelErrorNextState;
            Dynamicstate._EMVHardwareErrorNextState = Selectedstate.EMVHardwareErrorNextState;
            Dynamicstate._Reserved = Selectedstate.Reserved;

            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate.ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._PleaseWaitScreenNumber = Selectedstate.PleaseWaitScreenNumber;
            Dynamicstate._ProcessNotStartNextstate = Selectedstate.ProcessNotStartNextstate;
            Dynamicstate._FlagServiceCodechecking = Selectedstate.FlagServiceCodechecking;
            Dynamicstate._FlagConfirmCandidatesChecking = Selectedstate.FlagConfirmCandidatesChecking;
            Dynamicstate._ExtensionReserved = Selectedstate.ExtensionReserved;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Statee000 Selectedstate = new Statee000();
            Statee000 Dynamicstate = new Statee000();
            Selectedstate = (Statee000)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee000)ClassInstance;

            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            //Dynamicstate._SuccessfulNextstate = Selectedstate._SuccessfulNextstate;
            //Dynamicstate._FailedContactingNextState = Selectedstate._FailedContactingNextState;
            //Dynamicstate._NoAppAvaliableNextState = Selectedstate._NoAppAvaliableNextState;
            //Dynamicstate._EMVAppLevelErrorNextState = Selectedstate._EMVAppLevelErrorNextState;
            //Dynamicstate._EMVHardwareErrorNextState = Selectedstate._EMVHardwareErrorNextState;
            Dynamicstate._Reserved = Selectedstate.Reserved;

            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate.ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._PleaseWaitScreenNumber = Selectedstate.PleaseWaitScreenNumber;
            Dynamicstate._ProcessNotStartNextstate = Selectedstate.ProcessNotStartNextstate;
            Dynamicstate._FlagServiceCodechecking = Selectedstate.FlagServiceCodechecking;
            Dynamicstate._FlagConfirmCandidatesChecking = Selectedstate.FlagConfirmCandidatesChecking;
            Dynamicstate._ExtensionReserved = Selectedstate.ExtensionReserved;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Statee000 State = new Statee000();
            State = (Statee000)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;

            //Screen Extension
            string ex1sql = sql;
            if (State.ExtensionStateNumber != "255")
            {
                ex1sql = string.Format(ex1sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber,
                    State.ExtensionDescription, State.ExtensionType, ProjectName, TransactionName, State.PleaseWaitScreenNumber,
                    State.ProcessNotStartNextstate, State.FlagServiceCodechecking, State.FlagConfirmCandidatesChecking, State.ExtensionReserved,
                    State.ExtensionReserved, State.ExtensionReserved, State.ExtensionReserved,
                 State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex1sql);
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ChipCardOperation, State.SuccessfulNextstate, State.FailedContactingNextState,
                State.NoAppAvaliableNextState, State.EMVAppLevelErrorNextState, State.EMVHardwareErrorNextState, State.ExtensionStateNumber,
                State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "e";        
            StateDescription = "Contact Chip and Candidate List creation";
            _ChipCardOperation = "000";
            _SuccessfulNextstate = "255";
            _FailedContactingNextState = "255";
            _NoAppAvaliableNextState="255";
            _EMVAppLevelErrorNextState = "255";
            _EMVHardwareErrorNextState = "255";
            _Reserved = "000";

            _ExtensionStateNumber = "255";
            _ExtensionType = "Z";
            _ExtensionDescription = "Exit Paths Extension State";
            _PleaseWaitScreenNumber = "000";
            _ProcessNotStartNextstate = "255";
            _FlagServiceCodechecking = "255";
            _FlagConfirmCandidatesChecking = "255";           
            _ExtensionReserved = "000";
        }



        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Statee000 state = new Statee000();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ChipCardOperation = processRow[8].ToString();
            state._SuccessfulNextstate = processRow[9].ToString();
            state._FailedContactingNextState = processRow[10].ToString();
            state._NoAppAvaliableNextState = processRow[11].ToString();
            state._EMVAppLevelErrorNextState = processRow[12].ToString();
            state._EMVHardwareErrorNextState = processRow[13].ToString();
            state._ExtensionStateNumber = processRow[14].ToString();

            state._Reserved = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu Screen
            if (state.ExtensionStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber);
                state._ExtensionDescription = processRow[4].ToString();
                state._PleaseWaitScreenNumber = ExtensionState[8].ToString();
                state._ProcessNotStartNextstate = ExtensionState[9].ToString();
                state._FlagServiceCodechecking = ExtensionState[10].ToString();
                state._FlagConfirmCandidatesChecking = ExtensionState[11].ToString();
                state._ExtensionReserved = ExtensionState[12].ToString();
            }

            ////NextState Kontrolu
            if (state.SuccessfulNextstate != "255")
                ChildobjList.Add(GetChildState("SuccessfulNextstate", state.SuccessfulNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.FailedContactingNextState != "255")
                ChildobjList.Add(GetChildState("FailedContactingNextState", state.FailedContactingNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.NoAppAvaliableNextState != "255")
                ChildobjList.Add(GetChildState("NoAppAvaliableNextState", state.NoAppAvaliableNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.EMVAppLevelErrorNextState != "255")
                ChildobjList.Add(GetChildState("EMVAppLevelErrorNextState", state.EMVAppLevelErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
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