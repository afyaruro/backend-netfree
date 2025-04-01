using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Actor;
using Application.Service.Country;
using Application.Service.Movie;
using Application.Service.MovieActor;
using Application.Service.MovieActor.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/movie-actor")]
    public class MovieActorController : ControllerBase
    {
        private readonly CountryService _countryService;
        private readonly MovieActorService _movieActorService;
        private readonly MovieService _movieService;
        private readonly ActorService _actorService;


        public MovieActorController(CountryService countryService, MovieActorService movieActorService, MovieService movieService, ActorService actorService)
        {
            _countryService = countryService;
            _movieActorService = movieActorService;
            _movieService = movieService;
            _actorService = actorService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Created([FromBody] MovieActorInputDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos del actor de la pelicula no son válidos" });

            try
            {
                var response = await _movieActorService.Create(dto, _countryService, _movieService, _actorService);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return CreatedAtAction(nameof(Created), new { success = true, message = response.message, data = response.entity });
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
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] MovieActorDeleteDto dto)
        {
            if (dto.Id <= 0)
                return BadRequest(new { success = false, message = "El identificador del actor de la elicula no es válido." });

            try
            {
                var result = await _actorService.Delete(dto.Id);
                return Ok(new { success = result, message = result == true ? "El actor de la pelicula se ha eliminado correctamente." : "No se ha eliminado" });
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