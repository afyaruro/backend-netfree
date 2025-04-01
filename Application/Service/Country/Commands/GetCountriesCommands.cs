using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Country.Dto;
using Application.Service.Country.Mapping;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class GetCountriesCommands
    {
        public static async Task<ResponseEntity<CountryOutputDto>> GetAll(ICountryRepository repository, PaginationDto dto)
        {
            try
            {

                var resp = await repository.GetAll(dto.pageNumber, dto.pageSize);

                if (resp.listEntity!.Count == 0 || resp == null)
                {
                    throw new EntityNotFoundException("No se encontraron registros");
                }

                var outputResponse = new ResponseEntity<CountryOutputDto>("paises obtenidos", MappingCountry.ListEntityToListOutputDto(resp.listEntity));
                outputResponse.totalPages = resp.totalPages;
                outputResponse.totalRecords = resp.totalRecords;

                return outputResponse;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}