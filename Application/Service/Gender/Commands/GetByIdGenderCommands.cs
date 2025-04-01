
using Application.Common.Exceptions;
using Application.Service.Gender.Dto;
using Application.Service.Gender.Mapping;
using Domain.Entity.Gender;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public static class GetByIdGenderCommands
    {

        public static async Task<ResponseEntity<GenderOutputDto>> GetById(IGenderRepository repository, int idGender)
        {
            try
            {

                var resp = await repository.GetById(idGender);

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se encontro el genero");
                }

                return new ResponseEntity<GenderOutputDto>("Genero Obtenido", MappingGender.EntityToOutputDto(resp)); ;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}