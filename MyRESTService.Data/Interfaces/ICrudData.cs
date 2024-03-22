using System.Collections.Generic;

namespace MyRESTService.Data.Interfaces
{
    public interface ICrudData<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T entity);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
    }
}
