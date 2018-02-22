
namespace ATMDesigner.UI
{
    // Common interface for items that can be selected
    // on the DesignerCanvas; used by DesignerItem and Connection
    public interface IModelSelectable
    {
        bool IsSelected { get; set; }
    }
}
