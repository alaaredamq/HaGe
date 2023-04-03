using System.ComponentModel.DataAnnotations.Schema;

namespace HaGe.Core.Entities.Abstract; 

public class BaseEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime? CreationDate { get; set; } = DateTime.Now;
    public DateTime? ModificationDate { get; set; } = DateTime.Now;
}