using HaGe.Core.Entities.Base;

namespace HaGe.Core.Repositories.Base; 

public interface IRepository<T> : IRepositoryBase<T, Guid> where T : IEntityBase<Guid>
{
}