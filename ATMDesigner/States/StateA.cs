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
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
   
    public class StateA : StateBase
    {

        public StateA(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }
        
        public StateA()
        {
            SetDefaultValues();
        }    


        #region Converters

        public class ReadCondPropertyConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000, "None");
                strings.Add(001,"Read Track 3");
                strings.Add(002,"Read Track 2");
                strings.Add(003,"Read Track 2 and 3");
                strings.Add(004,"Read Track 1");
                strings.Add(005,"Read Track 1 and 3");
                strings.Add(006,"Read Track 1 and 2");
                strings.Add(007,"All tracks");
                strings.Add(008,"Chip connect ‐ read smart data");

                return strings;
            }
        }

        public class CardReturnFLagConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"Eject Card Immediately");
                strings.Add(001,"Return Card According Transaction Reply");
                return strings;
            }
        }


        #endregion

        #region state parameters

        private string _screen;
        public  string _GoodReadNextState;
        private string _misreadScreen;
        private string _readCondition1;
        private string _readCondition2;
        private string _readCondition3;
        private string _cardReturnFlag;
        public string _NoFITMatchNextState;

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

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Good Read Next State")]
        public string GoodReadNextState
        {
            get
            {
                return _GoodReadNextState;
            }

            set
            {
                _GoodReadNextState = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("MisRead Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string MisreadScreen
        {
            get
            {
                return _misreadScreen;
            }

            set
            {
                _misreadScreen = value.PadLeft(3, '0');
            }
        }

        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(4), DescriptionAttribute("Read Condition 1")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ReadCondPropertyConverter))]
        public string ReadCondition1
        {
            get
            {
                return _readCondition1;
            }
            set
            {
                _readCondition1 = value.PadLeft(3,'0');
            }
        }

        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(5), DescriptionAttribute("Read Condition 2")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ReadCondPropertyConverter))]
        public string ReadCondition2
        {
            get
            {
                return _readCondition2;
            }
            set
            {
                _readCondition2 = value.PadLeft(3, '0'); 
            }
        }

        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(6), DescriptionAttribute("Read Condition 3")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(ReadCondPropertyConverter))]
        public string ReadCondition3
        {
            get
            {
                return _readCondition3;
            }
            set
            {
                _readCondition3 = value.PadLeft(3, '0');
            }
        }

        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(7), DescriptionAttribute("Card Return Flag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(CardReturnFLagConverter))]
        public string CardReturnFlag
        {
            get
            {
                return _cardReturnFlag;
            }
            set
            {
                _cardReturnFlag = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true), DescriptionAttribute("No FIT Match Next State")]
        public string NoFITMatchNextState
        {
            get
            {
                return _NoFITMatchNextState;
            }

            set
            {
                _NoFITMatchNextState = value.PadLeft(3,'0');
            }
        }

        #endregion

        #region Events and Methods

        public override void Clear()
        {
            base.Clear();

            StateType = "A";
            ScreenNumber = "000";
            GoodReadNextState = "000";
            MisreadScreen = "000";
            ReadCondition1 = "";
            ReadCondition2 = "";
            ReadCondition3 = "";
            CardReturnFlag = "";
            NoFITMatchNextState = "000";
        }
        
        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance,PropertyGrid SelectedPgrid)       
        {
            //FillStateFromPropertyGrid
            StateA Selectedstate = new StateA();
            StateA Dynamicstate = new StateA();            
            Selectedstate = (StateA)SelectedPgrid.SelectedObject;          
            Dynamicstate = (StateA)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._cardReturnFlag = Selectedstate.CardReturnFlag;
            Dynamicstate._GoodReadNextState = Selectedstate.GoodReadNextState;
            Dynamicstate._misreadScreen = Selectedstate.MisreadScreen;
            Dynamicstate._NoFITMatchNextState = Selectedstate.NoFITMatchNextState;
            Dynamicstate._readCondition1 = Selectedstate.ReadCondition1;
            Dynamicstate._readCondition2 = Selectedstate.ReadCondition2;
            Dynamicstate._readCondition3 = Selectedstate.ReadCondition3;
            Dynamicstate._screen = Selectedstate.ScreenNumber;      

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateA Selectedstate = new StateA();
            StateA Dynamicstate = new StateA();
            Selectedstate = (StateA)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateA)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._cardReturnFlag = Selectedstate.CardReturnFlag;
           //Dynamicstate._GoodReadNextState = Selectedstate._GoodReadNextState;
            Dynamicstate._misreadScreen = Selectedstate.MisreadScreen;
            //Dynamicstate._nofitmatchState = Selectedstate._nofitmatchState;
            Dynamicstate._readCondition1 = Selectedstate.ReadCondition1;
            Dynamicstate._readCondition2 = Selectedstate.ReadCondition2;
            Dynamicstate._readCondition3 = Selectedstate.ReadCondition3;
            Dynamicstate._screen = Selectedstate.ScreenNumber;
            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateA State = new StateA();
            State = (StateA)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ScreenNumber, State.GoodReadNextState, State.MisreadScreen,
                State.ReadCondition1,State.ReadCondition2, State.ReadCondition3, State.CardReturnFlag, State.NoFITMatchNextState,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "A";
            StateDescription = "Card Read State";
            _screen = "000";
            _GoodReadNextState = "255";
            _misreadScreen = "000";           
            _readCondition1 = "000";
            _readCondition2 = "000";
            _readCondition3 = "000";
            _cardReturnFlag = "001";
            _NoFITMatchNextState = "255";

        }
 
       

        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateA state = new StateA();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._screen = processRow[8].ToString();
            state._GoodReadNextState = processRow[9].ToString();
            state._misreadScreen = processRow[10].ToString();
            state._readCondition1 = processRow[11].ToString();
            state._readCondition2 = processRow[12].ToString();
            state._readCondition3 = processRow[13].ToString();
            state._cardReturnFlag = processRow[14].ToString();
            state._NoFITMatchNextState = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            #region nextState arrange-parent and pointer
            
            if (state.GoodReadNextState != "255")
                ChildobjList.Add(GetChildState("GoodReadNextState", state.GoodReadNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.NoFITMatchNextState != "255")
                ChildobjList.Add(GetChildState("NoFITMatchNextState", state.NoFITMatchNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            
            #endregion



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