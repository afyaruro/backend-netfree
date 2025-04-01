
using Application.Common.Exceptions;
using Domain.Port;

namespace Application.Service.Movie.Commands
{
    public static class DeleteMovieCommands
    {
        public static async Task<bool> DeleteMovie(IMovieRepository repository, int idMovie)
        {
            try
            {

                var resp = await repository.Delete(idMovie);

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