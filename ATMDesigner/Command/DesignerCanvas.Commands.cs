using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;
using ATMDesigner.Business.Interfaces;
using ATMDesigner.UI.Services;
using System.Configuration;
using ATMDesigner.Common;
using ATMDesigner.UI.Popups;
using ATMDesigner.UI.View;
using System.Reflection;
using System.Windows.Threading;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace ATMDesigner.UI
{
    public partial class ViewModelDesignerCanvas
    {
        #region daha sonra kullanılabilir
        //public static RoutedCommand Group = new RoutedCommand();
        //public static RoutedCommand Ungroup = new RoutedCommand();
        //public static RoutedCommand BringForward = new RoutedCommand();
        //public static RoutedCommand BringToFront = new RoutedCommand();
        //public static RoutedCommand SendBackward = new RoutedCommand();
        //public static RoutedCommand SendToBack = new RoutedCommand();
        //public static RoutedCommand AlignTop = new RoutedCommand();
        //public static RoutedCommand AlignVerticalCenters = new RoutedCommand();
        //public static RoutedCommand AlignBottom = new RoutedCommand();
        //public static RoutedCommand AlignLeft = new RoutedCommand();
        //public static RoutedCommand AlignHorizontalCenters = new RoutedCommand();
        //public static RoutedCommand AlignRight = new RoutedCommand();
        //public static RoutedCommand DistributeHorizontal = new RoutedCommand();
        //public static RoutedCommand DistributeVertical = new RoutedCommand();
        #endregion

        public static RoutedCommand SelectAll = new RoutedCommand();
        public static RoutedCommand Transfer = new RoutedCommand();
        private ProgressBarWindow pbw = null;
        public List<ModelCanvasStateObject> CopiedStatesList=new List<ModelCanvasStateObject>();// = new List<ModelCanvasStateObject>();      
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);      
        public ITransactionStateBusiness StateBusiness;
        
        public ViewModelDesignerCanvas()
        { 
            
            #region daha sonra kullanılabilir
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.Group, Group_Executed, Group_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.Ungroup, Ungroup_Executed, Ungroup_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.BringForward, BringForward_Executed, Order_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.BringToFront, BringToFront_Executed, Order_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.SendBackward, SendBackward_Executed, Order_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.SendToBack, SendToBack_Executed, Order_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignTop, AlignTop_Executed, Align_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignVerticalCenters, AlignVerticalCenters_Executed, Align_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignBottom, AlignBottom_Executed, Align_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignLeft, AlignLeft_Executed, Align_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignHorizontalCenters, AlignHorizontalCenters_Executed, Align_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignRight, AlignRight_Executed, Align_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.DistributeHorizontal, DistributeHorizontal_Executed, Distribute_Enabled));
            //this.CommandBindings.Add(new CommandBinding(DesignerCanvas.DistributeVertical, DistributeVertical_Executed, Distribute_Enabled));
            #endregion


           // this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, Print_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, Cut_Executed, Cut_Enabled));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, Copy_Executed, Copy_Enabled));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, Paste_Executed, Paste_Enabled));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, Delete_Executed, Delete_Enabled));

            this.CommandBindings.Add(new CommandBinding(ViewModelDesignerCanvas.SelectAll, SelectAll_Executed));
            //this.CommandBindings.Add(new CommandBinding(ViewModelDesignerCanvas.Transfer, Transfer_Executed, Transfer_Enabled));
            SelectAll.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            
            //
            //Update AtmBrand and Config
            //Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            //{
            //    UpdateBrandAndConfig();
            //}));



            //pbw = new ProgressBarWindow();
            //pbw.ShowDialog();

            this.atmBrandList = this.AtmBrandList;
            this.atmConfigList = this.AtmConfigList;
            this.avaliableStateNumberList = this.AvaliableStateNumberList;
            this.AllowDrop = true;
            Clipboard.Clear();
        }


        #region Update Brand and Config

        private void UpdateBrandAndConfig()
        {
            try
            {
                this.atmBrandList = this.AtmBrandList;
                this.atmConfigList = this.AtmConfigList;
                this.avaliableStateNumberList = this.AvaliableStateNumberList;
                //foreach (var item in atmBrandList)
                //{
                   
                //     AddDisableFlagToConfig(item.BrandName.ToUpper(),item.BrandId);
                //}
               
            }
            catch (Exception ex)
            {
                log.Error("UpdateBrandAndConfig() Hata.! Brand ve Config update işlemlerinde bir hata oluştu. "+ex.Message);
                MessageBox.Show("Brand ve Config Update işlemlerinde bir hata oluştu.!", "Brand ve Config Update", MessageBoxButton.OK, MessageBoxImage.Error);
                        
            }
            finally
            {
                pbw.Close();        
            }

        }

        private static void AddDisableFlagToConfig(string key,string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        
        }

        #endregion


        #region Open Command

        public void Open_Executed(bool IsUploadEvent,string  ExTransName)
        {
          
            if (IsUploadEvent == true)
            {
                if (UploadStateList() != 100)
                {
                    MessageBox.Show("Transaction Stateleri Tutarlı bir şekilde Yüklenemedi.!", "Transaction yüklenmesi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                if (!SaveDesignerItems(ExTransName))
                {
                    log.Error("Open_Executed() Hata.! XML Dosyaları kaydedilemedi.! ");
                    MessageBox.Show("XML Arayüzlerinde parsing hatası", "XML Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }     

            XElement root = LoadSerializedDataFromFile();            
            if (root == null)
                return;

            this.Children.Clear();
            this.SelectionService.ClearSelection();

            IEnumerable<XElement> itemsXML = root.Elements("DesignerItems").Elements("ViewModelDesignerItem");
            foreach (XElement itemXML in itemsXML)
            {
                Guid id = new Guid(itemXML.Element("ID").Value);
                ViewModelDesignerItem item = DeserializeDesignerItem(itemXML, id, 0, 0);
                this.Children.Add(item);
                SetConnectorDecoratorTemplate(item);
            }

            this.InvalidateVisual();

            IEnumerable<XElement> connectionsXML = root.Elements("Connections").Elements("ViewModelConnection");
            foreach (XElement connectionXML in connectionsXML)
            {
                Guid sourceID = new Guid(connectionXML.Element("SourceID").Value);
                Guid sinkID = new Guid(connectionXML.Element("SinkID").Value);

                String sourceConnectorName = connectionXML.Element("SourceConnectorName").Value;
                String sinkConnectorName = connectionXML.Element("SinkConnectorName").Value;

                String SourceDesignerItem = connectionXML.Element("SourceDesignerItem").Value + "State_ConnectorDecorator";
                String SinkDesignerItem = connectionXML.Element("SinkDesignerItem").Value+"State_ConnectorDecorator";

                ViewModelConnector sourceConnector = GetConnector(sourceID, sourceConnectorName, SourceDesignerItem);
                ViewModelConnector sinkConnector = GetConnector(sinkID, sinkConnectorName, SourceDesignerItem);
                sourceConnector.DesignerItemName = connectionXML.Element("SourceDesignerItem").Value;
                sinkConnector.DesignerItemName = connectionXML.Element("SinkDesignerItem").Value;


                ViewModelConnection connection = new ViewModelConnection(sourceConnector, sinkConnector);
                
                Canvas.SetZIndex(connection, Int32.Parse(connectionXML.Element("zIndex").Value));
                this.Children.Add(connection);
            }

        }

        bool SaveDesignerItems(string ExTransName)
        {
            IEnumerable<ViewModelDesignerItem> designerItems = this.Children.OfType<ViewModelDesignerItem>();
            IEnumerable<ViewModelConnection> connections = this.Children.OfType<ViewModelConnection>();


            XElement designerItemsXML = SerializeDesignerItems(designerItems);
            XElement connectionsXML = SerializeConnections(connections);
            
            this.Children.Clear();
            this.SelectionService.ClearSelection();
            if (designerItemsXML == null || connectionsXML == null)
            {
                MessageBox.Show("designerItemsXML veya connectionsXML parsing hatası", "XML Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                log.Error("SaveDesignerItems() Hata.! designerItemsXML -  connectionsXML : " + connectionsXML + "-" + connectionsXML);              
                return false;
            }

            XElement root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(connectionsXML);
            return SaveFile(root, ExTransName);
        }

        #endregion

        #region Save Command

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                Save_Transactions();
            }));

            pbw = new ProgressBarWindow();           
            pbw.ShowDialog();

        }

        private void Save_Transactions()
        {
            if (TransactionList.Count < 1)
            {
                pbw.Close();
                return;
            }
            if (MessageBoxResult.No == MessageBox.Show("Değişiklikleri kaydetmek istediğinize emin misiniz?", "ATM State Designer", MessageBoxButton.YesNo))
            {
                pbw.Close();
                return;
            }

            log.Info("Save_Executed() Start.!"); 
            IEnumerable<ViewModelDesignerItem> designerItems = this.Children.OfType<ViewModelDesignerItem>();
            IEnumerable<ViewModelConnection> connections = this.Children.OfType<ViewModelConnection>();

            XElement designerItemsXML = SerializeDesignerItems(designerItems);
            XElement connectionsXML = SerializeConnections(connections);
            if (designerItemsXML==null ||connectionsXML==null)
            {
                MessageBox.Show("designerItemsXML veya connectionsXML parsing hatası", "XML Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                pbw.Close();
                return;
            }

            XElement root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(connectionsXML);
            if (SaveFile(root, this.CurrentTransactionName))
            {
                //List<string> ExtensionStates = new List<string>(new string[] { this.avaliableStateNumberList[0].ToString(), this.avaliableStateNumberList[1].ToString(),
                //this.avaliableStateNumberList[2].ToString(),this.avaliableStateNumberList[3].ToString(),this.avaliableStateNumberList[4].ToString() });

                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                int RetVal = StateBusiness.SaveProject(this.TransactionList, this.ProjectName, this.avaliableStateNumberList[0].ToString(), StateIdListFromProjectUpload);
                if (RetVal == 100)
                {
                    log.Info("Save_Executed(), işlem Başarı ile tamamlandı.!");
                    MessageBox.Show("Kayıt işlemi başarılı bir şekilde yapıldı.!", "Kayıt işlemi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    log.Error("Save_Executed() Hata.! Transactionlar DB'ye başarılı bir şekilde kaydedilemedi. Sayı:"+this.TransactionList.Count);
                    MessageBox.Show("Transactionlar DB'ye başarılı bir şekilde kaydedilemedi.!", "State Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                log.Error("Save_Executed() hata.! XML Dosyaları kaydedilemedi.! ");
                MessageBox.Show("XML Arayüzlerinde parsing hatası", "XML Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            pbw.Close();
        }

        #endregion

        #region Merge Database Tables-Transfer data

        public void Transfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBoxResult.No == MessageBox.Show("Değişiklikler Asıl tabloya aktarılacaktır,Devam etmek istediğinize emin misiniz?", "State tablo Aktarımı", MessageBoxButton.YesNo))
                return;

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                Synchronous_StateTables();
            }));

            pbw = new ProgressBarWindow();
            pbw.ShowDialog();
        }

        private void Synchronous_StateTables()
        {

            if (string.IsNullOrEmpty(this.ProjectName))
            {
                MessageBox.Show("State Datalarını aktaracağınız,bir projeyi Upload ediniz.!", "State tablo Aktarımı", MessageBoxButton.OK, MessageBoxImage.Information);
                pbw.Close();
                return;
            }

            if (MessageBoxResult.No == MessageBox.Show("State Datalarını Aktarmak istediğinize emin misiniz ?", "State tablo Aktarımı", MessageBoxButton.YesNo))
            {
                pbw.Close();
                return;
            }

            log.Info("Synchronous_StateTables() Start.! ProjectName: "+ProjectName);

            StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
            int RetVal = StateBusiness.Synchronous_StateTables(this.ProjectName);
            if (RetVal == 100)
            {
                log.Info("Synchronous_StateTables(), işlem Başarı ile tamamlandı.!");
                MessageBox.Show("State tablo Aktarımı işlemi başarılı bir şekilde yapıldı.!", "State tablo Aktarımı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (RetVal == 103)
            {
                log.Error("Synchronous_StateTables() Hata.! Transactionlar ve stateleri ,Mevcut tabloya()  başarılı bir şekilde aktarılamadı");
                MessageBox.Show(this.ProjectName+" Adında herhangi bir proje ve ilişkili bir transaction yapısı yok.!", "State tablo Aktarımı", MessageBoxButton.OK, MessageBoxImage.Warning);
           
            }
            else
            {
                log.Error("Synchronous_StateTables() Hata.! Transactionlar ve stateleri ,Mevcut tabloya()  başarılı bir şekilde aktarılamadı");
                MessageBox.Show("State Tabloları Arası Aktarım işlemi başarılı yapılamadı !", "State tablo Aktarımı", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            pbw.Close();
        }

        #endregion

        #region Print Command

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SelectionService.ClearSelection();
            PrintDialog printDialog = new PrintDialog();          
            if (true == printDialog.ShowDialog())
            {
                printDialog.PrintVisual(this, "ATM Transactions Diagram");
            }
        }

        #endregion

        #region Copy Command

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CopyCurrentSelection();
        }

        private void Copy_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectionService.CurrentSelection.Count() > 0;
        }

        public bool CopyProjectXmlsDirectory()
        {
            bool Retval = false;
            try
            {
                string SourceXMLPath = ConfigurationManager.AppSettings["XMLPath"];
                string TargetXMLPath = SourceXMLPath + DateTime.Now.ToString("yyyyMMddHHmmss");

                if (!System.IO.Directory.Exists(TargetXMLPath))
                {
                    System.IO.Directory.CreateDirectory(TargetXMLPath);
                }

                if (System.IO.Directory.Exists(SourceXMLPath))
                {
                    string[] files = System.IO.Directory.GetFiles(SourceXMLPath);

                    // Copy the files and overwrite destination files if they already exist.
                    foreach (string s in files)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        string fileName = System.IO.Path.GetFileName(s);
                        string destFile = System.IO.Path.Combine(TargetXMLPath, fileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }
                else
                {
                    log.Warn("CopyProjectXmlsDirectory() Hata.!, Xml'lerin kopyalanmaasında bir hata oluştu..");
                    if (MessageBox.Show("Projenin Xml çıktılarının yedeklenmesinde bir problem oluştu..!,Devam etmek istediğinize emin misiniz?", "Yedekleme Problemi", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK) 
                    {
                        Retval = true;
                    }
                    return Retval;
                }
                Retval = true;
            }
            catch (Exception ex)
            {

                log.Warn("CopyProjectXmlsDirectory() Hata.!, Xml'lerin kopyalanmaasında bir hata oluştu.." + ex.Message);
                if (MessageBox.Show("Projenin Xml çıktılarının yedeklenmesinde bir problem oluştu..!,Devam etmek istediğinize emin misiniz?", "Yedekleme Problemi", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK) 
                {
                    Retval = true;
                }
            }

            return Retval;
        }


        #endregion

        #region Paste Command

        private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            XElement root = LoadSerializedDataFromClipBoard();
            if (root == null)
                return;

            if (CopiedStatesList.Count==0)            
                return;
            if (CopiedStatesList[0].BrandId!=CurrentBrandId)
            {
                MessageBox.Show("Transactionlar aynı Brand'de olmalıdır");
                return;
            }


            // create DesignerItems          
            Dictionary<Guid, Guid> mappingOldToNewIDs = new Dictionary<Guid, Guid>();
            List<IModelSelectable> newItems = new List<IModelSelectable>();
            IEnumerable<XElement> itemsXML = root.Elements("DesignerItems").Elements("ViewModelDesignerItem");

            double offsetX = Double.Parse(root.Attribute("OffsetX").Value, CultureInfo.InvariantCulture);
            double offsetY = Double.Parse(root.Attribute("OffsetY").Value, CultureInfo.InvariantCulture);

            foreach (XElement itemXML in itemsXML)
            {
                Guid oldID = new Guid(itemXML.Element("ID").Value);
                Guid newID = Guid.NewGuid();
                mappingOldToNewIDs.Add(oldID, newID);
                ViewModelDesignerItem item = DeserializeDesignerItem(itemXML, newID, offsetX, offsetY);
                
                //Parent ve Child Item düzenlenmesi
                ArrangeCopiedDesignerItems(ref item);            

                this.Children.Add(item);
                SetConnectorDecoratorTemplate(item);
                newItems.Add(item);
            }

            //Parent ve Child Item düzenlenmesi
            for (int i = 0; i < CopiedStatesList.Count; i++)
            {
                TransactionList.Add(CopiedStatesList[i]);
            }
            CopiedStatesList.Clear();


            // update group hierarchy
            //SelectionService.ClearSelection();
            //foreach (ViewModelDesignerItem el in newItems)
            //{
            //    if (el.ParentID != Guid.Empty)
            //        el.ParentID = mappingOldToNewIDs[el.ParentID];
            //}

            #region Connection ve Designeritem xml upload

            foreach (ViewModelDesignerItem item in newItems)
            {
                if (item.ParentID == Guid.Empty)
                {
                    SelectionService.AddToSelection(item);
                }
            }

            // create Connections
            IEnumerable<XElement> connectionsXML = root.Elements("Connections").Elements("ViewModelConnection");
            foreach (XElement connectionXML in connectionsXML)
            {
                Guid oldSourceID = new Guid(connectionXML.Element("SourceID").Value);
                Guid oldSinkID = new Guid(connectionXML.Element("SinkID").Value);

                if (mappingOldToNewIDs.ContainsKey(oldSourceID) && mappingOldToNewIDs.ContainsKey(oldSinkID))
                {
                    Guid newSourceID = mappingOldToNewIDs[oldSourceID];
                    Guid newSinkID = mappingOldToNewIDs[oldSinkID];

                    String sourceConnectorName = connectionXML.Element("SourceConnectorName").Value;
                    String sinkConnectorName = connectionXML.Element("SinkConnectorName").Value;

                    String SourceDesignerItem = connectionXML.Element("SourceDesignerItem").Value + "State_ConnectorDecorator";
                    String SinkDesignerItem = connectionXML.Element("SinkDesignerItem").Value + "State_ConnectorDecorator";

                    ViewModelConnector sourceConnector = GetConnector(newSourceID, sourceConnectorName, SourceDesignerItem);
                    ViewModelConnector sinkConnector = GetConnector(newSinkID, sinkConnectorName, SinkDesignerItem);
                                      
                    ViewModelConnection connection = new ViewModelConnection(sourceConnector, sinkConnector);
                    Canvas.SetZIndex(connection, Int32.Parse(connectionXML.Element("zIndex").Value));
                    this.Children.Add(connection);

                    SelectionService.AddToSelection(connection);
                }
            }

            //Daha Sonra kullanılacak
           //DesignerCanvas.BringToFront.Execute(null, this);

            // update paste offset
            root.Attribute("OffsetX").Value = (offsetX + 10).ToString();
            root.Attribute("OffsetY").Value = (offsetY + 10).ToString();
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Xaml, root);

            #endregion

        }
        

        private void ArrangeCopiedDesignerItems(ref ViewModelDesignerItem item)
        {
            ModelCanvasStateObject CopiedItem= new ModelCanvasStateObject();           
            DockPanel pnl = (DockPanel)item.Content;
            PropertyGrid SelectedPgrid = new PropertyGrid();


            string ExStateNumber = pnl.Uid.ToString();
            string NewStateNumber=avaliableStateNumberList[0].ToString();
            avaliableStateNumberList.Remove(NewStateNumber);

            CopiedItem = CopiedStatesList.Find(x => x.Id == pnl.Uid.ToString());
            CopiedStatesList.Remove(CopiedItem);
            SelectedPgrid=CopiedItem.PropertyGrid;

            
            pnl.Uid = NewStateNumber;          
            item.StateNumber = pnl.Uid.ToString();
            item.Content = pnl;



            //Child State Güncellemesi        
            List<ModelChildStateObject> ChildStateList = new List<ModelChildStateObject>();
            ChildStateList = CopiedItem.ChildStateList;
            if (ChildStateList.Count!=0)
            {
                for (int i = 0; i < ChildStateList.Count; i++)
                {
                    CopiedStatesList.Find(x => x.Id == ChildStateList[i].ChildId).ParentStateList.Find(x => x.ParentId == ExStateNumber).ParentId = NewStateNumber;                    
                }
                
            }

            //parent State Güncellemesi                  
            List<ModelParentStateObject> PStateList = new List<ModelParentStateObject>();
            PStateList = CopiedItem.ParentStateList;
            if (PStateList.Count != 0)
            {
                PropertyGrid SelectedPgridPrnt = new PropertyGrid();
                for (int i = 0; i < PStateList.Count; i++)
                {
                    SelectedPgridPrnt = CopiedStatesList.Find(x => x.Id == PStateList[i].ParentId).PropertyGrid;

                    PropertyDescriptor theDescriptor = TypeDescriptor.GetProperties(SelectedPgridPrnt.SelectedObject)[PStateList[i].PropertyName];
                    ReadOnlyAttribute theDescriptorBrowsableAttribute = (ReadOnlyAttribute)theDescriptor.Attributes[typeof(ReadOnlyAttribute)];
                    FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("isReadOnly", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
                    isBrowsable.SetValue(theDescriptorBrowsableAttribute, false);

                 
                    SelectedPgridPrnt.SelectedObject.GetType().GetProperty(PStateList[i].PropertyName).SetValue(SelectedPgridPrnt.SelectedObject, NewStateNumber);
                    CopiedStatesList.Find(x => x.Id == PStateList[i].ParentId).PropertyGrid.SelectedObject = SelectedPgridPrnt.SelectedObject;
                    CopiedStatesList.Find(x => x.Id == PStateList[i].ParentId).ChildStateList.Find(x => x.ChildId == ExStateNumber).ChildId = NewStateNumber;

                    isBrowsable.SetValue(theDescriptorBrowsableAttribute, true);                
                }

            }


            SelectedPgrid.SelectedObject.GetType().GetProperty("StateNumber").SetValue(SelectedPgrid.SelectedObject, NewStateNumber);
            SelectedPgrid.SelectedObjectName = NewStateNumber;
            CopiedItem.PropertyGrid = SelectedPgrid;

            CopiedItem.Id = NewStateNumber;
            CopiedItem.BrandId = CurrentBrandId;
            CopiedItem.TransactionName = CurrentTransactionName;
            CopiedStatesList.Add(CopiedItem);
           // TransactionList.Add(CopiedItem);
    
        }



        private void Paste_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =Clipboard.ContainsData(DataFormats.Xaml);
        }




        #endregion

        #region Delete Command

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteCurrentSelection();
        }

        private void Delete_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.SelectionService.CurrentSelection.Count() > 0;
        }

        private void DeleteCurrentSelection()
        {
            foreach (ViewModelConnection connection in SelectionService.CurrentSelection.OfType<ViewModelConnection>())
            {
                this.Children.Remove(connection);
            }

            foreach (ViewModelDesignerItem item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>())
            {
                Control cd = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                
                List<ViewModelConnector> connectors = new List<ViewModelConnector>();
                GetConnectors(cd, connectors);

                //Gereksiz silinebilir -test amaçlıdır
                foreach (ViewModelConnector connector in connectors)
                {
                    foreach (ViewModelConnection con in connector.Connections)
                    {
                        this.Children.Remove(con);
                    }
                }

                //Connectionların silinmesi
                foreach (ViewModelConnection Conn in this.Children.OfType<ViewModelConnection>().ToList())
                {
                    if (Conn.Source.ParentDesignerItem.ID == item.ID)
                        this.Children.Remove(Conn);

                    if (Conn.Sink.ParentDesignerItem.ID == item.ID)
                        this.Children.Remove(Conn);
                }

                this.Children.Remove(item);

                ModelCanvasStateObject Transaction = new ModelCanvasStateObject();
                if (!TransactionList.Exists(x => x.Id == item.StateNumber))
                    continue;

                  Transaction = (ModelCanvasStateObject)TransactionList.FindAll(x => x.Id == item.StateNumber).Find(y => y.TransactionName == CurrentTransactionName);

                //parent State Güncellemesi                  
                List<ModelParentStateObject> PStateList = new List<ModelParentStateObject>();
                PStateList = Transaction.ParentStateList;
                if (PStateList.Count != 0)
                {
                    PropertyGrid SelectedPgridPrnt = new PropertyGrid();
                    for (int i = 0; i < PStateList.Count; i++)
                    {
                        if (!TransactionList.Exists(x => x.Id == PStateList[i].ParentId))
                            continue;
                        SelectedPgridPrnt = TransactionList.FindAll(x => x.Id == PStateList[i].ParentId).Find(y => y.TransactionName == CurrentTransactionName).PropertyGrid;

                        PropertyDescriptor theDescriptor = TypeDescriptor.GetProperties(SelectedPgridPrnt.SelectedObject)[PStateList[i].PropertyName];
                        ReadOnlyAttribute theDescriptorBrowsableAttribute = (ReadOnlyAttribute)theDescriptor.Attributes[typeof(ReadOnlyAttribute)];
                        FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("isReadOnly", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
                        isBrowsable.SetValue(theDescriptorBrowsableAttribute, false);


                        SelectedPgridPrnt.SelectedObject.GetType().GetProperty(PStateList[i].PropertyName).SetValue(SelectedPgridPrnt.SelectedObject, "255");

                        TransactionList.FindAll(x => x.Id == PStateList[i].ParentId).Find(y => y.TransactionName == CurrentTransactionName).PropertyGrid = SelectedPgridPrnt;
                        
                        isBrowsable.SetValue(theDescriptorBrowsableAttribute, true);                
                    }

                }           
                
                TransactionList.Remove(Transaction);
                
            }



            SelectionService.ClearSelection();
            UpdateZIndex();
        }


        #endregion

        #region Cut Command

        private void Cut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CopyCurrentSelection();
            DeleteCurrentSelection();
        }

        private void Cut_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;// this.SelectionService.CurrentSelection.Count() > 0;
        }

        #endregion
   
        #region SelectAll Command

        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.propertyGrid.SelectedObject = null;
            m.propertyGrid.SelectedObjectName = "";
            SelectionService.SelectAll();
        }

        #endregion

        #region Comment Events-daha sonra kullanılabilir

        #region Group Command

        private void Group_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var items = from item in this.SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                        where item.ParentID == Guid.Empty
                        select item;

            Rect rect = GetBoundingRectangle(items);

            ViewModelDesignerItem groupItem = new ViewModelDesignerItem();
            groupItem.IsGroup = true;
            groupItem.Width = rect.Width;
            groupItem.Height = rect.Height;
            Canvas.SetLeft(groupItem, rect.Left);
            Canvas.SetTop(groupItem, rect.Top);
            Canvas groupCanvas = new Canvas();
            groupItem.Content = groupCanvas;
            Canvas.SetZIndex(groupItem, this.Children.Count);
            this.Children.Add(groupItem);

            foreach (ViewModelDesignerItem item in items)
                item.ParentID = groupItem.ID;

            this.SelectionService.SelectItem(groupItem);
        }

        private void Group_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            int count = (from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                         where item.ParentID == Guid.Empty
                         select item).Count();

            e.CanExecute = count > 1;
        }

        #endregion

        #region Ungroup Command

        private void Ungroup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var groups = (from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                          where item.IsGroup && item.ParentID == Guid.Empty
                          select item).ToArray();

            foreach (ViewModelDesignerItem groupRoot in groups)
            {
                var children = from child in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                               where child.ParentID == groupRoot.ID
                               select child;

                foreach (ViewModelDesignerItem child in children)
                    child.ParentID = Guid.Empty;

                this.SelectionService.RemoveFromSelection(groupRoot);
                this.Children.Remove(groupRoot);
                UpdateZIndex();
            }
        }

        private void Ungroup_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            var groupedItem = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                              where item.ParentID != Guid.Empty
                              select item;


            e.CanExecute = groupedItem.Count() > 0;
        }

        #endregion

        #region BringForward Command

        private void BringForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> ordered = (from item in SelectionService.CurrentSelection
                                       orderby Canvas.GetZIndex(item as UIElement) descending
                                       select item as UIElement).ToList();

            int count = this.Children.Count;

            for (int i = 0; i < ordered.Count; i++)
            {
                int currentIndex = Canvas.GetZIndex(ordered[i]);
                int newIndex = Math.Min(count - 1 - i, currentIndex + 1);
                if (currentIndex != newIndex)
                {
                    Canvas.SetZIndex(ordered[i], newIndex);
                    IEnumerable<UIElement> it = this.Children.OfType<UIElement>().Where(item => Canvas.GetZIndex(item) == newIndex);

                    foreach (UIElement elm in it)
                    {
                        if (elm != ordered[i])
                        {
                            Canvas.SetZIndex(elm, currentIndex);
                            break;
                        }
                    }
                }
            }
        }

        private void Order_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = SelectionService.CurrentSelection.Count() > 0;
            e.CanExecute = true;
        }

        #endregion

        #region BringToFront Command

        private void BringToFront_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> selectionSorted = (from item in SelectionService.CurrentSelection
                                               orderby Canvas.GetZIndex(item as UIElement) ascending
                                               select item as UIElement).ToList();

            List<UIElement> childrenSorted = (from UIElement item in this.Children
                                              orderby Canvas.GetZIndex(item as UIElement) ascending
                                              select item as UIElement).ToList();

            int i = 0;
            int j = 0;
            foreach (UIElement item in childrenSorted)
            {
                if (selectionSorted.Contains(item))
                {
                    int idx = Canvas.GetZIndex(item);
                    Canvas.SetZIndex(item, childrenSorted.Count - selectionSorted.Count + j++);
                }
                else
                {
                    Canvas.SetZIndex(item, i++);
                }
            }
        }

        #endregion

        #region SendBackward Command

        private void SendBackward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> ordered = (from item in SelectionService.CurrentSelection
                                       orderby Canvas.GetZIndex(item as UIElement) ascending
                                       select item as UIElement).ToList();

            int count = this.Children.Count;

            for (int i = 0; i < ordered.Count; i++)
            {
                int currentIndex = Canvas.GetZIndex(ordered[i]);
                int newIndex = Math.Max(i, currentIndex - 1);
                if (currentIndex != newIndex)
                {
                    Canvas.SetZIndex(ordered[i], newIndex);
                    IEnumerable<UIElement> it = this.Children.OfType<UIElement>().Where(item => Canvas.GetZIndex(item) == newIndex);

                    foreach (UIElement elm in it)
                    {
                        if (elm != ordered[i])
                        {
                            Canvas.SetZIndex(elm, currentIndex);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region SendToBack Command

        private void SendToBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> selectionSorted = (from item in SelectionService.CurrentSelection
                                               orderby Canvas.GetZIndex(item as UIElement) ascending
                                               select item as UIElement).ToList();

            List<UIElement> childrenSorted = (from UIElement item in this.Children
                                              orderby Canvas.GetZIndex(item as UIElement) ascending
                                              select item as UIElement).ToList();
            int i = 0;
            int j = 0;
            foreach (UIElement item in childrenSorted)
            {
                if (selectionSorted.Contains(item))
                {
                    int idx = Canvas.GetZIndex(item);
                    Canvas.SetZIndex(item, j++);

                }
                else
                {
                    Canvas.SetZIndex(item, selectionSorted.Count + i++);
                }
            }
        }        

        #endregion

        #region AlignTop Command

        private void AlignTop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double top = Canvas.GetTop(selectedItems.First());

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = top - Canvas.GetTop(item);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        private void Align_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            //var groupedItem = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
            //                  where item.ParentID == Guid.Empty
            //                  select item;


            //e.CanExecute = groupedItem.Count() > 1;
            e.CanExecute = true;
        }

        #endregion

        #region AlignVerticalCenters Command

        private void AlignVerticalCenters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double bottom = Canvas.GetTop(selectedItems.First()) + selectedItems.First().Height / 2;

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = bottom - (Canvas.GetTop(item) + item.Height / 2);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        #endregion

        #region AlignBottom Command

        private void AlignBottom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double bottom = Canvas.GetTop(selectedItems.First()) + selectedItems.First().Height;

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = bottom - (Canvas.GetTop(item) + item.Height);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        #endregion

        #region AlignLeft Command

        private void AlignLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double left = Canvas.GetLeft(selectedItems.First());

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = left - Canvas.GetLeft(item);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        #endregion

        #region AlignHorizontalCenters Command

        private void AlignHorizontalCenters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double center = Canvas.GetLeft(selectedItems.First()) + selectedItems.First().Width / 2;

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = center - (Canvas.GetLeft(item) + item.Width / 2);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        #endregion

        #region AlignRight Command

        private void AlignRight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double right = Canvas.GetLeft(selectedItems.First()) + selectedItems.First().Width;

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = right - (Canvas.GetLeft(item) + item.Width);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        #endregion

        #region DistributeHorizontal Command

        private void DistributeHorizontal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                let itemLeft = Canvas.GetLeft(item)
                                orderby itemLeft
                                select item;

            if (selectedItems.Count() > 1)
            {
                double left = Double.MaxValue;
                double right = Double.MinValue;
                double sumWidth = 0;
                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    left = Math.Min(left, Canvas.GetLeft(item));
                    right = Math.Max(right, Canvas.GetLeft(item) + item.Width);
                    sumWidth += item.Width;
                }

                double distance = Math.Max(0, (right - left - sumWidth) / (selectedItems.Count() - 1));
                double offset = Canvas.GetLeft(selectedItems.First());

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = offset - Canvas.GetLeft(item);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                    offset = offset + item.Width + distance;
                }
            }
        }

        private void Distribute_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            //var groupedItem = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
            //                  where item.ParentID == Guid.Empty
            //                  select item;


            //e.CanExecute = groupedItem.Count() > 1;
            e.CanExecute = true;
        }

        #endregion

        #region DistributeVertical Command

        private void DistributeVertical_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>()
                                where item.ParentID == Guid.Empty
                                let itemTop = Canvas.GetTop(item)
                                orderby itemTop
                                select item;

            if (selectedItems.Count() > 1)
            {
                double top = Double.MaxValue;
                double bottom = Double.MinValue;
                double sumHeight = 0;
                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    top = Math.Min(top, Canvas.GetTop(item));
                    bottom = Math.Max(bottom, Canvas.GetTop(item) + item.Height);
                    sumHeight += item.Height;
                }

                double distance = Math.Max(0, (bottom - top - sumHeight) / (selectedItems.Count() - 1));
                double offset = Canvas.GetTop(selectedItems.First());

                foreach (ViewModelDesignerItem item in selectedItems)
                {
                    double delta = offset - Canvas.GetTop(item);
                    foreach (ViewModelDesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                    offset = offset + item.Height + distance;
                }
            }
        }

        #endregion

        #endregion
             
        #region Helper Methods

        private XElement LoadSerializedDataFromFile()
        {
            string XMLPath = ConfigurationManager.AppSettings["XMLPath"];
            XMLPath = Path.Combine(XMLPath, this.ProjectName + "_" + this.CurrentTransactionName + ".xml");
            try
            {
                log.Info("LoadSerializedDataFromFile() Start! XMLPath:" + XMLPath);
                if (File.Exists(XMLPath))
                {
                   return XElement.Load(XMLPath);
                }
                else
                {
                    log.Warn("LoadSerializedDataFromFile() Uyarı ! Dosya bulunamadı XMLPath:" + XMLPath);
                }
               
            }
            catch (Exception ex)
            {
                log.Error("LoadSerializedDataFromFile() Hata.! " + ex.Message);
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        bool SaveFile(XElement xElement, string TransName)
        {
            bool Retval = false;
            string XMLPath = "";
            try
            {
                XMLPath = ConfigurationManager.AppSettings["XMLPath"];
                XMLPath = Path.Combine(XMLPath, this.ProjectName + "_" + TransName + ".xml");
                xElement.Save(XMLPath); ;
                Retval = true;
                log.Info("SaveFile() Xml başarılı kaydedildi.! XMLPath:" + XMLPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                log.Info("SaveFile() Hata.! xml kaydedilemedi XMLPath:" + XMLPath);
                Retval = false;
            }
            
            return Retval;

        }

        private XElement LoadSerializedDataFromClipBoard()
        {
            if (Clipboard.ContainsData(DataFormats.Xaml))
            {
                String clipboardData = Clipboard.GetData(DataFormats.Xaml) as String;

                if (String.IsNullOrEmpty(clipboardData))
                    return null;
                try
                {
                    return XElement.Load(new StringReader(clipboardData));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace, e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return null;
        }

        private XElement SerializeDesignerItems(IEnumerable<ViewModelDesignerItem> designerItems)
        {
            XElement serializedItems;
            try
            {
                 serializedItems = new XElement("DesignerItems",
                                           from item in designerItems
                                           let contentXaml = XamlWriter.Save(((ViewModelDesignerItem)item).Content)
                                           select new XElement("ViewModelDesignerItem",
                                                      new XElement("Left", Canvas.GetLeft(item)),
                                                      new XElement("Top", Canvas.GetTop(item)),
                                                      new XElement("Width", item.Width),
                                                      new XElement("Height", item.Height),
                                                      new XElement("ID", item.ID),
                                                      new XElement("zIndex", Canvas.GetZIndex(item)),
                                                      new XElement("IsGroup", item.IsGroup),
                                                      new XElement("ParentID", item.ParentID),
                                                      new XElement("StateName", item.StateName),
                                                      new XElement("StateNumber", item.StateNumber),
                                                      new XElement("Content", contentXaml)
                                                  )
                                       );
                return serializedItems;
            }
            catch (Exception ex)
            {
                log.Error("SerializeDesignerItems() Hata.! "+ex);
                return null;             
            }
           
        }

        private XElement SerializeConnections(IEnumerable<ViewModelConnection> connections)
        {
            try
            {

                var serializedConnections = new XElement("Connections",
                               from connection in connections
                               select new XElement("ViewModelConnection",
                                          new XElement("SourceID", connection.Source.ParentDesignerItem.ID),
                                          new XElement("SinkID", connection.Sink.ParentDesignerItem.ID),
                                          new XElement("SourceDesignerItem", connection.Source.DesignerItemName),
                                          new XElement("SinkDesignerItem", connection.Sink.DesignerItemName),
                                          new XElement("SourceConnectorName", connection.Source.Name),
                                          new XElement("SinkConnectorName", connection.Sink.Name),
                                          new XElement("SourceArrowSymbol", connection.SourceArrowSymbol),
                                          new XElement("SinkArrowSymbol", connection.SinkArrowSymbol),
                                          new XElement("zIndex", Canvas.GetZIndex(connection))
                                         )
                                      );

                return serializedConnections;
            }
            catch (Exception ex)
            {
                log.Error("SerializeDesignerItems() Hata.! " + ex);
                return null;
            }
        }

        private static ViewModelDesignerItem DeserializeDesignerItem(XElement itemXML, Guid id, double OffsetX, double OffsetY)
        {
            try
            {
                ViewModelDesignerItem item = new ViewModelDesignerItem(id);
                item.Width = Double.Parse(itemXML.Element("Width").Value, CultureInfo.InvariantCulture);
                item.Height = Double.Parse(itemXML.Element("Height").Value, CultureInfo.InvariantCulture);
                item.ParentID = new Guid(itemXML.Element("ParentID").Value);
                item.StateName = itemXML.Element("StateName").Value;
                item.StateNumber = itemXML.Element("StateNumber").Value;
                item.IsGroup = Boolean.Parse(itemXML.Element("IsGroup").Value);
                Canvas.SetLeft(item, Double.Parse(itemXML.Element("Left").Value, CultureInfo.InvariantCulture) + OffsetX);
                Canvas.SetTop(item, Double.Parse(itemXML.Element("Top").Value, CultureInfo.InvariantCulture) + OffsetY);
                Canvas.SetZIndex(item, Int32.Parse(itemXML.Element("zIndex").Value));
                Object content = XamlReader.Load(XmlReader.Create(new StringReader(itemXML.Element("Content").Value)));
                item.Content = content;
                return item;
            }
            catch (Exception ex)
            {
                log.Error("DeserializeDesignerItem() Hata.! " + ex.Message);
                return null;
            }
        }

        private void CopyCurrentSelection()
        {
            IEnumerable<ViewModelDesignerItem> selectedDesignerItems =
                this.SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>();

            List<ViewModelConnection> selectedConnections =
                this.SelectionService.CurrentSelection.OfType<ViewModelConnection>().ToList();

            CopiedStatesList = new List<ModelCanvasStateObject>();
            ModelCanvasStateObject CopiedItem;
            List<ModelChildStateObject> ChildStateList;
            List<ModelParentStateObject> ParentStateList;
            DockPanel DockPanel;
            PropertyGrid PropertyGrid;

            foreach (var item in selectedDesignerItems)
            { 
                  CopiedItem = new ModelCanvasStateObject();
                 ChildStateList = new List<ModelChildStateObject>();
                 ParentStateList = new List<ModelParentStateObject>();
                 DockPanel = new DockPanel();
                 PropertyGrid = new PropertyGrid();


                for (int i = 0; i < TransactionList.Count; i++)
                {
                    if (TransactionList[i].Id==item.StateNumber&&TransactionList[i].TransactionName==CurrentTransactionName)
                    {

                       
                        string BrandId = TransactionList[i].BrandId;                      
                        for (int j = 0; j < TransactionList[i].ChildStateList.Count; j++)
                        {
                            ModelChildStateObject child = new ModelChildStateObject();
                            child = (ModelChildStateObject)TransactionList[i].ChildStateList[j].Clone();
                            ChildStateList.Add(child);
                        }
                     
                        string ConfigId = TransactionList[i].ConfigId;
                        DockPanel = TransactionList[i].dockPanel;
                        string StateNumber = TransactionList[i].Id;
                     
                        for (int j = 0; j < TransactionList[i].ParentStateList.Count; j++)
                        {
                            ModelParentStateObject parent = new ModelParentStateObject();
                            parent = (ModelParentStateObject)TransactionList[i].ParentStateList[j].Clone();
                            ParentStateList.Add(parent);
                        }

                        Type ClassType = TransactionList[i].PropertyGrid.SelectedObject.GetType();
                        Object ClassInstance = Activator.CreateInstance(ClassType);
                        object[] parametersArray = new object[] { ClassInstance, TransactionList[i].PropertyGrid};
                        object PropertyObj = ClassType.InvokeMember("FillPropertyGridFromState", BindingFlags.InvokeMethod, null, ClassInstance, parametersArray);
                           


                        string StateDescription = TransactionList[i].StateDescription;
                        string TransactionName = TransactionList[i].TransactionName;
                        string Type = TransactionList[i].Type;
                        

                        CopiedItem.BrandId = BrandId;
                        CopiedItem.ChildStateList =ChildStateList;
                        CopiedItem.ConfigId = ConfigId;
                        CopiedItem.dockPanel = DockPanel;
                        CopiedItem.Id = StateNumber;
                        CopiedItem.ParentStateList =ParentStateList;
                        CopiedItem.PropertyGrid.SelectedObject = PropertyObj;
                        CopiedItem.PropertyGrid.SelectedObjectName = StateNumber;
                        CopiedItem.StateDescription = StateDescription;
                        CopiedItem.TransactionName = TransactionName;
                        CopiedItem.Type = Type;
                        
                        CopiedStatesList.Add(CopiedItem);
                    }

                }              
               //CopiedItem=(ModelCanvasStateObject)TransactionList.FindAll(x => x.Id == item.StateNumber).Find(x => x.TransactionName == CurrentTransactionName); 
              
            }
            
            XElement designerItemsXML = SerializeDesignerItems(selectedDesignerItems);
            XElement connectionsXML = SerializeConnections(selectedConnections);

            XElement root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(connectionsXML);

            root.Add(new XAttribute("OffsetX", 10));
            root.Add(new XAttribute("OffsetY", 10));

            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Xaml, root);
        }


        private void UpdateZIndex()
        {
            List<UIElement> ordered = (from UIElement item in this.Children
                                       orderby Canvas.GetZIndex(item as UIElement)
                                       select item as UIElement).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                Canvas.SetZIndex(ordered[i], i);
            }
        }

        private static Rect GetBoundingRectangle(IEnumerable<ViewModelDesignerItem> items)
        {
            double x1 = Double.MaxValue;
            double y1 = Double.MaxValue;
            double x2 = Double.MinValue;
            double y2 = Double.MinValue;

            foreach (ViewModelDesignerItem item in items)
            {
                x1 = Math.Min(Canvas.GetLeft(item), x1);
                y1 = Math.Min(Canvas.GetTop(item), y1);

                x2 = Math.Max(Canvas.GetLeft(item) + item.Width, x2);
                y2 = Math.Max(Canvas.GetTop(item) + item.Height, y2);
            }

            return new Rect(new Point(x1, y1), new Point(x2, y2));
        }

        private void GetConnectors(DependencyObject parent, List<ViewModelConnector> connectors)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is ViewModelConnector)
                {
                    connectors.Add(child as ViewModelConnector);
                }
                else
                    GetConnectors(child, connectors);
            }
        }

        private ViewModelConnector GetConnector(Guid itemID, String connectorName,String StateConnectorDecoratar)
        {
            ViewModelDesignerItem designerItem = (from item in this.Children.OfType<ViewModelDesignerItem>()
                                         where item.ID == itemID
                                         select item).FirstOrDefault();
            Control connectorDecorator = designerItem.Template.FindName(StateConnectorDecoratar, designerItem) as Control;
            connectorDecorator.ApplyTemplate();
            
            ViewModelConnector connector = connectorDecorator.Template.FindName(connectorName, connectorDecorator) as ViewModelConnector;
            return connector;
        }

        #region Gruplama için
        private bool BelongToSameGroup(IModelGroupable item1, IModelGroupable item2)
        {
            IModelGroupable root1 = SelectionService.GetGroupRoot(item1);
            IModelGroupable root2 = SelectionService.GetGroupRoot(item2);

            return (root1.ID == root2.ID);
        }

        #endregion


        int UploadStateList()
        {
            int RetVal = 102;
            string StateType = "";
            try
            {
                log.Info("UploadStateList() Start!");
                StateBusiness = ApplicationServicesProvider.Instance.Provider.TransactionStateService;
                ArrayList StateList = StateBusiness.GetStateList(ProjectName);
                if (StateList.Count == 0)
                {
                    RetVal = 101;
                    return RetVal;
                }

                foreach (object[] processRow in StateList)
                {
                    if (string.IsNullOrEmpty(processRow[5].ToString()) || string.IsNullOrEmpty(processRow[3].ToString()))
                    {
                        RetVal = 99;
                        return RetVal;
                    }

                    StateIdListFromProjectUpload.Add(processRow[3].ToString());
                    StateType = GetStateType(processRow[5].ToString());
                    //wincor EMV stateleri -e
                    if (processRow[5].ToString() == "e")
                        StateType = GetStateType(processRow[8].ToString());

                    if (StateType == "Null")
                    {
                        log.Error("UploadStateList() Hata !  StateType:" + StateType);
                        return 102;
                    }
                    if (StateType == "Z")
                        continue;

                    Type ClassType = Type.GetType("ATMDesigner.UI.States.State" + StateType);
                    Object ClassInstance = Activator.CreateInstance(ClassType, this);
                    object[] parametersArray = new object[] { processRow, StateList };
                    object StateObject = ClassType.InvokeMember("FillStatesFromDB", BindingFlags.InvokeMethod, null, ClassInstance, parametersArray);

                    CurrentTransactionName = processRow[7].ToString();
                    CurrentConfigId = processRow[16].ToString();
                    CurrentBrandId = processRow[17].ToString();
                }

                ArrangeParentStates();
                RetVal = 100;
                log.Info("UploadStateList() DB 'den Upload Başarılı bir şekilde tamamlandı,State Tiplerine göre classlar oluşturuldu");
            }
            catch (Exception ex)
            {
                log.Error("UploadStateList() Hata !  " + ex);
                RetVal = 102;
            }

            return RetVal;
        }

        bool ArrangeParentStates()
        {
           bool Retval=false;
           ModelParentStateObject Parentobj;// = new List<ModelParentStateObject>();
           List<ModelChildStateObject> ChildobjList;// = new List<ModelChildStateObject>();
            
           for (int i = 0; i < TransactionList.Count; i++)
           {
               Parentobj= new ModelParentStateObject();
               ChildobjList = new List<ModelChildStateObject>();
               ChildobjList = TransactionList[i].ChildStateList;
               for (int j = 0; j < ChildobjList.Count; j++)
               {
                   Parentobj.ParentId = TransactionList[i].Id;
                   Parentobj.ParentType = TransactionList[i].Type;
                   Parentobj.PropertyName = ChildobjList[j].PropertyName;
                   TransactionList.FindAll(x => x.Id == ChildobjList[j].ChildId).Find(x => x.Type == ChildobjList[j].ChildType).ParentStateList.Add(Parentobj);
               }
           }
           Retval = true;
           return Retval;
        }




       public string GetStateType(string Type)
        {
            string StateType;
             switch(Type)
              {
                  case ">": StateType = "CAS"; break;
                  case "P": StateType = "P"; break;
                  case "+": StateType = "Plus"; break;
                  case ",": StateType = "Comma"; break;
                  case "-": StateType = "Dash"; break;
                  case ".": StateType = "Dot"; break;
                  case "/": StateType = "Slash"; break;
                  case "?": StateType = "Query"; break;
                  case ";": StateType = "Semicolon"; break;                     
                  case "A": StateType = "A"; break;
                  case "B": StateType = "B"; break;
                  case "b": StateType = "b"; break;
                  case "D": StateType = "D"; break;
                  case "E": StateType = "E"; break;                 
                  case "F": StateType = "F"; break;
                  case "H": StateType = "H"; break;
                  case "I": StateType = "I"; break;
                  case "J": StateType = "J"; break;
                  case "K": StateType = "K"; break;
                  case "k": StateType = "k"; break;
                  case "r": StateType = "r"; break;
                  case "W": StateType = "W"; break;
                  case "X": StateType = "X"; break;
                  case "Y": StateType = "Y"; break;
                  case "Z": StateType = "Z"; break;
                  case "000": StateType = "e000"; break;
                  case "001": StateType = "e001"; break;
                  case "002": StateType = "e002"; break;
                  case "003": StateType = "e003"; break;
                  case "004": StateType = "e004"; break;
                  case "005": StateType = "e005"; break;
                  case "006": StateType = "e006"; break;
                  case "007": StateType = "e007"; break;
                  default: StateType = "Null"; break;
              }

             return StateType;
        }


        #endregion
   
    }

    internal static class Extensions
    {
        public static IList<T> CloneList<T>(this IList<T> list) where T : ICloneable
        {
            IList<T> CList=null;
            foreach (var ls in list)
            {
               CList.Add((T)ls.Clone());
            }

            return CList;// list.Select(item => (T)item.Clone()).ToList();
        }
       
    }


}
