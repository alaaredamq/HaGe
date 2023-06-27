using System.Collections.Generic;
using System.Linq;
using HaGe.Application.Models.Base;

namespace HaGe.Application.Models; 

public class SettingsModel : BasicModel {
    public IEnumerable<IGrouping<string, CodeListModel>> CodeLists { get; set; }
    
    public SettingsModel() { }
}