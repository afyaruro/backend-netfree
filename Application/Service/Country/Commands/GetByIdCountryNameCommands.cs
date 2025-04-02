
using Application.Service.Country.Dto;
using Application.Service.Country.Mapping;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class GetByIdCountryNameCommands
    {
        public static async Task<ResponseEntity<CountryOutputDto>> GetByIdCountryName(ICountryRepository repository, int idCountry)
        {
            try
            {

                var resp = await repository.GetById(idCountry);

                if (resp == null)
                {
                    return new ResponseEntity<CountryOutputDto>("No existe", true);
                }

                return new ResponseEntity<CountryOutputDto>("Encontrado", MappingCountry.EntityToOutputDto(resp));

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}