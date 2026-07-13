using System.Linq.Expressions;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<List<T>> TGetListAsync();
        Task<List<T>> TGetListAsync(params Expression<Func<T, object>>[] includes);

        Task<T?> TGetByIdAsync(int id);
        Task<T?> TGetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task TInsertAsync(T t);
        Task TUpdateAsync(T t);
        Task TDeleteAsync(int id);
    }
}