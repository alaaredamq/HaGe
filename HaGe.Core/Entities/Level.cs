using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities; 

public class Level : BaseEntity, IEntityBase<Guid> {
    public string Name { get; set; }
    public string TrainingPath { get; set; }
    public double MaxExp { get; set; }
    public int TotalSteps { get; set; }
    public Guid? ParentId { get; set; }
    public int Order { get; set; }

    public Level(string name, int order) {
        Name = name;
        Order = order;
    }
}