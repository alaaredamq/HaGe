using HaGe.Core.Entities.Base;
using HaGe.Core.Repositories.Base;
using HaGe.Infrastructure.Context;

namespace HaGe.Infrastructure.Repositories.Base; 

public class Repository<T> : RepositoryBase<T, Guid>, IRepository<T>
    where T : class, IEntityBase<Guid>
{
    public Repository(HaGeContext context)
        : base(context)
    {
    }
}