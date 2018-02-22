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
    public partial class Denominations : Window
    {

        public Denominations(string DenomValue)
        {
            InitializeComponent();
            FillCheckBoxes(DenomValue);
            _Denom = DenomValue;         
        }

        #region Properties

        public string Denom
        {
            get { return _Denom; }
            set { _Denom = value; }
        }

        private string _Denom;
        
        int[,] DenomArray = new int[3, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

        #endregion

        #region Events

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {   
            this.DialogResult = true;
        }
      
        private void Checkboxes_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxesValueChanged(sender);
        }

        private void Checkboxes_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxesValueChanged(sender);
        }
        
        #endregion

        #region Method

        private void CheckBoxesValueChanged(object sender)
        {
            CheckBox chk = (CheckBox)sender;
            int mask;
            _Denom = "";
            if (Convert.ToInt32(chk.Name.Substring(1)) < 4)
            {
                DenomArray[0, Convert.ToInt32(chk.Name.Substring(1))] = (chk.IsChecked.Value) ? 1 : 0;
            }
            else if (Convert.ToInt32(chk.Name.Substring(1)) > 7)
            {
                DenomArray[2, Convert.ToInt32(chk.Name.Substring(1)) - 8] = (chk.IsChecked.Value) ? 1 : 0;
            }
            else
            {
                DenomArray[1, Convert.ToInt32(chk.Name.Substring(1)) - 4] = (chk.IsChecked.Value) ? 1 : 0;
            }


            for (int i = 0; i < 3; i++)
            {
                mask = 0;
                for (int j = 0; j < 4; j++)
                {
                    mask += (int)Math.Pow(2, j) * DenomArray[i, j];
                }
                _Denom += mask.ToString("X");
            }
        }

        void FillCheckBoxes(string DenomValue)
        {
            for (int i = 0; i <3 ; i++)
            {
                int part = int.Parse(DenomValue.Substring(i, 1), System.Globalization.NumberStyles.HexNumber);
                ToBinary(part, i);
            }


        }

        void ToBinary(int value,int index)
        {
            Int64 BinaryHolder;
            char[] BinaryArray;
            string BinaryResult = "";         
            while (value > 0)
            {
                BinaryHolder = value % 2;
                BinaryResult += BinaryHolder;
                value = value / 2;
            }
            BinaryArray = BinaryResult.ToCharArray();

            for (int i = 0; i < BinaryArray.Length; i++)
            {
                string ChkName = "C" + ((index * 4) + i).ToString();
                if (BinaryArray[i] == '1')
                {
                    CheckBox chb = (CheckBox)this.FindName(ChkName);
                    chb.IsChecked = true;
                }
            }



        }

        #endregion


    }
}
