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
    public partial class StateNumberSelection : Window
    {
        string CurrentStateNo = "";
        public StateNumberSelection(List<string> avaliableStateNumberList, string Buttoncontent, string CurrentBrandId, List<ModelCanvasStateObject> TransactionList)
        {
            InitializeComponent();
            CurrentStateNo = Buttoncontent;
            FillStatesCombo(avaliableStateNumberList, CurrentBrandId, TransactionList, CurrentStateNo);
            CmbState.SelectedValue = CurrentStateNo;
            CmbState.SelectedItem = CurrentStateNo;
          
        }

        #region Properties

        public string StateNumber
        {
            get { return _StateNumber; }
            set { _StateNumber = value; }
        }

        private string _StateNumber;

        bool IsClosing = false;
        
        #endregion

        #region Events

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            _StateNumber = CurrentStateNo;
            if (!string.IsNullOrEmpty(CmbState.SelectedValue.ToString()))
            {
                 _StateNumber = CmbState.SelectedValue.ToString();
            }

            IsClosing = true;
            this.DialogResult = true;
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            _StateNumber = CurrentStateNo;
            IsClosing = true;
            this.DialogResult = false;
        }

        void StateNumberSelection_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsClosing)
                e.Cancel = true;
        }  

   
        #endregion

        #region Method

        void FillStatesCombo(List<string> avaliableStateNumberList, string CurrentBrandId, List<ModelCanvasStateObject> TransactionList, string CurrentStateNo)
        {        
            string GenericBrandId = ConfigurationManager.AppSettings["GENERİC"];
            string NCRBrandId = ConfigurationManager.AppSettings["NCR"];
            string WINCORBrandId = ConfigurationManager.AppSettings["WINCOR"];
        
            if (CurrentBrandId==NCRBrandId)
            {
                AddStateNumberByBrand(avaliableStateNumberList, TransactionList, WINCORBrandId, CurrentStateNo);
            }
            else if (CurrentBrandId == WINCORBrandId)
            {
                AddStateNumberByBrand(avaliableStateNumberList, TransactionList, NCRBrandId, CurrentStateNo);
            }
            else if (CurrentBrandId==GenericBrandId)
            {
                AddStateNumberByBrand(avaliableStateNumberList, CurrentStateNo);
            }
            
         

          

        }

        void AddStateNumberByBrand(List<string> avaliableStateNumberList, List<ModelCanvasStateObject> TransactionList, string TempBrandId, string CurrentStateNo)
        {
            string stateNumber;           
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            for (int i = 0; i < avaliableStateNumberList.Count; i++)
            {
                stateNumber = avaliableStateNumberList[i];
                cbp.Add(new ComboBoxPairs(stateNumber, stateNumber));
            }


            for (int i = 0; i < TransactionList.Count; i++)
            {
                if (TransactionList[i].BrandId == TempBrandId)
                {
                    stateNumber = TransactionList[i].Id;
                    cbp.Add(new ComboBoxPairs(stateNumber, stateNumber));
                }
            }

            cbp.Add(new ComboBoxPairs(CurrentStateNo, CurrentStateNo));

            CmbState.DisplayMemberPath = "_Key";
            CmbState.SelectedValuePath = "_Value";
            CmbState.ItemsSource = cbp;
           
        }

        void AddStateNumberByBrand(List<string> avaliableStateNumberList, string CurrentStateNo)
        {
            string stateNumber;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            for (int i = 0; i < avaliableStateNumberList.Count; i++)
            {
                stateNumber = avaliableStateNumberList[i];
                cbp.Add(new ComboBoxPairs(stateNumber, stateNumber));
            }

            cbp.Add(new ComboBoxPairs(CurrentStateNo, CurrentStateNo));

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
