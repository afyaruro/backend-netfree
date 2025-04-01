using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Service.Country.Dto;
using Domain.Entity.Country;
using Domain.Entity.User;

namespace Application.Service.Country.Mapping
{
    public class MappingCountry
    {
        public static CountryEntity InputDtoToEntity(CountryInputDto dto)
        {
            CountryEntity entity = new CountryEntity();
            entity.updateDate = DateTime.Now;
            entity.name = dto.name;
            return entity;
        }

        public static CountryEntity UpdateDtoToEntity(CountryUpdateDto dto)
        {
            CountryEntity entity = new CountryEntity();
            entity.updateDate = DateTime.Now;
            entity.name = dto.name;
            entity.Id = dto.Id;
            return entity;
        }

        public static CountryOutputDto EntityToOutputDto(CountryEntity entity)
        {
            CountryOutputDto output = new CountryOutputDto();
            output.Id = entity.Id;
            output.name = entity.name;
            return output;
        }

        public static List<CountryOutputDto> ListEntityToListOutputDto(List<CountryEntity> entities)
        {
            List<CountryOutputDto> outputList = new List<CountryOutputDto>();

            foreach (var entity in entities)
            {
                outputList.Add(EntityToOutputDto(entity));
            }

            return outputList;
        }
    }
}