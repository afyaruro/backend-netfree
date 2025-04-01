using Application.Common.Exceptions;
using Application.Service.MovieActor.Dto;
using Application.Service.MovieActor.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.Movie;
using Application.Service.Actor;

namespace Application.Service.MovieActor.Commands
{
    public static class CreatedMovieActorCommands
    {
        public static async Task<ResponseEntity<MovieActorOutputDto>> CreateMovieActor(IMovieActorRepository repository, CountryService countryService, MovieService movieService, ActorService actorService, MovieActorInputDto dto)
        {

            try
            {

                ValidateMovieActor.ValidateMovieActorInputDto(dto);
                var respMovie = await movieService.GetById(dto.idMovie, countryService);
                var respActor = await actorService.GetById(dto.idActor, countryService);

                if (await repository.MovieActorExists(dto.idMovie, dto.idActor))
                {
                    throw new EntityExistException("El actor ya se encuentra registrado en la pelicula");
                }


                var resp = repository.Add(MappingMovieActor.InputDtoToEntity(dto));

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado el actor de la pelicula");
                }

                var outputDto = MappingMovieActor.EntityToOutputDto(await resp);
                outputDto.movie = respMovie.entity!.title;
                outputDto.actor = $"{respActor.entity!.firsName} {respActor.entity!.lastName}";

                return new ResponseEntity<MovieActorOutputDto>("Actor de la pelicula Creado", outputDto);

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}