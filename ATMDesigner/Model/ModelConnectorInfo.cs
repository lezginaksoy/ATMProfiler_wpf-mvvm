using ATMDesigner.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ATMDesigner.UI.Model
{
    // provides compact info about a connector; used for the 
    // routing algorithm, instead of hand over a full fledged Connector
   
    internal class ModelConnectorInfo
    {
        public double DesignerItemLeft { get; set; }
        public double DesignerItemTop { get; set; }
        public Size DesignerItemSize { get; set; }
        public Point Position { get; set; }
        public EnumConnectorOrientation Orientation { get; set; }
        public string ToolTip { get; set; }
        public string DesignerItemName { get; set; }
    }
}
