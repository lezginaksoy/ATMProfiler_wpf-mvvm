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

    // ; ICC Re-initialise State
    public class StateSemicolon : StateBase
    {

        public StateSemicolon(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateSemicolon()
        {
            SetDefaultValues();
        }    


        #region state parameters

        private string _GoodNextState;
        public string _ProcessingNotPerformedNextState;
        private string _Reserved;       

        [CategoryAttribute("State Parameters"), PropertyOrder(1), ReadOnly(true), DescriptionAttribute("Good Next State")]
        public string GoodNextState
        {
            get
            {
                return _GoodNextState;
            }
            set
            {
                _GoodNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Processing Not Performed Next State")]
        public string ProcessingNotPerformedNextState
        {
            get
            {
                return _ProcessingNotPerformedNextState;
            }
            set
            {
                _ProcessingNotPerformedNextState = value.PadLeft(3, '0');
            }
        }
      
        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true),DescriptionAttribute("Reserved")]
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
            StateSemicolon Selectedstate = new StateSemicolon();
            StateSemicolon Dynamicstate = new StateSemicolon();
            Selectedstate = (StateSemicolon)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateSemicolon)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._GoodNextState = Selectedstate.GoodNextState;
            Dynamicstate._ProcessingNotPerformedNextState = Selectedstate.ProcessingNotPerformedNextState;               
            Dynamicstate._Reserved = Selectedstate.Reserved;      

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateSemicolon Selectedstate = new StateSemicolon();
            StateSemicolon Dynamicstate = new StateSemicolon();
            Selectedstate = (StateSemicolon)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateSemicolon)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            //Dynamicstate._GoodNextState = Selectedstate._GoodNextState;
            //Dynamicstate._ProcessingNotPerformedNextState = Selectedstate._ProcessingNotPerformedNextState;
            Dynamicstate._Reserved = Selectedstate.Reserved;
            
            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateSemicolon State = new StateSemicolon();
            State = (StateSemicolon)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.GoodNextState, State.ProcessingNotPerformedNextState,
                State.Reserved, State.Reserved, State.Reserved, State.Reserved,
                State.Reserved,State.Reserved,State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = ";";
            StateDescription = "ICC Re-initialise State";
            _GoodNextState = "255";
            _ProcessingNotPerformedNextState = "255";
            _Reserved = "000";
        }
        
 

        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateSemicolon state = new StateSemicolon();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._GoodNextState = processRow[8].ToString();
            state._ProcessingNotPerformedNextState= processRow[9].ToString();
            state._Reserved = processRow[14].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            if (state.GoodNextState != "255")
                ChildobjList.Add(GetChildState("GoodNextState", state.GoodNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.ProcessingNotPerformedNextState != "255")
                ChildobjList.Add(GetChildState("ProcessingNotPerformedNextState ", state.ProcessingNotPerformedNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            
            
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