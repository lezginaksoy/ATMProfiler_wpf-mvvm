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

    //  e005  Generate 2nd AC/AAC cancellation- EMV Chip Card State
    public class Statee005 : StateBase
    {

        public Statee005(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public Statee005()
        {
            SetDefaultValues();
        }

        #region Converters
        #endregion
        
        #region state parameters

        private string _ChipCardOperation;    
        public string  _SecondACSuccessfulNextstate;
        public string  _EMVAppErrorNextstate;
        public string _EMVHardwareErrorNextstate;
        public string _EMVSequenceErrorNextstate;


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

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Specifies the state number if no error occurred.")]
        public string SecondACSuccessfulNextstate
        {
            get
            {
                return _SecondACSuccessfulNextstate;
            }

            set
            {
                _SecondACSuccessfulNextstate = value.PadLeft(3, '0');
            }
        }
                
        [CategoryAttribute("State Parameters"), PropertyOrder(3), ReadOnly(true), DescriptionAttribute("Specifies the state number if an EMV application-level error occurred.")]
        public string EMVAppErrorNextstate
        {
            get
            {
                return _EMVAppErrorNextstate;
            }

            set
            {
                _EMVAppErrorNextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(4), ReadOnly(true), DescriptionAttribute("Specifies the state number if an EMV hardware-level error occurred.")]
        public string EMVHardwareErrorNextstate
        {
            get
            {
                return _EMVHardwareErrorNextstate;
            }

            set
            {
                _EMVHardwareErrorNextstate = value.PadLeft(3, '0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(5), ReadOnly(true), DescriptionAttribute("Specifies the state number if the current conditions do not allow processing of this state/operation. This is the case if the EMV transaction data was not initialized (see Initialize EMV Transaction Data (Operation 003)).")]
        public string EMVSequenceErrorNextstate
        {
            get
            {
                return _EMVSequenceErrorNextstate;
            }

            set
            {
                _EMVSequenceErrorNextstate = value.PadLeft(3, '0');
            }
        }

      
        #endregion
        
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            Statee005 Selectedstate = new Statee005();
            Statee005 Dynamicstate = new Statee005();
            Selectedstate = (Statee005)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee005)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;
            Dynamicstate._SecondACSuccessfulNextstate = Selectedstate.SecondACSuccessfulNextstate;          
            Dynamicstate._EMVAppErrorNextstate = Selectedstate.EMVAppErrorNextstate;
            Dynamicstate._EMVHardwareErrorNextstate = Selectedstate.EMVHardwareErrorNextstate;
            Dynamicstate._EMVSequenceErrorNextstate = Selectedstate.EMVSequenceErrorNextstate;
         
            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid
            Statee005 Selectedstate = new Statee005();
            Statee005 Dynamicstate = new Statee005();
            Selectedstate = (Statee005)SelectedPgrid.SelectedObject;
            Dynamicstate = (Statee005)ClassInstance;

            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._Description= Selectedstate.StateDescription;
            Dynamicstate._ChipCardOperation = Selectedstate.ChipCardOperation;

            //Dynamicstate._SecondACSuccessfulNextstate = Selectedstate._SecondACSuccessfulNextstate;
            //Dynamicstate._EMVAppErrorNextstate = Selectedstate._EMVAppErrorNextstate;
            //Dynamicstate._EMVHardwareErrorNextstate = Selectedstate._EMVHardwareErrorNextstate;
            //Dynamicstate._EMVSequenceErrorNextstate = Selectedstate._EMVSequenceErrorNextstate;

            return Dynamicstate;
        }
        
        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            Statee005 State = new Statee005();
            State = (Statee005)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();
            string sql = SqlStr;
            
            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
                State.StateType, ProjectName, TransactionName, State.ChipCardOperation, State.SecondACSuccessfulNextstate,
                State.EMVAppErrorNextstate,State.EMVHardwareErrorNextstate,State.EMVSequenceErrorNextstate, 
                "000","000","000",State.ConfigId, State.BrandId, State.ConfigVersion);
            SqlStringList.Add(sql);

            return SqlStringList;
        }

        private void SetDefaultValues()
        {
            StateType = "e";
            StateDescription = "Generate 2nd AC/AAC cancellation";
            _ChipCardOperation = "005";
            _SecondACSuccessfulNextstate = "255";            
            _EMVAppErrorNextstate = "255";
            _EMVHardwareErrorNextstate = "255";
            _EMVSequenceErrorNextstate = "255";
        }


        public override Object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            Statee005 state = new Statee005();
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            state.Status = processRow[1].ToString();
            state.StateNumber = processRow[3].ToString();
            state._Description= processRow[4].ToString();
            state.StateType = processRow[5].ToString();

            state._ChipCardOperation = processRow[8].ToString();
            state._SecondACSuccessfulNextstate = processRow[9].ToString();
            state._EMVAppErrorNextstate = processRow[10].ToString();
            state._EMVHardwareErrorNextstate = processRow[11].ToString();
            state._EMVSequenceErrorNextstate = processRow[12].ToString();

            state.ConfigId = processRow[16].ToString();
            state.BrandId = processRow[17].ToString();
            state.ConfigVersion = processRow[18].ToString();

            ////NextState Kontrolu
            if (state.SecondACSuccessfulNextstate != "255")
                ChildobjList.Add(GetChildState("SecondACSuccessfulNextstate", state.SecondACSuccessfulNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.EMVAppErrorNextstate != "255")
                ChildobjList.Add(GetChildState("EMVAppErrorNextstate", state.EMVAppErrorNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.EMVHardwareErrorNextstate != "255")
                ChildobjList.Add(GetChildState("EMVHardwareErrorNextstate", state.EMVHardwareErrorNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
            if (state.EMVSequenceErrorNextstate != "255")
                ChildobjList.Add(GetChildState("EMVSequenceErrorNextstate", state.EMVSequenceErrorNextstate, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));


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