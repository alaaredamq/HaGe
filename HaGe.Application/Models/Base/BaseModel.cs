using System;
using HaGe.Core.Entities;
using Newtonsoft.Json;

namespace HaGe.Application.Models.Base;

public class BaseModel {
    [JsonProperty("id")] public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public User? User { get; set; }
    public Profile? Profile { get; set; }
    public List<Level> LevelProgressions { get; set; }
    public string Country { get; set; }
    public Dictionary<LevelModel, List<LevelModel>> Levels { get; set; }
}