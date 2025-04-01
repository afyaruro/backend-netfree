using Application.Common.Exceptions;
using Application.Service.Country.Dto;
using Application.Service.Country.Mapping;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class CreatedCountryCommands
    {
        public static async Task<ResponseEntity<CountryOutputDto>> CreateCountry(ICountryRepository repository, CountryInputDto dto)
        {

            try
            {

                dto.name = dto.name.ToUpper();
                if (string.IsNullOrWhiteSpace(dto.name))
                {
                    throw new BadRequestException("El nombre del país no puede estar vacío.");
                }

                if (await GetByNameCommands.Exist(repository, dto.name))
                {
                    throw new EntityExistException("Ya se encuentra registrado");
                }

                var resp = repository.Add(MappingCountry.InputDtoToEntity(dto));


                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado el pais");
                }

                return new ResponseEntity<CountryOutputDto>("Pais Creado", MappingCountry.EntityToOutputDto(await resp));

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}