
using System.Net;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Actor;
using Application.Service.Actor.Dto;
using Application.Service.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/actor")]
    public class ActorController : ControllerBase
    {
        private readonly CountryService _countryService;
        private readonly ActorService _actorService;


        public ActorController(CountryService countryService, ActorService actorService)
        {
            _countryService = countryService;
            _actorService = actorService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] ActorInputDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos del actor no son válidos" });

            try
            {
                var response = await _actorService.Create(dto, _countryService);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return CreatedAtAction(nameof(Create), new { success = true, message = response.message, data = response.entity });
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
                var response = await _actorService.GetAll(_countryService, dto);

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
        public async Task<IActionResult> Update([FromBody] ActorUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { success = false, message = "Datos del actor no son válidos" });
            }

            try
            {
                var response = await _actorService.Update(dto, _countryService);

                return Ok(new { success = true, message = "El actor se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] ActorDeleteDto dto)
        {
            if (dto.Id <= 0)
                return BadRequest(new { success = false, message = "El identificador del actor no es válido." });

            try
            {
                var result = await _actorService.Delete(dto.Id);
                return Ok(new { success = result, message = result == true ? "El actor se ha eliminado correctamente." : "No se ha eliminado" });
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