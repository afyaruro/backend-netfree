using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Director.Dto;
using Application.Service.Country;
using Domain.Port;

namespace Application.Service.Director.Mapping
{
    public static class ValidateDirector
    {
        public static void ValidateDirectorInputDto(DirectorInputDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.firsName))
            {
                throw new BadRequestException("El nombre del Director no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(dto.lastName))
            {
                throw new BadRequestException("El apellido del Director no puede estar vacío.");
            }

            if (dto.idCountry <= 0)
            {
                throw new BadRequestException("El país no es válido.");
            }
        }


        public static void ValidateDirectorUpdateDto(DirectorUpdateDto dto)
        {


            if (string.IsNullOrWhiteSpace(dto.firsName))
            {
                throw new BadRequestException("El nombre del Director no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(dto.lastName))
            {
                throw new BadRequestException("El apellido del Director no puede estar vacío.");
            }

            if (dto.idCountry <= 0)
            {
                throw new BadRequestException("El país no es válido.");
            }



        }
    }
}