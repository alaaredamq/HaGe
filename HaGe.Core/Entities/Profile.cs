using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities; 

public class Profile : BaseEntity, IEntityBase<Guid> {
    public Guid UserId { get; set; } 
    public int LevelUnlocked { get; set; } 
}