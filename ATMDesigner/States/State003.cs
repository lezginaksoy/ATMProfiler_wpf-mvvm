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

    //  e003  Initialize EMV Transaction Data- EMV Chip Card State
    public class Statee003 : StateBase
    {

        public Statee003(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public Statee003()
        {
            SetDefaultValues();
        }

        #region Converters

        public class AmountsourcebufferConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000, "Don’t set amount.");
                strings.Add(001, "Set amount to 0");
                strings.Add(002, "Set amount from Amount Buffer with no conversion");
                strings.Add(003, "Set amount from General Purpose Buffer B with no conversion");
                strings.Add(004, "Set amount from General Purpose Buffer C with no conversion");
                strings.Add(005, "Set amount from Amount Buffer with conversion");
                strings.Add(006, "Set amount from General Purpose Buffer B with conversion.");
                strings.Add(007, "Set amount from General Purpose Buffer C with conversion");

                return strings;
            }
        }

        public class ISOCurrencyExponentConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"000");
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
        
        public class ISOCurrencyCodeConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                for (int i = 0; i < 1000; i++)
                {
                     strings.Add(i.ToString().PadLeft(3,'0'));
                }              
               
                return strings;
            }
        }

        public class ISOTransactionTypeConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                for (int i = 0; i < 101; i++)
                {
                    strings.Add(i.ToString().PadLeft(3, '0'));
                }

                return strings;
            }
        }

        #endregion

        #region state parameters

        private string _ChipCardOperation;
        public string  _Nextstate;
        private string _ISOTransactionType;
        private string _ISOCurrencyCode;
        private string _ISOCurrencyExponent;
        private string _Amountsourcebuffer;      
   
     
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

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Number of Next state")]
        public string Nextstate
        {
            get
            {
                return _Nextstate;
            }

            set
            {
                _Nextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("ISO Transaction Type.")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ISOTransactionTypeConverter))] 
        public string ISOTransactionType
        {
            get
            {
                return _ISOTransactionType;
            }
            set
            {
                _ISOTransactionType = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), DescriptionAttribute("ISO Currency Code")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ISOCurrencyCodeConverter))] 
        public string ISOCurrencyCode
        {
            get
            {
                return _ISOCurrencyCode;
            }

            set
            {
                _ISOCurrencyCode = value.PadLeft(3, '0');
            }
        }
        
        [CategoryAttribute("State Parameters"), PropertyOrder(5), DescriptionAttribute("ISO Currency Exponent")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ISOCurrencyExponentConverter))]        
        public string ISOCurrencyExponent
        {
            get
            {
                return _ISOCurrencyExponent;
            }

            set
            {
                _ISOCurrencyExponent = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Amount source buffer and conversion flag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(AmountsourcebufferConverter))]  
        public string Amountsourcebuffer
        {
            get
            {
                return _Amountsourcebuffer;
            }

            set
            {
                _Amountsourcebuffer = value.PadLeft(3, '0');
            }
        }
        
        #endregion
        
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            Statee003 Selectedstate = new Statee003();
            Statee003 Dynamicstate = new Statee003();
            Selectedstate = (Statee003)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee003)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            Dynamicstate._Nextstate = Selectedstate.Nextstate;
            Dynamicstate._ISOTransactionType = Selectedstate.ISOTransactionType;
            Dynamicstate._ISOCurrencyCode = Selectedstate.ISOCurrencyCode;
            Dynamicstate._ISOCurrencyExponent = Selectedstate.ISOCurrencyExponent;
            Dynamicstate._Amountsourcebuffer = Selectedstate.Amountsourcebuffer;
         
            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Statee003 Selectedstate = new Statee003();
            Statee003 Dynamicstate = new Statee003();
            Selectedstate = (Statee003)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee003)ClassInstance;

            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._Description= Selectedstate.StateDescription;
            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            //Dynamicstate._Nextstate = Selectedstate._Nextstate;
            Dynamicstate._ISOTransactionType = Selectedstate.ISOTransactionType;
            Dynamicstate._ISOCurrencyCode = Selectedstate.ISOCurrencyCode;
            Dynamicstate._ISOCurrencyExponent = Selectedstate.ISOCurrencyExponent;
            Dynamicstate._Amountsourcebuffer = Selectedstate.Amountsourcebuffer;
         
            return Dynamicstate;
        }
        
        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Statee003 State = new Statee003();
            State = (Statee003)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;
            
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ChipCardOperation, State.Nextstate, State.ISOTransactionType,
                State.ISOCurrencyCode, State.ISOCurrencyExponent, State.Amountsourcebuffer,
                "000","000", State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "e";
            StateDescription = "Initialize EMV Transaction Data";
            _ChipCardOperation = "003";
            _Nextstate = "255";
            _ISOTransactionType = "100";
            _ISOCurrencyCode = "000";
            _ISOCurrencyExponent = "000";
            _Amountsourcebuffer = "000";
        }




        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Statee003 state = new Statee003();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ChipCardOperation = processRow[8].ToString();
            state._Nextstate = processRow[9].ToString();
            state._ISOTransactionType = processRow[10].ToString();
            state._ISOCurrencyCode = processRow[11].ToString();
            state._ISOCurrencyExponent = processRow[12].ToString();
            state._Amountsourcebuffer = processRow[13].ToString();
           

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            ////NextState Kontrolu
            if (state.Nextstate != "255")
                ChildobjList.Add(GetChildState("Nextstate", state.Nextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
        
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