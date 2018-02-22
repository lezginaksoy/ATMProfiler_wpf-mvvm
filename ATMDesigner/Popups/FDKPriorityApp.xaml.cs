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
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class FDKPriorityApp : Window
    {

        public FDKPriorityApp(string Value)
        {
            InitializeComponent();
            FillCheckBoxes(Value);
            _FDKValue = Value;    
        }

        #region Properties

        public string FDKValue
        {
            get { return _FDKValue; }
            set { _FDKValue = value; }
        }

        private string _FDKValue;
        
      
        #endregion

        #region Events

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {   
            this.DialogResult = true;
            _FDKValue = Cmbfirst.SelectedItem.ToString() + CmbMedium.SelectedItem.ToString() + CmbLast.SelectedItem.ToString();

        }
      
  
        
        #endregion

        #region Method

        void FillCheckBoxes(string Value)
        {
            string[] FDK = new string[] {"None","A","B","C","D","F","G","H","I"};
            for (int i = 0; i < FDK.Length; i++)
            {
                Cmbfirst.Items.Add(FDK[i]);
                CmbMedium.Items.Add(FDK[i]);
                CmbLast.Items.Add(FDK[i]);
            }
            Cmbfirst.SelectedItem = Value.Substring(0, 1);
            CmbMedium.SelectedItem = Value.Substring(1, 1);
            CmbLast.SelectedItem = Value.Substring(2, 1);

        }

       

        #endregion


    }
}
