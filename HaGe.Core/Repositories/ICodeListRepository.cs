using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories;

public interface ICodeListRepository : IRepository<CodeList>{
    Task<CodeList> GetCodeListByKeyValue(string keyValue);
    Task<List<CodeList>> GetCodeLists(List<string> keyValues);
}