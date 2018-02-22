using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using ATMDesigner.UI.Controls;
using ATMDesigner.UI.Model;
using System;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid;
using ATMDesigner.Common;
using ATMDesigner.UI.States;

namespace ATMDesigner.UI
{
    public class ViewModelConnectorAdorner : Adorner
    {
        private PathGeometry pathGeometry;
        private ViewModelDesignerCanvas designerCanvas;
        private ViewModelConnector sourceConnector;
        private Pen drawingPen;

        private ViewModelDesignerItem hitDesignerItem;
        private ViewModelDesignerItem HitDesignerItem
        {
            get { return hitDesignerItem; }
            set
            {
                if (hitDesignerItem != value)
                {
                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = false;

                    hitDesignerItem = value;

                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = true;
                }
            }
        }

        private ViewModelConnector hitConnector;
        private ViewModelConnector HitConnector
        {
            get { return hitConnector; }
            set
            {
                if (hitConnector != value)
                {
                    hitConnector = value;
                }
            }
        }

        public ViewModelConnectorAdorner(ViewModelDesignerCanvas designer, ViewModelConnector sourceConnector)
            : base(designer)
        {
            this.designerCanvas = designer;
            this.sourceConnector = sourceConnector;
            drawingPen = new Pen(Brushes.LightSlateGray, 2);
            drawingPen.LineJoin = PenLineJoin.Miter;
            this.Cursor = Cursors.Cross;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (HitConnector != null)
            {

                if (this.sourceConnector.Orientation.ToString() != "Top" && this.HitConnector.Orientation.ToString() == "Top"
                    && this.sourceConnector.Connections.Count==0)
                {
                    ModelChildStateObject SinkconnObj = new ModelChildStateObject();
                    ModelParentStateObject SourceConnObj = new ModelParentStateObject();                   

                    DockPanel Sourcepnl = (DockPanel)this.sourceConnector.ParentDesignerItem.Content;
                    DockPanel Sinkpnl = (DockPanel)this.HitConnector.ParentDesignerItem.Content;
                    ModelConnectorInfo SourceConnInfo = this.sourceConnector.GetInfo();
                

                    //Bağlanılan state'in Child'ına atanır
                    SinkconnObj.SourcePoint = this.sourceConnector.Position;
                    SinkconnObj.SinkPoint = this.HitConnector.Position;
                    SinkconnObj.ChildId = Sinkpnl.Uid;
                    SinkconnObj.ChildType = Sinkpnl.Tag.ToString();

                    //bağlanan state'in Parent'ına atanır
                    SourceConnObj.SourcePoint = this.sourceConnector.Position;
                    SourceConnObj.SinkPoint = this.HitConnector.Position;
                    SourceConnObj.ParentId = Sourcepnl.Uid;
                    SourceConnObj.ParentType = Sourcepnl.Tag.ToString();
                    SourceConnObj.PropertyName = SourceConnInfo.ToolTip;
               
                    //
                    if (ArrangeProperties(SinkconnObj, SourceConnObj, Sourcepnl, Sinkpnl))
                    {
                        ViewModelConnector sourceConnector = this.sourceConnector;
                        ViewModelConnector sinkConnector = this.HitConnector;
                        sourceConnector.DesignerItemName = Sourcepnl.Tag.ToString();
                        sourceConnector.Name = SourceConnInfo.ToolTip.ToString();

                        sinkConnector.DesignerItemName = Sinkpnl.Tag.ToString();
                        sinkConnector.Name = "Top";

                        ViewModelConnection newConnection = new ViewModelConnection(sourceConnector, sinkConnector);
                        newConnection.SourceToolTip = SourceConnObj.PropertyName;
                        newConnection.SourceDesignerItemId = Sourcepnl.Name;
                        newConnection.SinkDesignerItemId = Sinkpnl.Name;

                        Canvas.SetZIndex(newConnection, designerCanvas.Children.Count);
                        this.designerCanvas.Children.Add(newConnection);
                    }
                }
                else
                {
                    MessageBox.Show("Bağlantıyı doğru şekilde yapınız.!");
                }
            }
            if (HitDesignerItem != null)
            {
                this.HitDesignerItem.IsDragConnectionOver = false;
            }

            if (this.IsMouseCaptured) this.ReleaseMouseCapture();

            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
        }

        private bool ArrangeProperties(ModelChildStateObject SinkconnObj, ModelParentStateObject SourceConnObj, DockPanel Sourcepnl, DockPanel Sinkpnl)
        {          
            bool Retval=false;

            List<ModelCanvasStateObject> TransactionList = new List<ModelCanvasStateObject>();
            TransactionList = this.designerCanvas.TransactionList;
            Type ClassType = Type.GetType("ATMDesigner.UI.States.State" + Sourcepnl.Tag.ToString());

            if (!TransactionList.FindAll(x => x.Id == Sourcepnl.Uid).Exists(x=>x.TransactionName==designerCanvas.CurrentTransactionName))
            {
                MessageBox.Show("Bağlantıyı doğru şekilde yapınız.!");
                Retval = false;
                return Retval; 
            }

            PropertyGrid SelectedPgrid = TransactionList.FindAll(x => x.Id == Sourcepnl.Uid).Find(x=>x.TransactionName==designerCanvas.CurrentTransactionName).PropertyGrid;        
            Object PSelectedObj = SelectedPgrid.SelectedObject;
            PropertyInfo Property = ClassType.GetProperty(SourceConnObj.PropertyName.ToString());
            object PropertyValue = Property.GetValue(PSelectedObj);
            if (Property.GetValue(PSelectedObj).ToString()!="255")
            {
                MessageBox.Show("Bağlantıyı doğru şekilde yapınız.!");
                Retval = false;
                return Retval;
            }
           
            Property.SetValue(PSelectedObj, Sinkpnl.Uid, null);
            this.designerCanvas.TransactionList.FindAll(x => x.Id == Sourcepnl.Uid).Find(x => x.TransactionName == designerCanvas.CurrentTransactionName).PropertyGrid.SelectedObject = PSelectedObj;
           

            //
            //Pointer state bağlanılan stateler için(Transaction arası geçiş)
            //
            if (Sinkpnl.Tag.ToString()=="P")
            {           
                //yeni 
                PointerStateElements elements = new PointerStateElements();
                StateP pointerstate = new StateP();
                elements.SourceStateNumber = Sourcepnl.Uid.ToString();
                elements.SourceStateType = Sourcepnl.Tag.ToString();
                elements.SourcePropertyName = SourceConnObj.PropertyName;
                if (!TransactionList.FindAll(x => x.Id == Sinkpnl.Uid).Exists(x => x.TransactionName == designerCanvas.CurrentTransactionName))
                {
                    MessageBox.Show("Bağlantıyı doğru şekilde yapınız.!");
                    Retval = false;
                    return Retval;
                }

                Object PointerObj = TransactionList.FindAll(x => x.Id == Sinkpnl.Uid).Find(x => x.TransactionName == designerCanvas.CurrentTransactionName).PropertyGrid.SelectedObject;
                pointerstate = (StateP)PointerObj;

                Property.SetValue(PSelectedObj, pointerstate.NextStateNumber, null);
                this.designerCanvas.TransactionList.FindAll(x => x.Id == Sourcepnl.Uid).Find(x => x.TransactionName == designerCanvas.CurrentTransactionName).PropertyGrid.SelectedObject = PSelectedObj;
           

                pointerstate.SourceStatesList.Add(elements);

                this.designerCanvas.TransactionList.FindAll(x => x.Id == Sinkpnl.Uid).Find(x => x.TransactionName == designerCanvas.CurrentTransactionName).PropertyGrid.SelectedObject = pointerstate;

            }
            
            foreach (var item in TransactionList)
            {
                if (item.Id == SourceConnObj.ParentId)
                {
                    item.ChildStateList.Add(SinkconnObj);
                }
                else if (item.Id == SinkconnObj.ChildId)
                {
                    item.ParentStateList.Add(SourceConnObj);
                }
            }
            this.designerCanvas.TransactionList = TransactionList;
            Retval = true;

            return Retval;
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured) this.CaptureMouse();
                HitTesting(e.GetPosition(this));
                this.pathGeometry = GetPathGeometry(e.GetPosition(this));
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, drawingPen, this.pathGeometry);

            // without a background the OnMouseMove event would not be fired
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        private PathGeometry GetPathGeometry(Point position)
        {
            PathGeometry geometry = new PathGeometry();

            EnumConnectorOrientation targetOrientation;
            if (HitConnector != null)
                targetOrientation = HitConnector.Orientation;
            else
                targetOrientation = EnumConnectorOrientation.None;

            List<Point> pathPoints = ViewModelPathFinder.GetConnectionLine(sourceConnector.GetInfo(), position, targetOrientation);

            if (pathPoints.Count > 0)
            {
                PathFigure figure = new PathFigure();
                figure.StartPoint = pathPoints[0];
                pathPoints.Remove(pathPoints[0]);
                figure.Segments.Add(new PolyLineSegment(pathPoints, true));
                geometry.Figures.Add(figure);
            }

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            DependencyObject hitObject = designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                   hitObject != sourceConnector.ParentDesignerItem &&
                   hitObject.GetType() != typeof(ViewModelDesignerCanvas))
            {
                if (hitObject is ViewModelConnector)
                {
                    HitConnector = hitObject as ViewModelConnector;
                    hitConnectorFlag = true;
                }

                if (hitObject is ViewModelDesignerItem)
                {
                    HitDesignerItem = hitObject as ViewModelDesignerItem;
                    if (!hitConnectorFlag)
                        HitConnector = null;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            HitConnector = null;
            HitDesignerItem = null;
        }
   
    
    
    
    
    }
}
