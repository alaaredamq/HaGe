using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories;

public class CodeListRepository : Repository<CodeList>, ICodeListRepository {
    public CodeListRepository(HaGeContext context)
        : base(context) { }

    public async Task<CodeList> GetCodeListByKeyValue(string keyValue) {
        return Table.Where(x => x.KeyValue == keyValue).FirstOrDefault();
    }

    public async Task<List<CodeList>> GetCodeLists(List<string> keyValue) {
        return Table.Where(x => keyValue.Contains(x.KeyValue)).ToList();
    }
}