
using Application.Common.Exceptions;
using Application.Service.Actor.Dto;
using Application.Service.Actor.Mapping;
using Application.Service.Country;
using Domain.Entity.Actor;
using Domain.Port;

namespace Application.Service.Actor.Commands
{
    public static class UpdateActorCommands
    {
        public static async Task<bool> UpdateActor(IActorRepository repository, CountryService service, ActorUpdateDto dto)
        {
            try
            {


                ValidateActor.ValidateActorUpdateDto(dto);
                await service.GetById(dto.idCountry);

                ActorEntity entity = MappingActor.UpdateDtoToEntity(dto);

                var resp = await repository.Update(entity);

                if (!resp)
                {
                    throw new EntityNotUpdateException("No se ha actualizado el actor");
                }

                return resp;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}