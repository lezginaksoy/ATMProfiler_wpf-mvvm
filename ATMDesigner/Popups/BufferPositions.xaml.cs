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
    public partial class BufferPositions : Window
    {

        public BufferPositions(string Value)
        {
            InitializeComponent();
            FillCheckBoxes(Value);
            _BufferPositionValue = Value;    
        }

        #region Properties

        public string BufferPositionValue
        {
            get { return _BufferPositionValue; }
            set { _BufferPositionValue = value; }
        }

        private string _BufferPositionValue;
        
      
        #endregion

        #region Events

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {   
            this.DialogResult = true;
            _BufferPositionValue = Cmbfirst.SelectedItem.ToString() + CmbMedium.SelectedItem.ToString() + CmbLast.SelectedItem.ToString();

        }
      
  
        
        #endregion

        #region Method

        void FillCheckBoxes(string Value)
        {
            for (int i = 0; i <8 ; i++)
            {
                Cmbfirst.Items.Add(i.ToString());
                CmbMedium.Items.Add(i.ToString());
                CmbLast.Items.Add(i.ToString());
            }
            Cmbfirst.SelectedItem = Value.Substring(0, 1);
            CmbMedium.SelectedItem = Value.Substring(1, 1);
            CmbLast.SelectedItem = Value.Substring(2, 1);

        }

       

        #endregion


    }
}
