using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HaGe.Core.Entities.Base;
using HaGe.Core.Repositories.Base;
using HaGe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HaGe.Infrastructure.Repositories.Base; 

public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : class, IEntityBase<TId>
    {
        public RepositoryBase(HaGeContext context)
        {
            _context = context;
        }

        private readonly DbContext _context;

        private DbSet<T> _entities;

        protected virtual DbSet<T> Entities
        {
            get { return _entities ??= _context.Set<T>(); }
        }

        public virtual async Task<T> GetByIdAsync(TId id)
        {
            return await Entities.FindAsync(id);
        }

        public virtual T GetById(TId id)
        {
            return Entities.Find(id);
        }

        public async virtual Task<List<T>> GetByListIdAsync(List<TId> ids)
        {
            return await Table.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        //public async virtual IEnumerable<Task<T>> GetByIdsAsync(IEnumerable<T> entities)

        //{   
            
        //    return entitiesToReturn;
        //}

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ignored
            }

            return entities;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // ignored
            }

            return entities;
        }
        
        public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
        {
            if (!entities.Any()) return true;
            Entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            try
            {
                Entities.UpdateRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> SaveAsync(T entity)
        {
            try
            {
                if (entity.Id == null || entity.Id.Equals(default(TId)))
                {
                    var x = Entities.Add(entity);
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    //_context.Entry(entity).CurrentValues.SetValues(entity);

                }
                await _context.SaveChangesAsync();
            }

            catch (Exception e) { }
            return entity;
        }

        public T Save(T entity)
        {
            try
            {
                if (entity.Id == null || entity.Id.Equals(default(TId)))
                {
                    var x = Entities.Add(entity);
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    //_context.Entry(entity).CurrentValues.SetValues(entity);

                }
                _context.SaveChanges();
            }

            catch (Exception e) { }
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public int Delete(T entity)
        {
            Entities.Remove(entity);
            return _context.SaveChanges();
        }

        // Soft delete range async
        public async Task SoftDeleteRangeAsync(IEnumerable<T> entities)
        {
            if (entities.Any())
            {   
                // Find specific column
                var column = _context.Model.FindEntityType(typeof(T)).FindProperty("IsDeleted");
                if (column != null)
                {
                    foreach (var entity in entities)
                    {
                        // Set value to true
                        _context.Entry(entity).Property(column.Name).CurrentValue = true;
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task AddRemoveLikeAsync(T entity){
            if (entity != null) {
                Entities.Add(entity);
            } else {
                Entities.Remove(entity);
            }
            
            try {
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw;
            }
        }

        public int AddRemoveLike(T entity){
            var i = 0;
            if (entity.Id == null || entity.Id.Equals(default(TId))) {
                Entities.Add(entity);
            } else {
                Entities.Remove(entity);
                i = 1;
            }
            _context.SaveChanges();
            return i;
        }

        public int Recommend(T entity){
            if (entity == null) return -1;
            var column = _context.Model.FindEntityType(typeof(T)).FindProperty("IsRecommended");
            if (column == null) return -1;
            var value = (bool)_context.Entry(entity).Property(column.Name).CurrentValue;
            _context.Entry(entity).Property(column.Name).CurrentValue = value switch
            {
                true => false,
                false => true,
            };
            try {
                return _context.SaveChanges();
            }
            catch (Exception e) {
                throw;
            }
            return -1;
        }

        public virtual async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await Entities.ToListAsync();
        }
        
        public virtual IQueryable<T> ListAllAsyncAsQueryable()
        {
            return Entities.AsQueryable();
        }
        
        public virtual IQueryable<T> ListAllAsyncAsQueryableWithNoTracking()
        {
            return Entities.AsQueryable().AsNoTracking();
        }



        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null,
            bool disableTracking = true)
        {
            var query = disableTracking ? TableNoTracking : Table;

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await orderBy(query).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            bool disableTracking = true)
        {
            var query = disableTracking ? TableNoTracking : Table;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }
    }