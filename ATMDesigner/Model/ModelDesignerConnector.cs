
using ATMDesigner.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ATMDesigner.UI.Model
{
    public class ModelDesignerConnector
    {
        public EnumConnectorOrientation Orientation { get; set; }
        public  Point Position { get; set; }
        public ViewModelDesignerItem ParentDesignerItem { get; set; }

    }


}
