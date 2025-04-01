using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Country;
using Application.Service.Director;
using Application.Service.Gender;
using Application.Service.Movie;
using Application.Service.Movie.Dto;
using Application.Service.MovieActor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {

        private readonly MovieService _movieService;
        private readonly GenderService _genderService;
        private readonly CountryService _countryService;
        private readonly DirectorService _directorService;
        private readonly MovieActorService _movieActorService;


        public MovieController(MovieService movieService, GenderService genderService, CountryService countryService, DirectorService directorService, MovieActorService movieActorService)
        {
            _movieService = movieService;
            _genderService = genderService;
            _countryService = countryService;
            _directorService = directorService;
            _movieActorService = movieActorService;

        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieInputDto dto)
        {
            dto.codeTrailer = dto.codeTrailer.Replace("\"", "\\\"");
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos de la pelicula no son válidos" });

            try
            {
                var response = await _movieService.Create(dto, _countryService, _directorService, _genderService);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return CreatedAtAction(nameof(CreateMovie), new { success = true, message = response.message, data = response.entity });
            }

            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PaginationDto dto)
        {
            try
            {
                var response = await _movieService.GetAll(_countryService, _directorService, _genderService, _movieActorService, dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return Ok(new { success = true, message = response.message, totalRegistros = response.totalRecords, totalPages = response.totalPages, data = response.listEntity });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] MovieUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { success = false, message = "Datos de la pelicula no son válidos" });
            }

            try
            {
                var response = await _movieService.Update(dto, _countryService, _directorService, _genderService);

                return Ok(new { success = true, message = "La pelicula se ha actualizado exitosamente" });
            }
            catch (EntityNotUpdateException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

            catch (EntityNotFoundException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

            catch (BadRequestException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] MovieDeleteDto dto)
        {
            if (dto.Id <= 0)
                return BadRequest(new { success = false, message = "El identificador de la pelicula no es válido." });

            try
            {
                var result = await _movieService.Delete(dto.Id);
                return Ok(new { success = result, message = result == true ? "La pelicula se ha eliminado correctamente." : "No se ha eliminado" });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (EntityNotDelete ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

    }
}