using ATMDesigner.Common;
using ATMDesigner.UI.Popups;
using ATMDesigner.UI.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
    [DefaultPropertyAttribute("Name")]
    [CategoryOrder("State Description", 1)]
    [CategoryOrder("State Parameters", 2)]
    [CategoryOrder("Send General Purpose Buffer Data",3)]
    [CategoryOrder("Reserve Parameters",4)]
    [CategoryOrder("State FDKs Active Mask",5)]
    [CategoryOrder("State Extension Parameters", 6)]    
    public  abstract class StateBase: ICloneable
    {
        public static ViewModelDesignerCanvas designerCanvas;

        public StateBase(ViewModelDesignerCanvas Canvas)
        {
            designerCanvas = Canvas;
            _Guid = "1";
            _Status = "1";
            _ConfigVersion = "1";

        }

        public StateBase()
        {
            _Guid = "1";
            _Status = "1";
            _ConfigVersion = "1";

        }

        #region State Description

        private string _StateNumber;
        public string _Description;
        private string _Type;
        
        [CategoryAttribute("State Description"), PropertyOrder(1), DescriptionAttribute("State Number")]
        [Editor(typeof(SetStateNumber), typeof(SetStateNumber))]
        public string StateNumber
        {
            get
            {
                return _StateNumber;
            }

            set
            {
                _StateNumber = value;
            }
        }

        [CategoryAttribute("State Description"),PropertyOrder(2), DescriptionAttribute("State Description")]       
        public string StateDescription
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        [CategoryAttribute("State Description"), PropertyOrder(3), DescriptionAttribute("State Type"), ReadOnlyAttribute(true)]       
        public string StateType
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value.Substring(0,1);
            }
        }


        #endregion

        #region Converts
        //Screen içinTest için
        public class AssignmentScreenButton : ITypeEditor
        {
            Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem property;
            public FrameworkElement ResolveEditor(PropertyItem propertyItem)
            {
                property = propertyItem;
                Button button = new Button();
                button.Name = property.DisplayName;
                button.Content = property.Value;
                button.Click += button_Click;
                return button;
            }
            
            void button_Click(object sender, RoutedEventArgs e)
            {
                Button btn = (Button)sender;             
                ScreenSelection scrnSelec = new ScreenSelection(btn.Content.ToString());
                scrnSelec.ShowDialog();
                btn.Content = scrnSelec.ScreenNumber;

                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }
                DockPanel Sourcepnl = (DockPanel)Ditem.Content;
                PropertyGrid SelectedPgrid = designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == Sourcepnl.Uid).PropertyGrid;

                string SelectedProperty = btn.Name.ToString();
                string newValue = btn.Content.ToString();
                Object stateobj = (Object)SelectedPgrid.SelectedObject;

                Type ClassType = stateobj.GetType();
                PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                property.SetValue(stateobj, newValue, null);
                designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = stateobj;


            }
        }

      
        public class SetStateNumber : ITypeEditor
        {
            Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem property;
            public FrameworkElement ResolveEditor(PropertyItem propertyItem)
            {
                property = propertyItem;
                Button button = new Button();
                button.Name = property.DisplayName;
                button.Content = property.Value;
                button.Click += button_Click;
                return button;
            }

            void button_Click(object sender, RoutedEventArgs e)
            {
                Button btn = (Button)sender;
                StateNumberSelection StateSelectionPopup = new StateNumberSelection(designerCanvas.avaliableStateNumberList, btn.Content.ToString(),designerCanvas.CurrentBrandId
                    ,designerCanvas.TransactionList);
                StateSelectionPopup.ShowDialog();

                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }
                DockPanel Sourcepnl = (DockPanel)Ditem.Content;

                
                //StateNumber deðiþmememiþ
                if (btn.Content.ToString()==StateSelectionPopup.StateNumber)
                {
                    return;   
                }

                //State number ve baðlantýlý yerlerde gerekli güncellemeler
                string ExStateId = btn.Content.ToString();
                string NewStateId = StateSelectionPopup.StateNumber;
            
                PropertyGrid SelectedPgrid = designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == ExStateId).PropertyGrid;

                Object state = (Object)SelectedPgrid.SelectedObject;
                if (state != null)
                {
                    btn.Content = NewStateId;
                    string SelectedProperty = btn.Name.ToString();
                    string SelectedPropertyValue = NewStateId;

                    //state güncellemesi
                    SelectedPgrid.SelectedObject.GetType().GetProperty("StateNumber").SetValue(SelectedPgrid.SelectedObject, SelectedPropertyValue);
                    SelectedPgrid.SelectedObjectName = SelectedPropertyValue;
                    
                    designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName)
                        .Find(x => x.Id == ExStateId).PropertyGrid = SelectedPgrid;
                    designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName)
                       .Find(x => x.Id == ExStateId).Id = SelectedPropertyValue;

                    Sourcepnl.Uid = SelectedPropertyValue;
                    Ditem.Content = Sourcepnl;
                    designerCanvas.avaliableStateNumberList.Remove(SelectedPropertyValue);

                    //parent State Güncellemesi                  
                    List<ModelParentStateObject> PStateList = new List<ModelParentStateObject>();
                    PStateList = designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == SelectedPropertyValue).ParentStateList;
                    if (PStateList.Count != 0)
                    {
                        for (int i = 0; i < PStateList.Count; i++)
                        {
                            SelectedPgrid = designerCanvas.TransactionList.Find(x => x.Id == PStateList[i].ParentId).PropertyGrid;
                            SelectedPgrid.SelectedObject.GetType().GetProperty(PStateList[i].PropertyName).SetValue(SelectedPgrid.SelectedObject, SelectedPropertyValue);
                            designerCanvas.TransactionList.Find(x => x.Id == PStateList[i].ParentId).PropertyGrid.SelectedObject = SelectedPgrid.SelectedObject;
                        }
                    }

                }
                else
                {
                   
                }


            }

        }

       
        public class SetExtensionStateNumber : ITypeEditor
        {
            Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem property;
            public FrameworkElement ResolveEditor(PropertyItem propertyItem)
            {
                property = propertyItem;
                Button button = new Button();
                button.Name = property.DisplayName;
                button.Content = property.Value;
                button.Click += button_Click;
                return button;
            }

            void button_Click(object sender, RoutedEventArgs e)
            {

                Button btn = (Button)sender;
                StateNumberSelection StateSelectionPopup = new StateNumberSelection(designerCanvas.avaliableStateNumberList, btn.Content.ToString(), designerCanvas.CurrentBrandId
                    , designerCanvas.TransactionList);
                StateSelectionPopup.ShowDialog();

                //BufferPositions bufferPopup = new BufferPositions(btn.Content.ToString());
                //bufferPopup.ShowDialog();
                btn.Content = StateSelectionPopup.StateNumber;


                var Selection = designerCanvas.SelectionService.CurrentSelection;
                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    Ditem = (ViewModelDesignerItem)slc;
                }
                DockPanel Sourcepnl = (DockPanel)Ditem.Content;
                PropertyGrid SelectedPgrid = designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == Sourcepnl.Uid).PropertyGrid;
                string SelectedProperty = btn.Name.ToString();
                string newValue = btn.Content.ToString();
                Object stateobj = (Object)SelectedPgrid.SelectedObject;

                Type ClassType = stateobj.GetType();
                PropertyInfo property = ClassType.GetProperty(SelectedProperty);
                property.SetValue(stateobj, newValue, null);
                designerCanvas.TransactionList.FindAll(x => x.TransactionName == designerCanvas.CurrentTransactionName).Find(x => x.Id == Sourcepnl.Uid).PropertyGrid.SelectedObject = stateobj;
            }
        }




        #endregion

        #region Events And Methods
        
        public virtual void Clear()
        {
            StateNumber = "";
            StateDescription = "";
        }
        
        public abstract Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance, PropertyGrid SelectedPgrid);

        public abstract Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid);
     
        public abstract Object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber);

        public abstract Object FillStatesFromDB(object[] processRow, ArrayList StateList);

        public ModelChildStateObject GetChildState(string PropertyName, string NextState, ArrayList StateList, string TransName,
            string SourceStateType,string SourceStateNumber)
        {
           ModelChildStateObject ChildState = new ModelChildStateObject();
            foreach (object[] StatedRow in StateList)
            {
                //diðer statelerle karþýlaþtýrýlmasý
                if (NextState == StatedRow[3].ToString().PadLeft(3, '0'))
                {
                    ChildState.ChildId = StatedRow[3].ToString();
                    ChildState.ChildType = StatedRow[5].ToString();
                    ChildState.PropertyName = PropertyName;

                    return ChildState;
                }
            }

            return ChildState;
        }
        

        #endregion

        #region INotifyPropertyChanged Members

        // we could use DependencyProperties as well to inform others of property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Common State Parameters and Properties
        private string _Guid;
        private string _Status;
        private string _ConfigId;
        private string _BrandId;
        private string _ConfigVersion;


        [ReadOnlyAttribute(true),BrowsableAttribute(false)]
        public string Guid
        {
            get
            {
                return _Guid;
            }

            set
            {
                _Guid = value;
            }
        }

        [ReadOnlyAttribute(true), BrowsableAttribute(false)]
        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        [BrowsableAttribute(false)]
        public string ConfigId
        {
            get
            {
                return _ConfigId;
            }

            set
            {
                _ConfigId = value;
            }
        }

        [BrowsableAttribute(false)]
        public string BrandId
        {
            get
            {
                return _BrandId;
            }

            set
            {
                _BrandId = value;
            }
        }

        [ReadOnlyAttribute(true), BrowsableAttribute(false)]
        public string ConfigVersion
        {
            get
            {
                return _ConfigVersion;
            }

            set
            {
                _ConfigVersion = value;
            }
        }
       
        [ReadOnlyAttribute(true), BrowsableAttribute(false)]
        public string SqlStr
        {
            get
            {
                return @"INSERT INTO OC_ATM.ATM_STATE_DATA_DESGNR(GUID,STATUS,LASTUPDATED,STATE_ID,STATE_DSCR,STATE_TYPE,PROJECT_NAME,TRANS_NAME,PRM1,PRM2,PRM3,PRM4,PRM5,PRM6,PRM7,PRM8,CONFIG_ID,BRAND_ID,CONFIG_VERSION)
                            VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8:000}','{9:000}','{10:000}','{11:000}','{12:000}','{13:000}','{14:000}','{15:000}','{16}','{17}','{18}')";
            }        

        }


        #endregion



        //public virtual void StateChanged(PropertyValueChangedEventArgs e, DataGridView dataGridView1)
        //{
            
        //    if (e.ChangedItem.Label == "StateNumber")
        //    {
        //        dataGridView1.CurrentRow.Cells[0].Value = e.ChangedItem.Value.ToString();
        //        if (dataGridView1.CurrentRow.Cells[0].Value.ToString() == dataGridView1.CurrentRow.Cells[0].ToolTipText.ToString())
        //            dataGridView1.CurrentRow.Cells[0].Style.BackColor = Color.White;
        //        else
        //            dataGridView1.CurrentRow.Cells[0].Style.BackColor = Color.Red;

        //    }
        //    else if (e.ChangedItem.Label == "StateDescription")
        //    {
        //        dataGridView1.CurrentRow.Cells[1].Value = e.ChangedItem.Value.ToString();
        //        if (dataGridView1.CurrentRow.Cells[1].Value.ToString() == dataGridView1.CurrentRow.Cells[1].ToolTipText.ToString())
        //            dataGridView1.CurrentRow.Cells[1].Style.BackColor = Color.White;
        //        else
        //            dataGridView1.CurrentRow.Cells[1].Style.BackColor = Color.Red;
        //    }
        //    else if (e.ChangedItem.Label == "StateType")
        //    {
        //        dataGridView1.CurrentRow.Cells[2].Value = e.ChangedItem.Value.ToString();
        //        if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == dataGridView1.CurrentRow.Cells[2].ToolTipText.ToString())
        //            dataGridView1.CurrentRow.Cells[2].Style.BackColor = Color.White;
        //        else
        //            dataGridView1.CurrentRow.Cells[2].Style.BackColor = Color.Red;
        //    }
           

            
        //}



        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}