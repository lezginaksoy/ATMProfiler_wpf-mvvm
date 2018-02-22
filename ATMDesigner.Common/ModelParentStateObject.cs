
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ATMDesigner.Common
{
    public class ModelParentStateObject:ICloneable
    {
        public ModelParentStateObject()
        {
        }
        public Point SourcePoint { get; set; }
        public Point SinkPoint { get; set; }
        public string ParentId { get; set; }
        public string ParentType { get; set; }
        public string PropertyName{get;set;}



        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
