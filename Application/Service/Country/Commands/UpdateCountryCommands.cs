using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Country.Dto;
using Application.Service.Country.Mapping;
using Domain.Entity.Country;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class UpdateCountryCommands
    {
        public static async Task<bool> UpdateCountry(ICountryRepository repository, CountryUpdateDto dto)
        {
            try
            {

                dto.name = dto.name.ToUpper();
                if (string.IsNullOrWhiteSpace(dto.name))
                {
                    throw new BadRequestException("El nombre del país no puede estar vacío.");
                }

                CountryEntity entity = MappingCountry.UpdateDtoToEntity(dto);

                var resp = await repository.Update(entity);

                if (!resp)
                {
                    throw new EntityNotUpdateException("No se ha actualizado el pais");
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