
using Domain.Entity.Gender;

namespace Domain.Port
{
    public interface IGenderRepository : IGenericRepository<GenderEntity>
    {
        Task<GenderEntity?> GetByName(string name);
    }
}