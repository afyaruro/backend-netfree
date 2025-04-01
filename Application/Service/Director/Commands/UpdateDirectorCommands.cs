
using Application.Common.Exceptions;
using Application.Service.Director.Dto;
using Application.Service.Director.Mapping;
using Application.Service.Country;
using Domain.Entity.Director;
using Domain.Port;

namespace Application.Service.Director.Commands
{
    public static class UpdateDirectorCommands
    {
        public static async Task<bool> UpdateDirector(IDirectorRepository repository, CountryService service, DirectorUpdateDto dto)
        {
            try
            {


                ValidateDirector.ValidateDirectorUpdateDto(dto);
                await service.GetById(dto.idCountry);

                DirectorEntity entity = MappingDirector.UpdateDtoToEntity(dto);

                var resp = await repository.Update(entity);

                if (!resp)
                {
                    throw new EntityNotUpdateException("No se ha actualizado el Director");
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