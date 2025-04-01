using Application.Service.Director.Dto;
using Application.Service.Country;
using Domain.Entity.Director;

namespace Application.Service.Director.Mapping
{
    public class MappingDirector
    {
        public static DirectorEntity InputDtoToEntity(DirectorInputDto dto)
        {
            DirectorEntity entity = new DirectorEntity();
            entity.updateDate = DateTime.Now;
            entity.firsName = dto.firsName;
            entity.lastName = dto.lastName;
            entity.idCountry = dto.idCountry;
            return entity;
        }

        public static DirectorEntity UpdateDtoToEntity(DirectorUpdateDto dto)
        {
            DirectorEntity entity = new DirectorEntity();
            entity.updateDate = DateTime.Now;
            entity.firsName = dto.firsName;
            entity.lastName = dto.lastName;
            entity.idCountry = dto.idCountry;
            entity.Id = dto.Id;
            return entity;
        }

        public static DirectorOutputDto EntityToOutputDto(DirectorEntity entity)
        {
            DirectorOutputDto output = new DirectorOutputDto();
            output.Id = entity.Id;
            output.firsName = entity.firsName;
            output.lastName = entity.lastName;
            return output;
        }

        public static async Task<List<DirectorOutputDto>> ListEntityToListOutputDto(List<DirectorEntity> entities, CountryService service)
        {
            List<DirectorOutputDto> outputList = new List<DirectorOutputDto>();

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