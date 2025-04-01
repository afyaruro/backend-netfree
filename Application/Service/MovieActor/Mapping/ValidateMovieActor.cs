
using Application.Common.Exceptions;
using Application.Service.MovieActor.Dto;

namespace Application.Service.MovieActor.Mapping
{
    public static class ValidateMovieActor
    {
        public static void ValidateMovieActorInputDto(MovieActorInputDto dto)
        {


            if (dto.idActor <= 0)
            {
                throw new BadRequestException("El actor no es válido.");
            }

            if (dto.idMovie <= 0)
            {
                throw new BadRequestException("La pelicula no es válida.");
            }
        }


        public static void ValidateMovieActorUpdateDto(MovieActorUpdateDto dto)
        {


            if (dto.idActor <= 0)
            {
                throw new BadRequestException("El actor no es válido.");
            }

            if (dto.idMovie <= 0)
            {
                throw new BadRequestException("La pelicula no es válida.");
            }
        }



    }
}
