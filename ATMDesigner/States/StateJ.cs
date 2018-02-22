using ATMDesigner.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
   
    public class StateJ : StateBase
    {

        public StateJ(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public StateJ()
        {
            SetDefaultData();
        }
             

        #region State Parameters

        private string _ReceiptDeliveredScreen;
        private string _NextState;
        private string _NoReceiptDeliveredScreen;
        private string _CardRetainedScreen;
        private string _StatementDeliveredScreen;
        private string _Reserved;
        private string _BNANotesReturnedScreen;

        [CategoryAttribute("State Parameters"), PropertyOrder(1), DescriptionAttribute("Receipt Delivered Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ReceiptDeliveredScreen
        {
            get
            {
                return _ReceiptDeliveredScreen;
            }

            set
            {
                _ReceiptDeliveredScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Next State")]
        public string NextState
        {
            get
            {
                return _NextState;
            }

            set
            {
                _NextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("No Receipt Delivered Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string NoReceiptDeliveredScreen
        {
            get
            {
                return _NoReceiptDeliveredScreen;
            }

            set
            {
                _NoReceiptDeliveredScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), DescriptionAttribute("Card Retained Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string CardRetainedScreen
        {
            get
            {
                return _CardRetainedScreen;
            }

            set
            {
                _CardRetainedScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(5), DescriptionAttribute("Statement Delivered Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string StatementDeliveredScreen
        {
            get
            {
                return _StatementDeliveredScreen;
            }

            set
            {
                _StatementDeliveredScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Reserved")]
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
        
        [CategoryAttribute("State Parameters"), PropertyOrder(7), DescriptionAttribute("BNA Notes Returned Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string BNANotesReturnedScreen
        {
            get
            {
                return _BNANotesReturnedScreen;
            }

            set
            {
                _BNANotesReturnedScreen = value.PadLeft(3, '0');
            }
        }

        #endregion
        
        #region Extension State

        //Extension1      
        private string _ExtensionStateNumber1;
        private string _ExtensionStateType1;        
        private string _ExtensionDescription1;
        private string _DocumentScreenNumber;
        private string _ChequeProcessorDocument;      
        private string _BNANotesReturnRetainLeaveFlag1;


         //Extension2
        private string _ExtensionStateNumber2;
        private string _ExtensionStateType2;        
        private string _ExtensionDescription2;
        private string _ChequesScreenNumber;
        private string _RetractingPMediaScreenNumber;
        private string _BNANotesReturnRetainLeaveFlag2;


        #region converter
        public class ChequeProcessorDocumentConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(255, "None");
                strings.Add(000,"Retain");
                strings.Add(001,"Return and retract if not taken");
                strings.Add(002,"Return and do not retract if not taken");
                return strings;
            }
        }

        public class BNANotesConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(255, "None");
                strings.Add(000,"Return");
                strings.Add(001,"Retain");
                strings.Add(002,"Leave notes in escrow");
                return strings;
            }
        }
        #endregion

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(6), DescriptionAttribute("Extension State Number")]
        public string ExtensionStateNumber1
        {
            get
            {
                return _ExtensionStateNumber1;
            }
            set
            {
                _ExtensionStateNumber1 = value.PadLeft(3, '0');
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(7), DescriptionAttribute("Extension State Type"), ReadOnlyAttribute(true)]
        public string ExtensionStateType1
        {
            get
            {
                return _ExtensionStateType1;
            }                       
        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(8), DescriptionAttribute("Extension Description")]
        public string ExtensionDescription1
        {
            get
            {
                return _ExtensionDescription1;
            }
            set
            {
                _ExtensionDescription1 = value;
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(9), DescriptionAttribute("Document Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string DocumentScreenNumber
        {
            get
            {
                return _DocumentScreenNumber;
            }
            set 
            {
                _DocumentScreenNumber = value.PadLeft(3,'0');
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(10), DescriptionAttribute("Cheque Processor Document")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ChequeProcessorDocumentConverter))]
        public string ChequeProcessorDocument
        {
            get
            {
                return _ChequeProcessorDocument;
            }
            set
            {
                _ChequeProcessorDocument = value.PadLeft(3,'0');
            }
        }

       
        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(12), DescriptionAttribute("BNA Notes ReturnRetainLeaveFlag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BNANotesConverter))]
        public string BNANotesReturnRetainLeaveFlag1
        {
            get
            {
                return _BNANotesReturnRetainLeaveFlag1;
            }
            set
            {
                _BNANotesReturnRetainLeaveFlag1 = value.PadLeft(3,'0');
            }
        }

        //extension 2
        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(8), DescriptionAttribute("Extension State Number")]
        public string ExtensionStateNumber2
        {
            get
            {
                return _ExtensionStateNumber2;
            }
            set
            {
                _ExtensionStateNumber2 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(9), DescriptionAttribute("Extension State Type"), ReadOnlyAttribute(true)]
        public string ExtensionStateType2
        {
            get
            {
                return _ExtensionStateType2;
            }
        }

        [CategoryAttribute("State Extension 2 Parameters"),PropertyOrder(10), DescriptionAttribute("Extension Description")]
        public string ExtensionDescription2
        {
            get
            {
                return _ExtensionDescription2;
            }
            set
            {
                _ExtensionDescription2 = value;
            }

        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(11), DescriptionAttribute("Cheques Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ChequesScreenNumber
        {
            get
            {
                return _ChequesScreenNumber;
            }
            set
            {
                _ChequesScreenNumber = value.PadLeft(3, '0');
            }
        }
             

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(11), DescriptionAttribute("_Retracting P. Media Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string RetractingPMediaScreenNumber
        {
            get
            {
                return _RetractingPMediaScreenNumber;
            }
            set
            {
                _RetractingPMediaScreenNumber = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(12), DescriptionAttribute("BNA Notes ReturnRetainLeaveFlag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BNANotesConverter))]
        public string BNANotesReturnRetainLeaveFlag2
        {
            get
            {
                return _BNANotesReturnRetainLeaveFlag2;
            }
            set
            {
                _BNANotesReturnRetainLeaveFlag2 = value.PadLeft(3, '0');
            }
        }


        #endregion
        
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateJ Selectedstate = new StateJ();
            StateJ Dynamicstate = new StateJ();

            Selectedstate = (StateJ)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateJ)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ExtensionDescription1 = Selectedstate.ExtensionDescription1;
            Dynamicstate._ExtensionDescription2 = Selectedstate.ExtensionDescription2;

            Dynamicstate._BNANotesReturnedScreen = Selectedstate._BNANotesReturnedScreen;
            Dynamicstate._BNANotesReturnRetainLeaveFlag1 = Selectedstate._BNANotesReturnRetainLeaveFlag1;
            Dynamicstate._BNANotesReturnRetainLeaveFlag2 = Selectedstate._BNANotesReturnRetainLeaveFlag2;
            Dynamicstate._CardRetainedScreen = Selectedstate._CardRetainedScreen;
            Dynamicstate._ChequeProcessorDocument = Selectedstate._ChequeProcessorDocument;
            Dynamicstate._ChequesScreenNumber = Selectedstate._ChequesScreenNumber;
            Dynamicstate._DocumentScreenNumber = Selectedstate._DocumentScreenNumber;
            Dynamicstate._NextState = Selectedstate._NextState;
            Dynamicstate._NoReceiptDeliveredScreen = Selectedstate._NoReceiptDeliveredScreen;
            Dynamicstate._ReceiptDeliveredScreen = Selectedstate._ReceiptDeliveredScreen;
            Dynamicstate._Reserved = Selectedstate._Reserved;
            Dynamicstate._RetractingPMediaScreenNumber = Selectedstate._RetractingPMediaScreenNumber;
            Dynamicstate._StatementDeliveredScreen = Selectedstate._StatementDeliveredScreen;

            return Dynamicstate;
        }

        public override Object  FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateJ Selectedstate = new StateJ();
            StateJ Dynamicstate = new StateJ();

            Selectedstate = (StateJ)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateJ)ClassInstance;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._Description = Selectedstate.StateDescription;

            Dynamicstate._ExtensionDescription1 = Selectedstate.ExtensionDescription1;
            Dynamicstate._ExtensionDescription2 = Selectedstate.ExtensionDescription2;

            Dynamicstate._BNANotesReturnedScreen = Selectedstate._BNANotesReturnedScreen;
            Dynamicstate._BNANotesReturnRetainLeaveFlag1 = Selectedstate._BNANotesReturnRetainLeaveFlag1;
            Dynamicstate._BNANotesReturnRetainLeaveFlag2 = Selectedstate._BNANotesReturnRetainLeaveFlag2;
            Dynamicstate._CardRetainedScreen = Selectedstate._CardRetainedScreen;
            Dynamicstate._ChequeProcessorDocument = Selectedstate._ChequeProcessorDocument;
            Dynamicstate._ChequesScreenNumber = Selectedstate._ChequesScreenNumber;
            Dynamicstate._DocumentScreenNumber = Selectedstate._DocumentScreenNumber;
            //Dynamicstate._NextState = Selectedstate._NextState;
            Dynamicstate._NoReceiptDeliveredScreen = Selectedstate._NoReceiptDeliveredScreen;
            Dynamicstate._ReceiptDeliveredScreen = Selectedstate._ReceiptDeliveredScreen;
            Dynamicstate._Reserved = Selectedstate._Reserved;
            Dynamicstate._RetractingPMediaScreenNumber = Selectedstate._RetractingPMediaScreenNumber;
            Dynamicstate._StatementDeliveredScreen = Selectedstate._StatementDeliveredScreen;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateJ State = new StateJ();
            State = (StateJ)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            string exsql1 = SqlStr;
            string exsql2 = SqlStr;

            if (State.ExtensionStateNumber1 != "255")
            {
                exsql1 = string.Format(exsql1, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber1,
                State.ExtensionDescription1, State.ExtensionStateType1, ProjectName, TransactionName, State.DocumentScreenNumber,
                State.ChequeProcessorDocument, State.Reserved, State.Reserved, State.ExtensionStateNumber2, State.Reserved, State.BNANotesReturnRetainLeaveFlag1,
                State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(exsql1);


                if (State.ExtensionStateNumber2 != "255")
                {
                    exsql2 = string.Format(exsql2, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber2,
                    State.ExtensionDescription2, State.ExtensionStateType2, ProjectName, TransactionName, State.ChequesScreenNumber,
                    State.RetractingPMediaScreenNumber, State.Reserved, "000", State.Reserved, State.Reserved, State.BNANotesReturnRetainLeaveFlag2, State.Reserved,
                    State.ConfigId, State.BrandId, State.ConfigVersion);
                    SqlStringList.Add(exsql2);
                }
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ReceiptDeliveredScreen, State.NextState, State.NoReceiptDeliveredScreen,
                State.CardRetainedScreen, State.StatementDeliveredScreen, State.Reserved, State.BNANotesReturnedScreen, State.ExtensionStateNumber1,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }
        
        private void SetDefaultData()
        {
            StateType = "J";
            StateDescription = "Close State";

            _NextState = "255";
            _ReceiptDeliveredScreen = "000";
            _NoReceiptDeliveredScreen = "000";
            _CardRetainedScreen = "000";             
            _StatementDeliveredScreen = "000";
            _BNANotesReturnedScreen = "000";
            _DocumentScreenNumber = "000";
            _ExtensionStateNumber1 = "255";
            _ExtensionStateNumber2 = "255";
            _Reserved = "000";

            _ExtensionDescription1 = "State Z";
            _ExtensionDescription2 = "State Z";
            _ExtensionStateType1 = "Z";
            _ExtensionStateType2 = "Z";
            _ChequesScreenNumber = "000";
            _RetractingPMediaScreenNumber = "000";
            _BNANotesReturnRetainLeaveFlag1 = "255";
            _BNANotesReturnRetainLeaveFlag2 = "255";
            _ChequeProcessorDocument = "255";

           

        }



        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateJ state = new StateJ();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ReceiptDeliveredScreen = processRow[8].ToString();
            state._NextState = processRow[9].ToString();
            state._NoReceiptDeliveredScreen = processRow[10].ToString();
            state._CardRetainedScreen = processRow[11].ToString();
            state._StatementDeliveredScreen = processRow[12].ToString();
            state.Reserved = processRow[13].ToString();
            state._BNANotesReturnedScreen = processRow[14].ToString();
            state._ExtensionStateNumber1 = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu
            if (state.ExtensionStateNumber1 != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber1);
                state._ExtensionDescription1 = ExtensionState[4].ToString();
                state._DocumentScreenNumber = ExtensionState[8].ToString();
                state._ChequeProcessorDocument = ExtensionState[9].ToString();
                state._ExtensionStateNumber2 = ExtensionState[12].ToString();
                state._BNANotesReturnRetainLeaveFlag1 = ExtensionState[14].ToString();
                
                if (state.ExtensionStateNumber2 != "255")
                {
                    object[] ExtensionStates2 = GetExtensionState(ref StateList, state.ExtensionStateNumber2);
                    state._ExtensionDescription2 = ExtensionStates2[4].ToString();
                    state._ChequesScreenNumber = ExtensionStates2[8].ToString();
                    state._RetractingPMediaScreenNumber = ExtensionStates2[9].ToString();
                    state._BNANotesReturnRetainLeaveFlag2 = ExtensionStates2[14].ToString();
                }

            }
        
            //NextState Kontrolu
            if (state.NextState != "255")
            {
                ChildobjList.Add(GetChildState("NextState", state.NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
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