using ATMDesigner.UI.Model;
using ATMDesigner.UI.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace ATMDesigner.UI
{
    // Represents a selectable item in the Toolbox/>.
    public class ViewModelToolboxItem : ContentControl
    {
        // caches the start point of the drag operation
        private Point? dragStartPoint = null;

        static ViewModelToolboxItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ViewModelToolboxItem), new FrameworkPropertyMetadata(typeof(ViewModelToolboxItem)));
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.dragStartPoint = new Point?(e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            if (this.dragStartPoint.HasValue)
            {
                // XamlWriter.Save() has limitations in exactly what is serialized,
                // see SDK documentation; short term solution only;
                string xamlString = XamlWriter.Save(this.Content);
                ModelDragObject dataObject = new ModelDragObject();
                dataObject.Xaml = xamlString;

                WrapPanel panel = VisualTreeHelper.GetParent(this) as WrapPanel;
                if (panel != null)
                {
                    // desired size for DesignerCanvas is the stretched Toolbox item size
                    double scale =1.3;
                    dataObject.DesiredSize = new Size(panel.ItemWidth * scale, panel.ItemHeight * scale);
                }

                DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);

                e.Handled = true;
            }
        }
    }

    

}
