
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public class GetByIdGenderNameCommands
    {

        public static async Task<string> GetByIdGenderName(IGenderRepository repository, int idGender)
        {
            try
            {

                var resp = await repository.GetById(idGender);

                if (resp == null)
                {
                    return "El genero no existe";
                }

                return resp.name;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
