using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using System.Linq.Expressions;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericDal<T> _repository;

        public GenericManager(IGenericDal<T> repository)
        {
            _repository = repository;
        }

        public async Task<List<T>> TGetListAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<List<T>> TGetListAsync(params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetListAsync(includes);
        }

        public async Task<T?> TGetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T?> TGetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task TInsertAsync(T t)
        {
            await _repository.InsertAsync(t);
        }

        public async Task TUpdateAsync(T t)
        {
            await _repository.UpdateAsync(t);
        }

        public async Task TDeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}