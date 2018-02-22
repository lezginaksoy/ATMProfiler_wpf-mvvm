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
    
    //. Begin ICC Application Selection & Initialisation State
    public class StateDot: StateBase
    {

        public StateDot(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateDot()
        {
            SetDefaultValues();
        }    


        #region Converters

        public class FDKListConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"None");
                strings.Add(001,"FDK A");
                strings.Add(002,"FDK B");
                strings.Add(003,"FDK C");
                strings.Add(004,"FDK D");
                strings.Add(005,"FDK F");
                strings.Add(006,"FDK G");
                strings.Add(007,"FDK H");
                strings.Add(008,"FDK I");
               return strings;
            }
        }
        
        #endregion

        #region state parameters

        private string _CardholderSelectScreenNumber;      
        private string _Reserved;

        [Category("State Parameters"), PropertyOrder(1), DescriptionAttribute("Cardholder Selection Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string CardholderSelectScreenNumber
        {
            get
            {
                return _CardholderSelectScreenNumber;
            }
            set
            {
                _CardholderSelectScreenNumber = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true),DescriptionAttribute("Reserved")]
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
        
        #region Screen Numbers Extension States 
        
        public string _FDKTemplateScreenExtStateNumber;
        //Tüm Extensionlar için ortak
        private string _ScreenExtensionType;
        private string _ScreenExtensionDescription;
        private string _FDKAICCAppScreenNumber;
        private string _FDKBICCAppScreenNumber;
        private string _FDKCICCAppScreenNumber;
        private string _FDKDICCAppScreenNumber;
        private string _FDKFICCAppScreenNumber;
        private string _FDKGICCAppScreenNumber;
        private string _FDKHICCAppScreenNumber;
        private string _FDKIICCAppScreenNumber;
        
        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("Screen Extension States"), PropertyOrder(2), DescriptionAttribute("Extension State Number")]
        public string FDKTemplateScreenExtStateNumber
        {
            get
            {
                return _FDKTemplateScreenExtStateNumber;
            }
            set
            {
                _FDKTemplateScreenExtStateNumber = value.PadLeft(3, '0');
            }

        }
       
        [CategoryAttribute("Screen Extension States"), PropertyOrder(3), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ScreenExtensionType
        {
            get { return _ScreenExtensionType; }
        }
               
        [CategoryAttribute("Screen Extension States"), PropertyOrder(4), DescriptionAttribute("Extension Description")]
        public string ScreenExtensionDescription
        {
            get
            {
                return _ScreenExtensionDescription;
            }
            set
            {
                _ScreenExtensionDescription = value;
            }
        }

        [Category("Screen Extension States"), PropertyOrder(5), DescriptionAttribute("FDK A ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKAICCAppScreenNumber
        {
            get
            {
                return _FDKAICCAppScreenNumber;
            }
            set
            {
                _FDKAICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("Screen Extension States"), PropertyOrder(6), DescriptionAttribute("FDK B ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKBICCAppScreenNumber
        {
            get
            {
                return _FDKBICCAppScreenNumber;
            }
            set
            {
                _FDKBICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("Screen Extension States"), PropertyOrder(7), DescriptionAttribute("FDK C ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKCICCAppScreenNumber
        {
            get
            {
                return _FDKCICCAppScreenNumber;
            }
            set
            {
                _FDKCICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }
    
        [Category("Screen Extension States"), PropertyOrder(8), DescriptionAttribute("FDK D ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKDICCAppScreenNumber
        {
            get
            {
                return _FDKDICCAppScreenNumber;
            }
            set
            {
                _FDKDICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("Screen Extension States"), PropertyOrder(9), DescriptionAttribute("FDK F ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKFICCAppScreenNumber
        {
            get
            {
                return _FDKFICCAppScreenNumber;
            }
            set
            {
                _FDKFICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("Screen Extension States"), PropertyOrder(10), DescriptionAttribute("FDK G ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKGICCAppScreenNumber
        {
            get
            {
                return _FDKGICCAppScreenNumber;
            }
            set
            {
                _FDKGICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("Screen Extension States"), PropertyOrder(11), DescriptionAttribute("FDK H ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKHICCAppScreenNumber
        {
            get
            {
                return _FDKHICCAppScreenNumber;
            }
            set
            {
                _FDKHICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("Screen Extension States"), PropertyOrder(12), DescriptionAttribute("FDK I ICC App.Name Template Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKIICCAppScreenNumber
        {
            get
            {
                return _FDKIICCAppScreenNumber;
            }
            set
            {
                _FDKIICCAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        #endregion

        #region Action Keys Extension State
      
        private string _ActionKeysExtStateNumber;
        private string _ActionKeyExtensionType;
        private string _ActionKeyExtensionDescription;
        private string _MoreAppScreenNumber;
        private string _FDKForMoreApps;
        private string _BackToStartScreenNumber;
        private string _FDKForBackToStartofList;
        private string _ActionKeysReserved; 
             
        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("ActionKeys Extension States"), PropertyOrder(12), DescriptionAttribute("Extension State Number")]      
        public string ActionKeysExtStateNumber
        {
            get
            {
                return _ActionKeysExtStateNumber;
            }
            set
            {
                _ActionKeysExtStateNumber = value.PadLeft(3, '0');
            }

        }

        [CategoryAttribute("ActionKeys Extension States"), PropertyOrder(13), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ActionKeyExtensionType
        {
            get
            {
                return _ActionKeyExtensionType;
            }
            set
            {
                _ActionKeyExtensionType = value;
            }
        }

        [CategoryAttribute("ActionKeys Extension States"), PropertyOrder(14), DescriptionAttribute("Extension Description")]
        public string ActionKeyExtensionDescription
        {
            get
            {
                return _ActionKeyExtensionDescription;
            }
            set
            {
                _ActionKeyExtensionDescription = value;
            }
        }

        [Category("ActionKeys Extension States"), PropertyOrder(15), DescriptionAttribute("More Applications Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string MoreAppScreenNumber
        {
            get
            {
                return _MoreAppScreenNumber;
            }
            set
            {
                _MoreAppScreenNumber = value.PadLeft(3, '0');
            }
        }

        [Category("ActionKeys Extension States"), PropertyOrder(16), DescriptionAttribute("FDK For More Apps")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(FDKListConverter))]
        public string FDKForMoreApps
        {
            get
            {
                return _FDKForMoreApps;
            }
            set
            {
                _FDKForMoreApps = value.PadLeft(3, '0'); 
            }
        }

        [Category("ActionKeys Extension States"), PropertyOrder(16), DescriptionAttribute("Back To Start of list Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string BackToStartScreenNumber
        {
            get
            {
                return _BackToStartScreenNumber;
            }
            set
            {
                _BackToStartScreenNumber = value.PadLeft(3, '0'); 
            }
        }

        [Category("ActionKeys Extension States"), PropertyOrder(17), DescriptionAttribute("FDK For Back To Start of List")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(FDKListConverter))]
        public string FDKForBackToStartofList
        {
            get
            {
                return _FDKForBackToStartofList;
            }
            set
            {
                _FDKForBackToStartofList = value.PadLeft(3, '0'); 
            }
        }

        [Browsable(false)]
        [CategoryAttribute("ActionKeys Extension States"), DescriptionAttribute("Reserved")]
        public string ActionKeysReserved
        {
            get
            {
                return _ActionKeysReserved;
            }
            set
            {
                _ActionKeysReserved = value.PadLeft(3, '0'); 
            }
        }


        #endregion
        
        #region Exit Paths Extension State
       
        private string _ExitPathsExtStateNumber;
        private string _ExitPathsKeyExtensionType;
        private string _ExitPathExtensionDescription;
        private string _TimeOutNextState;
        private string _CancelKeyNextState;
        private string _CardholderAppNextState;
        private string _AppAutomaticallyNextState;
        private string _NoUsableAppNextState;
        private string _ExitPathsReserved; 

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("ExitPaths Extension States"), PropertyOrder(17), DescriptionAttribute("Extension State Number")]      
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

        [CategoryAttribute("ExitPaths Extension States"), PropertyOrder(18), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExitPathsKeyExtensionType
        {
            get
            {
                return _ExitPathsKeyExtensionType;
            }
            set
            {
                _ExitPathsKeyExtensionType = value;
            }
        }

        [CategoryAttribute("ExitPaths Extension States"), PropertyOrder(19), DescriptionAttribute("Extension Description")]
        public string ExitPathExtensionDescription
        {
            get
            {
                return _ExitPathExtensionDescription;
            }
            set
            {
                _ExitPathExtensionDescription = value;
            }
        }

        [Category("ExitPaths Extension States"), PropertyOrder(21),ReadOnly(true), DescriptionAttribute("TimeOut Next State Number")]
        public string TimeOutNextState
        {
            get
            {
                return _TimeOutNextState;
            }
            set
            {
                _TimeOutNextState = value.PadLeft(3, '0');
            }
        }
        
        [Category("ExitPaths Extension States"), PropertyOrder(22), ReadOnly(true), DescriptionAttribute("CancelKey Next State Number")]
        public string CancelKeyNextState
        {
            get
            {
                return _CancelKeyNextState;
            }
            set
            {
                _CancelKeyNextState = value.PadLeft(3, '0');
            }
        }
        
        [Category("ExitPaths Extension States"), PropertyOrder(23), ReadOnly(true), DescriptionAttribute("Cardholder Applications Next State Number")]
        public string CardholderAppNextState
        {
            get
            {
                return _CardholderAppNextState;
            }
            set
            {
                _CardholderAppNextState = value.PadLeft(3, '0');
            }
        }

        [Category("ExitPaths Extension States"), PropertyOrder(24), ReadOnly(true), DescriptionAttribute("Applications Automatically Next State Number")]
        public string AppAutomaticallyNextState
        {
            get
            {
                return _AppAutomaticallyNextState;
            }
            set
            {
                _AppAutomaticallyNextState = value.PadLeft(3, '0');
            }
        }

        [Category("ExitPaths Extension States"), PropertyOrder(25), ReadOnly(true), DescriptionAttribute("No Usable Applications Next State Number")]
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

        [Browsable(false)]
        [CategoryAttribute("ActionKeys Extension States"), DescriptionAttribute("Reserved")]
        public string ExitPathsReserved
        {
            get
            {
                return _ExitPathsReserved;
            }
            set
            {
                _ExitPathsReserved = value.PadLeft(3, '0'); 
            }
        }


        #endregion

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            StateDot Selectedstate = new StateDot();
            StateDot Dynamicstate = new StateDot();
            Selectedstate = (StateDot)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateDot)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._CardholderSelectScreenNumber = Selectedstate._CardholderSelectScreenNumber;
            Dynamicstate._Reserved = Selectedstate._Reserved;

            //extension screen
            Dynamicstate._FDKTemplateScreenExtStateNumber = Selectedstate._FDKTemplateScreenExtStateNumber;
            Dynamicstate._ScreenExtensionType = Selectedstate._ScreenExtensionType;
            Dynamicstate._ScreenExtensionDescription = Selectedstate._ScreenExtensionDescription;
            Dynamicstate._FDKAICCAppScreenNumber = Selectedstate._FDKAICCAppScreenNumber;
            Dynamicstate._FDKBICCAppScreenNumber = Selectedstate._FDKBICCAppScreenNumber;
            Dynamicstate._FDKCICCAppScreenNumber = Selectedstate._FDKCICCAppScreenNumber;
            Dynamicstate._FDKDICCAppScreenNumber = Selectedstate._FDKDICCAppScreenNumber;
            Dynamicstate._FDKFICCAppScreenNumber = Selectedstate._FDKFICCAppScreenNumber;
            Dynamicstate._FDKGICCAppScreenNumber = Selectedstate._FDKGICCAppScreenNumber;
            Dynamicstate._FDKHICCAppScreenNumber = Selectedstate._FDKHICCAppScreenNumber;
            Dynamicstate._FDKIICCAppScreenNumber = Selectedstate._FDKIICCAppScreenNumber;

            //Extension Action keys
            Dynamicstate._ActionKeysExtStateNumber = Selectedstate._ActionKeysExtStateNumber;
            Dynamicstate._ActionKeyExtensionType = Selectedstate._ActionKeyExtensionType;
            Dynamicstate._ActionKeyExtensionDescription = Selectedstate._ActionKeyExtensionDescription;
            Dynamicstate._MoreAppScreenNumber = Selectedstate._MoreAppScreenNumber;
            Dynamicstate._FDKForMoreApps = Selectedstate._FDKForMoreApps;
            Dynamicstate._BackToStartScreenNumber = Selectedstate._BackToStartScreenNumber;
            Dynamicstate._FDKForBackToStartofList = Selectedstate._FDKForBackToStartofList;

            //Extension Exit Paths
            Dynamicstate._ExitPathsExtStateNumber = Selectedstate._ExitPathsExtStateNumber;
            Dynamicstate.ExitPathsKeyExtensionType = Selectedstate.ExitPathsKeyExtensionType;
            Dynamicstate._ExitPathExtensionDescription = Selectedstate._ExitPathExtensionDescription;
            Dynamicstate._TimeOutNextState = Selectedstate._TimeOutNextState;
            Dynamicstate._CancelKeyNextState = Selectedstate._CancelKeyNextState;
            Dynamicstate._CardholderAppNextState = Selectedstate._CardholderAppNextState;
            Dynamicstate._AppAutomaticallyNextState = Selectedstate._AppAutomaticallyNextState;
            Dynamicstate._NoUsableAppNextState = Selectedstate._NoUsableAppNextState;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateDot Selectedstate = new StateDot();
            StateDot Dynamicstate = new StateDot();
            Selectedstate = (StateDot)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateDot)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._CardholderSelectScreenNumber = Selectedstate._CardholderSelectScreenNumber;
            Dynamicstate._Reserved = Selectedstate._Reserved;

            //extension screen
            Dynamicstate._FDKTemplateScreenExtStateNumber = Selectedstate._FDKTemplateScreenExtStateNumber;
            Dynamicstate._ScreenExtensionType = Selectedstate._ScreenExtensionType;
            Dynamicstate._ScreenExtensionDescription = Selectedstate._ScreenExtensionDescription;
            Dynamicstate._FDKAICCAppScreenNumber = Selectedstate._FDKAICCAppScreenNumber;
            Dynamicstate._FDKBICCAppScreenNumber = Selectedstate._FDKBICCAppScreenNumber;
            Dynamicstate._FDKCICCAppScreenNumber = Selectedstate._FDKCICCAppScreenNumber;
            Dynamicstate._FDKDICCAppScreenNumber = Selectedstate._FDKDICCAppScreenNumber;
            Dynamicstate._FDKFICCAppScreenNumber = Selectedstate._FDKFICCAppScreenNumber;
            Dynamicstate._FDKGICCAppScreenNumber = Selectedstate._FDKGICCAppScreenNumber;
            Dynamicstate._FDKHICCAppScreenNumber = Selectedstate._FDKHICCAppScreenNumber;
            Dynamicstate._FDKIICCAppScreenNumber = Selectedstate._FDKIICCAppScreenNumber;

            //Extension Action keys
            Dynamicstate._ActionKeysExtStateNumber = Selectedstate._ActionKeysExtStateNumber;
            Dynamicstate._ActionKeyExtensionType = Selectedstate._ActionKeyExtensionType;
            Dynamicstate._ActionKeyExtensionDescription = Selectedstate._ActionKeyExtensionDescription;
            Dynamicstate._MoreAppScreenNumber = Selectedstate._MoreAppScreenNumber;
            Dynamicstate._FDKForMoreApps = Selectedstate._FDKForMoreApps;
            Dynamicstate._BackToStartScreenNumber = Selectedstate._BackToStartScreenNumber;
            Dynamicstate._FDKForBackToStartofList = Selectedstate._FDKForBackToStartofList;

            //Extension Exit Paths
            Dynamicstate._ExitPathsExtStateNumber = Selectedstate._ExitPathsExtStateNumber;
            Dynamicstate.ExitPathsKeyExtensionType = Selectedstate.ExitPathsKeyExtensionType;
            //Dynamicstate._ExitPathExtensionDescription = Selectedstate._ExitPathExtensionDescription;
            //Dynamicstate._TimeOutNextState = Selectedstate._TimeOutNextState;
            //Dynamicstate._CancelKeyNextState = Selectedstate._CancelKeyNextState;
            //Dynamicstate._CardholderAppNextState = Selectedstate._CardholderAppNextState;
            //Dynamicstate._AppAutomaticallyNextState = Selectedstate._AppAutomaticallyNextState;
            //Dynamicstate._NoUsableAppNextState = Selectedstate._NoUsableAppNextState;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateDot State = new StateDot();
            State = (StateDot)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;
            string ex1sql = sql;
            string ex2sql = sql;
            string ex3sql = sql;

                //Screen Extension                
                if (State.FDKTemplateScreenExtStateNumber != "255")
                {
                    ex1sql = string.Format(ex1sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.FDKTemplateScreenExtStateNumber,
                        State.ScreenExtensionDescription, State.ScreenExtensionType, ProjectName, TransactionName, State.FDKAICCAppScreenNumber,
                        State.FDKBICCAppScreenNumber, State.FDKCICCAppScreenNumber, State.FDKDICCAppScreenNumber, State.FDKFICCAppScreenNumber,
                        State.FDKGICCAppScreenNumber, State.FDKHICCAppScreenNumber, State.FDKIICCAppScreenNumber,
                     State.ConfigId, State.BrandId, State.ConfigVersion);
                    SqlStringList.Add(ex1sql);
                }

                //action keys Extension 
                if (State.ActionKeysExtStateNumber != "255")
                {
                    ex2sql = string.Format(ex2sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ActionKeysExtStateNumber,
                        State._ActionKeyExtensionDescription, State._ActionKeyExtensionType, ProjectName, TransactionName, State.MoreAppScreenNumber,
                        State.FDKForMoreApps, State.BackToStartScreenNumber, State.FDKForBackToStartofList, State.ActionKeysReserved,
                        State.ActionKeysReserved, State.ActionKeysReserved, State.ActionKeysReserved,
                        State.ConfigId, State.BrandId, State.ConfigVersion);
                    SqlStringList.Add(ex2sql);
                }


                //Exit Paths Extension    
                if (State.ExitPathsExtStateNumber != "255")
                {
                    ex3sql = string.Format(ex3sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExitPathsExtStateNumber,
                        State.ExitPathExtensionDescription, State.ExitPathsKeyExtensionType, ProjectName, TransactionName,
                        State.TimeOutNextState, State.CancelKeyNextState, State.CardholderAppNextState,
                    State.AppAutomaticallyNextState, State.NoUsableAppNextState, State.ExitPathsReserved, State.ExitPathsReserved,
                    State.ExitPathsReserved, State.ConfigId, State.BrandId, State.ConfigVersion);
                    SqlStringList.Add(ex3sql);
                }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.CardholderSelectScreenNumber, State.FDKTemplateScreenExtStateNumber,
                State.ActionKeysExtStateNumber, State.ExitPathsExtStateNumber, State.Reserved, State.Reserved, State.Reserved, State.Reserved,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = ".";
            StateDescription = "Begin ICC App. Select. & Init. State";
            _CardholderSelectScreenNumber = "000";
            _Reserved = "000";

            _FDKTemplateScreenExtStateNumber = "255";
            _ScreenExtensionType = "Z";
            _ScreenExtensionDescription = "State Z";
            _FDKAICCAppScreenNumber = "000";
            _FDKBICCAppScreenNumber = "000";
            _FDKCICCAppScreenNumber = "000";
            _FDKDICCAppScreenNumber = "000";
            _FDKFICCAppScreenNumber = "000";
            _FDKGICCAppScreenNumber = "000";
            _FDKHICCAppScreenNumber = "000";
            _FDKIICCAppScreenNumber = "000";

            _ActionKeysExtStateNumber = "255";            
            _ActionKeyExtensionType = "Z";
            _ActionKeyExtensionDescription = "State Z";
            _MoreAppScreenNumber = "000";
            _FDKForMoreApps = "000";
            _BackToStartScreenNumber = "000";
            _FDKForBackToStartofList = "000";
            _ActionKeysReserved = "000";

            _ExitPathsExtStateNumber = "255";
            _ExitPathsKeyExtensionType = "Z";
            _ExitPathExtensionDescription = "State Z";
            _TimeOutNextState = "255";
            _CancelKeyNextState = "255";
            _CardholderAppNextState = "255";
            _AppAutomaticallyNextState = "255";
            _NoUsableAppNextState = "255";
            _ExitPathsReserved = "000";
        }


        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateDot state = new StateDot();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._CardholderSelectScreenNumber = processRow[8].ToString();
            state._FDKTemplateScreenExtStateNumber = processRow[9].ToString();
            state._ActionKeysExtStateNumber = processRow[10].ToString();
            state._ExitPathsExtStateNumber = processRow[11].ToString();
            state.Reserved = processRow[12].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu Screen
            if (state.FDKTemplateScreenExtStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.FDKTemplateScreenExtStateNumber);
                state._ScreenExtensionDescription = ExtensionState[4].ToString();
                state._FDKAICCAppScreenNumber = ExtensionState[8].ToString();
                state._FDKBICCAppScreenNumber = ExtensionState[9].ToString();
                state._FDKCICCAppScreenNumber = ExtensionState[10].ToString();
                state._FDKDICCAppScreenNumber = ExtensionState[11].ToString();
                state._FDKFICCAppScreenNumber = ExtensionState[12].ToString();
                state._FDKGICCAppScreenNumber = ExtensionState[13].ToString();
                state._FDKHICCAppScreenNumber = ExtensionState[14].ToString();
                state._FDKIICCAppScreenNumber = ExtensionState[15].ToString();
            }
            //Extension State Kontrolu Action Keys
            if (state.ActionKeysExtStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ActionKeysExtStateNumber);
                state._ActionKeyExtensionDescription = ExtensionState[4].ToString();
                state._MoreAppScreenNumber = ExtensionState[8].ToString();
                state._FDKForMoreApps = ExtensionState[9].ToString();
                state._BackToStartScreenNumber = ExtensionState[10].ToString();
                state._FDKForBackToStartofList = ExtensionState[11].ToString();
                state._ActionKeysReserved = ExtensionState[12].ToString();
            }

            //Extension State Kontrolu Exit
            if (state.ExitPathsExtStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExitPathsExtStateNumber);
                state.ExitPathExtensionDescription = ExtensionState[4].ToString();
                state.TimeOutNextState = ExtensionState[8].ToString();
                state.CancelKeyNextState = ExtensionState[9].ToString();
                state.CardholderAppNextState = ExtensionState[10].ToString();
                state.AppAutomaticallyNextState = ExtensionState[11].ToString();
                state.NoUsableAppNextState = ExtensionState[12].ToString();
                state.ExitPathsReserved = ExtensionState[13].ToString();                

                //NextState Kontrolu
                if (state.TimeOutNextState != "255")
                {
                    ChildobjList.Add(GetChildState("TimeOutNextState", state.TimeOutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                }
                if (state.CancelKeyNextState != "255")
                {
                    ChildobjList.Add(GetChildState("CancelKeyNextState", state.CancelKeyNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                }
                if (state.CardholderAppNextState != "255")
                {
                    ChildobjList.Add(GetChildState("CardholderAppNextState", state.CardholderAppNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                }
                if (state.AppAutomaticallyNextState != "255")
                {
                    ChildobjList.Add(GetChildState("AppAutomaticallyNextState", state.AppAutomaticallyNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                }
                if (state.NoUsableAppNextState != "255")
                {
                    ChildobjList.Add(GetChildState("NoUsableAppNextState", state.NoUsableAppNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
                }
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