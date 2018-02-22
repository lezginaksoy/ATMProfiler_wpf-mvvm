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

    //  ? CSet ICC Transaction Data State
    public class StateQuery : StateBase
    {

        public StateQuery(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateQuery()
        {
            SetDefaultValues();
        }    


        #region Converters

        public class AmountAuthorisedSource1Converter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();

                strings.Add(00, "Do nothing (no source buffer)");
                strings.Add(01, "General Purpose Buffer B");
                strings.Add(02, "General Purpose Buffer C");
                strings.Add(03, "Amount Buffer");
                strings.Add(04, "Set Amount Authorised to 0.");

                return strings;
            }
        }


        public class AmountAuthorisedSource2Converter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(0, "No conversion is required");
                strings.Add(1, "Conversion is required");                
                return strings;
            }
        }


        public class AmountOtherSource1Converter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();

                strings.Add(00, "Do nothing (no source buffer)");
                strings.Add(01, "General Purpose Buffer B");
                strings.Add(02, "General Purpose Buffer C");
                strings.Add(03, "Amount Buffer");
                strings.Add(04, "Set Amount Other to 0.");

                return strings;
            }
        }

     
        
        #endregion

        #region state parameters

        private string _NextState;
        public  string _CurrencyType;
        private string _TransactionType;
        private string _AmountAuthorisedSource1;
        private string _AmountAuthorisedSource2;
        private string _AmountOtherSource1;
        private string _AmountOtherSource2;
        private string _Reserved;

        
        [CategoryAttribute("State Parameters"), PropertyOrder(1), ReadOnly(true), DescriptionAttribute("Next State Number")]
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
                
        [CategoryAttribute("State Parameters"), PropertyOrder(2),DescriptionAttribute("Currency Type")]
        public string CurrencyType
        {
            get
            {
                return _CurrencyType;
            }

            set
            {
                _CurrencyType = value.PadLeft(3, '0');
            }
        }
                
        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("Transaction Type")]
        public string TransactionType
        {
            get
            {
                return _TransactionType;
            }

            set
            {
                _TransactionType = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), DescriptionAttribute("Amount Authorised Source")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(AmountAuthorisedSource1Converter))]
        public string AmountAuthorisedSource1
        {
            get
            {
                return _AmountAuthorisedSource1;
            }

            set
            {
                _AmountAuthorisedSource1 = value;
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(5), DescriptionAttribute("Amount Authorised Source")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(AmountAuthorisedSource2Converter))]
        public string AmountAuthorisedSource2
        {
            get
            {
                return _AmountAuthorisedSource2;
            }

            set
            {
                _AmountAuthorisedSource2 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), DescriptionAttribute("Amount Other Source")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(AmountOtherSource1Converter))]
        public string AmountOtherSource1
        {
            get
            {
                return _AmountOtherSource1;
            }

            set
            {
                _AmountOtherSource1 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(7), DescriptionAttribute("Amount Other Source")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(AmountAuthorisedSource2Converter))]
        public string AmountOtherSource2
        {
            get
            {
                return _AmountOtherSource2;
            }

            set
            {
                _AmountOtherSource2 = value.PadLeft(3, '0');
            }
        }


        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Reserved")]
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
            //FillStateFromPropertyGrid
            StateQuery Selectedstate = new StateQuery();
            StateQuery Dynamicstate = new StateQuery();
            Selectedstate = (StateQuery)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateQuery)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._NextState = Selectedstate._NextState;
            Dynamicstate._CurrencyType = Selectedstate._CurrencyType;
            Dynamicstate._TransactionType = Selectedstate._TransactionType;
            Dynamicstate._AmountAuthorisedSource1 = Selectedstate._AmountAuthorisedSource1;
            Dynamicstate._AmountAuthorisedSource2 = Selectedstate._AmountAuthorisedSource2;
            Dynamicstate._AmountOtherSource1 = Selectedstate._AmountOtherSource1;

            Dynamicstate._AmountOtherSource2 = Selectedstate._AmountOtherSource2;
            Dynamicstate._Reserved = Selectedstate._Reserved;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateQuery Selectedstate = new StateQuery();
            StateQuery Dynamicstate = new StateQuery();
            Selectedstate = (StateQuery)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateQuery)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            //Dynamicstate._NextState = Selectedstate._NextState;
            Dynamicstate._CurrencyType = Selectedstate._CurrencyType;
            Dynamicstate._TransactionType = Selectedstate._TransactionType;
            Dynamicstate._AmountAuthorisedSource1 = Selectedstate._AmountAuthorisedSource1;
            Dynamicstate._AmountAuthorisedSource2 = Selectedstate._AmountAuthorisedSource2;
            Dynamicstate._AmountOtherSource1 = Selectedstate._AmountOtherSource1;

            Dynamicstate._AmountOtherSource2 = Selectedstate._AmountOtherSource2;
            Dynamicstate._Reserved = Selectedstate._Reserved;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateQuery State = new StateQuery();
            State = (StateQuery)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;
            string AmountAuthorisedSource = AmountAuthorisedSource1 + AmountAuthorisedSource2;
            string AmountOtherSource = AmountOtherSource1 + AmountOtherSource2;

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.NextState, State.CurrencyType,
                State.TransactionType, AmountAuthorisedSource, AmountOtherSource, State.Reserved, State.Reserved,
                State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "?";
            StateDescription = "Set ICC Transaction Data State";


            _NextState = "255";
            _CurrencyType = "000";
            _TransactionType = "000";
            _AmountAuthorisedSource1 = "00";
            _AmountAuthorisedSource2 = "0";
            _AmountOtherSource1 = "00";
            _AmountOtherSource2 = "0";
            _Reserved = "000";
        }



        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateQuery state = new StateQuery();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            string AmountAuthorisedSource;
            string AmountOtherSource;

            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._NextState = processRow[8].ToString();
            state._CurrencyType= processRow[9].ToString();
            state._TransactionType = processRow[10].ToString();
           
            AmountAuthorisedSource=(String.IsNullOrEmpty(processRow[11].ToString())?"000":processRow[11].ToString());
            AmountOtherSource=(String.IsNullOrEmpty(processRow[12].ToString())?"000":processRow[12].ToString());
            state._AmountAuthorisedSource1 = AmountAuthorisedSource.Substring(0, 2);
            state._AmountAuthorisedSource2 = AmountAuthorisedSource.Substring(2, 1);
            state._AmountOtherSource1 = AmountOtherSource.Substring(0, 2);
            state._AmountOtherSource2 = AmountAuthorisedSource.Substring(2, 1);
            state._Reserved = processRow[13].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

         
                //NextState Kontrolu
            if (state.NextState != "255")
                ChildobjList.Add(GetChildState("NextState", state.NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
         
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