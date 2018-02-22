using ATMDesigner.Common;
using ATMDesigner.UI.Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
 
    public class StateY : StateBase
    {

        public StateY(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public StateY()
        {
            SetDefaultData();
        }

        #region Converters
        
        public class IsMultiLanguageConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(1,"Yes");
                strings.Add(0,"No");               
                return strings;
            }
        }

        public class IsExtensionStateConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(1, "Yes");
                strings.Add(0, "No");
                return strings;
            }
        }

        public class BufferPositionsWithOutExtConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add("000");
                strings.Add("001");
                strings.Add("002");
                strings.Add("003");               
                strings.Add("004");
                strings.Add("005");
                strings.Add("006");
                strings.Add("007");
                return strings;
            }
        }

        public class BufferPositionsWithExtConverter : ITypeEditor
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

                BufferPositions bufferPopup = new BufferPositions(btn.Content.ToString());
                bufferPopup.ShowDialog();
                btn.Content = bufferPopup.BufferPositionValue;


                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }
                DockPanel Sourcepnl = (DockPanel)Ditem.Content;
                PropertyGrid SelectedPgrid = designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == Sourcepnl.Uid).PropertyGrid;
                string SelectedProperty = btn.Name.ToString();
                string newValue = btn.Content.ToString();
                StateY statey = (StateY)SelectedPgrid.SelectedObject;

                Type ClassType = statey.GetType();
                PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                property.SetValue(statey, newValue, null);
                designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = statey;
            }
        }

        public class ExtensionFDKActiveConverter : ITypeEditor
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

                SelectFDK fdkpopup = new SelectFDK(btn.Content.ToString());
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
                StateY statey = (StateY)SelectedPgrid.SelectedObject;

                Type ClassType = statey.GetType();
                PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                property.SetValue(statey, newValue, null);
                designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = statey;
            }
        }

        public class AssignmentFDKMaskEditor : ITypeEditor
        {
            Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem property;
            public FrameworkElement ResolveEditor(PropertyItem propertyItem)
            {
                property = propertyItem;
                Xceed.Wpf.Toolkit.CheckComboBox ccb = new Xceed.Wpf.Toolkit.CheckComboBox();

                for (int i = 0; i < 8; i++)
                {
                    ccb.Items.Add("OpCode" + i.ToString());
                }
                ccb.Name = property.DisplayName;
                ccb.Text = property.Value.ToString();
                ccb.ItemSelectionChanged += ccb_ItemSelectionChanged;
                return ccb;
            }

            void ccb_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
            {
                Xceed.Wpf.Toolkit.CheckComboBox ccb = (Xceed.Wpf.Toolkit.CheckComboBox)sender;
                int mask = 0;
                foreach (var item in ccb.SelectedItems)
                {
                    double index = Convert.ToDouble(item.ToString().Substring(6));
                    mask = mask + (int)Math.Pow(2, index);
                }
                ccb.Text = mask.ToString("000");

                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }
                DockPanel Sourcepnl = (DockPanel)Ditem.Content;
                PropertyGrid SelectedPgrid = designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid;
                string SelectedProperty = property.DisplayName;
                string newValue = ccb.Text;
                StateY stated = (StateY)SelectedPgrid.SelectedObject;

                Type ClassType = stated.GetType();
                PropertyInfo propertyName = ClassType.GetProperty(SelectedProperty);
                propertyName.SetValue(stated, newValue, null);
                designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = stated;
            }

        }
        
        #endregion

        #region State Parameters

        private string _screen;
        private string _timeoutNextState;
        private string _cancelNextState;
        private string _fdkNextState;
       
        private string _bufferPositionswithExt;
        private string _bufferPositionswithoutExt;
        private string _FDKActiveMask;     

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
                _screen = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Timeout Next State")]
        public string TimeoutNextState
        {
            get
            {
                return _timeoutNextState;
            }

            set
            {
                _timeoutNextState = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Cancel Next State")]
        public string CancelNextState
        {
            get
            {
                return _cancelNextState;
            }

            set
            {
                _cancelNextState = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("FDK Next State")]
        public string FDKNextState
        {
            get
            {
                return _fdkNextState;
            }

            set
            {
                _fdkNextState = value.PadLeft(3, '0'); 
            }
        }
                       
        [Browsable(true)]
        [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Buffer Positions")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BufferPositionsWithOutExtConverter))]
        public string BufferPositionsWithoutExt
        {
            get
            {
                return _bufferPositionswithoutExt;
            }

            set
            {
                _bufferPositionswithoutExt = value;
            }
        }

       
         [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Buffer Positions")]
         [Editor(typeof(BufferPositionsWithExtConverter), typeof(BufferPositionsWithExtConverter))]
         public string BufferPositionsWithExt
         {
             get
             {
                 return _bufferPositionswithExt;
             }

             set
             {
                 _bufferPositionswithExt = value;
             }
         }

        [CategoryAttribute("State Parameters"), PropertyOrder(7), DescriptionAttribute("FDKs Active Mask")]
        [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
        public string FDKActiveMask
        {
            get
            {
                return _FDKActiveMask;
            }
            set
            {
                _FDKActiveMask = value.PadLeft(3, '0');
            }
        }
       
        #endregion

        #region Extension State

        private string _ExtensionStateNumber;
       private string _ExtensionDescription;
       private string _AFDKActive;
       private string _BFDKActive;
       private string _CFDKActive;
       private string _DFDKActive;
       private string _FFDKActive;
       private string _GFDKActive;
       private string _HFDKActive;
       private string _IFDKActive;

       
       [CategoryAttribute("State Extension Parameters"),PropertyOrder(8), DescriptionAttribute("Extension State Number")]
       [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]      
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
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(9), DescriptionAttribute("Extension State Type"), ReadOnlyAttribute(true)]
        public string ExtensionStateType
        {
            get
            {
                return  "Z";
            }

        }
               
       [CategoryAttribute("State Extension Parameters"),PropertyOrder(10),DescriptionAttribute("Extension Description")]
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
              
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(11), DescriptionAttribute("FDK A Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_A_Active
        {
            get
            {
                return _AFDKActive;
            }

            set
            {
                _AFDKActive = value;
            }
        }
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(12), DescriptionAttribute("FDK B Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_B_Active
        {
            get
            {
                return _BFDKActive;
            }

            set
            {
                _BFDKActive = value;
            }
        }
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(13), DescriptionAttribute("FDK C Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_C_Active
        {
            get
            {
                return _CFDKActive;
            }

            set
            {
                _CFDKActive = value;
            }
        }
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(14), DescriptionAttribute("FDK D Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_D_Active
        {
            get
            {
                return _DFDKActive;
            }

            set
            {
                _DFDKActive = value;
            }
        }
              
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(15), DescriptionAttribute("FDK F Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_F_Active
        {
            get
            {
                return _FFDKActive;
            }

            set
            {
                _FFDKActive = value;
            }
        }
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(16), DescriptionAttribute("FDK G Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_G_Active
        {
            get
            {
                return _GFDKActive;
            }

            set
            {
                _GFDKActive = value;
            }
        }
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(17), DescriptionAttribute("FDK H Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_H_Active
        {
            get
            {
                return _HFDKActive;
            }

            set
            {
                _HFDKActive = value;
            }
        }
               
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(18), DescriptionAttribute("FDK I Active")]
        [Editor(typeof(ExtensionFDKActiveConverter), typeof(ExtensionFDKActiveConverter))]
        public string FDK_I_Active
        {
            get
            {
                return _IFDKActive;
            }

            set
            {
                _IFDKActive = value;
            }
        }
      
        #endregion

        #region Extension-FDK Selection function

        private string _multilanguageExtensionStateNumber;      
        private string _MultiLanguageExtensionDescription;
        private string _FDKAScreen;
        private string _FDKBScreen;
        private string _FDKCScreen;
        private string _FDKDScreen;
        private string _FDKFScreen;
        private string _FDKGScreen;
        private string _FDKHScreen;
        private string _FDKIScreen;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]           
        [CategoryAttribute("Multi Language"), PropertyOrder(18), DescriptionAttribute("Multi Language Screens Selection Extension State")]
        public string MultiLanguageExtensionStateNumber
        {
            get
            {
                return _multilanguageExtensionStateNumber;
            }

            set
            {
                _multilanguageExtensionStateNumber = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Multi Language"), PropertyOrder(19),  DescriptionAttribute("Extension State Type"), ReadOnlyAttribute(true)]
        public string MultiLanguageExtensionStateType
        {
            get
            {
                return "Z";
            }

        }

        [CategoryAttribute("Multi Language"), PropertyOrder(20), DescriptionAttribute("Extension Description")]
        public string MultiLanguageExtensionDescription
        {
            get
            {
                return _MultiLanguageExtensionDescription;
            }
            set
            {
                _MultiLanguageExtensionDescription = value;
            }

        }

        [CategoryAttribute("Multi Language"), PropertyOrder(21), DescriptionAttribute("Please Enter FDK A Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKAScreen
        {
            get
            {
                return _FDKAScreen;
            }
            set
            {
                _FDKAScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"),  PropertyOrder(22), DescriptionAttribute("Please Enter FDK B Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKBScreen
        {
            get
            {
                return _FDKBScreen;
            }
            set
            {
                _FDKBScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"),  PropertyOrder(23), DescriptionAttribute("Please Enter FDK C Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKCScreen
        {
            get
            {
                return _FDKCScreen;
            }
            set
            {
                _FDKCScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"),  PropertyOrder(24), DescriptionAttribute("Please Enter FDK D Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKDScreen
        {
            get
            {
                return _FDKDScreen;
            }
            set
            {
                _FDKDScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"),  PropertyOrder(25), DescriptionAttribute("Please Enter FDK F Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKFScreen
        {
            get
            {
                return _FDKFScreen;
            }
            set
            {
                _FDKFScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"), PropertyOrder(26), DescriptionAttribute("Please Enter FDK G Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKGScreen
        {
            get
            {
                return _FDKGScreen;
            }
            set
            {
                _FDKGScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"), PropertyOrder(27), DescriptionAttribute("Please Enter FDK H Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKHScreen
        {
            get
            {
                return _FDKHScreen;
            }
            set
            {
                _FDKHScreen = value;
            }
        }

        [CategoryAttribute("Multi Language"),  PropertyOrder(28), DescriptionAttribute("Please Enter FDK I Screen")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string FDKIScreen
        {
            get
            {
                return _FDKIScreen;
            }
            set
            {
                _FDKIScreen = value;
            }
        }

        #endregion

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            // IState'inde IsExtensionState propertyinin eventinde
            //if (SelectedProperty == "IsMultiLanguage")
            //{

            //    string[] ExtensionStates = new string[] { "MultiLanguageExtensionStateType","FDKAScreen","FDKBScreen", "FDKCScreen", "FDKDScreen",
            //        "FDKFScreen","FDKGScreen","FDKHScreen","FDKIScreen"};

            //    bool Status = (newValue == "1") ? true : false;
            //    string VisibleProperty = "";
              
            //    for (int i = 0; i < ExtensionStates.Length; i++)
            //    {
            //        VisibleProperty = ExtensionStates[i];
            //        PropertyDescriptor theDescriptor = TypeDescriptor.GetProperties(ClassInstance.GetType())[VisibleProperty];
            //        BrowsableAttribute theDescriptorBrowsableAttribute = (BrowsableAttribute)theDescriptor.Attributes[typeof(BrowsableAttribute)];
            //        FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
            //        isBrowsable.SetValue(theDescriptorBrowsableAttribute, Status);

            //    }
            //}


            //if (SelectedProperty == "IsExtensionState")
            //{

            //    string[] ExtensionStates = new string[] { "BufferPositionsWithExt","ExtensionStateType","FDK_A_Active", "FDK_B_Active", "FDK_C_Active", "FDK_D_Active",
            //  "FDK_F_Active","FDK_G_Active","FDK_H_Active","FDK_I_Active"};

            //    bool bufferWithExtStatus = (newValue == "1") ? true : false;
            //    bool bufferWithoutExtStatus = (newValue == "0") ? true : false;

            //    string VisibleProperty = "BufferPositionsWithoutExt";
            //    PropertyDescriptor theDescriptor1 = TypeDescriptor.GetProperties(ClassInstance.GetType())[VisibleProperty];
            //    BrowsableAttribute theDescriptorBrowsableAttribute1 = (BrowsableAttribute)theDescriptor1.Attributes[typeof(BrowsableAttribute)];
            //    FieldInfo isBrowsable1 = theDescriptorBrowsableAttribute1.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
            //    isBrowsable1.SetValue(theDescriptorBrowsableAttribute1, bufferWithoutExtStatus);

            //    for (int i = 0; i < ExtensionStates.Length; i++)
            //    {
            //        VisibleProperty = ExtensionStates[i];
            //        PropertyDescriptor theDescriptor = TypeDescriptor.GetProperties(ClassInstance.GetType())[VisibleProperty];
            //        BrowsableAttribute theDescriptorBrowsableAttribute = (BrowsableAttribute)theDescriptor.Attributes[typeof(BrowsableAttribute)];
            //        FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
            //        isBrowsable.SetValue(theDescriptorBrowsableAttribute, bufferWithExtStatus);

            //    }
            //}

            return FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
        }

        private static object FillStateFromPropertyGrid(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateY Selectedstate = new StateY();
            StateY Dynamicstate = new StateY();

            Selectedstate = (StateY)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateY)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
          
            Dynamicstate._bufferPositionswithExt = Selectedstate._bufferPositionswithExt;
            Dynamicstate._bufferPositionswithoutExt = Selectedstate._bufferPositionswithoutExt;
           //Dynamicstate._isMultiLanguage = Selectedstate._isMultiLanguage;
            //Dynamicstate._isExtensionState = Selectedstate._isExtensionState;
            Dynamicstate._FDKActiveMask = Selectedstate._FDKActiveMask;

            Dynamicstate._MultiLanguageExtensionDescription = Selectedstate.MultiLanguageExtensionDescription;
            Dynamicstate._cancelNextState = Selectedstate._cancelNextState;
            Dynamicstate._multilanguageExtensionStateNumber = Selectedstate.MultiLanguageExtensionStateNumber;
            Dynamicstate._screen = Selectedstate._screen;
            Dynamicstate._timeoutNextState = Selectedstate._timeoutNextState;
            Dynamicstate._fdkNextState = Selectedstate._fdkNextState;


            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._AFDKActive = Selectedstate._AFDKActive;
            Dynamicstate._BFDKActive = Selectedstate._BFDKActive;
            Dynamicstate._CFDKActive = Selectedstate._CFDKActive;
            Dynamicstate._DFDKActive = Selectedstate._DFDKActive;
            Dynamicstate._FFDKActive = Selectedstate._FFDKActive;
            Dynamicstate._GFDKActive = Selectedstate._GFDKActive;
            Dynamicstate._HFDKActive = Selectedstate._HFDKActive;
            Dynamicstate._IFDKActive = Selectedstate._IFDKActive;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateY Selectedstate = new StateY();
            StateY Dynamicstate = new StateY();

            Selectedstate = (StateY)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateY)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;           
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._MultiLanguageExtensionDescription = Selectedstate.MultiLanguageExtensionDescription;
            Dynamicstate._bufferPositionswithExt = Selectedstate._bufferPositionswithExt;
            Dynamicstate._bufferPositionswithoutExt = Selectedstate._bufferPositionswithoutExt;
            //Dynamicstate._isMultiLanguage = Selectedstate._isMultiLanguage;
            //Dynamicstate._isExtensionState = Selectedstate._isExtensionState;
            Dynamicstate._FDKActiveMask = Selectedstate._FDKActiveMask;

            //Dynamicstate._cancelnextState = Selectedstate._cancelnextState;
            Dynamicstate._multilanguageExtensionStateNumber = Selectedstate.MultiLanguageExtensionStateNumber;
            Dynamicstate._screen = Selectedstate._screen;
            //Dynamicstate._timeoutnextState = Selectedstate._timeoutnextState;
            //Dynamicstate._fdkNextState = Selectedstate._fdkNextState;

            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
           
            Dynamicstate._AFDKActive = Selectedstate._AFDKActive;
            Dynamicstate._BFDKActive = Selectedstate._BFDKActive;
            Dynamicstate._CFDKActive = Selectedstate._CFDKActive;
            Dynamicstate._DFDKActive = Selectedstate._DFDKActive;
            Dynamicstate._FFDKActive = Selectedstate._FFDKActive;
            Dynamicstate._GFDKActive = Selectedstate._GFDKActive;
            Dynamicstate._HFDKActive = Selectedstate._HFDKActive;
            Dynamicstate._IFDKActive = Selectedstate._IFDKActive;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateY State = new StateY();
            State = (StateY)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;

            string BufferPositionsValue = State.BufferPositionsWithoutExt;

            if (State.ExtensionStateNumber!= "255")
            {
                string exsql1 = sql;
                BufferPositionsValue = State.BufferPositionsWithExt;
                exsql1 = string.Format(exsql1, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber,
                    State.ExtensionDescription, State.ExtensionStateType, ProjectName, TransactionName,
                    State.FDK_A_Active, State.FDK_B_Active, State.FDK_C_Active, State.FDK_D_Active, State.FDK_F_Active, State.FDK_G_Active,
                    State.FDK_H_Active, State.FDK_I_Active, State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(exsql1);                
            }

            if (State.MultiLanguageExtensionStateNumber != "255")
            {
                string exsql2 = sql;
                exsql2 = string.Format(exsql2, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.MultiLanguageExtensionStateNumber,
                    State.MultiLanguageExtensionDescription, State.MultiLanguageExtensionStateType, ProjectName, TransactionName,
                    State.FDKAScreen, State.FDKBScreen, State.FDKCScreen, State.FDKDScreen, State.FDKFScreen, State.FDKGScreen,
                    State.FDKHScreen, State.FDKIScreen, State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(exsql2);               
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ScreenNumber, State.TimeoutNextState, State.CancelNextState,
                State.FDKNextState, State.ExtensionStateNumber, BufferPositionsValue, State.FDKActiveMask, State.MultiLanguageExtensionStateNumber,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);


            return SqlStringList;
        }

        private void SetDefaultData()
        {
            StateType = "Y";
            StateDescription = "Eight FDK Selection Function State";
            _cancelNextState = "255";
            _timeoutNextState = "255";
            _fdkNextState = "255";
            _screen = "000";
            _ExtensionStateNumber = "255";
            _FDKActiveMask = "000";
            
            //_isMultiLanguage = "0";
            _multilanguageExtensionStateNumber = "255";
            //_isExtensionState = "0";
            _bufferPositionswithExt = "000";
            _bufferPositionswithoutExt = "000";

            _ExtensionDescription = "State Z";
            _AFDKActive = "@@@";
            _BFDKActive = "@@@";
            _CFDKActive = "@@@";
            _DFDKActive = "@@@";
            _FFDKActive = "@@@";
            _GFDKActive = "@@@";
            _HFDKActive = "@@@";
            _IFDKActive = "@@@";

            _MultiLanguageExtensionDescription = "Z State";
            _FDKAScreen = "000";
            _FDKBScreen = "000";
            _FDKCScreen = "000";
            _FDKDScreen = "000";
            _FDKFScreen = "000";
            _FDKGScreen = "000";
            _FDKHScreen = "000";
            _FDKIScreen = "000";
            
        }
                


        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateY state = new StateY();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            string BufferPositionsValue = "";

            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._screen = processRow[8].ToString();
            state._timeoutNextState = processRow[9].ToString();
            state._cancelNextState = processRow[10].ToString();
            state._fdkNextState = processRow[11].ToString();
            state._ExtensionStateNumber = processRow[12].ToString();
            //buffer
            BufferPositionsValue = processRow[13].ToString();
            state._FDKActiveMask = processRow[14].ToString();
            state._multilanguageExtensionStateNumber = processRow[15].ToString();

            //state.IsExtensionState = "0";
            //state.IsMultiLanguage = "0";

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            if (state.ExtensionStateNumber != "255")
            {
                //state._isExtensionState = "1";
                state._bufferPositionswithExt = BufferPositionsValue;
                object[] ExtensionState = GetExtensionState(ref StateList, state.ExtensionStateNumber);
                state._ExtensionDescription = ExtensionState[4].ToString();
                state._AFDKActive = ExtensionState[8].ToString();
                state._BFDKActive = ExtensionState[9].ToString();
                state._CFDKActive = ExtensionState[10].ToString();
                state._DFDKActive = ExtensionState[11].ToString();
                state._FFDKActive = ExtensionState[12].ToString();
                state._GFDKActive = ExtensionState[13].ToString();
                state._HFDKActive = ExtensionState[14].ToString();
                state._IFDKActive = ExtensionState[15].ToString();
            }
            else
            {
                state._bufferPositionswithoutExt = BufferPositionsValue;
            }

            if (state.MultiLanguageExtensionStateNumber != "255")
            {
                //state._isMultiLanguage = "1";
                object[] ExtensionState = GetExtensionState(ref StateList, state.MultiLanguageExtensionStateNumber);
                state._MultiLanguageExtensionDescription = ExtensionState[4].ToString();
                state._FDKAScreen = ExtensionState[8].ToString();
                state._FDKBScreen = ExtensionState[9].ToString();
                state._FDKCScreen = ExtensionState[10].ToString();
                state._FDKDScreen = ExtensionState[11].ToString();
                state._FDKFScreen = ExtensionState[12].ToString();
                state._FDKGScreen = ExtensionState[13].ToString();
                state._FDKHScreen = ExtensionState[14].ToString();
                state._FDKIScreen = ExtensionState[15].ToString();
            }

            //NextState Kontrolu
            if (state.TimeoutNextState != "255")
            {
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }

            if (state.CancelNextState != "255")
            {
                ChildobjList.Add(GetChildState("CancelNextState", state.CancelNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            if (state.FDKNextState != "255")
            {
                ChildobjList.Add(GetChildState("FDKNextState", state.FDKNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
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