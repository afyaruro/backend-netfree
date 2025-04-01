using Application.Service.MovieActor.Dto;
using Application.Service.Country;
using Domain.Entity.MovieActor;

namespace Application.Service.MovieActor.Mapping
{
    public class MappingMovieActor
    {
        public static MovieActorEntity InputDtoToEntity(MovieActorInputDto dto)
        {
            MovieActorEntity entity = new MovieActorEntity();
            entity.updateDate = DateTime.Now;
            entity.idMovie = dto.idMovie;
            entity.idActor = dto.idActor;

            return entity;
        }

        public static MovieActorEntity UpdateDtoToEntity(MovieActorUpdateDto dto)
        {
            MovieActorEntity entity = new MovieActorEntity();
            entity.updateDate = DateTime.Now;
            entity.idMovie = dto.idMovie;
            entity.idActor = dto.idActor;
            entity.Id = dto.Id;
            return entity;
        }

        public static MovieActorOutputDto EntityToOutputDto(MovieActorEntity entity)
        {
            MovieActorOutputDto output = new MovieActorOutputDto();
            output.Id = entity.Id;
            return output;
        }


    }
}