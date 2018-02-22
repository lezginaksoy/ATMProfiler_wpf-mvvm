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

    public class Stateb : StateBase
    {

        public Stateb(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            setDefaultData();
        }

        public Stateb()
        {
            setDefaultData();
        }
            
        #region State Parameters

        private string _FirstEntryScreen;
        private string _TimeoutNextState;
        private string _CancelNextState;
        private string _GoodNextState;      
        private string _CSPFailNextState;
        private string _SecondEntryScreen;
        private string _MisMatchFirstEntryScreen;

        [CategoryAttribute("State Parameters"), PropertyOrder(1), DescriptionAttribute("First Entry Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FirstEntryScreen
        {
            get
            {
                return _FirstEntryScreen;
            }

            set
            {
                _FirstEntryScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Timeout Next State")]
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

        [Category("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Cancel Next State")]
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

        [Category("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Good Next State")]
        public string GoodNextState
        {
            get
            {
                return _GoodNextState;
            }
            set
            {
                _GoodNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("CSP Fail  Next State Number")]
        public string CSPFailNextState
        {
            get
            {
                return _CSPFailNextState;
            }
            set
            {
                _CSPFailNextState = value.PadLeft(3, '0');
            }
        }

        [Category("State Parameters"), PropertyOrder(7), DescriptionAttribute("Second Entry Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string SecondEntryScreen
        {
            get
            {
                return _SecondEntryScreen;
            }
            set
            {
                _SecondEntryScreen = value.PadLeft(3,'0');
            }
        }

        [Category("State Parameters"), PropertyOrder(8), DescriptionAttribute("Mis Match First Entry Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string MisMatchFirstEntryScreen
        {
            get
            {
                return _MisMatchFirstEntryScreen;
            }
            set
            {
                _MisMatchFirstEntryScreen = value.PadLeft(3,'0');
            }
        }
      
        #endregion

        #region Extension State 

        private string _ExtensionStateNumber;
        private string _ExtensionType;
        private string _ExtensionDescription;
        private string _NumbermatchingCSPpair;
        private string _PINpairattempts;    
        private string _Reserved;
    
        #region converter

        public class NumbermatchingCSPpairConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();            
                strings.Add(255,"None");
                strings.Add(000, "000");
                strings.Add(001, "001");
                strings.Add(002, "002");
                strings.Add(003, "003");
                strings.Add(004, "004");
                strings.Add(005, "005");
                strings.Add(006, "006");
                strings.Add(007, "007");
                strings.Add(008, "008");
                strings.Add(009, "009");
                strings.Add(010, "010");               
                return strings;
            }
        }

        public class PINpairattemptsConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(255, "None");
                strings.Add(000, "Do not verify locally");
                strings.Add(001, "Verify both attempts locally");
                return strings;
            }
        }

       
        #endregion

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(7), DescriptionAttribute("Extension State Number")]
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
        
        [Category("State Extension Parameters"), PropertyOrder(11), DescriptionAttribute("Number of attempts allowed to enter a matching CSP pair")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(NumbermatchingCSPpairConverter))]
        public string NumbermatchingCSPpair
        {
            get
            {
                return _NumbermatchingCSPpair;
            }
            set
            {
                _NumbermatchingCSPpair = value;
            }
        }
      
        [Category("State Extension Parameters"), PropertyOrder(12), DescriptionAttribute("Use local verification of new PIN pair attempts")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(PINpairattemptsConverter))]
        public string PINpairattempts
        {
            get { return _PINpairattempts; }
            set { _PINpairattempts = value; }
        }

        [Category("State Extension Parameters"), PropertyOrder(13), ReadOnlyAttribute(true),DescriptionAttribute("Reserved")]
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

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Stateb Selectedstate = new Stateb();
            Selectedstate = (Stateb)SelectedPgrid.SelectedObject;
            Stateb Dynamicstate = new Stateb();
            Dynamicstate = (Stateb)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            
            Dynamicstate._FirstEntryScreen = Selectedstate.FirstEntryScreen;
            Dynamicstate._TimeoutNextState = Selectedstate.TimeoutNextState;
            Dynamicstate._CancelNextState = Selectedstate.CancelNextState;
            Dynamicstate._GoodNextState = Selectedstate.GoodNextState;
            Dynamicstate._CSPFailNextState = Selectedstate.CSPFailNextState;
            Dynamicstate._SecondEntryScreen = Selectedstate.SecondEntryScreen;
            Dynamicstate._MisMatchFirstEntryScreen = Selectedstate.MisMatchFirstEntryScreen;          
         
            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate.ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._NumbermatchingCSPpair = Selectedstate.NumbermatchingCSPpair;
            Dynamicstate._PINpairattempts = Selectedstate.PINpairattempts;
            Dynamicstate._Reserved = Selectedstate.Reserved;

            return Dynamicstate;           
        }
     
        public override object FillPropertyGridFromState(object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Stateb Selectedstate = new Stateb();
            Selectedstate = (Stateb)SelectedPgrid.SelectedObject;
            Stateb Dynamicstate = new Stateb();
            Dynamicstate = (Stateb)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._FirstEntryScreen = Selectedstate.FirstEntryScreen;
            //Dynamicstate._TimeoutNextState = Selectedstate._TimeoutNextState;
            //Dynamicstate._CancelNextState = Selectedstate._CancelNextState;
            //Dynamicstate._GoodNextState = Selectedstate._GoodNextState;
            //Dynamicstate._CSPFailNextState = Selectedstate._CSPFailNextState;
            Dynamicstate._SecondEntryScreen = Selectedstate.SecondEntryScreen;
            Dynamicstate._MisMatchFirstEntryScreen = Selectedstate.MisMatchFirstEntryScreen;

            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate.ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._NumbermatchingCSPpair = Selectedstate.NumbermatchingCSPpair;
            Dynamicstate._PINpairattempts = Selectedstate.PINpairattempts;
            Dynamicstate._Reserved = Selectedstate.Reserved;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Stateb State = new Stateb();
            State = (Stateb)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;

            string exsql = sql;
            if (State.ExtensionStateNumber != "255")
            {
                exsql = string.Format(exsql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber,
                 State.ExtensionDescription, State.ExtensionType, ProjectName, TransactionName, State.NumbermatchingCSPpair,
                 State.PINpairattempts, State.Reserved, State.Reserved, State.Reserved, State.Reserved,
                 State.Reserved, State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(exsql);
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.FirstEntryScreen, State.TimeoutNextState, State.CancelNextState,
                State.GoodNextState, State.CSPFailNextState, State.SecondEntryScreen, State.MisMatchFirstEntryScreen, State.ExtensionStateNumber,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }
        
        private void setDefaultData()
        {
            StateType = "b";
            StateDescription = "Customer‐Selectable PIN State";
            _FirstEntryScreen = "000";
            _TimeoutNextState = "255";
            _CancelNextState = "255";
            _GoodNextState = "255";
            _CSPFailNextState = "255";
            _SecondEntryScreen = "255";
            _MisMatchFirstEntryScreen = "000";

            _ExtensionStateNumber = "255";
            _ExtensionType = "Z";
            _ExtensionDescription = "Customer‐Selectable PIN  Extension";
            _NumbermatchingCSPpair="000";
            _PINpairattempts="000";
            _Reserved = "000";
        }



        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Stateb state = new Stateb();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._FirstEntryScreen = processRow[8].ToString();
            state._TimeoutNextState = processRow[9].ToString();
            state._CancelNextState= processRow[10].ToString();
            state._GoodNextState = processRow[11].ToString();
            state._CSPFailNextState = processRow[12].ToString();
            state._SecondEntryScreen = processRow[13].ToString();
            state._MisMatchFirstEntryScreen = processRow[14].ToString();
            state._ExtensionStateNumber= processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu
            if (state.ExtensionStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber);
                state._ExtensionDescription= ExtensionState[4].ToString();                
                state._NumbermatchingCSPpair = ExtensionState[8].ToString();
                state._PINpairattempts = ExtensionState[9].ToString();
                state._Reserved = ExtensionState[10].ToString();
            }
            

            //NextState Kontrolu
            //if (state.FirstEntryScreen != "255")
            //{
            //    ChildobjList.Add(GetChildState("FirstEntryScreen", state.FirstEntryScreen, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            //}
            if (state.TimeoutNextState != "255")
            {
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.CancelNextState != "255")
            {
                ChildobjList.Add(GetChildState("CancelNextState", state.CancelNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.GoodNextState != "255")
            {
                ChildobjList.Add(GetChildState("GoodNextState", state.GoodNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.CSPFailNextState != "255")
            {
                ChildobjList.Add(GetChildState("CSPFailNextState", state.CSPFailNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
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