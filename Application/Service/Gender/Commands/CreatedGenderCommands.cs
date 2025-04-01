using Application.Common.Exceptions;
using Application.Service.Gender.Dto;
using Application.Service.Gender.Mapping;
using Domain.Entity.Gender;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public static class CreatedGenderCommands
    {
        public static async Task<ResponseEntity<GenderOutputDto>> CreateGender(IGenderRepository repository, GenderInputDto dto)
        {

            try
            {

                dto.name = dto.name.ToUpper();
                if (string.IsNullOrWhiteSpace(dto.name))
                {
                    throw new BadRequestException("El nombre del genero no puede estar vacío.");
                }

                if (await GetByNameGenderCommands.Exist(repository, dto.name))
                {
                    throw new EntityExistException("Ya se encuentra registrado");
                }

                var resp = repository.Add(MappingGender.InputDtoToEntity(dto));


                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado el pais");
                }

                return new ResponseEntity<GenderOutputDto>("Genero Creado", MappingGender.EntityToOutputDto(await resp));

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}