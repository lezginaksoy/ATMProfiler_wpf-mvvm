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
    
    //- Automatic Language SelectionState
    public class StateDash : StateBase
    {

        public StateDash(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateDash()
        {
            SetDefaultValues();
        }    


        #region Converters

       
        #endregion

        #region state parameters

        private string _LanguageMatchNextState;
        private string _NoLanguageMatchNextState;
        private string _Reserved;


        [CategoryAttribute("State Parameters"), PropertyOrder(1), ReadOnly(true), DescriptionAttribute("Language Match Next State")]
        public string LanguageMatchNextState
        {
            get
            {
                return _LanguageMatchNextState;
            }

            set
            {
                _LanguageMatchNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("No Language Match Next State")]
        public string NoLanguageMatchNextState
        {
            get
            {
                return _NoLanguageMatchNextState;
            }

            set
            {
                _NoLanguageMatchNextState = value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("State Parameters"), PropertyOrder(7), ReadOnly(true), DescriptionAttribute("Reserved")]
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

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance,PropertyGrid SelectedPgrid)       
        {
            //FillStateFromPropertyGrid
            StateDash Selectedstate = new StateDash();
            StateDash Dynamicstate = new StateDash();
            Selectedstate = (StateDash)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateDash)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;          
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._LanguageMatchNextState = Selectedstate.LanguageMatchNextState;
            Dynamicstate._NoLanguageMatchNextState = Selectedstate.NoLanguageMatchNextState;         
            Dynamicstate._Reserved = Selectedstate.Reserved;      

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateDash Selectedstate = new StateDash();
            StateDash Dynamicstate = new StateDash();
            Selectedstate = (StateDash)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateDash)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;   
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            //Dynamicstate._LanguageMatchNextState = Selectedstate._LanguageMatchNextState;
            //Dynamicstate._NoLanguageMatchNextState = Selectedstate._NoLanguageMatchNextState;         
            Dynamicstate._Reserved = Selectedstate.Reserved;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateDash State = new StateDash();
            State = (StateDash)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.LanguageMatchNextState, State.NoLanguageMatchNextState,
                State.Reserved, State.Reserved, State.Reserved, State.Reserved,
                State.Reserved,State.Reserved,State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "-";
            StateDescription = "Automatic Language Selection State";
            _LanguageMatchNextState = "255";
            _NoLanguageMatchNextState = "255";
            _Reserved = "000";
        }



        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateDash state = new StateDash();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._LanguageMatchNextState = processRow[8].ToString();
            state._NoLanguageMatchNextState = processRow[9].ToString();
            state._Reserved = processRow[14].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            if (state.LanguageMatchNextState != "255")
                ChildobjList.Add(GetChildState("LanguageMatchNextState", state.LanguageMatchNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.NoLanguageMatchNextState != "255")
                ChildobjList.Add(GetChildState("NoLanguageMatchNextState", state.NoLanguageMatchNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

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