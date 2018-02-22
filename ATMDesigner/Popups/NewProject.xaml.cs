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
    public partial class NewProject : Window
    {
        List<string> ProjectNameList = new List<string>();
        public NewProject(List<string>ProjectList)
        {
            InitializeComponent();
            ProjectNameList = ProjectList;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectNameList.Exists(x=>x.ToString()==txtNewProject.Text))
            {
                MessageBox.Show(txtNewProject.Text + " Adında bir proje Zaten Mevcut !,Lütfen Farklı bir Proje Adı giriniz.");
                this.DialogResult = false;
                return;
            }

            if (string.IsNullOrEmpty(txtNewProject.Text))
            {
                this.DialogResult = false;
            }
            else
            {
                this.DialogResult = true;
            }
          
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtNewProject.SelectAll();
            txtNewProject.Focus();
        }

        public string NewProjectName
        {
            get { return txtNewProject.Text; }
        }


    }
}
