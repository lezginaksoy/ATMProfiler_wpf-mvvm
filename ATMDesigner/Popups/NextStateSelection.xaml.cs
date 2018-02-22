using ATMDesigner.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ATMDesigner.UI.Popups
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NextStateSelection : Window
    {
            
        public NextStateSelection(List<ModelCanvasStateObject> TransactionList, string Buttoncontent, string CurrentBrandId,string CurrentTransactionName)
        {
            InitializeComponent();
            FillStatesCombo(TransactionList,CurrentBrandId,CurrentTransactionName);
            CmbState.SelectedValue = Buttoncontent;
            if (Buttoncontent == "255")
                CmbState.SelectedIndex = 0;          

           
        }

        #region Properties

        public string NextStateNumber
        {
            get { return _NextStateNumber; }
            set { _NextStateNumber = value; }
        }

        private string _NextStateNumber;

        bool IsClosing = false;

        #endregion

        #region Events

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (CmbState.SelectedValue == ""||CmbState.SelectedValue == null)
                _NextStateNumber = "255";
            else
            _NextStateNumber = CmbState.SelectedValue.ToString();

            IsClosing = true;
            this.DialogResult = true;
           
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CmbState.SelectedValue == ""||CmbState.SelectedValue == null)
                _NextStateNumber = "255";
            else
                _NextStateNumber = CmbState.SelectedValue.ToString();

            IsClosing = true;
            this.DialogResult = false;           
        }

        void NextStateSelection_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsClosing)
                e.Cancel = true;
        }  

        #endregion
        
        #region Method

        void FillStatesCombo(List<ModelCanvasStateObject> TransactionList, string CurrentBrandId, string CurrentTransactionName)
        {
            string stateNumber;
            string StateDescription;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
           string GenericBrandId = ConfigurationManager.AppSettings["GENERİC"];
           string NCRBrandId = ConfigurationManager.AppSettings["NCR"];
           string WINCORBrandId = ConfigurationManager.AppSettings["WINCOR"];
            

           if (GenericBrandId == CurrentBrandId)
           {
               //Generic için
               for (int i = 0; i < TransactionList.Count; i++)
               {
                   if (TransactionList[i].Type != "P"&&CurrentTransactionName!=TransactionList[i].TransactionName)
                   {
                       Object obj = TransactionList[i].PropertyGrid.SelectedObject;
                       stateNumber = obj.GetType().GetProperty("StateNumber").GetValue(obj, null).ToString();
                       StateDescription = obj.GetType().GetProperty("StateDescription").GetValue(obj, null).ToString();
                       cbp.Add(new ComboBoxPairs(stateNumber + "_" + StateDescription, stateNumber));
                   }
               }
           }
           else if (NCRBrandId == CurrentBrandId)
           {
               //NCR için
               for (int i = 0; i < TransactionList.Count; i++)
               {
                   if ((TransactionList[i].Type != "P" && TransactionList[i].BrandId != WINCORBrandId) && CurrentTransactionName != TransactionList[i].TransactionName)
                   {
                       Object obj = TransactionList[i].PropertyGrid.SelectedObject;
                       stateNumber = obj.GetType().GetProperty("StateNumber").GetValue(obj, null).ToString();
                       StateDescription = obj.GetType().GetProperty("StateDescription").GetValue(obj, null).ToString();
                       cbp.Add(new ComboBoxPairs(stateNumber + "_" + StateDescription, stateNumber));
                   }
               }

           }
           else if (WINCORBrandId == CurrentBrandId)
           {
               //Wincor için
               for (int i = 0; i < TransactionList.Count; i++)
               {
                   if ((TransactionList[i].Type != "P" && TransactionList[i].BrandId != NCRBrandId) && CurrentTransactionName != TransactionList[i].TransactionName)
                   {
                       Object obj = TransactionList[i].PropertyGrid.SelectedObject;
                       stateNumber = obj.GetType().GetProperty("StateNumber").GetValue(obj, null).ToString();
                       StateDescription = obj.GetType().GetProperty("StateDescription").GetValue(obj, null).ToString();
                       cbp.Add(new ComboBoxPairs(stateNumber + "_" + StateDescription, stateNumber));
                   }
               }

           }
                  


            CmbState.DisplayMemberPath = "_Key";
            CmbState.SelectedValuePath = "_Value";
            CmbState.ItemsSource = cbp;

        }
        
        public class ComboBoxPairs
        {
            public string _Key { get; set; }
            public string _Value { get; set; }

            public ComboBoxPairs(string _key, string _value)
            {
                _Key = _key;
                _Value = _value;
            }
        }        
     
        #endregion



    }
}
