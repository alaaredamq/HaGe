using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories;

public interface ICodeListValueRepository : IRepository<CodeListValue> {
    Task<IEnumerable<CodeListValue>> GetCodeListValueById(Guid codeListId);
    Task<List<CodeListValue>> GetCodeListValues(List<Guid> Ids);
}