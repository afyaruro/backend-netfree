using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Country.Dto;
using Application.Service.Country.Mapping;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class GetByIdCountryCommands
    {

        public static async Task<ResponseEntity<CountryOutputDto>> GetById(ICountryRepository repository, int idCountry)
        {
            try
            {

                var resp = await repository.GetById(idCountry);

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se encontro el pais");
                }

                return new ResponseEntity<CountryOutputDto>("Pais Obtenido", MappingCountry.EntityToOutputDto(resp));

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}