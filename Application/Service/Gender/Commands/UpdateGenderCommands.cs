
using Application.Common.Exceptions;
using Application.Service.Gender.Dto;
using Application.Service.Gender.Mapping;
using Domain.Entity.Gender;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public static class UpdateGenderCommands
    {
        public static async Task<bool> UpdateGender(IGenderRepository repository, GenderUpdateDto dto)
        {
            try
            {

                dto.name = dto.name.ToUpper();
                if (string.IsNullOrWhiteSpace(dto.name))
                {
                    throw new BadRequestException("El nombre del genero no puede estar vacío.");
                }

                GenderEntity entity = MappingGender.UpdateDtoToEntity(dto);

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