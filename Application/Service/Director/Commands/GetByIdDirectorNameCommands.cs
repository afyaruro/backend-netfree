
using Domain.Port;

namespace Application.Service.Director.Commands
{
    public class GetByIdDirectorNameCommands
    {
        public static async Task<string> GetByIdDirectorName(IDirectorRepository repository, int idDirector)
        {
            try
            {

                var resp = await repository.GetById(idDirector);

                if (resp == null)
                {
                    return "El pais no existe";
                }

                return $"{resp.firsName} {resp.lastName}";

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
