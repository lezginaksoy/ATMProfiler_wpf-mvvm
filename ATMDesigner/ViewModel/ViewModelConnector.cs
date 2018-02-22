using ATMDesigner.UI.Controls;
using ATMDesigner.UI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ATMDesigner.UI
{
    public class ViewModelConnector : Control, INotifyPropertyChanged
    {
        // drag start point, relative to the DesignerCanvas
        private Point? dragStartPoint = null;

        public EnumConnectorOrientation Orientation { get; set; }

        // center position of this Connector relative to the DesignerCanvas
        private Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        private string designeritemname;
        public string DesignerItemName
        {
            get { return designeritemname; }
            set { designeritemname = value; }
        }
        
        // the DesignerItem this Connector belongs to;
        // retrieved from DataContext, which is set in the
        // DesignerItem template
        private ViewModelDesignerItem parentDesignerItem;
        public ViewModelDesignerItem ParentDesignerItem
        {
            get
            {
                if (parentDesignerItem == null)
                    parentDesignerItem = this.DataContext as ViewModelDesignerItem;

                return parentDesignerItem;
            }
            set 
            {
                parentDesignerItem = value;
            }
        }

        // keep track of connections that link to this connector
        private List<ViewModelConnection> connections;
        public List<ViewModelConnection> Connections
        {
            get
            {
                if (connections == null)
                    connections = new List<ViewModelConnection>();
                return connections;
            }
        }

        public ViewModelConnector()
        {
            // fired when layout changes
            base.LayoutUpdated += new EventHandler(Connector_LayoutUpdated);            
        }

        // when the layout changes we update the position property
        void Connector_LayoutUpdated(object sender, EventArgs e)
        {
            ViewModelDesignerCanvas designer = GetDesignerCanvas(this);
            if (designer != null)
            {
                //get centre position of this Connector relative to the DesignerCanvas
                this.Position = this.TransformToAncestor(designer).Transform(new Point(this.Width / 2, this.Height / 2));
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            ViewModelDesignerCanvas canvas = GetDesignerCanvas(this);
            if (canvas != null)
            {
                // position relative to DesignerCanvas
                this.dragStartPoint = new Point?(e.GetPosition(canvas));
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            // but if mouse button is pressed and start point value is set we do have one
            if (this.dragStartPoint.HasValue)
            {
                // create connection adorner 
                ViewModelDesignerCanvas canvas = GetDesignerCanvas(this);
                if (canvas != null)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                    if (adornerLayer != null)
                    {
                        ViewModelConnectorAdorner adorner = new ViewModelConnectorAdorner(canvas, this);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        internal ModelConnectorInfo GetInfo()
        {
            ModelConnectorInfo info = new ModelConnectorInfo();
            info.DesignerItemLeft = ViewModelDesignerCanvas.GetLeft(this.ParentDesignerItem);
            info.DesignerItemTop = ViewModelDesignerCanvas.GetTop(this.ParentDesignerItem);
            info.DesignerItemSize = new Size(this.ParentDesignerItem.ActualWidth, this.ParentDesignerItem.ActualHeight);
            info.Orientation = this.Orientation;
            info.Position = this.Position;
            info.ToolTip = this.ToolTip.ToString();
            return info;
        }

        // iterate through visual tree to get parent DesignerCanvas
        private ViewModelDesignerCanvas GetDesignerCanvas(DependencyObject element)
        {
            while (element != null && !(element is ViewModelDesignerCanvas))
                element = VisualTreeHelper.GetParent(element);

            return element as ViewModelDesignerCanvas;
        }

        #region INotifyPropertyChanged Members

        // we could use DependencyProperties as well to inform others of property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
    
}
