using System;
using System.ComponentModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Collections;
using ATMDesigner.Common;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
  
    public class Statek : StateBase
    {
        public Statek(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public Statek()
        {
            SetDefaultData();
        }
      
        #region Converter

        public class CardReturnFlagConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"Eject the card immediately");
                strings.Add(001,"Return the card as...");
                return strings;
            }
        }

        #endregion

        #region State Parameters

        private string _Reserved0;
        private string _NextState;
        private string _Reserved1;
        private string _Reserved2;
        private string _Reserved3;
        private string _Reserved4;
        private string _CardReturnFlag;
        private string _NoFitMatchNextState;
       
        [CategoryAttribute("State Parameters"),PropertyOrder(1), ReadOnly(true), DescriptionAttribute("Next State")]
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

        [CategoryAttribute("Reserve Parameters"), PropertyOrder(2), DescriptionAttribute("Reserved 0")]
        public string Reserved0
        {
            get
            {
                return _Reserved0;
            }

            set
            {
                _Reserved0 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("Reserve Parameters"), PropertyOrder(3), DescriptionAttribute("Reserved 1")]
        public string Reserved1
        {
            get
            {
                return _Reserved1;
            }

            set
            {
                _Reserved1 = value.PadLeft(3, '0');
            }
        }
        [CategoryAttribute("Reserve Parameters"), PropertyOrder(4), DescriptionAttribute("Reserved 2")]
        public string Reserved2
        {
            get
            {
                return _Reserved2;
            }

            set
            {
                _Reserved2 = value.PadLeft(3, '0');
            }
        }
        [CategoryAttribute("Reserve Parameters"), PropertyOrder(5), DescriptionAttribute("Reserved 3")]
        public string Reserved3
        {
            get
            {
                return _Reserved3;
            }

            set
            {
                _Reserved3 = value.PadLeft(3, '0');
            }
        }
        [CategoryAttribute("Reserve Parameters"), PropertyOrder(6), DescriptionAttribute("Reserved 4")]
        public string Reserved4
        {
            get
            {
                return _Reserved4;
            }

            set
            {
                _Reserved4 = value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("State Parameters"), PropertyOrder(7), DescriptionAttribute("Card Return Flag")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(CardReturnFlagConverter))]
        public string CardReturnFlag
        {
            get
            {
                return _CardReturnFlag;
            }

            set
            {
                _CardReturnFlag = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true), DescriptionAttribute("No FIT Match Next State Number")]
        public string NoFitMatchNextState
        {
            get
            {
                return _NoFitMatchNextState;
            }

            set
            {
                _NoFitMatchNextState = value.PadLeft(3,'0');
            }
        }

        #endregion
        
        #region Events and Methods
        
        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            Statek Selectedstate = new Statek();
            Statek Dynamicstate = new Statek();
            Dynamicstate._Description = Selectedstate.StateDescription;
            Selectedstate = (Statek)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statek)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
          
            Dynamicstate._CardReturnFlag = Selectedstate._CardReturnFlag;
            Dynamicstate._NextState = Selectedstate._NextState;
            Dynamicstate._NoFitMatchNextState = Selectedstate._NoFitMatchNextState;

            Dynamicstate._Reserved0 = Selectedstate._Reserved0;
            Dynamicstate._Reserved1 = Selectedstate._Reserved1;
            Dynamicstate._Reserved2 = Selectedstate._Reserved2;
            Dynamicstate._Reserved3 = Selectedstate._Reserved3;
            Dynamicstate._Reserved4 = Selectedstate._Reserved4;

            return Dynamicstate;
        }
        
        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            Statek Selectedstate = new Statek();
            Statek Dynamicstate = new Statek();

            Selectedstate = (Statek)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statek)ClassInstance;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate._CardReturnFlag = Selectedstate._CardReturnFlag;
            //Dynamicstate._nextState = Selectedstate._nextState;
            //Dynamicstate._NoFitMatchNextStateNum = Selectedstate._NoFitMatchNextStateNum;

            Dynamicstate._Reserved0 = Selectedstate._Reserved0;
            Dynamicstate._Reserved1 = Selectedstate._Reserved1;
            Dynamicstate._Reserved2 = Selectedstate._Reserved2;
            Dynamicstate._Reserved3 = Selectedstate._Reserved3;
            Dynamicstate._Reserved4 = Selectedstate._Reserved4;

            return Dynamicstate;
        }
        
        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Statek State = new Statek();
            State = (Statek)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.Reserved0, State.NextState, State.Reserved1,
                State.Reserved2, State.Reserved3, State.Reserved4, State.CardReturnFlag, State.NoFitMatchNextState, State.ConfigId,State.BrandId, 
                State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultData()
        {
            StateType = "k";
            StateDescription = "Smart FIT Check State";
            _NoFitMatchNextState = "255";
            _NextState = "255";
            _Reserved0 = "000";
            _Reserved1 = "000";
            _Reserved2 = "000";
            _Reserved3 = "000";
            _Reserved4 = "000";
            _CardReturnFlag = "000";
        }



        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Statek state = new Statek();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._Reserved0= processRow[8].ToString();
            state._NextState = processRow[9].ToString();
            state._Reserved1 = processRow[10].ToString();
            state._Reserved2 = processRow[11].ToString();
            state._Reserved3 = processRow[12].ToString();
            state._Reserved4 = processRow[13].ToString();
            state._CardReturnFlag = processRow[14].ToString();
            state._NoFitMatchNextState = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            #region nextState arrange-parent and pointer

            if (state.NextState != "255")
                ChildobjList.Add(GetChildState("NextState", state.NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.NoFitMatchNextState != "255")
                ChildobjList.Add(GetChildState("NoFitMatchNextState", state.NoFitMatchNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

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