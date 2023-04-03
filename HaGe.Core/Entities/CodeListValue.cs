using System.ComponentModel.DataAnnotations.Schema;
using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities;

public class CodeListValue : BaseEntity, IEntityBase<Guid> {
    

    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CodeListId { get; set; }
    public Guid? UserId { get; set; }
    public int? OrderNumber { get; set; }
    
    [NotMapped]
    public CodeList CodeList { get; set; }
    
    // public  CodeListValue(){}

    public CodeListValue(string title, string description, Guid codeListId) {
        Title = title;
        Description = description;
        CodeListId = codeListId;
        UserId = Guid.Empty;
        CreationDate = DateTime.Now;
        ModificationDate = DateTime.Now;
    }
    
    public CodeListValue(string title, string description, Guid codeListId, Guid userId) {
        Title = title;
        Description = description;
        CodeListId = codeListId;
        UserId = userId;
        CreationDate = DateTime.Now;
        ModificationDate = DateTime.Now;
    }
    
    public CodeListValue(string title, string description, Guid codeListId, int orderNumber) {
        Title = title;
        Description = description;
        CodeListId = codeListId;
        OrderNumber = orderNumber;
        CreationDate = DateTime.Now;
        ModificationDate = DateTime.Now;
    }
}