using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities; 

public class LevelProgression : BaseEntity, IEntityBase<Guid> {
    public Guid ProfileId { get; set; }
    public Guid LevelId { get; set; }
    public int StepNumber { get; set; }
    public double CurrentExp { get; set; }
    public int Percentage { get; set; }

    public LevelProgression(Guid profileId, Guid levelId) {
        ProfileId = profileId;
        LevelId = levelId;
        StepNumber = 0;
        CurrentExp = 0;
        Percentage = 0;
    }
}