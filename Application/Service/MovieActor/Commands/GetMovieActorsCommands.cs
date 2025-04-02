
using Application.Common.Exceptions;
using Application.Service.MovieActor.Dto;
using Application.Service.MovieActor.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.Actor.Dto;
using Application.Service.Actor.Mapping;

namespace Application.Service.MovieActor.Commands
{
    public static class GetMovieActorsCommands
    {
        public static async Task<List<ActorOutputDto>> GetAllByIdMovie(IMovieActorRepository repository, int idMovie, CountryService countryService)
        {
            try
            {

                var resp = await repository.ActorsByMovieId(idMovie);

                return await MappingActor.ListEntityToListOutputDto(resp, countryService);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}