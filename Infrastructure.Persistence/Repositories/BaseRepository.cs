using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly MainContext _context;
        protected readonly DbSet<TEntity> _entity;

        public BaseRepository(MainContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                await _entity.AddAsync(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _entity.Remove(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.AsNoTracking().AsEnumerable();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        {
            var query = _entity.AsNoTracking().AsQueryable();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return query.AsEnumerable();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _entity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] properties)
        {
            var query = _entity.AsNoTracking().AsQueryable();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _entity.Update(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
