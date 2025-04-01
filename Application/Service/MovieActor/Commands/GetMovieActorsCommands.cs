
using Application.Common.Exceptions;
using Application.Service.MovieActor.Dto;
using Application.Service.MovieActor.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.MovieActor.Commands
{
    public static class GetMovieActorsCommands
    {
        public static async Task<List<string>> GetAllByIdMovie(IMovieActorRepository repository, int idMovie)
        {
            try
            {

                var resp = await repository.ActorsByMovieId(idMovie);

                return resp;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}