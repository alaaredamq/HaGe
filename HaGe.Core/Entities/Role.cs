using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities; 

public class Role : BaseEntity, IEntityBase<Guid> {
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public bool IsView { get; set; } 

    public Role(string name, bool isDefault, bool isView) {
        Name = name;
        IsDefault = isDefault;
        IsView = isView;
    }
}