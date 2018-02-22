using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ATMDesigner.UI.Controls
{
    public class DragThumb : Thumb
    {
        public DragThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ViewModelDesignerItem designerItem = this.DataContext as ViewModelDesignerItem;
            ViewModelDesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as ViewModelDesignerCanvas;
            if (designerItem != null && designer != null && designerItem.IsSelected)
            {
                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;

                // we only move DesignerItems
                var designerItems = designer.SelectionService.CurrentSelection.OfType<ViewModelDesignerItem>();

                foreach (ViewModelDesignerItem item in designerItems)
                {
                    double left = Canvas.GetLeft(item);
                    double top = Canvas.GetTop(item);

                    minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                    minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);
                }

                double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                double deltaVertical = Math.Max(-minTop, e.VerticalChange);

                foreach (ViewModelDesignerItem item in designerItems)
                {
                    double left = Canvas.GetLeft(item);
                    double top = Canvas.GetTop(item);

                    if (double.IsNaN(left)) left = 0;
                    if (double.IsNaN(top)) top = 0;

                    Canvas.SetLeft(item, left + deltaHorizontal);
                    Canvas.SetTop(item, top + deltaVertical);
                }

                designer.InvalidateMeasure();
                e.Handled = true;
            }
        }
    }
}
