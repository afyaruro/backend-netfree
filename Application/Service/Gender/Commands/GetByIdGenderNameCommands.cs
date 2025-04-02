
using Application.Service.Gender.Dto;
using Application.Service.Gender.Mapping;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public class GetByIdGenderNameCommands
    {

        public static async Task<ResponseEntity<GenderOutputDto>> GetByIdGenderName(IGenderRepository repository, int idGender)
        {
            try
            {

                var resp = await repository.GetById(idGender);

                if (resp == null)
                {
                    return new ResponseEntity<GenderOutputDto>("No existe", true);
                }

                return new ResponseEntity<GenderOutputDto>("Encontrado", MappingGender.EntityToOutputDto(resp));


            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
