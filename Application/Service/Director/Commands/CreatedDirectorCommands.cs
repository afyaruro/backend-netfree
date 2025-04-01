using Application.Common.Exceptions;
using Application.Service.Director.Dto;
using Application.Service.Director.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Director.Commands
{
    public static class CreatedDirectorCommands
    {
        public static async Task<ResponseEntity<DirectorOutputDto>> CreateDirector(IDirectorRepository repository, CountryService countryService, DirectorInputDto dto)
        {

            try
            {

                ValidateDirector.ValidateDirectorInputDto(dto);
                var respCountry = await countryService.GetById(dto.idCountry);

                var resp = repository.Add(MappingDirector.InputDtoToEntity(dto));


                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado el director");
                }

                var outputDto = MappingDirector.EntityToOutputDto(await resp);
                outputDto.country = respCountry.entity!.name;

                return new ResponseEntity<DirectorOutputDto>("Director Creado", outputDto);

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}