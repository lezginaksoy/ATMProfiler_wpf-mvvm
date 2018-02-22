using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace ATMDesigner.Common
{
    public class ModelCanvasStateObject:ICloneable
    {

        public ModelCanvasStateObject()
        {
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string TransactionName { get; set; }
        public string ConfigId { get; set; }
        public string BrandId { get; set; }

        public string StateDescription { get; set; }
        public List<ModelChildStateObject> ChildStateList = new List<ModelChildStateObject>();
        public List<ModelParentStateObject> ParentStateList = new List<ModelParentStateObject>();
        public PropertyGrid PropertyGrid=new PropertyGrid();
        public DockPanel dockPanel = new DockPanel();

               

        public object Clone()
        {
           // ModelCanvasStateObject canvasobj = (ModelCanvasStateObject)this.MemberwiseClone();
         
            return this.MemberwiseClone();
        }



     
    }
}
