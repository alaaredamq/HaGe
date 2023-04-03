using System.Linq.Expressions;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Repositories.Base; 

public interface IRepositoryBase<T, TId> where T : IEntityBase<TId> {
    IQueryable<T> Table { get; }

    IQueryable<T> TableNoTracking { get; }

    Task<T> GetByIdAsync(TId id);
    T GetById(TId id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    IEnumerable<T> AddRange(IEnumerable<T> entities);

    Task<T> SaveAsync(T entity);
    T Save(T entity);

    Task DeleteAsync(T entity);
    int Delete(T entity);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeString = null,
        bool disableTracking = true);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);

    Task SoftDeleteRangeAsync(IEnumerable<T> entities);

    Task<bool> DeleteRangeAsync (IEnumerable<T> entities);
}