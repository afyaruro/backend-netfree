
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
                output.country = await service.GetByIdCountryName(resp.idCountry);
                return new ResponseEntity<DirectorOutputDto>("Director Obtenido", output);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}