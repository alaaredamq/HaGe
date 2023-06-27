using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Caching.Memory;

namespace HaGe.Infrastructure.Repositories;

public class CodeListValueRepository : Repository<CodeListValue>, ICodeListValueRepository {
    private readonly IMemoryCache _memoryCache;

    public CodeListValueRepository(HaGeContext dbContext, IMemoryCache memoryCache)
        : base(dbContext) {
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<CodeListValue>> GetCodeListValueById(Guid codeListId) {
        return Table.Where(x => x.CodeListId == codeListId);
    }

    public async Task<List<CodeListValue>> GetCodeListValues(List<Guid> ids) {
        return Table.Where(x => ids.Contains(x.CodeListId)).ToList();
    }
    
}