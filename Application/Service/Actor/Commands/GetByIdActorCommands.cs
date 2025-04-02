
using Application.Common.Exceptions;
using Application.Service.Actor.Dto;
using Application.Service.Actor.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Actor.Commands
{
    public static class GetByIdActorCommands
    {

        public static async Task<ResponseEntity<ActorOutputDto>> GetById(IActorRepository repository, CountryService service, int idActor)
        {
            try
            {

                var resp = await repository.GetById(idActor);

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se encontro el actor");
                }

                var output = MappingActor.EntityToOutputDto(resp);
                var countryResp = await service.GetByIdCountryName(resp.idCountry);
                output.country = countryResp.entity != null ? countryResp.entity.name : countryResp.message;
                return new ResponseEntity<ActorOutputDto>("Actor Obtenido", output);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}