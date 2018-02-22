using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using ATMDesigner.UI.Controls;
using ATMDesigner.UI.ViewModel;
using ATMDesigner.UI.Model;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid;
using ATMDesigner.Common;
using ATMDesigner.Business.Interfaces;
using ATMDesigner.UI.Services;
using System.Collections.ObjectModel;
using System.Configuration;

namespace ATMDesigner.UI
{
    public partial class ViewModelDesignerCanvas : Canvas
    {
        //public ITransactionStateBusiness StateBusiness;
        
        private Point? rubberbandSelectionStartPoint = null;

        private ViewModelSelectionService selectionService;
        internal ViewModelSelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new ViewModelSelectionService(this);

                return selectionService;
            }
        }

        public List<ModelCanvasStateObject>TransactionList = new List<ModelCanvasStateObject>();      
        public List<ModelAtmConfig> atmConfigList;// = new List<ModelAtmConfig>();
        public List<ModelAtmBrand> atmBrandList;// = new List<ModelAtmBrand>();
        public List<string> avaliableStateNumberList;// = new List<string>();
       
        public List<string> ProjectList;// = new List<string>();
        public List<string> TransactionNameList;// = new List<string>();
     
        public List<string> StateIdListFromProjectUpload = new List<string>();
        
        public List<ModelAtmConfig> AtmConfigList
        {
            get
            {
                if (atmConfigList == null)
                    atmConfigList = new List<ModelAtmConfig>();
                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                atmConfigList = StateBusiness.AtmConfigList();
                return atmConfigList;
            }
        }
        public List<string> AvaliableStateNumberList
        {
            get
            {
                if (avaliableStateNumberList == null)
                    avaliableStateNumberList = new List<string>();
                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                avaliableStateNumberList = StateBusiness.AvaliableStateNumberList();
               
                return avaliableStateNumberList;
            }
        }
        public List<ModelAtmBrand> AtmBrandList
        {
            get
            {
                if (atmBrandList == null)
                    atmBrandList = new List<ModelAtmBrand>();
                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                atmBrandList = StateBusiness.AtmBrandList();
                return atmBrandList;
            }
        }
      
        public ObservableCollection<TreeViewItem> items = new ObservableCollection<TreeViewItem>();

        public string ProjectName { get; set; }
        public string CurrentTransactionName { get; set; }
        public string CurrentBrandId { get; set; }
        public string CurrentConfigId { get; set; }      
      
        // keep track of selected items 
        private List<IModelSelectable> selectedItems;
        public List<IModelSelectable> SelectedItems
        {
            get
            {
                if (selectedItems == null)
                    selectedItems = new List<IModelSelectable>();
                return selectedItems;
            }
            set
            {
                selectedItems = value;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                SelectionService.ClearSelection();
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    ViewModelRubberbandAdorner adorner = new ViewModelRubberbandAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            Type t=e.GetType();
            base.OnDrop(e);
            ModelDragObject dragObject = e.Data.GetData(typeof(ModelDragObject)) as ModelDragObject;
            ModelCanvasStateObject state = new ModelCanvasStateObject();
            PropertyGrid SelectedPgrid=new PropertyGrid();
            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {             
                ViewModelDesignerItem newItem = null;
                Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));
                if (content != null)
                {
                    newItem = new ViewModelDesignerItem();
                    DockPanel pnl = (DockPanel)content;

                    string StateBrandName = pnl.ToolTip.ToString();
                    if (StateBrandName != "P" && StateBrandName != "GENERIC")
                    {
                        string StateBrandId = ConfigurationManager.AppSettings[StateBrandName];
                        if (CurrentBrandId != StateBrandId)
                        {
                            return;
                        }
                    }
                 

                    state.Id = avaliableStateNumberList[0].ToString();
                    avaliableStateNumberList.Remove(state.Id);
                   // GetStateType(pnl.Tag.ToString());                    
                    state.Type = pnl.Tag.ToString();
                    pnl.Name = state.Type + state.Id.ToString();
                    pnl.Uid = state.Id;
                    state.dockPanel = pnl;
                    content = pnl;

                    Type ClassType = Type.GetType("ATMDesigner.UI.States.State" + pnl.Tag.ToString());
                    Object ClassInstance = Activator.CreateInstance(ClassType,this);

                    PropertyInfo StateNo = ClassType.GetProperty("StateNumber");
                    StateNo.SetValue(ClassInstance, state.Id, null);
                    PropertyInfo BrandId = ClassType.GetProperty("BrandId");
                    BrandId.SetValue(ClassInstance, CurrentBrandId, null);
                    PropertyInfo ConfigId = ClassType.GetProperty("ConfigId");
                    ConfigId.SetValue(ClassInstance, CurrentConfigId, null);
                    
                    SelectedPgrid.SelectedObject = ClassInstance;
                    SelectedPgrid.SelectedObjectName = state.Id;

                    state.PropertyGrid = SelectedPgrid;
                    state.TransactionName = CurrentTransactionName;
                    state.BrandId = CurrentBrandId;
                    state.ConfigId = CurrentConfigId;
                    newItem.Content = content;
                    Point position = e.GetPosition(this);

                    if (dragObject.DesiredSize.HasValue)
                    {
                        Size desiredSize = dragObject.DesiredSize.Value;
                        newItem.Width = desiredSize.Width;
                        newItem.Height = desiredSize.Height;

                        ViewModelDesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
                        ViewModelDesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));
                    }
                    else
                    {
                        ViewModelDesignerCanvas.SetLeft(newItem, Math.Max(0, position.X));
                        ViewModelDesignerCanvas.SetTop(newItem, Math.Max(0, position.Y));
                    }
                    newItem.StateName = state.Type;
                    newItem.StateNumber = state.Id;
                    TransactionList.Add(state);     
                    Canvas.SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);                    
                    SetConnectorDecoratorTemplate(newItem);
                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();                 
                }

                e.Handled = true;
            }
           
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        private void SetConnectorDecoratorTemplate(ViewModelDesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = ViewModelDesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }

     
        //MVVM mimarisine uygun olması için bunlar kullanılacak daha sonra
        //protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        //{

        //}

        //protected override void OnPreviewDrop(DragEventArgs e)
        //{

        //}
        

    
    }
}
