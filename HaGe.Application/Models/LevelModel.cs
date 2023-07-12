using HaGe.Core.Entities;

namespace HaGe.Application.Models; 

public class LevelModel {
    public Guid Id { get; set; }
    public DateTime? CreationDate { get; set; } = DateTime.Now;
    public DateTime? ModificationDate { get; set; } = DateTime.Now;
    
    public string Name { get; set; }
    public string TrainingPath { get; set; }
    public string? Image { get; set; }
    
    public Guid? ParentId { get; set; }
    public int TotalSteps { get; set; }
    
    public int Order { get; set; }
    public int ParentOrder { get; set; }
    public int NumberOfTries { get; set; }
    
    public float Accuracy { get; set; }
    public bool? IsActive { get; set; }
    public User? User { get; set; }
    public Profile? Profile { get; set; }
    public string? VideoUrl { get; set; }
    public int Percentage { get; set; }
    public Dictionary<string,int> Occurences { get; set; }
    
    public LevelModel() {
        Occurences = new Dictionary<string, int>();
    }
}