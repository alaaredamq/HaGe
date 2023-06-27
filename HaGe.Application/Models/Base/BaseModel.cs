using System;
using Newtonsoft.Json;

namespace HaGe.Application.Models.Base;

public class BaseModel {
    [JsonProperty("id")] public Guid Id { get; set; }
}