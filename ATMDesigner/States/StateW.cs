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
 
    public class StateW : StateBase
    {

        public StateW(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultData();
        }

        public StateW()
        {
            SetDefaultData();
        }

        #region State Parameters

        private string _fdkANextState;
        private string _fdkBNextState;
        private string _fdkCNextState;
        private string _fdkDNextState;
        private string _fdkFNextState;
        private string _fdkGNextState;
        private string _fdkHNextState;
        private string _fdkINextState;
        
        [CategoryAttribute("State Parameters"),PropertyOrder(1), ReadOnly(true), DescriptionAttribute("FDK A Next State")]
        public string FDK_A_NextState
        {
            get
            {
                return _fdkANextState;
            }

            set
            {
                _fdkANextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("FDK B Next State")]
        public string FDK_B_NextState
        {
            get
            {
                return _fdkBNextState;
            }

            set
            {
                _fdkBNextState = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("FDK C Next State")]
        public string FDK_C_NextState
        {
            get
            {
                return _fdkCNextState;
            }

            set
            {
                _fdkCNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("FDK D Next State")]
        public string FDK_D_NextState
        {
            get
            {
                return _fdkDNextState;
            }

            set
            {
                _fdkDNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("FDK F Next State")]
        public string FDK_F_NextState
        {
            get
            {
                return _fdkFNextState;
            }

            set
            {
                _fdkFNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(6), ReadOnly(true), DescriptionAttribute("FDK G Next State")]
        public string FDK_G_NextState
        {
            get
            {
                return _fdkGNextState;
            }

            set
            {
                _fdkGNextState = value.PadLeft(3, '0');
            }
        }


        [CategoryAttribute("State Parameters"), PropertyOrder(7), ReadOnly(true), DescriptionAttribute("FDK H Next State")]
        public string FDK_H_NextState
        {
            get
            {
                return _fdkHNextState;
            }

            set
            {
                _fdkHNextState = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(8), ReadOnly(true), DescriptionAttribute("FDK I Next State")]
        public string FDK_I_NextState
        {
            get
            {
                return _fdkINextState;
            }

            set
            {
                _fdkINextState = value.PadLeft(3, '0');
            }
        }

        #endregion

        #region Events and Meth0ds

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateW Selectedstate = new StateW();
            StateW Dynamicstate = new StateW();

            Selectedstate = (StateW)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateW)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
          
            Dynamicstate._fdkANextState = Selectedstate._fdkANextState;
            Dynamicstate._fdkBNextState = Selectedstate._fdkBNextState;
            Dynamicstate._fdkCNextState = Selectedstate._fdkCNextState;
            Dynamicstate._fdkDNextState = Selectedstate._fdkDNextState;
            Dynamicstate._fdkFNextState = Selectedstate._fdkFNextState;
            Dynamicstate._fdkGNextState = Selectedstate._fdkGNextState;
            Dynamicstate._fdkHNextState = Selectedstate._fdkHNextState;
            Dynamicstate._fdkINextState = Selectedstate._fdkINextState;

            return Dynamicstate;
        }

        public override Object  FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateW Selectedstate = new StateW();
            StateW Dynamicstate = new StateW();

            Selectedstate = (StateW)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateW)ClassInstance;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._Description = Selectedstate.StateDescription;
          
            //Dynamicstate._fdkANextState = Selectedstate._fdkANextState;
            //Dynamicstate._fdkBNextState = Selectedstate._fdkBNextState;
            //Dynamicstate._fdkCNextState = Selectedstate._fdkCNextState;
            //Dynamicstate._fdkDNextState = Selectedstate._fdkDNextState;
            //Dynamicstate._fdkFNextState = Selectedstate._fdkFNextState;
            //Dynamicstate._fdkGNextState = Selectedstate._fdkGNextState;
            //Dynamicstate._fdkHNextState = Selectedstate._fdkHNextState;
            //Dynamicstate._fdkINextState = Selectedstate._fdkINextState;

            return Dynamicstate;
        }
             
        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateW State = new StateW();
            State = (StateW)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.FDK_A_NextState, State.FDK_B_NextState, State.FDK_C_NextState,
                State.FDK_D_NextState, State.FDK_F_NextState, State.FDK_G_NextState, State.FDK_H_NextState, State.FDK_I_NextState,
                State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultData()
        {
            StateType = "W";
            StateDescription = "FDK Switch State 2";
            _fdkANextState = "255";
            _fdkBNextState = "255";
            _fdkCNextState = "255";
            _fdkDNextState = "255";
            _fdkFNextState = "255";
            _fdkGNextState = "255";
            _fdkHNextState = "255";
            _fdkINextState = "255";

        }



        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateW state = new StateW();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state.StateDescription = processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._fdkANextState = processRow[8].ToString();
            state._fdkBNextState = processRow[9].ToString();
            state._fdkCNextState = processRow[10].ToString();
            state._fdkDNextState = processRow[11].ToString();
            state._fdkFNextState= processRow[12].ToString();
            state._fdkGNextState= processRow[13].ToString();
            state._fdkHNextState= processRow[14].ToString();
            state._fdkINextState= processRow[15].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            //NextState Kontrolu
            #region nextState arrange-parent and pointer

            if (state.FDK_A_NextState != "255")
                ChildobjList.Add(GetChildState("FDK_A_NextState", state.FDK_A_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_B_NextState != "255")
                ChildobjList.Add(GetChildState("FDK_B_NextState", state.FDK_B_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_C_NextState != "255")
                ChildobjList.Add(GetChildState("FDK_C_NextState", state.FDK_C_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_D_NextState != "255")
                ChildobjList.Add(GetChildState("FDK_D_NextState", state.FDK_D_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_F_NextState!= "255")
                ChildobjList.Add(GetChildState("FDK_F_NextState", state.FDK_F_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_G_NextState != "255")
                ChildobjList.Add(GetChildState("FDK_G_NextState", state.FDK_G_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_H_NextState!= "255")
                ChildobjList.Add(GetChildState("FDK_H_NextState", state.FDK_H_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

            if (state.FDK_I_NextState != "255")
                ChildobjList.Add(GetChildState("FDK_I_NextState", state.FDK_I_NextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));

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