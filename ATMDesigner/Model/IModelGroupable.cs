using System;

namespace ATMDesigner.UI
{
    public interface IModelGroupable
    {
        Guid ID { get; }
        Guid ParentID { get; set; }
        bool IsGroup { get; set; }
    }
}
