using ATMDesigner.Common;
using ATMDesigner.UI.Model;
using ATMDesigner.UI.Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace ATMDesigner.UI.States
{
    public class StateCAS:StateBase
    {
        public StateCAS(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }


        public StateCAS()
        {
            SetDefaultValues();
        }

        #region Converters

        public class MaskEditor : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"None");
                strings.Add(001,"A");
                strings.Add(002,"B");
                strings.Add(004,"C");
                strings.Add(008,"D");
                strings.Add(016,"F");
                strings.Add(032,"G");
                strings.Add(064,"H");
                strings.Add(128,"I");
                
                return strings;
            }
        }

        //StateCAS için
        public class SetDenominations :ITypeEditor
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
                Denominations DenomPopup = new Denominations(btn.Content.ToString());
                DenomPopup.ShowDialog();
                btn.Content = DenomPopup.Denom;


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
                StateCAS statecas = (StateCAS)SelectedPgrid.SelectedObject;

                Type ClassType = statecas.GetType();
                PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                property.SetValue(statecas, newValue, null);
               designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = statecas;     
            }

        }


        #endregion

        #region state parameters

        private string _CancelKeyMask; 
        private string _DepositKeyMask;
        private string _AddMoreKeyMask;
        private string _RefundKeyMask;             
        private string _Reserved;

        [CategoryAttribute("State Parameters"), PropertyOrder(1), DescriptionAttribute("Cancel Key Mask")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(MaskEditor))]      
        public string CancelKeyMask
        {
            get
            {
                return _CancelKeyMask;
            }
            set
            {
                _CancelKeyMask = value;
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), DescriptionAttribute("Deposit Key Mask")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(MaskEditor))]
        public string DepositKeyMask
        {
            get
            {
                return _DepositKeyMask;
            }
            set
            {
                _DepositKeyMask = value;
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("Add More Key Mask")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(MaskEditor))]
        public string AddMoreKeyMask
        {
            get
            {
                return _AddMoreKeyMask;
            }
            set
            {
                _AddMoreKeyMask = value;
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), DescriptionAttribute("Refund Key Mask")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(MaskEditor))]
        public string RefundKeyMask
        {
            get
            {
                return _RefundKeyMask;
            }
            set
            {
                _RefundKeyMask = value;
            }
        }
            
        [CategoryAttribute("State Parameters"), PropertyOrder(8), DescriptionAttribute("Reserved"), ReadOnlyAttribute(true),Browsable(false)]
        public string Reserved
        {
            get
            {
                return _Reserved;
            }

        }
       
        #endregion

        #region ExtensionStates1
        //Extension 1       
        private string _ExtensionStateNumber1;
        private string _ExtensionDescription1;
        private string _PleaseEnterNotesScreen;
        private string _PleaseRemoveNotesScreen;
        private string _ConfirmationScreen;
        private string _HardwareErrorScreen;
        private string _EscrowFullScreen;
        private string _ProcessingNotesScreen;
        private string _PleaseRemoveMt90NoScreen;
        private string _PleaseWaitScreen1;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]      
        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(7), DescriptionAttribute("Extension State Number1")]
        public string ExtensionStateNumber1
        {
            get
            {
                return _ExtensionStateNumber1;
            }
            set
            {
                _ExtensionStateNumber1 = value;
            }
        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(8), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExtensionState1Type
        {
            get
            {
                return "Z";
            }
        }

        [CategoryAttribute("State Extension 1 Parameters"), DescriptionAttribute("Extension Description")]
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

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(9), DescriptionAttribute("Please Enter Notes Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string PleaseEnterNotesScreen
        {
            get
            {
                return _PleaseEnterNotesScreen;
            }
            set
            {
                _PleaseEnterNotesScreen = value;
            }
        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(10), DescriptionAttribute("Please Remove Notes Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string PleaseRemoveNotesScreen
        {
            get
            {
                return _PleaseRemoveNotesScreen;
            }
            set
            {
                _PleaseRemoveNotesScreen = value;
            }

        }
      
        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(11), DescriptionAttribute("Confirmation Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ConfirmationScreen
        {
            get
            {
                return _ConfirmationScreen;
            }
            set
            {
                _ConfirmationScreen = value;
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(12), DescriptionAttribute("Hardware Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string HardwareErrorScreen
        {
            get
            {
                return _HardwareErrorScreen;
            }
            set
            {
                _HardwareErrorScreen = value;
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(13), DescriptionAttribute("Escrow Full Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string EscrowFullScreen
        {
            get
            {
                return _EscrowFullScreen;
            }
            set
            {
                _EscrowFullScreen = value;
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(14), DescriptionAttribute("Processing Notes Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ProcessingNotesScreen
        {
            get
            {
                return _ProcessingNotesScreen;
            }
            set
            {
                _ProcessingNotesScreen = value;
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(15), DescriptionAttribute("Please Remove Morethan90 Notes Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string PleaseRemoveMt90NoScreen
        {
            get
            {
                return _PleaseRemoveMt90NoScreen;
            }
            set
            {
                _PleaseRemoveMt90NoScreen = value;
            }

        }

        [CategoryAttribute("State Extension 1 Parameters"), PropertyOrder(16), DescriptionAttribute("Please Wait Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string PleaseWaitScreen1
        {
            get
            {
                return _PleaseWaitScreen1;
            }
            set
            {
                _PleaseWaitScreen1 = value;
            }

        }
                
        #endregion

        #region ExtensionStates2

        private string _ExtensionStateNumber2;      
        private string _ExtensionDescription2;
        private string _GoodNextState;
        private string _CancelNextState;
        private string _DeviceErrorNextState;
        private string _TimeoutNextState;
        private string _RefundSlotNextState;
        private string _PleaseWaitScreen2;
        private string _ReservedEx2_1;
        private string _ReservedEx2_2;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(16), DescriptionAttribute("Extension State Number2")]
        public string ExtensionStateNumber2
        {
            get
            {
                return _ExtensionStateNumber2;
            }
            set
            {
                _ExtensionStateNumber2= value;
            }
        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(17), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExtensionState2Type
        {
            get
            {
                return "Z";
            }
        }
      
        [CategoryAttribute("State Extension 2 Parameters"), DescriptionAttribute("Extension Description")]
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

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(18), DescriptionAttribute("Good NextState Number"), ReadOnlyAttribute(true)]
        public string GoodNextState
        {
            get
            {
                return _GoodNextState;
            }
            set
            {
                _GoodNextState = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(19), DescriptionAttribute("Cancel NextState Number"), ReadOnlyAttribute(true)]
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

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(20), DescriptionAttribute("Device Error NextState Number"), ReadOnlyAttribute(true)]
        public string DeviceErrorNextState
        {
            get
            {
                return _DeviceErrorNextState;
            }
            set
            {
                _DeviceErrorNextState = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(21), DescriptionAttribute("Timeout NextState Number"), ReadOnlyAttribute(true)]
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

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(22), DescriptionAttribute("Refund Slot NextState Number"), ReadOnlyAttribute(true)]
        public string RefundSlotNextState
        {
            get
            {
                return _RefundSlotNextState;
            }
            set
            {
                _RefundSlotNextState = value.PadLeft(3, '0'); 
            }
        }
      
        [CategoryAttribute("State Parameters"), PropertyOrder(23), DescriptionAttribute("Reserved"), ReadOnlyAttribute(true)]
        public string ReservedEx2_1
        {
            get
            {
                return _ReservedEx2_1;
            }

        }
      
        [CategoryAttribute("State Parameters"), PropertyOrder(23), DescriptionAttribute("Reserved"), ReadOnlyAttribute(true)]
        public string ReservedEx2_2
        {
            get
            {
                return _ReservedEx2_2;
            }

        }

        [CategoryAttribute("State Extension 2 Parameters"), PropertyOrder(23), DescriptionAttribute("Please Wait Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string PleaseWaitScreen2
        {
            get
            {
                return _PleaseWaitScreen2;
            }
            set
            {
                _PleaseWaitScreen2 = value;
            }
        }
        
        #endregion

        #region ExtensionStates3

        private string _ExtensionStateNumber3; 
        private string _ExtensionDescription3;
        private string _Denominations1_12;
        private string _Denominations13_24;
        private string _Denominations25_36;
        private string _Denominations37_48;
        private string _Denominations49_50;
        private string _ReservedEx3_1;
        private string _RemoveRefusedNScreen;
        private string _CounterfeitNRetainedScreen;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(23), DescriptionAttribute("Extension State Number3")]
        public string ExtensionStateNumber3
        {
            get
            {
                return _ExtensionStateNumber3;
            }
            set
            {
                _ExtensionStateNumber3= value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(24), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]
        public string ExtensionState3Type
        {
            get
            {
                return "Z";
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), DescriptionAttribute("Extension Description")]
        public string ExtensionDescription3
        {
            get
            {
                return _ExtensionDescription3;
            }
            set
            {
                _ExtensionDescription3 = value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(25), DescriptionAttribute("Set Denominations 1_12")]
        [Editor(typeof(SetDenominations), typeof(SetDenominations))]
        public string Denominations1_12
        {
            get
            {
                return _Denominations1_12;
            }
            set
            {
                _Denominations1_12 = value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(25), DescriptionAttribute("Set Denominations 13_24")]
        [Editor(typeof(SetDenominations), typeof(SetDenominations))]
        public string Denominations13_24
        {
            get
            {
                return _Denominations13_24;
            }
            set
            {
                _Denominations13_24 = value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(25), DescriptionAttribute("Set Denominations 25_36")]
        [Editor(typeof(SetDenominations), typeof(SetDenominations))]
        public string Denominations25_36
        {
            get
            {
                return _Denominations25_36;
            }
            set
            {
                _Denominations25_36 = value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(25), DescriptionAttribute("Set Denominations 37_48")]
        [Editor(typeof(SetDenominations), typeof(SetDenominations))]
        public string Denominations37_48
        {
            get
            {
                return _Denominations37_48;
            }
            set
            {
                _Denominations37_48 = value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(25), DescriptionAttribute("Set Denominations 49_50")]
        [Editor(typeof(SetDenominations), typeof(SetDenominations))]
        public string Denominations49_50
        {
            get
            {
                return _Denominations49_50;
            }
            set
            {
                _Denominations49_50 = value;
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(30), DescriptionAttribute("Reserved"), ReadOnlyAttribute(true)]
        public string ReservedEx3_1
        {
            get
            {
                return _ReservedEx3_1;
            }
        }
      
        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(30), DescriptionAttribute("Remove Refused Notes Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string RemoveRefusedNScreen
        {
            get
            {
                return _RemoveRefusedNScreen;
            }
            set
            {
                _RemoveRefusedNScreen = value;
            }
        }

        [CategoryAttribute("State Extension 3 Parameters"), PropertyOrder(31), DescriptionAttribute("Counterfeit Notes Retained Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string CounterfeitNRetainedScreen
        {
            get
            {
                return _CounterfeitNRetainedScreen;
            }
            set
            {
                _CounterfeitNRetainedScreen = value;
            }
        }
 
        #endregion

        #region Events and Methods
              
        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateCAS Selectedstate = new StateCAS();
            StateCAS Dynamicstate = new StateCAS();
            Selectedstate = (StateCAS)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateCAS)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._ExtensionDescription1 = Selectedstate.ExtensionDescription1;
            Dynamicstate._ExtensionDescription2 = Selectedstate.ExtensionDescription2;
            Dynamicstate._ExtensionDescription3 = Selectedstate.ExtensionDescription3;

            Dynamicstate._AddMoreKeyMask = Selectedstate.AddMoreKeyMask;
            Dynamicstate._CancelKeyMask = Selectedstate.CancelKeyMask;
            //Dynamicstate._CancelNextStateNumber = Selectedstate._CancelNextStateNumber;
            Dynamicstate._ConfirmationScreen = Selectedstate.ConfirmationScreen;
            Dynamicstate._CounterfeitNRetainedScreen = Selectedstate.CounterfeitNRetainedScreen;
            Dynamicstate._Denominations1_12 = Selectedstate.Denominations1_12;
            Dynamicstate._Denominations13_24 = Selectedstate.Denominations13_24;
            Dynamicstate._Denominations25_36 = Selectedstate.Denominations25_36;
            Dynamicstate._Denominations37_48 = Selectedstate.Denominations37_48;
            Dynamicstate._Denominations49_50 = Selectedstate.Denominations49_50;
            Dynamicstate._DepositKeyMask = Selectedstate.DepositKeyMask;
            //Dynamicstate._DeviceErrorNextStateNumber = Selectedstate._DeviceErrorNextStateNumber;
            Dynamicstate._EscrowFullScreen = Selectedstate.EscrowFullScreen;
            Dynamicstate._ExtensionStateNumber1 = Selectedstate.ExtensionStateNumber1;
            Dynamicstate._ExtensionStateNumber2 = Selectedstate.ExtensionStateNumber2;
            Dynamicstate._ExtensionStateNumber3 = Selectedstate.ExtensionStateNumber3;
            //Dynamicstate._GoodNextStateNumber = Selectedstate._GoodNextStateNumber;
            Dynamicstate._HardwareErrorScreen = Selectedstate.HardwareErrorScreen;
            Dynamicstate._PleaseEnterNotesScreen = Selectedstate.PleaseEnterNotesScreen;
            Dynamicstate._PleaseRemoveMt90NoScreen = Selectedstate.PleaseRemoveMt90NoScreen;
            Dynamicstate._PleaseRemoveNotesScreen = Selectedstate.PleaseRemoveNotesScreen;
            Dynamicstate._PleaseWaitScreen1 = Selectedstate.PleaseWaitScreen1;
            Dynamicstate._PleaseWaitScreen2 = Selectedstate.PleaseWaitScreen2;
            Dynamicstate._ProcessingNotesScreen = Selectedstate.ProcessingNotesScreen;
            Dynamicstate._RefundKeyMask = Selectedstate.RefundKeyMask;
            //Dynamicstate._RefundSlotNextStateNumber = Selectedstate._RefundSlotNextStateNumber;
            Dynamicstate._RemoveRefusedNScreen = Selectedstate.RemoveRefusedNScreen;
            //Dynamicstate._TimeoutNextStateNumber = Selectedstate._TimeoutNextStateN

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateCAS Selectedstate = new StateCAS();
            StateCAS Dynamicstate = new StateCAS();

            Selectedstate = (StateCAS)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateCAS)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate._ExtensionDescription1 = Selectedstate.ExtensionDescription1;
            Dynamicstate._ExtensionDescription2 = Selectedstate.ExtensionDescription2;
            Dynamicstate._ExtensionDescription3 = Selectedstate.ExtensionDescription3;

            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._AddMoreKeyMask = Selectedstate.AddMoreKeyMask;
            Dynamicstate._CancelKeyMask = Selectedstate.CancelKeyMask;            
          //Dynamicstate._CancelNextStateNumber = Selectedstate._CancelNextStateNumber;
            Dynamicstate._ConfirmationScreen = Selectedstate.ConfirmationScreen;
            Dynamicstate._CounterfeitNRetainedScreen = Selectedstate.CounterfeitNRetainedScreen;
            Dynamicstate._Denominations1_12 = Selectedstate.Denominations1_12;
            Dynamicstate._Denominations13_24 = Selectedstate.Denominations13_24;
            Dynamicstate._Denominations25_36 = Selectedstate.Denominations25_36;
            Dynamicstate._Denominations37_48 = Selectedstate.Denominations37_48;
            Dynamicstate._Denominations49_50 = Selectedstate.Denominations49_50;
            Dynamicstate._DepositKeyMask = Selectedstate.DepositKeyMask;
            //Dynamicstate._DeviceErrorNextStateNumber = Selectedstate._DeviceErrorNextStateNumber;
            Dynamicstate._EscrowFullScreen = Selectedstate.EscrowFullScreen;
            Dynamicstate._ExtensionStateNumber1 = Selectedstate.ExtensionStateNumber1;
            Dynamicstate._ExtensionStateNumber2 = Selectedstate.ExtensionStateNumber2;
            Dynamicstate._ExtensionStateNumber3 = Selectedstate.ExtensionStateNumber3;
            //Dynamicstate._GoodNextStateNumber = Selectedstate._GoodNextStateNumber;
            Dynamicstate._HardwareErrorScreen = Selectedstate.HardwareErrorScreen;
            Dynamicstate._PleaseEnterNotesScreen = Selectedstate.PleaseEnterNotesScreen;
            Dynamicstate._PleaseRemoveMt90NoScreen = Selectedstate.PleaseRemoveMt90NoScreen;
            Dynamicstate._PleaseRemoveNotesScreen = Selectedstate.PleaseRemoveNotesScreen;
            Dynamicstate._PleaseWaitScreen1 = Selectedstate.PleaseWaitScreen1;
            Dynamicstate._PleaseWaitScreen2 = Selectedstate.PleaseWaitScreen2;
            Dynamicstate._ProcessingNotesScreen = Selectedstate.ProcessingNotesScreen;
            Dynamicstate._RefundKeyMask = Selectedstate.RefundKeyMask;
            //Dynamicstate._RefundSlotNextStateNumber = Selectedstate._RefundSlotNextStateNumber;
            Dynamicstate._RemoveRefusedNScreen = Selectedstate.RemoveRefusedNScreen;
            //Dynamicstate._TimeoutNextStateNumber = Selectedstate._TimeoutNextStateNumber;       

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateCAS State = new StateCAS();
            State = (StateCAS)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            string ex1sql = sql;
            string ex2sql = sql;
            string ex3sql = sql;

            //Extension1


            if (State.ExtensionStateNumber1 != "255")
            {
                ex1sql = string.Format(ex1sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber1, State.ExtensionDescription1,
                 State.ExtensionState1Type, ProjectName, TransactionName, State.PleaseEnterNotesScreen, State.PleaseRemoveNotesScreen, State.ConfirmationScreen,
                 State.HardwareErrorScreen, State.EscrowFullScreen, State.ProcessingNotesScreen, State.PleaseRemoveMt90NoScreen, State.PleaseWaitScreen1,
                 State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex1sql);
            }


            //Extension2

            if (State.ExtensionStateNumber2 != "255")
            {

                ex2sql = string.Format(ex2sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber2, State.ExtensionDescription2,
                State.ExtensionState2Type, ProjectName, TransactionName, State.GoodNextState, State.CancelNextState, State.DeviceErrorNextState,
                State.TimeoutNextState, State.RefundSlotNextState, State.ReservedEx2_1, State.ReservedEx2_2, State.PleaseWaitScreen2,
                State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex3sql);
            }


            //Extension3
            if (State.ExtensionStateNumber3 != "255")
            {
                ex3sql = string.Format(ex3sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber3, State.ExtensionDescription3,
                State.ExtensionState3Type, ProjectName, TransactionName, State.Denominations1_12, State.Denominations13_24, State.Denominations25_36,
                State.Denominations37_48, State.Denominations49_50, State.ReservedEx3_1, State.RemoveRefusedNScreen, State.CounterfeitNRetainedScreen,
                State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(ex3sql);
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.CancelKeyMask, State.DepositKeyMask, State.AddMoreKeyMask, State.RefundKeyMask,
                State.ExtensionStateNumber1, State.ExtensionStateNumber2, State.ExtensionStateNumber3, State.Reserved,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }
        
        void SetDefaultValues()
        {
            StateType = ">";
            StateDescription = "Cash Accept State";
            _ExtensionDescription1 = "State Z";
            _ExtensionDescription2 = "State Z";
            _ExtensionDescription3 = "State Z";

            _Reserved = "000";
            _CancelNextState = "255";
            _DeviceErrorNextState = "255";
            _GoodNextState = "255";
            _RefundSlotNextState = "255";
            _TimeoutNextState = "255";
            _ExtensionStateNumber1 = "255";
            _ExtensionStateNumber2 = "255";
            _ExtensionStateNumber3 = "255";

            _Denominations1_12 = "000";
            _Denominations13_24 = "000";
            _Denominations25_36 = "000";
            _Denominations37_48 = "000";
            _Denominations49_50 = "000";

            _RemoveRefusedNScreen = "000";
            _CounterfeitNRetainedScreen = "000";
            _PleaseEnterNotesScreen = "000";
            _PleaseRemoveNotesScreen = "000";
            _ConfirmationScreen = "000";
            _HardwareErrorScreen = "000";
            _EscrowFullScreen = "000";
            _ProcessingNotesScreen = "000";
            _PleaseRemoveMt90NoScreen = "000";
            _PleaseWaitScreen1 = "000";
            _PleaseWaitScreen2 = "000";

            _CancelKeyMask = "000";
            _DepositKeyMask = "000";
            _AddMoreKeyMask = "000";
            _RefundKeyMask = "000";

            _ReservedEx3_1 = "000";
            _ReservedEx2_2 = "000";
            _ReservedEx2_1 = "000";
           
        }


        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateCAS state = new StateCAS();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._CancelKeyMask = processRow[8].ToString();
            state._DepositKeyMask = processRow[9].ToString();
            state._AddMoreKeyMask = processRow[10].ToString();
            state._RefundKeyMask = processRow[11].ToString();
            state._ExtensionStateNumber1 = processRow[12].ToString();
            state._ExtensionStateNumber2 = processRow[13].ToString();
            state._ExtensionStateNumber3 = processRow[14].ToString();
            state._Reserved = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu-1
            if (state.ExtensionStateNumber1!="255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber1);
                state._ExtensionDescription1 = ExtensionState[4].ToString();
                state._PleaseEnterNotesScreen = ExtensionState[8].ToString();
                state._PleaseRemoveNotesScreen = ExtensionState[9].ToString();
                state._ConfirmationScreen = ExtensionState[10].ToString();
                state._HardwareErrorScreen = ExtensionState[11].ToString();
                state._EscrowFullScreen = ExtensionState[12].ToString();
                state._ProcessingNotesScreen = ExtensionState[13].ToString();
                state._PleaseRemoveMt90NoScreen = ExtensionState[14].ToString();
                state._PleaseWaitScreen1 = ExtensionState[15].ToString();       
            }
            //Extension State Kontrolu-2
            if (state.ExtensionStateNumber2 != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber2);
             
                state._ExtensionDescription2= ExtensionState[4].ToString();
                state._GoodNextState = ExtensionState[8].ToString();
                state._CancelNextState = ExtensionState[9].ToString();
                state._DeviceErrorNextState = ExtensionState[10].ToString();
                state._TimeoutNextState = ExtensionState[11].ToString();
                state._RefundSlotNextState = ExtensionState[12].ToString();
                state._ReservedEx2_1 = ExtensionState[13].ToString();
                state._ReservedEx2_2 = ExtensionState[14].ToString();
                state._PleaseWaitScreen2 = ExtensionState[15].ToString();
            }
            //Extension State Kontrolu-3
            if (state.ExtensionStateNumber3 != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber3);

                state._ExtensionDescription3 = ExtensionState[4].ToString();             
                state._Denominations1_12 = ExtensionState[8].ToString();
                state._Denominations13_24 = ExtensionState[9].ToString();
                state._Denominations25_36 = ExtensionState[10].ToString();
                state._Denominations37_48 = ExtensionState[11].ToString();
                state._Denominations49_50 = ExtensionState[12].ToString();
                state._ReservedEx3_1 = ExtensionState[13].ToString();
                state._RemoveRefusedNScreen = ExtensionState[14].ToString();
                state._CounterfeitNRetainedScreen = ExtensionState[15].ToString();
            }


            //NextState Kontrolu
            if (state.GoodNextState != "255")
            {
                ChildobjList.Add(GetChildState("GoodNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.CancelNextState != "255")
            {
                ChildobjList.Add(GetChildState("CancelNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.DeviceErrorNextState != "255")
            {
                ChildobjList.Add(GetChildState("DeviceErrorNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            } 
            if (state.TimeoutNextState != "255")
            {
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.RefundSlotNextState != "255")
            {
                ChildobjList.Add(GetChildState("RefundSlotNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
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
