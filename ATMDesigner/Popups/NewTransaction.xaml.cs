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
    /// Interaction logic for NewTransaction.xaml
    /// </summary>
    public partial class NewTransaction : Window
    {
        List<string> transNameList = new List<string>();
        public NewTransaction(List<ModelAtmBrand> atmBrandList, List<ModelAtmConfig> atmConfigList, List<string> transactionNameList)
        {
            InitializeComponent();
            transNameList = transactionNameList;
            FillBrandCombo(atmBrandList);
            FillConfigCombo(atmConfigList);
        }


        #region Properties
        private string _BrandId;

        public string BrandId
        {
            get { return _BrandId; }
            set { _BrandId = value; }
        }

        private string _ConfigId;
        public string ConfigId
        {
            get { return _ConfigId; }
            set { _ConfigId = value; }
        }
      

        #endregion


        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (transNameList.Count>0&&transNameList.Exists(x => x== txtNewTransaction.Text))
            {
                MessageBox.Show(txtNewTransaction.Text + " Transaction zaten mevcut.! Farklı isimde bir  transaction oluşturun.");
                this.DialogResult = false;
                return;
            }

            if (CmbBrand.SelectedValue == null || CmbBrand.SelectedValue.ToString() == string.Empty ||
                CmbConfig.SelectedValue == null || CmbConfig.SelectedValue.ToString() == string.Empty
                || string.IsNullOrEmpty(txtNewTransaction.Text))
            {
                this.DialogResult = false;
            }
            else
            {
                this.DialogResult = true;
                _BrandId = CmbBrand.SelectedValue.ToString();
                _ConfigId = CmbConfig.SelectedValue.ToString();
            }


        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtNewTransaction.SelectAll();
            txtNewTransaction.Focus();
        }

        public string NewTransactionName
        {
            get { return txtNewTransaction.Text; }
        }


        #region Method

        void FillBrandCombo(List<ModelAtmBrand> atmBrandList)
        {          
            CmbBrand.DisplayMemberPath = "BrandName";
            CmbBrand.SelectedValuePath = "BrandId";
            CmbBrand.ItemsSource = atmBrandList;
            CmbBrand.SelectedIndex = 0;           
        }

        void FillConfigCombo(List<ModelAtmConfig> atmConfigList)
        {
            CmbConfig.DisplayMemberPath = "Name";
            CmbConfig.SelectedValuePath = "Code";
            CmbConfig.ItemsSource = atmConfigList;
            CmbConfig.SelectedIndex = 0;
        }
     

        #endregion


    }
}
