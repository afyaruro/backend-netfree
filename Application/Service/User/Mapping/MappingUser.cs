using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Service.User.Created.Dto;
using Domain.Entity.User;

namespace Application.Service.User.Created.Mapping
{
    public static class MappingUser
    {
        public static UserEntity InputDtoToEntity(UserInputDto dto)
        {
            UserEntity entity = new UserEntity();
            entity.mail = dto.mail;
            entity.password = dto.password;
            entity.firsName = dto.firsName;
            entity.lastName = dto.lastName;
            entity.updateDate = DateTime.Now;
            entity.genero = dto.genero;
            return entity;
        }

        public static UserOutputDto EntityToOutputDto(UserEntity entity)
        {
            UserOutputDto output = new UserOutputDto();
            output.mail = entity.mail;
            output.firsName = entity.firsName;
            output.lastName = entity.lastName;
            output.genero = entity.genero;
            return output;
        }

        public static List<UserOutputDto> ListEntityToListOutputDto(List<UserEntity> entities)
        {
            List<UserOutputDto> outputList = new List<UserOutputDto>();

            foreach (var entity in entities)
            {
                outputList.Add(EntityToOutputDto(entity));
            }

            return outputList;
        }


    }
}