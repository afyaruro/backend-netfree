
using Application.Common.Exceptions;
using Domain.Port;

namespace Application.Service.MovieActor.Commands
{
    public static class DeleteMovieActorCommands
    {
        public static async Task<bool> DeleteMovieActor(IMovieActorRepository repository, int idMovieActor)
        {
            try
            {

                var resp = await repository.Delete(idMovieActor);

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