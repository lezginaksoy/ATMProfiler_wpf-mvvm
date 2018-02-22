using ATMDesigner.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
   
    public class StateK : StateBase
    {

        public StateK(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public StateK()
        {
            SetDefaultData();
        }

        #region State Parameters

        private string _NextState0;
        private string _NextState1;
        private string _NextState2;
        private string _NextState3;
        private string _NextState4;
        private string _NextState5;
        private string _NextState6;
        private string _NextState7;


        [CategoryAttribute("State Parameters"),PropertyOrder(1), ReadOnly(true), DescriptionAttribute("Next State 0")]
        public string NextState0
        {
            get
            {
                return _NextState0;
            }

            set
            {
                _NextState0 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Next State 1")]
        public string NextState1
        {
            get
            {
                return _NextState1;
            }

            set
            {
                _NextState1 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Next State 2")]
        public string NextState2
        {
            get
            {
                return _NextState2;
            }

            set
            {
                _NextState2 = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Next State 3")]
        public string NextState3
        {
            get
            {
                return _NextState3;
            }

            set
            {
                _NextState3 = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("Next State 4")]
        public string NextState4
        {
            get
            {
                return _NextState4;
            }

            set
            {
                _NextState4 = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("Next State 5")]
        public string NextState5
        {
            get
            {
                return _NextState5;
            }

            set
            {
                _NextState5 = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(7), ReadOnly(true), DescriptionAttribute("Next State 6")]
        public string NextState6
        {
            get
            {
                return _NextState6;
            }

            set
            {
                _NextState6 = value.PadLeft(3, '0'); 
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true), DescriptionAttribute("Next State 7")]
        public string NextState7
        {
            get
            {
                return _NextState7;
            }

            set
            {
                _NextState7 = value.PadLeft(3, '0'); 
            }
        }

        #endregion
        
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateK Selectedstate = new StateK();
            StateK Dynamicstate = new StateK();

            Selectedstate = (StateK)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateK)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
          
            Dynamicstate._NextState0 = Selectedstate._NextState0;
            Dynamicstate._NextState1 = Selectedstate._NextState1;
            Dynamicstate._NextState2 = Selectedstate._NextState2;
            Dynamicstate._NextState3 = Selectedstate._NextState3;
            Dynamicstate._NextState4 = Selectedstate._NextState4;
            Dynamicstate._NextState5 = Selectedstate._NextState5;
            Dynamicstate._NextState6 = Selectedstate._NextState6;
            Dynamicstate._NextState7 = Selectedstate._NextState7;

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateK Selectedstate = new StateK();
            StateK Dynamicstate = new StateK();

            Selectedstate = (StateK)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateK)ClassInstance;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._Description = Selectedstate.StateDescription;
            //Dynamicstate._NextState0 = Selectedstate._NextState0;
            //Dynamicstate._NextState1 = Selectedstate._NextState1;
            //Dynamicstate._NextState2 = Selectedstate._NextState2;
            //Dynamicstate._NextState3 = Selectedstate._NextState3;
            //Dynamicstate._NextState4 = Selectedstate._NextState4;
            //Dynamicstate._NextState5 = Selectedstate._NextState5;
            //Dynamicstate._NextState6 = Selectedstate._NextState6;
            //Dynamicstate._NextState7 = Selectedstate._NextState7;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateK State = new StateK();
            State = (StateK)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.NextState0, State.NextState1, State.NextState2,
                State.NextState3, State.NextState4, State.NextState5, State.NextState6, State.NextState7, State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultData()
        {
            StateType = "K";
            StateDescription = "FIT Switch State";
            _NextState0 = "255";
            _NextState1 = "255";
            _NextState2 = "255";
            _NextState3 = "255";
            _NextState4 = "255";
            _NextState5 = "255";
            _NextState6 = "255";
            _NextState7 = "255";
        }

        
        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateK state = new StateK();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._NextState0 = processRow[8].ToString();
            state._NextState1 = processRow[9].ToString();
            state._NextState2 = processRow[10].ToString();
            state._NextState3 = processRow[11].ToString();
            state._NextState4 = processRow[12].ToString();
            state._NextState5 = processRow[13].ToString();
            state._NextState6 = processRow[14].ToString();
            state._NextState7 = processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            #region nextState arrange-parent and pointer

            if (state.NextState0 != "255")
                ChildobjList.Add(GetChildState("NextState0", state.NextState0, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.NextState1 != "255")
                ChildobjList.Add(GetChildState("NextState1", state.NextState1, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.NextState2 != "255")
                ChildobjList.Add(GetChildState("NextState2", state.NextState2, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.NextState3 != "255")
                ChildobjList.Add(GetChildState("NextState3", state.NextState3, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.NextState4 != "255")
                ChildobjList.Add(GetChildState("NextState4", state.NextState4, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.NextState5 != "255")
                ChildobjList.Add(GetChildState("NextState5", state.NextState5, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            
            if (state.NextState6 != "255")
                ChildobjList.Add(GetChildState("NextState6", state.NextState6, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
           
            if (state.NextState7 != "255")
                ChildobjList.Add(GetChildState("NextState7", state.NextState7, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

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