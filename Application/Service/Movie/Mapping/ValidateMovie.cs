
using Application.Common.Exceptions;
using Application.Service.Movie.Dto;

namespace Application.Service.Movie.Mapping
{
    public static class ValidateMovie
    {
        public static void ValidateMovieInputDto(MovieInputDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.review))
            {
                throw new BadRequestException("La reseña de la pelicula no puede estar vacia.");
            }

            if (string.IsNullOrWhiteSpace(dto.title))
            {
                throw new BadRequestException("El titulo de la pelicula no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(dto.coverImage))
            {
                throw new BadRequestException("La reseña de la pelicula no puede estar vacia.");
            }

            if (string.IsNullOrWhiteSpace(dto.codeTrailer))
            {
                throw new BadRequestException("El trailer de la pelicula no puede estar vacío.");
            }

            if (dto.idCountry <= 0)
            {
                throw new BadRequestException("El país no es válido.");
            }

            if (dto.idGender <= 0)
            {
                throw new BadRequestException("El genero no es válido.");
            }

            if (dto.idDirector <= 0)
            {
                throw new BadRequestException("El director no es válido.");
            }
        }


        public static void ValidateMovieUpdateDto(MovieUpdateDto dto)
        {


            if (string.IsNullOrWhiteSpace(dto.review))
            {
                throw new BadRequestException("La reseña de la pelicula no puede estar vacia.");
            }

            if (string.IsNullOrWhiteSpace(dto.title))
            {
                throw new BadRequestException("El titulo de la pelicula no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(dto.coverImage))
            {
                throw new BadRequestException("La reseña de la pelicula no puede estar vacia.");
            }

            if (string.IsNullOrWhiteSpace(dto.codeTrailer))
            {
                throw new BadRequestException("El titulo de la pelicula no puede estar vacío.");
            }

            if (dto.idCountry <= 0)
            {
                throw new BadRequestException("El país no es válido.");
            }

            if (dto.idGender <= 0)
            {
                throw new BadRequestException("El genero no es válido.");
            }

            if (dto.idDirector <= 0)
            {
                throw new BadRequestException("El director no es válido.");
            }


        }
    }
}