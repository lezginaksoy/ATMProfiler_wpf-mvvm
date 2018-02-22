using System;
using System.ComponentModel;
using System.Data;
using System.Collections;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Drawing.Design;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Reflection;
using ATMDesigner.Common;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 

    public class StateD : StateBase
    {

        public StateD(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            SetDefaultValues();
        }

        public StateD()
        {
            SetDefaultValues();
        }

     
        #region Converts

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
                StateD stated = (StateD)SelectedPgrid.SelectedObject;

                Type ClassType = stated.GetType();
                PropertyInfo propertyName = ClassType.GetProperty(SelectedProperty);
                propertyName.SetValue(stated, newValue, null);
                designerCanvas.TransactionList.Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = stated;
            }

        }
        
    
        #endregion

        #region State Parameters

        private string _NextState;
        private string _clearMask;
        private string _APreSetMask;
        private string _BPreSetMask;
        private string _CPreSetMask;
        private string _DPreSetMask;
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
                _NextState = value.ToString().PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"),PropertyOrder(2), DescriptionAttribute("Clear Mask")]
        [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]       
        public string ClearMask
        {
            get
            {
                return _clearMask;
            }
            set
            {
                _clearMask = value;
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(3), DescriptionAttribute("A Pre-Set Mask")]
        [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]       
        public string APreSetMask
        {
            get
            {
                return _APreSetMask;
            }
            set
            {
                _APreSetMask = value;
            }
        }

        [CategoryAttribute("State Parameters"),PropertyOrder(4), DescriptionAttribute("B Pre-Set Mask")]
        [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
        public string BPreSetMask
        {
            get
            {
                return _BPreSetMask;
            }
            set
            {
                _BPreSetMask = value;
            }
        }

        [CategoryAttribute("State Parameters"),PropertyOrder(5), DescriptionAttribute("C Pre-Set Mask")]
        [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]       
        public string CPreSetMask
        {
            get
            {
                return _CPreSetMask;
            }
            set
            {
                _CPreSetMask = value;
            }
        }

        [CategoryAttribute("State Parameters"),PropertyOrder(6), DescriptionAttribute("D Pre-Set Mask")]
        [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
        public string DPreSetMask
        {
            get
            {
                return _DPreSetMask;
            }
            set
            {
                _DPreSetMask = value;
                
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(7), DescriptionAttribute("Reserved"),ReadOnlyAttribute(true)]
        public string Reserved
        {
            get
            {
                return _Reserved;
            }

        }

        
        #endregion
        
        #region ExtensionState Parameters
   
        private string _ExtensionStateNumber;
        private string _ExtensionDescription;
        public  string _FPreSetMask;
        public  string _GPreSetMask;
        public  string _HPreSetMask;
        public  string _IPreSetMask;
        private string _ExtensionReserved;

        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension Parameters"), PropertyOrder(5), DescriptionAttribute("Extension State Number")]
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
        
        [CategoryAttribute("State Extension Parameters"),PropertyOrder(6), DescriptionAttribute("Extension Type"),ReadOnlyAttribute(true)]
        public string ExtensionType
        {
            get
            {
                return "Z";
            }

        }
      
        [CategoryAttribute("State Extension Parameters"),PropertyOrder(7),DescriptionAttribute("Extension Description")]
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
        
       [CategoryAttribute("State Extension Parameters"),PropertyOrder(8), DescriptionAttribute("F Pre-Set Mask")]
       [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
        public string FPreSetMask
        {
            get
            {
                return _FPreSetMask;
            }
            set
            {
                _FPreSetMask = value;
            }
        }

       [CategoryAttribute("State Extension Parameters"), PropertyOrder(9), DescriptionAttribute("G Pre-Set Mask")]
       [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
       public string GPreSetMask
       {
           get
           {
               return _GPreSetMask;
           }
           set
           {
               _GPreSetMask = value;
           }
       }

       [CategoryAttribute("State Extension Parameters"), PropertyOrder(10), DescriptionAttribute("H Pre-Set Mask")]
       [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
       public string HPreSetMask
       {
           get
           {
               return _HPreSetMask;
           }
           set
           {
               _HPreSetMask = value;
           }
       }

       [CategoryAttribute("State Extension Parameters"), PropertyOrder(11), DescriptionAttribute("I Pre-Set Mask")]
       [Editor(typeof(AssignmentFDKMaskEditor), typeof(AssignmentFDKMaskEditor))]
       public string IPreSetMask
       {
           get
           {
               return _IPreSetMask;
           }
           set
           {
               _IPreSetMask = value;
           }
       }

       [CategoryAttribute("State Extension Parameters"), PropertyOrder(12), DescriptionAttribute("Reserved"), ReadOnlyAttribute(true)]
       public string ExtensionReserved
       {
           get
           {
               return _ExtensionReserved;
           }

       }

       #endregion
          
        #region Events and Methods

        public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            //FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
            StateD Selectedstate = new StateD();
            StateD Dynamicstate = new StateD();

            Selectedstate = (StateD)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateD)ClassInstance;
            Dynamicstate._Description = Selectedstate.StateDescription;
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;

            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;            
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._APreSetMask = Selectedstate.APreSetMask;
            Dynamicstate._BPreSetMask = Selectedstate.BPreSetMask;
            Dynamicstate._clearMask = Selectedstate.ClearMask;
            Dynamicstate._CPreSetMask = Selectedstate.CPreSetMask;
            Dynamicstate._DPreSetMask = Selectedstate.DPreSetMask;
            Dynamicstate._FPreSetMask = Selectedstate.FPreSetMask;
            Dynamicstate._GPreSetMask = Selectedstate.GPreSetMask;
            Dynamicstate._HPreSetMask = Selectedstate.HPreSetMask;
            Dynamicstate._IPreSetMask = Selectedstate.IPreSetMask;
            Dynamicstate._NextState = Selectedstate.NextState;          

            return Dynamicstate;
        }

        public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
        {
            StateD Selectedstate = new StateD();
            StateD Dynamicstate = new StateD();

            Selectedstate = (StateD)SelectedPgrid.SelectedObject;
            Dynamicstate = (StateD)ClassInstance;

            Dynamicstate._Description = Selectedstate.StateDescription;        
            Dynamicstate.BrandId = Selectedstate.BrandId;
            Dynamicstate.ConfigId = Selectedstate.ConfigId;
            Dynamicstate._APreSetMask = Selectedstate.APreSetMask;
            Dynamicstate._BPreSetMask = Selectedstate.BPreSetMask;
            Dynamicstate._clearMask = Selectedstate.ClearMask;
            Dynamicstate._CPreSetMask = Selectedstate.CPreSetMask;
            Dynamicstate._DPreSetMask = Selectedstate.DPreSetMask;
            Dynamicstate._FPreSetMask = Selectedstate.FPreSetMask;
            Dynamicstate._GPreSetMask = Selectedstate.GPreSetMask;
            Dynamicstate._HPreSetMask = Selectedstate.HPreSetMask;
            Dynamicstate._IPreSetMask = Selectedstate.IPreSetMask;
            Dynamicstate._ExtensionDescription = Selectedstate.ExtensionDescription;
            Dynamicstate._ExtensionStateNumber = Selectedstate.ExtensionStateNumber;            
           
            //Dynamicstate._nextState = Selectedstate._nextState;

            return Dynamicstate;
        }

        public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
        {
            StateD Stated = new StateD();
            Stated = (StateD)SelectedPgrid.SelectedObject;
            List<string> SqlStringList = new List<string>();

            string sql = SqlStr;
            //Extension
            string exsql = sql;
            if (Stated.ExtensionStateNumber != "255")
            {
                exsql = string.Format(exsql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), Stated.ExtensionStateNumber, Stated.ExtensionDescription,
                Stated.ExtensionType, ProjectName, TransactionName, Stated.FPreSetMask, Stated.GPreSetMask, Stated.HPreSetMask, Stated.IPreSetMask,
                Stated.ExtensionReserved, Stated.ExtensionReserved, Stated.ExtensionReserved, Stated.ExtensionReserved,
                Stated.ConfigId, Stated.BrandId, Stated.ConfigVersion);
                SqlStringList.Add(exsql);
            }

            sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), Stated.StateNumber, Stated.StateDescription,
                Stated.StateType, ProjectName, TransactionName, Stated.NextState, Stated.ClearMask, Stated.APreSetMask, Stated.BPreSetMask,
                Stated.CPreSetMask, Stated.DPreSetMask, Stated.Reserved, Stated.ExtensionStateNumber, Stated.ConfigId, Stated.BrandId, ConfigVersion);
            SqlStringList.Add(sql);


            return SqlStringList;
        }
  
        private void SetDefaultValues()
        {
            StateType = "D";
            StateDescription = "Pre‐Set Operation Code Buffer State";
            _ExtensionStateNumber = "255";
            _ExtensionDescription = "State Z";
            _NextState = "255";
            _clearMask = "000";
            _APreSetMask = "000";
            _BPreSetMask = "000";
            _CPreSetMask = "000";
            _DPreSetMask = "000";
            _FPreSetMask = "000";
            _GPreSetMask = "000";
            _HPreSetMask = "000";
            _IPreSetMask = "000";
            _Reserved = "000";
            _ExtensionReserved = "000";
        }



        public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
        {
            StateD stated = new StateD();         
            ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
            List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
            List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
            stated.Status = processRow[1].ToString();
            stated.StateNumber = processRow[3].ToString();
            stated.StateDescription = processRow[4].ToString();
            stated.StateType = processRow[5].ToString();
            stated._NextState = processRow[8].ToString();
            stated._clearMask = processRow[9].ToString();
            stated._APreSetMask = processRow[10].ToString();
            stated._BPreSetMask = processRow[11].ToString();
            stated._CPreSetMask = processRow[12].ToString();
            stated._DPreSetMask = processRow[13].ToString();
            stated._Reserved = processRow[14].ToString();
            stated._ExtensionStateNumber = processRow[15].ToString();
            stated.ConfigId = processRow[16].ToString();
            stated.BrandId = processRow[17].ToString();
            stated.ConfigVersion = processRow[18].ToString();

            //Extension State Kontrolu
            if (stated.ExtensionStateNumber != "255")
            {
                object[] ExtensionState = GetExtensionState(ref StateList, stated.ExtensionStateNumber);
                stated._ExtensionDescription = ExtensionState[4].ToString();
                stated._FPreSetMask = ExtensionState[8].ToString();
                stated._GPreSetMask = ExtensionState[9].ToString();
                stated._HPreSetMask = ExtensionState[10].ToString();
                stated._IPreSetMask = ExtensionState[11].ToString();
            }
            
            //NextState Kontrolu
            if (stated.NextState!="255")
            {
                ChildobjList.Add(GetChildState("NextState", stated.NextState, StateList, processRow[7].ToString(), stated.StateType, stated.StateNumber));                
            }
            
            TransStateObj.BrandId = stated.BrandId;
            TransStateObj.ConfigId = stated.ConfigId;
            TransStateObj.Id = stated.StateNumber;
            TransStateObj.StateDescription = stated.StateDescription;
            TransStateObj.Type = stated.StateType;
            TransStateObj.TransactionName = processRow[7].ToString();
                       
            TransStateObj.PropertyGrid.SelectedObject=stated;
            TransStateObj.ParentStateList = ParentobjList;
            TransStateObj.ChildStateList = ChildobjList;
            designerCanvas.TransactionList.Add(TransStateObj);

            return StateList;           
        }

        private object[]GetExtensionState(ref ArrayList StateList, string ExtensionStateNumber)
        {
            object[] ExtensionState=null;
            foreach(object[] StatedRow in StateList)
            {
                //Extension state sorgusu
                if (ExtensionStateNumber == StatedRow[3].ToString().PadLeft(3,'0'))
                {
                    ExtensionState= StatedRow;
                  //  StateList.Remove(StatedRow);
                    return ExtensionState;                                        
                }
            }

            return ExtensionState;
        }



        //private ModelParentStateObject GetParentState(string PropertyName, string NextState, ArrayList StateList,string TransName)
        //{
        //    ModelParentStateObject PState = new ModelParentStateObject();
        //    foreach (object[] StatedRow in StateList)
        //    {
        //        //diğer statelerle karşılaştırılması
        //        if (NextState == StatedRow[3].ToString().PadLeft(3, '0'))
        //        {
        //            PState.ParentId = StatedRow[3].ToString();
        //            PState.ParentType = StatedRow[5].ToString();
        //            PState.PropertyName = PropertyName;
        //            if (TransName != StatedRow[7].ToString())
        //            {
        //                //Pointer state Oluştur
                        
        //            }

        //            return PState;
        //        }
        //    }

        //    return PState;
        //}
        
        #endregion


     
    }
}