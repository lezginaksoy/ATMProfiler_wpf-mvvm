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
    public partial class OpenProject : Window
    {
        public OpenProject(List<string> ProjectList)
        {
            InitializeComponent();
            FillProjectNameCombo(ProjectList);
           
        }
        
        #region Properties
        private string _ProjectName;

        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }

    
        #endregion



        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
          
            if (CmbProject.SelectedValue == null || CmbProject.SelectedValue.ToString() == string.Empty)
            {
                this.DialogResult = false;
                _ProjectName = "";
            }
            else
            {
                this.DialogResult = true;
                _ProjectName = CmbProject.SelectedValue.ToString();
            }
           
          
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {         
            CmbProject.Focus();
        }

      

        #region Method

        void FillProjectNameCombo(List<string> atmProjectList)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            for (int i = 0; i < atmProjectList.Count; i++)
            {
                cbp.Add(new ComboBoxPairs(atmProjectList[i], atmProjectList[i]));
            }
            
            CmbProject.DisplayMemberPath = "_Key";
            CmbProject.SelectedValuePath = "_Value";
            CmbProject.ItemsSource = cbp;
            CmbProject.SelectedIndex = 0;           
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
