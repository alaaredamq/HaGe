using HaGe.Application.Models.Base;
using Newtonsoft.Json;

namespace HaGe.Application.Models; 

public class CodeListModel : BaseModel {
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("creationDate")] public DateTime CreationDate { get; set; }
    [JsonProperty("keyValue")] public string KeyValue { get; set; }
    [JsonProperty("modificationDate")] public DateTime ModificationDate { get; set; }
    [JsonProperty("group")] public string Group { get; set; }
    [JsonProperty("codeListValues")] public List<CodeListValueModel> CodeListValues { get; set; }
    
    public CodeListModel () { }
}