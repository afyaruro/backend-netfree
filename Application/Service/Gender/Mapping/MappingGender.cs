using Application.Service.Gender.Dto;
using Domain.Entity.Gender;

namespace Application.Service.Gender.Mapping
{
    public class MappingGender
    {
        public static GenderEntity InputDtoToEntity(GenderInputDto dto)
        {
            GenderEntity entity = new GenderEntity();
            entity.updateDate = DateTime.Now;
            entity.name = dto.name;
            return entity;
        }

        public static GenderEntity UpdateDtoToEntity(GenderUpdateDto dto)
        {
            GenderEntity entity = new GenderEntity();
            entity.updateDate = DateTime.Now;
            entity.name = dto.name;
            entity.Id = dto.Id;
            return entity;
        }

        public static GenderOutputDto EntityToOutputDto(GenderEntity entity)
        {
            GenderOutputDto output = new GenderOutputDto();
            output.Id = entity.Id;
            output.name = entity.name;
            return output;
        }

        public static List<GenderOutputDto> ListEntityToListOutputDto(List<GenderEntity> entities)
        {
            List<GenderOutputDto> outputList = new List<GenderOutputDto>();

            foreach (var entity in entities)
            {
                outputList.Add(EntityToOutputDto(entity));
            }

            return outputList;
        }
    }
}