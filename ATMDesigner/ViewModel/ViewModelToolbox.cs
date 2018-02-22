using System.Windows;
using System.Windows.Controls;

namespace ATMDesigner.UI
{
    // Implements ItemsControl for ToolboxItems    
    public class ViewModelToolbox : ItemsControl
    {
        // Defines the ItemHeight and ItemWidth properties of
        // the WrapPanel used for this Toolbox
        public Size ItemSize
        {
            get { return itemSize; }
            set { itemSize = value; }
        }
        private Size itemSize = new Size(50, 50);

        // Creates or identifies the element that is used to display the given item.        
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ViewModelToolboxItem();
        }

        // Determines if the specified item is (or is eligible to be) its own container.        
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is ViewModelToolboxItem);
        }
    }
}
