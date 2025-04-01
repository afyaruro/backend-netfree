
using System.Threading.Tasks;
using Application.Service.Actor.Dto;
using Application.Service.Country;
using Domain.Entity.Actor;

namespace Application.Service.Actor.Mapping
{
    public class MappingActor
    {
        public static ActorEntity InputDtoToEntity(ActorInputDto dto)
        {
            ActorEntity entity = new ActorEntity();
            entity.updateDate = DateTime.Now;
            entity.firsName = dto.firsName;
            entity.lastName = dto.lastName;
            entity.idCountry = dto.idCountry;
            return entity;
        }

        public static ActorEntity UpdateDtoToEntity(ActorUpdateDto dto)
        {
            ActorEntity entity = new ActorEntity();
            entity.updateDate = DateTime.Now;
            entity.firsName = dto.firsName;
            entity.lastName = dto.lastName;
            entity.idCountry = dto.idCountry;
            entity.Id = dto.Id;
            return entity;
        }

        public static ActorOutputDto EntityToOutputDto(ActorEntity entity)
        {
            ActorOutputDto output = new ActorOutputDto();
            output.Id = entity.Id;
            output.firsName = entity.firsName;
            output.lastName = entity.lastName;
            return output;
        }

        public static async Task<List<ActorOutputDto>> ListEntityToListOutputDto(List<ActorEntity> entities, CountryService service)
        {
            List<ActorOutputDto> outputList = new List<ActorOutputDto>();

            foreach (var entity in entities)
            {

                var outputDto = EntityToOutputDto(entity);
                var respCountry = await service.GetByIdCountryName(entity.idCountry);
                outputDto.country = respCountry;
                outputList.Add(outputDto);
            }

            return outputList;
        }
    }
}