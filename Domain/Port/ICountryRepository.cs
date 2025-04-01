
using Domain.Entity.Country;
using Domain.Entity.Response;

namespace Domain.Port
{
    public interface ICountryRepository : IGenericRepository<CountryEntity>
    {
        Task<CountryEntity?> GetByName(string name);
        Task<ResponseEntity<CountryEntity>> GetAll(int pageNumber, int pageSize);
    }
}