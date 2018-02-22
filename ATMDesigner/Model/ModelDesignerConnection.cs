using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ATMDesigner.UI.Model
{
    public class ModelDesignerConnection : IEnumerable
    {
        public Point Position_Source { get; set; }
        public Point Position_Sink { get; set; }
        public string SourceToolTip { get; set; }
        public string SourceDesignerItemId { get; set; }
        public string SinkDesignerItemId { get; set; }
        public string AnchorAngleSource { get; set; }
        public string AnchorAngleSink { get; set; }

        public ModelDesignerConnector Connector_ConnectionSource { get; set; }
        public ModelDesignerConnector Connector_ConnectionSink { get; set; }
        public PathGeometry pathgeometry { get; set; }


        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
