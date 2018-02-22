using ATMDesigner.UI.ViewModel;
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

namespace ATMDesigner.UI.View
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            //ViewModelPropertyGrid viewmodel = new ViewModelPropertyGrid();
            //this.DataContext = viewmodel;


        }
        private ICommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                //if (loadCommand == null)
                //{
                //    loadCommand = new RelayCommand(p => CanLoad(), p => Load());
                //}
                return loadCommand;
            }
        }
    }
}
