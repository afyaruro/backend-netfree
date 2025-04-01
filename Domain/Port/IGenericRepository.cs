

using Domain.Entity.Response;

namespace Domain.Port
{
    public interface IGenericRepository<E>
    {
        Task<E?> GetById(int id);
        Task<E> Add(E entity);
        Task<bool> Update(E entity);
        Task<bool> Delete(int id);
        Task<ResponseEntity<E>> GetAll(int pageNumber, int pageSize);


    }
}
