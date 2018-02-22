using ATMDesigner.UI.Controls;
using ATMDesigner.UI.Model;
using ATMDesigner.UI.Popups;
using ATMDesigner.UI.States;
using ATMDesigner.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using Xceed.Wpf.Toolkit.PropertyGrid;
using ATMDesigner.Common;
using ATMDesigner.Business.Interfaces;
using ATMDesigner.UI.Services;
using System.Configuration;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
using ATMDesigner.UI.Helpers;
using System.Threading;
using System.Windows.Threading;

namespace ATMDesigner.UI.View
{
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);    
        PropertyGrid SelectedPgrid;
        List<PropertyGrid> PropertyGridList = new List<PropertyGrid>();
        public ITransactionStateBusiness StateBusiness;
        public ITransactionScreenBusiness ScreenBusiness;
        public static RoutedCommand Go_To = new RoutedCommand();
        ATMLogger logger = new ATMLogger();
        private ProgressBarWindow pbw = null;
        
     
        public MainWindow()
        {            
            InitializeComponent();
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, New_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,Open_Executed));
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
            this.CommandBindings.Add(new CommandBinding(MainWindow.Go_To, GoTo_NextState_Trans));
            this.CommandBindings.Add(new CommandBinding(ViewModelDesignerCanvas.Transfer,this.TransactionDiagram.Transfer_Executed));
           
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBoxResult.No == MessageBox.Show("Kapatmak istediğinize emin misiniz?", "ATM State Designer", MessageBoxButton.YesNo))
            {
                e.Cancel = true;
            }
            else
            {
                log.Info("ATMDesigner Closed !");
            }
            
           
        }
                        
        #region New Project Command

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {         
            if (TreeFlow.Items.Count > 0)
            {
                if (MessageBox.Show("Zaten Mevcut Bir Proje Var.Bu Kurulumda Sadece bir Proje ile çalışabilirsiniz.!. Devam etmek istediğinize emin misiniz?", "Yeni Proje", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    NewProject();
                }
                else
                {
                    return;
                }
            }
            else
            {
                NewProject();   
            }
            ExpanderTree.IsExpanded = true;
            
        }

        private void NewProject()
        {
            string NewProjectName = "";
            ClearClipBoard();
            StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
            
            this.TransactionDiagram.ProjectList = StateBusiness.AtmProjectList();
            NewProject newProject = new NewProject(this.TransactionDiagram.ProjectList);
            if (newProject.ShowDialog() == true)
            {
                NewProjectName = newProject.NewProjectName;
                TreeViewItem newChild = new TreeViewItem();
                newChild.Header = NewProjectName;
                newChild.IsSelected = true;
                newChild.MouseRightButtonDown += OpenMenu_MouseRightButtonDown;             
                TreeFlow.Items.Add(newChild);
                this.TransactionDiagram.ProjectName = NewProjectName;
                this.TransactionDiagram.TransactionNameList = StateBusiness.AtmTransactionList(this.TransactionDiagram.ProjectName);           
            }
        }
     
        #endregion

        #region Open Project Command
   
        public void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (TreeFlow.Items.Count > 0)
            {
                if (MessageBox.Show("Zaten Mevcut Bir Proje Var.Bu Kurulumda Sadece bir Proje ile çalışabilirsiniz.!. Devam etmek istediğinize emin misiniz?", "Yeni Proje", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                    {
                        GetProjectList();
                    }));

                    pbw = new ProgressBarWindow();
                    pbw.Owner = this;
                    pbw.ShowDialog();

                }
                else
                {
                    return;
                }
            }
            else
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                {
                    GetProjectList();
                }));

                pbw = new ProgressBarWindow();
                pbw.Owner = this;
                pbw.ShowDialog();

            }
          

        }
        
        void GetProjectList()
        {
            try
            {
                log.Info("GetProjectList() Start.!");
                ClearClipBoard();
                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                this.TransactionDiagram.ProjectList = StateBusiness.AtmProjectList();

                if (this.TransactionDiagram.ProjectList.Count != 0 && this.TransactionDiagram.ProjectList != null)
                {
                    OpenProject openProject = new OpenProject(this.TransactionDiagram.ProjectList);
                    if (openProject.ShowDialog() == true)
                    {
                        if (!this.TransactionDiagram.CopyProjectXmlsDirectory())
                        {
                            pbw.Close();
                            return;
                        }
                            this.TransactionDiagram.ProjectName = openProject.ProjectName;
                        GetTransactionList();

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetProjectList() Error.! " + ex.Message);
            }
            finally
            {
                pbw.Close();
            }
        }

        bool GetTransactionList()
        {
            bool Retval = false;
            try
            {
                log.Info("GetTransactionList() start.!");
                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                this.TransactionDiagram.TransactionNameList = StateBusiness.AtmTransactionList(this.TransactionDiagram.ProjectName);

                TreeViewItem parentNode = new TreeViewItem();
                parentNode.Header = this.TransactionDiagram.ProjectName;
                parentNode.IsSelected = true;
                parentNode.MouseRightButtonDown += OpenMenu_MouseRightButtonDown; 

                this.TransactionDiagram.CurrentTransactionName = this.TransactionDiagram.TransactionNameList[0];               
                this.TransactionDiagram.Open_Executed(true, this.TransactionDiagram.CurrentTransactionName);
                
                for (int i = 0; i < this.TransactionDiagram.TransactionNameList.Count; i++)
                {
                    TreeViewItem TransChild = new TreeViewItem();
                    TransChild.Header = this.TransactionDiagram.TransactionNameList[i];
                    if (this.TransactionDiagram.TransactionList.Exists(x => x.TransactionName == TransChild.Header.ToString()))
                    {
                        TransChild.Tag = this.TransactionDiagram.TransactionList.Find(x => x.TransactionName == TransChild.Header.ToString()).BrandId;
                        TransChild.ToolTip = "BrandID : " + TransChild.Tag;

                    }
                    TransChild.MouseDoubleClick += OpenDiagramTree_MouseDoubleClick;
                    parentNode.Items.Add(TransChild);
               }

                TreeFlow.Items.Add(parentNode);
                ExpanderTree.IsExpanded = true;
             
                TransDiagramName.Header = this.TransactionDiagram.CurrentTransactionName;

                Retval = true;
            }
            catch (Exception ex)
            {
                log.Error("GetTransactionList() Error.! " + ex);
                Retval = false;
            }

            return Retval;
        }

        void ClearClipBoard()
        {
            this.TransactionDiagram.Children.Clear();
            this.TransactionDiagram.SelectionService.ClearSelection();
            this.TransactionDiagram.TransactionList.Clear();
            propertyGrid.SelectedObject = null;
            propertyGrid.SelectedObjectName = "";
            TreeFlow.Items.Clear();
            ExpanderDItem.IsExpanded = false; 
        }
 
        #endregion

        #region TreeView proccess
        
        //Open Menu FlowTree_ Event
        public void OpenMenu_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.ContextMenu cm = this.FindResource("MenuAdd") as System.Windows.Controls.ContextMenu;
            cm.PlacementTarget = sender as System.Windows.Controls.Button;
            cm.IsOpen = true;
        }

        //Add Transaction(childitem) to Tree Flow
        public void AddTransaction_Event(object sender, RoutedEventArgs e)
        {
            string NewTransaction = "";
            NewTransaction newTrans = new NewTransaction(TransactionDiagram.atmBrandList,TransactionDiagram.atmConfigList,
                TransactionDiagram.TransactionNameList);
            TreeViewItem parentNode = new TreeViewItem();
            parentNode = TreeFlow.SelectedItem as TreeViewItem;
            ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(parentNode);           
            if (parent != null && parent.GetType() == typeof(TreeViewItem))
            {
                log.Error("AddTransaction_Event() parent is null.!");
                ExpanderDItem.IsExpanded = false;
                return;
            }
            else if (newTrans.ShowDialog() == true)
            {
                NewTransaction = newTrans.NewTransactionName;
                TransactionDiagram.CurrentBrandId = newTrans.BrandId;
                TransactionDiagram.CurrentConfigId = newTrans.ConfigId;
                TransactionDiagram.TransactionNameList.Add(newTrans.NewTransactionName);
                TreeViewItem TransChild = new TreeViewItem();
                TransChild.Header = NewTransaction;
                TransChild.Tag = newTrans.BrandId;
                TransChild.ToolTip ="BrandID : "+newTrans.BrandId;

                TransChild.MouseDoubleClick += OpenDiagramTree_MouseDoubleClick;                
                TreeFlow.Items.Remove(parentNode);
                parentNode.Items.Add(TransChild);
                parentNode.IsSelected = true;
                if (parentNode.Items.Count==1)
                {
                    TransDiagramName.Header = TransChild.Header;
                }
                TreeFlow.Items.Add(parentNode);
                ExpanderDItem.IsExpanded = true;  
            }

        }
     
        //Open a Diagram for that transaction(daha önce kayıtlı bir akış varsa DeSerialize yoksa yeni bir canvas )
        public void OpenDiagramTree_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem TransChild = new TreeViewItem();
            TransChild = (TreeViewItem)sender;

            string ExTransactionName = this.TransactionDiagram.CurrentTransactionName;
            this.TransactionDiagram.CurrentTransactionName = TransChild.Header.ToString();                
            if (!string.IsNullOrEmpty(ExTransactionName))
             this.TransactionDiagram.Open_Executed(false, ExTransactionName);

            this.TransactionDiagram.CurrentBrandId = TransChild.Tag.ToString();
            propertyGrid.SelectedObject = null;
            propertyGrid.SelectedObjectName = "";
            TransDiagramName.Header = TransChild.Header;
            ExpanderDItem.IsExpanded = IsTransactionSelected();
        }  
       

        #endregion

        #region Properties Proccess

        //Propertygrid'te yapılan her değişiklikte tetiklenecek
        private void propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            SelectedPgrid = new PropertyGrid();
            SelectedPgrid = (PropertyGrid)sender;

            string SelectedProperty = SelectedPgrid.SelectedProperty.ToString();
            string newValue = e.NewValue.ToString();

            Type ClassType = SelectedPgrid.SelectedObject.GetType();
            Object ClassInstance = Activator.CreateInstance(ClassType);
            PropertyInfo StateNo = ClassType.GetProperty("StateNumber");
            StateNo.SetValue(ClassInstance, SelectedPgrid.SelectedObjectName, null);

            object[] parametersArray = new object[] { SelectedProperty, newValue, ClassInstance, SelectedPgrid };
            object result = ClassType.InvokeMember("StateChanged", BindingFlags.InvokeMethod, null, ClassInstance, parametersArray);
            this.TransactionDiagram.TransactionList.FindAll(x=>x.TransactionName==this.TransactionDiagram.CurrentTransactionName).Find(x => x.Id == SelectedPgrid.SelectedObjectName).PropertyGrid.SelectedObject = result;
            propertyGrid.SelectedObject = result;       



        //    SelectedPgrid = new PropertyGrid();
        //    SelectedPgrid = (PropertyGrid)sender;
        //   Object Obj = SelectedPgrid.SelectedObject;

        //    this.TransactionDiagram.TransactionList.FindAll(x => x.TransactionName == this.TransactionDiagram.CurrentTransactionName).
        //        Find(x => x.Id == SelectedPgrid.SelectedObjectName).PropertyGrid.SelectedObject = Obj;
        //    propertyGrid.SelectedObject = Obj;
        
        }
     
        //Propertygrid screennumber için oluşturuldu
        private void propertyGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectedPgrid = new PropertyGrid();
            SelectedPgrid = (PropertyGrid)sender;
            if (SelectedPgrid == null || SelectedPgrid.SelectedProperty == null)
                return;
            if (!SelectedPgrid.SelectedProperty.ToString().EndsWith("Screen"))
                return;
              
            
            string SelectedProperty = SelectedPgrid.SelectedProperty.ToString();       
            Type ClassType = SelectedPgrid.SelectedObjectType;
            PropertyInfo PropScreenId = ClassType.GetProperty(SelectedProperty);
            string ScreenId = PropScreenId.GetValue(SelectedPgrid.SelectedObject).ToString();
           
            ScreenBusiness = ApplicationServicesProvider.Instance.Provider.TransactionScreenService;
            string data = ScreenBusiness.GetScreenData(ScreenId);
            ExpScreenPanel.Content = data;
        }

        //ViewProperties on mouseupclick
        private void TransactionDiagram_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {           
            var Selection = TransactionDiagram.SelectionService.CurrentSelection;
            if (Selection.Count!=1)
            {             
                propertyGrid.SelectedObject = null;
                propertyGrid.SelectedObjectName = "";
                return;
            }

            ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
            foreach (var slc in Selection)
            {
                if (slc.GetType().Name.ToString() != "ViewModelDesignerItem")
                    return;
                Ditem = (ViewModelDesignerItem)slc;
            }
            DockPanel Sourcepnl = (DockPanel)Ditem.Content;
          
             if (this.TransactionDiagram.TransactionList.Exists(x => x.Id == Sourcepnl.Uid))
            {               
                propertyGrid.SelectedObject = this.TransactionDiagram.TransactionList.FindAll(x => x.Id == Sourcepnl.Uid).Find
                    (x => x.TransactionName ==this.TransactionDiagram.CurrentTransactionName).PropertyGrid.SelectedObject;
               propertyGrid.SelectedObjectName = Sourcepnl.Uid;
            }
            else
            {
                MessageBox.Show("Bir Hata Oluştu.");
            }           

        }
       
        //ViewProperties on Drop to canvas
        private void TransactionDiagram_PreviewDrop(object sender, DragEventArgs e)
        {
            ViewModelDesignerCanvas dcanvas = new ViewModelDesignerCanvas();
            dcanvas = (ViewModelDesignerCanvas)sender;
            SelectedPgrid = new PropertyGrid();

            TreeViewItem ChildNode = new TreeViewItem();
            ChildNode = TreeFlow.SelectedItem as TreeViewItem;
            ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(ChildNode);
            TreeViewItem parentNode = parent as TreeViewItem;
            if (parentNode != null && !String.IsNullOrEmpty(parentNode.Header.ToString()))
            {
                dcanvas.ProjectName = parentNode.Header.ToString();
                dcanvas.CurrentTransactionName = TransDiagramName.Header.ToString();

                ModelDragObject dragObject = e.Data.GetData(typeof(ModelDragObject)) as ModelDragObject;
                ModelCanvasStateObject state = new ModelCanvasStateObject();

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
                            if (dcanvas.CurrentBrandId != StateBrandId)
                            {
                                MessageBox.Show("Kullanmak istediğiniz State " + StateBrandName + " için sadece kullanılabilir, lütfen Mevcut Brande uygun State kullanınız");
                                return;
                            }
                        }

                        string frmName = "State" + pnl.Tag.ToString();
                        string Id = dcanvas.avaliableStateNumberList[0].ToString();
                        Type ClassType = Type.GetType("ATMDesigner.UI.States." + frmName);
                        Object ClassInstance = Activator.CreateInstance(ClassType);
                        PropertyInfo StateNo = ClassType.GetProperty("StateNumber");
                        StateNo.SetValue(ClassInstance, Id, null);

                        propertyGrid.SelectedObject = ClassInstance;
                        propertyGrid.SelectedObjectName = Id;

                        SelectedPgrid.SelectedObject = ClassInstance;
                        SelectedPgrid.SelectedObjectName = Id;

                        PropertyGridList.Add(SelectedPgrid);

                    }
                }

            }
        }

        #endregion    

        #region Expander-DesignerItem 

        private void ExpanderDItem_Expanded(object sender, RoutedEventArgs e)
        {
            ExpanderDItem.IsExpanded = IsTransactionSelected();
        }

        bool IsTransactionSelected()
        {
            TreeViewItem parentNode = new TreeViewItem();
            parentNode = TreeFlow.SelectedItem as TreeViewItem;
            ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(parentNode);
            if (parent != null && parent.GetType() == typeof(TreeViewItem))
            {
                string a = parentNode.Header.ToString();
                ExpanderDItem.IsExpanded = true;
                return true;
            }
            else
            {
                ExpanderDItem.IsExpanded = false;
                return false;
            }
           
        }

        #endregion

        #region Pointer  -Go To- process

        public void GoTo_NextState_Trans(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                log.Info("GoTo_NextState_Trans() Start.!");

                var Selection = TransactionDiagram.SelectionService.CurrentSelection;
                if (Selection.Count != 1)
                {
                    propertyGrid.SelectedObject = null;
                    propertyGrid.SelectedObjectName = "";
                    return;
                }

                ViewModelDesignerItem Ditem = new ViewModelDesignerItem();
                foreach (var slc in Selection)
                {
                    if (slc.GetType().Name.ToString() != "ViewModelDesignerItem")
                        return;
                    Ditem = (ViewModelDesignerItem)slc;
                }

                DockPanel Dpnl = (DockPanel)Ditem.Content;
                if (Dpnl.Tag.ToString() != "P")
                {
                    log.Warn("GoTo_NextState_Trans(), Sadece Pointer state Go To özelliğne Sahiptir. State:" + Dpnl.Tag.ToString());
                    return;
                }

                StateP pState = new StateP();
                pState = (StateP)this.TransactionDiagram.TransactionList.FindAll(x => x.TransactionName == this.TransactionDiagram.CurrentTransactionName)
                    .Find(x => x.Id == Dpnl.Uid).PropertyGrid.SelectedObject;
                if (pState.NextStateNumber == "255")
                {
                    log.Warn("GoTo_NextState_Trans(), NextStateNumber 255'ten farklı olmalı pState.NextStateNumber:" + pState.NextStateNumber);
                    return;
                }

                PointerStateTransaction PointerTrans = new PointerStateTransaction(this.TransactionDiagram.TransactionList, pState.NextStateNumber);
                if (PointerTrans.ShowDialog() == true)
                {
                    string TransactionName = PointerTrans.TransactionName;
                    string BrandId = PointerTrans.BrandId;


                    string ExTransactionName = this.TransactionDiagram.CurrentTransactionName;
                    this.TransactionDiagram.CurrentTransactionName = TransactionName;
                    if (!string.IsNullOrEmpty(ExTransactionName))
                        this.TransactionDiagram.Open_Executed(false, ExTransactionName);

                    this.TransactionDiagram.CurrentBrandId = BrandId;
                    propertyGrid.SelectedObject = null;
                    propertyGrid.SelectedObjectName = "";
                    TransDiagramName.Header = TransactionName;
                    ExpanderDItem.IsExpanded = IsTransactionSelected();
                }

            }
            catch (Exception ex)
            {
                log.Error("GoTo_NextState_Trans() Error ! " + ex.Message);
                MessageBox.Show("Beklenmedik bir Hata Oluştu.!, Tekrar Deneyiniz.");
               
            }

        }
        
        #endregion



    }
}
