using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ATMDesigner.UI.Model
{
   public class ModelDragObject
    {
        // Xaml string that represents the serialized content
        public String Xaml { get; set; }

        // Defines width and height of the DesignerItem
        // when this DragObject is dropped on the DesignerCanvas
        public Size? DesiredSize { get; set; }


    }
}
