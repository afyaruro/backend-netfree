using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Actor.Dto;
using Application.Service.Country;
using Domain.Port;

namespace Application.Service.Actor.Mapping
{
    public static class ValidateActor
    {
        public static void ValidateActorInputDto(ActorInputDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.firsName))
            {
                throw new BadRequestException("El nombre del actor no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(dto.lastName))
            {
                throw new BadRequestException("El apellido del actor no puede estar vacío.");
            }

            if (dto.idCountry <= 0)
            {
                throw new BadRequestException("El país no es válido.");
            }
        }


        public static void ValidateActorUpdateDto(ActorUpdateDto dto)
        {


            if (string.IsNullOrWhiteSpace(dto.firsName))
            {
                throw new BadRequestException("El nombre del actor no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(dto.lastName))
            {
                throw new BadRequestException("El apellido del actor no puede estar vacío.");
            }

            if (dto.idCountry <= 0)
            {
                throw new BadRequestException("El país no es válido.");
            }



        }
    }
}