
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ATMDesigner.Common
{
    public class ModelChildStateObject : ICloneable
    {
        private Point _SourcePoint; 
        private Point _SinkPoint;
        private string _ChildId;
        private string _ChildType;
        
        public ModelChildStateObject()
        {
        }

        public Point SourcePoint
        {
            get{return _SourcePoint;}
            set { _SourcePoint = value; }
        }

        public Point SinkPoint 
        {
            get { return _SinkPoint; }
            set { _SinkPoint = value; }
        }

        public string ChildId
        {
            get { return _ChildId; }
            set { _ChildId = value; }
        }

        public string ChildType
        {
            get { return _ChildType; }
            set { _ChildType = value; }
        }

        public string PropertyName
        {
            get;
            set;
        }


      
        public object Clone()
        {
            return this.MemberwiseClone();
        }
     

    }
}
