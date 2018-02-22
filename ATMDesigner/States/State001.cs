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

    //  e001  Customer EMV Application selection- EMV Chip Card State
    public class Statee001 : StateBase
    {

        public Statee001(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public Statee001()
        {
            SetDefaultValues();
        }

        #region Converters

        public class NextPageFDKConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000, "None");
                strings.Add(001, "A");
                strings.Add(002, "B");
                strings.Add(003, "C");
                strings.Add(004, "D");
                strings.Add(006, "F");
                strings.Add(007, "G");
                strings.Add(008, "H");
                strings.Add(009, "I");               
                return strings;
            }
        }

        public class ExtensionFDKPriorityConverter : ITypeEditor
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

                FDKPriorityApp fdkpopup = new FDKPriorityApp(btn.Content.ToString());
                fdkpopup.ShowDialog();
                btn.Content = fdkpopup.FDKValue;


                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }
                DockPanel Sourcepnl = (DockPanel)Ditem.Content;
                PropertyGrid SelectedPgrid = designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid;
                string SelectedProperty = btn.Name.ToString();
                string newValue = btn.Content.ToString();
                Statee001 state = (Statee001)SelectedPgrid.SelectedObject;

                Type ClassType = state.GetType();
                PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                property.SetValue(state, newValue, null);
                designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = state;
            }
        }



        #endregion

        #region state parameters

        private string _ChipCardOperation;
        public string  _AppSelectedNextstate;
        private string _OneAppAvaliableNextState;
        private string _NoAppAvaliableNextState;
        private string _TimeoutNextState;
        private string _CancelNextState;      
        private string _AppSelectionScreenNumber;     
     
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

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Next state if App is selected")]
        public string AppSelectedNextstate
        {
            get
            {
                return _AppSelectedNextstate;
            }

            set
            {
                _AppSelectedNextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Next state number if there is one App is avaliable")]
        public string OneAppAvaliableNextState
        {
            get
            {
                return _OneAppAvaliableNextState;
            }
            set
            {
                _OneAppAvaliableNextState = value.PadLeft(3, '0');
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
        
        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("Next state if Timeout occurred")]
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

        [CategoryAttribute("State Parameters"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("Next state if Cancel is pressed")]
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

        [CategoryAttribute("State Parameters"), PropertyOrder(8), DescriptionAttribute("Screen number for application selection")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string AppSelectionScreenNumber
        {
            get
            {
                return _AppSelectionScreenNumber;
            }

            set
            {
                _AppSelectionScreenNumber = value.PadLeft(3,'0');
            }
        }

        #endregion

        #region Extension States

        private string _ExtensionStateNumber;
        private string _ExtensionType;
        private string _ExtensionDescription;     
        private string _NextPageFDK;     
        private string _NextPageFDKActiveScreenNumber;
        private string _FirstPageFDK;
        private string _FirstPageActiveScreenNumber;
        private string _FDKPriorityApp1To3;
        private string _FDKPriorityApp4To6;
        private string _FDKPriorityApp7To8;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("Extension States"), PropertyOrder(1), DescriptionAttribute("Extension State Number")]      
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

        [CategoryAttribute("Extension States"), PropertyOrder(4),DescriptionAttribute("Next Page FDK")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(NextPageFDKConverter))]       
        public string NextPageFDK
        {
            get
            {
                return _NextPageFDK;
            }
            set
            {
                _NextPageFDK = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(5), DescriptionAttribute("NextPage FDK Active Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string NextPageFDKActiveScreenNumber
        {
            get
            {
                return _NextPageFDKActiveScreenNumber;
            }
            set
            {
                _NextPageFDKActiveScreenNumber= value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("First Page FDK")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(NextPageFDKConverter))]       
        public string FirstPageFDK
        {
            get
            {
                return _FirstPageFDK;
            }
            set
            {
                _FirstPageFDK= value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Extension States"), PropertyOrder(7), ReadOnly(true), DescriptionAttribute("FirstPage Active Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FirstPageActiveScreenNumber
        {
            get
            {
                return _FirstPageActiveScreenNumber;
            }
            set
            {
                _FirstPageActiveScreenNumber= value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("Extension States"), PropertyOrder(8), DescriptionAttribute("FDK Priority for  App. 1To3")]
        [Editor(typeof(ExtensionFDKPriorityConverter), typeof(ExtensionFDKPriorityConverter))]       
        public string FDKPriorityApp1To3
        {
            get
            {
                return _FDKPriorityApp1To3;
            }
            set
            {
                _FDKPriorityApp1To3= value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("Extension States"), PropertyOrder(9), DescriptionAttribute("FDK Priority for  App. 4To6")]
        [Editor(typeof(ExtensionFDKPriorityConverter), typeof(ExtensionFDKPriorityConverter))]
        public string FDKPriorityApp4To6
        {
            get
            {
                return _FDKPriorityApp4To6;
            }
            set
            {
                _FDKPriorityApp4To6= value.PadLeft(3, '0');
            }
        }
        
        [CategoryAttribute("Extension States"), PropertyOrder(10),DescriptionAttribute("FDK Priority for  App. 7To8")]
        [Editor(typeof(ExtensionFDKPriorityConverter), typeof(ExtensionFDKPriorityConverter))]
        public string FDKPriorityApp7To8
        {
            get
            {
                return _FDKPriorityApp7To8;
            }
            set
            {
                _FDKPriorityApp7To8= value.PadLeft(3, '0');
            }
        }
             
        #endregion
                
        #region FDK Extension States

        private string _FDKExtensionStateNumber;
        private string _FDKExtensionType;
        private string _FDKExtensionDescription;        
        private string _AFDKappScreen;     
        private string _BFDKappScreen;     
        private string _CFDKappScreen;     
        private string _DFDKappScreen;     
        private string _FFDKappScreen;     
        private string _GFDKappScreen;     
        private string _HFDKappScreen;     
        private string _IFDKappScreen;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("FDK Extension States"), PropertyOrder(1), DescriptionAttribute("Extension Number")]
        public string FDKExtensionStateNumber
        {
            get
            {
                return _FDKExtensionStateNumber;
            }
            set
            {
                _FDKExtensionStateNumber = value.PadLeft(3, '0');
            }

        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(2), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string FDKExtensionType
        {
            get { return _FDKExtensionType; }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(3), DescriptionAttribute("Extension Description")]
        public string FDKExtensionDescription
        {
            get
            {
                return _FDKExtensionDescription;
            }
            set
            {
                _FDKExtensionDescription= value;
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(4), DescriptionAttribute("App. Screen Template number for FDK A.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string AFDKappScreen
        {
            get
            {
                return _AFDKappScreen;
            }
            set
            {
                _AFDKappScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(5), DescriptionAttribute("App. Screen Template number for FDK B.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string BFDKappScreen
        {
            get
            {
                return _BFDKappScreen;
            }
            set
            {
                _BFDKappScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(6), DescriptionAttribute("App. Screen Template number for FDK C.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string CFDKappScreen
        {
            get
            {
                return _CFDKappScreen;
            }
            set
            {
                _CFDKappScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(7), DescriptionAttribute("App. Screen Template number for FDK D.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string DFDKappScreen
        {
            get
            {
                return _DFDKappScreen;
            }
            set
            {
                _DFDKappScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(8), DescriptionAttribute("App. Screen Template number for FDK F.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FFDKappScreen
        {
            get
            {
                return _FFDKappScreen;
            }
            set
            {
                _FFDKappScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(9), DescriptionAttribute("App. Screen Template number for FDK G.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string GFDKappScreen
        {
            get
            {
                return _GFDKappScreen;
            }
            set
            {
                _GFDKappScreen = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("FDK Extension States"), PropertyOrder(10), DescriptionAttribute("App. Screen Template number for FDK H.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string HFDKappScreen
        {
            get
            {
                return _HFDKappScreen;
            }
            set
            {
                _HFDKappScreen = value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("FDK Extension States"), PropertyOrder(11), DescriptionAttribute("App. Screen Template number for FDK I.")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string IFDKappScreen
        {
            get
            {
                return _IFDKappScreen;
            }
            set
            {
                _IFDKappScreen = value.PadLeft(3, '0');
            }
        }
       
        
        #endregion
        
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            Statee001 Selectedstate = new Statee001();
            Statee001 Dynamicstate = new Statee001();
            Selectedstate = (Statee001)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee001)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            Dynamicstate._AppSelectedNextstate = Selectedstate.AppSelectedNextstate;
            Dynamicstate._OneAppAvaliableNextState = Selectedstate.OneAppAvaliableNextState;
            Dynamicstate._NoAppAvaliableNextState = Selectedstate.NoAppAvaliableNextState;
            Dynamicstate._NoAppAvaliableNextState = Selectedstate.NoAppAvaliableNextState;
            Dynamicstate._TimeoutNextState = Selectedstate.TimeoutNextState;
            Dynamicstate._CancelNextState = Selectedstate.CancelNextState;
            Dynamicstate._AppSelectionScreenNumber = Selectedstate.AppSelectionScreenNumber;

            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate.ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._NextPageFDK = Selectedstate.NextPageFDK;
            Dynamicstate._NextPageFDKActiveScreenNumber = Selectedstate.NextPageFDKActiveScreenNumber;
            Dynamicstate._FirstPageFDK = Selectedstate.FirstPageFDK;
            Dynamicstate._FirstPageActiveScreenNumber = Selectedstate.FirstPageActiveScreenNumber;
            Dynamicstate._FDKPriorityApp1To3 = Selectedstate.FDKPriorityApp1To3;
            Dynamicstate._FDKPriorityApp4To6 = Selectedstate.FDKPriorityApp4To6;
            Dynamicstate._FDKPriorityApp7To8 = Selectedstate.FDKPriorityApp7To8;

            Dynamicstate._FDKExtensionStateNumber = Selectedstate.FDKExtensionStateNumber;
            Dynamicstate._FDKExtensionType = Selectedstate.FDKExtensionType;
            Dynamicstate.FDKExtensionDescription = Selectedstate.FDKExtensionDescription;
            Dynamicstate._AFDKappScreen = Selectedstate.AFDKappScreen;
            Dynamicstate._BFDKappScreen = Selectedstate.BFDKappScreen;
            Dynamicstate._CFDKappScreen = Selectedstate.CFDKappScreen;
            Dynamicstate._DFDKappScreen = Selectedstate.DFDKappScreen;
            Dynamicstate._FFDKappScreen = Selectedstate.FFDKappScreen;
            Dynamicstate._GFDKappScreen = Selectedstate.GFDKappScreen;
            Dynamicstate._HFDKappScreen = Selectedstate.HFDKappScreen;
            Dynamicstate._IFDKappScreen = Selectedstate.IFDKappScreen;
            
            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Statee001 Selectedstate = new Statee001();
            Statee001 Dynamicstate = new Statee001();
            Selectedstate = (Statee001)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee001)ClassInstance;

            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            //Dynamicstate._AppSelectedNextstate = Selectedstate._AppSelectedNextstate;
            //Dynamicstate._OneAppAvaliableNextState = Selectedstate._OneAppAvaliableNextState;
            //Dynamicstate._NoAppAvaliableNextState = Selectedstate._NoAppAvaliableNextState;          
            //Dynamicstate._TimeoutNextState = Selectedstate._TimeoutNextState;
            //Dynamicstate._CancelNextState = Selectedstate._CancelNextState;
            Dynamicstate._AppSelectionScreenNumber = Selectedstate.AppSelectionScreenNumber;

            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;
            Dynamicstate._ExtensionType = Selectedstate.ExtensionType;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._NextPageFDK = Selectedstate.NextPageFDK;
            Dynamicstate._NextPageFDKActiveScreenNumber = Selectedstate.NextPageFDKActiveScreenNumber;
            Dynamicstate._FirstPageFDK = Selectedstate.FirstPageFDK;
            Dynamicstate._FirstPageActiveScreenNumber = Selectedstate.FirstPageActiveScreenNumber;
            Dynamicstate._FDKPriorityApp1To3 = Selectedstate.FDKPriorityApp1To3;
            Dynamicstate._FDKPriorityApp4To6 = Selectedstate.FDKPriorityApp4To6;
            Dynamicstate._FDKPriorityApp7To8 = Selectedstate.FDKPriorityApp7To8;

            Dynamicstate._FDKExtensionStateNumber = Selectedstate.FDKExtensionStateNumber;
            Dynamicstate._FDKExtensionType = Selectedstate.FDKExtensionType;
            Dynamicstate._FDKExtensionDescription = Selectedstate.FDKExtensionDescription;
            Dynamicstate._AFDKappScreen = Selectedstate.AFDKappScreen;
            Dynamicstate._BFDKappScreen = Selectedstate.BFDKappScreen;
            Dynamicstate._CFDKappScreen = Selectedstate.CFDKappScreen;
            Dynamicstate._DFDKappScreen = Selectedstate.DFDKappScreen;
            Dynamicstate._FFDKappScreen = Selectedstate.FFDKappScreen;
            Dynamicstate._GFDKappScreen = Selectedstate.GFDKappScreen;
            Dynamicstate._HFDKappScreen = Selectedstate.HFDKappScreen;
            Dynamicstate._IFDKappScreen = Selectedstate.IFDKappScreen;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Statee001 State = new Statee001();
            State = (Statee001)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;

            string ex1sql = sql;
            string ex2sql = sql;

            //Extension
            if (State.ExtensionStateNumber != "255")
            {
                ex1sql = string.Format(ex1sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber,
                    State.ExtensionDescription, State.ExtensionType, ProjectName, TransactionName, State.FDKExtensionStateNumber, State.NextPageFDK,
                    State.NextPageFDKActiveScreenNumber, State.FirstPageFDK, State.FirstPageActiveScreenNumber, State.FDKPriorityApp1To3,
                    State.FDKPriorityApp4To6, State.FDKPriorityApp7To8,
                 State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex1sql);
            }

            //Screen Extension

            if (State.FDKExtensionStateNumber != "255")
            {
                ex2sql = string.Format(ex2sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.FDKExtensionStateNumber,
                    State.FDKExtensionDescription, State.FDKExtensionType, ProjectName, TransactionName, State.AFDKappScreen,
                    State.BFDKappScreen, State.CFDKappScreen, State.DFDKappScreen, State.FFDKappScreen,
                    State.GFDKappScreen, State.HFDKappScreen, State.IFDKappScreen,
                 State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex2sql);
            }
            

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ChipCardOperation, State.AppSelectedNextstate, State.OneAppAvaliableNextState,
                State.NoAppAvaliableNextState, State.TimeoutNextState, State.CancelNextState, State.AppSelectionScreenNumber,
                State.ExtensionStateNumber, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "e";        
            StateDescription = "Customer EMV Application selection";
            _ChipCardOperation = "001";
            _AppSelectedNextstate = "255";
            _OneAppAvaliableNextState = "255";
            _NoAppAvaliableNextState="255";
            _TimeoutNextState = "255";
            _CancelNextState = "255";
           _AppSelectionScreenNumber = "000";


            _ExtensionStateNumber = "255";
            _ExtensionType = "Z";
            _ExtensionDescription = "Application selection Extension State";          
            _NextPageFDK = "000";
            _NextPageFDKActiveScreenNumber = "000";
            _FirstPageFDK = "000";           
            _FirstPageActiveScreenNumber = "000";
            _FDKPriorityApp1To3="000";
            _FDKPriorityApp4To6="000";
            _FDKPriorityApp7To8="000";


            _FDKExtensionStateNumber="255";
            _FDKExtensionType="Z";
            _FDKExtensionDescription="Application selection FDK usage Extension State";
            _AFDKappScreen="000";
            _BFDKappScreen="000";
            _CFDKappScreen="000";
            _DFDKappScreen="000";
            _FFDKappScreen="000";
            _GFDKappScreen="000";
            _HFDKappScreen="000";
            _IFDKappScreen="000";
            
        }



        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Statee001 state = new Statee001();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ChipCardOperation = processRow[8].ToString();
            state._AppSelectedNextstate = processRow[9].ToString();
            state._OneAppAvaliableNextState = processRow[10].ToString();
            state._NoAppAvaliableNextState = processRow[11].ToString();
            state._TimeoutNextState= processRow[12].ToString();
            state._CancelNextState = processRow[13].ToString();
            state._AppSelectionScreenNumber = processRow[14].ToString();
            state._ExtensionStateNumber = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu Screen
            if (state.ExtensionStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber);
                state._ExtensionDescription = processRow[4].ToString();
                state._FDKExtensionStateNumber = ExtensionState[8].ToString();
                state._NextPageFDK = ExtensionState[9].ToString();
                state._NextPageFDKActiveScreenNumber = ExtensionState[10].ToString();
                state._FirstPageFDK = ExtensionState[11].ToString();
                state._FirstPageActiveScreenNumber = ExtensionState[12].ToString();
                state._FDKPriorityApp1To3 = ExtensionState[13].ToString();
                state._FDKPriorityApp4To6 = ExtensionState[14].ToString();
                state._FDKPriorityApp7To8 = ExtensionState[15].ToString();

                //Extension FDK
                if (state.FDKExtensionStateNumber != "255")
                {
                    object[] FDKExtensionState = GetExtensionState(ref StateList, state.FDKExtensionStateNumber);
                    state._FDKExtensionDescription = FDKExtensionState[4].ToString();
                    state._AFDKappScreen = FDKExtensionState[8].ToString();
                    state._BFDKappScreen = ExtensionState[9].ToString();
                    state._CFDKappScreen = ExtensionState[10].ToString();
                    state._DFDKappScreen = ExtensionState[11].ToString();
                    state._FFDKappScreen = ExtensionState[12].ToString();
                    state._GFDKappScreen = ExtensionState[13].ToString();
                    state._HFDKappScreen = ExtensionState[14].ToString();
                    state._IFDKappScreen = ExtensionState[15].ToString();
                }

            }            
            

            ////NextState Kontrolu
            if (state.AppSelectedNextstate != "255")
                ChildobjList.Add(GetChildState("AppSelectedNextstate", state.AppSelectedNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.OneAppAvaliableNextState != "255")
                ChildobjList.Add(GetChildState("OneAppAvaliableNextState", state.OneAppAvaliableNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.NoAppAvaliableNextState != "255")
                ChildobjList.Add(GetChildState("NoAppAvaliableNextState", state.NoAppAvaliableNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.TimeoutNextState != "255")
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.CancelNextState != "255")
                ChildobjList.Add(GetChildState("CancelNextState", state.CancelNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));


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