
using Application.Common.Exceptions;
using Application.Service.Director.Dto;
using Application.Service.Director.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Director.Commands
{
    public static class GetByIdDirectorCommands
    {

        public static async Task<ResponseEntity<DirectorOutputDto>> GetById(IDirectorRepository repository, CountryService service, int idDirector)
        {
            try
            {

                var resp = await repository.GetById(idDirector);

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se encontro el Director");
                }

                var output = MappingDirector.EntityToOutputDto(resp);

                var countryResp = await service.GetByIdCountryName(resp.idCountry);
                output.country = countryResp.entity != null ? countryResp.entity.name : countryResp.message;

                return new ResponseEntity<DirectorOutputDto>("Director Obtenido", output);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}