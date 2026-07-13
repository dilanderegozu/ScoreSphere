using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ScoreSphere.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<List<T>> GetListAsync();

        Task<List<T>> GetListAsync(params Expression<Func<T, object>>[] includes);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
