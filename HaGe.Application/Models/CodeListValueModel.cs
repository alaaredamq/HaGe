using System;
using HaGe.Application.Models.Base;
using Newtonsoft.Json;

namespace HaGe.Application.Models;

public class CodeListValueModel {
    // [JsonProperty("title")] public string Title { get; set; }
    // [JsonProperty("description")] public string Description { get; set; }
    //
    // [JsonProperty("codeListId")] public string CodeListId { get; set; }
    // public DateTime CreationDate { get; set; }
    // public DateTime ModificationDate { get; set; }
    // public int? OrderNumber { get; set; }
    // public Guid? UserId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CodeListId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? OrderNumber { get; set; }
    public CodeListModel CodeList { get; set; }
    
    public CodeListValueModel() { }
}

public class CodeListValueModelResponse : BaseModel {
    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("value")] public string Value { get; set; }
    public string Image { get; set; }

    [JsonProperty("summary")] public string Summary { get; set; }
}