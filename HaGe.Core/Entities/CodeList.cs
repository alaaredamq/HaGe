using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities;

public class CodeList : BaseEntity, IEntityBase<Guid> {
    public string Title { get; set; }
    public string KeyValue { get; set; }
    public string? Group { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    
    [NotMapped] public List<CodeListValue> Values { get; set; }

    public CodeList(string title, string keyValue, string group, DateTime? creationDate, DateTime? modificationDate) {
        Title = keyValue;
        KeyValue = keyValue;
        Group = group;
        CreationDate = DateTime.Now;
        ModificationDate = SqlDateTime.MinValue.Value;
    }

    public CodeList(string title, string keyValue, string group) {
        Title = keyValue;
        KeyValue = keyValue;
        Group = group;
        CreationDate = DateTime.Now;
        ModificationDate = SqlDateTime.MinValue.Value;
    }
}