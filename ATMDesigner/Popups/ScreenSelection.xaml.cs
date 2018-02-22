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
    public partial class ScreenSelection : Window
    {
        public string ScreenNumber;       
        public ScreenSelection(string CurrentScreenNumber)
        {
            InitializeComponent();
            ScreenNumber = CurrentScreenNumber;            
            txtScreenNo.Text= CurrentScreenNumber;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtScreenNo.Text))
            {
                MessageBox.Show("Screen Number alanını boş bıraktığınız için Default olarak 000 atanacak.!");
                this.DialogResult = true;              
            }
            else
            {
                this.DialogResult = true;
                ScreenNumber = txtScreenNo.Text;
            }          
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {        
            this.DialogResult = true;
        }

        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void textBoxValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c) || Char.IsControl(c); });
        } 

    }
}
