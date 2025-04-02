
using Application.Service.Country;
using Application.Service.Director.Dto;
using Application.Service.Director.Mapping;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Director.Commands
{
    public class GetByIdDirectorNameCommands
    {
        public static async Task<ResponseEntity<DirectorOutputDto>> GetByIdDirectorName(IDirectorRepository repository, int idDirector, CountryService countryService)
        {
            try
            {

                var resp = await repository.GetById(idDirector);

                if (resp == null)
                {
                    return new ResponseEntity<DirectorOutputDto>("No existe", true);
                }
                var respDirector = new ResponseEntity<DirectorOutputDto>("Encontrado", MappingDirector.EntityToOutputDto(resp));
                var countryResp = await countryService.GetByIdCountryName(resp.idCountry);
                respDirector.entity!.country = countryResp.entity != null ? countryResp.entity.name : countryResp.message;

                return respDirector;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
