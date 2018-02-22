using System;
using System.ComponentModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid;
using ATMDesigner.Common;
using System.Collections;
using System.Reflection;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
   
    public class StateX : StateBase
    {

        public StateX(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public StateX()
        {
            SetDefaultData();
        }

       
        #region Converters

        public class BufferIDConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(01,"01X-General purpose buffer B");
                strings.Add(02,"02X-General purpose buffer C");
                strings.Add(03,"03X-Amount buffer");
                return strings;
            }
        }

        public class NumberOfZeroesConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add("0");
                strings.Add("1");
                strings.Add("2");
                strings.Add("3");
                strings.Add("4");
                strings.Add("5");
                strings.Add("6");
                strings.Add("7");
                strings.Add("8");
                strings.Add("9");
                return strings;
            }
        }

        public class ExtensionFDKConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                //000 ile 999 arası değer alır
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                for (int i = 0; i < 1000; i++)
                {
                    strings.Add(i.ToString().PadLeft(3,'0'));
                }                
                return strings;
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
                StateX stated = (StateX)SelectedPgrid.SelectedObject;

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
        private string _bufferID;
        private string _numberOfZeroes;      
        private string _Reserved;
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
                _timeoutNextState = value.PadLeft(3, '0'); 
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
                _cancelNextState = value.PadLeft(3, '0'); ;
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
        [Category("State Parameters"), PropertyOrder(5), DescriptionAttribute("Buffer ID")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BufferIDConverter))]  
        public string BufferID
        {
            get { return _bufferID; }
            set { _bufferID = value; }
        }
             
        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(6), DescriptionAttribute("Number Of Zeroes Padding To the Value")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(NumberOfZeroesConverter))]  
        public string PaddingNumberOfZeroes
        {
            get { return _numberOfZeroes; }
            set { _numberOfZeroes = value; }
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

        [CategoryAttribute("State Parameters"), PropertyOrder(8), DescriptionAttribute("Reserved")]
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
        
        #region Extension State

        private string _ExtensionDescription;
        private string _AFDKActive;
        private string _BFDKActive;
        private string _CFDKActive;
        private string _DFDKActive;
        private string _FFDKActive;
        private string _GFDKActive;
        private string _HFDKActive;
        private string _IFDKActive;
        private string _ExtensionStateNumber;

         [Editor(typeof(SetStateNumber), typeof(SetStateNumber))]
         [CategoryAttribute("State Extension Parameters"),PropertyOrder(5), DescriptionAttribute("ExtensionStateNumber")]
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

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(6), DescriptionAttribute("Extension State Type"), ReadOnlyAttribute(true)]
        public string ExtensionStateType
        {
            get
            {
                return  "Z";
            }

        }

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(7), DescriptionAttribute("Extension Description")]
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
                
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(8), DescriptionAttribute("FDK A Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))] 
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
    
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(9), DescriptionAttribute("FDK B Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))] 
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
             
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(10), DescriptionAttribute("FDK C Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))]
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

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(11), DescriptionAttribute("FDK D Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))]
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

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(12), DescriptionAttribute("FDK F Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))]
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

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(13), DescriptionAttribute("FDK G Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))]
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

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(14), DescriptionAttribute("FDK H Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))]
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

        [CategoryAttribute("State Extension Parameters"), PropertyOrder(15), DescriptionAttribute("FDK I Active")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ExtensionFDKConverter))]
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

        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateX Selectedstate = new StateX();
            StateX Dynamicstate = new StateX();

            Selectedstate = (StateX)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateX)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
          
            Dynamicstate._bufferID = Selectedstate._bufferID;
            Dynamicstate._cancelNextState = Selectedstate._cancelNextState;
            Dynamicstate._numberOfZeroes = Selectedstate._numberOfZeroes;
            Dynamicstate._Reserved = Selectedstate._Reserved;
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
            StateX Selectedstate = new StateX();
            StateX Dynamicstate = new StateX();

            Selectedstate = (StateX)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateX)ClassInstance;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate._bufferID = Selectedstate._bufferID;
            //Dynamicstate._cancelnextState = Selectedstate._cancelnextState;
            Dynamicstate._numberOfZeroes = Selectedstate._numberOfZeroes;
            Dynamicstate._Reserved = Selectedstate._Reserved;
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
            StateX State = new StateX();
            State = (StateX)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            
            //Extension
            string exsql = sql;
            if (State.ExtensionStateNumber != "255")
            {              
                exsql = string.Format(exsql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber, State.ExtensionDescription,
                State.ExtensionStateType, ProjectName, TransactionName, State.FDK_A_Active, State.FDK_B_Active, State.FDK_C_Active, State.FDK_D_Active,
                State.FDK_F_Active, State.FDK_G_Active, State.FDK_H_Active, State.FDK_I_Active,
                State.ConfigId, State.BrandId, State.ConfigVersion);
                SqlStringList.Add(exsql);
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ScreenNumber, State.TimeoutNextState, State.CancelNextState,
                State.FDKNextState, State.ExtensionStateNumber, State.BufferID + State.PaddingNumberOfZeroes,
                State.FDKActiveMask, State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);


            return SqlStringList;

        }

        private void SetDefaultData()
        {
            StateType = "X";
            StateDescription = "FDK Information Entry State";
            _ExtensionDescription = "State Z";
            _cancelNextState = "255";
            _timeoutNextState = "255";
            _fdkNextState = "255";
            _ExtensionStateNumber = "255";
            _screen = "000";
            _FDKActiveMask = "000";
            _Reserved = "000";
            _bufferID = "01";
            _numberOfZeroes = "2";
            _AFDKActive = "000";
            _BFDKActive = "000";
            _CFDKActive = "000";
            _DFDKActive = "000";
            _FFDKActive = "000";
            _GFDKActive = "000";
            _HFDKActive = "000";
            _IFDKActive = "000";
            
        }

        

        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateX state = new StateX();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._screen = processRow[8].ToString();
            state._timeoutNextState = processRow[9].ToString();
            state._cancelNextState = processRow[10].ToString();
            state._fdkNextState = processRow[11].ToString();
            state._ExtensionStateNumber = processRow[12].ToString();
            state._bufferID = processRow[13].ToString().Substring(0,2);
            state._numberOfZeroes = processRow[13].ToString().Substring(2, 1);
            state._FDKActiveMask = processRow[14].ToString();
            state._Reserved = processRow[15].ToString();
            
            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();
         
            //Extension 
            if (state.ExtensionStateNumber != "255")
            {
                object[] ExtensionStates = GetExtensionState(ref StateList, state.ExtensionStateNumber);
                state._ExtensionDescription = ExtensionStates[4].ToString();
                state._AFDKActive = ExtensionStates[8].ToString();
                state._BFDKActive = ExtensionStates[9].ToString();
                state._CFDKActive = ExtensionStates[10].ToString();
                state._DFDKActive = ExtensionStates[11].ToString();
                state._FFDKActive = ExtensionStates[12].ToString();
                state._GFDKActive = ExtensionStates[13].ToString();
                state._HFDKActive = ExtensionStates[14].ToString();
                state._IFDKActive = ExtensionStates[15].ToString();
            }

            //NextState Kontrolu
            if (state.TimeoutNextState != "255")
            {
                ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            //NextState Kontrolu
            if (state.CancelNextState != "255")
            {
                ChildobjList.Add(GetChildState("CancelNextState", state.CancelNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            }
            //NextState Kontrolu
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
                   // StateList.Remove(StatedRow);
                    return ExtensionState;
                }
            }

            return ExtensionState;
        }
    

        #endregion

  
    }
}