using ATMDesigner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for NextStateTransaction.xaml
    /// </summary>
    public partial class PointerStateTransaction : Window
    {
        public PointerStateTransaction(List<ModelCanvasStateObject> TransactionList, string NextStateNumber)
        {
            InitializeComponent();
            _NextStateNumber = NextStateNumber;
            TempTransactionList = TransactionList;
            FillStatesCombo(TransactionList,NextStateNumber);
           
        }


        #region Properties

        private string _TransactionName;
        public string TransactionName
        {
            get { return _TransactionName; }
            set { _TransactionName = value; }
        }

        private string _BrandId;
        public string BrandId
        {
            get { return _BrandId; }
            set { _BrandId = value; }
        }

        private string _NextStateNumber;
        public string NextStateNumber
        {
            get { return _NextStateNumber; }
            set { _NextStateNumber = value; }
        }

        List<ModelCanvasStateObject> TempTransactionList = new List<ModelCanvasStateObject>();

        bool IsClosing = false;

        #endregion
        
        #region events

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (CmbNextStates.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Lütfen Bir Seçim Yapınız.!");
                return;
            }

            IsClosing = true;
            this.DialogResult = true;
            TransactionName = CmbNextStates.SelectedValue.ToString();
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {    
            IsClosing = true;
            this.DialogResult = false;
        }

        void PointerStateTransaction_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsClosing)
                e.Cancel = true;
        }  
        
        private void CmbNextStates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbNextStates.SelectedValue.ToString() != "0")
            {
                _TransactionName = CmbNextStates.SelectedValue.ToString();
                _BrandId = TempTransactionList.Find(x => x.TransactionName == TransactionName).BrandId;
            }
        }

        #endregion
        
        #region Method

        void FillStatesCombo(List<ModelCanvasStateObject> TransactionList, string NextStateNumber)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Seçiniz","0")); 
            for (int i = 0; i < TransactionList.Count; i++)
            {
                if (TransactionList[i].Id.PadLeft(3,'0')==NextStateNumber)
                {
                    cbp.Add(new ComboBoxPairs(NextStateNumber + "_" + TransactionList[i].TransactionName,TransactionList[i].TransactionName));           
                }
            }

            CmbNextStates.DisplayMemberPath = "_Key";
            CmbNextStates.SelectedValuePath = "_Value";
            CmbNextStates.SelectedIndex= 0;
            CmbNextStates.ItemsSource = cbp;

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
