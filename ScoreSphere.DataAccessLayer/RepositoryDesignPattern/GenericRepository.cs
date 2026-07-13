using Microsoft.EntityFrameworkCore;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ScoreSphere.DataAccessLayer.RepositoryDesignPattern
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly ApiContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApiContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private string GetPrimaryKeyName()
        {
            var keyName = _context.Model
                .FindEntityType(typeof(T))!
                .FindPrimaryKey()!
                .Properties[0].Name;

            return keyName;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
       
        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, GetPrimaryKeyName()) == id);
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetListAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();   
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
