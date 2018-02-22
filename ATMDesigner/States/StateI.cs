using System;
using System.ComponentModel;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Reflection;
using ATMDesigner.Common;
using System.Collections;

namespace ATMDesigner.UI.States
{
    /// <summary>
    /// Customer class to be displayed in the property grid
    /// </summary>
    /// 
  
    public class StateI : StateBase
    {

        public StateI(ViewModelDesignerCanvas Canvas)
            : base(Canvas)
        {
            setDefaultData();
        }

        public StateI()
        {
            setDefaultData();
        }              

        #region Converter

        public class DisplayIDConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"Standard format,do not send Buffer A");                
                strings.Add(001,"Standard format,send Buffer A");             
                strings.Add(128,"Extended format,do not send Buffer A");
                strings.Add(129,"Extended format,send Buffer A");
              return strings;
            }
        }

        public class BufferIDConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"No");
                strings.Add(001,"Yes");              
                return strings;
            }
        }

        public class BufferIDTrack1_3Converter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000, "No   No");
                strings.Add(001, "No   Yes");
                strings.Add(002, "Yes   No");
                strings.Add(003, "Yes   Yes");              
                return strings;
            }
        }


        public class PurpDataConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
                strings.Add(000,"Send no buffers");
                strings.Add(001,"Send Buffer B");
                strings.Add(002,"Send Buffer C");
                strings.Add(003,"Send Buffer B and C");            
                return strings;

            }
        }            

        #endregion

        #region State Parameters

        private string _screen;
        private string _timeoutNextState;
        private string _sendTrack2Data;
        private string _send_Track1Track3_Data;
   
        private string _sendOpCodeData;
        private string _sendAmntData;
        private string _sendPinBuffData;
        private string _sendGenPurpDataOrExtState;
        private string _sendGenPurpData; 
       
        [CategoryAttribute("State Parameters"),PropertyOrder(1), DescriptionAttribute("State Screen Number")]
        [Editor(typeof(AssignmentScreenButton), typeof(AssignmentScreenButton))]
        public string ScreenNumber
        {
            get
            {
                return _screen;
            }

            set
            {
                _screen = value.PadLeft(3,'0');
            }
        }

        [CategoryAttribute("State Parameters"), PropertyOrder(2), ReadOnly(true), DescriptionAttribute("Timeout Next State")]
        public string TimeoutNextState
        {
            get
            {
                return _timeoutNextState;
            }

            set
            {
                _timeoutNextState = value.PadLeft(3, '0');
            }
        }
                
        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(3), DescriptionAttribute("Send Track 2 Data")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BufferIDConverter))]         
        public string SendTrack2Data
        {
            get 
            {
                return _sendTrack2Data; 
            }
            set 
            {
                _sendTrack2Data = value.PadLeft(3, '0');
            }
        }

        [Browsable(true)]
        [Category("State Parameters"), PropertyOrder(4), DescriptionAttribute("Send Track 1 Data")]
        [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BufferIDTrack1_3Converter))]
        public string Send_Track1Track3_Data
        {
            get 
            {
                return _send_Track1Track3_Data;
            }
            set 
            {
                _send_Track1Track3_Data = value.PadLeft(3, '0');
            }
        }
             

       [Browsable(true)]
       [Category("State Parameters"), PropertyOrder(6), DescriptionAttribute("Send Operation Code Data")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BufferIDConverter))]  
       public string SendOpCodeData
       {
           get 
           {
               return _sendOpCodeData; 
           }
           set 
           {
               _sendOpCodeData = value.PadLeft(3, '0'); 
           }
       }

       [Browsable(true)]
       [Category("State Parameters"), PropertyOrder(7), DescriptionAttribute("Send Amount Data")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(BufferIDConverter))]  
       public string SendAmntData
       {
           get
           {
               return _sendAmntData;
           }
           set
           {
               _sendAmntData = value.PadLeft(3, '0');
           }
       }

       [Browsable(true)]
       [Category("State Parameters"), PropertyOrder(8), DescriptionAttribute("Send Pin Buffer Data")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(DisplayIDConverter))]      
       public string SendPinBuffData
       {
           get
           {
               return _sendPinBuffData;
           }
           set
           {
               _sendPinBuffData = value.PadLeft(3, '0'); 
           }
       }
        
       
       [Browsable(true)]
       [Category("Send General Purpose Buffer Data"), PropertyOrder(9), DescriptionAttribute("Send General Purpose Buffer")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(PurpDataConverter))]         
       public string SendGenPurpData
       {
           get 
           { 
               return _sendGenPurpData;
           }
           set
           {
               _sendGenPurpData = value.PadLeft(3, '0'); 
           }
       }


       [Browsable(false)]      
       public string SendGenPurpDataOrExtState
       {
           get
           {
               return _sendGenPurpDataOrExtState;
           }
           set
           {
               _sendGenPurpDataOrExtState = value.PadLeft(3, '0');
           }
       }


        #endregion
        
        #region Extension State 1-2
       //SendPinBuffer seçimine göre 
       private string _ExtensionStateNumber1;
       private string _ExtensionType;    
       private string _ExtensionDescription1;
       private string _SendGenPurpData_Ex;
       private string _SendOptionalDataAH;
       private string _SendOptionalDataIL;
       private string _SendOptionalDataQV;
       private string _SendOptionalData;
       private string _Reserved;
       private string _EMVCAMProcessingFlag;
       private string _PerformEMVCAMProcessingFlag;
       private string _EMVCAMProcessing;

        //Extension 2
       private string _ExtensionStateNumber2;
       private string _ExtensionDescription2;
       private string _OfflineDeclineNextState;

       #region converter

       public class OptDataAHConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
       {
           public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
           {
               Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
               strings.Add(000,"None");
               strings.Add(001,"A");
               strings.Add(002,"B");
               strings.Add(003,"C");
               strings.Add(004,"D");
               strings.Add(005,"E");
               strings.Add(006,"F");
               strings.Add(007,"G");
               strings.Add(008,"H");
               return strings;

           }
       }

       public class OptDataILConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
       {
           public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
           {
               Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
               strings.Add(000, "None");               
               strings.Add(001,"I");
               strings.Add(002,"J");
               strings.Add(003,"K");
               strings.Add(004,"L");
               strings.Add(005,"Reserved For M");
               strings.Add(006,"Reserved For N");
               strings.Add(007,"Reserved For O");
               strings.Add(008,"Reserved For P");
               return strings;

           }
       }

       public class OptDataQVConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
       {
           public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
           {
               Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
               strings.Add(000,"None");
               strings.Add(001,"Q");
               strings.Add(002,"R");
               strings.Add(003,"S");
               strings.Add(004,"Reserved For T");
               strings.Add(005,"U");
               strings.Add(006,"V");
               strings.Add(007,"w");
               strings.Add(008,"a");
               return strings;

           }
       }

       public class OptDataConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
       {
           public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
           {
               Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
               strings.Add(000,"None");
               strings.Add(001,"User Data field");
               strings.Add(002,"b");
               strings.Add(003,"Reserved");
               strings.Add(004,"Reserved");
               strings.Add(005,"e");
               strings.Add(006,"g");
               strings.Add(007,"Reserved");
               strings.Add(008,"Reserved");
               return strings;

           }
       }
    
        public class EMVCAMConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
       {
           public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
           {
               Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
               strings.Add(0,"Do not perform EMV CAM processing (default)");
               strings.Add(1,"Perform EMV CAM processing");              
               return strings;
           }
       }

        public class PerformEMVCAMConverter : Xceed.Wpf.Toolkit.PropertyGrid.Attributes.IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection strings = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();             
                strings.Add(0,"Perform Full EMV Processing(default)");
                strings.Add(1,"Perform partial EMV Processing");
                return strings;
            }
        }

     #endregion

       //extension1
        [Browsable(false)]  
        [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
        [CategoryAttribute("State Extension  Parameters"), PropertyOrder(8), DescriptionAttribute("Extension State Number")]
        public string ExtensionStateNumber1
        {
            get 
            {
                return _ExtensionStateNumber1;
            }
            set
            {
                _ExtensionStateNumber1 = value.PadLeft(3, '0');
            }

        }

       [Browsable(false)]       
       [CategoryAttribute("State Extension Parameters"), PropertyOrder(9), DescriptionAttribute("Extension Type"), ReadOnlyAttribute(true)]     
       public string ExtensionType
       {
           get { return _ExtensionType; }          
       }
               
       [Browsable(false)]      
       [CategoryAttribute("State Extension Parameters"), PropertyOrder(10), DescriptionAttribute("Extension Description")]
       public string ExtensionDescription1
       {
           get
           {
               return _ExtensionDescription1;
           }
           set
           {
               _ExtensionDescription1 = value;
           }
       }

       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(11), DescriptionAttribute("Send General Purpose Buffer")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(PurpDataConverter))]         
       public string SendGenPurpData_extension
       {
           get 
           { return _SendGenPurpData_Ex; }
           set 
           { _SendGenPurpData_Ex = value.PadLeft(3,'0'); }
       }

       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(12), DescriptionAttribute("Send Optional Data Field A-H")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(OptDataAHConverter))]    
       public string SendOptionalDataAH
       {
           get { return _SendOptionalDataAH; }
           set { _SendOptionalDataAH = value; }
       }

       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(13), DescriptionAttribute("Send Optional Data Field I-L")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(OptDataILConverter))]    
       public string SendOptionalDataIL
       {
           get { return _SendOptionalDataIL; }
           set { _SendOptionalDataIL = value; }
       }

       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(14), DescriptionAttribute("Send Optional Data Field Q-V")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(OptDataQVConverter))]   
       public string SendOptionalDataQV
       {
           get { return _SendOptionalDataQV; }
           set { _SendOptionalDataQV = value; }
       }

       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(15), DescriptionAttribute("Send Optional Data Field")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(OptDataConverter))]      
       public string SendOptionalData
       {
           get { return _SendOptionalData; }
           set { _SendOptionalData = value; }
       }

       [Browsable(false), ReadOnlyAttribute(true)]
       [Category("State Extension Parameters"), PropertyOrder(15), DescriptionAttribute("Reserved")]
       public string Reserved
       {
           get
           {
               return _Reserved;
           }
       }
        
       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(16), DescriptionAttribute("EMV CAM Processing Flag")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(EMVCAMConverter))]     
       public string EMVCAMProcessingFlag
       {
           get { return _EMVCAMProcessingFlag; }
           set { _EMVCAMProcessingFlag = value.PadLeft(3,'0'); }
       }

       [Browsable(false)]
       [Category("State Extension Parameters"), PropertyOrder(17), DescriptionAttribute("Preform EMV CAM Processing Flag")]
       [Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemsSource(typeof(PerformEMVCAMConverter))]     
       public string PerformEMVCAMProcessingFlag
       {
           get { return _PerformEMVCAMProcessingFlag; }
           set { _PerformEMVCAMProcessingFlag = value.PadLeft(3, '0'); }
       }

       [Browsable(false)]         
       public string EMVCAMProcessing
       {
           get { return _EMVCAMProcessing; }
           set {_EMVCAMProcessing = value.PadLeft(3, '0'); }
       }

        //Extension 2
       [Browsable(false)]
       [Editor(typeof(SetExtensionStateNumber), typeof(SetExtensionStateNumber))]
       [CategoryAttribute("State Extension2 Parameters"), PropertyOrder(9), DescriptionAttribute("Extension State Number 2")]    
       public string ExtensionStateNumber2
       {
           get
           {
               return _ExtensionStateNumber2;
           }
           set
           {
               _ExtensionStateNumber2 = value.PadLeft(3, '0');
           }

       }
      
       [Browsable(false)]
       [CategoryAttribute("State Extension2 Parameters"), PropertyOrder(10), DescriptionAttribute("Extension Description")]
       public string ExtensionDescription2
       {
           get
           {
               return _ExtensionDescription2;
           }
           set
           {
               _ExtensionDescription2 = value;
           }
       }

       [Browsable(false)]
       [Category("State Extension2 Parameters"), PropertyOrder(18),ReadOnly(true), DescriptionAttribute("Offline Decline NextState Number")]    
       public string OfflineDeclineNextState
       {
           get { return _OfflineDeclineNextState; }
           set { _OfflineDeclineNextState = value.PadLeft(3, '0');  }
       }
        
       #endregion

        #region Events and Methods

       public override Object StateChanged(string SelectedProperty, string newValue, Object ClassInstance,PropertyGrid SelectedPgrid)
       {
           // IState'inde SendPinBuffData propertyinin eventinde
           if (SelectedProperty == "SendPinBuffData")
           {
               string[] ExtensionStates = new string[] {"ExtensionStateNumber1","ExtensionDescription1", "ExtensionType", "SendGenPurpData_extension", "SendOptionalDataAH", "SendOptionalDataIL",
              "SendOptionalDataQV","SendOptionalData","EMVCAMProcessingFlag","ExtensionStateNumber2","ExtensionDescription2","OfflineDeclineNextState"};
               bool Standarstatus = (newValue == "000" || newValue == "001") ? true : false;
               bool extensionstatus = (newValue == "128" || newValue == "129") ? true : false;
               if (!extensionstatus)
               {
                   ExtensionStates = null;
                   ExtensionStates = new string[] {"ExtensionStateNumber1","ExtensionDescription1", "ExtensionType", "SendGenPurpData_extension", "SendOptionalDataAH", "SendOptionalDataIL",
              "SendOptionalDataQV","SendOptionalData","EMVCAMProcessingFlag","ExtensionStateNumber2","ExtensionDescription2","OfflineDeclineNextState","PerformEMVCAMProcessingFlag"};
               }
               
               string VisibleProperty ="SendGenPurpData";
               PropertyDescriptor theDescriptor = TypeDescriptor.GetProperties(ClassInstance.GetType())[VisibleProperty];
               BrowsableAttribute theDescriptorBrowsableAttribute = (BrowsableAttribute)theDescriptor.Attributes[typeof(BrowsableAttribute)];
               FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
               isBrowsable.SetValue(theDescriptorBrowsableAttribute, Standarstatus);
               for (int i = 0; i < ExtensionStates.Length; i++)
               {
                   VisibleProperty = ExtensionStates[i];
                   PropertyDescriptor theDescriptor1 = TypeDescriptor.GetProperties(ClassInstance.GetType())[VisibleProperty];
                   BrowsableAttribute theDescriptorBrowsableAttribute1 = (BrowsableAttribute)theDescriptor1.Attributes[typeof(BrowsableAttribute)];
                   FieldInfo isBrowsable1 = theDescriptorBrowsableAttribute1.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
                   isBrowsable.SetValue(theDescriptorBrowsableAttribute1, extensionstatus);
               }
           }

           if (SelectedProperty=="EMVCAMProcessingFlag")
           {
               string VisibleProperty = "PerformEMVCAMProcessingFlag";
               bool Standarstatus = (newValue== "0") ? false : true;
               PropertyDescriptor theDescriptor = TypeDescriptor.GetProperties(ClassInstance.GetType())[VisibleProperty];
               BrowsableAttribute theDescriptorBrowsableAttribute = (BrowsableAttribute)theDescriptor.Attributes[typeof(BrowsableAttribute)];
               FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
               isBrowsable.SetValue(theDescriptorBrowsableAttribute, Standarstatus);             
           }


           return FillStateFromPropertyGrid(ClassInstance, SelectedPgrid);
       }

       public override Object FillPropertyGridFromState(Object ClassInstance, PropertyGrid SelectedPgrid)
       {
           StateI Selectedstate = new StateI();
           Selectedstate = (StateI)SelectedPgrid.SelectedObject;
           StateI Dynamicstate = new StateI();
           Dynamicstate = (StateI)ClassInstance;
           Dynamicstate.BrandId = Selectedstate.BrandId;
           Dynamicstate.ConfigId = Selectedstate.ConfigId;
           Dynamicstate._Description = Selectedstate.StateDescription;
           Dynamicstate._ExtensionDescription1 = Selectedstate.ExtensionDescription1;
           Dynamicstate._ExtensionDescription2 = Selectedstate.ExtensionDescription2;

           Dynamicstate._EMVCAMProcessing = Selectedstate._EMVCAMProcessing;
           //Dynamicstate._OfflineDeclineNextStateNumber = Selectedstate._OfflineDeclineNextStateNumber;
           Dynamicstate._screen = Selectedstate._screen;
           Dynamicstate._sendAmntData = Selectedstate._sendAmntData;
           Dynamicstate._SendGenPurpData_Ex = Selectedstate._SendGenPurpData_Ex;
           Dynamicstate._sendGenPurpDataOrExtState = Selectedstate._sendGenPurpDataOrExtState;
           Dynamicstate._sendOpCodeData = Selectedstate._sendOpCodeData;
           Dynamicstate._SendOptionalData = Selectedstate._SendOptionalData;
           Dynamicstate._SendOptionalDataAH = Selectedstate._SendOptionalDataAH;
           Dynamicstate._SendOptionalDataIL = Selectedstate._SendOptionalDataIL;
           Dynamicstate._SendOptionalDataQV = Selectedstate._SendOptionalDataQV;
           Dynamicstate._sendPinBuffData = Selectedstate._sendPinBuffData;
           Dynamicstate.Send_Track1Track3_Data = Selectedstate.Send_Track1Track3_Data;
           Dynamicstate._sendTrack2Data = Selectedstate._sendTrack2Data;
           //Dynamicstate._timeoutnextState = Selectedstate._timeoutnextState;
           return Dynamicstate;
       }

       private static StateI FillStateFromPropertyGrid(Object ClassInstance, PropertyGrid SelectedPgrid)
       {
           StateI Selectedstate = new StateI();
           Selectedstate = (StateI)SelectedPgrid.SelectedObject;
           StateI Dynamicstate = new StateI();
           Dynamicstate = (StateI)ClassInstance;

           Dynamicstate._EMVCAMProcessing = "000";
           if ( Selectedstate._EMVCAMProcessingFlag=="1")
           {
               Dynamicstate.EMVCAMProcessing = "001";
               if (Dynamicstate._PerformEMVCAMProcessingFlag=="1")
               {
                   Dynamicstate.EMVCAMProcessing = "003";
               }
           }

           Dynamicstate._Description = Selectedstate.StateDescription;
           Dynamicstate.BrandId = Selectedstate.BrandId;
           Dynamicstate.ConfigId = Selectedstate.ConfigId;
           Dynamicstate._ExtensionDescription1 = Selectedstate.ExtensionDescription1;
           Dynamicstate._ExtensionDescription2 = Selectedstate.ExtensionDescription2;

           Dynamicstate._OfflineDeclineNextState = Selectedstate._OfflineDeclineNextState;
           Dynamicstate._screen = Selectedstate._screen;
           Dynamicstate._sendAmntData = Selectedstate._sendAmntData;
           Dynamicstate._SendGenPurpData_Ex = Selectedstate._SendGenPurpData_Ex;
           Dynamicstate._sendGenPurpDataOrExtState = Selectedstate._sendGenPurpDataOrExtState;
           Dynamicstate._sendOpCodeData = Selectedstate._sendOpCodeData;
           Dynamicstate._SendOptionalData = Selectedstate._SendOptionalData;
           Dynamicstate._SendOptionalDataAH = Selectedstate._SendOptionalDataAH;
           Dynamicstate._SendOptionalDataIL = Selectedstate._SendOptionalDataIL;
           Dynamicstate._SendOptionalDataQV = Selectedstate._SendOptionalDataQV;
           Dynamicstate._sendPinBuffData = Selectedstate._sendPinBuffData;
           Dynamicstate.Send_Track1Track3_Data = Selectedstate.Send_Track1Track3_Data;
           Dynamicstate._sendTrack2Data = Selectedstate._sendTrack2Data;          
           Dynamicstate._timeoutNextState = Selectedstate._timeoutNextState;
           return Dynamicstate;
       }

       public override object CreateInsertCommandScript(PropertyGrid SelectedPgrid, string ProjectName, string TransactionName, int ExtensionStateNumber)
       {
           StateI State = new StateI();
           State = (StateI)SelectedPgrid.SelectedObject;
           List<string> SqlStringList = new List<string>();

           string sql = SqlStr;
     
           State.SendGenPurpDataOrExtState = State.SendGenPurpData;
           if ((State.SendPinBuffData=="128"||State.SendPinBuffData=="129"))
           {
               string exsql = sql;
               if (State.ExtensionStateNumber1 != "255")
               {

                   exsql = string.Format(exsql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber1,
                   State.ExtensionDescription1, State.ExtensionType, ProjectName, TransactionName, State.SendGenPurpData_extension,
                   State.SendOptionalDataAH, State.SendOptionalDataIL, State.SendOptionalDataQV, State.SendOptionalData, State.Reserved,
                   State.EMVCAMProcessing, State.ExtensionStateNumber2, State.ConfigId, State.BrandId, State.ConfigVersion);
                   SqlStringList.Add(exsql);
                   State.SendGenPurpDataOrExtState = State.ExtensionStateNumber1;
                   
                   if (State.OfflineDeclineNextState != "255")
                   {
                       string exsql1 = sql;
                       if (State.ExtensionStateNumber2 == "255")
                       {
                           exsql1 = string.Format(exsql1, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.ExtensionStateNumber2,
                               State.ExtensionDescription2, State.ExtensionType, ProjectName, TransactionName,
                               State.OfflineDeclineNextState, State.Reserved, State.Reserved, State.Reserved, State.Reserved, State.Reserved,
                               State.Reserved, State.Reserved, State.ConfigId, State.BrandId, State.ConfigVersion);
                           SqlStringList.Add(exsql1);
                       }
                   }

               }
 
           }

         
           sql = string.Format(sql, Guid, Status, DateTime.Now.ToString("yyyyMMddHHmmss"), State.StateNumber, State.StateDescription,
               State.StateType, ProjectName, TransactionName, State.ScreenNumber, State.TimeoutNextState, State.SendTrack2Data,
               State.Send_Track1Track3_Data, State.SendOpCodeData, State.SendAmntData, State.SendPinBuffData, State.SendGenPurpDataOrExtState,
               State.ConfigId, State.BrandId, State.ConfigVersion);
           SqlStringList.Add(sql);

           return SqlStringList;
       }
        
       private void setDefaultData()
       {
           StateType = "I";
           StateDescription = "Transaction Request State";
           _screen = "000";
           _OfflineDeclineNextState = "255";
           _timeoutNextState = "255";
           _ExtensionType = "Z";
           _ExtensionDescription1 = "State Z";
           _ExtensionDescription2 = "State Z";
           
           _SendGenPurpData_Ex = "000";
           _SendOptionalDataAH = "000";
           _SendOptionalDataIL = "000";
           _SendOptionalDataQV = "000";
           _SendOptionalData = "000";
           _Reserved = "000";
           _EMVCAMProcessing = "000";
           _EMVCAMProcessingFlag = "0";
           _PerformEMVCAMProcessingFlag = "0";
           _ExtensionStateNumber1 = "255";
           _ExtensionStateNumber2 = "255";
           _send_Track1Track3_Data = "000";
           _sendTrack2Data = "000";
           _sendOpCodeData = "000";
           _sendAmntData = "000";
           _sendPinBuffData = "000";
           _sendGenPurpData = "000";
           _sendGenPurpDataOrExtState = "255";
       }



       public override object FillStatesFromDB(object[] processRow, ArrayList StateList)
       {
           StateI state = new StateI();
           ModelCanvasStateObject TransStateObj = new ModelCanvasStateObject();
           List<ModelParentStateObject> ParentobjList = new List<ModelParentStateObject>();
           List<ModelChildStateObject> ChildobjList = new List<ModelChildStateObject>();
            
           state.Status = processRow[1].ToString();
           state.StateNumber = processRow[3].ToString();
           state.StateDescription = processRow[4].ToString();
           state.StateType = processRow[5].ToString();

           state._screen = processRow[8].ToString();
           state._timeoutNextState = processRow[9].ToString();
           state._sendTrack2Data = processRow[10].ToString();
           state._send_Track1Track3_Data = processRow[11].ToString();
           state._sendOpCodeData = processRow[12].ToString();
           state._sendAmntData = processRow[13].ToString();
           state._sendPinBuffData = processRow[14].ToString();
           state._sendGenPurpDataOrExtState = processRow[15].ToString();

           state.ConfigId = processRow[16].ToString();
           state.BrandId = processRow[17].ToString();
           state.ConfigVersion = processRow[18].ToString();

           //Extension State Kontrolu
           if (state.SendPinBuffData == "128" || state.SendPinBuffData == "129")
           {
               object[] ExtensionState = GetExtensionState(ref StateList, state.SendGenPurpDataOrExtState);
               state._sendGenPurpDataOrExtState = ExtensionState[3].ToString();
               state._ExtensionDescription1 = ExtensionState[4].ToString();

               state._SendGenPurpData_Ex = ExtensionState[8].ToString();
               state._SendOptionalDataAH = ExtensionState[9].ToString();
               state._SendOptionalDataIL = ExtensionState[10].ToString();
               state._SendOptionalDataQV = ExtensionState[11].ToString();
               state._SendOptionalData = ExtensionState[12].ToString();
               state._Reserved = ExtensionState[13].ToString();
               state._EMVCAMProcessing = ExtensionState[14].ToString();
               state._ExtensionStateNumber2 = ExtensionState[15].ToString();

               if (state.ExtensionStateNumber2 != "255")
               {
                   object[] ExtensionStates2 = GetExtensionState(ref StateList, state.ExtensionStateNumber2);
                   state._ExtensionDescription2 = ExtensionStates2[4].ToString();

                   state._OfflineDeclineNextState = ExtensionStates2[8].ToString();
               }

           }

           //NextState Kontrolu
           if (state.TimeoutNextState != "255")
           {
               ChildobjList.Add(GetChildState("TimeoutNextState", state.TimeoutNextState, StateList, processRow[7].ToString(), state.StateType, state.StateNumber));
           }

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
        
       private object[] GetExtensionState(ref ArrayList StateList, string ExtensionStateNumber)
       {
           object[] ExtensionState = null;
           foreach (object[] StatedRow in StateList)
           {
               //Extension state sorgusu
               if (ExtensionStateNumber == StatedRow[3].ToString().PadLeft(3, '0'))
               {
                   ExtensionState = StatedRow;
                  // StateList.Remove(StatedRow);
                   return ExtensionState;
               }
           }

           return ExtensionState;
       }
    

       #endregion

      
    }
}