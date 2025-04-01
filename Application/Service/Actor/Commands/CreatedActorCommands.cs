using Application.Common.Exceptions;
using Application.Service.Actor.Dto;
using Application.Service.Actor.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Actor.Commands
{
    public static class CreatedActorCommands
    {
        public static async Task<ResponseEntity<ActorOutputDto>> CreateActor(IActorRepository repository, CountryService countryService, ActorInputDto dto)
        {

            try
            {

                ValidateActor.ValidateActorInputDto(dto);
                var respCountry = await countryService.GetById(dto.idCountry);

                var resp = repository.Add(MappingActor.InputDtoToEntity(dto));


                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado el actor");
                }

                var outputDto = MappingActor.EntityToOutputDto(await resp);
                outputDto.country = respCountry.entity!.name;

                return new ResponseEntity<ActorOutputDto>("actor Creado", outputDto);

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}