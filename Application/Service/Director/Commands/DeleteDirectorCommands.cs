
using Application.Common.Exceptions;
using Domain.Port;

namespace Application.Service.Director.Commands
{
    public static class DeleteDirectorCommands
    {
        public static async Task<bool> DeleteDirector(IDirectorRepository repository, int idDirector)
        {
            try
            {

                var resp = await repository.Delete(idDirector);

                if (!resp)
                {
                    throw new EntityNotDelete("No se encontro el registro");
                }

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}