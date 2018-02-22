using System;
using System.ComponentModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Reflection;
using ATMDesigner.Common;
using System.Collections;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 

    public class Stater : StateBase
    {
        //Wincor States
        public Stater(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            setDefaultData();
        }

        public Stater()
        {
            setDefaultData();
        }

        #region Converter

        public class ScriptId_NoteIdConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                for (int i = 0; i < 1000; i++)
                {
                    strings.Add(i.ToString().PadLeft(3, '0'));
                }
                return strings;
            }
        }

        #endregion

        #region State Parameters

        private string _OKNextState;
        private string _CancelAndESCROWEmptyNextState;
        private string _ErrorNextState;
        private string _TimeoutNextState;
        private string _CashCapturedNextState;
        private string _ScriptId;
        private string _NoteId;

        [CategoryAttribute("State Parameters"), PropertyOrder(1), ReadOnly(true), DescriptionAttribute("OK next state number")]
        public string OKNextState
        {
            get
            {
                return _OKNextState;
            }

            set
            {
                _OKNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("“Cancel” and “ESCROW empty” next state number")]
        public string CancelAndESCROWEmptyNextState
        {
            get
            {
                return _CancelAndESCROWEmptyNextState;
            }

            set
            {
                _CancelAndESCROWEmptyNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Error Next State")]
        public string ErrorNextState
        {
            get
            {
                return _ErrorNextState;
            }
            set
            {
                _ErrorNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Timeout Next State")]
        public string TimeoutNextState
        {
            get
            {
                return _TimeoutNextState;
            }
            set
            {
                _TimeoutNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("Cash Captured Next State")]
        public string CashCapturedNextState
        {
            get
            {
                return _CashCapturedNextState;
            }
            set
            {
                _CashCapturedNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(7), DescriptionAttribute("Script Id")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ScriptId_NoteIdConverter))]
        public string ScriptId
        {
            get
            {
                return _ScriptId;
            }
            set
            {
                _ScriptId = value;
            }
        }
        
        [Category("State Parameters"), PropertyOrder(8), DescriptionAttribute("Note Id")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ScriptId_NoteIdConverter))]
        public string NoteId
        {
            get
            {
                return _NoteId;
            }
            set
            {
                _NoteId = value;
            }
        }
      
        #endregion

        #region Extension State 

        private string _ExtensionStateNumber;
        private string _ExtensionType;
        private string _ExtensionDescription;
        private string _RollbackScreen;
        private string _RollbackScreenL2_L3;
        private string _TakeMoneyScreen;
        private string _TakemoneyScreenL2_L3;
        private string _AllMoneyCapturedScreen;
        private string _Reserved;
    
        #region converter

        #endregion

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(8), DescriptionAttribute("Extension State Number")]
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
    
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(9), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExtensionType
        {
            get { return "Z"; }
        }
       
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(10), DescriptionAttribute("Extension Description")]
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
        
        [Category("State Extension Parameters"), PropertyOrder(11), DescriptionAttribute("Rollback Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string RollbackScreen
        {
            get
            { 
                return _RollbackScreen;
            }
            set
            {
                _RollbackScreen = value;
            }
        }
      
        [Category("State Extension Parameters"), PropertyOrder(12), DescriptionAttribute("“Rollback” screen, if L2/L3 will be captured")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string RollbackScreenL2_L3
        {
            get { return _RollbackScreenL2_L3; }
            set { _RollbackScreenL2_L3 = value; }
        }

        [Category("State Extension Parameters"), PropertyOrder(13), DescriptionAttribute("TakeMoney Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string TakeMoneyScreen
        {
            get { return _TakeMoneyScreen; }
            set { _TakeMoneyScreen = value; }
        }

        [Category("State Extension Parameters"), PropertyOrder(14), DescriptionAttribute("“Take money” screen, if L2/L3 notes have been captured (some notes returned)")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string TakemoneyScreenL2_L3
        {
            get { return _TakemoneyScreenL2_L3; }
            set { _TakemoneyScreenL2_L3 = value; }
        }

        [Category("State Extension Parameters"), PropertyOrder(15), DescriptionAttribute("All Money Captured Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string AllMoneyCapturedScreen
        {
            get { return _AllMoneyCapturedScreen; }
            set { _AllMoneyCapturedScreen = value; }
        }

        [Category("State Extension Parameters"), PropertyOrder(16), DescriptionAttribute("Reserved")]
        public string Reserved
        {
            get
            {
                return _Reserved;
            }
        }

        #endregion

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Stater Selectedstate = new Stater();
            Selectedstate = (Stater)SelectedPgrid.SelectedObject;
            Stater Dynamicstate = new Stater();
            Dynamicstate = (Stater)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._OKNextState = Selectedstate._OKNextState;
            Dynamicstate._CancelAndESCROWEmptyNextState = Selectedstate._CancelAndESCROWEmptyNextState;
            Dynamicstate._ErrorNextState = Selectedstate._ErrorNextState;
            Dynamicstate._TimeoutNextState = Selectedstate._TimeoutNextState;
            Dynamicstate._CashCapturedNextState = Selectedstate._CashCapturedNextState;
            Dynamicstate._ScriptId = Selectedstate._ScriptId;
            Dynamicstate._NoteId = Selectedstate._NoteId;

            Dynamicstate._ExtensionStateNumber = Selectedstate._ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate._ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate._ExtensionDescription;
            Dynamicstate._RollbackScreen = Selectedstate._RollbackScreen;
            Dynamicstate._RollbackScreenL2_L3 = Selectedstate._RollbackScreenL2_L3;
            Dynamicstate._TakeMoneyScreen = Selectedstate._TakeMoneyScreen;
            Dynamicstate._TakemoneyScreenL2_L3 = Selectedstate._TakemoneyScreenL2_L3;
            Dynamicstate._AllMoneyCapturedScreen = Selectedstate._AllMoneyCapturedScreen;
            Dynamicstate._Reserved = Selectedstate._Reserved;


            return Dynamicstate;
           
        }
     
        public override object FillPropertyGridFromState(object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Stater Selectedstate = new Stater();
            Selectedstate = (Stater)SelectedPgrid.SelectedObject;
            Stater Dynamicstate = new Stater();
            Dynamicstate = (Stater)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            //Dynamicstate._OKNextState = Selectedstate._OKNextState;
            //Dynamicstate._CancelAndESCROWEmptyNextState = Selectedstate._CancelAndESCROWEmptyNextState;
            //Dynamicstate._ErrorNextState = Selectedstate._ErrorNextState;
            //Dynamicstate._TimeoutNextState = Selectedstate._TimeoutNextState;
            //Dynamicstate._CashCapturedNextState = Selectedstate._CashCapturedNextState;
            Dynamicstate._ScriptId = Selectedstate._ScriptId;
            Dynamicstate._NoteId = Selectedstate._NoteId;

            Dynamicstate._ExtensionStateNumber = Selectedstate._ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate._ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate._ExtensionDescription;
            Dynamicstate._RollbackScreen = Selectedstate._RollbackScreen;
            Dynamicstate._RollbackScreenL2_L3 = Selectedstate._RollbackScreenL2_L3;
            Dynamicstate._TakeMoneyScreen = Selectedstate._TakeMoneyScreen;
            Dynamicstate._TakemoneyScreenL2_L3 = Selectedstate._TakemoneyScreenL2_L3;
            Dynamicstate._AllMoneyCapturedScreen = Selectedstate._AllMoneyCapturedScreen;
            Dynamicstate._Reserved = Selectedstate._Reserved;


            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Stater State = new Stater();
            State = (Stater)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;

            string exsql = sql;
            if (State.ExtensionStateNumber != "255")
            {
                exsql = string.Format(exsql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber,
                State.ExtensionDescription, State.ExtensionType, ProjectName, TransactionName, State.RollbackScreen,
                State.RollbackScreenL2_L3, State.TakeMoneyScreen, State.TakemoneyScreenL2_L3, State.AllMoneyCapturedScreen, State.Reserved,
                State.Reserved, State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(exsql);

            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.OKNextState, State.CancelAndESCROWEmptyNextState, State.ErrorNextState,
                State.TimeoutNextState, State.CashCapturedNextState, State.ScriptId, State.NoteId, State.ExtensionStateNumber,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void setDefaultData()
        {
            StateType = "r";
            StateDescription = "Wincor Cash In";
            _OKNextState = "255";
            _CancelAndESCROWEmptyNextState = "255";
            _ErrorNextState = "255";
            _TimeoutNextState = "255";
            _CashCapturedNextState = "255";
            _ScriptId = "000";
            _NoteId = "000";

            _ExtensionStateNumber = "255";
            _ExtensionType = "Z";
            _ExtensionDescription = "CashIn Extension";
            _RollbackScreen = "000";
            _RollbackScreenL2_L3 = "000";
            _TakeMoneyScreen = "000";
            _TakemoneyScreenL2_L3 = "000";
            _AllMoneyCapturedScreen = "000";
            _Reserved = "000";

        }


        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Stater state = new Stater();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._OKNextState = processRow[8].ToString();
            state._CancelAndESCROWEmptyNextState = processRow[9].ToString();
            state._ErrorNextState = processRow[10].ToString();
            state._TimeoutNextState = processRow[11].ToString();
            state._CashCapturedNextState = processRow[12].ToString();
            state._ScriptId = processRow[13].ToString();
            state._NoteId = processRow[14].ToString();
            state._ExtensionStateNumber= processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu
            if (state.ExtensionStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber);
                state._ExtensionDescription = ExtensionState[4].ToString();
                state._RollbackScreen = ExtensionState[8].ToString();
                state._RollbackScreenL2_L3 = ExtensionState[9].ToString();
                state._TakeMoneyScreen = ExtensionState[10].ToString();
                state._TakemoneyScreenL2_L3 = ExtensionState[11].ToString();
                state._AllMoneyCapturedScreen = ExtensionState[12].ToString();
                state._Reserved = ExtensionState[13].ToString();
                state._Reserved = ExtensionState[14].ToString();
                state._Reserved = ExtensionState[15].ToString();
            }
            

            //NextState Kontrolu
            if (state.OKNextState != "255")
            {
                ChildobjList.Add(GetChildState("OKNextState", state.OKNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            } 
            if (state.CancelAndESCROWEmptyNextState != "255")
            {
                ChildobjList.Add(GetChildState("CancelAndESCROWEmptyNextState", state.CancelAndESCROWEmptyNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.ErrorNextState != "255")
            {
                ChildobjList.Add(GetChildState("ErrorNextState", state.ErrorNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            } 
            if (state.TimeoutNextState != "255")
            {
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.CashCapturedNextState != "255")
            {
                ChildobjList.Add(GetChildState("CashCapturedNextState", state.CashCapturedNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
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
                   // StateList.Remove(StatedRow);
                    return ExtensionState;
                }
            }

            return ExtensionState;
        }


        #endregion

      
    }
}